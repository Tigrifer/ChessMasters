using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Queen
/// </summary>
public class Queen : Figure
{
    public Queen(Game _g, sbyte x, sbyte y, Color c)
    {
        this.game = _g;
        this.field.x = x;
        this.field.y = y;
        this.color = c;
        this.moveCount = 0;
        this.type = FigureTypes.Queen;
    }

    public override void GetBeatFields()
    {
        this.BeatFields = new List<Field>();
        this.AttackFields = new List<Field>();
        this.MoveFields = new List<Field>();
        if (this.disabled) return;
        // like bishop
        for (int i = 1; !this.game.IsOutOfBound((sbyte)(this.field.x + i), (sbyte)(this.field.y + i)); i++) // right-up
        {
          Figure f = this.game.GetFigureByXY((sbyte)(this.field.x + i), (sbyte)(this.field.y + i));
            if (f == null)
              this.MoveFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y + i)));
            else
            {
                if (f.color != this.color)
                  this.BeatFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y + i)));
                else
                  this.AttackFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y + i)));
                break;
            }
        }
        for (int i = 1; !this.game.IsOutOfBound((sbyte)(this.field.x - i), (sbyte)(this.field.y - i)); i++) // left-down
        {
            var f = this.game.GetFigureByXY((sbyte)(this.field.x - i), (sbyte)(this.field.y - i));
            if (f == null)
                this.MoveFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y - i)));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y - i)));
                else
                    this.AttackFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y - i)));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound((sbyte)(this.field.x - i), (sbyte)(this.field.y + i)); i++) // left-up
        {
          var f = this.game.GetFigureByXY((sbyte)(this.field.x - i), (sbyte)(this.field.y + i));
            if (f == null)
              this.MoveFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y + i)));
            else
            {
                if (f.color != this.color)
                  this.BeatFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y + i)));
                else
                  this.AttackFields.Add(new Field((sbyte)(this.field.x - i), (sbyte)(this.field.y + i)));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound((sbyte)(this.field.x + i), (sbyte)(this.field.y - i)); i++) // right-down
        {
          var f = this.game.GetFigureByXY((sbyte)(this.field.x + i), (sbyte)(this.field.y - i));
            if (f == null)
              this.MoveFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y - i)));
            else
            {
                if (f.color != this.color)
                  this.BeatFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y - i)));
                else
                  this.AttackFields.Add(new Field((sbyte)(this.field.x + i), (sbyte)(this.field.y - i)));
                break;
            }
        }

        // like rook
        for (sbyte x = (sbyte)(this.field.x + 1); !this.game.IsOutOfBound(x, this.field.y); x++)
        {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (f == null)
                this.MoveFields.Add(new Field(x, this.field.y));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.Add(new Field(x, this.field.y));
                else
                    this.AttackFields.Add(new Field(x, this.field.y));
                break;
            }
        }
        for (sbyte x = (sbyte)(this.field.x - 1); !this.game.IsOutOfBound(x, this.field.y); x--)
        {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (f == null)
                this.MoveFields.Add(new Field(x, this.field.y));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.Add(new Field(x, this.field.y));
                else
                    this.AttackFields.Add(new Field(x, this.field.y));
                break;
            }
        }
        for (sbyte y = (sbyte)(this.field.y + 1); !this.game.IsOutOfBound(this.field.x, y); y++)
        {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (f == null)
                this.MoveFields.Add(new Field(this.field.x, y));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.Add(new Field(this.field.x, y));
                else
                    this.AttackFields.Add(new Field(this.field.x, y));
                break;
            }
        }
        for (sbyte y = (sbyte)(this.field.y - 1); !this.game.IsOutOfBound(this.field.x, y); y--)
        {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (f == null)
                this.MoveFields.Add(new Field(this.field.x, y));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.Add(new Field(this.field.x, y));
                else
                    this.AttackFields.Add(new Field(this.field.x, y));
                break;
            }
        }
        this.BeatFields.AddRange(this.MoveFields);
    }
}