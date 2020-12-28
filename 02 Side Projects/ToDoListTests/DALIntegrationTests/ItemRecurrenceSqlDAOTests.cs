using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList.DAL;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class ItemRecurrenceSqlDAOTests: BaseToDoDAOTests
    {
        [TestMethod]
        public void TestGetList_ReturnsMoreThanZeroRows()
        {
            //arrange
            ItemRecurrenceSqlDAO dao = new ItemRecurrenceSqlDAO(ConnectionString);
            //act
            IList<ItemRecurrence> resultList = dao.List();
            //assert
            Assert.AreNotEqual(0, resultList.Count);
        }

        [TestMethod]
        public void TestGetById_ReturnsOneRow()
        {
            //arrange
            ItemRecurrenceSqlDAO dao = new ItemRecurrenceSqlDAO(ConnectionString);
            //act
            ItemRecurrence result = dao.Get(NewItemRecurrenceId);
            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetByName_ReturnsOneRow()
        {
            //arrange
            ItemRecurrenceSqlDAO dao = new ItemRecurrenceSqlDAO(ConnectionString);
            //act
            ItemRecurrence result = dao.Get(NewItemRecurrence);
            //assert
            Assert.IsNotNull(result);
        }
    }
}
