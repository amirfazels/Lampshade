﻿using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IFileUploader _fileUploader;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);

            _accountRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");
            var password = _passwordHasher.Hash(command.Password);

            var account = new Account
                (
                    command.FullName, 
                    command.Username,
                    password, 
                    command.Mobile, 
                    command.RoleId,
                    path
                );

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");

            account.Edit(
                command.FullName,
                command.Username,
                command.Mobile,
                command.RoleId,
                path
                );

            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetByUsername(command.Username);

            if (account == null)
                return operation.Failed(ApplicationMessages.UserOrPassNotMatch);

            var result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
                return operation.Failed(ApplicationMessages.UserOrPassNotMatch);

            var permissions = _roleRepository
                .Get(account.RoleId)
                .Permissions
                .Select(x => x.Code)
                .ToList();

            var authViewModel = new AuthViewModel
                (
                    account.Id,
                    account.Username,
                    account.FullName,
                    account.RoleId,
                    permissions
                );

            _authHelper.Signin(authViewModel);

            return operation.Succedded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }
}
