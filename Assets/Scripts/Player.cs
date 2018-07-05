using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ChessBoardManager ChessBoardManager { get; private set; }
    public ChessManager ChessManager { get; private set; }



    private void Awake()
    {
        ChessBoardManager = GameObject.Find("ChessBoard").GetComponent<ChessBoardManager>();
        ChessManager = GameObject.Find("ChessManager").GetComponent<ChessManager>();

        ChessBoardManager.OnAwake();
        ChessManager.OnAwake();
    }

    public void Update()
    {
        PlayerPlayChess();

        ChessManager.OnUpdate();
    }

    public void PlayerPlayChess()
    {
        if (ChessManager.CanPlay && Input.GetMouseButtonDown(0))
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pointPos;
            if (ChessBoardManager.InputAxisToPoint(p, out pointPos))
            {
                Vector2 chessPos;
                if (ChessBoardManager.PointCanPlayChess(pointPos) && ChessBoardManager.GetAxisByPoint(pointPos, out chessPos))
                {
                    ChessManager.DoPlayChess(chessPos);
                    ChessBoardManager.PlayChess(pointPos, ChessManager.NowChessType);
                    ChessManager.SwitchNowChessType();
                }
            }
        }
    }


}
