using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        public AccountContext _accountContext;
        public AccountRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }
        public EditAccount GetDetails(long id)
        {
            return _accountContext.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                FullName = x.FullName,
                Username = x.Username,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _accountContext.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Username = x.Username,
                Mobile = x.Mobile,
                Role = "مدیر سیستم",
                RoleId = 2,
                ProfilePhoto = x.ProfilePhoto,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RoleId > 0)
                query = query.Where(x => searchModel.RoleId == x.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
