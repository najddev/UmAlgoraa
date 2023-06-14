using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UmAlgoraa.Controllers.Data;
using UmAlgoraa.Models;
using UmAlgoraa.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace UmAlgoraa.Controllers
{
    public class AdsController : Controller
    {
        AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _host;

        public AdsController(IHostingEnvironment host, AppDbContext _appDbContext, UserManager<ApplicationUser> _userManager) 
        { 
            appDbContext = _appDbContext;
            this.userManager = _userManager;
            _host = host;

        }

        
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser appUser = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                List<Ads> ads = appDbContext.Ads.Where(a => a.UserId == appUser.Id)
                .Where(a => a.IsDrafted == false).ToList();

                if (ads != null)
                {

                    foreach (var item in ads)
                    {
                        item.User = new User();
                    }
                    return View(ads);
                }

                return View(ads = new List<Ads>());
            }
            List<Ads> emptyAds = new List<Ads>();
            return View(emptyAds);
        }

        
        [HttpGet]
        public IActionResult New()
        {
           
            AdsViewModel ads = new AdsViewModel();
            return View(ads);
        }

        
        [HttpPost]
        public async Task<IActionResult> New(AdsViewModel adsvm)
        {
            Ads ads = new Ads();
          
            if (ModelState.IsValid)
            {
                ads.Title = adsvm.Title;
                ads.Competition = adsvm.Competition;
                ads.OpenDate = adsvm.OpenDate;
                ads.Deadline = adsvm.Deadline;
                ads.PampletValue = adsvm.PampletValue;
                ads.CompetitionNum = adsvm.CompetitionNum;
                ads.Detalies = adsvm.Detalies;
                ads.Notes = adsvm.Notes;

                ads.AdStatus = "تحت المراجعة";

                ads.IsDrafted = adsvm.IsDrafted;

                ApplicationUser appUser = await userManager.GetUserAsync(User);
                ads.UserId = appUser.Id;

                string fileName = string.Empty;
                if (adsvm.File != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "img");
                    fileName = adsvm.File.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    adsvm.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    ads.ImagePath = fileName;
                }

                appDbContext.Ads.Add(ads);
                appDbContext.SaveChanges();

                return RedirectToAction("NewDone");
            }
            return View(adsvm);
        }

        
        public IActionResult NewDone()
        {
            return View();
        }

        
        public IActionResult Details(int Id)
        {
            Ads ads = appDbContext.Ads.Where(Ads => Ads.Id == Id)
                .Where(a => a.IsDrafted == false).FirstOrDefault();
            
            ads.User = appDbContext.Users.Where(u => u.ApplicationUserId == ads.UserId).FirstOrDefault();

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
            adsvm.Id=ads.Id;

            return View(adsvm);
        }
    }
}