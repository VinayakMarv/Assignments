using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : PieceProperties
{    
    public override void PathHighlighter()
    {
        SetKnight();
    }
    void SetKnight()
    {
        HighlightSelector(row + 1, column + 2);
        HighlightSelector(row - 1, column - 2);
        HighlightSelector(row + 2, column + 1);
        HighlightSelector(row - 2, column - 1);
        HighlightSelector(row + 1, column - 2);
        HighlightSelector(row - 1, column + 2);
        HighlightSelector(row + 2, column - 1);
        HighlightSelector(row - 2, column + 1);
    }
}
