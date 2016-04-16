using UnityEngine;
using System.Collections;
using MoreMountains.CorgiEngine;

public class FollowMouse : PathFollow
{

    public float speed = 1;


    protected override void FixedUpdate()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        Vector3 initialPosition = transform.position;

        var pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        //transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        Path.Points[0].position = pos;
        Path.Points[Path.Points.Length - 1].position = pos;
        if (Type == FollowType.MoveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        else if (Type == FollowType.Lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            _currentPoint.MoveNext();

        Vector3 finalPosition = transform.position;
        CurrentSpeed = (finalPosition - initialPosition) / Time.deltaTime;
    }

}
