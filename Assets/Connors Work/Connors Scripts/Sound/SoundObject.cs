using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private float playerDistanceX;
    private float playerDistanceZ;
    private AudioSource gameAudio;
    public GameObject player;
    private Vector3 playerPos;
    private Vector3 soundObjectPos;
    private Vector3 Distance;
    private bool isPlaying = false;
    public float maxDistanceNum;
    public float volume;

    private void Start()
    {
        gameAudio = this.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //playerDistanceX = (player.transform.position.x - this.transform.position.x);
        //playerDistanceZ = (player.transform.position.z - this.transform.position.z);

        playerPos = player.transform.position;
        soundObjectPos = this.transform.position;
        Distance = (playerPos - soundObjectPos);

        Debug.Log("Distance X "+Distance.x);
        Debug.Log("Distance Z "+Distance.z);

        Debug.Log("Player X " + playerPos.x);
        Debug.Log("Player Z " + playerPos.z);

        Debug.Log("Sound Object X " + soundObjectPos.x);
        Debug.Log("Sound Object Z " + soundObjectPos.z);

        //if (playerDistanceX <= 10 && playerDistanceZ <= 10)
        if (Distance.x <= maxDistanceNum && isPlaying == false || Distance.z <= maxDistanceNum && isPlaying == false)
        {
            gameAudio.Play();
            gameAudio.volume = volume;
            gameAudio.loop = true;
            isPlaying = true;
            //gameAudio.isPlaying



            //StartCoroutine(FadeOut(gameAudio, 1));
        }
        
        if(Distance.x > maxDistanceNum && isPlaying == true || Distance.z > maxDistanceNum && isPlaying == true)
        {
            isPlaying = false;
            //gameAudio.volume = 0.4f;
            //gameAudio.volume = 0.3f;
            //gameAudio.volume = 0.2f;
            //gameAudio.volume = 0.1f;
            //gameAudio.volume = 0.0f;
            gameAudio.Stop();
            //StartCoroutine(FadeOut(gameAudio, 5));
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
