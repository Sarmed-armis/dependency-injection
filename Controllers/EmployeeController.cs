using FulentNHirbent001.Models;
using FulentNHirbent001.NHibarnateHelper;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using System;
using Microsoft.Reporting.WinForms;
using WebApplication1.Data.Entity.services.imp;

namespace FulentNHirbent001.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly employeeServies _employeeServies = new employeeServies();

        //public EmployeeController(employeeServies employeeServies)
        //{
        //    _employeeServies = employeeServies;

        //}

        // GET: Employee
        public ActionResult Index()
        {


            var employees = _employeeServies.GetAll();


            return View(employees);


        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
     
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
               
             
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(employee);
                            transaction.Commit();
                        }
                    }

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var employeetoUpdate = session.Get<Employee>(id);

                    employeetoUpdate.Designation = employee.Designation;
                    employeetoUpdate.FirstName = employee.FirstName;
                    employeetoUpdate.LastName = employee.LastName;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(employeetoUpdate);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetReportpdf()
        
        {
            List<Employee> employees;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                 employees = session.Query<Employee>().ToList();
              
            }
            var ReportPath = Server.MapPath("~/Reports/em.rdlc");

            RenderReport(ReportPath, "PDF", employees);

            return View();
        }



        private void RenderReport(string reportPath, string v, List<Employee> employees)
        {
            var localReport = new LocalReport { ReportPath = reportPath };

            //Give the collection a name (EmployeeCollection) so that we can reference it in our report designer
            var reportDataSource = new ReportDataSource("PRT_Emploee", employees);
            localReport.DataSources.Add(reportDataSource);

            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            var deviceInfo =
                string.Format(
                    "<DeviceInfo><OutputFormat>{0}</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>1in</MarginLeft><MarginRight>1in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>",
                    v);

            Warning[] warnings;
            string[] streams;

            //Render the report
            var renderedBytes = localReport.Render(
                v,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            //Clear the response stream and write the bytes to the outputstream
            //Set content-disposition to "attachment" so that user is prompted to take an action
            //on the file (open or save)
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=foo." + fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            Response.End();
        }

      
    }
}
