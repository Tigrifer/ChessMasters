using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessCore.Figures;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для Objext
  /// </summary>
  public abstract class Figure : FigureState
  {
    public bool disabled = false;
    public GameObject GameObject;
    public List<Field> BeatFields = new List<Field>();
    public List<Field> MoveFields = new List<Field>();
    public List<Field> AttackFields = new List<Field>();
    public abstract void GetBeatFields();

    public bool Move(sbyte _x, sbyte _y)
    {
      bool takenOrPawnMove = false;
      Field from = new Field(this.field.x, this.field.y);
      Field to = new Field(_x, _y);
      this.GameObject.UpdateAllBeatFields();
      if (GameObject.IsOutOfBound(_x, _y) || GameObject.GetColorTurn() != this.color)
        return false;
      int old_x = this.field.x;
      Figure figureToTake = this.GameObject.GetFigureByXY(_x, _y);
      // if crossfield taking
      if (this.type == FigureTypes.Pawn && figureToTake == null)
      {
        Figure f = null;
        if (this.color == Color.black)
          f = this.GameObject.GetFigureByXY(_x, (sbyte) (_y + 1));
        if (this.color == Color.white)
          f = this.GameObject.GetFigureByXY(_x, (sbyte) (_y - 1));
        if (f != null && f.type == FigureTypes.Pawn && f.moveCount == this.GameObject.moveCount && f.color != color)
          figureToTake = f;
      }
      bool moved = false;
      if (figureToTake != null)
      {
        if (this.TryTakeFigure(figureToTake, _x, _y))
        {
          moved = true;
          if (!this.GameObject.RemoveFigureByXY(figureToTake.field.x, figureToTake.field.y))
          {
            moved = false;
          }
          this.field.x = _x;
          this.field.y = _y;
          takenOrPawnMove = true;
        }
      }
      else
      {
        if (this.TryMove(_x, _y))
        {
          moved = true;
          this.field.x = _x;
          this.field.y = _y;
          takenOrPawnMove = this.type == FigureTypes.Pawn;
        }
      }
      // is castling - move rook
      if (this.type == FigureTypes.King && Math.Abs(_x - old_x) == 2 && moved)
      {
        sbyte rook_x = (sbyte) (_x == 7 ? 8 : 1);
        Figure rook = this.GameObject.GetFigureByXY(rook_x, this.field.y);
        rook.field.x = (sbyte) ((old_x + _x)/2);
      }

      if (moved)
      {
        this.GameObject.moveCount++;
        this.moveCount = this.GameObject.moveCount;
        this.GameObject.UpdateAllBeatFields();
        if (takenOrPawnMove)
          this.GameObject.movesToDraw = 0;
        else
          this.GameObject.movesToDraw++;
        this.GameObject.IsGameOvered();
        this.GameObject.moves.Add(new Moves(from, to));
        this.GameObject.positionsHash.Add(this.GameObject.GenertePositionHash());
      }
      return moved;
    }

    public bool TryTakeFigure(Figure ftt, sbyte _x, sbyte _y)
    {
      if (!CanAttackPosition(_x, _y))
        return false;
      if (ftt == null || this.color == ftt.color)
        return false;
      ftt.disabled = true;
      sbyte old_x = this.field.x;
      sbyte old_y = this.field.y;
      short old_mc = this.moveCount;
      sbyte old_ftt_x = ftt.field.x;
      sbyte old_ftt_y = ftt.field.y;
      ftt.field.x = (sbyte) -1;
      ftt.field.y = (sbyte) -1; // move figure outside the board
      this.field.x = _x;
      this.field.y = _y;
      this.moveCount = this.GameObject.moveCount;
      this.GameObject.UpdateAllBeatFields();
      King k = this.GameObject.GetKing(this.color);
      bool ok = !this.GameObject.IsFieldUnderAttack(k.field.x, k.field.y, k.color);
      this.field.x = old_x;
      this.field.y = old_y;
      this.moveCount = old_mc;
      ftt.disabled = false;
      ftt.field.x = old_ftt_x;
      ftt.field.y = old_ftt_y;
      this.GameObject.UpdateAllBeatFields();
      return ok;
    }

    public bool TryMove(sbyte _x, sbyte _y)
    {
      if (!CanMoveToPosition(_x, _y))
        return false;
      sbyte old_x = this.field.x;
      sbyte old_y = this.field.y;
      short old_mc = this.moveCount;
      this.field.x = _x;
      this.field.y = _y;
      this.moveCount = this.GameObject.moveCount;
      this.GameObject.UpdateAllBeatFields();
      Figure k = this.GameObject.GetKing(this.color);
      bool ok = !this.GameObject.IsFieldUnderAttack(k.field.x, k.field.y, k.color);
      this.field.x = old_x;
      this.field.y = old_y;
      this.moveCount = old_mc;
      this.GameObject.UpdateAllBeatFields();
      return ok;
    }

    public bool CanAttackPosition(sbyte x, sbyte y)
    {
      foreach (Field field in this.AttackFields)
        if (field.x == x && field.y == y)
          return true;
      foreach (Field field in this.BeatFields)
        if (field.x == x && field.y == y)
          return true;
      return false;
    }

    public bool CanMoveToPosition(sbyte x, sbyte y)
    {
      foreach (Field field in this.MoveFields)
        if (field.x == x && field.y == y)
          return true;
      return false;
    }

    public FigureState GetState()
    {
      FigureState fs = new FigureState()
      {
        color = this.color,
        field = this.field,
        moveCount = this.moveCount,
        type = this.type
      };
      return fs;
    }
  }
}