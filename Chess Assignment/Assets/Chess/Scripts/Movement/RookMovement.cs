using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : PieceProperties
{    
    public override void PathHighlighter()
    {
        SetVerticalHoriontal();
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
