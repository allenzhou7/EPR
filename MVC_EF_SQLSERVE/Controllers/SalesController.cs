using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MVC_EF_SQLSERVE.DAL;
using MVC_EF_SQLSERVE.Models;

namespace MVC_EF_SQLSERVE.Controllers
{
    public class SalesController : Controller
    {
        private SalesContext db = new SalesContext();

        // GET: Sales/Search
        public ActionResult Search()
        {
            Models.Filter filter = GetFilter();
            if (filter.FilterID > 0)
            {
                ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name", filter.CustomerID);
                return View(filter);
            }
            else
            {
                ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name");
                return View();
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "FilterID,CustomerID,StartDate,EndDate")] Models.Filter filter)
        {
            if (db.Filter.ToList().Any(r => r.FilterID == filter.FilterID))
            {
                if (!db.Filter.ToList().Any(r => r.FilterID == filter.FilterID && r.CustomerID == filter.CustomerID && r.StartDate == filter.StartDate && r.EndDate == filter.EndDate))
                {
                    RemoveHoldingEntityInContext(filter);
                    db.Entry(filter).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                db.Filter.Add(filter);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Sales
        public ActionResult Index()
        {
            return View(GetSales());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create(int? id)
        {
            if (id > 0)
            {
                var sale = db.Sale.Where(r => r.SaleID == id).FirstOrDefault();
                sale.SaleID = 0;
                ViewBag.ProductID = new SelectList(db.Product, "ProductID", "FullName", sale.ProductID);
                ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name", sale.CustomerID);
                return View(sale);
            }
            else
            {
                ViewBag.ProductID = new SelectList(db.Product, "ProductID", "FullName");
                ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name");
                return View();
            }
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SaleDate,CustomerID,ProductID,Number,Count,Price,Amount,Comments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sale.Add(sale);
                db.SaveChanges();
                UTIL.Common manager = new UTIL.Common();
                manager.UpdateFilter(sale.CustomerID);
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "FullName", sale.ProductID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name", sale.CustomerID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name");
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SaleDate,CustomerID,ProductID,Number,Count,Price,Amount,Comments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "FullName", sale.ProductID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "Name", sale.CustomerID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sale.Find(id);
            db.Sale.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ExportExcel: 
        public FileResult ExportExcel()
        {
            //return File(ExcelHandle(), "application/ms-excel", "出货记录.xls");
            return File(CSVHandle(), "text/comma-separated-values", "出货记录.csv");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Models.Filter GetFilter()
        {
            //Get Filter
            List<Models.Filter> filters = db.Filter.ToList();
            Models.Filter filter = new Models.Filter();
            if (filters.Count > 0)
            {
                filter = filters.Last();
            }
            return filter;
        }

        private List<Sale> GetSales()
        {
            //Get Filter
            Models.Filter filter = GetFilter();
            //
            if (filter.CustomerID != null && filter.StartDate != null && filter.EndDate != null)
                return (db.Sale.Where(s => s.CustomerID == filter.CustomerID && s.SaleDate >= filter.StartDate && s.SaleDate <= filter.EndDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID != null && filter.StartDate != null && filter.EndDate == null)
                return (db.Sale.Where(s => s.CustomerID == filter.CustomerID && s.SaleDate >= filter.StartDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID != null && filter.StartDate == null && filter.EndDate != null)
                return (db.Sale.Where(s => s.CustomerID == filter.CustomerID && s.SaleDate <= filter.EndDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID == null && filter.StartDate != null && filter.EndDate != null)
                return (db.Sale.Where(s => s.SaleDate >= filter.StartDate && s.SaleDate <= filter.EndDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID != null && filter.StartDate == null && filter.EndDate == null)
                return (db.Sale.Where(s => s.CustomerID == filter.CustomerID).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID == null && filter.StartDate != null && filter.EndDate == null)
                return (db.Sale.Where(s => s.SaleDate >= filter.StartDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else if (filter.CustomerID == null && filter.StartDate == null && filter.EndDate != null)
                return (db.Sale.Where(s => s.SaleDate <= filter.EndDate).OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
            else
                return (db.Sale.OrderBy(s => s.Product.FullName).OrderBy(s => s.SaleDate).ToList());
        }

        private bool RemoveHoldingEntityInContext(Models.Filter entity)
        {
            ObjectContext objContext = ((IObjectContextAdapter)db).ObjectContext;
            var objSet = objContext.CreateObjectSet<Models.Filter>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
            return (exists);
        }

        private string ExcelHandle()
        {
            string fileName = string.Empty;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (app == null)
                    return fileName;
                //Environment.CurrentDirectory +
                Microsoft.Office.Interop.Excel.Workbook workbook = app.Application.Workbooks.Open(@"C:\Project\Downloads\temp.xls", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);

                if (worksheet == null)
                    return fileName;

                //数据
                int columnCount = 10;
                string sLen = ((char)(64 + columnCount % 26)).ToString();
                List<Sale> Sales = GetSales();
                object[] obj = new object[columnCount];
                int rowIndex = 0;
                foreach (Sale sale in Sales)
                {
                    int columnIndex = 0;
                    obj[columnIndex++] = sale.Customer.Name;
                    obj[columnIndex++] = sale.SaleDate.ToString();
                    obj[columnIndex++] = sale.Product.Name;
                    obj[columnIndex++] = sale.Product.Length;
                    obj[columnIndex++] = sale.Product.Colour;
                    obj[columnIndex++] = sale.Number;
                    obj[columnIndex++] = sale.Count;
                    obj[columnIndex++] = sale.Price;
                    obj[columnIndex++] = sale.Amount;
                    obj[columnIndex++] = sale.Comments;

                    string cell1 = sLen + ((int)(rowIndex + 2)).ToString();
                    string cell2 = "A" + ((int)(rowIndex + 2)).ToString();
                    Microsoft.Office.Interop.Excel.Range ran = worksheet.get_Range(cell1, cell2);
                    ran.Value2 = obj;
                    rowIndex++;
                }
                //总金额
                object[] sum = new object[] { "总金额" };
                Microsoft.Office.Interop.Excel.Range lastRan1 = worksheet.get_Range("I" + ((int)(Sales.Count() + 2)).ToString(), "I" + ((int)(Sales.Count() + 2)).ToString());
                lastRan1.Value2 = sum;
                Microsoft.Office.Interop.Excel.Range lastRan2 = worksheet.get_Range("I" + ((int)(Sales.Count() + 3)).ToString(), "I" + ((int)(Sales.Count() + 3)).ToString());
                lastRan2.Formula = "=SUM(I2:I" + (Sales.Count() + 1) +  ")";
                //保存
                fileName = @"C:\Project\Downloads\temp2.xls";
                workbook.SaveCopyAs(fileName);
                workbook.Saved = true;

                return fileName;
            }
            finally
            {
                //关闭
                app.UserControl = false;
                app.Quit();
                app = null;
            }
        }

        private string CSVHandle()
        {
            string fileName = @"C:\Project\Downloads\temp2.csv";
            StreamWriter csvStreamWriter = new StreamWriter(fileName, false, System.Text.Encoding.UTF8);
            try
            {
                //output header data
                string delimiter = ",";
                string strHeader = "客户,日期,品名,规格,颜色,件数,数量,单价,金额,备注";
                csvStreamWriter.WriteLine(strHeader);

                List<Sale> Sales = GetSales();
                foreach (Sale sale in Sales)
                {
                    string strRowValue = "";
                    strRowValue += sale.Customer.Name + delimiter;
                    strRowValue += sale.SaleDate.ToString() + delimiter;
                    strRowValue += sale.Product.Name + delimiter;
                    strRowValue += sale.Product.Length + delimiter;
                    strRowValue += sale.Product.Colour + delimiter;
                    strRowValue += sale.Number + delimiter;
                    strRowValue += sale.Count + delimiter;
                    strRowValue += sale.Price + delimiter;
                    strRowValue += sale.Amount + delimiter;
                    strRowValue += sale.Comments;

                    csvStreamWriter.WriteLine(strRowValue);
                }
                if(Sales.Count > 0)
                {
                    string strRowValue = delimiter + delimiter + delimiter + delimiter
                        + delimiter + delimiter + delimiter + delimiter + "总金额";
                    csvStreamWriter.WriteLine(strRowValue);
                    strRowValue = delimiter + delimiter + delimiter + delimiter
                        + delimiter + delimiter + delimiter + delimiter + Sales.Sum(r => r.Amount) + delimiter;
                    csvStreamWriter.WriteLine(strRowValue);
                }
                csvStreamWriter.Close();
                return fileName;
            }
            finally
            {
                //关闭
                csvStreamWriter.Close();
            }
        }
    }
}
