using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudDiary.Controllers
{
    public class DiaryController : Controller
    {
        //GET: Diary
        //user MUST be logged in in order to view this method
        [Authorize]
        public IActionResult Index()
        {
            return View("Diary");
        }
    }
}