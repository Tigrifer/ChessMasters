using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для King
  /// </summary>
  public class King : Figure
  {
    public List<Field> moves;

    public King(GameObject _g, sbyte x, sbyte y, Color c)
    {
      this.GameObject = _g;
      this.field = new Field(x, y);
      this.color = c;
      this.moveCount = 0;
      this.type = FigureTypes.King;
      this.moves = new List<Field>();
      this.moves.Add(new Field((sbyte) 1, (sbyte) 1));
      this.moves.Add(new Field((sbyte) 1, (sbyte) 0));
      this.moves.Add(new Field((sbyte) 1, (sbyte) -1));
      this.moves.Add(new Field((sbyte) 0, (sbyte) 1));
      this.moves.Add(new Field((sbyte) 0, (sbyte) -1));
      this.moves.Add(new Field((sbyte) -1, (sbyte) 1));
      this.moves.Add(new Field((sbyte) -1, (sbyte) 0));
      this.moves.Add(new Field((sbyte) -1, (sbyte) -1));
      this.type = FigureTypes.King;
    }

    public override void GetBeatFields()
    {
      this.BeatFields = new List<Field>();
      this.AttackFields = new List<Field>();
      this.MoveFields = new List<Field>();
      if (this.disabled) return;
      foreach (var m in this.moves)
      {
        if (this.GameObject.IsOutOfBound((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)))
          continue;
        this.AttackFields.Add(new Field((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)));

        var f1 = this.GameObject.GetFigureByXY((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y));
        if (f1 == null &&
            !this.GameObject.IsFieldUnderAttack((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y), this.color))
          this.MoveFields.Add(new Field((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)));
      }
      // short castling
      Figure rook = this.GameObject.GetFigureByXY(8, this.field.y);
      if (this.moveCount == 0 && rook != null &&
          rook.moveCount == 0 && this.field.x == 5 &&
          this.GameObject.GetFigureByXY(7, this.field.y) == null &&
          this.GameObject.GetFigureByXY(6, this.field.y) == null &&
          !this.GameObject.IsFieldUnderAttack(6, this.field.y, this.color) &&
          !this.GameObject.IsFieldUnderAttack(7, this.field.y, this.color) &&
          !this.GameObject.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
        this.MoveFields.Add(new Field(7, this.field.y));

      // long castling
      rook = this.GameObject.GetFigureByXY(1, this.field.y);
      if (this.moveCount == 0 && rook != null &&
          rook.moveCount == 0 && this.field.x == 5 &&
          this.GameObject.GetFigureByXY(3, this.field.y) == null &&
          this.GameObject.GetFigureByXY(4, this.field.y) == null &&
          !this.GameObject.IsFieldUnderAttack(3, this.field.y, this.color) &&
          !this.GameObject.IsFieldUnderAttack(4, this.field.y, this.color) &&
          !this.GameObject.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
        this.MoveFields.Add(new Field(3, this.field.y));
    }
  }
}