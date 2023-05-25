using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : PieceProperties
{    
    public override void PathHighlighter()
    {
        SetDiagonal();
    }
    void SetDiagonal()
    {
        bool proceed = true;
        int i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row + i, column + i++);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row + i, column - i++);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row-i, column+i++);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row-i, column-i++);
        }
    }
}
