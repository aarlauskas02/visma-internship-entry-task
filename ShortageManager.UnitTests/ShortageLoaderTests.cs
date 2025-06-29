namespace ShortageManager.UnitTests
{
    [TestClass]
    public sealed class ShortageLoaderTests
    {
        private ShortageLoader _shortageLoader;

        [TestMethod]
        public void LoadShortages_ValidFilePath()
        {
            _shortageLoader = new ShortageLoader("shortagedata.json");
            var shortages = _shortageLoader.shortages;
            Shortage shortage1 = new Shortage
            {
                Title = "TestTitle",
                Name = "TestName",
                Room = Room.Meeting_room,
                Category = Category.Electronics,
                Priority = 10,
                CreatedOn = DateTime.Parse("2025-06-28T14:31:04.7617256+02:00"),
                CreatedBy = new User { Username = "testAdmin", Role = Role.Admin }
            };
            Shortage shortage2 = new Shortage
            {
                Title = "TestTitle2",
                Name = "TestName2",
                Room = Room.Meeting_room,
                Category = Category.Electronics,
                Priority = 5,
                CreatedOn = DateTime.Parse("2025-06-28T15:04:55.3364114+02:00"),
                CreatedBy = new User { Username = "testAdmin", Role = Role.Admin }
            };

            Assert.IsNotNull(shortages, "Shortages should not be null for valid file path");
            Assert.AreEqual(2, shortages.Count, "Shortage count should be 2 for valid file path");
            Assert.AreEqual(shortage1.Title, shortages[0].Title, "First shortage title does not match expected value");
            Assert.AreEqual(shortage1.Name, shortages[0].Name, "First shortage name does not match expected value");
            Assert.AreEqual(shortage1.Room, shortages[0].Room, "First shortage room does not match expected value");
            Assert.AreEqual(shortage1.Category, shortages[0].Category, "First shortage category does not match expected value");
            Assert.AreEqual(shortage1.Priority, shortages[0].Priority, "First shortage priority does not match expected value");
            Assert.AreEqual(shortage1.CreatedOn, shortages[0].CreatedOn, "First shortage created date does not match expected value");
            Assert.AreEqual(shortage1.CreatedBy.Username, shortages[0].CreatedBy.Username, "First shortage created by username does not match expected value");
            Assert.AreEqual(shortage2.Title, shortages[1].Title, "Second shortage title does not match expected value");
            Assert.AreEqual(shortage2.Name, shortages[1].Name, "Second shortage name does not match expected value");
            Assert.AreEqual(shortage2.Room, shortages[1].Room, "Second shortage room does not match expected value");
            Assert.AreEqual(shortage2.Category, shortages[1].Category, "Second shortage category does not match expected value");
            Assert.AreEqual(shortage2.Priority, shortages[1].Priority, "Second shortage priority does not match expected value");
            Assert.AreEqual(shortage2.CreatedOn, shortages[1].CreatedOn, "Second shortage created date does not match expected value");
            Assert.AreEqual(shortage2.CreatedBy.Username, shortages[1].CreatedBy.Username, "Second shortage created by username does not match expected value");

        }

        [TestMethod]
        public void LoadShortages_InvalidFilePath()
        {
            _shortageLoader = new ShortageLoader("wrong.json");
            var shortages = _shortageLoader.shortages;
            Assert.IsNotNull(shortages, "Shortages should not be null for invalid file path");
            Assert.AreEqual(0, shortages.Count, "Shortages count should be 0 for invalid file path");
        }

        [TestMethod]
        public void LoadShortages_EmptyFile()
        {
            _shortageLoader = new ShortageLoader("empty.json");
            var shortages = _shortageLoader.shortages;
            Assert.IsNotNull(shortages, "Shortages should not be null for empty file");
            Assert.AreEqual(0, shortages.Count, "Shortages count should be 0 for empty file");

        }
        [TestMethod]
        public void LoadShortages_EmptyList()
        {
            _shortageLoader = new ShortageLoader("emptylist.json");
            var shortages = _shortageLoader.shortages;
            Assert.IsNotNull(shortages, "Shortages should not be null for empty list file");
            Assert.AreEqual(0, shortages.Count, "Shortages count should be 0 for empty list file");

        }
    }
}
