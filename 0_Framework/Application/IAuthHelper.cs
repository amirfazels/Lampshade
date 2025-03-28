namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void Signin(AuthViewModel account);
        string CurrentAccountRole();
        long CurrentAccountId();
        string CurrentAccountMobile();
        AuthViewModel CurrentAccountInfo();
        List<int> GetPermissions();
    }
}
