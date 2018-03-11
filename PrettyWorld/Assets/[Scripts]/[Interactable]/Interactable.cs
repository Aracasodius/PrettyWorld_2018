using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    Canvas info;

    private void Start()
    {
        info = gameObject.GetComponentInChildren<Canvas>();
        gameObject.tag = "Interactable";
    }

    private void OnDrawGizmosSelected()
    {

    }

    public virtual void Interact()
    {

    }

    private void Update()
    {
        Ray detectRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (info != null)
        {
            if (Physics.Raycast(detectRay, out hit))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    info.enabled = true;
                }
                else
                {
                    info.enabled = false;
                }
            }
        }
    }
}