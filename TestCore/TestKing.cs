using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestKing
  {
    [TestMethod]
    public void TestMove()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 2, 1, Color.white);
      Queen wQueen = new Queen(GameObject, 2, 2, Color.white);
      King bKing = new King(GameObject, 7, 8, Color.black);
      Queen bQueen = new Queen(GameObject, 7, 7, Color.black);
      Rook bRook1 = new Rook(GameObject, 1, 8, Color.black);
      Rook bRook2 = new Rook(GameObject, 8, 8, Color.black);
      Rook wRook1 = new Rook(GameObject, 1, 1, Color.white);
      Rook wRook2 = new Rook(GameObject, 8, 1, Color.white);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bQueen);
      GameObject.whites.Add(wQueen);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wKing.MoveFields.Count == 4);
      Assert.IsTrue(wKing.CanMoveToPosition(1, 1));
      Assert.IsTrue(wKing.CanMoveToPosition(1, 2));
      Assert.IsTrue(wKing.CanMoveToPosition(3, 1));
      Assert.IsTrue(wKing.CanMoveToPosition(3, 2));

      Assert.IsTrue(bKing.MoveFields.Count == 4);
      Assert.IsTrue(bKing.CanMoveToPosition(6, 8));
      Assert.IsTrue(bKing.CanMoveToPosition(6, 7));
      Assert.IsTrue(bKing.CanMoveToPosition(8, 8));
      Assert.IsTrue(bKing.CanMoveToPosition(8, 7));

      Assert.IsTrue(wKing.Move(3, 2));
      Assert.IsTrue(bKing.Move(6, 7));
      Assert.IsFalse(wKing.Move(3, 3));
      Assert.IsTrue(wQueen.Move(7, 7));
      Assert.IsTrue(GameObject.blacks.Count == 1);
      Assert.IsFalse(bKing.Move(5, 7));
      Assert.IsFalse(bKing.Move(6, 8));
      Assert.IsTrue(bKing.Move(7, 7));
      Assert.IsTrue(GameObject.whites.Count == 1);

      // verify castling
      GameObject.blacks.Add(bRook1);
      GameObject.blacks.Add(bRook2);
      GameObject.whites.Add(wRook1);
      GameObject.whites.Add(wRook2);
      wKing.field = new Field(5, 1);
      bKing.field = new Field(5, 8);
      wKing.moveCount = 0;
      bKing.moveCount = 0;
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKing.CanMoveToPosition(7, 1));
      Assert.IsTrue(wKing.CanMoveToPosition(3, 1));
      wKing.moveCount = 1;
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      wRook2.moveCount = 1;
      wRook1.moveCount = 1;
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      wKing.moveCount = 0;
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      wRook2.moveCount = 0;
      wRook1.moveCount = 0;
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKing.CanMoveToPosition(7, 1));
      Assert.IsTrue(wKing.CanMoveToPosition(3, 1));

      bRook1.field = new Field(2, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKing.CanMoveToPosition(3, 1));
      bRook1.field = new Field(3, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      bRook1.field = new Field(4, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      bRook1.field = new Field(5, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));
      Assert.IsFalse(wKing.CanMoveToPosition(3, 1));
      bRook1.field = new Field(6, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKing.CanMoveToPosition(3, 1));
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));
      bRook1.field = new Field(7, 7);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.CanMoveToPosition(7, 1));

      // check figure taking by king
      bRook1.field = new Field(5, 2);
      bRook2.field = new Field(7, 2);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.Move(5, 2));

      bRook2.field = new Field(7, 3);
      bKing.field = new Field(6, 3);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(wKing.Move(5, 2));

      bRook2.field = new Field(7, 3);
      bKing.field = new Field(6, 4);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKing.Move(5, 2));
      Assert.IsFalse(bKing.Move(6, 3));
      Assert.IsFalse(bKing.Move(7, 3));
      Assert.IsTrue(bKing.Move(5, 4));
    }

    [TestMethod]
    public void TestAttackAndTake()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 2, 1, Color.white);
      Queen wQueen = new Queen(GameObject, 2, 2, Color.white);
      King bKing = new King(GameObject, 7, 8, Color.black);
      Queen bQueen = new Queen(GameObject, 7, 7, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bQueen);
      GameObject.whites.Add(wQueen);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wKing.CanAttackPosition(1, 1));
      Assert.IsTrue(wKing.CanAttackPosition(1, 2));
      Assert.IsTrue(wKing.CanAttackPosition(3, 1));
      Assert.IsTrue(wKing.CanAttackPosition(3, 2));
      Assert.IsTrue(wKing.CanAttackPosition(2, 2));

      Assert.IsTrue(bKing.CanAttackPosition(6, 8));
      Assert.IsTrue(bKing.CanAttackPosition(6, 7));
      Assert.IsTrue(bKing.CanAttackPosition(8, 8));
      Assert.IsTrue(bKing.CanAttackPosition(8, 7));
      Assert.IsTrue(bKing.CanAttackPosition(7, 7));
    }
  }
}
