using _0_Framework.Application;

namespace DiscountManagement.Applicatiom.Contract.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        EditCustomerDiscount GetDetails(long id);
        ICollection<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
