using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    //variable declaration
    private float playerDistanceX;
    private float playerDistanceZ;
    private AudioSource gameAudio;
    public GameObject player;
    private Vector3 playerPos;
    private Vector3 soundObjectPos;
    private Vector3 Distance;
    private bool isPlaying = false;
    public float maxDistanceNum;

    private void Start()
    {
        gameAudio = this.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    // Plays the specified audio clip at the distance the player is at to the object specified. So if 10, the distance
    // must be at or below the specified distace to the sound object before the audio clip will play. If the distance is
    // too great the audio will not play as the player is too far away.
    void Update()
    {
        playerPos = player.transform.position;
        soundObjectPos = this.transform.position;
        Distance = (playerPos - soundObjectPos);

        if (Distance.x <= maxDistanceNum && isPlaying == false || Distance.z <= maxDistanceNum && isPlaying == false)
        {
            gameAudio.Play();
            gameAudio.loop = true;
            isPlaying = true;
        }
        
        if(Distance.x > maxDistanceNum && isPlaying == true || Distance.z > maxDistanceNum && isPlaying == true)
        {
            isPlaying = false;
            gameAudio.Stop();
        }
    }
}
