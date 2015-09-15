using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestBishop
  {
    [TestMethod]
    public void TestMove()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 1, Color.white);
      Bishop wBish = new Bishop(GameObject, 3, 3, Color.white);
      King bKing = new King(GameObject, 7, 7, Color.black);
      Bishop bBish = new Bishop(GameObject, 5, 5, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bBish);
      GameObject.whites.Add(wBish);

      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wBish.MoveFields.Count == 6);
      Assert.IsTrue(wBish.CanMoveToPosition(2, 4));
      Assert.IsTrue(wBish.CanMoveToPosition(1, 5));
      Assert.IsTrue(wBish.CanMoveToPosition(2, 2));
      Assert.IsTrue(wBish.CanMoveToPosition(4, 2));
      Assert.IsTrue(wBish.CanMoveToPosition(5, 1));
      Assert.IsTrue(wBish.CanMoveToPosition(4, 4));

      Assert.IsTrue(bBish.MoveFields.Count == 8);
      Assert.IsTrue(bBish.CanMoveToPosition(6, 6));
      Assert.IsTrue(bBish.CanMoveToPosition(6, 4));
      Assert.IsTrue(bBish.CanMoveToPosition(7, 3));
      Assert.IsTrue(bBish.CanMoveToPosition(8, 2));
      Assert.IsTrue(bBish.CanMoveToPosition(4, 4));
      Assert.IsTrue(bBish.CanMoveToPosition(4, 6));
      Assert.IsTrue(bBish.CanMoveToPosition(3, 7));
      Assert.IsTrue(bBish.CanMoveToPosition(2, 8));

      Assert.IsFalse(wBish.Move(2, 4));
      Assert.IsFalse(wBish.Move(1, 5));
      Assert.IsFalse(wBish.Move(4, 2));
      Assert.IsFalse(wBish.Move(5, 1));
      Assert.IsTrue(wBish.Move(2, 2));

      Assert.IsFalse(bBish.Move(6, 4));
      Assert.IsFalse(bBish.Move(7, 3));
      Assert.IsFalse(bBish.Move(8, 2));
      Assert.IsFalse(bBish.Move(4, 6));
      Assert.IsFalse(bBish.Move(3, 7));
      Assert.IsFalse(bBish.Move(2, 8));
      Assert.IsTrue(bBish.Move(4, 4));

      Assert.IsTrue(wBish.Move(4, 4));
      Assert.IsTrue(GameObject.blacks.Count == 1);
    }

    [TestMethod]
    public void TestTakeAndAttack()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 2, 1, Color.white);
      Bishop wBish = new Bishop(GameObject, 3, 3, Color.white);
      King bKing = new King(GameObject, 8, 7, Color.black);
      Bishop bBish = new Bishop(GameObject, 5, 5, Color.black);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bBish);
      GameObject.whites.Add(wBish);

      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wBish.CanAttackPosition(2, 4));
      Assert.IsTrue(wBish.CanAttackPosition(1, 5));
      Assert.IsTrue(wBish.CanAttackPosition(1, 1));
      Assert.IsTrue(wBish.CanAttackPosition(2, 2));
      Assert.IsTrue(wBish.CanAttackPosition(4, 2));
      Assert.IsTrue(wBish.CanAttackPosition(5, 1));
      Assert.IsTrue(wBish.CanAttackPosition(4, 4));
      Assert.IsTrue(wBish.CanAttackPosition(5, 5));

      Assert.IsTrue(bBish.CanAttackPosition(6, 6));
      Assert.IsTrue(bBish.CanAttackPosition(7, 7));
      Assert.IsTrue(bBish.CanAttackPosition(8, 8));
      Assert.IsTrue(bBish.CanAttackPosition(6, 4));
      Assert.IsTrue(bBish.CanAttackPosition(7, 3));
      Assert.IsTrue(bBish.CanAttackPosition(8, 2));
      Assert.IsTrue(bBish.CanAttackPosition(4, 4));
      Assert.IsTrue(bBish.CanAttackPosition(3, 3));
      Assert.IsTrue(bBish.CanAttackPosition(4, 6));
      Assert.IsTrue(bBish.CanAttackPosition(3, 7));
      Assert.IsTrue(bBish.CanAttackPosition(2, 8));
    }
  }
}
