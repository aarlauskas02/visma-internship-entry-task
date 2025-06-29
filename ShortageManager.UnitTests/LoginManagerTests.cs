namespace ShortageManager.UnitTests
{
    [TestClass]
    public sealed class LoginManagerTests
    {
        private readonly LoginManager _loginManager;

        public LoginManagerTests()
        {
            _loginManager = new LoginManager();
        }

        [TestMethod]
        public void Login_ValidRegular()
        {

            bool result = _loginManager.Login("testRegular");
            Assert.AreEqual(Role.Regular, _loginManager.CurrentUser.Role, "Current user should be a regular user after login");
            Assert.IsTrue(result, "Login should succeed for valid username");
        }

        [TestMethod]
        public void Login_ValidAdmin()
        {
            bool result = _loginManager.Login("testAdmin");
            Assert.AreEqual(Role.Admin, _loginManager.CurrentUser.Role, "Current user should be an admin user after login");
            Assert.IsTrue(result, "Login should succeed for valid username");
        }

        [TestMethod]
        public void Login_Invalid()
        {
            bool result = _loginManager.Login("testInvalid");
            Assert.IsFalse(result, "Login should fail for invalid username");
        }
        [TestMethod]
        public void Login_Empty()
        {
            bool result = _loginManager.Login("");
            Assert.IsFalse(result, "Login should fail for empty username");
        }
    }
}