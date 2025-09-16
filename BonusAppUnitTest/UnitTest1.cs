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
                Name = "M�lk",
                Value = 10.0,
                AvailableFrom = new DateTime(2024, 3, 1),
                AvailableTo = new DateTime(2024, 3, 5)
            });
            order.AddProduct(new Product
            {
                Name = "Sm�r",
                Value = 15.0,
                AvailableFrom = new DateTime(2024, 3, 3),
                AvailableTo = new DateTime(2024, 3, 4)
            });
            order.AddProduct(new Product
            {
                Name = "P�l�g",
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
            Assert.AreEqual(2.0, result1, "Forventet bonus p� 2.0 for bel�b 10.0");
            Assert.AreEqual(0.0, result2, "Forventet bonus p� 0.0 for bel�b 4.0");
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

            Assert.AreEqual("Sm�r", result[0].Name);

            Assert.AreEqual("M�lk", result[1].Name);

            Assert.AreEqual("P�l�g", result[2].Name);

        }

        [TestMethod]
        public void SortProductOrderByName_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Name);
            string[] expectedOrder = { "M�lk", "P�l�g", "Sm�r" };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].Name, $"Forkert r�kkef�lge ved sortering efter Name p� indeks {i}");
            }
        }

        [TestMethod]
        public void SortProductOrderByValue_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Value);
            double[] expectedValues = { 10.0, 15.0, 20.0 };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedValues[i], result[i].Value, $"Forkert r�kkef�lge ved sortering efter Value p� indeks {i}");
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
                Assert.AreEqual(expectedDates[i], result[i].AvailableFrom, $"Forkert r�kkef�lge ved sortering efter AvailableFrom p� indeks {i}");
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
                Assert.AreEqual(expectedDates[i], result[i].AvailableTo, $"Forkert r�kkef�lge ved sortering efter AvailableTo p� indeks {i}");
            }
        }
    }
}