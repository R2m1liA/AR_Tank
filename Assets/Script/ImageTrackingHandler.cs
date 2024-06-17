using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageAndPlaneTrackingHandler : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARTrackedImageManager trackedImageManager;
    public GameObject playerTankPrefab;
    public GameObject aiTankPrefab;
    public GameObject obstaclePrefab;

    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, int> aiTankCount = new Dictionary<string, int>();
    public int aiTankLimit = 5; // 每个AI生成点的生成上限
    public float aiSpawnInterval = 10f; // AI坦克生成间隔时间

    void Awake()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            InstantiatePrefab(trackedImage);
            if (trackedImage.referenceImage.name == "AISpawn")
            {
                StartCoroutine(SpawnAITanks(trackedImage));
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdatePrefab(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            RemovePrefab(trackedImage);
        }
    }

    void InstantiatePrefab(ARTrackedImage trackedImage)
    {
        GameObject prefab = null;
        switch (trackedImage.referenceImage.name)
        {
            case "PlayerSpawn":
                prefab = playerTankPrefab;
                break;
            case "AISpawn":
                prefab = aiTankPrefab;
                break;
            case "Obstacle":
                prefab = obstaclePrefab;
                break;
        }

        if (prefab != null && !instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;

            ARPlane nearestPlane = FindNearestPlane(position);
            if (nearestPlane != null)
            {
                position.y = nearestPlane.center.y;
            }

            GameObject instantiatedPrefab = Instantiate(prefab, position, rotation);
            instantiatedPrefab.transform.parent = trackedImage.transform;
            instantiatedPrefabs[trackedImage.referenceImage.name] = instantiatedPrefab;

            if (trackedImage.referenceImage.name == "AISpawn")
            {
                aiTankCount[trackedImage.referenceImage.name] = 0;
            }
        }
    }

    void UpdatePrefab(ARTrackedImage trackedImage)
    {
        if (instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
        {
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
        }
    }

    void RemovePrefab(ARTrackedImage trackedImage)
    {
        if (instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
        {
            Destroy(prefab);
            instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }

    ARPlane FindNearestPlane(Vector3 position)
    {
        ARPlane nearestPlane = null;
        float minDistance = float.MaxValue;

        foreach (ARPlane plane in planeManager.trackables)
        {
            float distance = Vector3.Distance(position, plane.center);
            if (distance < minDistance)
            {
                nearestPlane = plane;
                minDistance = distance;
            }
        }

        return nearestPlane;
    }

    System.Collections.IEnumerator SpawnAITanks(ARTrackedImage trackedImage)
    {
        while (aiTankCount[trackedImage.referenceImage.name] < aiTankLimit)
        {
            yield return new WaitForSeconds(aiSpawnInterval);

            if (instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject aiSpawnPrefab))
            {
                Vector3 position = trackedImage.transform.position;
                Quaternion rotation = trackedImage.transform.rotation;

                ARPlane nearestPlane = FindNearestPlane(position);
                if (nearestPlane != null)
                {
                    position.y = nearestPlane.center.y;
                }

                GameObject aiTank = Instantiate(aiTankPrefab, position, rotation);
                aiTank.GetComponent<AITankBehavior>().StartPatrolling(); // 启动AI坦克巡逻逻辑
                aiTankCount[trackedImage.referenceImage.name]++;
            }
        }
    }
}
