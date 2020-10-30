using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interview.Entities;
using Interview.Service.Interface;

namespace Interview.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductDataService _productDataService;

        public ProductService(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public ServiceResult<Product> CreateProduct(Product product)
        {

            var result = _productDataService.CreateProduct(product);

            if (result == null)
            {
                return new ServiceResult<Product>() { Message = "Product couldn't be created", Status = "Error", Data = product };
            }

            return new ServiceResult<Product>() { Data = product, Status = "Success", Message = "Product successfully created." };
        }

        public void DeleteProduct(int id)
        {
            if(_productDataService.GetById(id)!=null)
            { 
            _productDataService.DeleteProduct(id);
            }
        }


        public List<Product> GetProducts()
        {
            return _productDataService.GetProducts();
        }

        public ServiceResult<Product> ProductById(int id)
        {
            var result = _productDataService.GetById(id);
            return new ServiceResult<Product>();
        }

        public ServiceResult<Product> UpdateProduct(Product product)
        {
            var result = _productDataService.UpdateProduct(product);

            if (result == null)
            {
                return new ServiceResult<Product>() { Message = "Product couldn't be updated", Status = "Error", Data = product };
            }

            return new ServiceResult<Product>() { Data = product, Status = "Success", Message = "Product successfully updated." };
        }
    }
}
