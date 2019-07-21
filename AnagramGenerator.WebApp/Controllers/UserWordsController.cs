﻿using Contracts.Models;
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

            return View("Update", new UserWordsUpdateViewModel
            {
                GotUpdated = true,
                UserWord = _userWordsService.GetUserWord(word)
            });
        }

        [HttpPost("userWords/remove")]
        public IActionResult Remove(string word)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _userWordsService.RemoveUserWord(word);

            return Redirect("userWords");
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
