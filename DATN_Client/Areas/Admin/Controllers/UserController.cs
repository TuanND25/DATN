﻿using DATN_Client.Areas.Customer.Component;
using Microsoft.AspNetCore.Mvc;

namespace DATN_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
			//if (Login.Roleuser == "Admin" )
			//{
				
			//}
			//else
			//{
			//	return Unauthorized();
			//}
		}
	}
}
