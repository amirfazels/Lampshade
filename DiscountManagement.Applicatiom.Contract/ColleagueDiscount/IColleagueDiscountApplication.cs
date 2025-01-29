using _0_Framework.Application;

namespace DiscountManagement.Applicatiom.Contract.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditColleagueDiscount GetDetails(long id);
        ICollection<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
