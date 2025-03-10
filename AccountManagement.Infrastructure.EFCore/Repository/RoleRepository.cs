using _0_Framework.Application;
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
            var query = _roleContext.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.ToList();
        }
    }
}
