using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Window/New Option")]
    private static void NewMenuOption()
    {

    }

    [MenuItem("Tools/SubMenu/Option")]
    private static void NewNestedOption()
    {
    }
}
