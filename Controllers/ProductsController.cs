using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDemo.Models;
using System.Data;
using System.Data.Entity;
using PagedList;
using System.Net;
namespace WebDemo.Controllers
{
    public class ProductsController : Controller
    {
        private DemoTibox dt = new DemoTibox();

        // GET: Products
        public ActionResult Index(string sortOrder, int? page)
        {
            if (Session["sesion"] != null && Session["sesion"].ToString() == "True")
            {
                ViewBag.order = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
                var prod = from s in dt.Producto select s;
                var productos = prod;
                switch (sortOrder)
                {
                    case "Nombre":
                        productos = prod.OrderBy(s => s.nomProducto);
                        break;
                    case "Nombre_desc":
                        productos = prod.OrderByDescending(s => s.nomProducto);
                        break;
                    case "Valor":
                        productos = prod.OrderBy(s => s.valorProducto);
                        break;
                    case "valor_desc":
                        productos = prod.OrderByDescending(s => s.valorProducto);
                        break;
                    case "Stock":
                        productos = prod.OrderBy(s => s.stockProducto);
                        break;
                    case "stock_desc":
                        productos = prod.OrderByDescending(s => s.stockProducto);
                        break;
                    default:
                        productos = prod.OrderBy(s => s.idProducto);
                        break;
                }
                //var productos = prod;
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(productos.ToPagedList(pageNumber,pageSize));
            }
            else { return RedirectToAction("Login", "Login"); }
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            if (Session["sesion"] != null && Session["sesion"].ToString() == "True")
            {
                Producto producto = dt.Producto.Find(id);
                return View(producto);
            }
            else { return RedirectToAction("index", "Home"); }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["sesion"] != null && Session["sesion"].ToString() == "True")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.Producto.Add(collection);
                    dt.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["sesion"] != null && Session["sesion"].ToString() == "True")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = dt.Producto.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.Entry(producto).State = EntityState.Modified;
                    dt.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["sesion"] != null && Session["sesion"].ToString() == "True")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = dt.Producto.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Producto producto = dt.Producto.Find(id);
                dt.Producto.Remove(producto);
                dt.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
    
    public class ProductoController : System.Web.Http.ApiController
    {
        private DemoTibox dt = new DemoTibox();
        // GET: api/producto
        public IEnumerable<Producto> Get()
        {
            return dt.Producto.ToList();
        }
    }
}
