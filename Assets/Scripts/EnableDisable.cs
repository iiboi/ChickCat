using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("ENABLED");
    }
    private void OnDisable()
    {
        Debug.Log("DISABLED");
    }
}
