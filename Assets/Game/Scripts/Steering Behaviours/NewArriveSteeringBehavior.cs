using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArriveSteeringBehavior : SeekSteeringBehaviour
{
    //variable declaration
    public float SlowdownDistance = 1.0f;
    public float Deceleration = 2.5f;
    public float StoppingDistance = 0.1f;
    public Vector3 pointToGoTo1;
    public Vector3 pointToGoTo2;
    public Vector3 pointToGoTo3;
    public Vector3 pointToGoTo4;
    public Vector3 pointToGoToFlee;
    public float Timer;
    public int counter;
    public int MaxRandNum;
    public int NumTimmerWillStopAt;
    public GameObject Player;
    public GameObject Bunny;
    public float DistanceToStayAway;

    //Picks a random point to go to baased on the data given, and goes to it.
    //While picking points to go to away from the player if the player gets near.
    private void Update()
    {
        float distance = Vector3.Distance(Bunny.transform.position, Player.transform.position);

        if(distance <= DistanceToStayAway)
        {
            float x;
            float z;
            if(Player.transform.position.x > 0)
            {
                x = Bunny.transform.position.x - 3;
            }
            else
            {
                x = Bunny.transform.position.x + 3;
            }

            if (Player.transform.position.z > 0)
            {
                z = Bunny.transform.position.z + 3;
            }
            else
            {
                z = Bunny.transform.position.z - 3;
            }
            pointToGoToFlee.Set(x, 0, z);
            target = pointToGoToFlee;
            Timer = 0;
        }
        else
        {

        }

        Timer += Time.deltaTime;
        if (Timer >= NumTimmerWillStopAt && counter == 1)
        {
            target = pointToGoTo1;
            Timer = 0;
            counter = 2;
        }
        else if (Timer >= NumTimmerWillStopAt && counter == 2)
        {
            target = pointToGoTo2;
            Timer = 0;
            counter = 3;
        }
        else if (Timer >= NumTimmerWillStopAt && counter == 3)
        {
            target = pointToGoTo3;
            Timer = 0;
            counter = 4;
        }
        else if (Timer >= NumTimmerWillStopAt && counter == 4)
        {
            target = pointToGoTo4;
            Timer = 0;
            counter = 1;

            float x;
            float z;
            float d;

            x = Random.Range(1, MaxRandNum);
            z = Random.Range(1, MaxRandNum);

            d = Random.Range(1, 3);

            if (d == 1)
            {

            }
            else
            {
                x = x * -1;
                z = z * -1;
            }

            pointToGoTo1.Set(x, 0, z);

            x = Random.Range(1, MaxRandNum);
            z = Random.Range(1, MaxRandNum);

            d = Random.Range(1, 3);

            if (d == 1)
            {

            }
            else
            {
                x = x * -1;
                z = z * -1;
            }

            pointToGoTo2.Set(x, 0, z);

            x = Random.Range(1, MaxRandNum);
            z = Random.Range(1, MaxRandNum);

            d = Random.Range(1, 3);

            if (d == 1)
            {

            }
            else
            {
                x = x * -1;
                z = z * -1;
            }

            pointToGoTo3.Set(x, 0, z);

            x = Random.Range(1, MaxRandNum);
            z = Random.Range(1, MaxRandNum);

            d = Random.Range(1, 3);

            if (d == 1)
            {

            }
            else
            {
                x = x * -1;
                z = z * -1;
            }

            pointToGoTo4.Set(x, 0, z);
        }
    }

    private void Awake()
    {
        float x;
        float z;
        float d;

        x = Random.Range(1, MaxRandNum);
        z = Random.Range(1, MaxRandNum);

        d = Random.Range(1, 3);

        if(d == 1)
        {

        }
        else
        {
            x = x * -1;
            z = z * -1;
        }

        pointToGoTo1.Set(x, 0, z);

        x = Random.Range(1, MaxRandNum);
        z = Random.Range(1, MaxRandNum);

        d = Random.Range(1, 3);

        if (d == 1)
        {

        }
        else
        {
            x = x * -1;
            z = z * -1;
        }

        pointToGoTo2.Set(x, 0, z);

        x = Random.Range(1, MaxRandNum);
        z = Random.Range(1, MaxRandNum);

        d = Random.Range(1, 3);

        if (d == 1)
        {

        }
        else
        {
            x = x * -1;
            z = z * -1;
        }

        pointToGoTo3.Set(x, 0, z);

        x = Random.Range(1, MaxRandNum);
        z = Random.Range(1, MaxRandNum);

        d = Random.Range(1, 3);

        if (d == 1)
        {

        }
        else
        {
            x = x * -1;
            z = z * -1;
        }

        pointToGoTo4.Set(x, 0, z);
        Timer = 0;
        counter = 1;
    }

}
