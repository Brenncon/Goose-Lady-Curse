using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class MouseOverAddon : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private bool isClickHintShown;
    private bool isGooseIconShown;
    public UnityEvent showGooseIcons;
    public UnityEvent hideGooseIcons;
    public UnityEvent<Vector2> showClickHint;
    public UnityEvent hideClickHint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(ray, out hit, 500f);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider.CompareTag("Item"))
        {

            isClickHintShown = true;
            showClickHint.Invoke(Mouse.current.position.ReadValue());
        }
        else
        {
            if (isClickHintShown)
            {
                isClickHintShown = false;
                hideClickHint.Invoke();
            }
        }
        if (hit.collider.CompareTag("Goose"))
        {
            if (!isGooseIconShown)
            {
                isGooseIconShown = true;
                showGooseIcons.Invoke();
            }
        }
        else
        {
            if (isGooseIconShown)
            {
                isGooseIconShown = false;
                hideGooseIcons.Invoke();
            }
        }
    }
}
