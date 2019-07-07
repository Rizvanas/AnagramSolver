using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class CookieInfoViewModel
    {
        public IRequestCookieCollection Cookies { get; set; }
    }
}
