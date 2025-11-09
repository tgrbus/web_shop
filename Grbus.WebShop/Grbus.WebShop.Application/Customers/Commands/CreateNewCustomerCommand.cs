using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Customers.DTOs;
using Grbus.WebShop.Domain.Aggregates.Customers;
using Grbus.WebShop.Domain.Aggregates.Customers.Repository;
using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Grbus.WebShop.Application.Customers.Commands
{
    public record CreateNewCustomerCommand : CustomerDto, IRequest<Result>
    {
    }

    public class CreateNewCustomerCommandHandler : IRequestHandler<CreateNewCustomerCommand, Result>
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ILogger<CreateNewCustomerCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewCustomerCommandHandler(
            ICustomerRepository customerRepo, 
            IUnitOfWork unitOfWork,
            ILogger<CreateNewCustomerCommandHandler> logger)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateNewCustomerCommand command, CancellationToken ct)
        {
            try
            {
                var customer = new Customer
                {
                    Email = command.Email,
                    FirstName = command.FirstName,
                    LastName = command.LastName
                };
                await _customerRepo.InsertAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.Failure(ApplicationErrors.DatabaseException);
            }
        }
    }
}
