using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace CalculateTotal.Test
{
    [TestClass]
    public class CalculateTotalTest
    {

        [TestMethod]
        public void Test_RetrieveFile()
        {
            //Arrange

            //Act
            string fileText = Utimco_Form_Project.Form1.retrieveFile(new DirectoryInfo(Environment.CurrentDirectory)
                .Parent
                .Parent.FullName + "//jsonTest.txt");
            //Assert
            Assert.IsNotNull(fileText);
        }

        [TestMethod]
        public void Test_CalculateTotal()
        {
            // Arrange
            List<Utimco_Form_Project.Form1.RootObject> menus;
            string totals = "";

            //Act

            string fileText = Utimco_Form_Project.Form1.retrieveFile(new DirectoryInfo(Environment.CurrentDirectory)
                .Parent
                .Parent.FullName + "//jsonTest.txt");
                //.GetDirectories().First(d => d.Name == "CalculateTotal").FullName + "\\jsonTest.txt");
            menus = Utimco_Form_Project.Form1.calculateTotals(JsonConvert.DeserializeObject<List<Utimco_Form_Project.Form1.RootObject>>(fileText));
            foreach (var menu in menus)
            {
                if (menu.menu.sum.ToString() == "248")
                {
                    totals += menu.menu.sum.ToString();
                }
                else
                {
                    totals += menu.menu.sum.ToString() + ",";
                }
            }

            //Assert
            Assert.AreEqual<string>("46,0,248",totals);
        }
    }
}
