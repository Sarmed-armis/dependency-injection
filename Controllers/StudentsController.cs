using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data.Entity.services.imp;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : Controller
    {


        private readonly StudentsServies _StudentsServies = new StudentsServies();
        // GET: Students
        public ActionResult Index()
        {
            var Students = _StudentsServies.GetAll();


            return View(Students);
        }

        public ActionResult Create()
        {
            return View();

        }



        [HttpPost]
        public ActionResult Create(Students Students)

        {
            return View();

        }
    }
}