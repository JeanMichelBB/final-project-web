using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRental.Models;

namespace PropertyRental.Controllers
{
    public class BuildingsController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        // GET: Buildings
        public ActionResult Index()
        {
            var buildings = db.Buildings.Include(b => b.Address);
            return View(buildings.ToList());
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingID,AddressID,NumberOfFloors,ConstructionYear,Amenities,BuildingName,StreetName,StreetNumber,City,PostalCode,Country,Province")] BuildingViewModel buildingView)
        {
            Address address;
            Building building;
            if (ModelState.IsValid)
            {
                address = new Address
                {
                    StreetName = buildingView.StreetName,
                    StreetNumber = buildingView.StreetNumber,
                    City = buildingView.City,
                    PostalCode = buildingView.PostalCode,
                    Country = buildingView.Country,
                    Province = buildingView.Province
                };
                building = new Building
                {
                    NumberOfFloors = buildingView.NumberOfFloors,
                    ConstructionYear = buildingView.ConstructionYear,
                    Amenities = buildingView.Amenities,
                    BuildingName = buildingView.BuildingName
                };
                db.Addresses.Add(address);
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildingView);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuildingViewModel buildingModel = (from b in db.Buildings
                                                   join a in db.Addresses on b.AddressID equals a.AddressID
                                                   where b.BuildingID == id
                                                   select new BuildingViewModel
                                                    {
                                                   BuildingID = b.BuildingID,
                                                   AddressID = b.AddressID,
                                                   NumberOfFloors = b.NumberOfFloors,
                                                   ConstructionYear = b.ConstructionYear,
                                                   Amenities = b.Amenities,
                                                   BuildingName = b.BuildingName,
                                                   StreetName = a.StreetName,
                                                   StreetNumber = a.StreetNumber,
                                                   City = a.City,
                                                   PostalCode = a.PostalCode,
                                                   Country = a.Country,
                                                   Province = a.Province
                                               }).FirstOrDefault();
            if (buildingModel == null)
            {
                return HttpNotFound();
            }
            return View(buildingModel);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingID,NumberOfFloors,ConstructionYear,Amenities,BuildingName,StreetName,StreetNumber,City,PostalCode,Country,Province")] BuildingViewModel buildingModel)
        {
            if (ModelState.IsValid)
            {
                // Load the existing building entity from the database
                var existingBuilding = db.Buildings
                    .Include(b => b.Address)  // Include the related address
                    .Single(b => b.BuildingID == buildingModel.BuildingID);

                // Update the building properties
                existingBuilding.NumberOfFloors = buildingModel.NumberOfFloors;
                existingBuilding.ConstructionYear = buildingModel.ConstructionYear;
                existingBuilding.Amenities = buildingModel.Amenities;
                existingBuilding.BuildingName = buildingModel.BuildingName;

                // Update the address properties
                existingBuilding.Address.StreetName = buildingModel.StreetName;
                existingBuilding.Address.StreetNumber = buildingModel.StreetNumber;
                existingBuilding.Address.City = buildingModel.City;
                existingBuilding.Address.PostalCode = buildingModel.PostalCode;
                existingBuilding.Address.Country = buildingModel.Country;
                existingBuilding.Address.Province = buildingModel.Province;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildingModel);
        }


        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            db.Buildings.Remove(building);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
