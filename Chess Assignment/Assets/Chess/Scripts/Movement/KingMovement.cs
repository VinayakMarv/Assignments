using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : PieceProperties
{    
    public override void PathHighlighter()
    {
        SetSquareKing();
    }
    void SetSquareKing()
    {
        HighlightSelector(row + 1, column + 0);
        HighlightSelector(row + 1, column + 1);
        HighlightSelector(row + 1, column - 1);
        HighlightSelector(row + 0, column + 0);
        HighlightSelector(row + 0, column + 1);
        HighlightSelector(row + 0, column - 1);
        HighlightSelector(row - 1, column + 0);
        HighlightSelector(row - 1, column + 1);
        HighlightSelector(row - 1, column - 1);
    }
    private void OnDestroy()
    {
        GameManager.instance.EndGame(player == Player.Player1 ? true : false);
    }
}
