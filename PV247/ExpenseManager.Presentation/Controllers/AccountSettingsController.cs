using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.AccountSettingsViewModel;
using ExpenseManager.Presentation.Models.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndexViewModel = ExpenseManager.Presentation.Models.User.IndexViewModel;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for displaying account settings
    /// </summary>
    [Authorize]
    public class AccountSettingsController : BaseController
    {
        private readonly BalanceFacade _balanceFacade;

        private readonly AccountFacade _accountFacade;

        /// <summary>
        /// Constructor for AccountSettingsController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="currentAccountProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="accountFacade"></param>
        public AccountSettingsController(
            BalanceFacade balanceFacade, 
            ICurrentAccountProvider currentAccountProvider, 
            Mapper mapper, 
            AccountFacade accountFacade) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
            _accountFacade = accountFacade;
        }

        /// <summary>
        /// Displays account settings
        /// </summary>
        [Authorize(Policy = "HasAccount")]
        public IActionResult Index()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);
            var currentUserModel = Mapper.Map<IndexViewModel>(CurrentAccountProvider.GetCurrentUser(HttpContext.User));

            var model = new AddAccessViewModel()
            {
                UsersWithAccess = GetAllUsersWithAccess(account),
                CurrentUser = currentUserModel
            };

            return View(model);
        }

        private List<IndexViewModel> GetAllUsersWithAccess(Account account)
        {
          
            var users = _accountFacade.ListUsers(account.Id, null, null, null);

            return Mapper.Map<List<IndexViewModel>>(users);
        }

        /// <summary>
        /// Adds access to new user for this account
        /// </summary>
        [Authorize(Policy = "HasAccount")]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult AddAccessToAccount(AddAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectWithError(ExpenseManagerResource.InvalidInputData);
            }

            var user = GetUserFromEmail(model.Email);
            if (user == null)
            {
                return RedirectWithError(ExpenseManagerResource.EmailDoesntExist);
            }
            if (user.AccountId != Guid.Empty) // todo this might not be best check
            {
                return RedirectWithError(ExpenseManagerResource.AlreadyHasAccount);
            }

            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            _accountFacade.AttachAccountToUser(user.Id, account.Id, model.AccessType);

            return RedirectToAction("Index", new { successMessage = ExpenseManagerResource.AccessGranted });
        }

        private User GetUserFromEmail(string email)
        {
            var users = _accountFacade.ListUsers(null, null, email,null);
            return users.FirstOrDefault();
        }

        /// <summary>
        /// Displays view for user which doesn't have account yet
        /// </summary>
        public IActionResult NoAccount()
        {
            return View();
        }

        /// <summary>
        /// Creates currently logged-in user new account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StoreAccount()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            if (account != null)
            {
                return RedirectWithError(ExpenseManagerResource.YouHaveAccount);
            }

            var user = CurrentAccountProvider.GetCurrentUser(HttpContext.User);
            _accountFacade.CreateAccount(user.Id);

            return RedirectToAction("Index", "Expense", new { sucessMessage = ExpenseManagerResource.AccountCreated });
        }
    }
}