using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    public bool gameOvered = false;
    public GameStatus status;
}

public enum GameStatus
{
    GameInProccess = 0,
    WhitesWins = 1,
    BlackWins = 2,
    Draw = 3
}