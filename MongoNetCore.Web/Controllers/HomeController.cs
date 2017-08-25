using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoNetCore.Data;
using MongoNetCore.Infraestructure.Interfaces;
using MongoNetCore.Infraestructure.Repositories;

namespace MongoNetCore.Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly INoteRepository _noteRepository;

		public HomeController(INoteRepository noteRepository)
		{
			_noteRepository = noteRepository;
		}

        public async Task<IActionResult> Index()
        {
            try
            {
				//_noteRepository.AddNote(new Note { body = "Test Test", created_on = DateTime.Now, updated_on = DateTime.Now });
				
                var notes = await _noteRepository.GetAllNotes();

                if(!(notes is null))
                {
                    var date = DateTime.TryParse(notes.Select(x => x.updated_on).First().ToString(), out var myNewDate);
                    var date2 = myNewDate;
                }

                return View();
            } 
            catch(Exception ex)
            {
                var t = ex;
                return View();
            }

        }

       

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
