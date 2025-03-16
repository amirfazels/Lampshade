using _0_Framework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public class Permission : EntityBase
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public Permission(int code)
        {
            Code = code;
        }

        public Permission(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
