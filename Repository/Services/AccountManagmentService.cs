using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;
namespace Repository.Services
{
    public class AccountManagmentService
    {
        private readonly IAccountRepository _accountRepository;


        public AccountManagmentService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> AddNewAccount(String name, String surname, String email, String password)
        {
            var newAccount = new Account()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password

            };
            var entity = await _accountRepository.Insert(newAccount);

            return newAccount;
        }
        public async Task<Account> AddNewAccount(String name, String surname, String email, String password, String phone, String Role)
        {
            var newAccount = new Account()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
                PhoneNumber = phone

            };
            var entity = await _accountRepository.Insert(newAccount);

            return newAccount;
        }

        public async void DeleteAccount(int id)
        {
            var tmp = await _accountRepository.GetAll();
            var primaryKeys = new List<int>();
            foreach (var item in tmp)
                primaryKeys.Add(item.IdAccount);

            if (primaryKeys.Contains(id))
            {
                await _accountRepository.Delete(id);



            }

        }
        //TODO tu może być coś nie tak
        public async Task<Account> UpdateAccount(int idAccount, String name, String surname, String email, String password, String phone, String role)
        {
            var updatedAccount = new Account()
            {
                IdAccount = idAccount,
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
                PhoneNumber = phone,
                Role = role
            };
            await _accountRepository.Update(updatedAccount);


            return updatedAccount;
        }

        public Task<List<Account>> GetAccounts()
        {
            return _accountRepository.GetAll();
        }
        public async Task<Account> GetAccountByEmailAndPassword(string email, string password)
        {
            var accounts = await _accountRepository.GetAll();
            var accountTmp = new Account();
            bool found = false;

            foreach (var account in accounts)
                if (account.Email == email && account.Password == password)
                {
                    accountTmp = account;
                    found = true;
                    break;
                }
            if (found)
                return accountTmp;
            else throw new Exception("Incorrect email or password");
        }
        public  bool isAccountWithSameEmail(string email)
        {
           
            var accounts = Task.Run(async () => await _accountRepository.GetAll());
            bool found = false;

            foreach (var account in accounts.Result)
                if (account.Email == email)
                {
                   
                    found = true;
                    break;
                }
            return found;
            
           
        }

        public async Task<Account> GetAccountByID(int id)
        {
            return await _accountRepository.GetById(id);
        }

        public async void UpdateAccountStatus(int id, string status)
        {
            var tmp = await _accountRepository.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdAccount);

            if (primaryKeys.Contains(id))
            {
                Account modifiedClient = await _accountRepository.UpdateStatus(id, status);
            }
        }

        public async Task<List<Account>> GetLowerAccounts()
        {
            return await _accountRepository.GetLowerAccounts();
        }
        public async Task<List<Account>> GetAccountsWithoutAdmin()
        {
            
            
            return await _accountRepository.GetAccountsWithoutAdmin();
        }
        public async Task<List<Account>> GetRolelessAccounts()
        {
            return await _accountRepository.GetRolelessAccounts();
        }
        public async Task<List<Account>> GetAccountsByFullName(string name)
        {

            var accounts = await _accountRepository.GetLowerAccounts();
            /*var accountTmp = new List<Account>();


            foreach (var account in accounts)
            {
                var temp = account.Name + " " + account.Surname;
                if (temp.ToUpperInvariant() == name.ToUpperInvariant())
                {
                    accountTmp.Add(account);


                }
            }
            return accountTmp;
*/
            string[] temp = name.Split(' ');
            if (temp != null)
            {
                if (temp.Length == 1)
                {
                    temp = new[] { temp[0], "" };
                }


                return accounts.Where(o => (o.Name.ToUpperInvariant().Contains(temp[0].ToUpperInvariant()) && o.Surname.ToUpperInvariant().Contains(temp[1].ToUpperInvariant())) || (o.Surname.ToUpperInvariant().Contains(temp[0].ToUpperInvariant()) && o.Name.ToUpperInvariant().Contains(temp[1].ToUpperInvariant()))).ToList();
            }
            else
                return accounts;
        




    }

        public async Task<List<Account>> GetAccountsByFullNameRoleless(string name)
        {

            var accounts = await _accountRepository.GetRolelessAccounts();
           
            string[] temp = name.Split(' ');
            if (temp != null)
            {
                if (temp.Length == 1)
                {
                    temp = new[] { temp[0], "" };
                }


                return accounts.Where(o => (o.Name.ToUpperInvariant().Contains(temp[0].ToUpperInvariant()) && o.Surname.ToUpperInvariant().Contains(temp[1].ToUpperInvariant())) || (o.Surname.ToUpperInvariant().Contains(temp[0].ToUpperInvariant()) && o.Name.ToUpperInvariant().Contains(temp[1].ToUpperInvariant()))).ToList();
            }
            else
                return accounts;





        }
    }

    
}
