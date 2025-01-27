using _0_Framework.Application;
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
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new NotImplementedException();
        }

        public EditCustomerDiscount GetDetails()
        {
            throw new NotImplementedException();
        }

        public ICollection<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
