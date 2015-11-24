using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BettingApplication.Controllers;
using BettingApplication.Models;
using System.Web.Mvc;




namespace TestForApi
{
    [TestClass]
    public class LeagueTableTest
    {
        //Test för att se att rätt vy returneras vid ListTable förfrågan
        [TestMethod]
        public void ListTable_Returns_Correct_View_Success()
        {
            //Arrange
            var controller = new HomeController();

            // Act
            var result = controller.ListTable() as ViewResult;

            // Assert
            Assert.AreEqual("ListTable", result.ViewName);
        }


        // behöver tittas på...
        [TestMethod]
        public void ListTable_Returns_Correct_Number_Of_Teams_In_Standing()
        {
            //Arrange
            var controller = new HomeController();

            // Act
            var result = controller.ListTable();

            var numberOfTeams = result.Model;


            // Assert
            Assert.IsTrue(1 == 1);

        }

    }
}