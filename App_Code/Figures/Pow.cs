using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Pawn
/// </summary>
public class Pow:Figure
{
	public Pow(Game _g, sbyte x, sbyte y, Color c) {
        this.game = _g;
        this.field.x = x;
        this.field.y = y;
        this.color = c;
        this.moveCount = 0;
        this.type = FigureTypes.Pow;
    }
    public override void GetBeatFields()
    {
        this.BeatFields = new List<Field>();
        this.MoveFields = new List<Field>();
        this.AttackFields = new List<Field>();
        if (this.disabled) return;
        if (this.color == Color.white) {
          if (this.game.GetFigureByXY(this.field.x, (sbyte)(this.field.y + 1)) == null && !this.game.IsOutOfBound(this.field.x, (sbyte)(this.field.y + 1))) // move forward
            {
              this.MoveFields.Add(new Field(this.field.x, (sbyte)(this.field.y + 1)));
              if (this.moveCount == 0 && this.game.GetFigureByXY(this.field.x, (sbyte)(this.field.y + 2)) == null) // first long move
                this.MoveFields.Add(new Field(this.field.x, (sbyte)(this.field.y + 2)));
            }
          Figure f = this.game.GetFigureByXY((sbyte)(this.field.x + 1), (sbyte)(this.field.y + 1));
            if (f!= null && f.color != this.color) // take enemy right
              this.BeatFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y + 1)));
            Figure f1 = this.game.GetFigureByXY((sbyte)(this.field.x - 1), (sbyte)(this.field.y + 1));
            if (f1!=null && f1.color != this.color) // take enemy left
              this.BeatFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y + 1)));
            // take enemy pow on crossfield
            Figure fcl = this.game.GetFigureByXY((sbyte)(this.field.x - 1), this.field.y); // left
            if (fcl!=null && fcl.type == FigureTypes.Pow && fcl.color != this.color && fcl.moveCount == this.game.moveCount) {
              this.BeatFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y + 1)));
            }
            var fcr = this.game.GetFigureByXY((sbyte)(this.field.x + 1), this.field.y); // right
            if (fcr!=null && fcr.type == FigureTypes.Pow && fcr.color != this.color && fcr.moveCount == this.game.moveCount) {
              this.BeatFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y + 1)));
            }

            if (!this.game.IsOutOfBound((sbyte)(this.field.x + 1), (sbyte)(this.field.y + 1)))
              this.AttackFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y + 1)));
            if (!this.game.IsOutOfBound((sbyte)(this.field.x - 1), (sbyte)(this.field.y + 1)))
              this.AttackFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y + 1)));
        }
        if (this.color == Color.black) {
          if (this.game.GetFigureByXY(this.field.x, (sbyte)(this.field.y - 1)) == null && !this.game.IsOutOfBound(this.field.x, (sbyte)(this.field.y - 1))) // move forward
            {
              this.MoveFields.Add(new Field(this.field.x, (sbyte)(this.field.y - 1)));
              if (this.moveCount == 0 && this.game.GetFigureByXY(this.field.x, (sbyte)(this.field.y - 2)) == null) // first long move
                this.MoveFields.Add(new Field(this.field.x, (sbyte)(this.field.y - 2)));
            }
          Figure f = this.game.GetFigureByXY((sbyte)(this.field.x + 1), (sbyte)(this.field.y - 1));
            if (f!=null && f.color != this.color) // take enemy right
              this.BeatFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y - 1)));
            Figure f1 = this.game.GetFigureByXY((sbyte)(this.field.x - 1), (sbyte)(this.field.y - 1));
            if (f1!=null && f1.color != this.color) // take enemy left
              this.BeatFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y - 1)));
            // take enemy pow on crossfield
            Figure fcl = this.game.GetFigureByXY((sbyte)(this.field.x - 1), this.field.y); // left
            if (fcl != null && fcl.type == FigureTypes.Pow && fcl.color != this.color && fcl.moveCount == this.game.moveCount)
              this.BeatFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y - 1)));
            Figure fcr = this.game.GetFigureByXY((sbyte)(this.field.x + 1), this.field.y); // right
            if (fcr !=null && fcr.type == FigureTypes.Pow && fcr.color != this.color && fcr.moveCount == this.game.moveCount) {
              this.BeatFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y - 1)));
            }

            if (!this.game.IsOutOfBound((sbyte)(this.field.x + 1), (sbyte)(this.field.y - 1)))
              this.AttackFields.Add(new Field((sbyte)(this.field.x + 1), (sbyte)(this.field.y - 1)));
            if (!this.game.IsOutOfBound((sbyte)(this.field.x - 1), (sbyte)(this.field.y - 1)))
              this.AttackFields.Add(new Field((sbyte)(this.field.x - 1), (sbyte)(this.field.y - 1)));
        }
    }
}