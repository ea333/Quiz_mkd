using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using NuGet.Versioning;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Domain_Transfer;
using Quiz.Repository.Implementation;
using Quiz.Repository.Interface;
using Quiz.Web.Domain_Transfer.Interface;
using System.Net.WebSockets;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class RangListController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IApplicationUserRepository _applicationUserRepository;

        private readonly IRangListDetailsGeneral _rangListDetailGeneral;

        public RangListController(
            IUnitOfWork unitOfWork,
            IApplicationUserRepository applicationUserRepository,
            IRangListDetailsGeneral rangListDetailGeneral)
        {
            _unitOfWork = unitOfWork;
            _applicationUserRepository = applicationUserRepository;
            _rangListDetailGeneral = rangListDetailGeneral;
        }


        public IActionResult Index()
        {

            var rangsListToBeSelected = _unitOfWork.RangList_User.GetAll(u => u.Points != null, includeProperties: "RangList");

            var eventsToBeSelected = rangsListToBeSelected.Select(u => u.RangList.EventId);

            ShowRangListOptionsVM showRangListOpetionsVM = new ShowRangListOptionsVM
            {
                Events = _unitOfWork.Event.GetAll(u=> eventsToBeSelected.Contains(u.Id)).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Categories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.categoryName,
                    Value = u.Id.ToString()
                }),
                EventsByYear = _unitOfWork.Event.GetAll().Select(u => u.StartDate.Year).Distinct().OrderBy(u => u)
                .Select(u => new SelectListItem
                {
                    Text = u.ToString(),
                    Value = u.ToString()
                }),
                ShowRangList = null,
            };

            return View(showRangListOpetionsVM);
        }


        [HttpPost]
        public IActionResult FilterRangList(ShowRangListOptionsVM showRangListOptionsVM)
        {
            var selectedEventId = showRangListOptionsVM.SelectedEventId;
            var selectedCategoryId = showRangListOptionsVM.SelectedCategoryId;
            var selectedYear = showRangListOptionsVM.SelectedYear;

            Event _event = null;
            RangList rangList = null;

            List<RangList_User> rangListUsers = new List<RangList_User>();
            List<Category_RangList> categoryRangList = new List<Category_RangList>();
            List<Category_User> categoryUsersForView = new List<Category_User>();


            //all of the fields are not selected 
            if (selectedEventId == null && selectedCategoryId == null && selectedYear == null)
            {
                return RedirectToAction("Index", "RangList");
            }

            //only the event field is selected
            if (selectedEventId != null && selectedCategoryId == null && selectedYear == null)
            {

               RangListDetails temp = _rangListDetailGeneral.EventFieldSelectedOnly(selectedEventId);
                rangListUsers = temp.rangListUsers;
                categoryRangList =temp.categoryRangList;
                categoryUsersForView = temp.categoryUsersForView;
            }

            //only the category field is selected
            if (selectedEventId == null && selectedCategoryId != null && selectedYear == null)
            {
                RangListDetails temp = _rangListDetailGeneral.CategoryFieldSelectedOnly(selectedCategoryId);
                rangListUsers = temp.rangListUsers;
                categoryRangList = temp.categoryRangList;
                categoryUsersForView = temp.categoryUsersForView;
            }

            //only the year field is selected 
            if (selectedEventId == null && selectedCategoryId == null && selectedYear != null)
            {
                int year = int.Parse(selectedYear);
                RangListDetails temp = _rangListDetailGeneral.YearFieldSelectedOnly(year);
                rangListUsers = temp.rangListUsers;
                categoryRangList = temp.categoryRangList;
                categoryUsersForView = temp.categoryUsersForView;


            }
            //the event & category filds are selected 
            if (selectedEventId != null && selectedCategoryId != null && selectedYear == null)
            {
                RangListDetails temp = _rangListDetailGeneral.EventAndCategoryFieldSelected(selectedEventId,selectedCategoryId);
                rangListUsers = temp.rangListUsers;
                categoryRangList = temp.categoryRangList;
                categoryUsersForView = temp.categoryUsersForView;

            }
            //the event & year fields are selected 
            if (selectedEventId != null && selectedCategoryId == null && selectedYear != null)
            {
                int year = int.Parse(selectedYear);
                RangListDetails temp = _rangListDetailGeneral.EventAndYearFieldSelected(selectedEventId, year);
                if (temp != null)
                {
                    rangListUsers = temp.rangListUsers;
                    categoryRangList = temp.categoryRangList;
                    categoryUsersForView = temp.categoryUsersForView;
                }

            }

            //the category & year are selected
            if (selectedEventId == null && selectedCategoryId != null && selectedYear != null)
            {

                int year = int.Parse(selectedYear);
                RangListDetails temp = _rangListDetailGeneral.CategoryAndYearFieldSelected(selectedCategoryId, year);
                rangListUsers = temp.rangListUsers;
                categoryRangList = temp.categoryRangList;
                categoryUsersForView = temp.categoryUsersForView;

            }

            //all of the fields are selected
            if (selectedEventId != null && selectedCategoryId != null && selectedYear != null) 
            {
                int year = int.Parse(selectedYear);
                RangListDetails temp = _rangListDetailGeneral.AllOfTheFiledsSelected(selectedEventId, selectedCategoryId,year);
                if (temp != null)
                {
                    rangListUsers = temp.rangListUsers;
                    categoryRangList = temp.categoryRangList;
                    categoryUsersForView = temp.categoryUsersForView;
                }
            }


            RangListVM rangListVM = new RangListVM()
            {
                RangListUsers = rangListUsers,
                CategoriesRangList = categoryRangList,
                CategoryUsers = categoryUsersForView,
                Event = _event
            };



            var rangsListToBeSelected = _unitOfWork.RangList_User.GetAll(u => u.Points != null, includeProperties: "RangList");

            var eventsToBeSelected = rangsListToBeSelected.Select(u => u.RangList.EventId);


            ShowRangListOptionsVM showRangListOptionsVMfinal = new ShowRangListOptionsVM
            {

                Events = _unitOfWork.Event.GetAll(u=> eventsToBeSelected.Contains(u.Id)).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Categories = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.categoryName,
                    Value = u.Id.ToString()
                }),
                EventsByYear = _unitOfWork.Event.GetAll().Select(u => u.StartDate.Year).Distinct().OrderBy(u => u)
               .Select(u => new SelectListItem
               {
                   Text = u.ToString(),
                   Value = u.ToString()
               }),
                ShowRangList = rangListVM,
                SelectedEventId = selectedEventId,
                SelectedCategoryId = selectedCategoryId,
                SelectedYear =  selectedYear

            };


            return View(showRangListOptionsVMfinal);
        }

    }
}
