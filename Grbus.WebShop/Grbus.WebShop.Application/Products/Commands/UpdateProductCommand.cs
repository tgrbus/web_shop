using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Products.Commands
{
    public record UpdateProductCommand : ProductDto, IRequest<Result>
    {
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductRepository productRepo,
            IUnitOfWork unitOfWork
            ) 
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateProductCommand command, CancellationToken ct)
        {
            var product = await _productRepo.GetByIdAsync(command.Id);

            if(product == null)
            {
                return Result.Failure(ApplicationErrors.RecordDoesNotExistForGivenKey);
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.TaxPercentage = command.TaxPercentage;
            product.SKU = command.SKU;
            
            product.SetStockQuantity(command.StockQuantity ?? 0);

            _productRepo.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
