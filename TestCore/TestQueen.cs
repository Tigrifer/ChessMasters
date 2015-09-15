using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestQueen
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
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bQueen);
      GameObject.whites.Add(wQueen);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wQueen.MoveFields.Count == 20);
      Assert.IsTrue(wQueen.CanMoveToPosition(1, 1));
      Assert.IsTrue(wQueen.CanMoveToPosition(3, 3));
      Assert.IsTrue(wQueen.CanMoveToPosition(4, 4));
      Assert.IsTrue(wQueen.CanMoveToPosition(5, 5));
      Assert.IsTrue(wQueen.CanMoveToPosition(6, 6));
      Assert.IsTrue(wQueen.CanMoveToPosition(1, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(1, 3));
      Assert.IsTrue(wQueen.CanMoveToPosition(3, 1));
      Assert.IsTrue(wQueen.CanMoveToPosition(3, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(4, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(5, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(6, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(7, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(8, 2));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 3));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 4));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 5));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 6));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 7));
      Assert.IsTrue(wQueen.CanMoveToPosition(2, 8));

      Assert.IsTrue(bQueen.MoveFields.Count == 20);
      Assert.IsTrue(bQueen.CanMoveToPosition(8, 8));
      Assert.IsTrue(bQueen.CanMoveToPosition(3, 3));
      Assert.IsTrue(bQueen.CanMoveToPosition(4, 4));
      Assert.IsTrue(bQueen.CanMoveToPosition(5, 5));
      Assert.IsTrue(bQueen.CanMoveToPosition(6, 6));
      Assert.IsTrue(bQueen.CanMoveToPosition(8, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(8, 6));
      Assert.IsTrue(bQueen.CanMoveToPosition(6, 8));
      Assert.IsTrue(bQueen.CanMoveToPosition(6, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(5, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(4, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(3, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(2, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(1, 7));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 6));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 5));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 4));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 3));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 2));
      Assert.IsTrue(bQueen.CanMoveToPosition(7, 1));

      Assert.IsTrue(wQueen.Move(7, 2));
      Assert.IsFalse(bQueen.Move(6, 7));
      Assert.IsFalse(bQueen.Move(2, 2));
      Assert.IsTrue(bQueen.Move(7, 4));
      Assert.IsTrue(wQueen.Move(7,4));
      Assert.IsTrue(GameObject.blacks.Count == 1);
    }

    [TestMethod]
    public void TestTakeAndAttack()
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

      Assert.IsTrue(wQueen.CanAttackPosition(1, 1));
      Assert.IsTrue(wQueen.CanAttackPosition(3, 3));
      Assert.IsTrue(wQueen.CanAttackPosition(4, 4));
      Assert.IsTrue(wQueen.CanAttackPosition(5, 5));
      Assert.IsTrue(wQueen.CanAttackPosition(6, 6));
      Assert.IsTrue(wQueen.CanAttackPosition(1, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(1, 3));
      Assert.IsTrue(wQueen.CanAttackPosition(3, 1));
      Assert.IsTrue(wQueen.CanAttackPosition(3, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(4, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(5, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(6, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(7, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(8, 2));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 3));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 4));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 5));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 6));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 7));
      Assert.IsTrue(wQueen.CanAttackPosition(2, 8));
      Assert.IsTrue(wQueen.CanAttackPosition(7, 7));

      Assert.IsTrue(bQueen.CanAttackPosition(8, 8));
      Assert.IsTrue(bQueen.CanAttackPosition(3, 3));
      Assert.IsTrue(bQueen.CanAttackPosition(4, 4));
      Assert.IsTrue(bQueen.CanAttackPosition(5, 5));
      Assert.IsTrue(bQueen.CanAttackPosition(6, 6));
      Assert.IsTrue(bQueen.CanAttackPosition(8, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(8, 6));
      Assert.IsTrue(bQueen.CanAttackPosition(6, 8));
      Assert.IsTrue(bQueen.CanAttackPosition(6, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(5, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(4, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(3, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(2, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(1, 7));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 6));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 5));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 4));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 3));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 2));
      Assert.IsTrue(bQueen.CanAttackPosition(7, 1));
      Assert.IsTrue(bQueen.CanAttackPosition(2, 2));
    }
  }
}
