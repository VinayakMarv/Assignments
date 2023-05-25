using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private bool up, down;
    [SerializeField] private int elevatorCommingForUp=-1,elevatorCommingForDown=-1;
    public bool[] elevatorStoppage = new bool[2];
    public int floorNumber;
    public int ElevatorCommingForUp { get { return elevatorCommingForUp; } set { elevatorCommingForUp=value; } }
    public int ElevatorCommingForDown { get { return elevatorCommingForDown; } set { elevatorCommingForDown = value; } }
    public bool Elevator0Stoppage { get { return elevatorStoppage[0]; } set { elevatorStoppage[0] = value; } }
    public bool Elevator1Stoppage { get { return elevatorStoppage[1]; } set { elevatorStoppage[1] = value; } }
    public bool IsUpRequesting { get { return elevatorCommingForUp!=-1 ? false:up; } set { up = value; } }
    public bool IsDownRequesting { get { return elevatorCommingForDown!=-1 ? false : down; } set { down = value; } }
    public void ElevatorReachedForUp()
    {
        elevatorCommingForUp = -1;
        up = false;
    }
    public void ElevatorReachedForDown()
    {
        elevatorCommingForDown = -1;
        down = false;
    }
    public void ElevatorReachedForInternal(int elevator)
    {
        elevatorStoppage[elevator] = false;
    }
    public void Interrupted(int elevator)
    {
        if(elevatorCommingForUp == elevator) { elevatorCommingForUp = -1; }
        if (elevatorCommingForDown == elevator) { elevatorCommingForDown = -1; }
        if (elevatorStoppage[elevator]) ;
    }
}
