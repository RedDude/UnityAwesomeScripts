#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Mesh/Vert Handler")]
[ExecuteInEditMode]
public class VertHandler : MonoBehaviour
{


    public VertGroup selectedHandles;
    public bool groupSelected; //group the selected set of verticies, add them to the groups list
    public bool removeFromGroup; // removes the selected objects from a given group
    public bool removeFromAll; // removes the selected objects from all groups
    public List<VertGroup> groups; //list of vertex groups
    public bool destroy;

    private Mesh mesh;
    private Vector3[] verts;
    private Vector3 vertPos;
    private VertHandleGizmo[] handles;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        verts = GetUniqueVerticies(mesh.vertices);
        if (handles == null)
        {
            foreach (Vector3 vert in verts)
            {
                vertPos = transform.TransformPoint(vert);
                GameObject handle = new GameObject("VertHandle");
                handle.transform.position = vertPos;
                handle.transform.parent = transform;
                VertHandleGizmo gizmo = handle.AddComponent<VertHandleGizmo>();
                gizmo.parent = this;
            }
        }
    }

    void OnDrawGizmos()
    {
        //display a list of the currently selected vertex handles
        handles = this.gameObject.GetComponentsInChildren<VertHandleGizmo>();
        if (selectedHandles == null)
            return;
        if (selectedHandles.vertHandles == null)
            return;
        selectedHandles.vertHandles.Clear();
        foreach (VertHandleGizmo handle in handles)
        {
            if (handle.selected)
            {
                selectedHandles.vertHandles.Add(handle.gameObject);
            }
        }
    }

    void Update()
    {
        if (destroy)
        {
            destroy = false;
            DestroyImmediate(this);
            return;
        }



        if (groupSelected)
        {
            groupSelected = false;
            bool newGroup = true;
            //for all existing groups
            foreach (VertGroup group in groups)
            {
                //if this group has the same name as what's in the selected group
                if (group.name == selectedHandles.name)
                {
                    //this group already exists
                    newGroup = false;
                    //add any new edges to the group
                    foreach (GameObject vertHandle in selectedHandles.vertHandles)
                    {
                        if (!group.vertHandles.Contains(vertHandle))
                        {
                            group.vertHandles.Add(vertHandle);
                        }
                        //make the edge know about the group it now belongs to
                        VertHandleGizmo gizmo = vertHandle.GetComponent<VertHandleGizmo>();
                        if (!gizmo.HasGroup(group))
                        {
                            gizmo.groups.Add(group);
                        }
                    }
                    //set the group color
                    group.color = selectedHandles.color;
                }

            }
            if (newGroup)
            {
                VertGroup g = new VertGroup(selectedHandles);// selectedHandles.name, selectedHandles.edges, selectedHandles.color);
                groups.Add(g);
            }
        }

        if (removeFromGroup)
        {
            removeFromGroup = false;
            foreach (VertGroup group in groups)
            {
                if (group.name == selectedHandles.name)
                {
                    foreach (GameObject vertHandle in selectedHandles.vertHandles)
                    {
                        if (group.vertHandles.Contains(vertHandle))
                        {
                            group.vertHandles.Remove(vertHandle);
                            VertHandleGizmo gizmo = vertHandle.GetComponent<VertHandleGizmo>();
                            if (gizmo.groups.Contains(group))
                            {
                                gizmo.groups.Remove(group);
                            }
                        }
                    }
                }
            }
            CleanGroups();
        }

        if (removeFromAll)
        {
            removeFromAll = false;
            foreach (VertGroup group in groups)
            {
                foreach (GameObject vertHandle in selectedHandles.vertHandles)
                {
                    if (group.vertHandles.Contains(vertHandle))
                    {
                        group.vertHandles.Remove(vertHandle);
                        VertHandleGizmo gizmo = vertHandle.GetComponent<VertHandleGizmo>();
                        if (gizmo.groups.Contains(group))
                        {
                            gizmo.groups.Remove(group);
                        }
                    }
                }
            }
            CleanGroups();
        }


        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = handles[i].transform.localPosition;
        }
        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }


    //clean out groups that have no edges in them
    void CleanGroups()
    {
        List<VertGroup> toRemove = new List<VertGroup>();
        foreach (VertGroup group in groups)
        {
            if (group.vertHandles.Count == 0)
            {
                toRemove.Add(group);
            }
        }
        foreach (VertGroup group in toRemove)
        {
            groups.Remove(group);
        }
    }

    Vector3[] GetUniqueVerticies(Vector3[] verticies)
    {
        List<Vector3> uniqueVerts = new List<Vector3>();

        foreach (Vector3 vert in verticies)
        {
            if (!uniqueVerts.Contains(vert))
            {
                uniqueVerts.Add(vert);
            }
        }


        return uniqueVerts.ToArray();
    }
}

[System.Serializable]
public class VertGroup
{

    public string name;
    public Color color = Color.white;
    public List<GameObject> vertHandles;


    public VertGroup(VertGroup selectedGroup)
    {
        name = selectedGroup.name;
        vertHandles = new List<GameObject>();
        color = selectedGroup.color;
        foreach (GameObject vertHandle in selectedGroup.vertHandles)
        {
            vertHandles.Add(vertHandle);
            VertHandleGizmo gizmo = vertHandle.GetComponent<VertHandleGizmo>();
            gizmo.groups.Add(this);
        }
    }


}

[ExecuteInEditMode]
public class VertHandleGizmo : MonoBehaviour
{

    private static float CURRENT_SIZE = 0.1f;

    public float _size = CURRENT_SIZE;
    public VertHandler parent;
    public bool selected = false;
    public bool destroy;
    private Color color = Color.white;
    public List<VertGroup> groups = new List<VertGroup>(); // the groups this gizmo belongs to
    private float lastKnownSize = CURRENT_SIZE;

    private bool first = true;

    void Update()
    {
        // Change the size if the user requests it
        if (lastKnownSize != _size)
        {
            lastKnownSize = _size;
            CURRENT_SIZE = _size;
        }

        // Ensure the rest of the gizmos know the size has changed...
        if (CURRENT_SIZE != lastKnownSize)
        {
            lastKnownSize = CURRENT_SIZE;
            _size = lastKnownSize;
        }

     
        if (first)
        {
            Vector3 toCenter = (parent.transform.position - transform.position).normalized;
            Vector3 up = Vector3.Cross(Vector3.right, toCenter).normalized;
            Vector3 right = Vector3.Cross(toCenter, up).normalized;
            float radius = CURRENT_SIZE;

            //draw an icon for each group this vertex belongs to
            float angleStep = 360f / groups.Count;
            for (int i = 0; i < groups.Count; i++)
            {
                VertGroup group = groups[i];
                Gizmos.color = group.color;
                float angle = angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                float x = Mathf.Cos(rad) * radius;
                float y = Mathf.Sin(rad) * radius;
                Vector3 position = transform.position + right * x + up * y;

                Gizmos.DrawCube(position, Vector3.one * CURRENT_SIZE * .5f);
            }

            //draw the selectable cube
            Gizmos.color = color;
            selected = Selection.Contains(this.gameObject); //IsSelected();
            if (selected)
            {
                Gizmos.color = Color.black;
            }
            Gizmos.DrawCube(transform.position, Vector3.one * CURRENT_SIZE);

            first = false;
        }


        if (destroy)
            DestroyImmediate(parent);
    }

    void OnDrawGizmos()
    {
        return;
        Vector3 toCenter = (parent.transform.position - transform.position).normalized;
        Vector3 up = Vector3.Cross(Vector3.right, toCenter).normalized;
        Vector3 right = Vector3.Cross(toCenter, up).normalized;
        float radius = CURRENT_SIZE;

        //draw an icon for each group this vertex belongs to
        float angleStep = 360f / groups.Count;
        for (int i = 0; i < groups.Count; i++)
        {
            VertGroup group = groups[i];
            Gizmos.color = group.color;
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;
            Vector3 position = transform.position + right * x + up * y;
            Gizmos.DrawCube(position, Vector3.one * CURRENT_SIZE * .5f);
        }

        //draw the selectable cube
        Gizmos.color = color;
        selected = Selection.Contains(this.gameObject);//IsSelected();
        if (selected)
        {
            Gizmos.color = Color.black;
        }
        Gizmos.DrawCube(transform.position, Vector3.one * CURRENT_SIZE);
    }

    public bool HasGroup(VertGroup g)
    {
        foreach (VertGroup group in groups)
        {
            if (group.name == g.name)
            {
                return true;
            }
        }
        return false;
    }
}


#endif