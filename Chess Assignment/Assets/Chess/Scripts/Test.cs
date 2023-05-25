using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int rows=3;
    public string[] Lines ;
    void Start()
    {
        SecondPattern();
    }
    void SecondPattern()
    {
        Lines = new string[rows];
        int j = rows;
        int rowNum=0;
        while (j > 0)
        {
            for (int i = 0; i < rows; i++)
            {
                if (i == j-1) { Lines[rowNum] += " 1 "; }
                else { Lines[rowNum] += " 0 "; }
            }
            rowNum++;
            j--;
        }
    }
    void FirstPattern()
    {
        Lines = new string[rows];
        int i = 1;
        int k = rows;
        int rowNum = 0;
        while (rows > 0)
        {
            for (int j = k - 1; j < rows; j++)
            {
                Lines[rowNum] += " " + i.ToString();
                i++;
            }
            rowNum++;
            rows--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
