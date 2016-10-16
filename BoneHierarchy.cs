using UnityEngine;
using System.Collections;

public class BoneHierarchy : MonoBehaviour {

    public SkinnedMeshRenderer R; // assign in the inspector
    public Transform RootBone;    // assign in the inspector

    string ProcessBone(Transform aBone, string indent)
    {
        string result = indent + aBone.name + "\n";
        foreach (Transform T in aBone)
        {
            // Had to exclude other gameobjects since we have attached armor parts
            //Now only gameobjects are included that contains "Bip" ;)
            if (T.name.Contains("Bip"))
                result += ProcessBone(T, indent + "|--");
        }
        return result;
    }

    void Start()
    {
        Transform[] bones = R.bones;
        string text = "Bone array:";
        for (int i = 0; i < bones.Length; i++)
        {
            text += "Bone index:" + i + "  Name: " + bones[i].name + "\n";
        }
        text += "\nBone Hierarchy:\n";
        text += ProcessBone(RootBone, "");
        Debug.Log(text);
    }
}
