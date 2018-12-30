using BahadirDemir.WebApiDemo.Entities;
using BahadirDemir.WebApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahadirDemir.WebApiDemo.DataAccess
{
    public interface IProductDal : IEntitiyRepository<Product>
    {
        List<ProductModel> GetProductWithDetails();
    }
}
