using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Fakes;
using ClassLibrary1;
using Microsoft.QualityTools.Testing.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class MyBizLogicTests
    {
        [TestMethod]
        public void UpdateExisingUser_UserNotExisted_DoNotUpdate()
        {
            // Arrange

            var updateExecuted = false;

            var userRepository = new StubIUserRepository();
            userRepository.GetByIDGuid = (id) => null;
            userRepository.UpdateUser = (user) => updateExecuted = true;
            var bizLogic = new MyBizLogic(userRepository);

            // Act
            bizLogic.UpdateExisingUser(Guid.NewGuid(), "newFirstName", "newLastName");

            // Assert
            Assert.IsFalse(updateExecuted);
        }

        [TestMethod]
        public void CreateUser_UserAdded()
        {
            // Arrange
            var firstName = "firstName";
            var lastName = "lastName";
            var id = Guid.NewGuid();
            var userRepository = new StubIUserRepository();

            userRepository.AddUser = (user) =>
            {
                Assert.AreEqual(id, user.ID);
                Assert.AreEqual(firstName, user.FirstName);
                Assert.AreEqual(lastName, user.LastName);
            };
            var bizLogic = new MyBizLogic(userRepository);
            // Act
            var createdUser = default(User);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimGuid.NewGuid = () => id;

                // Act

                createdUser = bizLogic.CreateUser(firstName, lastName);
            }

            // Assert
            Assert.AreEqual(firstName, createdUser.FirstName);
            Assert.AreEqual(lastName, createdUser.LastName);

        }
    }
}
