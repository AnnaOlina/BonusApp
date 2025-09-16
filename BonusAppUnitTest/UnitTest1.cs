using BonusApp;

namespace BonusAppUnitTest
{

    [TestClass]
    public class UnitTest1
    {
        Order order;

        [TestInitialize]
        public void InitializeTest()  // This method runs before each test. 
        {
            // Arrange
            order = new Order();  // Create a new Order instance before each test
            order.AddProduct(new Product
            {
                Name = "Mælk",
                Value = 10.0,
                AvailableFrom = new DateTime(2024, 3, 1),
                AvailableTo = new DateTime(2024, 3, 5)
            });
            order.AddProduct(new Product
            {
                Name = "Smør",
                Value = 15.0,
                AvailableFrom = new DateTime(2024, 3, 3),
                AvailableTo = new DateTime(2024, 3, 4)
            });
            order.AddProduct(new Product
            {
                Name = "Pålæg",
                Value = 20.0,
                AvailableFrom = new DateTime(2024, 3, 4),
                AvailableTo = new DateTime(2024, 3, 7)
            });
        }
        [TestMethod]
        public void TenPercent_Test()
        {
            Assert.AreEqual(4.5, Bonuses.TenPercent(45.0));
            Assert.AreEqual(40.0, Bonuses.TenPercent(400.0));
        }
        [TestMethod]
        public void FlatTwoIfAMountMoreThanFive_Test()
        {
            //Assert.AreEqual(2.0, Bonuses.FlatTwoIfAmountMoreThanFive(10.0));
            //Assert.AreEqual(0.0, Bonuses.FlatTwoIfAmountMoreThanFive(4.0));

            // Act
            double result1 = Bonuses.FlatTwoIfAmountMoreThanFive(10.0);
            double result2 = Bonuses.FlatTwoIfAmountMoreThanFive(4.0);

            // Assert
            Assert.AreEqual(2.0, result1, "Forventet bonus på 2.0 for beløb 10.0");
            Assert.AreEqual(0.0, result2, "Forventet bonus på 0.0 for beløb 4.0");
        }
        [TestMethod]
        public void GetValueOfProducts_Test()
        {
            Assert.AreEqual(45.0, order.GetValueOfProducts());
        }
        [TestMethod]
        public void GetBonus_Test()
        {
            order.Bonus = Bonuses.TenPercent;
            Assert.AreEqual(4.5, order.GetBonus());

            order.Bonus = Bonuses.FlatTwoIfAmountMoreThanFive;
            Assert.AreEqual(2.0, order.GetBonus());
        }
        
        [TestMethod]
        public void GetTotalPriceByDate_Test()
        {
            Assert.AreEqual(0.0, order.GetTotalPrice(new DateTime(2024, 2, 28), x => x * 0.2m));
            Assert.AreEqual(8.0, order.GetTotalPrice(new DateTime(2024, 3, 2), x => x * 0.2m));
            Assert.AreEqual(20.0, order.GetTotalPrice(new DateTime(2024, 3, 3), x => x * 0.2m));
            Assert.AreEqual(36.0, order.GetTotalPrice(new DateTime(2024, 3, 4), x => x * 0.2m));
        }

        [TestMethod]

        public void GetValueOfProductsByDate_Test()

        {

            Assert.AreEqual(0.0, order.GetValueOfProducts(new DateTime(2024, 2, 28)));

            Assert.AreEqual(10.0, order.GetValueOfProducts(new DateTime(2024, 3, 2)));

            Assert.AreEqual(25.0, order.GetValueOfProducts(new DateTime(2024, 3, 3)));

            Assert.AreEqual(45.0, order.GetValueOfProducts(new DateTime(2024, 3, 4)));

        }

        [TestMethod]

        public void SortByAvailableToTest()

        {
            // Act
            List<Product> result = order.SortProductOrderByAvailableTo();

            // Assert
            Assert.AreEqual(3, result.Count);  

            Assert.AreEqual("Smør", result[0].Name);

            Assert.AreEqual("Mælk", result[1].Name);

            Assert.AreEqual("Pålæg", result[2].Name);

        }

        [TestMethod]
        public void SortProductOrderByName_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Name);
            string[] expectedOrder = { "Mælk", "Pålæg", "Smør" };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].Name, $"Forkert rækkefølge ved sortering efter Name på indeks {i}");
            }
        }

        [TestMethod]
        public void SortProductOrderByValue_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Value);
            double[] expectedValues = { 10.0, 15.0, 20.0 };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedValues[i], result[i].Value, $"Forkert rækkefølge ved sortering efter Value på indeks {i}");
            }
        }

        [TestMethod]
        public void SortProductOrderByAvailableFrom_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.AvailableFrom);
            DateTime[] expectedDates =
            {
                new DateTime(2024, 3, 1),
                new DateTime(2024, 3, 3),
                new DateTime(2024, 3, 4)
            };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedDates[i], result[i].AvailableFrom, $"Forkert rækkefølge ved sortering efter AvailableFrom på indeks {i}");
            }
        }

        [TestMethod]
        public void SortProductOrderByAvailableTo_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.AvailableTo);
            DateTime[] expectedDates =
            {
                new DateTime(2024, 3, 4),
                new DateTime(2024, 3, 5),
                new DateTime(2024, 3, 7)
            };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedDates[i], result[i].AvailableTo, $"Forkert rækkefølge ved sortering efter AvailableTo på indeks {i}");
            }
        }
    }
}