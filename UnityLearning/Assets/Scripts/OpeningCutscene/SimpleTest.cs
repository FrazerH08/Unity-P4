using UnityEngine;

public class SimpleTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed in SimpleTest!");
        }
    }
}
