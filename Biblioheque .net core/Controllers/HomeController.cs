using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioheque_.net_core.Models;
using NeatLib.Attributs.Transaction;

namespace Biblioheque_.net_core.Controllers
{

    [TransactionName("AD40")]
    [TranactionStepList(typeof(TransactionStepSetter))]
    [TransactionValidator(typeof(int))]
    public class HomeController : Controller
    {

        [TransactionStepName("Index")]
        public IActionResult Index()
        {
            var i = this.HttpContext.Session.GetTransactionSession<int>(this.RouteData);

            return View();
        }
        public IActionResult test(int a)
        {
            return View();
        }


        [TransactionStepName("About")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        [TransactionStepName("Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
