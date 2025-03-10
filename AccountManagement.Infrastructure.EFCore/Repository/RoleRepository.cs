using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        public AccountContext _roleContext;

        public RoleRepository(AccountContext roleContext) : base(roleContext)
        {
            _roleContext = roleContext;
        }

        public EditRole GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<RoleViewModel> Search(RoleSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
