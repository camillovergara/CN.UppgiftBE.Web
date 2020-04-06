using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository.DB;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Service
{
    public interface IProductService
    {
        List<ProductDB> GetProducts();
        int RemoveAllProducts();
        void AddProduct(List<EntityModels.Product> productList);
    }
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory _dbContextFactory;
        public ProductService(IDbContextFactory DbContextFactory)
        {
            _dbContextFactory = DbContextFactory;
        }
        public void AddProduct(List<EntityModels.Product> productList)
        {
            using (var db = _dbContextFactory.CreateDbProduct())
            {
                foreach (var productItem in productList)
                {
                    db.Products
                   .Value(p => p.Id, productItem.Id)
                   .Value(p => p.Name, productItem.Name)
                   .Value(p => p.Price, productItem.Price)
                   .Insert();
                }

            }
          }
        public List<ProductDB> GetProducts()
        {
            using (var db = _dbContextFactory.CreateDbProduct())
            {
                var query = from p in db.Products 
                            orderby p.Name descending
                            select p;
                var list = query.ToList();
                return list;
            }
        }
        public int RemoveAllProducts()
        {
            using (var db = _dbContextFactory.CreateDbProduct())
            {
                return  db.Products.Delete();
            }
            
        }
    }
}