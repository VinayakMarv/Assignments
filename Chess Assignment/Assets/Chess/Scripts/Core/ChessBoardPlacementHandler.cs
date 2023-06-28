using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    [SerializeField] private GameObject _endMenu;
    private GameObject[,] _chessBoard;
    private Dictionary<Vector2, GameObject> CellDictionary = new Dictionary<Vector2, GameObject>();
    private List<GameObject> currentlyHightlighted = new List<GameObject>();
    private GameObject currentlySelected;
    internal static ChessBoardPlacementHandler Instance;
    private void Awake() {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j) {
        try {
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal bool Highlight(int row, int col, bool red = false) {
        var tile = GetTile(row, col);
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return false;
        }
        var highlight = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        if (red)
            highlight.GetComponent<SpriteRenderer>().color = Color.red; //= Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        highlight.GetComponent<CellSelect>().SetRowCol(row, col);
        currentlyHightlighted.Add(highlight);
        return true;
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }
    internal void ClearAllHighlights()
    {
        foreach(GameObject obj in currentlyHightlighted) { Destroy(obj); }
        currentlyHightlighted.Clear();
    }
    internal GameObject GetCurrentObject()
    {
        return currentlySelected;
    }
    internal void SetCurrentObject(GameObject obj)
    {
        currentlySelected = obj;
    }
    internal GameObject GetObjectOnTile(int row, int col)
    {
        try
        {
            return CellDictionary[new Vector2(row,col)];
        }
        catch (Exception)
        {
            return null;
        }
    }
    internal void UpdateCellDictionary(GameObject piece, int newRow,int newCol,int oldRow= -1, int oldCol = -1)
    {
        CellDictionary.Remove(new Vector2(oldRow, oldCol));
        CellDictionary.Remove(new Vector2(newRow, newCol));
        CellDictionary.Add(new Vector2(newRow, newCol),piece);
    }
    internal void EndGame()
    {
        _endMenu.SetActive(true);
    }
    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion
}