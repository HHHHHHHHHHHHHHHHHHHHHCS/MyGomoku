using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 管理棋盘
/// </summary>
public class ChessBoardManager : MonoBehaviour,IMono
{
    public enum MoveDir
    {
        TransverseLine=0,
        VerticalLine,
        LeftSlantLine,
        RightSlantLine,
    }


    public const int chessMaxBoard = 15;
    private static readonly Vector2 startPos = new Vector2(-6.58f, -6.45f), chessScale = new Vector2(0.95f, 0.95f)
        , halfChessScale, offestPos;

    public ChessType[,] GridArray { get; private set; }
    private Stack<ChessInfo> chessInfoStack;

    static ChessBoardManager()
    {
        halfChessScale = chessScale / 2;
        offestPos = new Vector2((-startPos.x + halfChessScale.x) / chessScale.x, (-startPos.y + halfChessScale.y) / chessScale.y);
    }

    public void OnAwake()
    {
        GridArray = new ChessType[chessMaxBoard, chessMaxBoard];
        chessInfoStack = new Stack<ChessInfo>();
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
        if ((inputPoint.x >= 0 && inputPoint.x < chessMaxBoard) && (inputPoint.y >= 0 && inputPoint.y < chessMaxBoard))
        {
            return true;
        }
        return false;
    }

    public bool PointCanPlayChess(Vector2Int inputPos)
    {
        return GridArray[inputPos.x, inputPos.y] == ChessType.None;
    }

    public bool PlayChess(Vector2Int _inputPos, ChessType _chess, GameObject _go)
    {
        GridArray[_inputPos.x, _inputPos.y] = _chess;
        ChessInfo info = new ChessInfo(_inputPos, _chess, _go);
        chessInfoStack.Push(info);
        MainGameManager.Instance.MainUIManager.SetRetractButton();
        return CheckWineer(_inputPos, _chess);
    }

    public bool CheckWineer(Vector2Int inputPos, ChessType _chess)
    {
        return CheckOneLine(inputPos, _chess, MoveDir.TransverseLine) || CheckOneLine(inputPos, _chess, MoveDir.VerticalLine)
            || CheckOneLine(inputPos, _chess, MoveDir.LeftSlantLine) || CheckOneLine(inputPos, _chess, MoveDir.RightSlantLine);
    }

    /// <summary>
    /// 检测线
    /// </summary>
    /// <param name="inputPos"></param>
    /// <param name="_chess"></param>
    /// <param name="_moveDir"></param>
    /// <returns></returns>
    public bool CheckOneLine(Vector2Int inputPos, ChessType _chess, MoveDir _moveDir)
    {
        int chessNumber = 1;
        int xDir = 0, yDir = 0;
        switch (_moveDir)
        {
            case MoveDir.TransverseLine:
                xDir = 1; yDir = 0;
                break;
            case MoveDir.VerticalLine:
                xDir = 0; yDir = 1;
                break;
            case MoveDir.LeftSlantLine:
                xDir = 1; yDir = -1;
                break;
            case MoveDir.RightSlantLine:
                xDir = 1; yDir = 1;
                break;
            default:
                break;
        }

        for (int dir = -1; dir <= 1; dir += 2)
        {
            for (int i = 1; i < chessMaxBoard; i++)
            {
                Vector2Int newPoint = new Vector2Int(inputPos.x + dir * xDir * i, inputPos.y + dir * yDir * i);
                if (CheckBorder(newPoint) && GridArray[newPoint.x, newPoint.y] == _chess)
                {
                    chessNumber++;
                }
                else
                {
                    break;
                }
            }
        }
        return chessNumber >= 5;
    }

    public bool CanRetractChess()
    {
        return chessInfoStack.Count > 1;
    }

    public bool RetractChess()
    {
        if (CanRetractChess())
        {
            var item = chessInfoStack.Pop();
            Destroy(item.go);
            GridArray[item.pos.x, item.pos.y] = ChessType.None;
            item = chessInfoStack.Pop();
            Destroy(item.go);
            GridArray[item.pos.x, item.pos.y] = ChessType.None;
        }
        return false;
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnRelease()
    {
        throw new System.NotImplementedException();
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }
}
