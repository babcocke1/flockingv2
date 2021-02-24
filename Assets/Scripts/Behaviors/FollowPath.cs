using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    int targetNumber = 0;
    public GameObject[] targets = new GameObject[5];
    public FollowPath(GameObject[] targets)
    {
        for (int i=0; i<5; i++)
        {
            this.targets[i] = targets[i];
        }
    }

    protected override Vector3 getTargetPosition()
    {
        //checks if at a waypoint and increments if so
        if ((character.transform.position - target.transform.position).magnitude < .05)
        {
            targetNumber = (targetNumber + 1) % 5;
        }
        //sets target and returns its pos
        target = targets[targetNumber];
        return target.transform.position;
    }
    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        Vector3 targetPosition = getTargetPosition();
        if (targetPosition == Vector3.positiveInfinity)
        {
            return null;
        }

        // Get the direction to the target
        if (flee)
        {
            //result.linear = character.transform.position - target.transform.position;
            result.linear = character.transform.position - targetPosition;
        }
        else
        {
            //result.linear = target.transform.position - character.transform.position;
            result.linear = targetPosition - character.transform.position;
        }

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= 100f;

        result.angular = 0;
        return result;
    }
}

