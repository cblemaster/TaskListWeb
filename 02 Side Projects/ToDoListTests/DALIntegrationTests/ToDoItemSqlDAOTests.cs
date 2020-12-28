using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Models;
using ToDoList.DAL;
using System.Data.SqlTypes;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class ToDoItemSqlDAOTests: BaseToDoDAOTests
    {
        [TestMethod]
        public void TestCreate_AddsOneToRowCount()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            ToDoItem newItem = new ToDoItem
            {
                Name = "Test",
                Recurrence = "none",
                Reminder = DateTime.Now.AddDays(7),
                DueDate = DateTime.Now.AddDays(7),
                CreatedDate = DateTime.Now,
                FolderName = "Test",
                IsComplete = false,
                IsImportant = false
            };
            //Act
            int startingRowCount = GetTableRowCount("todo_item");
            dao.Create(newItem);
            int endingRowCount = GetTableRowCount("todo_item");
            //Assert
            Assert.AreEqual(startingRowCount + 1, endingRowCount);
        }
        [TestMethod]
        public void TestDelete_SubtractsOneFromRowCount()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            //Act
            int startingRowCount = GetTableRowCount("todo_item");
            dao.Delete(NewToDoItemId);
            int endingRowCount = GetTableRowCount("todo_item");
            //Assert
            Assert.AreEqual(startingRowCount - 1, endingRowCount);
        }
        [TestMethod]
        public void TestGetList_ReturnsMoreThanZeroRows()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            //Act
            IList<ToDoItem> resultList = dao.List();
            //Assert
            Assert.AreNotEqual(0, resultList.Count);
        }
        [TestMethod]
        public void TestGetByFolderName_ReturnsMoreThanZeroRows()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            //Act
            IList<ToDoItem> resultList = dao.List(NewFolderName);
            //Assert
            Assert.AreNotEqual(0, resultList.Count);
        }

        [TestMethod]
        public void TestGetById_ReturnsMoreThanZeroRows()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            //Act
            ToDoItem result = dao.Get(NewToDoItemId);
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestUpdate_ChangesProperties()
        {
            //Arrange
            ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
            ToDoItem updatedItem = dao.Get(NewToDoItemId);
            string startingName = updatedItem.Name;
            SqlDateTime startingDueDate = updatedItem.DueDate;
            SqlDateTime startingReminder = updatedItem.Reminder;
            string startingFolderName = updatedItem.FolderName;
            SqlBoolean startingIsImportant = updatedItem.IsImportant;
            SqlBoolean startingIsComplete = updatedItem.IsComplete;
            string startingRecurrence = updatedItem.Recurrence;
            //Act
            updatedItem.Name += " UPDATED";
            updatedItem.DueDate = DateTime.Now.AddDays(21);
            updatedItem.Reminder = DateTime.Now.AddDays(28);
            updatedItem.Recurrence = "weekly";
            updatedItem.IsComplete = true;
            updatedItem.IsImportant = true;
            updatedItem.FolderName = "TEST";
            dao.Update(updatedItem);
            ToDoItem tdi = dao.Get(NewToDoItemId);
            string endingName = tdi.Name;
            SqlDateTime endingDueDate = tdi.DueDate;
            SqlDateTime endingReminder = tdi.Reminder;
            string endingFolderName = tdi.FolderName;
            SqlBoolean endingIsImportant = tdi.IsImportant;
            SqlBoolean endingIsComplete = tdi.IsComplete;
            string endingRecurrence = tdi.Recurrence;

            //Assert
            Assert.AreNotEqual(startingName, endingName);
            Assert.AreNotEqual(startingDueDate, endingDueDate);
            Assert.AreNotEqual(startingReminder, endingReminder);
            Assert.AreNotEqual(startingRecurrence, endingRecurrence);
            Assert.AreNotEqual(startingFolderName, endingFolderName);
            Assert.AreNotEqual(startingIsImportant, endingIsImportant);
            Assert.AreNotEqual(startingIsComplete, endingIsComplete);
        }

        //[TestMethod]
        //public void TestGetMaxId_Returns_OneRow()
        //{
        //    //arrange
        //    ToDoItemSqlDAO dao = new ToDoItemSqlDAO(ConnectionString);
        //    //act
        //    int result = dao.GetMaxId();
        //    //assert
        //    Assert.AreNotEqual(0, result);
        //}
    }
}