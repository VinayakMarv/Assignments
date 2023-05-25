using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEngine : MonoBehaviour
{
    public float maxSpeed, accelaration,breakPower;
    public int elevatorNumber;
    public float waitTime = 1;
    public List<Floor> internalRequests;
    public int CurrentFloor { get { return currentFloor; } }
    public int TargetFloor { get { return targetFloor; } }
    public bool WasMovingUp { get { return wasMovingUp; } }
    public bool WasMovingDown { get { return wasMovingDown; } }
    public bool IsMoving { get { return displacement > 1 ? true:false ; } }

    public bool wasMovingUp, wasMovingDown;
    private float currentSpeed;
    [SerializeField] private int currentFloor;
    private int targetFloor;
    private float distance,displacement;
    private Vector3 targetFloorPos=Vector3.one*999;//, currentFloor;
    private float refVelocity;

    private void Update()
    {
        if(targetFloorPos == Vector3.one*999) { return; }
        UpdateCurrentFloor();
        UpdatePosition();
    }
    public void MoveToFloor(Floor floor)
    {
        StartCoroutine(ShutTheDoor(floor));
    }
    public IEnumerator ShutTheDoor(Floor floor)
    {
        yield return new WaitForSeconds(waitTime);
        targetFloorPos = floor.transform.position; targetFloor = floor.floorNumber;
    }
    private void UpdateCurrentFloor()
    {
        var floor = currentFloor + ((targetFloor - currentFloor) > 0 ? 1 : -1);
        if(floor == -1 || floor == 10) { return; }
        var nextFloor = ElevatorAndFloorManager.Instance.floors[floor];
        var distance = transform.position.y - nextFloor.transform.position.y;
        if ((distance > 0 ? distance : -distance) < 1) { currentFloor = nextFloor.floorNumber; }
    }
    private void UpdatePosition()
    {
        distance = transform.position.y - targetFloorPos.y;
        displacement = distance > 0 ? distance : -distance;
        wasMovingDown = distance > 0.1f ? true : false;
        wasMovingUp = distance < -0.1f ? true : false;
        //if (displacement > breakPower * currentSpeed)
        //{ //currentSpeed = 
        //}
        if (displacement >0.1f)
        {
            transform.position = new Vector3(transform.position.x,Mathf.SmoothDamp(transform.position.y, targetFloorPos.y,ref refVelocity,accelaration,maxSpeed), transform.position.z);
        }
        else { ReachedAFloor(); }
    }
    public void ReachedAFloor()
    {
        targetFloorPos = Vector3.one * 999;
        currentFloor = targetFloor;
        ElevatorAndFloorManager.Instance.FloorReached(targetFloor, elevatorNumber);
    }
}
