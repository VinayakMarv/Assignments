using System.Collections.Generic;
using UnityEngine;
public class GridManager : MonoBehaviour
{
    public bool run = false;
    public Transform gridParent;
    public GameObject[] gridCells;
    public List<GameObject> ChildObjects;
    //private List<GameObject> childObjectsClone;
    private List<GameObject> possibleChild=new List<GameObject>();
    private List<GameObject> newSpawned = new List<GameObject> ();
    private int gridSize;
    void Start()
    {
        //childObjectsClone = new List<GameObject>(ChildObjects);
        Empt();
        SpawnChilds();
        Checkoverlap();
    }

    // Update is called once per frame
    private void Update()
    {
        if (run || Input.GetKeyDown(KeyCode.Space))
        {
            run = false;
            Empt();
            SpawnChilds();
            Checkoverlap();
        }
    }
    public void Empt()
    {
        while (newSpawned.Count > 0)
        {
            Destroy(newSpawned[0]);
            newSpawned.RemoveAt(0);
        }
    }
    void SpawnChilds()
    {
        gridSize = (int)Mathf.Sqrt(gridCells.Length);
        for(int i = 0; i < gridSize; i++)
        {
            possibleChild.Clear();
            possibleChild.AddRange(ChildObjects);
            if(i == 0) { ChildElimination("UpperSpan",possibleChild); }
            else if (i == gridSize - 1) { ChildElimination("LowerSpan",possibleChild); }
            for(int j = 0; j < gridSize; j++)
            {
                SpawnAt(i, j,possibleChild);
            }
        }
    }
    void ChildElimination(string eliminate, List<GameObject> possibleChild)
    {
        for(int i = 0; i < possibleChild.Count; i++)
        {
            if(possibleChild[i].name==eliminate)
            { possibleChild.RemoveAt(i); return; }
        }
    }
    void SpawnAt(int row,int col,List<GameObject> possibleChild)
    {
        if (col == 0) { ChildElimination("LeftSpan",possibleChild); }
        else if (col == gridSize - 1) { ChildElimination("RightSpan",possibleChild); }
        if(row>0 && col > 0)
            if (newSpawned[(row - 1) * gridSize + col-1].name.Contains("RightSpan")) { ChildElimination("UpperSpan", possibleChild); }
        if(row>0 && col < gridSize -1)
            if (newSpawned[(row - 1) * gridSize + col+1].name.Contains("LeftSpan")) { ChildElimination("UpperSpan", possibleChild); }
        if (row > 0 && col > 0)
            if (newSpawned[(row - 1) * gridSize + col - 1].name.Contains("LowerSpan")) { ChildElimination("LeftSpan", possibleChild); }
        if (row > 0 && col < gridSize - 1)
            if (newSpawned[(row - 1) * gridSize + col + 1].name.Contains("LowerSpan")) { ChildElimination("RightSpan", possibleChild); }
        //if (newSpawned[(row - 2) * gridSize + col].name.Contains("LowerSpan")) { ChildElimination("UpperSpan", possibleChild); }
        if (row > 1)
            if (newSpawned[(row - 2) * gridSize + col].name.Contains("LowerSpan")) { ChildElimination("UpperSpan", possibleChild); }
        if(col>1)
            if (newSpawned[row * gridSize + col-2].name.Contains("RightSpan")) { ChildElimination("LeftSpan", possibleChild); }
        var child = Instantiate(possibleChild[Random.Range(0, possibleChild.Count)],gridParent);
        child.transform.position = gridCells[row * gridSize + col].transform.position;
        newSpawned.Add(child);
    }
    public void Checkoverlap()
    {
        int i = 0;
        foreach(var child in newSpawned)
        {
            if (!child.activeSelf) { 
                i++;continue; }

            if (child.name.Contains("LeftSpan"))
            {
                newSpawned[i-1].SetActive(false);
            }else
            if (child.name.Contains("LowerSpan"))
            {
                newSpawned[i + gridSize].SetActive(false);
            }
            else
            if (child.name.Contains("UpperSpan"))
            {
                newSpawned[i - gridSize].SetActive(false);
            }
            else
            if (child.name.Contains("RightSpan"))
            {
                newSpawned[i + 1].SetActive(false);
            }
            i++;
        }
    }
}
