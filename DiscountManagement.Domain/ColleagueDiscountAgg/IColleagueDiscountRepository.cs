using _0_Framework.Domain;
using DiscountManagement.Applicatiom.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        ICollection<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
