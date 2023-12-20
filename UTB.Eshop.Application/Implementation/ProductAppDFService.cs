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
    public class ProductAppDFService : IProductAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;
        public Product GetProductById(int id)
        {
            return _eshopDbContext.Products.Find(id);
        }
        public ProductAppDFService(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public IList<Product> Select()
        {
            return DatabaseFake.Products;
        }

        public async Task Create(Product product)
        {
            if(DatabaseFake.Products != null
                && DatabaseFake.Products.Count > 0)
            {
                product.Id = DatabaseFake.Products.Last().Id + 1;
            }
            else
                product.Id = 1;

            string imageSrc = await _fileUploadService.FileUploadAsync(product.Image, Path.Combine("img", "products"));
            product.ImageSrc = imageSrc;

            if (DatabaseFake.Products != null)
                DatabaseFake.Products.Add(product);
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Product? product
                = DatabaseFake.Products.FirstOrDefault(prod => prod.Id == id);

            if (product != null)
            {
                deleted = DatabaseFake.Products.Remove(product);
            }
            return deleted;
        }
        public async Task Edit(Product editedProduct)
        {
            Product existingProduct = _eshopDbContext.Products.Find(editedProduct.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = editedProduct.Name;
                existingProduct.Description = editedProduct.Description;
                existingProduct.Price = editedProduct.Price;
                existingProduct.Brewery = editedProduct.Brewery;
                // ... (update other properties as needed)

                if (editedProduct.Image != null)
                {
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedProduct.Image, Path.Combine("img", "products"));
                    existingProduct.ImageSrc = newImageSrc;
                }

                _eshopDbContext.SaveChanges();
            }
        }
    }
}
