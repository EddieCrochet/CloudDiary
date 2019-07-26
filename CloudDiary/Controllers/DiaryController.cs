using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudDiary.Models;
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
            var diaryEntries = new List<DiaryEntry>();
            diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now, Text = "Just feel like coding today haha" });
            diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now.AddDays(-1),
                Text = "Rained all day great thats a happy person lolol" });
            diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now.AddDays(-2), Text = "What a fun project" });

            ViewBag.DiaryEntries = diaryEntries;

            return View("Diary");
        }
    }
}