using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionService.Database
{
    [Table("question")]
    public class DatabaseQuestion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        public string Text { get; set; }
        [Column]
        public string Answer1 { get; set; }
        [Column]
        public string Answer2 { get; set; }
        [Column]
        public string Answer3 { get; set; }
        [Column]
        public string Answer4 { get; set; }
        [Column]
        public int RightAnswerIndex { get; set; }
    }
}
