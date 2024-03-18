using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;


namespace WebUni_Management.Controllers
{
	public class PersonalInfoController : Controller
	{
		private readonly IPersonalInfoService personalInfoService;
        public PersonalInfoController(IPersonalInfoService _personalInfoService)
        {
            personalInfoService = _personalInfoService;
        }
        public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> RentedBooks(string userId) 
		{ 
			if(await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }
			var model = await personalInfoService.MyRentedBooksAsync(userId);
			return View(model);
		}
		public async Task<IActionResult> RemoveRent(int id)
		{
			if(await personalInfoService.RentedBookExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (await personalInfoService.UserWithIdHasRentedBookAsync(id, userId) == false)
			{
				return BadRequest();
			}
			await personalInfoService.RemoveBookRentAsync(id, userId);
			return RedirectToAction("RentedBooks", new { userId = userId });
		}
		public async Task<IActionResult> CheckRemainingRentTime(int id)
		{
            if (await personalInfoService.RentedBookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await personalInfoService.UserWithIdHasRentedBookAsync(id, userId) == false)
            {
                return BadRequest();
            }
        }
	}
}
