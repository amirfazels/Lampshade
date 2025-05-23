﻿using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Account> Account { get; private set; }
        public List<Permission> Permissions { get; set; }

        protected Role() { }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Account = new List<Account>();
            Permissions = permissions;
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
