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
            var list = await db.Products.Include(p => p.Category).ToListAsync();
            var productList = mapper.Map<List<ProductVM>>(list);
            return productList;
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
    }


}
//public async Task<IActionResult> Create(ProductVM model)
//{
//    try
//    {
//        var product = mapper.Map<Product>(model);

//        if (model.Image != null)
//        {
//            string fileName = UploadImage(model.Image);
//            //product.ImageUrl = fileName;
//        }

//        await db.Products.AddAsync(product);
//        await db.SaveChangesAsync();
//        return new OkResult();
//    }
//    catch (Exception)
//    {
//        return new BadRequestResult();
//    }
//}
//public async Task<IList<Product>> GetAllProducts()
//{
//    return await db.Products.ToListAsync();  //.Include(p => p.Category)
//}
//private string UploadImage(IFormFile image)
//{
//    string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
//    string uploadPath = Path.Combine(en.WebRootPath, "Images", fileName);

//    using (var stream = new FileStream(uploadPath, FileMode.Create))
//    {
//        image.CopyTo(stream);
//    }

//    return fileName;
//}