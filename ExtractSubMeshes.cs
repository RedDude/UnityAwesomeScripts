//Takes all submeshes and creates new child game objects with the extracted meshes.
//Assign to gameobject with mesh rendered or mesh collider that has desired meshes to extract,
//Click activateRender or ActivateCollider depending on the need.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExtractSubMeshes : MonoBehaviour
{
    public Mesh mesh;
    private int[] subMeshTris;
    public bool activateRenderMesh;
    public bool activateColliderMesh;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        if (activateRenderMesh)
        {
            activateRenderMesh = false;
            MeshFilter mf = this.GetComponent<MeshFilter>();
            mesh = mf.sharedMesh;

            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                GameObject go = new GameObject(mesh.name + "_SubMesh_" + i, typeof(MeshFilter), typeof(MeshRenderer));
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                subMeshTris = mesh.GetTriangles(i);
                go.GetComponent<MeshFilter>().sharedMesh = CreateMesh(subMeshTris, i);
            }
        }
        if (activateColliderMesh)
        {
            activateColliderMesh = false;
            MeshCollider mc = this.GetComponent<MeshCollider>();
            mesh = mc.sharedMesh;

            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                GameObject go = new GameObject(mesh.name + "_Collider_" + i, typeof(MeshFilter), typeof(MeshRenderer));
                go.transform.parent = transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                subMeshTris = mesh.GetTriangles(i);
                go.GetComponent<MeshFilter>().sharedMesh = CreateMesh(subMeshTris, i);
            }
        }
    }

    Mesh CreateMesh(int[] triangles, int index)
    {
        Mesh newMesh = new Mesh();
        List<int> vertexIndices = new List<int>();
        List<Vector3> verts = new List<Vector3>();
        List<Color> colors = new List<Color>();
        List<Vector3> normals = new List<Vector3>();

        List<Vector2> uvs, uvs2, uvs3, uvs4;
        uvs = new List<Vector2>(); uvs2 = new List<Vector2>(); uvs3 = new List<Vector2>(); uvs4 = new List<Vector2>();
        List<int> tris = new List<int>();

        newMesh.Clear();
        int curVertIndex = 0;
        int newVertIndex;
        int curSubVertIndex = 0;
        for (int i = 0; i < triangles.Length; i++)
        {
            curVertIndex = triangles[i];

            if (!vertexIndices.Contains(curVertIndex))
            {
                newVertIndex = curSubVertIndex;
                vertexIndices.Add(curVertIndex);

                verts.Add(mesh.vertices[curVertIndex]);

                if (mesh.colors != null && mesh.colors.Length > curVertIndex)
                    colors.Add(mesh.colors[curVertIndex]);

                normals.Add(mesh.normals[curVertIndex]);

                if (mesh.uv != null && mesh.uv.Length > curVertIndex)
                    uvs.Add(mesh.uv[curVertIndex]);
                if (mesh.uv2 != null && mesh.uv2.Length > curVertIndex)
                    uvs2.Add(mesh.uv2[curVertIndex]);
                if (mesh.uv3 != null && mesh.uv3.Length > curVertIndex)
                    uvs3.Add(mesh.uv3[curVertIndex]);
                if (mesh.uv4 != null && mesh.uv4.Length > curVertIndex)
                    uvs4.Add(mesh.uv4[curVertIndex]);

                curSubVertIndex++;
            }
            else
            {
                newVertIndex = vertexIndices.IndexOf(curVertIndex);
            }

            tris.Add(newVertIndex);
        }

        newMesh.vertices = verts.ToArray();
        newMesh.triangles = tris.ToArray();
        if (uvs.Count > 0)
            newMesh.uv = uvs.ToArray();
        if (uvs2.Count > 0)
            newMesh.uv2 = uvs2.ToArray();
        if (uvs3.Count > 0)
            newMesh.uv3 = uvs3.ToArray();
        if (uvs4.Count > 0)
            newMesh.uv4 = uvs4.ToArray();
        if (colors.Count > 0)
            newMesh.colors = colors.ToArray();

        newMesh.Optimize();
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();

        return newMesh;
        //AssetDatabase.CreateAsset(newMesh, "Assets/"+mesh.name+"_submesh["+index+"].asset");
    }
}