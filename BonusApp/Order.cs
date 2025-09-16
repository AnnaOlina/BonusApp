using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusApp
{
    public class Order
    {
        public BonusProvider Bonus { get; set; }
        private List<Product> _products = new List<Product>();
        
        public List<Product> Products
        {
            get { return _products; }
        }

        public void AddProduct(Product p)
        {
            _products.Add(p);
        }

        public double GetValueOfProducts(DateTime date)
        {
            //double valueOfProducts = 0;
            //foreach (Product product in _products)
            //{
            //    valueOfProducts += product.Value;
            //}
            //return valueOfProducts;

            //return _products.Sum(p => p.Value);

            //return _products
            //    .Where(p => p.AvailableFrom <= date && p.AvailableTo >= date)
            //    .Sum(p => p.Value);

            return _products
                .Sum(p => (p.AvailableFrom <= date && p.AvailableTo >= date) ? p.Value : 0);
        }

        // Oprindelig GetBonus-metode, som bruger BonusProvider
        public double GetBonus()
        {
            if (Bonus == null)
                throw new InvalidOperationException("Bonus delegate is not set.");
            return Bonus(GetValueOfProducts());
        }

        public decimal GetBonus(DateTime date, Func<decimal, decimal> bonusCalculation)
        {
            decimal totalValue = _products
                .Where(p => p.AvailableFrom <= date && p.AvailableTo >= date)
                .Sum(p => (decimal)p.Value); // Kast Value til decimal

            return bonusCalculation(totalValue);
        }

        public decimal GetTotalPrice(DateTime date, Func<decimal, decimal> bonusCalculation)
        {
            decimal totalValue = _products
                .Where(p => p.AvailableFrom <= date && p.AvailableTo >= date)
                .Sum(p => (decimal)p.Value); // Kast Value til decimal

            decimal bonus = bonusCalculation(totalValue);
            return totalValue - bonus; // Bonus trækkes fra
        }

        public double GetValueOfProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> SortProductOrderByAvailableTo()
        {
            return Products
                .OrderBy(p => p.AvailableTo)  // sortér efter AvailableTo
                .ToList();                    // lav resultatet til en liste
        }

        public List<Product> SortProductOrderBy(Func<Product, object> keySelector)
        {
            return _products.OrderBy(keySelector).ToList();
        }
    }
}
