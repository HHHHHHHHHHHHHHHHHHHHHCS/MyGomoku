using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IMono
{
    public ChessBoardManager ChessBoardManager { get; private set; }
    public ChessManager ChessManager { get; private set; }

    private bool isWin;

    public void OnAwake()
    {
        ChessBoardManager = GameObject.Find("ChessBoard").GetComponent<ChessBoardManager>();
        ChessManager = GameObject.Find("ChessManager").GetComponent<ChessManager>();

        ChessBoardManager.OnAwake();
        ChessManager.OnAwake();
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {
        if (!isWin)
        {
            PlayerPlayChess();
            ChessManager.OnUpdate();
        }
    }

    public void PlayerPlayChess()
    {
        if (ChessManager.CanPlay && Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject())
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pointPos;
            if (ChessBoardManager.InputAxisToPoint(p, out pointPos))
            {
                Vector2 chessPos;
                if (ChessBoardManager.PointCanPlayChess(pointPos) && ChessBoardManager.GetAxisByPoint(pointPos, out chessPos))
                {
                    var go = ChessManager.DoPlayChess(chessPos);
                    isWin = ChessBoardManager.PlayChess(pointPos, ChessManager.NowChessType, go);
                    if (!isWin)
                    {
                        ChessManager.SwitchNowChessType();
                    }
                }
            }
        }
    }





    public void OnRelease()
    {
        throw new System.NotImplementedException();
    }


}
