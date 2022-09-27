using Microsoft.AspNetCore.Mvc;
using SimpleWebMVC.Context;
using SimpleWebMVC.Models;
using System;
using System.Linq;

namespace SimpleWebMVC.Controllers
{


    public class DivisionController : Controller

    {
        MyContext myContext;
        public DivisionController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            var data = myContext.Divisions.ToList();
            return View(data);
        }

        public IActionResult FindId(int id)
        {
            Division data = myContext.Divisions.Find(id);
            if (data == null)
            {
                return View("_ErorPage");
            }
            Console.WriteLine($"{data.Id} - {data.DivisionName}");

            return View("Index", new[] { data });
        }

        public IActionResult AddDivision(Division division)
        {
            if (ModelState.IsValid)
            {
                myContext.Divisions.Add(division);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("_ErorPage");
        }

        public IActionResult DeleteDivision(Division division)
        {
            if (ModelState.IsValid)
            {
                myContext.Divisions.Remove(division);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("_ErorPage");
        }
        public IActionResult UpdateDivision(Division division)
        {
            if (ModelState.IsValid)
            {
                myContext.Divisions.Update(division);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("_ErorPage");

        }
    }
}
