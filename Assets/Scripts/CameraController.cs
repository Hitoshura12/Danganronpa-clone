using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Transform pivot;
    

    float rotationSpeed = 25f;
    float height;

    [SerializeField] Transform cameraTransform;
    CameraEffectController effectController;

    private void Start()
    {
        pivot = transform.parent;
        effectController = GetComponent<CameraEffectController>();
        height = cameraTransform.position.y;
    }
    [SerializeField] float newAngle = 0f;
    [SerializeField] float speed = 50f;
    [SerializeField] float radius = 3f;
    
    private void Update()
    {
        Vector3 targetDir = target.position - pivot.position;
        targetDir.y = 0f;
        Vector3 cameraDir = cameraTransform.position - effectController.position - pivot.position;
        cameraDir.y = 0;
        
        float targetAngle = Vector3.SignedAngle(targetDir, pivot.forward, Vector3.up);
        float cameraAngle = Vector3.SignedAngle(cameraDir, pivot.forward, Vector3.up);

        newAngle = Mathf.MoveTowardsAngle(cameraAngle, targetAngle, speed * Time.deltaTime);
      
        newAngle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(-newAngle) * (radius + effectController.zoom);
        float z = Mathf.Cos(newAngle)  * (radius + effectController.zoom);
        cameraTransform.position = new Vector3(x, height, z) + effectController.position;

        Vector3 cameraPos = cameraTransform.position;
        Vector3 textPivotPos = pivot.position;
        cameraPos.y = 0;
        textPivotPos.y = 0;

        cameraTransform.rotation = Quaternion.LookRotation(cameraPos - textPivotPos);
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + effectController.rotation);
    }

}
