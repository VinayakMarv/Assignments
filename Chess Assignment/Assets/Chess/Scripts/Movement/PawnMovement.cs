using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : PieceProperties
{
    //PieceDetails pieceDetails;
    int vertical;
    bool firstMove=true;
    private void Awake()
    {
        vertical = (int)player == 0 ? 1 : -1;
    }
    
    public override void PathHighlighter()
    {
        SetVerticalPawn();
        SetDiagonalPawn();
    }
    void SetVerticalPawn()
    {
        if (firstMove)
        {
            if(HighlightSelector(row + vertical, column))
            HighlightSelector(row + vertical*2, column);
        }
        else { HighlightSelector(row + vertical, column); }
    }
    void SetDiagonalPawn()
    {
        HighlightSelector(row + vertical, column + 1,true);
        HighlightSelector(row + vertical, column - 1,true);
    }
    public override void SpecialMove()
    {
        if(firstMove)
        firstMove = false;
    }
    public override bool HighlightSelector(int row, int col,bool specialMove=false)
    {
        GameObject piece = null;
        if (specialMove)
        {
            if (piece = ChessBoardPlacementHandler.Instance.GetObjectOnTile(row, col))
            {
                if (piece.GetComponent<PieceProperties>().player == player) { return false; }
                else { ChessBoardPlacementHandler.Instance.Highlight(row, col); return true; }
            }
            else { return false; }
        }
        if (piece = ChessBoardPlacementHandler.Instance.GetObjectOnTile(row, col))
        {
            if (piece.GetComponent<PieceProperties>().player == player) { return false; }
            else { return false; }
        }
        else { ChessBoardPlacementHandler.Instance.Highlight(row, col); return true; }
    }
}
