using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 管理棋盘
/// </summary>
public class ChessBoardManager : AbsMono
{
    private const int chessBoardMaxX = 15, chessBoardMaxY = 15;
    private static readonly Vector2 startPos = new Vector2(-6.58f, -6.45f), chessScale = new Vector2(0.95f, 0.95f)
        , halfChessScale, offestPos;

    private ChessType[,] gridArray;

    static ChessBoardManager()
    {
        halfChessScale = chessScale / 2;
        offestPos = new Vector2((-startPos.x + halfChessScale.x) / chessScale.x, (-startPos.y + halfChessScale.y) / chessScale.y);
    }

    public override void OnAwake()
    {
        gridArray = new ChessType[chessBoardMaxX, chessBoardMaxY];
    }

    public bool InputAxisToPoint(Vector3 inputPos, out Vector2Int outPos)
    {
        outPos = new Vector2Int((int)(inputPos.x / chessScale.x + offestPos.x), (int)(inputPos.y / chessScale.y + offestPos.y));
        return CheckBorder(outPos);
    }

    public bool GetAxisByPoint(Vector2Int inputPoint, out Vector2 outPos)
    {
        if (CheckBorder(inputPoint))
        {
            outPos = startPos + new Vector2(chessScale.x * inputPoint.x, chessScale.y * inputPoint.y);
            return true;
        }
        outPos = Vector2.zero;
        return false;
    }

    public bool CheckBorder(Vector2Int inputPoint)
    {
        if ((inputPoint.x >= 0 && inputPoint.x < chessBoardMaxX) || (inputPoint.y >= 0 && inputPoint.y < chessBoardMaxY))
        {
            return true;
        }
        return false;
    }

    public bool PointCanPlayChess(Vector2Int inputPos)
    {
        return gridArray[inputPos.x, inputPos.y] == ChessType.None;
    }

    public bool PlayChess(Vector2Int inputPos, ChessType _chess)
    {
        gridArray[inputPos.x, inputPos.y] = _chess;
        return CheckWineer(inputPos);
    }

    public bool CheckWineer(Vector2Int inputPos)
    {
        return false;
    }
}
