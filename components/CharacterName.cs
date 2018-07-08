using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterName : MonoBehaviour {

    private GUIStyle guiStyle = new GUIStyle();
    private GUIContent guiName = new GUIContent("");
    private Rect nameRect = new Rect(0, 0, 0, 0);
    public Transform nameTransform;
    private String playerName;


    public void SetColor(Texture2D texture)
    {
        guiStyle.normal.background = texture;
    }


    public void SetName(string name)
    {
        playerName = name;
        gameObject.name = "Player-" + playerName;
        guiName = new GUIContent(playerName);
        Vector2 size = guiStyle.CalcSize(guiName);
        nameRect.width = size.x + 12;
        nameRect.height = size.y + 5;
    }

    void OnGUI()
    {
        // If someone knows a better way to do
        // names in Unity3D please tell me!
        Vector2 size = guiStyle.CalcSize(guiName);
        Vector3 coords = Camera.main.WorldToScreenPoint(nameTransform.position);
        nameRect.x = coords.x - size.x * 0.5f - 5f;
        nameRect.y = Screen.height - coords.y;
        guiStyle.normal.textColor = Color.black;
        guiStyle.contentOffset = new Vector2(4, 2);
        GUI.Box(nameRect, playerName, guiStyle);
    }

}
