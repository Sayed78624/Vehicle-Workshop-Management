using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VehicleWorkShop.Data;
using VehicleWorkShop.Models;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Utilities;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Service.Repository
{
    public class ProductRP : IProduct
    {
        private readonly WorkShopDbContext db;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment en;
        public ProductRP(WorkShopDbContext db, IMapper mapper, IWebHostEnvironment en)
        {
            this.db = db;
            this.mapper = mapper;
            this.en = en;
        }

        public async Task<List<ProductVM>> GetAll()
        {
            var list = await (from p in db.Products
                              join c in db.Categories on p.CategoryId equals c.CategoryId
                              join m in db.VehicleModels on p.ModelId equals m.ModelId
                              select new ProductVM
                              {
                                  ProductId = p.ProductId,
                                  ProductName = p.ProductName,
                                  PartNo = p.PartNo,
                                  Description = p.Description,
                                  Price = p.Price,
                                  CategoryId = p.CategoryId,
                                  ModelId = p.ModelId,
                                  CategoryName = c.Name,
                                  ModelName = m.ModelName,
                                  ImageName = p.ImageName
                              }).ToListAsync();

            return list;
        }





        public async Task<IActionResult> Create(ProductVM model)
        {
            try
            {
                Product product = new Product()
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    PartNo = model.PartNo,
                    Description = model.Description ?? "",
                    CategoryId = model.CategoryId,
                    ModelId = model.ModelId,
                };

                if (model.Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    var filePath = Path.Combine(en.WebRootPath, "Images", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    product.ImageName = uniqueFileName;
                }
                else
                {
                    product.ImageName = "default_image.png";
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return new BadRequestObjectResult(error);
            }
        }



        public async Task<IList<Product>> GetAllProducts()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var products = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if(products != null)
            {
                if (!string.IsNullOrEmpty(products.ImageName) && products.ImageName != "default_image.png")
                {
                    var filePath = Path.Combine(en.WebRootPath, "Images", products.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                db.Products.Remove(products);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();
        }
    }


}
