using _0_Framework.Application;
using DiscountManagement.Applicatiom.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();

            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            throw new NotImplementedException();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Remove(long id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Restore(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
