using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessType
{
    None,
    White,
    Black,
}


/// <summary>
/// 生产管理棋子
/// </summary>
public class ChessManager : AbsMono
{
    private const float maxTime = 1;

    [SerializeField]
    private GameObject whiteChess;
    [SerializeField]
    private GameObject blackChess;

    public ChessType NowChessType { get; private set; }
    private float playTimer;
    private Transform chessParent;

    public bool CanPlay { get { return playTimer <= 0; } }

    public override void OnAwake()
    {
        NowChessType = ChessType.White;
        playTimer = 0;
        chessParent = transform;// new GameObject("ChessParent").transform;
    }

    public override void OnUpdate()
    {
        if (playTimer > 0)
        {
            playTimer -= Time.deltaTime;
        }
    }

    public void DoPlayChess(Vector2 chessPos)
    {
        GameObject prefab = null;
        if (NowChessType == ChessType.White)
        {
            prefab = whiteChess;
        }
        else if (NowChessType == ChessType.Black)
        {
            prefab = blackChess;
        }
        Instantiate(prefab, chessPos, Quaternion.identity, chessParent);

        playTimer = maxTime;
    }

    public void SwitchNowChessType()
    {
        if (NowChessType == ChessType.White)
        {
            NowChessType = ChessType.Black;
        }
        else if (NowChessType == ChessType.Black)
        {
            NowChessType = ChessType.White;
        }
    }
}
