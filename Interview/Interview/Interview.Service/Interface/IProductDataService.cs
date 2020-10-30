using System;
using System.Collections.Generic;
using System.Text;
using Interview.Entities;

namespace Interview.Service.Interface
{
    public interface IProductDataService
    {
        Product CreateProduct(Product product);

        Product UpdateProduct(Product product);

        void DeleteProduct(int id);

        List<Product> GetProducts();

        Product GetById(int id);

    }
}
