using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList.DAL;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class ToDoFolderSqlDAOTests: BaseToDoDAOTests
    {

        [TestMethod]
        public void TestCreate_AddsOneToRowCount()
        {
            //Arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
            ToDoFolder newFolder = new ToDoFolder
            {
                Name = "Test2",
                SortField = "none",
                SortOrder = "none",
                CreatedDate = DateTime.Now
            };
            //Act
            int startingRowCount = GetTableRowCount("todo_folder");
            dao.Create(newFolder);
            int endingRowCount = GetTableRowCount("todo_folder");
            //Assert
            Assert.AreEqual(startingRowCount + 1, endingRowCount);
        }

        [TestMethod]
        public void TestDelete_SubtractsOneFromRowCount()
        {
            //Arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);

            // -- Have to delete the test todo item before the folder can be deleted
            ToDoItemSqlDAO idao = new ToDoItemSqlDAO(ConnectionString);
            idao.Delete(NewToDoItemId);
            
            //Act
            int startingRowCount = GetTableRowCount("todo_folder");
            dao.Delete(NewToDoFolderId);
            int endingRowCount = GetTableRowCount("todo_folder");
            //Assert
            Assert.AreEqual(startingRowCount - 1, endingRowCount);
        }
        
        [TestMethod]
        public void TestUpdate_ChangesNameProperty()
        {
            // ideally the updates to sortfolderid and sortorderid
            // should be tested too 
            
            //Arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
            ToDoFolder updatedFolder = dao.Get(NewToDoFolderId);
            string startingName = updatedFolder.Name;
            //Act
            updatedFolder.Name += " UPDATED";
            dao.Update(updatedFolder);
            string endingName = dao.Get(NewToDoFolderId).Name;
            //Assert
            Assert.AreNotEqual(startingName, endingName);
        }
        
        [TestMethod]
        public void TestGetList_ReturnsMoreThanZeroRows()
        {
            //arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
            //act
            IList<ToDoFolder> resultList = dao.List();
            //assert
            Assert.AreNotEqual(0, resultList.Count);
        }

        [TestMethod]
        public void TestGetById_ReturnsOneRow()
        {
            //arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
            //act
            ToDoFolder result = dao.Get(NewToDoFolderId);
            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetByName_ReturnsOneRow()
        {
            //arrange
            ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
            //act
            ToDoFolder result = dao.Get(NewFolderName);
            //assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void TestGetMaxId_Returns_OneRow()
        //{
        //    //arrange
        //    ToDoFolderSqlDAO dao = new ToDoFolderSqlDAO(ConnectionString);
        //    //act
        //    int result = dao.GetMaxId();
        //    //assert
        //    Assert.AreNotEqual(0, result);
        //}
    }
}