using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Repositores
{
    public class ItemRepository
    {
        private readonly RestaurantDBEntities restaurantDBEntities;
        private RestaurantDBEntities db = new RestaurantDBEntities();
        public ItemRepository()
        {
            restaurantDBEntities = new RestaurantDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            IEnumerable<SelectListItem> objSelectListItems = new List<SelectListItem>();

            objSelectListItems = (
                                    from obj in restaurantDBEntities.Items
                                    select new SelectListItem
                                    {
                                        Text = obj.itemName,
                                        Value = obj.itemId.ToString(),
                                        Selected = true
                                    }
                                ).ToList();

            return objSelectListItems;
        }
    }
}