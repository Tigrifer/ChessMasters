using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для Pawn
  /// </summary>
  public class Pawn : Figure
  {
    public Pawn(GameObject _g, sbyte x, sbyte y, Color c)
    {
      this.GameObject = _g;
      this.field = new Field(x, y);
      this.color = c;
      this.moveCount = 0;
      this.type = FigureTypes.Pawn;
    }

    public override void GetBeatFields()
    {
      this.BeatFields = new List<Field>();
      this.MoveFields = new List<Field>();
      this.AttackFields = new List<Field>();
      if (this.disabled) return;
      if (this.color == Color.white)
      {
        if (this.GameObject.GetFigureByXY(this.field.x, (sbyte) (this.field.y + 1)) == null &&
            !this.GameObject.IsOutOfBound(this.field.x, (sbyte) (this.field.y + 1))) // move forward
        {
          this.MoveFields.Add(new Field(this.field.x, (sbyte) (this.field.y + 1)));
          if (this.moveCount == 0 && this.GameObject.GetFigureByXY(this.field.x, (sbyte) (this.field.y + 2)) == null)
            // first long move
            this.MoveFields.Add(new Field(this.field.x, (sbyte) (this.field.y + 2)));
        }
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x + 1), (sbyte) (this.field.y + 1));
        if (f != null && f.color != this.color) // take enemy right
          this.BeatFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y + 1)));
        Figure f1 = this.GameObject.GetFigureByXY((sbyte) (this.field.x - 1), (sbyte) (this.field.y + 1));
        if (f1 != null && f1.color != this.color) // take enemy left
          this.BeatFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y + 1)));
        // take enemy pow on crossfield
        Figure fcl = this.GameObject.GetFigureByXY((sbyte) (this.field.x - 1), this.field.y); // left
        if (fcl != null && fcl.type == FigureTypes.Pawn && fcl.color != this.color &&
            fcl.moveCount == this.GameObject.moveCount)
        {
          this.BeatFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y + 1)));
        }
        var fcr = this.GameObject.GetFigureByXY((sbyte) (this.field.x + 1), this.field.y); // right
        if (fcr != null && fcr.type == FigureTypes.Pawn && fcr.color != this.color &&
            fcr.moveCount == this.GameObject.moveCount)
        {
          this.BeatFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y + 1)));
        }

        if (!this.GameObject.IsOutOfBound((sbyte) (this.field.x + 1), (sbyte) (this.field.y + 1)))
          this.AttackFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y + 1)));
        if (!this.GameObject.IsOutOfBound((sbyte) (this.field.x - 1), (sbyte) (this.field.y + 1)))
          this.AttackFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y + 1)));
      }
      if (this.color == Color.black)
      {
        if (this.GameObject.GetFigureByXY(this.field.x, (sbyte) (this.field.y - 1)) == null &&
            !this.GameObject.IsOutOfBound(this.field.x, (sbyte) (this.field.y - 1))) // move forward
        {
          this.MoveFields.Add(new Field(this.field.x, (sbyte) (this.field.y - 1)));
          if (this.moveCount == 0 && this.GameObject.GetFigureByXY(this.field.x, (sbyte) (this.field.y - 2)) == null)
            // first long move
            this.MoveFields.Add(new Field(this.field.x, (sbyte) (this.field.y - 2)));
        }
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x + 1), (sbyte) (this.field.y - 1));
        if (f != null && f.color != this.color) // take enemy right
          this.BeatFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y - 1)));
        Figure f1 = this.GameObject.GetFigureByXY((sbyte) (this.field.x - 1), (sbyte) (this.field.y - 1));
        if (f1 != null && f1.color != this.color) // take enemy left
          this.BeatFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y - 1)));
        // take enemy pow on crossfield
        Figure fcl = this.GameObject.GetFigureByXY((sbyte) (this.field.x - 1), this.field.y); // left
        if (fcl != null && fcl.type == FigureTypes.Pawn && fcl.color != this.color &&
            fcl.moveCount == this.GameObject.moveCount)
          this.BeatFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y - 1)));
        Figure fcr = this.GameObject.GetFigureByXY((sbyte) (this.field.x + 1), this.field.y); // right
        if (fcr != null && fcr.type == FigureTypes.Pawn && fcr.color != this.color &&
            fcr.moveCount == this.GameObject.moveCount)
        {
          this.BeatFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y - 1)));
        }

        if (!this.GameObject.IsOutOfBound((sbyte) (this.field.x + 1), (sbyte) (this.field.y - 1)))
          this.AttackFields.Add(new Field((sbyte) (this.field.x + 1), (sbyte) (this.field.y - 1)));
        if (!this.GameObject.IsOutOfBound((sbyte) (this.field.x - 1), (sbyte) (this.field.y - 1)))
          this.AttackFields.Add(new Field((sbyte) (this.field.x - 1), (sbyte) (this.field.y - 1)));
      }
    }

    public bool Transform(FigureTypes figureTypes)
    {
      if ((field.y == 8 && color == Color.white) ||
          (field.y == 1 && color == Color.black))
      {
        Figure newFigure;
        switch (figureTypes)
        {
          case FigureTypes.Bishop:
            newFigure = new Bishop(GameObject, field.x, field.y, color);
            break;
          case FigureTypes.Knight:
            newFigure = new Knight(GameObject, field.x, field.y, color);
            break;
          case FigureTypes.Queen:
            newFigure = new Queen(GameObject, field.x, field.y, color);
            break;
          case FigureTypes.Rook:
            newFigure = new Rook(GameObject, field.x, field.y, color);
            break;
          default:
            return false;
        }
        GameObject.RemoveFigureByXY(field.x, field.y);
        if (color == Color.black)
          GameObject.blacks.Add(newFigure);
        else
          GameObject.whites.Add(newFigure);
        GameObject.UpdateAllBeatFields();
        return true;
      }
      return false;
    }
  }
}