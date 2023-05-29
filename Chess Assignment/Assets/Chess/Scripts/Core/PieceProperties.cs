using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceProperties : MonoBehaviour
{
    public enum Player { Player1, Player2 };
    ////public bool isActive;
    public static Player chance=Player.Player1;
    public Player player;
    public int row, column;
    public float speed=1.6f;
    public abstract void PathHighlighter();
    public virtual void SpecialMove() { }
    private void Start()
    {
        row = GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>().row;
        column = GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>().column;
    }
    public virtual void OnMouseDown()
    {
        if(chance != player) { return; }
        ChessBoardPlacementHandler.Instance.ClearAllHighlights();
        ChessBoardPlacementHandler.Instance.SetCurrentObject(this.gameObject);
        PathHighlighter();
    }
    public virtual void Move(int ro, int col)
    {
        ChessBoardPlacementHandler.Instance.SetCurrentObject(null);
        ChessBoardPlacementHandler.Instance.ClearAllHighlights();
        //transform.position = Vector3.Lerp(transform.position,ChessBoardPlacementHandler.Instance.GetTile(ro, col).transform.position,speed);
        StartCoroutine(MoveCoroutine(ChessBoardPlacementHandler.Instance.GetTile(ro, col).transform.position));
        if(ChessBoardPlacementHandler.Instance.GetObjectOnTile(ro,col))
        {
            ChessBoardPlacementHandler.Instance.GetObjectOnTile(ro, col).GetComponent<PieceProperties>().Die();
        }
        ChessBoardPlacementHandler.Instance.UpdateCellDictionary(this.gameObject, ro, col, row, column);
        row = ro;
        column = col;
        chance = player == Player.Player1 ? Player.Player2 : Player.Player1;
        SpecialMove();
    }
    public virtual bool HighlightSelector(int row, int col, bool specialMove = false)
    {
        GameObject piece = null;
        if (piece = ChessBoardPlacementHandler.Instance.GetObjectOnTile(row, col))
        {
            if (piece.GetComponent<PieceProperties>().player != player)
            {
                ChessBoardPlacementHandler.Instance.Highlight(row, col,true);
            }
            return false;
        }
        else { return ChessBoardPlacementHandler.Instance.Highlight(row, col); }
    }
    public virtual void Die()
    {
        ChessBoardPlacementHandler.Instance.UpdateCellDictionary(this.gameObject, -1, -1, row, column);
        Destroy(this.gameObject);
    }
    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        float t = 0f;
        Vector3 startPosition = transform.position;

        while (t < 1f)
        {
            t += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }
}
