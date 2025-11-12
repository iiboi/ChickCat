using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;

    private PlayableDirector PlayableDirector;

    private void Awake()
    {
        PlayableDirector = GetComponent<PlayableDirector>();
    }
    
    private void OnEnable() 
    {
        PlayableDirector.Play();
        PlayableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        GameManager.ChangeGameState(GameState.Play);
    }
}
