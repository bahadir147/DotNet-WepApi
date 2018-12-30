using BahadirDemir.WebApiDemo.Entities;
using BahadirDemir.WebApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahadirDemir.WebApiDemo.DataAccess
{
    public class EfProductDal : EfEntitiyRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductModel> GetProductWithDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductModel
                             {
                                 CategoryName = c.CategoryName,
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 UnitPrice = p.UnitPrice
                             };
                return result.ToList();
            }
        }
    }
}
