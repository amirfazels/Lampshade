using System.Reflection;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Admin = "1";
        public const string User = "2";
        public const string ContentUploader = "3";
        public const string Colleague = "4";

        public static string? GetRoleName(long value)
        {
            return typeof(Roles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(f => f.GetValue(null)?.ToString() == value.ToString())
                ?.Name;
        }
    }
}
