﻿using _0_Framework.Application;
using DiscountManagement.Applicatiom.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(
                command.ProductId, 
                command.DiscountRate, 
                startDate, 
                endDate, 
                command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);

            if (customerDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_customerDiscountRepository
                .Exists(x => x.Id != command.Id && x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(
                command.ProductId,
                command.DiscountRate,
                startDate,
                endDate,
                command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public ICollection<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
