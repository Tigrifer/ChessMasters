using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessCore.DBManaging;

namespace TestCore
{
  [TestClass]
  public class TestUsers
  {
    [TestMethod]
    public void TestUsersMethods()
    {
      using (DataContextDataContext dc = new DataContextDataContext())
      {
        User usr1 = DBDataManager.GetUserByLogin("testuser1");
        User usr2 = DBDataManager.GetUserByLogin("testuser2");
        if (usr1 != null) dc.Users.DeleteOnSubmit(dc.Users.First(u => u.Id == usr1.Id));
        if (usr2 != null) dc.Users.DeleteOnSubmit(dc.Users.First(u => u.Id == usr2.Id));
        dc.SubmitChanges();

        Assert.IsTrue(DBDataManager.CreateUser("testuser1", "123", "name1", "testuser1@gmail.com"));
        Assert.IsFalse(DBDataManager.CreateUser("testuser1", "123", "name1", "testuser1@gmail.com"));
        Assert.IsTrue(DBDataManager.CreateUser("testuser2", "123", "name2", "testuser2@gmail.com"));
        usr1 = DBDataManager.GetUserByLogin("testuser1");
        usr2 = DBDataManager.GetUserByLogin("testuser2");
        Assert.IsTrue(usr1 != null && usr2 != null);
      }
    }
  }
}