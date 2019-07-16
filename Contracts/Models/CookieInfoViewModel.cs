using Microsoft.AspNetCore.Http;

namespace AnagramGenerator.WebApp.Models
{
    public class CookieInfoViewModel
    {
        public IRequestCookieCollection Cookies { get; set; }
    }
}
