using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AILevelOne : Player
{
    private Dictionary<string, int> scoreDic = new Dictionary<string, int>();
    private int[,] score;
    private int chessMaxBoard = ChessBoardManager.chessMaxBoard;


    public override void OnAwake()
    {
        ChessType = ChessType.Black;
    }


    public override void OnStart()
    {
        base.OnStart();
        chessMaxBoard = ChessBoardManager.chessMaxBoard;
        score = new int[chessMaxBoard, chessMaxBoard];

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

        scoreDic.Add("11111", 2147483600);
        scoreDic.Add("011110", 2147483600);
        scoreDic.Add("111110", 2147483600);
        scoreDic.Add("011111", 2147483600);
    }

    public override void OnUpdate()
    {

    }

    public int SetCheckScore(Vector2Int inputPos)
    {
        int score = 0;
        for(int md = 0; md < 4; md++)
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
                        if (chessBoardManager.GridArray[newPoint.x, newPoint.y] == ChessType)
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
                    }
                    else
                    {
                        break;
                    }
                }
            }

            int _s;
            if (scoreDic.TryGetValue(str,out _s))
            {
                score += _s;
            }
        }
        return score;
    }
}
