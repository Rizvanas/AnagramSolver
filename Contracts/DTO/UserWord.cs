using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public class UserWord
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
