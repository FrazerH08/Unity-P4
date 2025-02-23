using UnityEngine;

public class KeyDebug : MonoBehaviour
{
    void  Update()
    {
       if (Input.GetKeyDown(KeyCode.M))
       {
            Debug.Log(" M key is being detected");
       }
    }
}
