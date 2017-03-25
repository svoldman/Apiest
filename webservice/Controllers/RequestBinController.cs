using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Mvc;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;
using Apiest.Features.RequestBin.Infrastructure.Data;
using Apiest.Features.RequestBin.Infrastructure.Data.Repositories;
using Apiest.Features.RequestBin.Services.Services;
using webservice.App_Start;

namespace webservice.Controllers
{
    public class RequestBinController : Controller
    {
        private readonly IInfrastructureDataContext _infrastructreDataContext;
        private IEnumerable<WebRequest> _webRequests;
        private IEnumerable<WebRequestGroup> _webRequestGroups;
     

        public RequestBinController()
        {
            _infrastructreDataContext = new InfrastructureDataContext();
        }

        public RequestBinController(IInfrastructureDataContext dataContext)
        {
            _infrastructreDataContext = dataContext;
        }

        public ActionResult NewRequestBin(string groupName)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "navRequestBinCreate";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);

                if (!string.IsNullOrEmpty(groupName) && webRequestService.GetGroup(groupName) == null)
                {
                    webRequestService.CreateNewGroup(groupName);
                }
                ViewBag.ActiveMenuItem = "navRequestBinCreate";
                _webRequestGroups = webRequestService.Groups;
            });

            return View(_webRequestGroups);
        }

        // GET: RequestBin
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Index(string groupName)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "navRequestBinList";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);

                _webRequestGroups = webRequestService.GetAllGroups();

            });

            return View(_webRequestGroups);
        }

        public ActionResult AllRequests(string SearchByQueryString)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "navRequestBinAllRequests";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);
                if (Request.IsAjaxRequest() && !string.IsNullOrEmpty(SearchByQueryString))
                {
                    _webRequests = webRequestService.FilterGroup(
                        p =>
                            p.QueryString.StartsWith(SearchByQueryString));
                }
                else
                {
                    _webRequests = webRequestService.GetAllRequests();
                }
            });

            if (Request.IsAjaxRequest() && !string.IsNullOrEmpty(SearchByQueryString))
            {
                return PartialView("_GroupRequests", _webRequests);
            }

            return View(_webRequests);
        }

        public ActionResult DeleteRequestBin(WebRequestGroup webRequestGroup)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "NewRequestBin";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);
                webRequestService.DeleteGroup(webRequestGroup);
            });

            return RedirectToAction("NewRequestBin");
        }

        [HttpGet]
        public ActionResult ModifyGroup(WebRequestGroup webRequestGroup)
        {
            ViewBag.ActiveMenuItem = "navRequestBinCreate";
            return View(webRequestGroup);
        }

        [HttpPost]
        [ActionName("ModifyGroup")]
        public ActionResult ModifyGroupUpdate(WebRequestGroup group)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "NewRequestBin";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);
                webRequestService.UpdateGroup(group);

            });

            return RedirectToAction("NewRequestBin");
        }


        public ActionResult RequestsGroup(WebRequestGroup webRequestGroup)
        {
            ViewBag.ActiveMenuItem = "navRequestBinList";
            return View(webRequestGroup);
        }

        public ActionResult ResetGroup(WebRequestGroup webRequestGroup)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "NewRequestBin";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);
                var group = webRequestService.GetGroup(webRequestGroup.GroupUniqueId);
                if (group != null)
                    webRequestService.DeleteGroupRequests(group.Id);
            });

            return RedirectToAction("Index");
        }

        public ActionResult GroupRequests(int? groupId)
        {
            _infrastructreDataContext.InitializeRequestBinUnitOfWork(unitOfWork =>
            {
                ViewBag.ActiveMenuItem = "NewRequestBin";
                IRequestBinService webRequestService = new RequestBinService(unitOfWork);
                if (groupId.HasValue)
                    _webRequests = webRequestService.GetGroupRequests(groupId.Value);
                else
                    _webRequests = webRequestService.GetAllGroupRequests();

            });

            return PartialView(_webRequests);
        }
    }

    
}