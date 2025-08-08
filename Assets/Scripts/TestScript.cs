using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TestScript : MonoBehaviour
{
    int number1 = 9;
    int number2 = 12;
    void Start()
    {
        if (number1 > 6 || number2 > 14)
        {
            Debug.Log("Number1 is bigger than 6, number2 is bigger than 14!");

        }

        else
        {
            Debug.Log("Number1 is not bigger than 6, number2 not bigger than 14!");

        }
    }
}