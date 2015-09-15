using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestPawn
  {
    [TestMethod]
    public void TestMove()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 5, Color.white);
      Pawn wPawn1 = new Pawn(GameObject, 3, 2, Color.white);
      Pawn wPawn2 = new Pawn(GameObject, 4, 2, Color.white);
      Pawn wPawn3 = new Pawn(GameObject, 5, 2, Color.white);
      King bKing = new King(GameObject, 8, 4, Color.black);
      Pawn bPawn1 = new Pawn(GameObject, 3, 7, Color.black);
      Pawn bPawn2 = new Pawn(GameObject, 4, 7, Color.black);
      Pawn bPawn3 = new Pawn(GameObject, 5, 7, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.whites.Add(wPawn1);
      GameObject.whites.Add(wPawn2);
      GameObject.whites.Add(wPawn3);

      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bPawn1);
      GameObject.blacks.Add(bPawn2);
      GameObject.blacks.Add(bPawn3);
      GameObject.UpdateAllBeatFields();

      Assert.IsFalse(wPawn1.Move(3, 5));
      Assert.IsFalse(wPawn1.Move(2, 3));
      Assert.IsTrue(wPawn1.Move(3, 4));
      Assert.IsTrue(bPawn3.Move(5, 6));
      Assert.IsTrue(wPawn1.Move(3, 5));
      Assert.IsFalse(bPawn3.Move(5, 4));
      Assert.IsTrue(bPawn2.Move(4, 5));
      Assert.IsTrue(wPawn1.Move(4, 6)); // cross-field taking
      Assert.IsTrue(GameObject.blacks.Count == 3);

      Assert.IsTrue(bPawn1.Move(4, 6));
      Assert.IsTrue(GameObject.whites.Count == 3);

      bPawn1.field = new Field(3, 7);
      bPawn1.moveCount = 0;
      wPawn2.field = new Field(4, 5);
      Queen bQueen = new Queen(GameObject, 8, 5, Color.black);
      GameObject.blacks.Add(bQueen);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wKing.Move(2, 5));
      Assert.IsTrue(bPawn1.Move(3, 5));
      Assert.IsFalse(wPawn2.Move(3, 6)); // Can't take on cross-field because of wKing became opened to attack
      Assert.IsTrue(wKing.Move(1, 6));
      Assert.IsTrue(bQueen.Move(7, 4));
      Assert.IsFalse(wPawn2.Move(3, 6)); // Can't take on cross-field because of different move count
    }

    [TestMethod]
    public void TestAttackAndTake()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 5, Color.white);
      Pawn wPawn1 = new Pawn(GameObject, 3, 2, Color.white);
      Pawn wPawn2 = new Pawn(GameObject, 4, 2, Color.white);
      Pawn wPawn3 = new Pawn(GameObject, 5, 2, Color.white);
      King bKing = new King(GameObject, 8, 4, Color.black);
      Pawn bPawn1 = new Pawn(GameObject, 3, 7, Color.black);
      Pawn bPawn2 = new Pawn(GameObject, 4, 7, Color.black);
      Pawn bPawn3 = new Pawn(GameObject, 5, 7, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.whites.Add(wPawn1);
      GameObject.whites.Add(wPawn2);
      GameObject.whites.Add(wPawn3);

      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bPawn1);
      GameObject.blacks.Add(bPawn2);
      GameObject.blacks.Add(bPawn3);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wPawn1.CanAttackPosition(2, 3));
      Assert.IsTrue(wPawn1.CanAttackPosition(4, 3));

      Assert.IsTrue(bPawn2.CanAttackPosition(3, 6));
      Assert.IsTrue(bPawn2.CanAttackPosition(5, 6));
    }

    [TestMethod]
    public void TestTransform()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 5, Color.white);
      King bKing = new King(GameObject, 8, 4, Color.black);
      Pawn wPawn1 = new Pawn(GameObject, 1, 7, Color.white);
      GameObject.blacks.Add(bKing);
      GameObject.whites.Add(wKing);
      GameObject.whites.Add(wPawn1);
      GameObject.UpdateAllBeatFields();

      Assert.IsFalse(wPawn1.Transform(FigureTypes.Knight));
      Assert.IsTrue(wPawn1.Move(1, 8));
      Assert.IsFalse(wPawn1.Transform(FigureTypes.Pawn));
      Assert.IsFalse(wPawn1.Transform(FigureTypes.King));
      Assert.IsTrue(wPawn1.Transform(FigureTypes.Knight));
      Figure newFigure = GameObject.GetFigureByXY(wPawn1.field.x, wPawn1.field.y);
      Assert.IsTrue(newFigure.type == FigureTypes.Knight);
      Assert.IsTrue(newFigure.field.x == 1);
      Assert.IsTrue(newFigure.field.y == 8);
      Assert.IsTrue(newFigure.MoveFields.Count == 2);
      Assert.IsTrue(newFigure.CanMoveToPosition(2, 6));
      Assert.IsTrue(newFigure.CanMoveToPosition(3, 7));
    }
  }
}