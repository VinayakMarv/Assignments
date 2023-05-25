using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorAndFloorManager : MonoBehaviour
{
    public ElevatorEngine[] elevators;
    public Floor[] floors;
    public Toggle[] floorTogglesUp,floorTogglesDown,elevator1TargetsToggle, elevator2TargetsToggle;
    public static ElevatorAndFloorManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    void FindClosestElevator(bool E0 = false, bool E1 = false)  //false means no need to check
    {
        for (int i = 0; i < 10; i++)
        {
            if (!E0)
                if (elevators[0].wasMovingUp)
                {
                    if (i <= elevators[0].TargetFloor - elevators[0].CurrentFloor)
                    { E0 = CheckAtIndexOf(i, 0, true, false); }
                }
                else if (elevators[0].wasMovingDown)
                {
                    if (i <= elevators[0].CurrentFloor - elevators[0].TargetFloor)
                    { E0 = CheckAtIndexOf(-i, 0, false, true); }
                }
                else
                {
                    E0 = CheckAtIndexOf(i, 0) || CheckAtIndexOf(-i, 0);
                }
            if (!E1)
                if (elevators[1].wasMovingUp)
                {
                    if (i <= elevators[1].TargetFloor - elevators[1].CurrentFloor)
                    { E1 = CheckAtIndexOf(i, 1, true, false); }
                }
                else if (elevators[1].wasMovingDown)
                {
                    if (i <= elevators[1].CurrentFloor - elevators[1].TargetFloor)
                    { E1 = CheckAtIndexOf(-i, 1, false, true); }
                }
                else
                {
                    E1 = CheckAtIndexOf(i, 1) || CheckAtIndexOf(-i, 1);
                }
        }
    }
    bool CheckAtIndexOf(int index, int elevator, bool checkUpRequest = true, bool checkDownRequest=true)    //checks for each floor
    {
        var floorNum = elevators[elevator].CurrentFloor + index;
        if (floorNum < 10 && floorNum >= 0)
        if (checkUpRequest && floors[floorNum].IsUpRequesting)
        {
            floors[elevators[elevator].TargetFloor].Interrupted(elevator);
            elevators[elevator].MoveToFloor(floors[floorNum]);
            floors[floorNum].ElevatorCommingForUp=elevator; return true ;
        }
        else if (checkDownRequest && floors[floorNum].IsDownRequesting)
        {
            floors[elevators[elevator].TargetFloor].Interrupted(elevator);
            elevators[elevator].MoveToFloor(floors[floorNum]);
            floors[floorNum].ElevatorCommingForDown = elevator; return true;
        }
        else if (floors[floorNum].elevatorStoppage[elevator])
        {
            floors[elevators[elevator].TargetFloor].Interrupted(elevator);
            elevators[elevator].MoveToFloor(floors[floorNum]);
            return true;
        }
        return false;
    }
    public void FloorReached(int floor,int elevator)    //Called by Elevator
    {
        if (floors[floor].ElevatorCommingForDown == elevator)
        { 
            floors[floor].ElevatorReachedForDown(); 
            floorTogglesDown[floor].isOn = false; 
            floorTogglesDown[floor].interactable = true; 
        }
        if (floors[floor].ElevatorCommingForUp == elevator) 
        {
            floors[floor].ElevatorReachedForUp(); 
            floorTogglesUp[floor].isOn = false; 
            floorTogglesUp[floor].interactable = true; 
        }
        if (floors[floor].elevatorStoppage[elevator])
        {
            floors[floor].ElevatorReachedForInternal(elevator);
            if (elevator == 0)
            {
                elevator1TargetsToggle[floor].isOn = false;
                elevator1TargetsToggle[floor].interactable = true;
            }
            else 
            { 
                elevator2TargetsToggle[floor].interactable = true;
                elevator2TargetsToggle[floor].isOn = false;
            }
        }
        FindClosestElevator(elevator == 0 ? false : true, elevator == 1 ? false : true);
    }
    #region UI Functions
    public void SetUpToggle(int floor)
    {
        if (!floorTogglesUp[floor].isOn) { return; }
        floors[floor].IsUpRequesting = true ;
        floorTogglesUp[floor].interactable = false ;
        FindClosestElevator(); ;
    }
    public void SetDownToggle(int floor)
    {
        if (!floorTogglesDown[floor].isOn) { return; }
        floors[floor].IsDownRequesting=true;
        floorTogglesDown[floor].interactable = false ;
        FindClosestElevator();
    }
    public void SetElevator0Target(int floor)
    {
        if (!elevator1TargetsToggle[floor].isOn) { return; }
        floors[floor].Elevator0Stoppage = true;
        elevator1TargetsToggle[floor].interactable = false;
        FindClosestElevator(false,true);
    }
    public void SetElevator1Target(int floor)
    {
        if (!elevator2TargetsToggle[floor].isOn) { return; }
        floors[floor].Elevator1Stoppage = true;
        elevator2TargetsToggle[floor].interactable = false;
        FindClosestElevator(true,false);
    }
    #endregion
}