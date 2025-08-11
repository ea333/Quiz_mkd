using Quiz.Domain.Domain_Models;
using Quiz.Repository.Domain_Transfer;
using Quiz.Repository.Implementation;
using Quiz.Repository.Interface;
using Quiz.Web.Domain_Transfer.Interface;

namespace Quiz.Web.Domain_Transfer.Implementation
{
    public class RangListDetailsGeneral : IRangListDetailsGeneral
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IApplicationUserRepository _applicationUserRepository;

       
        public RangListDetailsGeneral(IUnitOfWork unitOfWork, IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
        }

        public RangListDetails AllOfTheFiledsSelected(int? selectedEventId, int? selectedCategoryId, int selectedYear)
        {
           var  _event = _unitOfWork.Event.Get(u => u.Id == selectedEventId && u.StartDate.Year == selectedYear, includeProperties: "RangList");

            if(_event == null)
            {
                return null;
            }
            var rangList = _event.RangList;


           var rangListUsers = _unitOfWork.RangList_User
            .GetAll(u => u.RangListId == rangList.Id, includeProperties: "User")
            .OrderByDescending(u => (double)u.Points).ToList();

            var categoryRangList = _unitOfWork.Category_RangList
          .GetAll(u => u.RangListId == rangList.Id && u.CategoryId == selectedCategoryId, includeProperties: "Category").ToList();

            var categoryUsersForView = _unitOfWork.Category_User
                .GetAll(u => u.RangListId == rangList.Id).ToList();

            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }

        public RangListDetails CategoryAndYearFieldSelected(int? selectedCategoryId, int selectedYear)
        {
            var categoryRangList = _unitOfWork.Category_RangList
               .GetAll(u => u.CategoryId == selectedCategoryId, includeProperties: "Category")
               .ToList();

            var rangIdsList = categoryRangList.DistinctBy(u => u.RangListId).Select(u => u.RangListId).ToList();
            categoryRangList = categoryRangList.DistinctBy(u => u.CategoryId).ToList();

            List<int?> listEventThatAreInTheSelectedYear = new List<int?>();
            
            foreach (var i in rangIdsList)
            {
                var temp = _unitOfWork.RangList.Get(u => u.Id == i, includeProperties: "Event");
                if (temp.Event.StartDate.Year == selectedYear)
                {
                    listEventThatAreInTheSelectedYear.Add(i);
                }
            }
            rangIdsList = listEventThatAreInTheSelectedYear;

            var rangListUsers = _unitOfWork.RangList_User
                   .GetAll(u => rangIdsList.Contains(u.RangListId), includeProperties: "User")
                   .DistinctBy(u => u.UserId)
                   .ToList();


            var userIds = rangListUsers.DistinctBy(u => u.UserId).Select(u => u.UserId).ToList();

            var userTotalPointsPerCategory = _unitOfWork.UserTotalPointsPerCategory
                .GetAll(u => u.CategoryId == selectedCategoryId);

            List<Category_User> l = new List<Category_User>();
            foreach (var n in userTotalPointsPerCategory)
            {
                var obj = new Category_User
                {
                    UserId = n.UserId,
                    CategoryId = selectedCategoryId,
                    Points = n.Points,
                };
                l.Add(obj);
            }


            var orderedCategoryUsers = l.OrderByDescending(u => u.Points).ThenByDescending(u => u.UserId).ToList();

            rangListUsers = rangListUsers
                .Join(orderedCategoryUsers, r => r.UserId, c => c.UserId, (r, c) =>
                new {
                    RangList_User = r,
                    Category_User = c
                })
                .OrderByDescending(x => x.Category_User.Points)
                .ThenByDescending(x => x.RangList_User?.User?.Surname)
                .Select(x => x.RangList_User)
                .ToList();

            var categoryUsersForView = orderedCategoryUsers;

            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }

        public RangListDetails CategoryFieldSelectedOnly(int? selectedCategoryId)
        {
            var categoryRangList = _unitOfWork.Category_RangList
                .GetAll(u => u.CategoryId == selectedCategoryId, includeProperties: "Category")
                .ToList();

            var rangIdsList = categoryRangList.DistinctBy(u => u.RangListId).Select(u => u.RangListId).ToList();
            categoryRangList = categoryRangList.DistinctBy(u => u.CategoryId).ToList();

            var rangListUsers = _unitOfWork.RangList_User
                   .GetAll(u => rangIdsList.Contains(u.RangListId), includeProperties: "User")
                   .DistinctBy(u => u.UserId)
                   .ToList();


            var userIds = rangListUsers.DistinctBy(u => u.UserId).Select(u => u.UserId).ToList();

            var userTotalPointsPerCategory = _unitOfWork.UserTotalPointsPerCategory
                .GetAll(u => u.CategoryId == selectedCategoryId);

            List<Category_User> l = new List<Category_User>();
            foreach (var n in userTotalPointsPerCategory)
            {
                var obj = new Category_User
                {
                    UserId = n.UserId,
                    CategoryId = selectedCategoryId,
                    Points = n.Points,
                };
                l.Add(obj);
            }


            var orderedCategoryUsers = l.OrderByDescending(u => u.Points).ThenByDescending(u => u.UserId).ToList();

            rangListUsers = rangListUsers
                .Join(orderedCategoryUsers, r => r.UserId, c => c.UserId, (r, c) =>
                new {
                    RangList_User = r,
                    Category_User = c
                })
                .OrderByDescending(x => x.Category_User.Points)
                .ThenByDescending(x => x.RangList_User?.User?.Surname)
                .Select(x => x.RangList_User)
                .ToList();



            var categoryUsersForView = orderedCategoryUsers;

            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }

        public RangListDetails EventAndCategoryFieldSelected(int? selectedEventId, int? selectedCategoryId)
        {
            var _event = _unitOfWork.Event.Get(u => u.Id == selectedEventId, includeProperties: "RangList");

           var rangList = _event.RangList;

            var rangListUsers = _unitOfWork.RangList_User
            .GetAll(u => u.RangListId == rangList.Id, includeProperties: "User")
            .OrderByDescending(u => (double)u.Points).ToList();

            var categoryRangList = _unitOfWork.Category_RangList
          .GetAll(u => u.RangListId == rangList.Id && u.CategoryId == selectedCategoryId, includeProperties: "Category").ToList();

            var categoryUsersForView = _unitOfWork.Category_User
                .GetAll(u => u.RangListId == rangList.Id).ToList();

            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }

        public RangListDetails EventAndYearFieldSelected(int? selectedEventId, int selectedYear)
        {
            
            
            var _event = _unitOfWork.Event.Get(u => u.Id == selectedEventId && u.StartDate.Year == selectedYear, includeProperties: "RangList");
            
            if(_event == null)
            {
                //throw new ArgumentException("Event not found for the selected ID and year.");
                return null;
            }
           
            var rangList = _event.RangList;

            var rangListUsers = _unitOfWork.RangList_User
            .GetAll(u => u.RangListId == rangList.Id, includeProperties: "User")
            .OrderByDescending(u => (double)u.Points).ToList();

            var categoryRangList = _unitOfWork.Category_RangList
          .GetAll(u => u.RangListId == rangList.Id, includeProperties: "Category").ToList();

            var categoryUsersForView = _unitOfWork.Category_User
                .GetAll(u => u.RangListId == rangList.Id).ToList();

            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }

        public RangListDetails EventFieldSelectedOnly(int? selectedEventId)
        {
            
            var _event = _unitOfWork.Event.Get(u => u.Id == selectedEventId, includeProperties: "RangList");
            var  rangList = _event.RangList;

            var rangListUsers = _unitOfWork.RangList_User
            .GetAll(u => u.RangListId == rangList.Id, includeProperties: "User")
            .OrderByDescending(u => (double)u.Points).ToList();

            var categoryRangList = _unitOfWork.Category_RangList
          .GetAll(u => u.RangListId == rangList.Id, includeProperties: "Category").ToList();

            var categoryUsersForView = _unitOfWork.Category_User
                .GetAll(u => u.RangListId == rangList.Id).ToList();

            var result = new RangListDetails {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;

        }

        public RangListDetails YearFieldSelectedOnly(int selectedYear)
        {
            
            var _events = _unitOfWork.Event.GetAll(u => u.StartDate.Year == selectedYear, includeProperties: "RangList");
            List<RangList_User> listForRangListUsers = new List<RangList_User>();
            List<Category_RangList> listForCategoryRangList = new List<Category_RangList>();
            List<Category_User> listForCategoryUser = new List<Category_User>();

            List<RangList_User> tempRangListUsers = new List<RangList_User>();
            List<Category_RangList> tempCategoryRangList = new List<Category_RangList>();
            List<Category_User> tempCategoryUser = new List<Category_User>();

            foreach (var e in _events)
            {
                var rangList = e.RangList;

                tempRangListUsers = _unitOfWork.RangList_User
                .GetAll(u => u.RangListId == rangList.Id, includeProperties: "User").ToList();


                tempCategoryRangList = _unitOfWork.Category_RangList
                .GetAll(u => u.RangListId == rangList.Id, includeProperties: "Category").ToList();

                tempCategoryUser = _unitOfWork.Category_User
                .GetAll(u => u.RangListId == rangList.Id).ToList();


                listForRangListUsers.AddRange(tempRangListUsers);
                listForCategoryRangList.AddRange(tempCategoryRangList);
                listForCategoryUser.AddRange(tempCategoryUser);
            }
            var distinctUsers = listForRangListUsers.DistinctBy(u => u.UserId).ToList();
            var distinctCategories = listForCategoryRangList.DistinctBy(u => u.CategoryId).ToList();
            Dictionary<string, double?> userRangListPoints = new Dictionary<string, double?>();
            Dictionary<string, Dictionary<int?, double?>> userCategoryPoints = new Dictionary<string, Dictionary<int?, double?>>();

            foreach (var user in distinctUsers)
            {
                userRangListPoints.Add(user.UserId, 0.0);
                userCategoryPoints.Add(user.UserId, new Dictionary<int?, double?>());

                foreach (var category in distinctCategories)
                {
                    userCategoryPoints[user.UserId].Add(category.CategoryId, 0.0);
                }

            }


            foreach (var n in listForRangListUsers)
            {
                userRangListPoints[n.UserId] += n.Points;
            }
            userRangListPoints = userRangListPoints.OrderByDescending(u => u.Value).ToDictionary();



            foreach (var n in listForCategoryUser)
            {
                //if(userCategoryPoints[n.UserId][n.CategoryId])
                //userCategoryPoints[n.UserId][n.CategoryId] += n.Points;

                if (userCategoryPoints.ContainsKey(n.UserId) && userCategoryPoints[n.UserId].ContainsKey(n.CategoryId))
                {
                    userCategoryPoints[n.UserId][n.CategoryId] += n.Points;
                }
            }
            List<RangList_User> flag = new List<RangList_User>();
            foreach (var n in userRangListPoints)
            {
                RangList_User temp = new RangList_User
                {
                    UserId = n.Key,
                    User = _applicationUserRepository.GetById(n.Key),
                    Points = n.Value
                };
                flag.Add(temp);
            }
            var rangListUsers = flag;

            var categoryRangList = distinctCategories;

            List<Category_User> flag1 = new List<Category_User>();
            foreach (var n in userCategoryPoints)
            {
                foreach (var m in n.Value)
                {

                    Category_User temp = new Category_User
                    {
                        UserId = n.Key,
                        User = _applicationUserRepository.GetById(n.Key),
                        CategoryId = m.Key,
                        Category = _unitOfWork.Category.Get(u => u.Id == m.Key),
                        Points = m.Value
                    };
                    flag1.Add(temp);
                }

            }
            var categoryUsersForView = flag1;
            var result = new RangListDetails
            {
                rangListUsers = rangListUsers,
                categoryRangList = categoryRangList,
                categoryUsersForView = categoryUsersForView
            };
            return result;
        }
      


         
    }
}
