using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] int smoothFrames = 6;
    List<float> rotationArrayX, rotationArrayY;
    float rotationAvgX, rotationAvgY;
    float rotationX, rotationY;
    [SerializeField] float sensitivty = 3f;

    private void Start()
    {
        rotationArrayX = new List<float>();
        rotationArrayY = new List<float>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Update()
    {
        rotationY += Input.GetAxis("Mouse Y")* sensitivty;
        rotationX = Input.GetAxis("Mouse X");
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);

        rotationArrayX.Add(rotationX);
        rotationArrayY.Add(rotationY);

        if (rotationArrayX.Count >= smoothFrames)
        {
            rotationArrayX.RemoveAt(0);
        }
        if (rotationArrayY.Count>= smoothFrames)
        {
            rotationArrayY.RemoveAt(0);
        }
        rotationAvgX = 0f;
        rotationAvgY = 0f;
        for (int i = 0; i < rotationArrayX.Count; i++)
        {
            rotationAvgX += rotationArrayX[i];
        }
        for (int j = 0; j < rotationArrayY.Count; j++)
        {
            rotationAvgY += rotationArrayY[j];
        }
        rotationAvgX /= rotationArrayX.Count;
        rotationAvgY /= rotationArrayY.Count;

        Vector3 rotation = new Vector3(0f, rotationAvgX * sensitivty, 0f);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationAvgY, Vector3.left);
        playerRb.MoveRotation( playerRb.rotation*Quaternion.Euler(rotation));
        cam.transform.localRotation = yQuaternion;
       
    }
}
