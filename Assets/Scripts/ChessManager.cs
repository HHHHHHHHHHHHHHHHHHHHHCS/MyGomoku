using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessType
{
    None,
    White,
    Black,
}

public struct ChessInfo
{
    public Vector2Int pos;
    public ChessType chessType;
    public GameObject go;

    public ChessInfo(Vector2Int _pos, ChessType _chessType, GameObject _go)
    {
        pos = _pos;
        chessType = _chessType;
        go = _go;
    }
}



/// <summary>
/// 生产管理棋子
/// </summary>
public class ChessManager : MonoBehaviour, IMono
{
    private const float maxTime = 1;

    [SerializeField]
    private GameObject whiteChess;
    [SerializeField]
    private GameObject blackChess;
    [SerializeField]
    private GameObject redMarkPrefab;

    public ChessType NowChessType { get; set; }
    private float playTimer;
    private Transform chessParent;
    private Transform redMark;

    public bool CanPlay { get { return playTimer <= 0; } }

    public void OnAwake()
    {
        playTimer = 0;
        chessParent = transform;// new GameObject("ChessParent").transform;
    }

    public void OnStart()
    {
        NowChessType = MainGameManager.Instance.NowPlayer.ChessType;
    }

    public void OnUpdate()
    {
        if (playTimer > 0)
        {
            playTimer -= Time.deltaTime;
        }
    }

    public GameObject DoPlayChess(Vector2 chessPos)
    {
        GameObject prefab = null,go=null;
        if (NowChessType == ChessType.White)
        {
            prefab = whiteChess;
        }
        else if (NowChessType == ChessType.Black)
        {
            prefab = blackChess;
        }
        if(prefab != null)
        {
            go = Instantiate(prefab, chessPos, Quaternion.identity, chessParent);
            ChangeRedMark(chessPos);
            playTimer = maxTime;
        }
        return go;
    }

    public void OnRelease()
    {
        throw new System.NotImplementedException();
    }

    private void ChangeRedMark(Vector3 pos)
    {
        if(redMark==null)
        {
            redMark = Instantiate(redMarkPrefab, pos, Quaternion.identity).transform;
        }
        else
        {
            redMark.transform.position = pos;
        }
    }


}
