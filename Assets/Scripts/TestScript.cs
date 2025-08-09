using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class TestScript : MonoBehaviour
{
    int[] numbers = { 1, 2, 3, 4, 5 };
    void Start()
    {
        for (int i = 0; i < numbers.Length; i++)
        Debug.Log(numbers[i]);
    }
}
