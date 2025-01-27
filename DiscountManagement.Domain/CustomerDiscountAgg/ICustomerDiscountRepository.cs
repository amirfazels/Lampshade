using _0_Framework.Domain;
using DiscountManagement.Applicatiom.Contract.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        ICollection<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
