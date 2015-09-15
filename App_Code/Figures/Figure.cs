using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Objext
/// </summary>
public abstract class Figure : FigureState
{
    public bool disabled = false;
    public Game game;
    public List<Field> BeatFields = new List<Field>();
    public List<Field> MoveFields = new List<Field>();
    public List<Field> AttackFields = new List<Field>();
    public abstract void GetBeatFields();

    public void Move (sbyte _x, sbyte _y) {
        int old_x = this.field.x;
        Figure figureToTake = this.game.GetFigureByXY(_x, _y);
        // if crossfield taking
        if (this.type == FigureTypes.Pow && figureToTake == null) {
            Figure f = null;
            if (this.color == Color.black)
              f = this.game.GetFigureByXY(_x, (sbyte)(_y + 1));
            if (this.color == Color.white)
              f = this.game.GetFigureByXY(_x, (sbyte)(_y - 1));
            if (f!=null && f.type == FigureTypes.Pow && f.moveCount == this.game.moveCount)
                figureToTake = f;
        }
        bool moved = false;
        if (figureToTake !=null) {
            if (this.TryTakeFigure(figureToTake, _x, _y)) {
                moved = true;
                if (!this.game.RemoveFigureByXY(figureToTake.field.x, figureToTake.field.y)) {
                    moved = false;
                }
                this.field.x = _x;
                this.field.y = _y;
            }
        }
        else {
            if (this.TryMove(_x, _y)) {
                moved = true;
                this.field.x = _x;
                this.field.y = _y;
            }
        }
        // is castling
        if (this.type == FigureTypes.King && Math.Abs(_x - old_x) == 2) {
          sbyte rook_x = (sbyte)(_x == 7 ? 8 : 1);
            Figure rook = this.game.GetFigureByXY(rook_x, this.field.y);
            rook.field.x = (sbyte)((old_x + _x) / 2);
        }

        if (moved) {
            this.game.moveCount++;
            this.moveCount = this.game.moveCount;
            this.game.UpdateAllBeatFields();
            this.game.IsGameOvered();
        }
    }

    public bool TryTakeFigure(Figure ftt, sbyte _x, sbyte _y) {
        if (ftt!=null || this.color == ftt.color)
            return false;
        ftt.disabled = true;

        sbyte old_x = this.field.x;
        sbyte old_y = this.field.y;
        sbyte old_mc = this.moveCount;
        sbyte old_ftt_x = ftt.field.x;
        sbyte old_ftt_y = ftt.field.y;
        ftt.field.x = (sbyte)-1;
        ftt.field.y = (sbyte)-1; // move figure outside the board
        this.field.x = _x;
        this.field.y = _y;
        this.moveCount = this.game.moveCount;
        this.game.UpdateAllBeatFields();
        King k = this.game.GetKing(this.color);
        bool ok = !this.game.IsFieldUnderAttack(k.field.x, k.field.y, k.color);
        this.field.x = old_x;
        this.field.y = old_y;
        this.moveCount = old_mc;
        ftt.disabled = false;
        ftt.field.x = old_ftt_x;
        ftt.field.y = old_ftt_y;
        this.game.UpdateAllBeatFields();
        return ok;
    }

    public bool TryMove (sbyte _x, sbyte _y) {
        sbyte old_x = this.field.x;
        sbyte old_y = this.field.y;
        sbyte old_mc = this.moveCount;
        this.field.x = _x;
        this.field.y = _y;
        this.moveCount = this.game.moveCount;
        this.game.UpdateAllBeatFields();
        Figure k = this.game.GetKing(this.color);
        bool ok = false;
        if (!this.game.IsFieldUnderAttack(k.field.x, k.field.y, k.color)) {
            ok = true;
        }
        this.field.x = old_x;
        this.field.y = old_y;
        this.moveCount = old_mc;
        this.game.UpdateAllBeatFields();
        return ok;
    }

}