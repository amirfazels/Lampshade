using _0_Framework.Application;
using DiscountManagement.Applicatiom.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            
            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            
            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);

            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);

            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            
            colleagueDiscount.Remove();

            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
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
