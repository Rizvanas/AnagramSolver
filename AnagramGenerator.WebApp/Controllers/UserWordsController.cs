using Contracts.DTO;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Controllers
{
    public class UserWordsController : Controller
    {
        private readonly IUserWordsService _userWordsService;

        public UserWordsController(IUserWordsService userWordsService)
        {
            _userWordsService = userWordsService;
        }

        [HttpGet("userWords")]
        public IActionResult Index(int? page, int pageSize)
        {
            var cookie = Request.Cookies["CurrentPage"];

            page = (!String.IsNullOrEmpty(cookie) && page == null)
                ? Convert.ToInt32(cookie)
                : page;

            SetPagingCookie(page);
            return View(new UserWordsViewModel
            {
                UserWords = _userWordsService.GetUserWords(page, pageSize).ToList(),
                Page = page
            });
        }

        [HttpGet("userWords/update")]
        public IActionResult Update()
        {
            return View(new UserWordsUpdateViewModel
            {
                GotUpdated = true,
                UserWord = null
            });
        }

        [HttpPost("userWords/update")]
        public IActionResult UpdateList(string word)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _userWordsService.AddUserWord(word, ipAddress);

            return Redirect($"/userWords?pageSize=100");
        }

        [HttpPost("userWords/remove")]
        public IActionResult Remove(string word)
        {
            _userWordsService.RemoveUserWord(word);

            return Redirect($"/userWords?pageSize=100");
        }

        [HttpPost("userWords/search")]
        public IActionResult Search(string searchPhrase)
        {
            var words = new List<UserWord>();
            try
            {
                words = _userWordsService.GetUserWords(searchPhrase).ToList();
            }
            catch
            {
                return Redirect($"/userWords?pageSize=100");
            }

            return View("Index", new UserWordsViewModel { UserWords = words });
        }

        [HttpPost("userWords/change")]
        public IActionResult Change(int wordId, string word)
        {
            try
            {
                _userWordsService.UpdateUserWord(wordId, word);
            }
            catch
            {

            }

            return Redirect($"/userWords?pageSize=100");
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
