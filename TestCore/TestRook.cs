using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestRook
  {
    [TestMethod]
    public void TestMove()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 1, Color.white);
      Rook wRook = new Rook(GameObject, 1, 2, Color.white);
      King bKing = new King(GameObject, 1, 8, Color.black);
      Rook bRook = new Rook(GameObject, 1, 7, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bRook);
      GameObject.whites.Add(wRook);

      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wRook.MoveFields.Count == 11);
      Assert.IsTrue(wRook.CanMoveToPosition(1, 3));
      Assert.IsTrue(wRook.CanMoveToPosition(1, 4));
      Assert.IsTrue(wRook.CanMoveToPosition(1, 5));
      Assert.IsTrue(wRook.CanMoveToPosition(1, 6));
      Assert.IsTrue(wRook.CanMoveToPosition(2, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(3, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(4, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(5, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(6, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(7, 2));
      Assert.IsTrue(wRook.CanMoveToPosition(8, 2));

      Assert.IsTrue(bRook.MoveFields.Count == 11);
      Assert.IsTrue(bRook.CanMoveToPosition(1, 3));
      Assert.IsTrue(bRook.CanMoveToPosition(1, 4));
      Assert.IsTrue(bRook.CanMoveToPosition(1, 5));
      Assert.IsTrue(bRook.CanMoveToPosition(1, 6));
      Assert.IsTrue(bRook.CanMoveToPosition(2, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(3, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(4, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(5, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(6, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(7, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(8, 7));

      // with gaps
      wRook.field.x = 3;
      wRook.field.y = 3;
      bRook.field.x = 6;
      bRook.field.y = 6;
      GameObject.UpdateAllBeatFields();
      Pawn wp1 = new Pawn(GameObject, 1, 3, Color.white);
      Pawn wp2 = new Pawn(GameObject, 3, 2, Color.white);
      Pawn bp1 = new Pawn(GameObject, 6, 3, Color.black);
      Pawn bp2 = new Pawn(GameObject, 3, 6, Color.black);
      GameObject.whites.Add(wp1);
      GameObject.whites.Add(wp2);
      GameObject.blacks.Add(bp1);
      GameObject.blacks.Add(bp2);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wRook.MoveFields.Count == 5);
      Assert.IsTrue(wRook.CanMoveToPosition(2, 3));
      Assert.IsTrue(wRook.CanMoveToPosition(4, 3));
      Assert.IsTrue(wRook.CanMoveToPosition(5, 3));
      Assert.IsTrue(wRook.CanMoveToPosition(3, 4));
      Assert.IsTrue(wRook.CanMoveToPosition(3, 5));

      Assert.IsTrue(bRook.MoveFields.Count == 8);
      Assert.IsTrue(bRook.CanMoveToPosition(4, 6));
      Assert.IsTrue(bRook.CanMoveToPosition(5, 6));
      Assert.IsTrue(bRook.CanMoveToPosition(7, 6));
      Assert.IsTrue(bRook.CanMoveToPosition(8, 6));
      Assert.IsTrue(bRook.CanMoveToPosition(6, 8));
      Assert.IsTrue(bRook.CanMoveToPosition(6, 7));
      Assert.IsTrue(bRook.CanMoveToPosition(6, 5));
      Assert.IsTrue(bRook.CanMoveToPosition(6, 4));

      wKing.field = new Field(6, 1);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wRook.Move(6, 3));
      Assert.IsTrue(GameObject.blacks.Count == 3);
      Assert.IsTrue(bRook.Move(6, 7));
      Assert.IsFalse(wRook.Move(5, 3));
      Assert.IsFalse(wRook.Move(8, 3));
      Assert.IsTrue(wRook.Move(6, 7));
      Assert.IsTrue(GameObject.blacks.Count == 2);
    }

    [TestMethod]
    public void TestAttackAndTake()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 1, Color.white);
      Rook wRook = new Rook(GameObject, 1, 2, Color.white);
      King bKing = new King(GameObject, 1, 8, Color.black);
      Rook bRook = new Rook(GameObject, 1, 7, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bRook);
      GameObject.whites.Add(wRook);

      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wRook.CanAttackPosition(1, 3));
      Assert.IsTrue(wRook.CanAttackPosition(1, 4));
      Assert.IsTrue(wRook.CanAttackPosition(1, 5));
      Assert.IsTrue(wRook.CanAttackPosition(1, 6));
      Assert.IsTrue(wRook.CanAttackPosition(1, 7));
      Assert.IsTrue(wRook.CanAttackPosition(2, 2));
      Assert.IsTrue(wRook.CanAttackPosition(3, 2));
      Assert.IsTrue(wRook.CanAttackPosition(4, 2));
      Assert.IsTrue(wRook.CanAttackPosition(5, 2));
      Assert.IsTrue(wRook.CanAttackPosition(6, 2));
      Assert.IsTrue(wRook.CanAttackPosition(7, 2));
      Assert.IsTrue(wRook.CanAttackPosition(8, 2));

      Assert.IsTrue(bRook.CanAttackPosition(1, 2));
      Assert.IsTrue(bRook.CanAttackPosition(1, 3));
      Assert.IsTrue(bRook.CanAttackPosition(1, 4));
      Assert.IsTrue(bRook.CanAttackPosition(1, 5));
      Assert.IsTrue(bRook.CanAttackPosition(1, 6));
      Assert.IsTrue(bRook.CanAttackPosition(2, 7));
      Assert.IsTrue(bRook.CanAttackPosition(3, 7));
      Assert.IsTrue(bRook.CanAttackPosition(4, 7));
      Assert.IsTrue(bRook.CanAttackPosition(5, 7));
      Assert.IsTrue(bRook.CanAttackPosition(6, 7));
      Assert.IsTrue(bRook.CanAttackPosition(7, 7));
      Assert.IsTrue(bRook.CanAttackPosition(8, 7));
    }
  }
}
