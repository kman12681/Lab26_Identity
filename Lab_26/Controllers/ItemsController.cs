using Lab_26.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab_26.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {

        private ItemDAO dao = new ItemDAO();

        // GET: Items
        public ActionResult Index()
        {
            return View(dao.GetItemList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = dao.GetItem((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Quantity,Price,Image")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dao.AddItem(item);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                ViewBag.Message = $"Something went wrong: {e.Message}";
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = dao.GetItem((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Quantity,Price,Image")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dao.EditItem(item);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                ViewBag.Message = $"Something went wrong: {e.Message}";
            }
            return View(item);
        }

        //GET: Items/Delete/5
        //public actionresult delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new httpstatuscoderesult(httpstatuscode.badrequest);
        //    }
        //    item item = dao.getitem((int)id);
        //    if (item == null)
        //    {
        //        return httpnotfound();
        //    }
        //    viewbag.item = item;
        //    return view(item);
        //}

        // POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            dao.DeleteItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult ItemList()
        {
            dao.GetItemList();
            List<Item> items = dao.GetItemList();
            ViewBag.Items = items;
            
            return View();
        }

        public ActionResult ViewCart()
        {
            return View("Cart");
        }

        public ActionResult Cart(int id)
        {

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of items
                List<Item> cart = new List<Item>();
                //add this item to it
                cart.Add((from i in dao.GetItemList() where i.ID == id select i).Single());
                //add the list to the session
                Session.Add("Cart", cart);
            }
            else
            {
                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);
                //add this item to it
                cart.Add((from i in dao.GetItemList() where i.ID == id select i).Single());
                //(add it back to the session)
                //Session["Cart"] = cart;
            }
            return View();
        }

        //Sort Items in Store
        public ActionResult ItemListSorted(string column)
        {

            // LINQ Query
            if (column == "Name")
            {
                ViewBag.Items = (from i in dao.GetItemList() orderby i.Name select i).ToList();
            }
            else if (column == "Description")
            {
                ViewBag.Items = (from i in dao.GetItemList() orderby i.Description select i).ToList();
            }
            else if (column == "Price")
            {
                ViewBag.Items = (from i in dao.GetItemList() orderby i.Price select i).ToList();

            }


            return View("ItemList");
        }

        public ActionResult ItemListByDescription(string description, string name)
        {

            List<Item> items = (from i in dao.GetItemList() where i.Description.Contains(description) select i).ToList();
            ViewBag.Items = items;

            return View("ItemList");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dao.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}