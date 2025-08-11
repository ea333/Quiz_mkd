using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Quiz.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Event_User> Events_Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Domain.Domain_Models.Quiz> Quizes { get; set; }
        public DbSet<ApplicationUser>  ApplicationUsers { get; set; }
        public DbSet<TypeQuestion> TypeQuestions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Category_User> Category_Users { get; set; }

        public DbSet<RangList> RangLists { get; set; }

        public DbSet<RangList_User> RangList_Users { get; set; }

        public DbSet<Category_RangList> Category_RangLists { get; set; }

        public DbSet<EventPending_User> EventPending_Users { get; set; }

        public DbSet<UserTotalPointsPerCategory> UserTotalPointsPerCategories { get; set; }

        public DbSet<RangList_TotalPointsPerCategory> RangList_TotalPointsPerCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // Event -> RangList (One-to-One)
            modelbuilder.Entity<RangList>()
                .HasOne(r => r.Event)
                .WithOne(e => e.RangList)
                .HasForeignKey<RangList>(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade); // CASCADE DELETE

            // Event -> Event_User (One-to-Many)
            modelbuilder.Entity<Event_User>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.Event_User)
                .HasForeignKey(eu => eu.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // modelbuilder -> RangList_User (One-to-Many)
            modelbuilder.Entity<RangList_User>()
                .HasOne(ru => ru.RangList)
                .WithMany(r => r.Participants)
                .HasForeignKey(ru => ru.RangListId)
                .OnDelete(DeleteBehavior.Cascade);

            // RangList -> Category_User (One-to-Many)
            modelbuilder.Entity<Category_User>()
                .HasOne(cu => cu.RangList)
                .WithMany(r => r.Category_Users)
                .HasForeignKey(cu => cu.RangListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Domain.Domain_Models.Quiz>()
                .HasMany(u => u.QuestionList)
                .WithOne(u => u.Quiz)
                .HasForeignKey(u => u.QuizId)
                .OnDelete(DeleteBehavior.SetNull);

            modelbuilder.Entity<Question>()
                .HasMany(u => u.AnswersList)
                .WithOne(u => u.Question)
                .HasForeignKey(u => u.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Event_User)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ApplicationUser>()
               .HasMany(u => u.Category_User)
               .WithOne(u => u.User)
               .HasForeignKey(u => u.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ApplicationUser>()
            .HasMany(u => u.RangList_User)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<ApplicationUser>()
          .HasMany(u => u.EventPending_User)
          .WithOne(u => u.User)
          .HasForeignKey(u => u.UserId)
          .OnDelete(DeleteBehavior.Cascade);




            modelbuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Географија на Македонија",
                    Description = "Провери си го знаење за Географија во Македонија",
                    StartDate = new DateTime(2025,2,15),//year,month,day
                    EndDate = new DateTime(2025,2,25),
                    Destination = "Битола",
                    ImageUrl=""
                    
                },
                  new Event
                  {
                      Id = 2,
                      Name = "Историја на Македонија",
                      Description = "Провери си го знаење за Историја во Македонија",
                      StartDate = new DateTime(2025, 3, 15),//year,month,day
                      EndDate = new DateTime(2025, 3, 25),
                      Destination = "Скопје",
                      ImageUrl = ""

                  },
                  new Event
                  {
                      Id = 3,
                      Name = "Географија на Македонија 2 дел",
                      Description = "Провери си го знаење за Географија 2 дел во Македонија",
                      StartDate = new DateTime(2025, 3, 15),//year,month,day
                      EndDate = new DateTime(2025, 3, 25),
                      Destination = "Куманово",
                      ImageUrl = ""


                  }

                );

            modelbuilder.Entity<Category>().HasData(
                    new Category 
                    {
                        Id = 1,
                        categoryName = "Ж.Стил"
                    },
                     new Category
                     {
                         Id = 2,
                         categoryName = "Спорт"
                     },
                     new Category
                     {
                         Id = 3,
                         categoryName = "Наука"
                     },
                     new Category
                     {
                         Id = 4,
                         categoryName = "Култура"
                     },
                     new Category
                     {
                         Id = 5,
                         categoryName = "Медиуми"
                     },
                     new Category
                     {
                         Id = 6,
                         categoryName = "Свет"
                     },
                      new Category
                      {
                          Id = 7,
                          categoryName = "Историја"
                      },
                     new Category
                     {
                         Id = 8,
                         categoryName = "Забава"
                     }

                );

            modelbuilder.Entity<RangList>().HasData(
                    new RangList 
                    {
                        Id=1,
                        EventId = 1
                    
                    },
                    new RangList
                    {
                        Id = 2,
                        EventId = 2

                    },
                    new RangList
                    {
                        Id = 3,
                        EventId = 3

                    }

                );

            modelbuilder.Entity<Category_RangList>().HasData(
    new Category_RangList { Id = 1, CategoryId = 1, RangListId = 1 },
    new Category_RangList { Id = 2, CategoryId = 2, RangListId = 1 },
    new Category_RangList { Id = 3, CategoryId = 3, RangListId = 2 },
    new Category_RangList { Id = 4, CategoryId = 4, RangListId = 2 },
    new Category_RangList { Id = 5, CategoryId = 5, RangListId = 3 },
    new Category_RangList { Id = 6, CategoryId = 6, RangListId = 3 },
    new Category_RangList { Id = 7, CategoryId = 7, RangListId = 3 },
    new Category_RangList { Id = 8, CategoryId = 8, RangListId = 1 }
);


            //modelbuilder.Entity<Event_User>().HasData(
            //   new Event_User
            //   {
            //       Id=1,
            //       EventId = 1,
            //       UserId = 1,
            //   },
            //    new Event_User
            //    {
            //        Id = 1,
            //        EventId = 2,
            //        UserId = 1,
            //    }// dovrshi ne znam dali kje pravi problemi


            //   );


            modelbuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    Text = "Која е највисоката планина во Македонија?",
                    TypeQuestionId = 1,
                    QuizId = 1
                },
                  new Question
                  {
                      Id = 2,
                      Text = "Која е најголема река во Македонија?",
                      TypeQuestionId = 1,
                      QuizId = 1
                  },
                   new Question
                   {
                       Id = 3,
                       Text = "Кое е најголемото природно езеро во Македонија?",
                       TypeQuestionId = 1,
                       QuizId = 1
                   },
                    new Question
                    {
                        Id = 4,
                        Text = "Кој национален парк е дом на загрозениот балкански рис?",
                        TypeQuestionId = 1,
                        QuizId = 1
                    },
                     new Question
                     {
                         Id = 5,
                         Text = "Кој од наведените градови е втор по големина во Македонија?",
                         TypeQuestionId = 1,
                         QuizId = 1
                     },
                        new Question
                        {
                            Id = 6,
                            Text = "Кое античко кралство ја опфаќало територијата на модерна Македонија?",
                            TypeQuestionId = 2,
                            QuizId = 2
                        },
                     new Question
                     {
                         Id = 7,
                         Text = "Која година Македонија прогласи независност од Југославија?",
                         TypeQuestionId = 2,
                         QuizId = 2
                     },
                       new Question
                       {
                           Id = 8,
                           Text = "Која позната историска личност, родена во Пела, Грција," +
                           " имаше значително влијание врз регионот на Македонија?",
                           TypeQuestionId = 2,
                           QuizId = 2
                       },
                      new Question
                      {
                          Id = 9,
                          Text = "Која империја владеела со територијата на модерна Македонија над 500 години?",
                          TypeQuestionId = 2,
                          QuizId = 2
                      },
                     new Question
                     {
                         Id = 10,
                         Text = "Како се викаше договорот со кој се реши долгогодишниот " +
                         "спор за името меѓу Грција и Македонија во 2018 година?",
                         TypeQuestionId = 2,
                         QuizId = 2
                     },
                       new Question
                       {
                           Id = 11,
                           Text = "Кој е најголемиот остров во Охридското Езеро?",
                           TypeQuestionId = 1,
                           QuizId = 3
                       },
                        new Question
                        {
                            Id = 12,
                            Text = "Која карпеста формација во Македонија е позната како „Камени кукли“ ?",
                            TypeQuestionId = 1,
                            QuizId = 3
                        },
                        new Question
                        {
                            Id = 13,
                            Text = "Кој град во Македонија се наоѓа најсеверно?",
                            TypeQuestionId = 1,
                            QuizId = 3
                        },
                          new Question
                          {
                              Id = 14,
                              Text = "Која е најдлабоката пештера во Македонија?",
                              TypeQuestionId = 1,
                              QuizId = 3
                          },
                           new Question
                           {
                               Id = 15,
                               Text = "На која река се наоѓа Козјак – најголемото вештачко езеро во Македонија?",
                               TypeQuestionId = 1,
                               QuizId = 3
                           }

                );

            modelbuilder.Entity<Domain.Domain_Models.Quiz>().HasData(
              new Domain.Domain_Models.Quiz
              {
                  Id = 1,
                  Name = "Брза Географија",
                  FileName = "",
                  ImageUrl = ""


              },
                new Domain.Domain_Models.Quiz
                {
                    Id = 2,
                    Name = "Пат низ минатотo",
                    FileName = "",
                    ImageUrl = ""

                },
                 new Domain.Domain_Models.Quiz
                 {
                     Id = 3,
                     Name = "Брза географија 2 дел",
                     FileName = "",
                     ImageUrl = ""

                 }


              );

            modelbuilder.Entity<TypeQuestion>().HasData(
               new TypeQuestion
               {
                   Id = 1,
                   Type = "Географија",
                   
               },
               new TypeQuestion
               {
                   Id = 2,
                   Type = "Историја",
                   
               }


               );

            modelbuilder.Entity<Answer>().HasData(
                new Answer {
                    Id = 1,
                    QuestionId = 1,
                    Text = "Јакупица",
                    isCorrect = false
                },
                 new Answer
                 {
                     Id = 2,
                     QuestionId = 1,
                     Text = "Шар Планина",
                     isCorrect = false
                 },
                   new Answer
                   {
                       Id = 3,
                       QuestionId = 1,
                       Text = "Кораб",
                       isCorrect = true
                   },
                     new Answer
                     {
                         Id = 4,
                         QuestionId = 1,
                         Text = "Пелистер",
                         isCorrect = false
                     },
                new Answer
                {
                    Id = 5,
                    QuestionId = 2,
                    Text = "Треска",
                    isCorrect = false
                },
                 new Answer
                 {
                     Id = 6,
                     QuestionId = 2,
                     Text = "Црна Река",
                     isCorrect = false
                 },
                 new Answer
                 {
                     Id = 7,
                     QuestionId = 2,
                     Text = "Брегалница",
                     isCorrect = false
                 },
                  new Answer
                  {
                      Id = 8,
                      QuestionId = 2,
                      Text = "Вардар",
                      isCorrect = true
                  },
                   new Answer
                   {
                       Id = 9,
                       QuestionId = 3,
                       Text = "Преспа",
                       isCorrect = false
                   },
                     new Answer
                     {
                         Id = 10,
                         QuestionId = 3,
                         Text = "Дојран",
                         isCorrect = false
                     },
                      new Answer
                      {
                          Id = 11,
                          QuestionId = 3,
                          Text = "Охрид",
                          isCorrect = true
                      },
                      new Answer
                      {
                          Id = 12,
                          QuestionId = 3,
                          Text = "Тиквеш",
                          isCorrect = false
                      },
                       new Answer
                       {
                           Id = 13,
                           QuestionId = 4,
                           Text = "Галичица",
                           isCorrect = false
                       },
                         new Answer
                         {
                             Id = 14,
                             QuestionId = 4,
                             Text = "Маврово",
                             isCorrect = true
                         },
                          new Answer
                          {
                              Id = 15,
                              QuestionId = 4,
                              Text = "Јасен",
                              isCorrect = false
                          },
                             new Answer
                             {
                                 Id = 16,
                                 QuestionId = 4,
                                 Text = "Пелистер",
                                 isCorrect = false
                             },
                      new Answer
                      {
                          Id = 17,
                          QuestionId = 5,
                          Text = "Битола",
                          isCorrect = true
                      },
                        new Answer
                        {
                            Id = 18,
                            QuestionId = 5,
                            Text = "Охрид",
                            isCorrect = false
                        },
                        new Answer
                        {
                            Id = 19,
                            QuestionId = 5,
                            Text = "Куманово",
                            isCorrect = false
                        },
                        new Answer
                        {
                            Id = 20,
                            QuestionId = 5,
                            Text = "Тетово",
                            isCorrect = false
                        },
                         new Answer
                         {
                             Id = 21,
                             QuestionId = 6,
                             Text = "Дарданиа",
                             isCorrect = false
                         },
                          new Answer
                          {
                              Id = 22,
                              QuestionId = 6,
                              Text = "Пеониа",
                              isCorrect = true
                          },
                            new Answer
                            {
                                Id = 23,
                                QuestionId = 6,
                                Text = "Алирија",
                                isCorrect = false
                            },
                          new Answer
                          {
                              Id = 24,
                              QuestionId = 6,
                              Text = "Англија",
                              isCorrect = false
                          },
                           new Answer
                           {
                               Id = 25,
                               QuestionId = 7,
                               Text = "1989",
                               isCorrect = false
                           },
                           new Answer
                           {
                               Id = 26,
                               QuestionId = 7,
                               Text = "1995",
                               isCorrect = false
                           },
                            new Answer
                            {
                                Id = 27,
                                QuestionId = 7,
                                Text = "1991",
                                isCorrect = true
                            },
                             new Answer
                             {
                                 Id = 28,
                                 QuestionId = 7,
                                 Text = "2001",
                                 isCorrect = false
                             },
                              new Answer
                              {
                                  Id = 29,
                                  QuestionId = 8,
                                  Text = "Филип II Македонски",
                                  isCorrect = false
                              },
                              
                                new Answer
                                {
                                    Id = 30,
                                    QuestionId = 8,
                                    Text = "Александар Македонски",
                                    isCorrect = true
                                },
                                
                                 new Answer
                                 {
                                     Id = 31,
                                     QuestionId = 8,
                                     Text = "Јустинијан I",
                                     isCorrect = false
                                 },
                                 
                        new Answer
                        {
                            Id = 32,
                            QuestionId = 8,
                            Text = "Скендербег",
                            isCorrect = false
                        },
                        
                         new Answer
                         {
                             Id = 33,
                             QuestionId = 9,
                             Text = "Римската империја",
                             isCorrect = false
                         },
                         
                           new Answer
                           {
                               Id = 34,
                               QuestionId = 9,
                               Text = "Византиска империја",
                               isCorrect = false
                           },
                           
                             new Answer
                             {
                                 Id = 35,
                                 QuestionId = 9,
                                 Text = "Отоманската империја",
                                 isCorrect = true
                             },
                             
                                new Answer
                                {
                                    Id = 36,
                                    QuestionId = 9,
                                    Text = "Австро - унгарската империја",
                                    isCorrect = false
                                },
                                
                          new Answer
                          {
                              Id = 37,
                              QuestionId = 10,
                              Text = "Скопски договор",
                              isCorrect = false
                          },
                           new Answer
                           {
                               Id = 38,
                               QuestionId = 10,
                               Text = "Договорот од Преспа",
                               isCorrect = true
                           },
                            new Answer
                            {
                                Id = 39,
                                QuestionId = 10,
                                Text = "Балкански договор",
                                isCorrect = false
                            },
                             new Answer
                             {
                                 Id = 40,
                                 QuestionId = 10,
                                 Text = "Охридски рамковен договор",
                                 isCorrect = false
                             },
                              new Answer
                              {
                                  Id = 41,
                                  QuestionId = 11,
                                  Text = "Градско Острово",
                                  isCorrect = false
                              },
                              new Answer
                              {
                                  Id = 42,
                                  QuestionId = 11,
                                  Text = "Голем Град",
                                  isCorrect = true
                              },
                              new Answer
                              {
                                  Id = 43,
                                  QuestionId = 11,
                                  Text = "Мал Град",
                                  isCorrect = false
                              },
                               new Answer
                               {
                                   Id = 44,
                                   QuestionId = 11,
                                   Text = "Пештани",
                                   isCorrect = false
                               },
                               new Answer
                               {
                                   Id = 45,
                                   QuestionId = 12,
                                   Text = "Куклица",
                                   isCorrect = true
                               },
                                new Answer
                                {
                                    Id = 46,
                                    QuestionId = 12,
                                    Text = "Маркови Кули",
                                    isCorrect = false
                                },
                                new Answer
                                {
                                    Id = 47,
                                    QuestionId = 12,
                                    Text = "Долни Полог",
                                    isCorrect = false
                                },
                                 new Answer
                                 {
                                     Id = 48,
                                     QuestionId = 12,
                                     Text = "Плочата",
                                     isCorrect = false
                                 },
                                 new Answer
                                 {
                                     Id = 49,
                                     QuestionId = 13,
                                     Text = "Куманово",
                                     isCorrect = true
                                 },
                                 new Answer
                                 {
                                     Id = 50,
                                     QuestionId = 13,
                                     Text = "Крива Паланка",
                                     isCorrect = false
                                 },
                                    new Answer
                                    {
                                        Id = 51,
                                        QuestionId = 13,
                                        Text = "Тетово",
                                        isCorrect = false
                                    },
                                     new Answer
                                     {
                                         Id = 52,
                                         QuestionId = 13,
                                         Text = "Кратово",
                                         isCorrect = false
                                     },
                              new Answer
                              {
                                  Id = 53,
                                  QuestionId = 14,
                                  Text = "Слатински Извор",
                                  isCorrect = false
                              },
                                new Answer
                                {
                                    Id = 54,
                                    QuestionId = 14,
                                    Text = "Врело",
                                    isCorrect = true
                                },
                             new Answer
                             {
                                 Id = 55,
                                 QuestionId = 14,
                                 Text = "Алилица",
                                 isCorrect = false
                             },
                              new Answer
                              {
                                  Id = 56,
                                  QuestionId = 14,
                                  Text = "Голубарница",
                                  isCorrect = false
                              },
                               new Answer
                               {
                                   Id = 57,
                                   QuestionId = 15,
                                   Text = "Треска",
                                   isCorrect = true
                               },
                                 new Answer
                                 {
                                     Id = 58,
                                     QuestionId = 15,
                                     Text = "Вардар",
                                     isCorrect = false
                                 },
                                  new Answer
                                  {
                                      Id = 59,
                                      QuestionId = 15,
                                      Text = "Радика",
                                      isCorrect = false
                                  },
                                   new Answer
                                   {
                                       Id = 60,
                                       QuestionId = 15,
                                       Text = "Црна",
                                       isCorrect = false
                                   }

                );
        }


    }
}
