using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class ProductRepository: Repository<Product>
    {
        //public List<Cart> GetCartById(string id)
        //{
        //    return this.context.Carts.Where(x => x.CustomerId == id).ToList();
        //}

        public List<Product> GetProductByCateg(int id)
        {
            return this.context.Products.Where(y => y.CategoryId == id).ToList();
        }

        public List<Product> GetProductByPrice(int top)
        {
            return this.context.Products.OrderByDescending(i => i.Selling_Price).Take(top).ToList();
        }





        public List<ProductViewModelDemo> GetProductWithCategory()
        {
            var pro = from p in context.Products
                      join c in context.Categories
                      on p.CategoryId equals c.CategoryId
                      select new ProductViewModelDemo()
                      {
                          Buying_Price = p.Buying_Price,
                          CategoryId = p.CategoryId,
                          Description = p.Description,
                          CategoryName = c.CategoryName,
                          Picture = p.Picture,
                          ProductId = p.ProductId,
                          ProductName = p.ProductName,
                          Quantity = p.Quantity,
                          Selling_Price = p.Selling_Price
                      };


            return pro.ToList();
        }

        public ProductViewModelDemo GetProductByProdIdLinq(int id)
        {
            var pro = from p in context.Products
                      join c in context.Categories
                      on p.CategoryId equals c.CategoryId
                      select new ProductViewModelDemo()
                      {
                          Buying_Price = p.Buying_Price,
                          CategoryId = p.CategoryId,
                          Description = p.Description,
                          CategoryName = c.CategoryName,
                          Picture = p.Picture,
                          ProductId = p.ProductId,
                          ProductName = p.ProductName,
                          Quantity = p.Quantity,
                          Selling_Price = p.Selling_Price
                      };


            return pro.Where(p => p.ProductId == id).FirstOrDefault();
        }
        public List<ProductViewModelDemo> GetProductByPriceLinq(int top)
        {
            var pro = from p in context.Products
                      join c in context.Categories
                      on p.CategoryId equals c.CategoryId
                      select new ProductViewModelDemo()
                      {
                          Buying_Price = p.Buying_Price,
                          CategoryId = p.CategoryId,
                          Description = p.Description,
                          CategoryName = c.CategoryName,
                          Picture = p.Picture,
                          ProductId = p.ProductId,
                          ProductName = p.ProductName,
                          Quantity = p.Quantity,
                          Selling_Price = p.Selling_Price
                      };


            return pro.OrderByDescending(j => j.Selling_Price).Take(top).ToList();
        }


    }
}