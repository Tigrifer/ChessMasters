using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessCore
{
  /// <summary>
  /// Сводное описание для Bishop
  /// </summary>
  public class Bishop : Figure
  {
    public Bishop(GameObject _g, sbyte x, sbyte y, Color c)
    {
      this.GameObject = _g;
      this.field = new Field(x, y);
      this.color = c;
    }

    public override void GetBeatFields()
    {
      this.BeatFields = new List<Field>();
      this.AttackFields = new List<Field>();
      this.MoveFields = new List<Field>();
      if (this.disabled) return;
      for (sbyte i = 1; !this.GameObject.IsOutOfBound((sbyte) (this.field.x + i), (sbyte) (this.field.y + i)); i++)
        // right-up
      {
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x + i), (sbyte) (this.field.y + i));
        if (f == null)
          this.MoveFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y + i)));
        else
        {
          if (f.color != this.color)
            this.BeatFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y + i)));
          else
            this.AttackFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y + i)));
          break;
        }
      }
      for (sbyte i = 1; !this.GameObject.IsOutOfBound((sbyte) (this.field.x - i), (sbyte) (this.field.y - i)); i++)
        // left-down
      {
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x - i), (sbyte) (this.field.y - i));
        if (f == null)
          this.MoveFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y - i)));
        else
        {
          if (f.color != this.color)
            this.BeatFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y - i)));
          else
            this.AttackFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y - i)));
          break;
        }
      }
      for (sbyte i = 1; !this.GameObject.IsOutOfBound((sbyte) (this.field.x - i), (sbyte) (this.field.y + i)); i++) // left-up
      {
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x - i), (sbyte) (this.field.y + i));
        if (f == null)
          this.MoveFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y + i)));
        else
        {
          if (f.color != this.color)
            this.BeatFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y + i)));
          else
            this.AttackFields.Add(new Field((sbyte) (this.field.x - i), (sbyte) (this.field.y + i)));
          break;
        }
      }
      for (sbyte i = 1; !this.GameObject.IsOutOfBound((sbyte) (this.field.x + i), (sbyte) (this.field.y - i)); i++)
        // right-down
      {
        Figure f = this.GameObject.GetFigureByXY((sbyte) (this.field.x + i), (sbyte) (this.field.y - i));
        if (f == null)
          this.MoveFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y - i)));
        else
        {
          if (f.color != this.color)
            this.BeatFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y - i)));
          else
            this.AttackFields.Add(new Field((sbyte) (this.field.x + i), (sbyte) (this.field.y - i)));
          break;
        }
      }
      this.BeatFields.AddRange(this.MoveFields);
    }
  }
}