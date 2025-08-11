using Quiz.Repository.Data;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.Identity;
using System;
using System.Linq;

namespace Quiz.Web.Data
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // ===== Update existing event images =====
            var geo = context.Events.FirstOrDefault(e => e.Id == 1);
            if (geo != null) { geo.ImageUrl = "/Images/event/geo.png"; geo.Category = "Свет"; }

            var hist = context.Events.FirstOrDefault(e => e.Id == 2);
            if (hist != null) { hist.ImageUrl = "/Images/event/history.jpg"; geo.Category = "Историја"; }

            var geo2 = context.Events.FirstOrDefault(e => e.Id == 3);
            if (geo2 != null) { geo2.ImageUrl = "/Images/event/geo2.jpg"; geo.Category = "Свет"; }

            // ===== Ensure football event exists =====
            var rangEvent = context.Events.FirstOrDefault(e => e.Id == 4);
            if (rangEvent == null)
            {
                rangEvent = new Event
                {
                    Id = 4,
                    Name = "Светски првенства во фудбал",
                    Description = "Тестирајте го вашето знаење и вештини во областа на Светските првенства во фудбал, опфаќајќи ги најнезаборавните моменти, најдобрите играчи и највозбудливите натпревари од последните 40 години. Потсетете се на големите легенди, изненадувањата и рекордите што го обликуваа овој најпопуларен спорт во светот.",
                    StartDate = new DateTime(2025, 9, 2),
                    EndDate = new DateTime(2025, 9, 4),
                    Destination = "Скопје",
                    ImageUrl = "/Images/event/6c70da4d-6f24-4cdb-8922-71a0ed22253c.jpg"
                };
                context.Events.Add(rangEvent);
                context.SaveChanges();
            }

            // ===== Users to seed =====
            var seedUsers = new[]
            {
                new ApplicationUser
                {
                    UserName = "bariiseni42",
                    Email = "bariiseni@example.com",
                    NameUser = "Бари",
                    Surname = "Исени",
                    PlaceOfOrigin = "Скопје"
                },
                new ApplicationUser
                {
                    UserName = "ilija",
                    Email = "ilijastevkov@example.com",
                    NameUser = "Илија",
                    Surname = "Стевков",
                    PlaceOfOrigin = "Битола"
                },
                new ApplicationUser
                {
                    UserName = "stefanstosikj@example.com",
                    Email = "stefanstosikj@example.com",
                    NameUser = "Стефан",
                    Surname = "Стошиќ",
                    PlaceOfOrigin = "Тетово"
                }
            };

            // Upsert users (insert or update)
            foreach (var user in seedUsers)
            {
                var existingUser = context.Users.FirstOrDefault(u => u.UserName == user.UserName);
                if (existingUser == null)
                {
                    user.Id = Guid.NewGuid().ToString();
                    context.Users.Add(user);
                }
                else
                {
                    // update fields if needed
                    existingUser.NameUser = user.NameUser;
                    existingUser.Surname = user.Surname;
                    existingUser.PlaceOfOrigin = user.PlaceOfOrigin;
                }
            }
            context.SaveChanges();

            // ===== Ensure RangList for event 4 exists =====
            var rangList = context.RangLists.FirstOrDefault(r => r.EventId == 4);
            if (rangList == null)
            {
                rangList = new RangList { EventId = 4 };
                context.RangLists.Add(rangList);
                context.SaveChanges();
            }

            // ===== Upsert RangList_Users with points for seeded users =====
            foreach (var userSeed in seedUsers)
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == userSeed.UserName);
                if (user != null)
                {
                    var rlUser = context.RangList_Users.FirstOrDefault(rlu => rlu.UserId == user.Id && rlu.RangListId == rangList.Id);

                    int points = userSeed.UserName switch
                    {
                        "bariiseni42" => 1280,
                        "ilija" => 1140,
                        "stefanstosikj@example.com" => 1015,
                        _ => 0
                    };

                    if (rlUser == null)
                    {
                        rlUser = new RangList_User
                        {
                            UserId = user.Id,
                            RangListId = rangList.Id,
                            Points = points
                        };
                        context.RangList_Users.Add(rlUser);
                    }
                    else
                    {
                        rlUser.Points = points; // update points if changed
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
