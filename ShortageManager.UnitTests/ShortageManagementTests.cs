namespace ShortageManager.UnitTests
{
    [TestClass]
    public sealed class ShortageManagementTests
    {
        private User user1;
        private User user2;
        private DateTime date;
        private DateTime date2;
        private Shortage shortage1;
        private Shortage shortage2;
        private Shortage shortage3;
        private ShortageManagement _shortageManager;
        public ShortageManagementTests()
        {
            _shortageManager = new ShortageManagement("management.json");
            DateTime.TryParse("2025-06-06", out date);
            DateTime.TryParse("2025-06-05", out date2);
            user1 = new User { Username = "Test User", Role = Role.Regular };
            user2 = new User { Username = "Test User2", Role = Role.Admin };
            shortage1 = new Shortage
            {
                Title = "TestAdd",
                Name = "TestName",
                Room = Room.Meeting_room,
                Category = Category.Food,
                Priority = 5,
                CreatedOn = date,
                CreatedBy = user1
            };

            shortage2 = new Shortage
            {
                Title = "TestAdd2",
                Name = "TestName",
                Room = Room.Kitchen,
                Category = Category.Other,
                Priority = 5,
                CreatedOn = date2,
                CreatedBy = user2
            };
            shortage3 = new Shortage
            {
                Title = "TestAdd2",
                Name = "TestName",
                Room = Room.Kitchen,
                Category = Category.Other,
                Priority = 9,
                CreatedOn = date2,
                CreatedBy = user2
            };
        }



        [TestMethod]
        public void AddShortage_ValidRegular()
        {


            _shortageManager.AddShortage(shortage1, user1);
            Assert.IsTrue(_shortageManager.shortages.Any(s =>
                s.Title == shortage1.Title &&
                s.Name == shortage1.Name &&
                s.Room == shortage1.Room &&
                s.Category == shortage1.Category &&
                s.Priority == shortage1.Priority &&
                s.CreatedBy.Username == shortage1.CreatedBy.Username &&
                s.CreatedBy.Role == shortage1.CreatedBy.Role &&
                s.CreatedOn == shortage1.CreatedOn
            ), "Shortage manager should contain the new regular shortage");


        }
        [TestMethod]
        public void AddShortage_ValidAdmin()
        {

            _shortageManager.AddShortage(shortage2, user2);
            Assert.IsTrue(_shortageManager.shortages.Any(s =>
                s.Title == shortage2.Title &&
                s.Name == shortage2.Name &&
                s.Room == shortage2.Room &&
                s.Category == shortage2.Category &&
                s.Priority == shortage2.Priority &&
                s.CreatedBy.Username == shortage2.CreatedBy.Username &&
                s.CreatedBy.Role == shortage2.CreatedBy.Role &&
                s.CreatedOn == shortage2.CreatedOn
            ), "Shortage manager should contain the new admin shortage");


        }
        [TestMethod]
        public void AddShortage_NoDuplicates()
        {
            _shortageManager.AddShortage(shortage2, user2);
            Assert.IsTrue(_shortageManager.shortages.Any(s =>
                s.Title == shortage2.Title &&
                s.Name == shortage2.Name &&
                s.Room == shortage2.Room &&
                s.Category == shortage2.Category &&
                s.Priority == shortage2.Priority &&
                s.CreatedBy.Username == shortage2.CreatedBy.Username &&
                s.CreatedBy.Role == shortage2.CreatedBy.Role &&
                s.CreatedOn == shortage2.CreatedOn
            ), "Shortage manager should not contain duplicates");


        }
        [TestMethod]
        public void AddShortage_LowerPriority()
        {
            shortage2 = new Shortage
            {
                Title = "TestAdd2",
                Name = "TestName",
                Room = Room.Kitchen,
                Category = Category.Other,
                Priority = 4,
                CreatedOn = date2,
                CreatedBy = user2
            };

            _shortageManager.AddShortage(shortage2, user2);
            Assert.IsFalse(_shortageManager.shortages.Any(s =>
                s.Title == shortage2.Title &&
                s.Name == shortage2.Name &&
                s.Room == shortage2.Room &&
                s.Category == shortage2.Category &&
                s.Priority == shortage2.Priority &&
                s.CreatedBy.Username == shortage2.CreatedBy.Username &&
                s.CreatedBy.Role == shortage2.CreatedBy.Role &&
                s.CreatedOn == shortage2.CreatedOn
            ), "Shortage manager should contain the same shortage with the higher priority");


        }
        [TestMethod]
        public void AddShortage_UpdateShortage()
        {



            _shortageManager.AddShortage(shortage3, user2);
            Assert.IsTrue(_shortageManager.shortages.Any(s =>
                s.Title == shortage3.Title &&
                s.Name == shortage3.Name &&
                s.Room == shortage3.Room &&
                s.Category == shortage3.Category &&
                s.Priority == shortage3.Priority &&
                s.CreatedBy.Username == shortage3.CreatedBy.Username &&
                s.CreatedBy.Role == shortage3.CreatedBy.Role &&
                s.CreatedOn == shortage3.CreatedOn
            ), "Shortage manager should contain the updated admin shortage");


        }
        [TestMethod]
        public void RemoveShortage_ValidRegular()
        {
            _shortageManager.RemoveShortageByTitleAndRoom("TestAdd", Room.Meeting_room, user1);
            Assert.IsFalse(_shortageManager.shortages.Any(s =>
                s.Title == shortage1.Title &&
                s.Name == shortage1.Name &&
                s.Room == shortage1.Room &&
                s.Category == shortage1.Category &&
                s.Priority == shortage1.Priority &&
                s.CreatedBy.Username == shortage1.CreatedBy.Username &&
                s.CreatedBy.Role == shortage1.CreatedBy.Role &&
                s.CreatedOn == shortage1.CreatedOn
            ), "Shortage manager should not contain the regular shortage");

        }
        [TestMethod]
        public void RemoveShortage_InvalidRegular()
        {

            _shortageManager.RemoveShortageByTitleAndRoom("TestAdd2", Room.Kitchen, user1);
            Assert.IsTrue(_shortageManager.shortages.Any(s =>
                s.Title == shortage3.Title &&
                s.Name == shortage3.Name &&
                s.Room == shortage3.Room &&
                s.Category == shortage3.Category &&
                s.Priority == shortage3.Priority &&
                s.CreatedBy.Username == shortage3.CreatedBy.Username &&
                s.CreatedBy.Role == shortage3.CreatedBy.Role &&
                s.CreatedOn == shortage3.CreatedOn
            ), "Shortage manager should contain the admin shortage");
        }
        [TestMethod]
        public void RemoveShortage_ValidAdmin()
        {
            _shortageManager.RemoveShortageByTitleAndRoom("TestAdd2", Room.Kitchen, user2);
            Assert.IsFalse(_shortageManager.shortages.Any(s =>
                s.Title == shortage3.Title &&
                s.Name == shortage3.Name &&
                s.Room == shortage3.Room &&
                s.Category == shortage3.Category &&
                s.Priority == shortage3.Priority &&
                s.CreatedBy.Username == shortage3.CreatedBy.Username &&
                s.CreatedBy.Role == shortage3.CreatedBy.Role &&
                s.CreatedOn == shortage3.CreatedOn
            ), "Shortage manager should not contain the admin shortage");
        }
        [TestMethod]
        public void EmptyCheck_Empty()
        {

            Assert.IsTrue(_shortageManager.CheckIfEmpty());
        }
        [TestMethod]
        public void ClearFilters_Exists()
        {
            _shortageManager.AddShortage(shortage1, user1);
            _shortageManager.AddShortage(shortage2, user2);
            _shortageManager.ClearFilters();
            Assert.AreEqual(_shortageManager.filteredShortages.Count(), 0);

        }

        [TestMethod]
        public void FilterByTitle_Exists()
        {
            _shortageManager.AddShortage(shortage1, user1);
            _shortageManager.AddShortage(shortage2, user2);
            _shortageManager.FilterByTitle("2");
            Assert.AreEqual(_shortageManager.filteredShortages.Count(), 1);

            Assert.IsTrue(_shortageManager.filteredShortages.Any(s =>
            s.Title == shortage2.Title &&
            s.Name == shortage2.Name &&
            s.Room == shortage2.Room &&
            s.Category == shortage2.Category &&
            s.Priority == shortage2.Priority &&
            s.CreatedBy.Username == shortage2.CreatedBy.Username &&
            s.CreatedBy.Role == shortage2.CreatedBy.Role &&
            s.CreatedOn == shortage2.CreatedOn
            ), "Filter should only show shortages with 2 in the title");
            _shortageManager.ClearFilters();
        }
        [TestMethod]
        public void FilterByCreatedOn_Exists()
        {
            _shortageManager.FilterByCreatedOn("2025-06-04 2025-06-05");
            Assert.AreEqual(_shortageManager.filteredShortages.Count(), 1);

            Assert.IsTrue(_shortageManager.filteredShortages.Any(s =>
            s.Title == shortage2.Title &&
            s.Name == shortage2.Name &&
            s.Room == shortage2.Room &&
            s.Category == shortage2.Category &&
            s.Priority == shortage2.Priority &&
            s.CreatedBy.Username == shortage2.CreatedBy.Username &&
            s.CreatedBy.Role == shortage2.CreatedBy.Role &&
            s.CreatedOn == shortage2.CreatedOn
            ), "Filter should only show shortages made between 2025-06-04 and 2025-06-05");
            _shortageManager.ClearFilters();

        }
        [TestMethod]
        public void FilterByCategory_Exists()
        {
            _shortageManager.FilterByCategory("Other");
            Assert.AreEqual(_shortageManager.filteredShortages.Count(), 1);

            Assert.IsTrue(_shortageManager.filteredShortages.Any(s =>
            s.Title == shortage2.Title &&
            s.Name == shortage2.Name &&
            s.Room == shortage2.Room &&
            s.Category == shortage2.Category &&
            s.Priority == shortage2.Priority &&
            s.CreatedBy.Username == shortage2.CreatedBy.Username &&
            s.CreatedBy.Role == shortage2.CreatedBy.Role &&
            s.CreatedOn == shortage2.CreatedOn
            ), "Filter should only show shortages with Other as the category");
            _shortageManager.ClearFilters();
        }
        [TestMethod]
        public void FilterByRoom_Exists()
        {
            _shortageManager.FilterByRoom("Meeting_room");
            Assert.AreEqual(_shortageManager.filteredShortages.Count(), 1);

            Assert.IsTrue(_shortageManager.filteredShortages.Any(s =>
            s.Title == shortage1.Title &&
            s.Name == shortage1.Name &&
            s.Room == shortage1.Room &&
            s.Category == shortage1.Category &&
            s.Priority == shortage1.Priority &&
            s.CreatedBy.Username == shortage1.CreatedBy.Username &&
            s.CreatedBy.Role == shortage1.CreatedBy.Role &&
            s.CreatedOn == shortage1.CreatedOn
            ), "Filter should only show shortages with Meeting_room as the room");
            _shortageManager.ClearFilters();
            _shortageManager.RemoveShortageByTitleAndRoom("TestAdd", Room.Meeting_room, user1);
            _shortageManager.RemoveShortageByTitleAndRoom("TestAdd2", Room.Kitchen, user2);

        }
    }
}