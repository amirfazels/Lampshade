using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditCreate command);
        EditCreate GetDetails(long id);
        List<RoleViewModel> Search(RoleSearchModel searchModel);
    }
}
