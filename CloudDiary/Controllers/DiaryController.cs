using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudDiary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using CloudDiary.Data;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AddDiaryEntryViewModel model)
        {
            if(!ModelState.IsValid)
            {
                //if the model has errors it will return the viewModel
                //which will display the form with the current datum in it
                return View("Diary", model);
            }

            var diaryEntry = new DiaryEntry()
            {
                Id = Guid.NewGuid(),
                UserId = User.Identity.GetUserId(),
                Created = DateTime.Now,
                Text = model.Text
            };

            var options = new DbContextOptions<ApplicationDbContext>();

            using (var context = new ApplicationDbContext(options))
            {
                context.DiaryEntry.Add(diaryEntry);
                context.SaveChanges();
            }

            //The below old code simple added to the index view only the entry that was just created

            //    var diaryEntries = new List<DiaryEntry>();
            //diaryEntries.Add(new DiaryEntry { Created = DateTime.Now, Text = model.Text });

            //ViewBag.DiaryEntries = diaryEntries;

            //ToDo: storing diary entry in database

            return View("Diary");
        }
    }
}