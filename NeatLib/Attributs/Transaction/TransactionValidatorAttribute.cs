using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NeatLib.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    /// <summary>
    /// Atribut permettant de gérer une transaction
    /// </summary>
    public class TransactionValidatorAttribute : ActionFilterAttribute
    {

        private Controller _controller;
        private ITempDataDictionary _tempData;
        private IDictionary<object,object> _items;
        private ISession _session;

        private string _actionName;
        private string _controllerName;
        private string _area ;
        private string _id ;
        private string _transactionName;
        private Type _transactionSessionType;

        public TransactionValidatorAttribute(Type transactionSessionType)
        {
            this._transactionSessionType = transactionSessionType;
            this.Order = 3;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            PreparerDonnees(context);
            if (!string.IsNullOrWhiteSpace(this._transactionName))
            {
                validerTransaction(context);
                ValiderEtapeTransaction(context);
            }
        }

        
        private void PreparerDonnees(ActionExecutingContext context)
        {
            this._controller = context.Controller as Controller;
            this._tempData = this._controller.TempData;
            this._items = context.HttpContext.Items;
            this._session = context.HttpContext.Session;
            this._actionName = (string)context.RouteData.Values["action"];
            this._controllerName = (string)context.RouteData.Values["controller"];
            this._area = (string)context.RouteData.Values["area"];
            this._id = (string)context.RouteData.Values["id"];
            this._transactionName = context.Filters.OfType<TransactionNameAttribute>().First().Name;
        }

        private void validerTransaction(ActionExecutingContext context)
        {
            var referer = (string)context.HttpContext.Request.Headers["referer"];
            //Si la transaction n'est pas démarer on la démare
            if (!context.RouteData.Values.ContainsKey("transaction") || !context.RouteData.Values.ContainsKey("guid"))
            {
                preparerTransaction(context);
            }
            // mauvaise transaction
            else if ((string)context.RouteData.Values["transaction"] != this._transactionName)
            {
                preparerTransaction(context);
            }
            //Transaction invalide ou session morte
            else if (!_session.Keys.Any(x => x == (string)context.RouteData.Values["guid"]))
            {
                //session expiré
                if(!this._session.Keys.Any())
                    this.
                preparerTransaction(context);
            }
            //f5 ou accès depuis un favory ou taper a bras
            else if ((string.IsNullOrWhiteSpace(referer) || !referer.Contains(context.HttpContext.Request.Host.Host))
                     && this._tempData["guid"] == null)
            {
                preparerTransaction(context);
            }
        }
        
        private void preparerTransaction(ActionExecutingContext context)
        {
            var guid = Guid.NewGuid().ToString();
            this._session.SetObjectAsJson(guid, Activator.CreateInstance(this._transactionSessionType));
            this._session.SetObjectAsJson(guid + this._transactionName + "LastStep", "");
            this._tempData["guid"] = guid;
            context.Result = new RedirectResult(PreparerURL(guid, this._transactionName, this._area,this._controllerName,this._actionName), true);
        }

        private void ValiderEtapeTransaction(ActionExecutingContext context)
        {
            var guid = (string)context.RouteData.Values["guid"] ?? (string)this._tempData.Peek("guid");
            var transactionStep = context.Filters.OfType<TransactionStepNameAttribute>().First().Step;
            var transactionStepList = context.Filters.OfType<TranactionStepListAttribute>().First().StepList;
            var last_step = this._session.GetObjectFromJson<string>(guid + this._transactionName + "LastStep");

            if (!string.IsNullOrWhiteSpace(last_step))
            {
                var lastId = transactionStepList.IndexOf(new TransactionStep() { Name = last_step });
                var currentId = transactionStepList.IndexOf(new TransactionStep() { Name = transactionStep });

                if (Math.Abs(lastId - currentId) > 1)
                {
                    var stepFirst = transactionStepList.First();
                    context.Result = new RedirectResult(PreparerURL(guid, this._transactionName, stepFirst.Area, stepFirst.ControllerName, transactionStepList.First().ActionName), true);
                    last_step = stepFirst.Name;
                }
                else
                {
                    last_step = transactionStep;
                }
            }
            else if (transactionStepList.First().Name != transactionStep)
            {
                var stepFirst = transactionStepList.First();
                context.Result = new RedirectResult(PreparerURL(guid, this._transactionName, stepFirst.Area, stepFirst.ControllerName, transactionStepList.First().ActionName), true);
                last_step = stepFirst.Name;
            }
            else
            {
                last_step = transactionStep;
            }
            this._session.SetObjectAsJson(guid + this._transactionName + "LastStep", last_step);
        }

        private string PreparerURL(string guid, string transactionName, string area, string controllerName, string actionName)
        {
            var url = string.IsNullOrWhiteSpace(this._id)
                                     ? string.Format("/{0}/{1}/{2}/{3}", transactionName, guid, controllerName, actionName)
                                     : string.Format("/{0}/{1}/{2}/{3}/{4}", transactionName, guid, controllerName, actionName, this._id);
            if (!string.IsNullOrWhiteSpace(area))
                url.Insert(0, string.Format("/{0}", this._area));
            return url;
        }
    }
}