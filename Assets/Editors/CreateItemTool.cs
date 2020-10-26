using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateItemTool : EditorWindow
{
    [MenuItem("Editors/Tools/CreateOrder")]
    public static void Init()
    {
        CreateItemTool window = (CreateItemTool)EditorWindow.GetWindow(typeof(CreateItemTool));
        window.minSize = new Vector2(200f, 200f);
        window.Show();
    }

    private void OnGUI()
    {




    }


}
