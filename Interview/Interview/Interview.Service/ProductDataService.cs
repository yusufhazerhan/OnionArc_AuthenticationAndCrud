using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interview.Entities;
using Interview.Service.Interface;

namespace Interview.Service
{
    public class ProductDataService : IProductDataService
    {
        public Product CreateProduct(Product product)
        {
            using var dbContext = new DataContext();
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Products.Update(product);
                dbContext.SaveChanges();
                return product;
            }
        }

        public void DeleteProduct(int id)
        {
            using (var dbContext = new DataContext())
            {
                var deleteProduct = GetById(id);
                dbContext.Products.Remove(deleteProduct);
                dbContext.SaveChanges();
            }
        }

        public List<Product> GetProducts()
        {
            using (var dbContext = new DataContext())
            {
                return dbContext.Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            using(var dbContext= new DataContext())
            {
                return dbContext.Products.Find(id);
            }
        }
    }
}
