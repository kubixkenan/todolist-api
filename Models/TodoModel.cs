using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        public string TodoTxt { get; set; }
        public bool Done { get; set; }
        public Guid UserId { get; set; }
    }
}
