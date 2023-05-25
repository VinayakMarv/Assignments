using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class UserInputManger : MonoBehaviour
{
    public enum State { IDLE, Selected}
    public State state = State.IDLE;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (state == State.IDLE)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            Vector2 cell = GetCell(hit.transform);
        //            if (cell != Vector2.positiveInfinity)
        //            {
        //                print(ChessBoardPlacementHandler.Instance.GetTile((int)cell.x, (int)cell.y).GetComponent<MonoBehaviour>());
        //            }
        //        }
        //    }
        //    else
        //    {

        //    }
        //}
    }
    Vector2 GetCell(Transform cell)
    {
        Vector2 cellPos = Vector2.positiveInfinity;
        try
        {
            cellPos.x = int.Parse(""+cell.name.Last());
            cellPos.y = int.Parse(""+cell.parent.name.Last());
            return cellPos;
        }
        catch (Exception)
        {
            Debug.LogError("Invalid row or column.");
            return cellPos;
        }
    }
}
