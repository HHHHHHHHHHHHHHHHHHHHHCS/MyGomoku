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
    private List<MiniMaxNode> GetList(int[,] grid,ChessType chessType ,bool isSelf)
    {
        List<MiniMaxNode> nodelist = new List<MiniMaxNode>();
        MiniMaxNode node;
        for (int y=0;y<ChessBoardManager.chessMaxBoard;y++)
        {
            for (int x = 0; x < ChessBoardManager.chessMaxBoard; x++)
            {

                if (grid[x, y] != 0)
                {
                    continue;
                }
                node = new MiniMaxNode
                {
                    pos = new Vector2Int(x, y),
                    chessType = chessType
                };

                node.value = (isSelf ? 1 : -1) * SetCheckScore(x, y, chessType);

                if (nodelist.Count < 4)
                {
                    nodelist.Add(node);
                }
                else
                {
                    for(int i=0;i<nodelist.Count;i++)
                    {
                        if (isSelf)
                        {//极大值
                            if (nodelist[i].value >= node.value)
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



}
