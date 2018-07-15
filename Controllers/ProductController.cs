using System;
using System.Linq;
using ClientNotifications;
using Microsoft.AspNetCore.Mvc;
using ProductMgmt.Data;
using ProductMgmt.Models;
using static ClientNotifications.Helpers.NotificationHelper;

namespace ProductMgmt.Controllers {
    public class ProductController : Controller {
        private ApplicationDbContext _context;
        private IClientNotification _clientNotification;
        public ProductController (ApplicationDbContext context, IClientNotification clientNotification) {
            _context = context;
            _clientNotification = clientNotification;

        }
        public IActionResult Index () {
            var products = _context.Products.ToList ();
            return View (products);
        }
        public IActionResult Detail (int id) {
            var product = _context.Products.FirstOrDefault (x => x.Id == id);
            return View (product);
        }

        [HttpGet]
        public IActionResult New () {
            return View ();
        }

        [HttpPost]
        public IActionResult New (Product product) {
            if (!ModelState.IsValid)
                return View (product);

            try {
                _context.Products.Add (product); // to save in database 
                _context.SaveChanges (); // to save in database

                _clientNotification.AddToastNotification ("Product added successfully",
                    NotificationType.success,
                    null);
            } catch (Exception) {
                _clientNotification.AddToastNotification ("Could not add product",
                    NotificationType.error,
                    null);
            }

            return RedirectToAction (nameof (Index));

        }
        public IActionResult Edit (int id) {
            return View ();
        }
        public IActionResult Delete (int id) {
            try {
                var product = _context.Products.FirstOrDefault (x => x.Id == id);

                _context.Products.Remove (product);
                _context.SaveChanges ();

                _clientNotification.AddToastNotification("Product removed successfully",
                                                             NotificationType.success,
                                                             null);
            } catch (Exception) {
                _clientNotification.AddToastNotification("Could not remove product",
                                                             NotificationType.success,
                                                             null);
            }

            return RedirectToAction (nameof (Index)); // to convert into string form you hav 2 option i.e. "Index" or nameof(Index)
        }
    }

}