using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelect : MonoBehaviour
{
    public int row, column;
    public void SetRowCol(int ro,int col)
    {
        row = ro;
        column = col;
    }
    void OnMouseDown()
    {
        ChessBoardPlacementHandler.Instance.GetCurrentObject().GetComponent<PieceProperties>().Move(row,column);
    }
}
