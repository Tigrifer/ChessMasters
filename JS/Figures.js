var Color = {};
Color.black = 0;
Color.white = 1;

var XPosition = {};
XPosition.a = 1;
XPosition.b = 2;
XPosition.c = 3;
XPosition.d = 4;
XPosition.e = 5;
XPosition.f = 6;
XPosition.g = 7;
XPosition.h = 8;

var FigureType = {};
FigureType.Pow = 0;
FigureType.Knight = 1;
FigureType.Bishop = 2;
FigureType.Rook = 3;
FigureType.Queen = 4;
FigureType.King = 5;

function Game() {
    this.whitePlayer = "";
    this.blackPlayer = "";
    this.moveCount = 0;
    this.taken = false;
    this.GetColorTurn = function(){return this.moveCount%2 == 0 ? Color.white : Color.black;}
    this.blacks = new Array();
    this.whites = new Array();
    this.gameOvered = false;
    this.gameContainer = null;
    this.Init = function (obj) {
        this.gameContainer = obj;
        obj.css({ width: "406px", height: "406px", float: "left", position:"relative" });
        var tbl = $("<table>").css({ borderCollapse: "collapse", border: "none", margin: "15px" }).appendTo(obj);
        var c = false;
        for (var i = 8; i >= 1; i--) {
            var tr = $("<tr>").appendTo(tbl);
            for (var j = 1; j <= 8; j++) {
                var td = $("<td>").appendTo(tr);
                var div = $("<div>").appendTo(td);
                var self = this;
                div.attr("id", "f" + j + "" + i)
                    .width(45)
                    .height(45)
                    .css("align", "center")
                    .css("vertical-align", "middle")
                    .attr("x", j)
                    .attr("y", i)
                    .addClass("field")
                    .bind("mouseover", function () {
                        var figure = self.GetFigureByXY($(this).attr("x"), $(this).attr("y"));
                        if (figure)
                            figure.HighLightBeatFields();
                        else
                            if (!self.taken) $(".field").css("background", "transparent").css("opacity", "1");

                    });
                if (!c)
                    td.css("background", "gray");
                c = !c;
            }
            c = !c;
        }
    };
    this.New = function () {
        this.moveCount = 0;
        this.blacks = new Array();
        this.whites = new Array();
        this.taken = false;
        var p1 = new Pow(this, XPosition.a, 7, Color.black);
        this.blacks.push(p1);

        var p2 = new Pow(this, XPosition.b, 7, Color.black);
        this.blacks.push(p2);

        var p3 = new Pow(this, XPosition.c, 7, Color.black);
        this.blacks.push(p3);

        var p4 = new Pow(this, XPosition.d, 7, Color.black);
        this.blacks.push(p4);

        var p5 = new Pow(this, XPosition.e, 7, Color.black);
        this.blacks.push(p5);

        var p6 = new Pow(this, XPosition.f, 7, Color.black);
        this.blacks.push(p6);

        var p7 = new Pow(this, XPosition.g, 7, Color.black);
        this.blacks.push(p7);

        var p8 = new Pow(this, XPosition.h, 7, Color.black);
        this.blacks.push(p8);

        var wp1 = new Pow(this, XPosition.a, 2, Color.white);
        this.whites.push(wp1);

        var wp2 = new Pow(this, XPosition.b, 2, Color.white);
        this.whites.push(wp2);

        var wp3 = new Pow(this, XPosition.c, 2, Color.white);
        this.whites.push(wp3);

        var wp4 = new Pow(this, XPosition.d, 2, Color.white);
        this.whites.push(wp4);

        var wp5 = new Pow(this, XPosition.e, 2, Color.white);
        this.whites.push(wp5);

        var wp6 = new Pow(this, XPosition.f, 2, Color.white);
        this.whites.push(wp6);

        var wp7 = new Pow(this, XPosition.g, 2, Color.white);
        this.whites.push(wp7);

        var wp8 = new Pow(this, XPosition.h, 2, Color.white);
        this.whites.push(wp8);

        var b1 = new Bishop(this, XPosition.c, 8, Color.black);
        this.blacks.push(b1);

        var b2 = new Bishop(this, XPosition.f, 8, Color.black);
        this.blacks.push(b2);

        var wb1 = new Bishop(this, XPosition.c, 1, Color.white);
        this.whites.push(wb1);

        var wb2 = new Bishop(this, XPosition.f, 1, Color.white);
        this.whites.push(wb2);

        var r1 = new Rook(this, XPosition.a, 8, Color.black);
        this.blacks.push(r1);

        var r2 = new Rook(this, XPosition.h, 8, Color.black);
        this.blacks.push(r2);

        var wr1 = new Rook(this, XPosition.a, 1, Color.white);
        this.whites.push(wr1);

        var wr2 = new Rook(this, XPosition.h, 1, Color.white);
        this.whites.push(wr2);

        var kn1 = new Knight(this, XPosition.b, 8, Color.black);
        this.blacks.push(kn1);

        var kn2 = new Knight(this, XPosition.g, 8, Color.black);
        this.blacks.push(kn2);

        var wkn1 = new Knight(this, XPosition.b, 1, Color.white);
        this.whites.push(wkn1);

        var wkn2 = new Knight(this, XPosition.g, 1, Color.white);
        this.whites.push(wkn2);

        var q = new Queen(this, XPosition.d, 8, Color.black);
        this.blacks.push(q);

        var wq = new Queen(this, XPosition.d, 1, Color.white);
        this.whites.push(wq);

        var k = new King(this, XPosition.e, 8, Color.black);
        this.blacks.push(k);

        var wk = new King(this, XPosition.e, 1, Color.white);
        this.whites.push(wk);

        this.UpdateAllBeatFields();
        this.DrawFigures();
    }
    
    this.IsOutOfBound = function(_x, _y){
        return !(_x>=1 && _x<=8 && _y>=1 && _y<=8);
    }

    this.GetKing = function (col) {
        if (col == Color.white) {
            for (var i in this.whites)
                if (this.whites[i].type == FigureType.King)
                    return this.whites[i];
        }
        else {
            for (var i in this.blacks)
                if (this.blacks[i].type == FigureType.King)
                    return this.blacks[i];
        }
    }

    this.RemoveFigureByXY = function (_x, _y) {
        for (var f in this.whites)
            if (this.whites[f].field.x == _x && this.whites[f].field.y == _y) {
                if (this.whites[f].container)
                    this.whites[f].container.remove();
                this.whites.splice(f, 1);
                return true;
            }
        for (var b in this.blacks)
            if (this.blacks[b].field.x == _x && this.blacks[b].field.y == _y) {
                if (this.blacks[b].container)
                    this.blacks[b].container.remove();
                this.blacks.splice(b, 1);
                return true;
            }
        return false;
    }
    
    this.GetFigureByXY = function(_x, _y){
        if (this.IsOutOfBound(_x, _y))
            return false;
        for (var f in this.whites)
            if (this.whites[f].field.x == _x && this.whites[f].field.y == _y) return this.whites[f];
        for (var b in this.blacks)
            if (this.blacks[b].field.x == _x && this.blacks[b].field.y == _y) return this.blacks[b];
        return false;
    };

    this.GetDomElement = function (x, y) {
        return this.gameContainer.find("#f" + x + "" + y);
    };

    this.IsFieldUnderAttack = function (_x, _y, _c) {
        if (_c == Color.black) {
            for (var i in this.whites) {
                for (var j in this.whites[i].BeatFields) {
                    var field = this.whites[i].BeatFields[j];
                    if (field.x == _x && field.y == _y)
                        return true;
                }
                for (var j in this.whites[i].AttackFields) {
                    var field = this.whites[i].AttackFields[j];
                    if (field.x == _x && field.y == _y)
                        return true;
                }
            }
        }
        if (_c == Color.white) {
            for (var i in this.blacks) {
                for (var j in this.blacks[i].BeatFields) {
                    var field = this.blacks[i].BeatFields[j];
                    if (field.x == _x && field.y == _y)
                        return true;
                }
                for (var j in this.blacks[i].AttackFields) {
                    var field = this.blacks[i].AttackFields[j];
                    if (field.x == _x && field.y == _y)
                        return true;
                }
            }
        }
    }

    this.DrawFigures = function () {
        for (var w in this.whites)
            this.GetDomElement(this.whites[w].field.x, this.whites[w].field.y).append(this.whites[w].container);
        for (var b in this.blacks)
            this.GetDomElement(this.blacks[b].field.x, this.blacks[b].field.y).append(this.blacks[b].container);
    };

    this.UpdateAllBeatFields = function () {
        for (var w in this.whites)
            this.whites[w].GetBeatFields();
        for (var b in this.blacks)
            this.blacks[b].GetBeatFields();
        var wk = this.GetKing(Color.white);
        var bk = this.GetKing(Color.black);
        wk.GetBeatFields();
        bk.GetBeatFields();
    }

    this.IsGameOvered = function () {
        if (this.IsMat()) {
            alert("Мат. Победили " + (this.GetColorTurn() == Color.white ? "черные." : "белые." + " Поздравляем."));
            this.gameOvered = true;
        }
        if (this.IsPat()) {
            alert("Пат. Ничья.");
            this.gameOvered = true;
        }
    }

    this.IsPat = function () {
        var color = this.GetColorTurn();
        var king = this.GetKing(color);
        if (!this.IsFieldUnderAttack(king.field.x, king.field.y, color)) {
            var figures = false;
            if (color == Color.black)
                figures = this.blacks;
            else
                figures = this.whites;
            for (var i = 0; i < figures.length; i++) {
                var figure = figures[i];

                for (var j = 0; j < figure.MoveFields.length; j++) {
                    var field = figure.MoveFields[j];
                    if (figure.TryMove(field.x, field.y))
                        return false;
                }

                for (var j = 0; j < figure.BeatFields.length; j++) {
                    var field = figure.BeatFields[j];
                    var ftt = this.GetFigureByXY(field.x, field.y);
                    if (figure.type == FigureType.Pow && ftt == false) {
                        var f = false;
                        if (color == Color.black)
                            f = this.GetFigureByXY(field.x, field.y + 1);
                        if (color == Color.white)
                            f = this.GetFigureByXY(field.x, field.y - 1);
                        if (f && f.type == FigureType.Pow && f.jumped && f.moveCount == this.moveCount)
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

    this.IsMat = function () {
        var color = this.GetColorTurn() == Color.black ? Color.black : Color.white;
        var king = this.GetKing(color);
        if (this.IsFieldUnderAttack(king.field.x, king.field.y, color)) {
            var figures = false;
            if (color == Color.black)
                figures = this.blacks;
            else
                figures = this.whites;
            for (var i = 0; i < figures.length; i++) {
                var figure = figures[i];
                for (var j = 0; j < figure.MoveFields.length; j++) {
                    var field = figure.MoveFields[j];
                    if (figure.TryMove(field.x, field.y))
                        return false;
                }

                for (var j = 0; j < figure.BeatFields.length; j++) {
                    var field = figure.BeatFields[j];
                    var ftt = this.GetFigureByXY(field.x, field.y);
                    if (figure.type == FigureType.Pow && ftt == false) {
                        var f = false;
                        if (color == Color.black)
                            f = this.GetFigureByXY(field.x, field.y + 1);
                        if (color == Color.white)
                            f = this.GetFigureByXY(field.x, field.y - 1);
                        if (f && f.type == FigureType.Pow && f.jumped && f.moveCount == this.moveCount)
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
}

function Field(_x, _y){
    this.x = _x;
    this.y = _y;
};

function Figure(x, y, t, c) {
    this.disabled = false;
    this.field = new Field(0, 0);
    this.type = 0;
    this.color = 0;
    this.Move = function(x,y){};
    this.BeatFields = new Array();
    this.MoveFields = new Array();
    this.AttackFields = new Array();
    this.container = {};
    this.field.x = x;
    this.field.y = y;
    this.type = t;
    this.color = c;
    this.container = $("<div>");
    this.HighLightBeatFields = function () {
        if (this.game.gameOvered) return;
        if (this.game.taken)
            return;
        $(".field").css("background", "transparent").css("opacity", "1").droppable({ disabled: true });
        if (this.game.GetColorTurn() == this.color) {
            var _self = this;
            this.container.draggable({ revert: "invalid", disabled: false, start: function () { _self.game.taken = true; }, stop: function () { _self.game.taken = false; } });
            for (var i in this.BeatFields) {
                var id = "f" + this.BeatFields[i].x + "" + this.BeatFields[i].y;
                _self.game.gameContainer.find("#" + id).css("opacity", "0.5").css("background", "red").droppable({ disabled: false,
                    drop: this.DropHandler(this)
                });
            }
            for (var i in this.MoveFields) {
                var id = "f" + this.MoveFields[i].x + "" + this.MoveFields[i].y;
                _self.game.gameContainer.find("#" + id).css("opacity", "0.5").css("background", "green").droppable({ disabled: false,
                    drop: _self.DropHandler(this)
                });
            }
        }
        else
            this.container.draggable({ revert: "invalid", disabled: true });
    };
    this.DropHandler = function(obj){
        return function(event, ui){
            obj.Move($(this).attr("x"), $(this).attr("y"));
        };
    }

    this.Move = function (_x, _y) {
        var start = new Date();

        _x = parseInt(_x);
        _y = parseInt(_y);
        var old_x = this.field.x;
        var figureToTake = this.game.GetFigureByXY(_x, _y);
        // if crossfield taking
        if (this.type == FigureType.Pow && figureToTake == false) {
            var f = false;
            if (this.color == Color.black) {
                f = this.game.GetFigureByXY(_x, _y + 1);

            }
            if (this.color == Color.white) {
                f = this.game.GetFigureByXY(_x, _y - 1);
            }
            if (f && f.type == FigureType.Pow && f.jumped && f.moveCount == this.game.moveCount)
                figureToTake = f;
        }
        var moved = false;
        if (figureToTake) {
            if (this.TryTakeFigure(figureToTake, _x, _y)) {
                moved = true;
                if (!this.game.RemoveFigureByXY(figureToTake.field.x, figureToTake.field.y)) {
                    console.log('Error: can\'t remove figure.');
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
        if (this.type == FigureType.King && Math.abs(_x - old_x) == 2) {
            var rook_x = _x == 7 ? 8 : 1;
            var rook = this.game.GetFigureByXY(rook_x, this.field.y);
            rook.field.x = (old_x + _x) / 2;
        }
        // is pow transform
        if (this.type == FigureType.Pow) {
            var target_y = this.color == Color.black ? 1 : 8;
            if (this.field.y == target_y)
                this.Transform();
        }
        if (moved) {
            this.container.css("top", "0").css("left", "0");
            this.game.moveCount++;
            this.moveCount = this.game.moveCount;
            this.jumped = true; // pow case
            this.game.UpdateAllBeatFields();
            this.game.IsGameOvered();
        }
        else
            this.container.draggable({ revert: true });
        this.game.DrawFigures();

        var end = new Date();
        var diff = end - start;
        console.log("Moved in " + diff + " miliseconds");
    }

    this.TryTakeFigure = function (ftt, _x, _y) {
        if (!ftt || this.color == ftt.color)
            return false;
        ftt.disabled = true;

        var old_x = this.field.x;
        var old_y = this.field.y;
        var old_mc = this.moveCount;
        var old_jmp = this.jumped;
        var old_ftt_x = ftt.field.x;
        var old_ftt_y = ftt.field.y;
        ftt.field.x = -1;
        ftt.field.y = -1; // move figure outside the board
        this.field.x = _x;
        this.field.y = _y;
        this.moveCount = this.game.moveCount;
        this.jumped = true;
        this.game.UpdateAllBeatFields();
        var k = this.game.GetKing(this.color);
        var ok = false;
        if (!this.game.IsFieldUnderAttack(k.field.x, k.field.y, k.color)) {
            ok = true;
        }
        this.field.x = old_x;
        this.field.y = old_y;
        this.moveCount = old_mc;
        this.jumped = old_jmp;
        ftt.disabled = false;
        ftt.field.x = old_ftt_x;
        ftt.field.y = old_ftt_y;
        this.game.UpdateAllBeatFields();
        return ok;
    }

    this.TryMove = function (_x, _y) {
        var old_x = this.field.x;
        var old_y = this.field.y;
        var old_mc = this.moveCount;
        var old_jmp = this.jumped;
        this.field.x = _x;
        this.field.y = _y;
        this.moveCount = this.game.moveCount;
        this.jumped = true;
        this.game.UpdateAllBeatFields();
        var k = this.game.GetKing(this.color);
        var ok = false;
        if (!this.game.IsFieldUnderAttack(k.field.x, k.field.y, k.color)) {
            ok = true;
        }
        this.field.x = old_x;
        this.field.y = old_y;
        this.moveCount = old_mc;
        this.jumped = old_jmp;
        this.game.UpdateAllBeatFields();
        return ok;
    }

}

function Pow(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.Pow, c));
    this.game = _g;
    $(this.container).addClass('figure '+(this.color==Color.black?'b':'')+'pow').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.moveCount = 0;
    this.jumped = false;
    this.GetBeatFields = function ()
    {
        this.BeatFields = new Array();
        this.MoveFields = new Array();
        this.AttackFields = new Array();
        if (this.disabled) return;
        if (this.color == Color.white) {
            if (!this.game.GetFigureByXY(this.field.x, this.field.y + 1) && !this.game.IsOutOfBound(this.field.x, this.field.y + 1)) // move forward
            {
                this.MoveFields.push(new Field(this.field.x, this.field.y + 1));
                if (this.moveCount == 0 && !this.game.GetFigureByXY(this.field.x, this.field.y + 2)) // first long move
                    this.MoveFields.push(new Field(this.field.x, this.field.y + 2));
            }
            var f = this.game.GetFigureByXY(this.field.x + 1, this.field.y + 1);
            if (f && f.color != this.color) // take enemy right
                this.BeatFields.push(new Field(this.field.x + 1, this.field.y + 1));
            var f1 = this.game.GetFigureByXY(this.field.x - 1, this.field.y + 1);
            if (f1 && f1.color != this.color) // take enemy left
                this.BeatFields.push(new Field(this.field.x - 1, this.field.y + 1));
            // take enemy pow on crossfield
            var fcl = this.game.GetFigureByXY(this.field.x - 1, this.field.y); // left
            if (fcl && fcl.type == FigureType.Pow && fcl.color != this.color && fcl.moveCount == this.game.moveCount && fcl.jumped) {
                this.BeatFields.push(new Field(this.field.x - 1, this.field.y + 1));
            }
            var fcr = this.game.GetFigureByXY(this.field.x + 1, this.field.y); // right
            if (fcr && fcr.type == FigureType.Pow && fcr.color != this.color && fcr.moveCount == this.game.moveCount && fcr.jumped) {
                this.BeatFields.push(new Field(this.field.x + 1, this.field.y + 1));
            }

            if (!this.game.IsOutOfBound(this.field.x + 1, this.field.y + 1))
                this.AttackFields.push(new Field(this.field.x + 1, this.field.y + 1));
            if (!this.game.IsOutOfBound(this.field.x - 1, this.field.y + 1))
                this.AttackFields.push(new Field(this.field.x - 1, this.field.y + 1));
        }
        if (this.color == Color.black) {
            if (!this.game.GetFigureByXY(this.field.x, this.field.y - 1) && !this.game.IsOutOfBound(this.field.x, this.field.y - 1)) // move forward
            {
                this.MoveFields.push(new Field(this.field.x, this.field.y - 1));
                if (this.moveCount == 0 && !this.game.GetFigureByXY(this.field.x, this.field.y - 2)) // first long move
                    this.MoveFields.push(new Field(this.field.x, this.field.y - 2));
            }
            var f = this.game.GetFigureByXY(this.field.x + 1, this.field.y - 1);
            if (f && f.color != this.color) // take enemy right
                this.BeatFields.push(new Field(this.field.x + 1, this.field.y - 1));
            var f1 = this.game.GetFigureByXY(this.field.x - 1, this.field.y - 1);
            if (f1 && f1.color != this.color) // take enemy left
                this.BeatFields.push(new Field(this.field.x - 1, this.field.y - 1));
            // take enemy pow on crossfield
            var fcl = this.game.GetFigureByXY(this.field.x - 1, this.field.y); // left
            if (fcl && fcl.type == FigureType.Pow && fcl.color != this.color && fcl.moveCount == this.game.moveCount && fcl.jumped) {
                this.BeatFields.push(new Field(this.field.x - 1, this.field.y - 1));
            }
            var fcr = this.game.GetFigureByXY(this.field.x + 1, this.field.y); // right
            if (fcr && fcr.type == FigureType.Pow && fcr.color != this.color && fcr.moveCount == this.game.moveCount && fcr.jumped) {
                this.BeatFields.push(new Field(this.field.x + 1, this.field.y - 1));
            }

            if (!this.game.IsOutOfBound(this.field.x + 1, this.field.y - 1))
                this.AttackFields.push(new Field(this.field.x + 1, this.field.y - 1));
            if (!this.game.IsOutOfBound(this.field.x - 1, this.field.y - 1))
                this.AttackFields.push(new Field(this.field.x - 1, this.field.y - 1));
        }
    };

    this.Transform = function () {
        var div = $("<div>").appendTo(this.game.gameContainer).addClass("PawnTransformPanel").attr("id", "PawnTransformPanel");
        var overlay = $("<div>").appendTo(this.game.gameContainer).addClass("BoardOverlay").attr("id", "BoardOverlay");
        var self = this;
        var knight = $("<div>").addClass('figure left pointer ' + (this.color == Color.black ? 'b' : '') + 'knight').appendTo(div).bind('click', function () { self.CompleteTransform(FigureType.Knight); });
        var rook = $("<div>").addClass('figure left pointer ' + (this.color == Color.black ? 'b' : '') + 'rook').appendTo(div).bind('click', function () { self.CompleteTransform(FigureType.Rook); });
        var queen = $("<div>").addClass('figure left pointer ' + (this.color == Color.black ? 'b' : '') + 'queen').appendTo(div).bind('click', function () { self.CompleteTransform(FigureType.Queen); });
        var bishop = $("<div>").addClass('figure left pointer ' + (this.color == Color.black ? 'b' : '') + 'bishop').appendTo(div).bind('click', function () { self.CompleteTransform(FigureType.Bishop); });
    };

    this.CompleteTransform = function (type) {
        var newFigure;
        switch (type) {
            case FigureType.Knight:
                var newFigure = new Knight(this.game, this.field.x, this.field.y, this.color);
                break;
            case FigureType.Bishop:
                var newFigure = new Bishop(this.game, this.field.x, this.field.y, this.color);
                break;
            case FigureType.Queen:
                var newFigure = new Queen(this.game, this.field.x, this.field.y, this.color);
                break;
            case FigureType.Rook:
                var newFigure = new Rook(this.game, this.field.x, this.field.y, this.color);
                break;
        }
        this.container.remove();
        this.container = newFigure.container;
        this.type = newFigure.type;
        this.GetBeatFields = newFigure.GetBeatFields;
        this.game.UpdateAllBeatFields();
        this.game.IsGameOvered();
        this.game.gameContainer.find("#PawnTransformPanel").remove();
        this.game.gameContainer.find("#BoardOverlay").remove();
    }
}

function Rook(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.Rook, c));
    this.game = _g;
    $(this.container).addClass('figure ' + (this.color == Color.black ? 'b' : '') + 'rook').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.moveCount = 0;
    this.GetBeatFields = function () {
        this.BeatFields = new Array();
        this.MoveFields = new Array();
        this.AttackFields = new Array();
        if (this.disabled) return;
        for (var x = this.field.x + 1; !this.game.IsOutOfBound(x, this.field.y); x++) {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (!f)
                this.MoveFields.push(new Field(x, this.field.y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(x, this.field.y));
                else
                    this.AttackFields.push(new Field(x, this.field.y))
                break;
            }
        }
        for (var x = this.field.x - 1; !this.game.IsOutOfBound(x, this.field.y); x--) {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (!f)
                this.MoveFields.push(new Field(x, this.field.y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(x, this.field.y));
                else
                    this.AttackFields.push(new Field(x, this.field.y));
                break;
            }
        }
        for (var y = this.field.y + 1; !this.game.IsOutOfBound(this.field.x, y); y++) {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (!f)
                this.MoveFields.push(new Field(this.field.x, y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x, y));
                else
                    this.AttackFields.push(new Field(this.field.x, y));
                break;
            }
        }
        for (var y = this.field.y - 1; !this.game.IsOutOfBound(this.field.x, y); y--) {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (!f)
                this.MoveFields.push(new Field(this.field.x, y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x, y));
                else
                    this.AttackFields.push(new Field(this.field.x, y));
                break;
            }
        }
        this.BeatFields = this.BeatFields.concat(this.MoveFields);
    };
}

function King(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.King, c));
    this.game = _g;
    $(this.container).addClass('figure ' + (this.color == Color.black ? 'b' : '') + 'king').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.moveCount = 0;
    this.moves = [new Field(1, 1), new Field(1, 0), new Field(1, -1), new Field(0, 1),
                  new Field(0, -1), new Field(-1, 1), new Field(-1, 0), new Field(-1, -1)];
    this.GetBeatFields = function () {
        this.BeatFields = new Array();
        this.AttackFields = new Array();
        this.MoveFields = new Array();
        if (this.disabled) return;
        for (var m in this.moves) {
            var f1 = this.game.GetFigureByXY(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y);
            if (f1) {
                if (f1.color == this.color)
                    this.AttackFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
                else if (!this.game.IsFieldUnderAttack(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y, this.color))
                    this.BeatFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
            }
            if (!this.game.IsOutOfBound(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y) &&
                !f1 &&
                !this.game.IsFieldUnderAttack(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y, this.color))
                this.MoveFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
        }
        // short castling
        var rook = this.game.GetFigureByXY(8, this.field.y);
        if (this.moveCount == 0 &&
        rook.moveCount == 0 &&
        !this.game.GetFigureByXY(7, this.field.y) &&
        !this.game.GetFigureByXY(6, this.field.y) &&
        !this.game.IsFieldUnderAttack(6, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(7, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
            this.MoveFields.push(new Field(7, this.field.y));

        // long castling
        var rook = this.game.GetFigureByXY(1, this.field.y);
        if (this.moveCount == 0 &&
        rook.moveCount == 0 &&
        !this.game.GetFigureByXY(3, this.field.y) &&
        !this.game.GetFigureByXY(4, this.field.y) &&
        !this.game.IsFieldUnderAttack(3, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(4, this.field.y, this.color) &&
        !this.game.IsFieldUnderAttack(this.field.x, this.field.y, this.color))
            this.MoveFields.push(new Field(3, this.field.y));
    };
}

function Knight(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.Knight, c));
    this.game = _g;
    $(this.container).addClass('figure ' + (this.color == Color.black ? 'b' : '') + 'knight').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.moves = [new Field(1, 2), new Field(-1, 2), new Field(1, -2), new Field(-1, -2),
                  new Field(2, 1), new Field(-2, 1), new Field(2, -1), new Field(-2, -1)];
    this.GetBeatFields = function () {
        this.BeatFields = new Array();
        this.MoveFields = new Array();
        this.AttackFields = new Array();
        if (this.disabled) return;
        for (var m in this.moves) {
            if (!this.game.IsOutOfBound(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y)) {
                var f = this.game.GetFigureByXY(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y);
                if (f) {
                    if (f.color != this.color)
                        this.BeatFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
                    else
                        this.AttackFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
                }
                if (!f)
                    this.MoveFields.push(new Field(this.field.x + this.moves[m].x, this.field.y + this.moves[m].y));
            }
        }
        this.BeatFields = this.BeatFields.concat(this.MoveFields);
    };
}

function Bishop(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.Bishop, c));
    this.game = _g;
    $(this.container).addClass('figure ' + (this.color == Color.black ? 'b' : '') + 'bishop').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.GetBeatFields = function(){
        this.BeatFields = new Array();
        this.AttackFields = new Array();
        this.MoveFields = new Array();
        if (this.disabled) return;
        for (var i = 1; !this.game.IsOutOfBound(this.field.x + i, this.field.y + i); i++) // right-up
        {
            var f = this.game.GetFigureByXY(this.field.x + i, this.field.y + i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x+i, this.field.y+i));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x+i, this.field.y+i));
                else
                    this.AttackFields.push(new Field(this.field.x+i, this.field.y+i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x - i, this.field.y - i); i++) // left-down
        {
            var f = this.game.GetFigureByXY(this.field.x - i, this.field.y - i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x - i, this.field.y - i));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x-i, this.field.y-i));
                else
                    this.AttackFields.push(new Field(this.field.x-i, this.field.y-i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x - i, this.field.y + i); i++) // left-up
        {
            var f = this.game.GetFigureByXY(this.field.x - i, this.field.y + i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x - i, this.field.y + i));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x-i, this.field.y+i));
                else
                    this.AttackFields.push(new Field(this.field.x-i, this.field.y+i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x + i, this.field.y - i); i++) // right-down
        {
            var f = this.game.GetFigureByXY(this.field.x + i, this.field.y - i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x + i, this.field.y - i));
            else
            {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x+i, this.field.y-i));
                else
                    this.AttackFields.push(new Field(this.field.x+i, this.field.y-i));
                break;
            }
        }
        this.BeatFields = this.BeatFields.concat(this.MoveFields);
    };
}

function Queen(_g, x, y, c) {
    $.extend(this, new Figure(x, y, FigureType.Queen, c));
    this.game = _g;
    $(this.container).addClass('figure ' + (this.color == Color.black ? 'b' : '') + 'queen').appendTo(this.game.GetDomElement(this.field.x, this.field.y));
    this.GetBeatFields = function () {
        this.BeatFields = new Array();
        this.AttackFields = new Array();
        this.MoveFields = new Array();
        if (this.disabled) return;
        // like bishop
        for (var i = 1; !this.game.IsOutOfBound(this.field.x + i, this.field.y + i); i++) // right-up
        {
            var f = this.game.GetFigureByXY(this.field.x + i, this.field.y + i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x + i, this.field.y + i));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x + i, this.field.y + i));
                else
                    this.AttackFields.push(new Field(this.field.x + i, this.field.y + i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x - i, this.field.y - i); i++) // left-down
        {
            var f = this.game.GetFigureByXY(this.field.x - i, this.field.y - i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x - i, this.field.y - i));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x - i, this.field.y - i));
                else
                    this.AttackFields.push(new Field(this.field.x - i, this.field.y - i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x - i, this.field.y + i); i++) // left-up
        {
            var f = this.game.GetFigureByXY(this.field.x - i, this.field.y + i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x - i, this.field.y + i));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x - i, this.field.y + i));
                else
                    this.AttackFields.push(new Field(this.field.x - i, this.field.y + i));
                break;
            }
        }
        for (var i = 1; !this.game.IsOutOfBound(this.field.x + i, this.field.y - i); i++) // right-down
        {
            var f = this.game.GetFigureByXY(this.field.x + i, this.field.y - i);
            if (!f)
                this.MoveFields.push(new Field(this.field.x + i, this.field.y - i));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x + i, this.field.y - i));
                else
                    this.AttackFields.push(new Field(this.field.x + i, this.field.y - i));
                break;
            }
        }

        // like rook
        for (var x = this.field.x + 1; !this.game.IsOutOfBound(x, this.field.y); x++) {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (!f)
                this.MoveFields.push(new Field(x, this.field.y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(x, this.field.y));
                else
                    this.AttackFields.push(new Field(x, this.field.y))
                break;
            }
        }
        for (var x = this.field.x - 1; !this.game.IsOutOfBound(x, this.field.y); x--) {
            var f = this.game.GetFigureByXY(x, this.field.y);
            if (!f)
                this.MoveFields.push(new Field(x, this.field.y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(x, this.field.y));
                else
                    this.AttackFields.push(new Field(x, this.field.y));
                break;
            }
        }
        for (var y = this.field.y + 1; !this.game.IsOutOfBound(this.field.x, y); y++) {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (!f)
                this.MoveFields.push(new Field(this.field.x, y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x, y));
                else
                    this.AttackFields.push(new Field(this.field.x, y));
                break;
            }
        }
        for (var y = this.field.y - 1; !this.game.IsOutOfBound(this.field.x, y); y--) {
            var f = this.game.GetFigureByXY(this.field.x, y);
            if (!f)
                this.MoveFields.push(new Field(this.field.x, y));
            else {
                if (f.color != this.color)
                    this.BeatFields.push(new Field(this.field.x, y));
                else
                    this.AttackFields.push(new Field(this.field.x, y));
                break;
            }
        }
        this.BeatFields = this.BeatFields.concat(this.MoveFields);
    };
}