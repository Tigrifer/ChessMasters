using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ChessCore;
using ChessCore.DBManaging;

/// <summary>
/// Сводное описание для DBDataManager
/// </summary>
public class DBDataManager
{
  public DBDataManager()
  {
  }

  public static Figure FigureFromXML()
  {
    return null;
  }

  public static ChessCore.DBManaging.Game GameFromDBObj()
  {
    //from //TODO: restore GameObject from db obj
    return null;
  }

  public static bool UpdateGame(ChessCore.GameObject gs)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        var data = (from G in dc.Games
                    where G.Id == gs.gameId
                    select G).FirstOrDefault();
        if (data == null) return false;
        data.GameData = Serialize(gs);
        data.GameStatus = (int) gs.status;
        dc.SubmitChanges();
      }
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  public static int CreateGame(int bId, int wId)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        Game dbGame = new Game();
        dbGame.BlackUserId = bId;
        dbGame.WhiteUserId = wId;
        GameObject cGameObject = new GameObject();
        dbGame.GameData = Serialize(cGameObject.GetState());
        dc.Games.InsertOnSubmit(dbGame);
        dc.SubmitChanges();
        return dbGame.Id;
      }
    }
    catch (Exception)
    {
      return 0;
    }
  }

  public static GameObject LoadGameById(int gId)
  {
    try
    {
      GameState GameState;
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        Game dbGame = dc.Games.FirstOrDefault(g => g.Id == gId);
        if (dbGame == null)
          return null;
        GameState = (GameState)Deserialize(dbGame);
      }
      GameObject GameObject = null;
      return GameObject;
    }
    catch (Exception)
    {
      return null;
    }
  }

  public static bool CreateUser(string login, string pass, string name, string email)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User u = new User()
          {
            Email = email,
            Login = login,
            Name = login,
            Password = ChessCore.Utils.CalculateMD5Hash(pass)
          };
        dc.Users.InsertOnSubmit(u);
        dc.SubmitChanges();
      }
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  public static User GetUserById(int id)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User user = dc.Users.Where(u => u.Id == id).FirstOrDefault();
        return user;
      }
    }
    catch (Exception)
    {
      return null;
    }
  }

  public static User GetUserByEmail(string email)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User user = dc.Users.Where(u => u.Email == email).FirstOrDefault();
        return user;
      }
    }
    catch (Exception)
    {
      return null;
    }
  }

  public static User GetUserByLogin(string login)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User user = dc.Users.Where(u => u.Login == login).FirstOrDefault();
        return user;
      }
    }
    catch (Exception)
    {
      return null;
    }
  }

  public static void RemoveUser(User usr)
  {
    try
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        dc.Users.DeleteOnSubmit(usr);
        dc.SubmitChanges();
      }
    }
    catch (Exception)
    {
    }
  }

  private static byte[] Serialize(object obj)
  {
    BinaryFormatter bf = new BinaryFormatter();
    MemoryStream stream = new MemoryStream();
    bf.Serialize(stream, obj);
    return stream.ToArray();
  }

  private static GameState Deserialize(ChessCore.DBManaging.Game g)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    MemoryStream stream = new MemoryStream(g.GameData.ToArray());
    GameState gs = (GameState)formatter.Deserialize(stream);
    return gs;
  }
}