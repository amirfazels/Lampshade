using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        public AccountContext _roleContext;

        public RoleRepository(AccountContext roleContext) : base(roleContext)
        {
            _roleContext = roleContext;
        }

        public EditRole? GetDetails(long id)
        {
            return _roleContext.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions = MapPermissions(x.Permissions),
                Permissions = x.Permissions.Select(x => x.Code).ToList()

            })
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto
            (
                x.Code,
                x.Name
            )).ToList();
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
