using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelTwo : AILevelOne
{
    protected override void OnInitScoreDic()
    {
        scoreDic.Add("11000", 100);                      //眠二
        scoreDic.Add("10100", 100);
        scoreDic.Add("00011", 100);
        scoreDic.Add("00101", 100);
        scoreDic.Add("10010", 100);
        scoreDic.Add("01001", 100);
        scoreDic.Add("10001", 100);


        scoreDic.Add("001100", 500);                     //活二 "011000"
        scoreDic.Add("01010", 500);
        scoreDic.Add("010010", 500);

        scoreDic.Add("01100", 500);
        scoreDic.Add("00110", 500);


        scoreDic.Add("10101", 1000);                     // bool lfirst = true, lstop,rstop = f1lse  int 1llNum = 1
        scoreDic.Add("11001", 1000);
        scoreDic.Add("01101", 1000);
        scoreDic.Add("10110", 1000);
        scoreDic.Add("01011", 1000);
        scoreDic.Add("11010", 1000);
        scoreDic.Add("11100", 1000);                     //眠三

        scoreDic.Add("011010", 9000);                    //跳活三
        scoreDic.Add("010110", 9000);

        scoreDic.Add("01110", 10000);                    //活三       

        scoreDic.Add("10111", 15000);                    //冲四
        scoreDic.Add("11101", 15000);                    //冲四
        scoreDic.Add("01111", 15000);                    //冲四
        scoreDic.Add("11110", 15000);                    //冲四
        scoreDic.Add("11011", 15000);                    //冲四        

        scoreDic.Add("011110", 1000000);                 //活四

        scoreDic.Add("11111", 100000000);           //连五
    }


    public override int SetCheckScore(int x, int y, ChessType chessType)
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

            bool nextLeft = false,nextRight=false, leftStop = false, rightStop = false,lastLeftPos=true;
            int count = 1,maxCont = 6;
            for (int i = 1; i < chessMaxBoard; i++)
            {
                if ((leftStop && rightStop) || count >= maxCont)
                {
                    break;
                }

                for (int dir = -1; dir <= 1; dir += 2)
                {
                    if((nextLeft &&dir>0) || (nextRight &&dir<0))
                    {
                        continue;
                    }
                    if ((dir < 0 && leftStop) || (dir > 0 && rightStop))
                    {
                        continue;
                    }
                    if (count >= maxCont)
                    {
                        break;
                    }

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
                            if (dir > 0)
                            {
                                rightStop = true;
                            }
                            else if (dir < 0)
                            {
                                leftStop = true;
                            }
                            continue;
                        }
                        if (dir < 0)
                        {
                            lastLeftPos = true;
                            str = ch + str;
                            if(ch=="1")
                            {
                                nextLeft = true;
                            }
                        }
                        else if (dir > 0)
                        {
                            lastLeftPos = false;
                            str = str + ch;
                            if (ch == "1")
                            {
                                nextRight = true;
                            }
                        }
                        count++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            int _s;
            if (chessType == ChessType.White)
            {
                Debug.LogFormat("x:{0},y:{1},string:{2}", x, y, str);
            }
            if (count <= 5)
            {
                if (scoreDic.TryGetValue(str, out _s))
                {
                    score += _s;
                }
            }
            else if (count <= 6)
            {
                if (scoreDic.TryGetValue(str, out _s))
                {
                    score += _s;
                }
                else
                {
                    str = str.Substring(lastLeftPos?0:1, 5);
                    if (scoreDic.TryGetValue(str, out _s))
                    {
                        score += _s;
                    }
                }

            }
        }


        return score;
    }
}
