using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;

public class ReclculateSmoothNormals : Editor
{
    [MenuItem("Tools/模型平滑法线写入切线")]
    public static void RecalculateSmoothNormal()
    {
        GameObject gameObject = Selection.activeGameObject;
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        foreach (var meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            WriteAverageNormalToTangent(mesh);
        }

        SkinnedMeshRenderer[] skinMeshRenders = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var skinMeshRender in skinMeshRenders)
        {
            Mesh mesh = skinMeshRender.sharedMesh;
            WriteAverageNormalToTangent(mesh);
        }
    }

    public static void WriteAverageNormalToTangent(Mesh mesh)
    {
        var averageNormalHash = new Dictionary<Vector3, Vector3>();
        for (var i = 0; i < mesh.vertexCount; i++)
        {
            if (!averageNormalHash.ContainsKey(mesh.vertices[i]))
            {
                averageNormalHash.Add(mesh.vertices[i], mesh.normals[i]);
            }
            else
            {
                averageNormalHash[mesh.vertices[i]] = (averageNormalHash[mesh.vertices[i]] + mesh.normals[i]).normalized;
            }
        }
        
        var averageNormals = new Vector3[mesh.vertexCount];
        for (var i = 0; i < mesh.vertexCount; i++)
        {
            averageNormals[i] = averageNormalHash[mesh.vertices[i]];
        }

        var tangents = new Vector4[mesh.vertexCount];
        for (var i = 0; i < mesh.vertexCount; i++)
        {
            tangents[i] = new Vector4(averageNormals[i].x, averageNormals[i].y, averageNormals[i].z, 0);
        }

        mesh.tangents = tangents;
    }
}
