using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxNode
{
    public ChessType chess;
    public Vector2Int pos;
    public List<MiniMaxNode> child;
    public float value;
}

public class AILevelThree : AILevelOne
{

    private Dictionary<string, float> toScore;

    protected override void OnInitScoreDic()
    {
        toScore = new Dictionary<string, float>();

        toScore.Add("aa___", 100);                      //眠二
        toScore.Add("a_a__", 100);
        toScore.Add("___aa", 100);
        toScore.Add("__a_a", 100);
        toScore.Add("a__a_", 100);
        toScore.Add("_a__a", 100);
        toScore.Add("a___a", 100);


        toScore.Add("__aa__", 500);                     //活二 
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);

        toScore.Add("_aa__", 500);
        toScore.Add("__aa_", 500);


        toScore.Add("a_a_a", 1000);
        toScore.Add("aa__a", 1000);
        toScore.Add("_aa_a", 1000);
        toScore.Add("a_aa_", 1000);
        toScore.Add("_a_aa", 1000);
        toScore.Add("aa_a_", 1000);
        toScore.Add("aaa__", 1000);                     //眠三

        toScore.Add("_aa_a_", 9000);                    //跳活三
        toScore.Add("_a_aa_", 9000);

        toScore.Add("_aaa_", 10000);                    //活三       


        toScore.Add("a_aaa", 15000);                    //冲四
        toScore.Add("aaa_a", 15000);                    //冲四
        toScore.Add("_aaaa", 15000);                    //冲四
        toScore.Add("aaaa_", 15000);                    //冲四
        toScore.Add("aa_aa", 15000);                    //冲四        


        toScore.Add("_aaaa_", 1000000);                 //活四

        toScore.Add("aaaaa", maxScore);           //连五


    }

    public float CheckOneLine(ChessType[,] grid, Vector2Int pos, Vector2Int offset, ChessType chess)
    {
        float score = 0;
        bool lfirst = true, lstop = false, rstop = false;
        int AllNum = 1;
        string str = "a";
        int ri = offset.x, rj = offset.y;
        int li = -offset.x, lj = -offset.y;
        while (AllNum < 7 && (!lstop || !rstop))
        {
            if (lfirst)
            {
                //左边
                if ((pos.x + li >= 0 && pos.x + li < 15) &&
            pos.y + lj >= 0 && pos.y + lj < 15 && !lstop)
                {
                    if (grid[pos.x + li, pos.y + lj] == chess)
                    {
                        AllNum++;
                        str = "a" + str;

                    }
                    else if (grid[pos.x + li, pos.y + lj] == 0)
                    {
                        AllNum++;
                        str = "_" + str;
                        if (!rstop) lfirst = false;
                    }
                    else
                    {
                        lstop = true;
                        if (!rstop) lfirst = false;
                    }
                    li -= offset.x; lj -=offset.y;
                }
                else
                {
                    lstop = true;
                    if (!rstop) lfirst = false;
                }
            }
            else
            {
                if ((pos.x + ri >= 0 && pos.x + ri < 15) &&
          pos.y + rj >= 0 && pos.y + rj < 15 && !lfirst && !rstop)
                {
                    if (grid[pos.x + ri, pos.y + rj] == chess)
                    {
                        AllNum++;
                        str += "a";

                    }
                    else if (grid[pos.x + ri, pos.y + rj] == 0)
                    {
                        AllNum++;
                        str += "_";
                        if (!lstop) lfirst = true;
                    }
                    else
                    {
                        rstop = true;
                        if (!lstop) lfirst = true;
                    }
                    ri += offset.x; rj +=offset.y;
                }
                else
                {
                    rstop = true;
                    if (!lstop) lfirst = true;
                }
            }
        }

        string cmpStr = "";
        foreach (var keyInfo in toScore)
        {
            if (str.Contains(keyInfo.Key))
            {
                if (cmpStr != "")
                {
                    if (toScore[keyInfo.Key] > toScore[cmpStr])
                    {
                        cmpStr = keyInfo.Key;
                    }
                }
                else
                {
                    cmpStr = keyInfo.Key;
                }
            }
        }

        if (cmpStr != "")
        {
            score += toScore[cmpStr];
        }
        return score;
    }

    public float GetScore(ChessType[,] grid, Vector2Int pos)
    {
        float score = 0;

        score += CheckOneLine(grid, pos, new Vector2Int ( 1, 0), ChessType.White);
        score += CheckOneLine(grid, pos,new Vector2Int ( 1, 1), ChessType.White);
        score += CheckOneLine(grid, pos,new Vector2Int ( 1, -1), ChessType.White);
        score += CheckOneLine(grid, pos,new Vector2Int ( 0, 1), ChessType.White);
                                        
        score += CheckOneLine(grid, pos,new Vector2Int ( 1, 0), ChessType.Black);
        score += CheckOneLine(grid, pos,new Vector2Int ( 1, 1), ChessType.Black);
        score += CheckOneLine(grid, pos,new Vector2Int ( 1, -1), ChessType.Black);
        score += CheckOneLine(grid, pos,new Vector2Int( 0, 1), ChessType.Black);

        return score;
    }

    public override void PlayChess()
    {
        if (chessBoardManager.chessInfoStack.Count == 0)
        {
            AIPlayChess(7, 7);
        }

        MiniMaxNode node = null;
        foreach (var item in GetList(chessBoardManager.GridArray, ChessType, true))
        {
            CreateTree(item, (ChessType[,])chessBoardManager.GridArray.Clone(), 3, false);
            float a = -maxScore;
            float b = +maxScore;
            item.value += AlphaBeta(item, 3, false, a, b);
            if (node != null)
            {//挑选最大的下旗点
                if (node.value < item.value)
                    node = item;
            }
            else
            {
                node = item;
            }
        }
        AIPlayChess(node.pos.x, node.pos.y);
    }

    //返回节点 极大极小
    private List<MiniMaxNode> GetList(ChessType[,] grid, ChessType chess, bool mySelf)
    {
        List<MiniMaxNode> nodeList = new List<MiniMaxNode>();
        MiniMaxNode node;
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (grid[pos.x, pos.y] != 0) continue;

                node = new MiniMaxNode();
                node.pos = pos;
                node.chess = chess;
                if (mySelf)
                    node.value = GetScore(grid, pos);
                else
                    node.value = -GetScore(grid, pos);
                if (nodeList.Count < 4)
                {
                    nodeList.Add(node);
                }
                else
                {
                    foreach (var item in nodeList)
                    {
                        if (mySelf)//极大点
                        {
                            if (node.value > item.value)
                            {
                                nodeList.Remove(item);
                                nodeList.Add(node);
                                break;
                            }
                        }
                        else//极小点
                        {
                            if (node.value < item.value)
                            {
                                nodeList.Remove(item);
                                nodeList.Add(node);
                                break;
                            }
                        }
                    }
                }
            }
        }

        return nodeList;
    }

    //创建树
    public void CreateTree(MiniMaxNode node, ChessType[,] grid, int deep, bool mySelf)
    {
        if (deep == 0 || node.value == maxScore)
        {
            return;
        }
        grid[node.pos.x, node.pos.y] = node.chess;
        node.child = GetList(grid, node.chess, !mySelf);
        foreach (var item in node.child)
        {
            CreateTree(item, (ChessType[,])grid.Clone(), deep - 1, !mySelf);
        }
    }


    public float AlphaBeta(MiniMaxNode node, int deep, bool mySelf, float alpha, float beta)
    {

        if (deep == 0 || node.value == float.MaxValue || node.value == float.MinValue)
        {
            return node.value;
        }

        if (mySelf)
        {
            foreach (var child in node.child)
            {
                alpha = Mathf.Max(alpha, AlphaBeta(child, deep - 1, !mySelf, alpha, beta));

                //alpha剪枝

                if (alpha >= beta)
                {
                    return alpha;
                }

            }
            return alpha;
        }
        else
        {
            foreach (var child in node.child)
            {
                beta = Mathf.Min(beta, AlphaBeta(child, deep - 1, !mySelf, alpha, beta));

                //beta剪枝
                if (alpha >= beta)
                {
                    return beta;
                }

            }
            return beta;
        }
    }




}
