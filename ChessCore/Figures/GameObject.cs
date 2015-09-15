using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ChessCore.Figures;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для GameObject
  /// </summary>
  public class GameObject : GameState
  {
    public List<Figure> blacks = new List<Figure>();
    public List<Figure> whites = new List<Figure>();

    public Color GetColorTurn()
    {
      return this.moveCount % 2 == 0 ? Color.white : Color.black;
    }

    public GameObject()
    {
      for (sbyte i = 1; i <= 8; i++)
      {
        Pawn p1 = new Pawn(this, i, 7, Color.black);
        this.blacks.Add(p1);

        Pawn wp1 = new Pawn(this, i, 2, Color.white);
        this.whites.Add(wp1);
      }

      Bishop b1 = new Bishop(this, 3, 8, Color.black);
      this.blacks.Add(b1);

      Bishop b2 = new Bishop(this, 6, 8, Color.black);
      this.blacks.Add(b2);

      Bishop wb1 = new Bishop(this, 3, 1, Color.white);
      this.whites.Add(wb1);

      Bishop wb2 = new Bishop(this, 6, 1, Color.white);
      this.whites.Add(wb2);

      Rook r1 = new Rook(this, 1, 8, Color.black);
      this.blacks.Add(r1);

      Rook r2 = new Rook(this, 8, 8, Color.black);
      this.blacks.Add(r2);

      Rook wr1 = new Rook(this, 1, 1, Color.white);
      this.whites.Add(wr1);

      Rook wr2 = new Rook(this, 8, 1, Color.white);
      this.whites.Add(wr2);

      Knight kn1 = new Knight(this, 2, 8, Color.black);
      this.blacks.Add(kn1);

      Knight kn2 = new Knight(this, 7, 8, Color.black);
      this.blacks.Add(kn2);

      Knight wkn1 = new Knight(this, 2, 1, Color.white);
      this.whites.Add(wkn1);

      Knight wkn2 = new Knight(this, 7, 1, Color.white);
      this.whites.Add(wkn2);

      var q = new Queen(this, 4, 8, Color.black);
      this.blacks.Add(q);

      var wq = new Queen(this, 4, 1, Color.white);
      this.whites.Add(wq);

      var k = new King(this, 5, 8, Color.black);
      this.blacks.Add(k);

      var wk = new King(this, 5, 1, Color.white);
      this.whites.Add(wk);

      this.UpdateAllBeatFields();
    }

    public static GameObject GameObjectFromGameState(GameState fromState)
    {
      if (fromState == null)
        throw new Exception("Trying to restore game object from empty state.");
      return (GameObject)fromState;
    }

    public bool IsOutOfBound(sbyte _x, sbyte _y)
    {
      return !(_x >= 1 && _x <= 8 && _y >= 1 && _y <= 8);
    }

    public King GetKing(Color col)
    {
      if (col == Color.white)
      {
        foreach (Figure f in this.whites)
          if (f.type == FigureTypes.King)
            return (King) f;
      }
      else
      {
        foreach (Figure f in this.blacks)
          if (f.type == FigureTypes.King)
            return (King) f;
      }
      return null;
    }

    public bool Transform(sbyte x, sbyte y, FigureTypes type)
    {
      Figure p = this.GetFigureByXY(x, y);
      if (p.type != FigureTypes.Pawn)
        return false;
      Figure newFigure;
      switch (type)
      {
        case FigureTypes.Knight:
          newFigure = new Knight(this, p.field.x, p.field.y, p.color);
          break;
        case FigureTypes.Bishop:
          newFigure = new Bishop(this, p.field.x, p.field.y, p.color);
          break;
        case FigureTypes.Queen:
          newFigure = new Queen(this, p.field.x, p.field.y, p.color);
          break;
        case FigureTypes.Rook:
          newFigure = new Rook(this, p.field.x, p.field.y, p.color);
          break;
        default:
          return false;
      }
      if (p.color == Color.white)
      {
        this.whites.Remove(p);
        this.whites.Add(newFigure);
      }
      else
      {
        this.blacks.Remove(p);
        this.blacks.Add(newFigure);
      }
      this.UpdateAllBeatFields();
      this.IsGameOvered();
      return true;
    }

    public bool RemoveFigureByXY(sbyte _x, sbyte _y)
    {
      foreach (Figure f in this.whites)
        if (f.field.x == _x && f.field.y == _y)
        {
          this.whites.Remove(f);
          return true;
        }
      foreach (Figure b in this.blacks)
        if (b.field.x == _x && b.field.y == _y)
        {
          this.blacks.Remove(b);
          return true;
        }
      return false;
    }

    public Figure GetFigureByXY(sbyte _x, sbyte _y)
    {
      if (this.IsOutOfBound(_x, _y))
        return null;
      foreach (var f in this.whites)
        if (f.field.x == _x && f.field.y == _y) return f;
      foreach (var b in this.blacks)
        if (b.field.x == _x && b.field.y == _y) return b;
      return null;
    }

    public bool IsFieldUnderAttack(sbyte _x, sbyte _y, Color _c)
    {
      if (_c == Color.black)
      {
        foreach (Figure w in this.whites)
        {
          foreach (Field bf in w.BeatFields)
            if (bf.x == _x && bf.y == _y)
              return true;
          foreach (var af in w.AttackFields)
            if (af.x == _x && af.y == _y)
              return true;
        }
      }
      if (_c == Color.white)
      {
        foreach (Figure b in this.blacks)
        {
          foreach (Field bf in b.BeatFields)
            if (bf.x == _x && bf.y == _y)
              return true;
          foreach (Field af in b.AttackFields)
            if (af.x == _x && af.y == _y)
              return true;
        }
      }
      return false;
    }

    public void UpdateAllBeatFields()
    {
      foreach (Figure w in this.whites)
        if (w.type != FigureTypes.King)
          w.GetBeatFields();
      foreach (Figure b in this.blacks)
        if (b.type != FigureTypes.King)
          b.GetBeatFields();
      King wk = this.GetKing(Color.white);
      King bk = this.GetKing(Color.black);
      wk.GetBeatFields();
      bk.GetBeatFields();
    }

    public void IsGameOvered()
    {
      this.isMat = this.IsMat();
      this.isPat = this.IsPat();
      this.isDraw = this.IsDraw();
    }

    public bool IsPat()
    {
      Color color = this.GetColorTurn();
      King king = this.GetKing(color);
      if (!this.IsFieldUnderAttack(king.field.x, king.field.y, color))
      {
        List<Figure> figures;
        if (color == Color.black)
          figures = this.blacks;
        else
          figures = this.whites;
        foreach (Figure figure in figures)
        {
          foreach (Field field in figure.MoveFields)
            if (figure.TryMove(field.x, field.y))
              return false;

          foreach (Field field in figure.BeatFields)
          {
            Figure ftt = this.GetFigureByXY(field.x, field.y);
            if (figure.type == FigureTypes.Pawn && ftt == null)
            {
              Figure f = null;
              if (color == Color.black)
                f = this.GetFigureByXY(field.x, (sbyte) (field.y + 1));
              if (color == Color.white)
                f = this.GetFigureByXY(field.x, (sbyte) (field.y - 1));
              if (f != null && f.type == FigureTypes.Pawn && f.moveCount == this.moveCount)
                ftt = f;
            }
            if (figure.TryTakeFigure(ftt, figure.field.x, figure.field.y))
              return false;
          }
        }
        return true;
      }
      return false;
    }

    public bool IsMat()
    {
      Color color = this.GetColorTurn();
      King king = this.GetKing(color);
      if (this.IsFieldUnderAttack(king.field.x, king.field.y, color))
      {
        List<Figure> figures;
        if (color == Color.black)
          figures = this.blacks;
        else
          figures = this.whites;
        foreach (var figure in figures)
        {
          foreach (var field in figure.MoveFields)
            if (figure.TryMove(field.x, field.y))
              return false;

          foreach (Field field in figure.BeatFields)
          {
            Figure ftt = this.GetFigureByXY(field.x, field.y);
            if (figure.type == FigureTypes.Pawn && ftt == null)
            {
              Figure f = null;
              if (color == Color.black)
                f = this.GetFigureByXY(field.x, (sbyte) (field.y + 1));
              if (color == Color.white)
                f = this.GetFigureByXY(field.x, (sbyte) (field.y - 1));
              if (f != null && f.type == FigureTypes.Pawn && f.moveCount == this.moveCount)
                ftt = f;
            }
            if (figure.TryTakeFigure(ftt, figure.field.x, figure.field.y))
              return false;
          }
        }

        return true;
      }
      return false;
    }

    public bool IsDraw()
    {
      if (movesToDraw >= 100)
        return true;
      return false;
    }

    public bool IsPositionRepeatsThirdTime()
    {
      foreach (string phash in positionsHash)
      {
        sbyte repeats = 0;
        foreach (string hashToCompare in positionsHash)
        {
          if (hashToCompare == phash)
            repeats++;
          if (repeats >= 3)
            return true;
        }
      }
      return false;
    }

    public string GenertePositionHash()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(whites.Count);
      sb.Append(blacks.Count);
      foreach (Figure figure in whites)
      {
        sb.AppendFormat("s{0}{1}{2}", (int) figure.type, figure.field.x, figure.field.y);
        foreach (Field field in figure.MoveFields)
        {
          sb.AppendFormat("m{0}{1}", field.x, field.y);
        }
        foreach (Field field in figure.AttackFields)
        {
          sb.AppendFormat("a{0}{1}", field.x, field.y);
        }
        foreach (Field field in figure.BeatFields)
        {
          sb.AppendFormat("b{0}{1}", field.x, field.y);
        }
      }
      foreach (Figure figure in blacks)
      {
        sb.AppendFormat("s{0}{1}{2}", (int) figure.type, figure.field.x, figure.field.y);
        foreach (Field field in figure.MoveFields)
        {
          sb.AppendFormat("m{0}{1}", field.x, field.y);
        }
        foreach (Field field in figure.AttackFields)
        {
          sb.AppendFormat("a{0}{1}", field.x, field.y);
        }
        foreach (Field field in figure.BeatFields)
        {
          sb.AppendFormat("b{0}{1}", field.x, field.y);
        }
      }

      return Utils.CalculateMD5Hash(sb.ToString());
    }

    public GameState GetState()
    {
      GameState gs = new GameState()
        {
          blacksTimeLeft = this.blacksTimeLeft,
          gameId = this.gameId,
          gameOvered = this.gameOvered,
          isDraw = this.isDraw,
          isMat = this.isMat,
          isPat = this.isPat,
          mode = this.mode,
          moveCount = this.moveCount,
          moves = this.moves,
          movesToDraw = this.movesToDraw,
          positionsHash = this.positionsHash,
          status = this.status,
          whitesTimeLeft = this.whitesTimeLeft
        };
      foreach (Figure f in whites)
        gs._whites.Add(f.GetState());
      foreach (Figure f in blacks)
        gs._blacks.Add(f.GetState());
      return gs;
    }
  }
}