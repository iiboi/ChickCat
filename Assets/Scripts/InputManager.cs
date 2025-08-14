using System.Runtime.CompilerServices;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject _SphereGameObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            _SphereGameObject.SetActive(true);

        else if (Input.GetKeyDown(KeyCode.Y))
            _SphereGameObject.SetActive(false);
    }
}

