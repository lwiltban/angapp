using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AngApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AngApp.Tests.Controllers
{
    [TestClass]
    public class ActionControllerTest
    {
        [TestMethod]
        public void JsonUsersTest()
        {
            // Arrange
            ActionController controller = new ActionController();

            // Act
            string jsonUsers = controller.JsonUsers(null) as string;

            List<AngModel.User> users = new JavaScriptSerializer().Deserialize<List<AngModel.User>>(jsonUsers);

            // Assert
            Assert.IsTrue(users.Count > 0, "Returned a valid list of users");
        }

        [TestMethod]
        public void HtmlUsersTest()
        {
            // Arrange
            ActionController controller = new ActionController();

            // Act
            ViewResult result = controller.HtmlUsers(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
