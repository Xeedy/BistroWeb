using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class ProductAppService : IProductAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;

        public ProductAppService(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }
        public Product GetProductById(int id)
        {
            return _eshopDbContext.Products.Find(id);
        }

        public IList<Product> Select()
        {
            return _eshopDbContext.Products.ToList();
        }

        public async Task Create(Product product)
        {
            string imageSrc = await _fileUploadService.FileUploadAsync(product.Image, Path.Combine("img", "products"));
            product.ImageSrc = imageSrc;

            if (_eshopDbContext.Products != null)
            {
                _eshopDbContext.Products.Add(product);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Product? product
                = _eshopDbContext.Products.FirstOrDefault(prod => prod.Id == id);

            if (product != null)
            {
                _eshopDbContext.Products.Remove(product);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }
        public async Task Edit(Product editedProduct)
        {
            Product existingProduct = _eshopDbContext.Products.Find(editedProduct.Id);

            if (existingProduct != null)
            {
                // Update the properties of the existing product with the edited values
                existingProduct.Name = editedProduct.Name;
                existingProduct.Description = editedProduct.Description;
                existingProduct.Price = editedProduct.Price;
                existingProduct.Brewery = editedProduct.Brewery;
                // ... (update other properties as needed)

                // If a new image is provided, upload and update the image source
                if (editedProduct.Image != null)
                {
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedProduct.Image, Path.Combine("img", "products"));
                    existingProduct.ImageSrc = newImageSrc;
                }

                // Save the changes to the database
                _eshopDbContext.SaveChanges();
            }
        }
    }
}
