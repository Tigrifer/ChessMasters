using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для Knight
  /// </summary>
  public class Knight : Figure
  {
    private List<Field> moves;

    public Knight(GameObject _g, sbyte x, sbyte y, Color c)
    {
      this.GameObject = _g;
      this.field = new Field(x, y);
      this.color = c;
      this.moveCount = 0;
      this.type = FigureTypes.Knight;
      this.moves = new List<Field>();
      this.moves.Add(new Field((sbyte) 1, (sbyte) 2));
      this.moves.Add(new Field((sbyte) -1, (sbyte) 2));
      this.moves.Add(new Field((sbyte) 1, (sbyte) -2));
      this.moves.Add(new Field((sbyte) -1, (sbyte) -2));
      this.moves.Add(new Field((sbyte) 2, (sbyte) 1));
      this.moves.Add(new Field((sbyte) -2, (sbyte) 1));
      this.moves.Add(new Field((sbyte) 2, (sbyte) -1));
      this.moves.Add(new Field((sbyte) -2, (sbyte) -1));
    }

    public override void GetBeatFields()
    {
      this.BeatFields = new List<Field>();
      this.MoveFields = new List<Field>();
      this.AttackFields = new List<Field>();
      if (this.disabled) return;
      foreach (var m in this.moves)
      {
        if (!this.GameObject.IsOutOfBound((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)))
        {
          var f = this.GameObject.GetFigureByXY((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y));
          if (f != null)
          {
            if (f.color != this.color)
              this.BeatFields.Add(new Field((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)));
            else
              this.AttackFields.Add(new Field((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)));
          }
          if (f == null)
            this.MoveFields.Add(new Field((sbyte) (this.field.x + m.x), (sbyte) (this.field.y + m.y)));
        }
      }
      this.BeatFields.AddRange(this.MoveFields);
    }
  }
}