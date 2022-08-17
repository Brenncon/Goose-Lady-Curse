using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class QuestFourTrigger : Interactable
{
    public bool isCorrectTrigger;
    public bool isTriggered;
    public QuestFour quest;
    public SurvivalStat health;

    public float floatAmplitude = 0.5f;
    private float y;

    void Start()
    {
        y = Random.Range(-1f, 1f);
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void Update()
    {
        if (y >= 1)
        {
            y = -1;
        }
        y += Time.deltaTime;
        //transform.Rotate(0, 15f * Time.deltaTime, 0);
        transform.Translate(floatAmplitude * Vector3.up * Mathf.Sin(y * Mathf.PI) * Time.deltaTime);
    }

    private void Awake()
    {
        gameObject.tag = "Item";
    }

    public void OnTriggerClicked()
    {
        if (isCorrectTrigger&&!isTriggered)
        {
            isTriggered = true;
            quest.SetNextTrigger();
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            health.currentValue -= 25;
        }
    }
}
