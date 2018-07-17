using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxNode
{
    public ChessType chessType;
    public Vector2Int pos;
    public List<MiniMaxNode> child;
    public int value;
}


public class AILevelThree : AILevelTwo
{
    public override void PlayerPlayChess()
    {
        if (chessBoardManager.chessInfoStack.Count == 0)
        {
            AIPlayChess(7, 7);
        }
        else
        {
            MiniMaxNode node = null;
            foreach(var item in GetList(chessBoardManager.GridArray,ChessType,true))
            {
                CreateTree(item, (ChessType[,])chessBoardManager.GridArray.Clone(), 3, false);
                int a = maxScore;
                int b = -maxScore;
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

    }

    private List<MiniMaxNode> GetList(ChessType[,] grid, ChessType chessType, bool isSelf)
    {
        List<MiniMaxNode> nodelist = new List<MiniMaxNode>();
        MiniMaxNode node;
        for (int y = 0; y < ChessBoardManager.chessMaxBoard; y++)
        {
            for (int x = 0; x < ChessBoardManager.chessMaxBoard; x++)
            {

                if (grid[x, y] !=  ChessType.None)
                {
                    continue;
                }
                node = new MiniMaxNode
                {
                    pos = new Vector2Int(x, y),
                    chessType = isSelf ? chessType : (chessType == ChessType.White ? ChessType.Black : ChessType.White)
                };

                node.value = (isSelf ? 1 : -1) * SetCheckScore(grid,x, y, node.chessType);

                if (nodelist.Count < 4)
                {
                    nodelist.Add(node);
                }
                else
                {
                    for (int i = 0; i < nodelist.Count; i++)
                    {
                        if (isSelf)
                        {//极大值
                            if (nodelist[i].value > node.value)
                            {
                                nodelist.RemoveAt(i);
                                nodelist.Add(node);
                                break;
                            }
                        }
                        else if (nodelist[i].value < node.value)
                        {//极小值
                            nodelist.RemoveAt(i);
                            nodelist.Add(node);
                            break;
                        }
                    }
                }
            }
        }
        return nodelist;
    }

    private void CreateTree(MiniMaxNode node, ChessType[,] grid, int deep, bool isSelf)
    {
        if (deep == 0 || node.value == maxScore)
        {
            return;
        }
        grid[node.pos.x, node.pos.y] = node.chessType;
        node.child = GetList(grid, node.chessType, !isSelf);
        foreach (var item in node.child)
        {
            CreateTree(item, (ChessType[,])grid.Clone(), deep - 1, !isSelf);
        }
    }

    private int AlphaBeta(MiniMaxNode node, int deep, bool isSelf, int alpha, int beta)
    {
        if (deep == 0 || node.value == maxScore || node.value ==-maxScore)
        {
            return node.value;
        }

        if (isSelf)
        {
            foreach (var child in node.child)
            {
                alpha = Mathf.Max(alpha, AlphaBeta(child, deep - 1, !isSelf, alpha, beta));

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
                beta = Mathf.Min(beta, AlphaBeta(child, deep - 1, !isSelf, alpha, beta));

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
