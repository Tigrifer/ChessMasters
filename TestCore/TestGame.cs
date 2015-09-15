using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore;
using ChessCore.DBManaging;

namespace TestCore
{
  /// <summary>
  /// Summary description for UnitTest1
  /// </summary>
  [TestClass]
  public class TestGame
  {
    public TestGame()
    {
      //
      // TODO: Add constructor logic here
      //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get { return testContextInstance; }
      set { testContextInstance = value; }
    }

    #region Additional test attributes

    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //

    #endregion

    [TestMethod]
    public void CreateGame()
    {
      GameObject GameObject = new GameObject();
      // check defaults
      Assert.IsNotNull(GameObject);
      Assert.IsTrue(GameObject.GetColorTurn() == Color.white);
      Assert.IsFalse(GameObject.IsMat());
      Assert.IsFalse(GameObject.IsPat());
      Assert.IsNotNull(GameObject.whites);
      Assert.IsNotNull(GameObject.blacks);
      Assert.IsTrue(GameObject.moveCount == 0);

      // check whites
      foreach (Figure f in GameObject.whites)
        Assert.IsTrue(f.color == Color.white);

      Assert.IsNotNull(GameObject.GetFigureByXY(1, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(8, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(2, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(7, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(3, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(6, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(4, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(5, 1));
      Assert.IsNotNull(GameObject.GetFigureByXY(1, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(2, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(3, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(4, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(5, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(6, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(7, 2));
      Assert.IsNotNull(GameObject.GetFigureByXY(8, 2));

      Assert.IsInstanceOfType(GameObject.GetFigureByXY(1, 1), typeof (Rook));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(8, 1), typeof (Rook));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(2, 1), typeof (Knight));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(7, 1), typeof (Knight));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(3, 1), typeof (Bishop));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(6, 1), typeof (Bishop));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(4, 1), typeof (Queen));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(5, 1), typeof (King));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(1, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(2, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(3, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(4, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(5, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(6, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(7, 2), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(8, 2), typeof (Pawn));

      Assert.IsTrue(GameObject.GetFigureByXY(1, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(8, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(2, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(7, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(3, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(6, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(4, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(5, 1).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(1, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(2, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(3, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(4, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(5, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(6, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(7, 2).color == Color.white);
      Assert.IsTrue(GameObject.GetFigureByXY(8, 2).color == Color.white);

      // check blacks
      foreach (Figure f in GameObject.blacks)
        Assert.IsTrue(f.color == Color.black);

      Assert.IsNotNull(GameObject.GetFigureByXY(1, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(8, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(2, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(7, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(3, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(6, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(4, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(5, 8));
      Assert.IsNotNull(GameObject.GetFigureByXY(1, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(2, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(3, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(4, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(5, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(6, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(7, 7));
      Assert.IsNotNull(GameObject.GetFigureByXY(8, 7));

      Assert.IsInstanceOfType(GameObject.GetFigureByXY(1, 8), typeof (Rook));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(8, 8), typeof (Rook));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(2, 8), typeof (Knight));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(7, 8), typeof (Knight));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(3, 8), typeof (Bishop));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(6, 8), typeof (Bishop));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(4, 8), typeof (Queen));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(5, 8), typeof (King));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(1, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(2, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(3, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(4, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(5, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(6, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(7, 7), typeof (Pawn));
      Assert.IsInstanceOfType(GameObject.GetFigureByXY(8, 7), typeof (Pawn));

      Assert.IsTrue(GameObject.GetFigureByXY(1, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(8, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(2, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(7, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(3, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(6, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(4, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(5, 8).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(1, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(2, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(3, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(4, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(5, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(6, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(7, 7).color == Color.black);
      Assert.IsTrue(GameObject.GetFigureByXY(8, 7).color == Color.black);

      // check kings
      Assert.IsInstanceOfType(GameObject.GetKing(Color.black), typeof (King));
      Assert.IsInstanceOfType(GameObject.GetKing(Color.white), typeof (King));
      Assert.IsTrue(GameObject.GetKing(Color.black).color == Color.black);
      Assert.IsTrue(GameObject.GetKing(Color.white).color == Color.white);
    }

    [TestMethod]
    public void GameMoves()
    {
      GameObject game = new GameObject();
      // whites ♔ WHITE KING | ♕ WHITE QUEEN | ♖ WHITE ROOK | ♗ WHITE BISHOP | ♘ WHITE KNIGHT | ♙ WHITE PAWN
      // blacks ♚ BLACK KING | ♛ BLACK QUEEN | ♜ BLACK ROOK | ♝ BLACK BISHOP | ♞ BLACK KNIGHT | ♟ BLACK PAWN
      short movecount = game.moveCount;
      Assert.IsFalse(game.blacks[0].Move(1, 4)); // not black turn
      Assert.IsTrue(movecount == game.moveCount && movecount == game.blacks[0].moveCount);
      Assert.IsFalse(game.GetKing(Color.white).Move(5, 1)); // can't move to the field figure standing in
      Assert.IsTrue(movecount == game.moveCount && 0 == game.GetKing(Color.white).moveCount);
      Assert.IsFalse(game.whites[0].Move(1, 5)); // wrong move for pawn
      Assert.IsTrue(movecount == game.moveCount && movecount == game.whites[0].moveCount);
      Assert.IsTrue(game.whites[4].Move(5, 4)); // correct move by King's pawn
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == game.whites[4].moveCount);
      Assert.IsFalse(game.GetKing(Color.white).Move(5, 2)); // can't move - black's turn
      Assert.IsTrue(movecount == game.moveCount && 0 == game.GetKing(Color.white).moveCount);
      Assert.IsTrue(game.blacks[4].Move(5, 5)); // correct move by King's pawn
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == game.blacks[4].moveCount);

      Figure wBishop = game.GetFigureByXY(6, 1);
      Assert.IsTrue(wBishop.Move(4, 3));
      Assert.IsNull(game.GetFigureByXY(6, 1));
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == wBishop.moveCount);

      Figure bBishop = game.GetFigureByXY(6, 8);
      Assert.IsTrue(bBishop.Move(4, 6));
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == bBishop.moveCount);

      Figure wKnight = game.GetFigureByXY(7, 1);
      Assert.IsTrue(wKnight.Move(6, 3));
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == wKnight.moveCount);

      Figure bKnight = game.GetFigureByXY(7, 8);
      Assert.IsTrue(bKnight.Move(6, 6));
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == bKnight.moveCount);

      King wKing = game.GetKing(Color.white); // castling
      Assert.IsTrue(wKing.Move(7, 1));
      movecount++;
      Assert.IsTrue(movecount == game.moveCount && movecount == wKing.moveCount);

      Figure bRook = game.GetFigureByXY(8, 8); // prevent castling for black's
      bRook.moveCount = movecount;
      King bKing = game.GetKing(Color.black);
      Assert.IsFalse(bKing.Move(7, 8));
      Assert.IsTrue(bKing.field.x == 5);
      bRook.moveCount = 0;
      Assert.IsTrue(bKing.Move(7, 8));
    }

    [TestMethod]
    public void GameMate()
    {
      // Variant 1
      GameObject game = new GameObject();
      Assert.IsTrue(game.GetFigureByXY(5, 2).Move(5, 4));
      Assert.IsTrue(game.GetFigureByXY(5, 7).Move(5, 5));
      Assert.IsTrue(game.GetFigureByXY(6, 1).Move(3, 4));
      Assert.IsTrue(game.GetFigureByXY(6, 8).Move(3, 5));
      Assert.IsTrue(game.GetFigureByXY(4, 1).Move(6, 3));
      Assert.IsTrue(game.GetFigureByXY(8, 7).Move(8, 5));
      Assert.IsTrue(game.GetFigureByXY(6, 3).Move(6, 7));
      Assert.IsTrue(game.isMat);

      // Variant 2
      game = new GameObject();
      game.whites = new List<Figure>();
      game.blacks = new List<Figure>();
      King wKing = new King(game, 2, 3, Color.white);
      King bKing = new King(game, 1, 1, Color.black);
      Knight bKnight = new Knight(game, 2, 1, Color.black);
      Rook wRook = new Rook(game, 7, 2, Color.white);
      game.whites.Add(wKing);
      game.whites.Add(wRook);
      game.blacks.Add(bKing);
      game.blacks.Add(bKnight);
      game.UpdateAllBeatFields();

      Assert.IsTrue(wRook.Move(1, 2));
      Assert.IsTrue(game.isMat);
    }

    [TestMethod]
    public void GamePat()
    {
      GameObject game = new GameObject();
      game.whites = new List<Figure>();
      game.blacks = new List<Figure>();
      King wKing = new King(game, 2, 3, Color.white);
      King bKing = new King(game, 1, 1, Color.black);
      Knight bKnight = new Knight(game, 2, 1, Color.black);
      Rook wRook = new Rook(game, 7, 2, Color.white);
      game.whites.Add(wKing);
      game.whites.Add(wRook);
      game.blacks.Add(bKing);
      game.blacks.Add(bKnight);
      game.UpdateAllBeatFields();

      Assert.IsTrue(wRook.Move(2, 2));
      Assert.IsFalse(game.isPat);
      Assert.IsTrue(bKnight.Move(4, 2));
      Assert.IsTrue(wRook.Move(4, 2));
      Assert.IsTrue(bKing.Move(2, 1));
      Assert.IsTrue(wRook.Move(3, 2));
      Assert.IsTrue(bKing.Move(1, 1));
      Assert.IsTrue(wRook.Move(2, 2));
      Assert.IsTrue(game.isPat);
    }

    [TestMethod]
    public void TestDraw()
    {
      GameObject game = new GameObject();
      // get knight
      game.whites = new List<Figure>();
      game.blacks = new List<Figure>();
      King wKing = new King(game, 2, 2, Color.white);
      King bKing = new King(game, 2, 6, Color.black);
      Rook wRook = new Rook(game, 1, 1, Color.white);
      Rook bRook = new Rook(game, 1, 7, Color.black);
      Pawn wPawn = new Pawn(game, 5, 2, Color.white);
      game.whites.Add(wKing);
      game.whites.Add(wRook);
      game.blacks.Add(bKing);
      game.blacks.Add(bRook);
      game.whites.Add(wPawn);
      game.UpdateAllBeatFields();

      Assert.IsFalse(game.isDraw);
      wRook.Move(2, 1);
      bRook.Move(2, 7);
      Assert.IsTrue(game.movesToDraw == 2);
      wPawn.Move(5, 4);
      Assert.IsTrue(game.movesToDraw == 0);
      bRook.Move(3, 7);
      wPawn.Move(5, 5);
      Assert.IsTrue(game.movesToDraw == 0);
      bRook.Move(4, 7);
      wPawn.Move(5, 6);
      Assert.IsTrue(game.movesToDraw == 0);
      bRook.Move(1, 7);
      wPawn.Move(5, 7);
      Assert.IsTrue(game.movesToDraw == 0);
      bRook.Move(2, 7);
      wRook.Move(1, 1);
      Assert.IsTrue(game.movesToDraw == 2);
      bRook.Move(5, 7);
      Assert.IsTrue(game.movesToDraw == 0);

      sbyte new_x;
      for (int i = 0; i < 100; i++)
      {
        if (game.GetColorTurn() == Color.white)
        {
          new_x = (sbyte) (wRook.field.x == 1 ? 2 : 1);
          Assert.IsTrue(wRook.Move(new_x, 1));
        }
        else
        {
          new_x = (sbyte) (bRook.field.x == 1 ? 2 : 1);
          Assert.IsTrue(bRook.Move(new_x, 7));
        }
        if (game.movesToDraw == 100)
          Assert.IsTrue(game.isDraw);
      }
    }

    [TestMethod]
    public void TestRepeatPosition3Times()
    {
      GameObject game = new GameObject();
      game.UpdateAllBeatFields();
      game.whites[0].Move(1, 4);
      game.blacks[0].Move(1, 5);
      Assert.IsFalse(game.IsPositionRepeatsThirdTime());
      game.GetFigureByXY(2, 1).Move(3, 3);
      game.GetFigureByXY(2, 8).Move(3, 6);
      Assert.IsFalse(game.IsPositionRepeatsThirdTime());
      game.GetFigureByXY(3, 3).Move(2, 1);
      game.GetFigureByXY(3, 6).Move(2, 8);
      Assert.IsFalse(game.IsPositionRepeatsThirdTime());
      game.GetFigureByXY(2, 1).Move(3, 3);
      game.GetFigureByXY(2, 8).Move(3, 6);
      Assert.IsFalse(game.IsPositionRepeatsThirdTime());
      game.GetFigureByXY(3, 3).Move(2, 1);
      game.GetFigureByXY(3, 6).Move(2, 8);
      Assert.IsTrue(game.IsPositionRepeatsThirdTime());

      game = new GameObject();
      game.whites = new List<Figure>();
      game.blacks = new List<Figure>();
      King wKing = new King(game, 2, 2, Color.white);
      King bKing = new King(game, 2, 6, Color.black);
      game.whites.Add(wKing);
      game.blacks.Add(bKing);
      game.UpdateAllBeatFields();
      wKing.Move(1, 2);
      bKing.Move(1, 6);
      wKing.Move(2, 2);
      bKing.Move(2, 6);
      wKing.Move(1, 2);
      bKing.Move(1, 6);
      wKing.Move(2, 2);
      bKing.Move(2, 6);
      wKing.Move(1, 2);
      Assert.IsTrue(game.IsPositionRepeatsThirdTime());
    }

    [TestMethod]
    public void TestCreateGame()
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User usr1 = DBDataManager.GetUserByLogin("testuser1");
        User usr2 = DBDataManager.GetUserByLogin("testuser2");
        if (usr1 == null || usr2 == null)
        {
          Assert.IsTrue(DBDataManager.CreateUser("testuser1", "123", "name1", "testuser1@gmail.com"));
          Assert.IsTrue(DBDataManager.CreateUser("testuser2", "123", "name2", "testuser2@gmail.com")); 
          usr1 = DBDataManager.GetUserByLogin("testuser1");
          usr2 = DBDataManager.GetUserByLogin("testuser2");
        }

        Assert.IsTrue(DBDataManager.CreateGame(usr1.Id, usr2.Id) != 0);
      }
    }

    [TestMethod]
    public void TestLoadGame()
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        int gameID = DBDataManager.CreateGame(7, 9);
        GameObject gameObject = DBDataManager.LoadGameById(gameID);
      }
    }
  }
}