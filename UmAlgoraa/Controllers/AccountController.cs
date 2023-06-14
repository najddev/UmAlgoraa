using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UmAlgoraa.Controllers.Data;
using UmAlgoraa.Models;
using UmAlgoraa.ViewModels;

namespace UmAlgoraa.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        AppDbContext appDbContext;
        public AccountController(AppDbContext appDbContext , UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = signInManager;
            this.appDbContext=appDbContext;
        }
        //regieter
        //open view "link
        public IActionResult Registration()
        {
            return View();
        }
        //save data dabe Request ==>db
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel newUserVM)
        //not all property model + extra property for view Confirm + validation [][][]
        {
            if (ModelState.IsValid)
            {
                //map from vm to model
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName; 
                userModel.EmpName = newUserVM.EmpName;
                userModel.NationalId=newUserVM.NationalId;
                userModel.PhoneNumber= newUserVM.PhoneNumber;
                userModel.Ministry=newUserVM.Ministry;
                userModel.Manger=newUserVM.Manger;
                userModel.Department=newUserVM.Department;
                userModel.Email=newUserVM.Email;
                userModel.PasswordHash=newUserVM.Password;
                userModel.ConfirmPassword = newUserVM.ConfirmPassWord;

                //save data by create account (6 A a $ 7)
                IdentityResult result =
                    await userManager.CreateAsync(userModel, newUserVM.Password);//object insert db

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "User");//insert row UserRole
                    //create cookie
                    List<Claim> addClaim = new List<Claim>();
                    await signInManager.SignInWithClaimsAsync(userModel, false, addClaim);

                    ApplicationUser appUser = appDbContext.ApplicationUsers
                        .Where(a => a.UserName == userModel.UserName).FirstOrDefault();
                    
                    User newUser = new User();
                    newUser.ApplicationUserId = appUser.Id;
                    appDbContext.Users.Add(newUser);
                    appDbContext.SaveChanges();

                    return RedirectToAction("SignIn", "Account");
                }
                else
                {
                    //some issue ==>send user view
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newUserVM);
        }



        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel userVmReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {

                //check valid  account "found in db"
                ApplicationUser userModel =
                    await userManager.FindByNameAsync(userVmReq.Username);
                
                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, userVmReq.Password, userVmReq.rememberMe, false);
                    //check cookie
                    if (rr.Succeeded)
                        return RedirectToAction("Index", "Ads");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Data !!");
                }
            }
            return View(userVmReq);
        }

        public async Task<IActionResult> signOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
