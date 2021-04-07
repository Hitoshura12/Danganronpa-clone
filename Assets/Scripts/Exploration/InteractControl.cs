using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractControl : MonoBehaviour
{
    [SerializeField] float maxDistance = 2f;
    Ray ray;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
    }
    private void CastRay()
    {
         ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit,maxDistance))
        {     
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
           
            if (interactable!=null)
            {
                interactable.Interact(gameObject);
            }
        }
    }
}
