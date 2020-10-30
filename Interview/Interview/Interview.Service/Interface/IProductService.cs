using System;
using System.Collections.Generic;
using System.Text;
using Interview.Entities;

namespace Interview.Service.Interface
{
    public interface IProductService
    {
        ServiceResult<Product> CreateProduct(Product product);
        ServiceResult<Product> UpdateProduct(Product product);
        void DeleteProduct(int id);
        ServiceResult<Product> ProductById(int id);
        List<Product> GetProducts();

    }
}
