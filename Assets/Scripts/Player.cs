using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player :  IMono
{
    protected static MainGameManager mainGameManager;
    protected static ChessManager chessManager;
    protected static ChessBoardManager chessBoardManager;

    public ChessType ChessType { get; protected set; }

    public virtual Player OnInit(ChessType _chessType)
    {
        ChessType = _chessType;

        return this;
    }

    public virtual void OnAwake()
    {

    }

    public virtual void OnStart()
    {
        if(mainGameManager==null)
        {
            mainGameManager = MainGameManager.Instance;
            chessBoardManager = MainGameManager.Instance.ChessBoardManager;
            chessManager = MainGameManager.Instance.ChessManager;
        }
    }

    public virtual void OnUpdate()
    {
        PlayChess();
    }

    public virtual void PlayChess()
    {
        if (chessManager.CanPlay && Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {

            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pointPos;
            if (chessBoardManager.InputAxisToPoint(p, out pointPos))
            {
                Vector2 chessPos;
                if (chessBoardManager.PointCanPlayChess(pointPos) && chessBoardManager.GetAxisByPoint(pointPos, out chessPos))
                {
                    var go = chessManager.DoPlayChess(chessPos);
                    if(chessBoardManager.PlayChess(pointPos, ChessType, go))
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
    }





    public void OnRelease()
    {
        throw new System.NotImplementedException();
    }


}
