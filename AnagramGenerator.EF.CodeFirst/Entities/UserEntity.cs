using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class UserEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ip { get; set; }
    }
}
