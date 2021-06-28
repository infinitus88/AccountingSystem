using AccountingSystem.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Filters
{
    public class UnitOfWorkFilterAttribute : ActionFilterAttribute
    {
        private readonly IActionTransactionHelper _helper;

        public UnitOfWorkFilterAttribute(IActionTransactionHelper helper)
        {
            _helper = helper;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _helper.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _helper.EndTransaction(actionExecutedContext);
            _helper.CloseSession();
        }
    }
}
