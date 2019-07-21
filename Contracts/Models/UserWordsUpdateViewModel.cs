using Contracts.DTO;

namespace Contracts.Models
{
    public class UserWordsUpdateViewModel
    {
        public UserWord UserWord { get; set; }
        public bool GotUpdated { get; set; }
    }
}
