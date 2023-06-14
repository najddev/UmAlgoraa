using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmAlgoraa.Controllers.Data;
using UmAlgoraa.Models;
using UmAlgoraa.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace UmAlgoraa.Controllers
{
    [Authorize]//[Authorize(Roles = "User")]
    public class DraftController : Controller
    {
        AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _host;


        public DraftController(IHostingEnvironment host, AppDbContext _appDbContext, UserManager<ApplicationUser> _userManager)
        {
            appDbContext = _appDbContext;
            this.userManager = _userManager;
            _host = host;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser appUser = await userManager.GetUserAsync(User);

            List<Ads> ads = appDbContext.Ads.Where(d => d.UserId == appUser.Id)
                .Where(d=>d.IsDrafted==true).ToList();
            if(ads != null)
            {
                foreach (var item in ads)
                {
                    item.User = new User();
                }
                return View(ads);
            }
            List<Ads> emptyAds = new List<Ads>();
            return View(emptyAds);
        }


        public IActionResult Details(int Id)
        {
            Ads ads = appDbContext.Ads.Where(d => d.Id == Id)
                .Where(d=>d.IsDrafted==true).FirstOrDefault();
            
            //ads.User = appDbContext.Users
            //    .Where(u => u.ApplicationUserId == ads.UserId).FirstOrDefault();

            AdsViewModel adsvm = new AdsViewModel();
            adsvm.Title = ads.Title;
            adsvm.Competition = ads.Competition;
            adsvm.OpenDate = ads.OpenDate;
            adsvm.Deadline = ads.Deadline;
            adsvm.PampletValue = ads.PampletValue;
            adsvm.CompetitionNum = ads.CompetitionNum;
            adsvm.Detalies = ads.Detalies;
            adsvm.Notes = ads.Notes;
            adsvm.ImagePath = ads.ImagePath;
            adsvm.Id = ads.Id;

            return View(adsvm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            //Old to ease for the user to rember what he wrote and make it easy to change
            Ads oldAds = appDbContext.Ads.Where(a => a.Id == Id)
                .Where(d => d.IsDrafted == true).FirstOrDefault();

            AdsViewModel adsViewModel = new AdsViewModel();

            adsViewModel.Title = oldAds.Title;
            adsViewModel.Competition = oldAds.Competition;
            adsViewModel.OpenDate = oldAds.OpenDate;
            adsViewModel.Deadline = oldAds.Deadline;
            adsViewModel.PampletValue = oldAds.PampletValue;
            adsViewModel.CompetitionNum = oldAds.CompetitionNum;
            adsViewModel.Detalies = oldAds.Detalies;
            adsViewModel.Notes = oldAds.Notes;
            
            ApplicationUser appUser = await userManager.GetUserAsync(User);
            User user = appDbContext.Users.Where(u => u.ApplicationUserId == appUser.Id).FirstOrDefault();
            return View(adsViewModel);
        }

        // Image Will be as string not IformFile ...
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, AdsViewModel adsvm)
        {
            if (ModelState.IsValid)
            {
                Ads oldAds = appDbContext.Ads.Where(ads => ads.Id == Id)
                    .Where(d => d.IsDrafted == true).FirstOrDefault();
                
                if (oldAds != null)
                {
                    oldAds.Title = adsvm.Title;
                    oldAds.Competition = adsvm.Competition;
                    oldAds.OpenDate = adsvm.OpenDate;
                    oldAds.Deadline = adsvm.Deadline;
                    oldAds.PampletValue = adsvm.PampletValue;
                    oldAds.CompetitionNum = adsvm.CompetitionNum;
                    oldAds.Detalies = adsvm.Detalies;
                    oldAds.Notes = adsvm.Notes;
                    oldAds.IsDrafted=adsvm.IsDrafted;

                    string fileName = string.Empty;
                    if (adsvm.File != null)
                    {
                        string myUpload = Path.Combine(_host.WebRootPath, "img");
                        fileName = adsvm.File.FileName;
                        string fullPath = Path.Combine(myUpload, fileName);
                        adsvm.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                        adsvm.ImagePath = fileName;
                    }

                    appDbContext.Ads.Update(oldAds);
                    appDbContext.SaveChanges();

                    return RedirectToAction("EditDone");
                }
            }
            return View(adsvm);
        }

        public IActionResult EditDone()
        {
            return View();
        }

        public IActionResult Delete (int Id)
        {
            Ads ads = appDbContext.Ads.Where(a => a.Id == Id)
                .Where(a => a.IsDrafted == true).FirstOrDefault();

            if (ads != null)
            {
                appDbContext.Ads.Remove(ads);
                appDbContext.SaveChanges();

                return View();

                
            }
            return RedirectToAction("Index");
        }
    }
}
