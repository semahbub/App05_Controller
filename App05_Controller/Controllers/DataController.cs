using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App05_Controller.Models;
using Microsoft.AspNetCore.Mvc;

namespace App05_Controller.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public string TextData()
        //{
        //    return "This is our first String from Controller.";
        //}

        public string TextData()
        {
            return "This is our first String from Controller.";
        }

        //public JsonResult JsonData()
        //{
        //    return Json(new {result="ok", msg= "This is our first Json Object from Controller." });
        //}

        public IActionResult JsonData()
        {
            return Json(new { result = "ok", msg = "This is our first Json Object from Controller." });
        }

        //public ContentResult ContentData()
        //{
        //    return Content("This is our first content from Controller.");
        //}

        public IActionResult ContentData()
        {
            return Content("This is our first content from Controller.");
        }

        public IActionResult UserData()
        {
            return View();
        }

        //[HttpPost]
        public IActionResult UserDataPost(string Name, int Age, string Country)
        {
            ViewBag.name = Name;
            ViewBag.age = Age;
            ViewBag.country = Country;

            if (string.IsNullOrEmpty(Name))
            {
                ViewBag.msg = "Person Name is Empty. Please Enter Person Name";
                return View("UserData");
            }

            if (Age <= 0)
            {
                ViewBag.msg = "Person Age could not be Zero or Negative. Please Enter Valid Age.";
                return View("UserData");
            }
            if (string.IsNullOrEmpty(Country))
            {
                ViewBag.msg = "Country Name is Empty. Please Enter Country Name";
                return View("UserData");
            }

            //string curdir = Directory.GetCurrentDirectory();
            //string subdir = Path.Combine(curdir, "wwwroot", "OurData");
            string subdir = @"D:\Temp\";
            if (!Directory.Exists(subdir))
            {
                Directory.CreateDirectory(subdir);
            }

            string df = Path.Combine(subdir, "data.csv");
            if (!System.IO.File.Exists(df))
            {
                System.IO.File.Create(df);
            }
            string[] lines = System.IO.File.ReadAllLines(df);

            string last = "";
            List<string> strlist = new List<string>();
            
            foreach (var item in lines)
            {
                strlist.Add(item);
                last = item;
            }
            
            string[] strid = last.Split(',');
            int newId = Convert.ToInt32("0" + strid[0].Trim()) + 1;

            string data = newId.ToString()+ "," + Name + "," + Age.ToString() + "," + Country;
            strlist.Add(data);

            System.IO.File.WriteAllLines(df, strlist);

            ViewBag.name = "";
            ViewBag.age = "";
            ViewBag.country = "";

            ViewBag.msg = "Thank You. Data has been saved successfully.";
            return View("UserData");
        }


        public IActionResult ShowUserData()
        {
            string subdir = @"D:\Temp\";
            string df = Path.Combine(subdir, "data.csv");
            string[] lines = System.IO.File.ReadAllLines(df);

            List<Person> people = new List<Person>();

            foreach (var line in lines)
            {
                string[] element = line.Split(',');

                Person p = new Person();

                p.Id = Convert.ToInt32("0" + element[0].Trim());
                p.Name = element[1].Trim();
                p.Age = Convert.ToInt32("0" + element[2].Trim());
                p.Country = element[3].Trim();

                people.Add(p);
            }
            return View(people);
        }

    }
}