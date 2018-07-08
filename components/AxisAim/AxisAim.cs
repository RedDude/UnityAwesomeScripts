using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AxisAim : MonoBehaviour
{
    public Vector2 aimVector;
    public bool fire;
    private float _FireRate;
    public float _TimeToFire = 2f;
    public Transform aim;
    public boolan hideWhenNoAiming = false;

    void Update()
    {
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;
        if (aimVector.y != 0 || aimVector.x != 0)
        {
            aim.gameObject.SetActive(true);

            aim.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 

            if (fire && Time.time > _FireRate)
            {
                _FireRate = Time.time + _TimeToFire;
                ExecuteEvents.Execute<IAxisAim>(gameObject, null, (x, y) => x.Shoot(aim));
            }
        }
        else
        {
            if(!hideWhenNoAiming)
                aim.gameObject.SetActive(false);
        }
    }

    public interface IAxisAim : IEventSystemHandler
    {
        void Shoot(Transform aim);
    }
}
