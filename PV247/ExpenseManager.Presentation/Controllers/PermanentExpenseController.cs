using System.Collections.Generic;
using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Enums;
using ExpenseManager.Business.Facades;
using ExpenseManager.Presentation.Authentication;
using ExpenseManager.Presentation.Models.PermanentExpense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing permanent expenses
    /// </summary>
    [Authorize]
    [Authorize(Policy = "HasAccount")]
    public class PermanentExpenseController : BaseController
    {

        private readonly BalanceFacade _balanceFacade;


        /// <summary>
        /// Constructor for ExpenseController
        /// </summary>
        /// <param name="balanceFacade"></param>
        /// <param name="mapper"></param>
        /// <param name="currentAccountProvider"></param>
        public PermanentExpenseController(BalanceFacade balanceFacade, Mapper mapper, ICurrentAccountProvider currentAccountProvider) : base(currentAccountProvider, mapper)
        {
            _balanceFacade = balanceFacade;
        }

        /// <summary>
        /// Displays permanent expenses
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "HasAccount")]
        public IActionResult Index()
        {
            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);
            var currentUserModel = Mapper.Map<Models.User.IndexViewModel>(CurrentAccountProvider.GetCurrentUser(HttpContext.User));

            var model = new PermanentExpensesIndexViewModel()
            {
                Expenses = GetAllPermanentExpenses(account),
                CurrentUser = currentUserModel
            };

            return View(model);
        }

        /// <summary>
        /// Displays form for creating permanent expenses
        /// </summary>
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Create([FromQuery] string errorMessage = null)
        {
            var model = new CreatePermanentExpenseViewModel
            {
                CostTypes = GetAllCostTypes(),
                ErrorMessage = errorMessage
            };
            return View(model);
        }

        /// <summary>
        /// Stores permanent expense
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HasFullRights")]
        public IActionResult Store(CreatePermanentExpenseViewModel costInfoViewModel)
        {
            var costType = _balanceFacade.GetItemType(costInfoViewModel.TypeId);

            if (!ModelState.IsValid || costType == null)
            {
                return RedirectToAction("Create", new { errorMessage = ExpenseManagerResource.InvalidInputData });
            }

            var costInfo = Mapper.Map<CostInfo>(costInfoViewModel);

            var account = CurrentAccountProvider.GetCurrentAccount(HttpContext.User);

            costInfo.AccountId = account.Id;

            _balanceFacade.CreateItem(costInfo);

            return RedirectToAction("Index", new { successMessage = ExpenseManagerResource.ExpenseCreated });
        }

        private List<Models.CostType.IndexViewModel> GetAllCostTypes()
        {
            var costTypes = _balanceFacade.ListItemTypes(null, null);
            var costTypeViewModels = Mapper.Map<List<Models.CostType.IndexViewModel>>(costTypes);
            return costTypeViewModels;
        }

        private List<IndexPermanentExpenseViewModel> GetAllPermanentExpenses(Account account)
        {
            var expenses = _balanceFacade.ListItems(account.Id, Periodicity.Day, null);
            expenses.AddRange(_balanceFacade.ListItems(account.Id, Periodicity.Week, null));
            expenses.AddRange(_balanceFacade.ListItems(account.Id, Periodicity.Month, null));
            return Mapper.Map<List<IndexPermanentExpenseViewModel>>(expenses);
        }
    }
}