using UnityEngine;

public class ComponentController : MonoBehaviour
{
    public GameObject[] componentsToShow;
    public GameObject[] componentsToHide;

    void Start()
    {
        SetComponentsActive(componentsToShow, true);
        SetComponentsActive(componentsToHide, false);
    }

    private void SetComponentsActive(GameObject[] components, bool isActive)
    {
        foreach (GameObject component in components)
        {
            if (component != null)
            {
                component.SetActive(isActive);
            }
        }
    }
}
