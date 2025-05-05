public class ProductRP : IProduct
{
    private readonly ApplicationDbContext _context;

    public ProductRP(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Create(ProductVM productVM)
    {
        var product = new Product
        {
            ProductName = productVM.ProductName,
            PartNo = productVM.PartNo,
            Description = productVM.Description,
            Price = productVM.Price,
            CategoryId = productVM.CategoryId,
            Image = "default.jpg" // Handle image upload separately
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return new OkResult();
    }

    // Other methods...
}
