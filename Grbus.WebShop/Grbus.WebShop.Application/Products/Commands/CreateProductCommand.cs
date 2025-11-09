using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Grbus.WebShop.Application.Products.Commands
{
    public record CreateProductCommand : ProductDto, IRequest<Result>
    {
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        IProductRepository _productRepo;
        IUnitOfWork _unitOfWork;
        ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            IProductRepository productRepo, 
            IUnitOfWork unitOfWork,
            ILogger<CreateProductCommandHandler> logger)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product(command.StockQuantity ?? 0)
            {
                Name = command.Name,
                SKU = command.SKU,
                Description = command.Description,
                Price = command.Price,
                TaxPercentage = command.TaxPercentage                
            };

            try
            {
                await _productRepo.InsertAsync(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product: {command}", command);
                return Result.Failure(ApplicationErrors.DatabaseException);
            }
        }
    }
}
