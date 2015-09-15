using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;

namespace TestCore
{
  [TestClass]
  public class TestKnight
  {
    [TestMethod]
    public void TestMove()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 1, Color.white);
      King bKing = new King(GameObject, 8, 8, Color.black);
      Knight bKnight = new Knight(GameObject, 5, 5, Color.black);
      Knight wKnight = new Knight(GameObject, 5, 4, Color.white);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bKnight);
      GameObject.whites.Add(wKnight);

      // knights in the middle of the field
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKnight.MoveFields.Count == 8);
      Assert.IsTrue(wKnight.CanMoveToPosition(3, 5));
      Assert.IsTrue(wKnight.CanMoveToPosition(3, 3));
      Assert.IsTrue(wKnight.CanMoveToPosition(4, 6));
      Assert.IsTrue(wKnight.CanMoveToPosition(4, 2));
      Assert.IsTrue(wKnight.CanMoveToPosition(6, 6));
      Assert.IsTrue(wKnight.CanMoveToPosition(6, 2));
      Assert.IsTrue(wKnight.CanMoveToPosition(7, 3));
      Assert.IsTrue(wKnight.CanMoveToPosition(7, 5));

      Assert.IsTrue(bKnight.MoveFields.Count == 8);
      Assert.IsTrue(bKnight.CanMoveToPosition(3, 6));
      Assert.IsTrue(bKnight.CanMoveToPosition(3, 4));
      Assert.IsTrue(bKnight.CanMoveToPosition(4, 7));
      Assert.IsTrue(bKnight.CanMoveToPosition(4, 3));
      Assert.IsTrue(bKnight.CanMoveToPosition(6, 7));
      Assert.IsTrue(bKnight.CanMoveToPosition(6, 3));
      Assert.IsTrue(bKnight.CanMoveToPosition(7, 4));
      Assert.IsTrue(bKnight.CanMoveToPosition(7, 6));

      // knights at the border of the field
      bKnight.field.x = 8;
      bKnight.field.y = 7;
      wKnight.field.x = 3;
      wKnight.field.y = 2;
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wKnight.MoveFields.Count == 5);
      Assert.IsTrue(wKnight.CanMoveToPosition(1, 3));
      Assert.IsTrue(wKnight.CanMoveToPosition(2, 4));
      Assert.IsTrue(wKnight.CanMoveToPosition(4, 4));
      Assert.IsTrue(wKnight.CanMoveToPosition(5, 3));
      Assert.IsTrue(wKnight.CanMoveToPosition(5, 1));

      Assert.IsTrue(bKnight.MoveFields.Count == 3);
      Assert.IsTrue(bKnight.CanMoveToPosition(6, 8));
      Assert.IsTrue(bKnight.CanMoveToPosition(6, 6));
      Assert.IsTrue(bKnight.CanMoveToPosition(7, 5));

      // Add white rook
      Rook wRook = new Rook(GameObject, 8, 2, Color.white);
      GameObject.whites.Add(wRook);
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wRook.Move(8, 3));
      Assert.IsFalse(bKnight.Move(6, 8)); // Can't move knight - open attack on king
      Assert.IsTrue(bKnight.field.x == 8 && bKnight.field.y == 7);
      Assert.IsFalse(bKnight.Move(6, 6)); // Can't move knight - open attack on king
      Assert.IsTrue(bKnight.field.x == 8 && bKnight.field.y == 7);
      Assert.IsFalse(bKnight.Move(7, 5)); // Can't move knight - open attack on king
      Assert.IsTrue(bKnight.field.x == 8 && bKnight.field.y == 7);

      // Add white pawn and beat it by knight
      Pawn p1 = new Pawn(GameObject, 6, 6, Color.white);
      GameObject.whites.Add(p1);
      GameObject.UpdateAllBeatFields();
      Assert.IsFalse(bKnight.Move(6, 6));

      // remove rook
      GameObject.whites.Remove(wRook);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(bKnight.Move(6, 6));

      // add white rook
      wRook.field.x = 5;
      wRook.field.y = 1;
      GameObject.whites.Add(wRook);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wRook.Move(5, 8));
      Assert.IsTrue(bKnight.Move(5, 8));

      // add white rook again
      wRook.field.x = 1;
      wRook.field.y = 2;
      bKnight.field.x = 6;
      bKnight.field.y = 6;
      GameObject.whites.Add(wRook);
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wRook.Move(1, 8));
      Assert.IsFalse(bKnight.Move(5, 4)); //king not covered
      Assert.IsFalse(bKnight.Move(7, 4)); //king not covered
      Assert.IsTrue(bKnight.Move(7, 8)); //cover king
    }

    [TestMethod]
    public void TestAttackAndTake()
    {
      GameObject GameObject = new GameObject();
      GameObject.whites = new List<Figure>();
      GameObject.blacks = new List<Figure>();
      King wKing = new King(GameObject, 1, 1, Color.white);
      King bKing = new King(GameObject, 8, 8, Color.black);
      Knight bKnight = new Knight(GameObject, 5, 5, Color.black);
      Knight wKnight = new Knight(GameObject, 5, 4, Color.white);
      GameObject.whites.Add(wKing);
      GameObject.blacks.Add(bKing);
      GameObject.blacks.Add(bKnight);
      GameObject.whites.Add(wKnight);

      // knights in the middle of the field
      GameObject.UpdateAllBeatFields();
      Assert.IsTrue(wKnight.CanAttackPosition(3, 5));
      Assert.IsTrue(wKnight.CanAttackPosition(3, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(4, 6));
      Assert.IsTrue(wKnight.CanAttackPosition(4, 2));
      Assert.IsTrue(wKnight.CanAttackPosition(6, 6));
      Assert.IsTrue(wKnight.CanAttackPosition(6, 2));
      Assert.IsTrue(wKnight.CanAttackPosition(7, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(7, 5));

      Assert.IsTrue(bKnight.CanAttackPosition(3, 6));
      Assert.IsTrue(bKnight.CanAttackPosition(3, 4));
      Assert.IsTrue(bKnight.CanAttackPosition(4, 7));
      Assert.IsTrue(bKnight.CanAttackPosition(4, 3));
      Assert.IsTrue(bKnight.CanAttackPosition(6, 7));
      Assert.IsTrue(bKnight.CanAttackPosition(6, 3));
      Assert.IsTrue(bKnight.CanAttackPosition(7, 4));
      Assert.IsTrue(bKnight.CanAttackPosition(7, 6));

      // knights at the border of the field
      bKnight.field.x = 8;
      bKnight.field.y = 7;
      wKnight.field.x = 3;
      wKnight.field.y = 2;
      GameObject.UpdateAllBeatFields();

      Assert.IsTrue(wKnight.CanAttackPosition(1, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(2, 4));
      Assert.IsTrue(wKnight.CanAttackPosition(4, 4));
      Assert.IsTrue(wKnight.CanAttackPosition(5, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(5, 1));

      Assert.IsTrue(bKnight.CanMoveToPosition(6, 8));
      Assert.IsTrue(bKnight.CanMoveToPosition(6, 6));
      Assert.IsTrue(bKnight.CanMoveToPosition(7, 5));

      // Add black pawns
      Pawn bp1 = new Pawn(GameObject, 2, 4, Color.black);
      Pawn bp2 = new Pawn(GameObject, 5, 3, Color.black);
      GameObject.blacks.Add(bp1);
      GameObject.blacks.Add(bp2);
      Assert.IsTrue(wKnight.CanAttackPosition(1, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(2, 4));
      Assert.IsTrue(wKnight.CanAttackPosition(4, 4));
      Assert.IsTrue(wKnight.CanAttackPosition(5, 3));
      Assert.IsTrue(wKnight.CanAttackPosition(5, 1));
    }
  }
}
