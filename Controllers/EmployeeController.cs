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
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web;

namespace FulentNHirbent001.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly employeeServies _employeeServies = new employeeServies();
        private readonly rtpemployeeServies _rtpemployeeServies = new rtpemployeeServies();

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
        public JsonResult Details(int id)
        {
            IList<SqlParameter> pars = new List<SqlParameter> { new SqlParameter("Id", id) };
            IList<rtpEmployee> employees= _rtpemployeeServies.PushStoredProcedure("dbo.monkey", pars);

            return Json(employees, JsonRequestBehavior.AllowGet);
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


                _employeeServies.Insert(employee);

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
            //try
            //{
            //    using (ISession session = NHibernateHelper.OpenSession())
            //    {
            //        var employeetoUpdate = session.Get<Employee>(id);

            //        employeetoUpdate.Designation = employee.Designation;
            //        employeetoUpdate.FirstName = employee.FirstName;
            //        employeetoUpdate.LastName = employee.LastName;

            //        using (ITransaction transaction = session.BeginTransaction())
            //        {
            //            session.Save(employeetoUpdate);
            //            transaction.Commit();
            //        }
            //    }
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
             return View();
            //}
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
            var employees = _employeeServies.GetAll().ToList();
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


        #region printreprt
        public void PrintReport()
        {

            IList<SqlParameter> pars = new List<SqlParameter> { new SqlParameter("Id",1) };
            IList<rtpEmployee> employees = _rtpemployeeServies.PushStoredProcedure("dbo.monkey", pars);
            //string HTMLContent = "Hello <b>World</b>";
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(GetPDF(employees));
            Response.End();
        }

        public byte[] GetPDF(IList<rtpEmployee> employees)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
          

            // 1: create object of a itextsharp document class  
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            ////http://www.worldbestlearningcenter.com/index_files/csharp-pdf-pagesetup.htm

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file  
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document  
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document  
            doc.Open();
            doc.Add(new Paragraph(employees[0].FirstName));
            doc.Add(new Paragraph(employees[0].LastName));
            doc.Add(new Paragraph(employees[0].name));
            doc.Add(new Paragraph(employees[0].country));

            // 6: close the document and the worker  

            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }
#endregion

        

    }
}
