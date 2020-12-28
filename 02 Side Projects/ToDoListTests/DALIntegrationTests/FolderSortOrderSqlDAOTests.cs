using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList.DAL;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class FolderSortOrderSqlDAOTests: BaseToDoDAOTests
    {
        [TestMethod]
        public void TestGetList_ReturnsMoreThanZeroRows()
        {
            //arrange
            FolderSortOrderSqlDAO dao = new FolderSortOrderSqlDAO(ConnectionString);
            //act
            IList<FolderSortOrder> resultList = dao.List();
            //assert
            Assert.AreNotEqual(0, resultList.Count);
        }

        [TestMethod]
        public void TestGetById_ReturnsOneRow()
        {
            //arrange
            FolderSortOrderSqlDAO dao = new FolderSortOrderSqlDAO(ConnectionString);
            //act
            FolderSortOrder result = dao.Get(NewSortOrderId);
            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetByName_ReturnsOneRow()
        {
            //arrange
            FolderSortOrderSqlDAO dao = new FolderSortOrderSqlDAO(ConnectionString);
            //act
            FolderSortOrder result = dao.Get(NewSortOrderName);
            //assert
            Assert.IsNotNull(result);
        }
    }
}
