using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : PieceProperties
{    
    public override void PathHighlighter()
    {
        SetVerticalHoriontal();
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
            proceed = HighlightSelector(row - i, column + i++);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row - i, column - i++);
        }
    }
    void SetVerticalHoriontal()
    {
        bool proceed = true;
        int i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row + i++, column);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row - i++, column);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row, column+i++);
        }
        proceed = true;
        i = 1;
        while (proceed)
        {
            proceed = HighlightSelector(row, column-i++);
        }
    }
}
