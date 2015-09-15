using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessCore.Figures;

/// <summary>
/// Сводное описание для GameState
/// </summary>
[Serializable]
public class GameState
{
  public int gameId;
  public sbyte moveCount = 0;
  public List<FigureState> _blacks = new List<FigureState>();
  public List<FigureState> _whites = new List<FigureState>();
  public bool isMat = false;
  public bool isPat = false;
  public bool isDraw = false;
  public bool gameOvered = false;
  public GameStatus status;
  public int movesToDraw = 0;
  public List<Moves> moves = new List<Moves>();
  public TimeSpan blacksTimeLeft = new TimeSpan();
  public TimeSpan whitesTimeLeft = new TimeSpan();
  public GameMode mode;
  public List<string> positionsHash = new List<string>();
}

public enum GameStatus
{
  InProccess = 0,
  WhitesWins = 1,
  BlackWins = 2,
  Draw = 3,
  Canceled = 4
}

public enum GameMode
{
  Unlimited = 0,
  WhitesWins = 1,
  BlackWins = 2,
  Draw = 3,
  Canceled = 4
}