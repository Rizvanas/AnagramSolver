using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class UserWordEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Word { get; set; }

        public int UserId { get; set; }
        public UserEntity UserIp { get; set; }
    }
}
