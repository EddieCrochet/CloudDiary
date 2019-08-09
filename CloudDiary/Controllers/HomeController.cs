using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudDiary.Models;
using CloudDiary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

namespace CloudDiary.Controllers
{
    public class HomeController : Controller
    {
        ////GET: Diary
        ////user MUST be logged in in order to view this method
        //[Authorize]
        //public IActionResult Index()
        //{
        //    //this is simply how I created test data
        //    //var diaryEntries = new List<DiaryEntry>();
        //    //diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now, Text = "Just feel like coding today haha" });
        //    //diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now.AddDays(-1),
        //    //    Text = "Rained all day great thats a happy person lolol" });
        //    //diaryEntries.Add(new DiaryEntry() { Created = DateTime.Now.AddDays(-2), Text = "What a fun project" });

        //    var options = new DbContextOptions<ApplicationDbContext>();
        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        var UserId = User.Identity.GetUserId();

        //        //linq query to get entries from database and see newest
        //        var diaryEntries = context.DiaryEntries
        //            .Where(d => d.UserId == UserId)
        //            .OrderByDescending(d => d.Created);

        //        ViewBag.DiaryEntries = diaryEntries.ToList();
        //    }

        //    return View("Diary");
        //}

            //the above was old code from the diaryController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AddDiaryEntryViewModel model)
        {
            if (!ModelState.IsValid)
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
                context.DiaryEntries.Add(diaryEntry);
                context.SaveChanges();
            }

            //The below old code simple added to the index view only the entry that was just created

            //    var diaryEntries = new List<DiaryEntry>();
            //diaryEntries.Add(new DiaryEntry { Created = DateTime.Now, Text = model.Text });

            //ViewBag.DiaryEntries = diaryEntries;

            //ToDo: storing diary entry in database

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var options = new DbContextOptions<ApplicationDbContext>();
                using (var context = new ApplicationDbContext(options))
                {
                    var UserId = User.Identity.GetUserId();

                    //linq query to get entries from database and see newest
                    var diaryEntries = context.DiaryEntries
                        .Where(d => d.UserId == UserId)
                        .OrderByDescending(d => d.Created);

                    ViewBag.DiaryEntries = diaryEntries.ToList();
                }
                return View("Diary");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
