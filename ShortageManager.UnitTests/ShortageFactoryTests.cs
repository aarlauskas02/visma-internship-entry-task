namespace ShortageManager.UnitTests
{
    [TestClass]
    public sealed class ShortageFactoryTests
    {

        private readonly ShortageFactory _shortageFactory;

        public ShortageFactoryTests()
        {
            _shortageFactory = new ShortageFactory();
        }

        [TestMethod]
        public void CreateShortage_ValidData()
        {
            var shortage = _shortageFactory.CreateShortage(
                "Test Title",
                "Test Name",
                Room.Kitchen,
                Category.Food,
                5,
                new User { Username = "Test User", Role = Role.Regular });

            Assert.IsNotNull(shortage, "Shortage should not be null for valid data");
            Assert.AreEqual("Test Title", shortage.Title, "Shortage title should match the input");
            Assert.AreEqual("Test Name", shortage.Name, "Shortage name should match the input");
            Assert.AreEqual(Room.Kitchen, shortage.Room, "Shortage room should match the input");
            Assert.AreEqual(Category.Food, shortage.Category, "Shortage category should match the input");
            Assert.AreEqual(5, shortage.Priority, "Shortage priority should match the input");
            Assert.IsNotNull(shortage.CreatedOn, "Shortage created date should not be null");
            Assert.IsNotNull(shortage.CreatedBy, "Shortage created by user should not be null");
            Assert.AreEqual("Test User", shortage.CreatedBy.Username, "Shortage created by user should match the input");


        }

        [TestMethod]
        public void CreateShortage_InvalidData()
        {
            var shortage = _shortageFactory.CreateShortage(
                "",
                "",
                Room.Kitchen,
                Category.Food,
                11,
                new User { Username = "Test User", Role = Role.Regular });

            Assert.IsNull(shortage, "Shortage should be null for invalid data");


        }



    }
}