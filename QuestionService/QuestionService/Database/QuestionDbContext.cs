using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuestionService.Database
{
    public class QuestionDbContext : DbContext, IDatabaseHandler
    {
        public DbSet<DatabaseQuestion> Questions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres");

        public void EnsureCreated()
        {
            this.Database.EnsureCreated();
        }

        private void GenerateTestData()
        {
            this.Questions.Add(new DatabaseQuestion()
            {
                Text = "What is the best programming language?",

                Answer1 = "Java",
                Answer2 = "C#",
                Answer3 = "TypeScript",
                Answer4 = "All are awesome",

                RightAnswerIndex = 4,
            });
            this.Questions.Add(new DatabaseQuestion()
            {
                Text = "What is the second best programming language?",

                Answer1 = "Java",
                Answer2 = "C#",
                Answer3 = "TypeScript",
                Answer4 = "All are still the same amount awesome",

                RightAnswerIndex = 4,
            });
            this.Questions.Add(new DatabaseQuestion()
            {
                Id = 10000,
                Text = "What is the third best programming language?",

                Answer1 = "Java",
                Answer2 = "C#",
                Answer3 = "TypeScript",
                Answer4 = "All are still the same amount awesome",

                RightAnswerIndex = 4,
            });
            this.SaveChanges();
            

            // postgres=# select * from question;
            //   Id   |                     Text                      | Answer1 | Answer2 |  Answer3   |                Answer4                | RightAnswerIndex
            // -------+-----------------------------------------------+---------+---------+------------+---------------------------------------+------------------
            //      1 | What is the best programming language?        | Java    | C#      | TypeScript | All are awesome                       |                4
            //      2 | What is the second best programming language? | Java    | C#      | TypeScript | All are still the same amount awesome |                4
            //  10000 | What is the third best programming language?  | Java    | C#      | TypeScript | All are still the same amount awesome |                4
            // (3 Zeilen)
        }

        public DatabaseQuestion GetQuestion()
        {
            var dbQuestion = this.Questions.FirstOrDefault();
            
            if (dbQuestion == null)
            {
                this.GenerateTestData();
                dbQuestion = this.Questions.FirstOrDefault();
            }

            return dbQuestion;
        }
    }
}
