using System.Runtime.CompilerServices;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private EnableDisable _EnableDisable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            _EnableDisable.enabled = true;

        else if (Input.GetKeyDown(KeyCode.Y))
            _EnableDisable.enabled = false;
    }
}

