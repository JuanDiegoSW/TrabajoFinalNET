using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Models;
using Restaurant.ViewModels;

namespace Restaurant.Repositores
{
    public class OrderRepository
    {
        private readonly RestaurantDBEntities restaurantDBEntities;
        public OrderRepository()
        {
            restaurantDBEntities = new RestaurantDBEntities();
        }

        public bool AddOrder(OrderViewModel orderViewModel)
        {
            try
            {
                Orders objOrder = new Orders()
                {
                    CustomerId = orderViewModel.CustomerId,
                    FinalTotal = orderViewModel.FinalTotal,
                    OrderDate = orderViewModel.OrderDate,
                    OrderNumber = String.Format("{0:ddmmyyyyhhmmss}", DateTime.Now),
                    PaymentTypedId = orderViewModel.PaymentTypeId,
                };
                restaurantDBEntities.Orders.Add(objOrder);
                restaurantDBEntities.SaveChanges();

                foreach (var item in orderViewModel.listOrderDetailViewModel)
                {
                    var objOrderDetails = new OrderDetails()
                    {
                        Discount = item.Discount,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        Orderid = objOrder.OrderId,
                        Total = item.Total,
                        UnitPrice = item.UnitPrice
                    };
                    restaurantDBEntities.OrderDetails.Add(objOrderDetails);
                    restaurantDBEntities.SaveChanges();

                    Transactions objTransaction = new Transactions()
                    {
                        ItemId = item.ItemId,
                        Quantity = (-1) * item.Quantity,
                        TransactionDate = orderViewModel.OrderDate,
                        TransactionId = 2
                    };
                    restaurantDBEntities.Transactions.Add(objTransaction);
                    restaurantDBEntities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}