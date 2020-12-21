using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudOperationUsingJqueryCodeFirst.Models;
using CrudOperationUsingJqueryCodeFirst.Views;

namespace CrudOperationUsingJqueryCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly CDBContext _context = null;

        public HomeController()
        {
            _context = new CDBContext();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Test2()
        {
            return View();
        }

        public JsonResult GetStateList()
        {
            try
            {
                var stateData = _context.States.Select(x => new { x.Stateid, x.Statename }).ToList();

                return Json(new { isSuccess = true, stateData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCityList()
        {
            try
            {
                var cityData = _context.Cities.Select(x => new { x.StateId, x.CityId, x.CityName }).ToList();
                return Json(new { isSuccess = true, cityData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { isSuccess = false, massage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Insert(Employee obj)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testFiles = file.FileName.Split(new char[] { '\\' });
                            fname = testFiles[testFiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        var uploadRootFolderInput = AppDomain.CurrentDomain.BaseDirectory + "\\Uploads";
                        Directory.CreateDirectory(uploadRootFolderInput);
                        var directoryFullPathInput = uploadRootFolderInput;
                        fname = Path.Combine(directoryFullPathInput, fname);

                        file.SaveAs(fname);

                        obj.Image = file.FileName;
                    }

                }

                _context.Employees.Add(obj);
                _context.SaveChanges();

                return Json("Record insert Sucessfully", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(ex, JsonRequestBehavior.AllowGet);
            }



           
        }

        public JsonResult GetById(int id)
        {
            var data = _context.Employees.Find(id);


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DisplayListData()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);



                //List<Employee> data = _context.employees.ToList();
                List<EmployeeViewModel> data = new List<EmployeeViewModel>();
                data = (from e1 in _context.Employees
                        join s1 in _context.States on e1.StateId equals s1.Stateid
                        join c1 in _context.Cities on e1.CityId equals c1.CityId
                        select new EmployeeViewModel
                        {


                            Id = e1.Id,
                            Name = e1.Name,
                            Address = e1.Address,
                            Image = e1.Image,
                            Dob = e1.Dob,
                            Statename = s1.Statename,
                            CityName = c1.CityName,
                            Contect=e1.Contect

                        }).ToList();




                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.Id.ToString().ToLower().Contains(search.ToLower()) ||
                        p.Name.ToString().Contains(search.ToLower()) ||
                        p.Address.ToString().Contains(search.ToLower()) ||
                        p.Image.ToString().Contains(search.ToLower()) ||
                        p.Dob.ToString().Contains(search.ToLower()) ||
                        p.Contect.ToString().Contains(search.ToLower()) ||
                        p.Statename.ToString().Contains(search.ToLower()) ||
                        p.CityName.ToString().Contains(search.ToLower())
                     ).ToList();
                }
                data = SortTableData(order, orderDir, data);
                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Id,
                        d.Name,
                        d.Address,
                        d.Contect,
                        d.Dob,
                        d.Statename,
                        d.CityName,
                        d.Image
                    }
                    );
                result = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        private List<EmployeeViewModel> SortTableData(string order, string orderDir, List<EmployeeViewModel> data)
        {
            List<EmployeeViewModel> lst = new List<EmployeeViewModel>();
            try
            {
                switch (order)
                {
                   
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList()
                                                                                                 : data.OrderBy(p => p.Name).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Address).ToList()
                                                                                                 : data.OrderBy(p => p.Address).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Dob).ToList()
                                                                                                 : data.OrderBy(p => p.Dob).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Dob).ToList()
                                                                                                   : data.OrderBy(p => p.Dob).ToList();
                        break;
                    case "5":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Statename).ToList()
                                                                                                   : data.OrderBy(p => p.Statename).ToList();
                        break;
                       
                    case "6":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CityName).ToList()
                                                                                                   : data.OrderBy(p => p.CityName).ToList();
                        break;
                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Id).ToList()
                                                                                                 : data.OrderBy(p => p.Id).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = _context.Employees.Find(id);
            _context.Employees.Remove(data);
            _context.SaveChanges();

            var massage = "Delete Sucessfully";
            return Json(massage, JsonRequestBehavior.AllowGet);
        }



    }
}