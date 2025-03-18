namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public AuthViewModel(long id, string username, string fullname, long roleId, List<int> permissions)
        {
            Id = id;
            Username = username;
            Fullname = fullname;
            RoleId = roleId;
            Permissions = permissions;
        }
        public AuthViewModel() { }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public List<int> Permissions { get; set; }
    }
}