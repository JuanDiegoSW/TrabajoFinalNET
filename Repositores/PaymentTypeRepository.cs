using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Repositores
{
    public class PaymentTypeRepository
    {
        private readonly RestaurantDBEntities restaurantDBEntities;
        public PaymentTypeRepository()
        {
            restaurantDBEntities = new RestaurantDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllPaymentType()
        {
            IEnumerable<SelectListItem> objSelectListItems = new List<SelectListItem>();

            objSelectListItems = (
                                    from obj in restaurantDBEntities.PaymentTypes
                                    select new SelectListItem
                                    {
                                        Text = obj.PaymentTypeName,
                                        Value = obj.PaymentTypeId.ToString(),
                                        Selected = true
                                    }
                                ).ToList();

            return objSelectListItems;
        }
    }
}