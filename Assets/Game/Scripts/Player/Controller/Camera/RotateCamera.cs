using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed;
    public InputReader reader;
    private Transform player;

    private enum RotateDirection
    {
        left,
        right,
        none
    }

    private RotateDirection direction;

    private void OnEnable()
    {
        reader.RotateCameraLeftEvent += RotateLeft;
        reader.RotateCameraRightEvent += RotateRight;
        reader.RotateCameraCancelEvent += CancelRotation;
    }
    private void OnDisable()
    {
        reader.RotateCameraLeftEvent -= RotateLeft;
        reader.RotateCameraRightEvent -= RotateRight;
        reader.RotateCameraCancelEvent -= CancelRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = RotateDirection.none;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position;
        switch (direction)
        {
            case RotateDirection.left:
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                break;
            case RotateDirection.right:
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
                break;
        }
    }

    public void RotateLeft()
    {
        Debug.Log("rotate left");
        direction = RotateDirection.left;   
    }

    public void RotateRight()
    {
        Debug.Log("rotate right");
        direction = RotateDirection.right;
    }

    public void CancelRotation()
    {
        Debug.Log("rotate cancel");
        direction = RotateDirection.none;
    }
}
