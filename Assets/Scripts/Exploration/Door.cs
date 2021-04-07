using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum DoorType
{
    Transition,
    Scene,
    Closed
}
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] DoorType doorType;
    [SerializeField] Transform destination;
    [SerializeField]
    string sceneName;
 
    public void Interact(GameObject interactor)
    {
        
        switch (doorType)
        {
            case DoorType.Transition:
                interactor.transform.position = destination.position;
                interactor.transform.rotation = destination.rotation;
                break;
            case DoorType.Scene:
                SceneManager.LoadScene(sceneName);
                break;
            case DoorType.Closed:
                break;
        }
      
    }

    private void OnDrawGizmos()
    {
        if (doorType == DoorType.Transition)
        {
            Debug.DrawLine(transform.position, destination.position);
        }
    }
}
