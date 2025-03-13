namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SingOut();
        void SignIn(AuthViewModel account);
    }
}
