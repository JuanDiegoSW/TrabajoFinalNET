using Restaurant.Models;
using Restaurant.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Restaurant.ViewModels;
using System.Data.Entity;
namespace Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly RestaurantDBEntities restaurantDBEntities;
        public OrderController()
        {
            restaurantDBEntities = new RestaurantDBEntities();
        }
        // GET: Order
        [Authorize]
        public ActionResult Index()
        {
            var objCustomerRepository = new CustomerRepository();
            var objItemRepository = new ItemRepository();
            var objPaymentRepository = new PaymentTypeRepository();
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>(
                    objCustomerRepository.GetAllCustomers(), objItemRepository.GetAllItems(), objPaymentRepository.GetAllPaymentType());

            return View(objMultipleModels);
        } 
        [Authorize]
        public ActionResult List()
        {
            var orders = restaurantDBEntities.Orders.Include(i => i.Customers);
            return View(orders.ToList());
        }

        [HttpGet]

        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = restaurantDBEntities.Items.Single(model => model.itemId == itemId).itemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            bool isStatus = objOrderRepository.AddOrder(objOrderViewModel);
            string SuccessMessage = String.Empty;

            if (isStatus)
            {
                SuccessMessage = "Your Order Has Been Successfully Placed.";
            }
            else
            {
                SuccessMessage = "There Is Some Issue While Placing Order.";
            }
            return Json(SuccessMessage, JsonRequestBehavior.AllowGet);
        }
    }
}