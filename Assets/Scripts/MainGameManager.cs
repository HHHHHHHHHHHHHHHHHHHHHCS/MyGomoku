using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public MainUIManager MainUIManager { get; private set; }
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public ChessBoardManager ChessBoardManager { get; private set; }
    public ChessManager ChessManager { get; private set; }

    public bool IsStart { get; private set; }
    public bool IsWin { get; private set; }
    public Player NowPlayer { get; private set; }

    private void Awake()
    {
        Instance = this;
        ChessBoardManager = GameObject.Find("ChessBoard").GetComponent<ChessBoardManager>();
        ChessManager = GameObject.Find("ChessManager").GetComponent<ChessManager>();
        MainUIManager = GameObject.Find("UIRoot").GetComponent<MainUIManager>();

        switch (PlayerInfo.gameModel)
        {
            case PlayerInfo.GameModel.ManMachine:
                bool playerFirst = PlayerInfo.isPlayerFirst;
                Player1 = new Player().OnInit(playerFirst ? ChessType.White: ChessType.Black);
                switch (PlayerInfo.aiLevel)
                {
                    case PlayerInfo.AILevel.Primary:
                        Player2 = new AILevelOne();
                        break;
                    case PlayerInfo.AILevel.Intermediate:
                        Player2 = new AILevelTwo();
                        break;
                    case PlayerInfo.AILevel.Senior:
                        Player2 = new AILevelThree();
                        break;
                    default:
                        break;
                }
                Player2.OnInit(playerFirst ? ChessType.Black : ChessType.White);
                NowPlayer = playerFirst? Player1:Player2;
                break;
            case PlayerInfo.GameModel.DoubleMan:
                Player1 = new Player().OnInit(ChessType.White);
                Player2 = new Player().OnInit(ChessType.Black);
                NowPlayer = Player1;
                break;
            case PlayerInfo.GameModel.Net:
                Debug.Log("还没有做");
                break;
            default:
                break;
        }

        ChessBoardManager.OnAwake();
        ChessManager.OnAwake();
        MainUIManager.OnAwake();
        Player1.OnAwake();
        Player2.OnAwake();


    }

    private void Start()
    {
        MainUIManager.OnStart();
        ChessManager.OnStart();
        Player1.OnStart();
        Player2.OnStart();
    }

    private void Update()
    {
        if(IsStart && !IsWin)
        {
            NowPlayer.OnUpdate();
            ChessManager.OnUpdate();
        }
    }

    public void StartGame()
    {
        IsStart = true;
    }

    public void WinGame()
    {
        IsWin = true;
        MainUIManager.EndGame(ChessManager.NowChessType);
    }

    public void SwitchNowPlayer()
    {
        NowPlayer = NowPlayer == Player1 ? Player2 : Player1;
        ChessManager.NowChessType = NowPlayer.ChessType;
    }
}
