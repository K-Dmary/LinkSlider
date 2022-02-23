using UnityEngine;
using UnityEngine.InputSystem;
public class LevelScript : MonoBehaviour
{
    public int experience;

    public int Level
    {
        get { return experience / 750; }
    }

    public void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame) Debug.Log("afafaf");
    }
}

