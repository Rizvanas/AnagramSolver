using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Models
{
    public class UserWordsViewModel
    {
        public List<UserWord> UserWords { get; set; }
        public int? Page { get; set; }
    }
}
