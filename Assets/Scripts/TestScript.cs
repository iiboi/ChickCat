using System.Globalization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TestScript : MonoBehaviour
{
    int number = 10;
    void Start()
    {
        // ForLoopExample();
        // WhileLoopExample();
        DoWhileLoopExample();
    }
    void ForLoopExample()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
        }

    }

    void WhileLoopExample()
    {
        

        while (number > 0)
        {
            Debug.Log(number);
            number--;
        }
    }

    void DoWhileLoopExample()
    {
        number = -5;
        do
        {
            Debug.Log(number);
            number--;
        }
        while (number > 0);
    }
    

}