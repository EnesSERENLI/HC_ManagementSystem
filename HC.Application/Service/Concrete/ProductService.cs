using AutoMapper;
using HC.Application.Extensions;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);
            
            var result = await _unitOfWork.ProductRepository.Any(x=>x.ProductName == product.ProductName);
            if (result)
            {
                return "This Product already exists";
            }

            ImageUploader imageUploader = new ImageUploader();
            var imageResult = imageUploader.IsValid(model.Image);
            if (imageResult)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(200, 200));
                image.Save("wwwroot/Content/images/products/" + product.ProductName + imageUploader.GetExtension());
                product.ImagePath = ("/Content/images/products/" + product.ProductName + imageUploader.GetExtension());

                await _unitOfWork.ProductRepository.Add(product);

                return "Product added!";
            }


            return "Please choise a image!";
        }

        public async Task<string> Delete(Guid id)
        {
            await _unitOfWork.ProductRepository.Delete(id);

            return "Product deleted!";
        }

        public async Task<UpdateProductDTO> GetById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefault(selector: x => new Product
            {
                ID = x.ID,
                ProductName = x.ProductName,
                Description = x.Description,
                ImagePath = x.ImagePath,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                SubCategoryId = x.SubCategoryId,
                CreatedIP = x.CreatedIP,
                CreatedDate = x.CreatedDate,
                CreatedComputerName = x.CreatedComputerName,
                UpdatedComputerName = x.UpdatedComputerName,
                UpdatedIP = x.UpdatedIP,
                UpdatedDate = x.UpdatedDate,
                DeletedIP = x.DeletedIP,
                DeletedDate = x.DeletedDate,
                DeletedComputerName = x.DeletedComputerName
            },
            expression: x=> x.ID == id);

            var model = _mapper.Map<UpdateProductDTO>(product);

            return model;
        }

        public async Task<List<ProductVM>> GetDefaultProducts()
        {
            var productList = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefaults(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ImagePath = x.ImagePath,
                Status = x.Status,
                SubCategoryName = x.SubCategory.SubCategoryName
            },
            expression: x=>x.Status != Domain.Enums.Status.Deleted);

            return productList;
        }

        public async Task<List<ProductVM>> GetProducts()
        {
            var productList = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefaults(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ImagePath = x.ImagePath,
                SubCategoryName = x.SubCategory.SubCategoryName,
                Status = x.Status
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated || x.Status == Domain.Enums.Status.Deleted);

            return productList;
        }

        public async Task<string> Update(UpdateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            ImageUploader imageUploader = new ImageUploader();
            var imageResult = imageUploader.IsValid(model.Image);
            if (imageResult)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(150, 150));
                image.Save("wwwroot/Content/images/products/" + product.ProductName + imageUploader.GetExtension());
                product.ImagePath = ("/Content/images/products/" + product.ProductName + imageUploader.GetExtension());
            }

            await _unitOfWork.ProductRepository.Update(product);

            return "Product updated!";
        }
    }
}
