using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AILevelOne : Player
{
    private Dictionary<string, int> scoreDic;
    private int chessMaxBoard = ChessBoardManager.chessMaxBoard;


    public override void OnAwake()
    {
        ChessType = ChessType.Black;
    }

    public override void OnStart()
    {
        base.OnStart();
        scoreDic = new Dictionary<string, int>();
        chessMaxBoard = ChessBoardManager.chessMaxBoard;

        //1为是自己的棋子  2为别人的棋子   0为空地
        scoreDic.Add("0110", 100);
        scoreDic.Add("110", 50);
        scoreDic.Add("011", 50);

        scoreDic.Add("01110", 1000);
        scoreDic.Add("1110", 500);
        scoreDic.Add("0111", 500);

        scoreDic.Add("011110", 10000);
        scoreDic.Add("11110", 5000);
        scoreDic.Add("01111", 5000);

        scoreDic.Add("11111", 10000000);
        scoreDic.Add("0111110", 10000000);
        scoreDic.Add("111110", 10000000);
        scoreDic.Add("011111", 10000000);
    }

    public override void OnUpdate()
    {
        PlayerPlayChess();
    }

    public int SetCheckScore(int x, int y,ChessType chessType)
    {
        Vector2Int inputPos = new Vector2Int(x, y);
        int score = 0;

        for (int md = 0; md < 4; md++)
        {
            ChessBoardManager.MoveDir _moveDir = (ChessBoardManager.MoveDir)md;
            string str = "1";
            int xDir = 0, yDir = 0;
            switch (_moveDir)
            {
                case ChessBoardManager.MoveDir.TransverseLine:
                    xDir = 1; yDir = 0;
                    break;
                case ChessBoardManager.MoveDir.VerticalLine:
                    xDir = 0; yDir = 1;
                    break;
                case ChessBoardManager.MoveDir.LeftSlantLine:
                    xDir = 1; yDir = -1;
                    break;
                case ChessBoardManager.MoveDir.RightSlantLine:
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
                    if (chessBoardManager.CheckBorder(newPoint))
                    {
                        string ch;
                        if (chessBoardManager.GridArray[newPoint.x, newPoint.y] == chessType)
                        {
                            ch = "1";
                        }
                        else if (chessBoardManager.GridArray[newPoint.x, newPoint.y] == ChessType.None)
                        {
                            ch = "0";
                        }
                        else
                        {
                            break;
                        }
                        if (dir < 0)
                        {
                            str = ch + str;
                        }
                        else if (dir > 0)
                        {
                            str = str + ch;
                        }
                        if(ch=="0")
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(chessType== ChessType.White)
            {
                Debug.LogFormat("x:{0},y:{1},string:{2}", x, y, str);
            }
            int _s;
            if (scoreDic.TryGetValue(str, out _s))
            {
                score += _s;
            }
        }


        return score;
    }

    public override void PlayerPlayChess()
    {
        if (chessBoardManager.chessInfoStack.Count == 0)
        {
            AIPlayChess(7, 7);
        }
        else
        {
            int maxX = 0, maxY = 0;
            float maxScore = -1;

            for (int x = 0; x < chessMaxBoard; x++)
            {
                for (int y = 0; y < chessMaxBoard; y++)
                {
                    if ((chessBoardManager.GridArray[x, y] == ChessType.None))
                    {
                        float newScore = SetCheckScore(x, y,ChessType);
                        float newEnemyScore = SetCheckScore(x, y, ChessType== ChessType.Black? ChessType.White: ChessType.Black);

                        newScore += 1.5f * newEnemyScore;
                        if (newScore >= maxScore)
                        {
                            maxX = x;
                            maxY = y;
                            maxScore = newScore;
                        }
                    }
                }
            }

            AIPlayChess(maxX, maxY);
        }
    }

    public virtual void AIPlayChess(int x, int y)
    {
        Vector2Int pointPos = new Vector2Int(x, y);
        Vector2 chessPos;
        if (chessBoardManager.PointCanPlayChess(pointPos) && chessBoardManager.GetAxisByPoint(pointPos, out chessPos))
        {
            var go = chessManager.DoPlayChess(chessPos);
            if (chessBoardManager.PlayChess(pointPos, ChessType, go))
            {
                mainGameManager.WinGame();
            }
            else
            {
                mainGameManager.SwitchNowPlayer();
            }
        }
    }
}
