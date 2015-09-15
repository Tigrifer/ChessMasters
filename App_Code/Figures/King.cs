using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для King
/// </summary>
public class King : Figure
{
    public List<Field> moves;
	public King(Game _g, sbyte x, sbyte y, Color c)
    {
        this.game = _g;
        this.field.x = x;
        this.field.y = y;
        this.color = c;
        this.moveCount = 0;
        this.type = FigureTypes.King;
        this.moves = new List<Field>();
        this.moves.Add(new Field((sbyte)1, (sbyte)1));
        this.moves.Add(new Field((sbyte)1, (sbyte)0));
        this.moves.Add(new Field((sbyte)1, (sbyte)-1));
        this.moves.Add(new Field((sbyte)0, (sbyte)1));
        this.moves.Add(new Field((sbyte)0, (sbyte)-1));
        this.moves.Add(new Field((sbyte)-1, (sbyte)1));
        this.moves.Add(new Field((sbyte)-1, (sbyte)0));
        this.moves.Add(new Field((sbyte)-1, (sbyte)-1));
        this.type = FigureTypes.King;
    }

    public override void GetBeatFields() {
        this.BeatFields = new List<Field>();
        this.AttackFields = new List<Field>();
        this.MoveFields = new List<Field>();
        if (this.disabled) return;
        foreach (var m in this.moves) {
          var f1 = this.game.GetFigureByXY((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y));
            if (f1!=null) {
                if (f1.color == this.color)
                  this.AttackFields.Add(new Field((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y)));
                else if (!this.game.IsFieldUnderAttack((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y), this.color))
                  this.BeatFields.Add(new Field((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y)));
            }
            if (!this.game.IsOutOfBound((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y)) &&
                f1 !=null &&
                !this.game.IsFieldUnderAttack((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y), this.color))
              this.MoveFields.Add(new Field((sbyte)(this.field.x + m.x), (sbyte)(this.field.y + m.y)));
        }
        // short castling
        Figure rook = this.game.GetFigureByXY(8, this.field.y);
        if (this.moveCount == 0 &&
        rook.moveCount == 0 &&
        this.game.GetFigureByXY(7, this.field.y)==null &&
        this.game.GetFigureByXY(6, this.field.y)==null &&
        !this.game.IsFieldUnderAttack(6, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(7, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
            this.MoveFields.Add(new Field(7, this.field.y));

        // long castling
        rook = this.game.GetFigureByXY(1, this.field.y);
        if (this.moveCount == 0 &&
        rook.moveCount == 0 &&
        this.game.GetFigureByXY(3, this.field.y) ==null &&
        this.game.GetFigureByXY(4, this.field.y)==null &&
        !this.game.IsFieldUnderAttack(3, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(4, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
            this.MoveFields.Add(new Field(3, this.field.y));
    }
}