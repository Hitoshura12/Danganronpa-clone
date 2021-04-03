using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Behaviour Editor/Camera Effect/Shake")]
public class ShakeCameraEffect : CameraEffect
{
    [SerializeField] Vector3 limits;
    [SerializeField] int intensity = 10; 
    public override void Apply(CameraEffectController effectController)
    {
        if (Time.frameCount% intensity==0)
        {
            Vector3 newPosition = new Vector3(
                Random.Range(-limits.x, limits.x),
                Random.Range(-limits.y, limits.y),
                Random.Range(-limits.z, limits.z)
                );
            effectController.position = newPosition;
        }
    }
}
