using Microsoft.AspNetCore.Http;

namespace Contracts.Models
{
    public class CookieInfoViewModel
    {
        public IRequestCookieCollection Cookies { get; set; }
    }
}
