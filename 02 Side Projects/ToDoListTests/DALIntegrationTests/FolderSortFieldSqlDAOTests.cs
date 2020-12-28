using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Microsoft.Data.SqlClient;
using ToDoList.DAL;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class FolderSortFieldSqlDAOTests : BaseToDoDAOTests
    {
        [TestMethod]
        public void TestGetList_ReturnsMoreThanZeroRows()
        {
            //arrange
            FolderSortFieldSqlDAO dao = new FolderSortFieldSqlDAO(ConnectionString);
            //act
            IList<FolderSortField> resultList = dao.List();
            //assert
            Assert.AreNotEqual(0, resultList.Count);            
        }

        [TestMethod]
        public void TestGetById_ReturnsOneRow()
        {
            //arrange
            FolderSortFieldSqlDAO dao = new FolderSortFieldSqlDAO(ConnectionString);
            //act
            FolderSortField result = dao.Get(NewSortFieldId);
            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetByName_ReturnsOneRow()
        {
            //arrange
            FolderSortFieldSqlDAO dao = new FolderSortFieldSqlDAO(ConnectionString);
            //act
            FolderSortField result = dao.Get(NewSortFieldName);
            //assert
            Assert.IsNotNull(result);
        }
    }
}
