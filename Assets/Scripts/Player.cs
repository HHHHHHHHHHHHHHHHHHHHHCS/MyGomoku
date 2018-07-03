using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly Vector2 startPos = new Vector2(6.58f, 6.45f), chessScale = new Vector2(0.95f, 0.95f)
        , halfChessScale, offestPos;

    public ChessType chessType = ChessType.Black;

    static Player()
    {
        halfChessScale = chessScale / 2;
        offestPos = new Vector2((startPos.x + halfChessScale.x) / chessScale.x, (startPos.y + halfChessScale.y) / chessScale.y);
    }


    public void Update()
    {
        PlayerChess();
    }

    public void PlayerChess()
    {
        if (Input.GetMouseButton(0))
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            InputPointToAxis(p);

        }
    }

    private Vector2Int InputPointToAxis(Vector3 inputPos)
    {
        Vector2Int vec2 = new Vector2Int((int)(inputPos.x  / chessScale.x+ offestPos.x), (int)(inputPos.y  / chessScale.y+ offestPos.y));
        vec2.x = Mathf.Clamp(vec2.x, 0, 14);
        vec2.y = Mathf.Clamp(vec2.y, 0, 14);
        Debug.Log(vec2);
        return vec2;
    }
}
