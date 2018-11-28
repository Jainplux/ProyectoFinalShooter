using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ControlAnimations : MonoBehaviour {
    public PlayableDirector playabledirect;
    public List<TimelineAsset> timelines;
	// Use this for initialization
	void Start () {
        playabledirect.Play();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            playabledirect.Pause();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            playabledirect.Play();

        }
    }
    }
