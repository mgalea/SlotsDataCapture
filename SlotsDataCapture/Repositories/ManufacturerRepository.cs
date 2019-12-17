using SlotsDataCapture.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlotsDataCapture.Repositories
{
    public class ManufacturerRepository
    {
        public IEnumerable<SelectListItem> GetManufacturers()
        {
            using (var context = new SDCEntities())
            {
                List<SelectListItem> manufacturers = context.Manufacturers.AsNoTracking()
                    .OrderBy(n => n.Name)
                    .Select(n =>
                        new SelectListItem
                        {
                            Value = n.ManufacturerID.ToString(),
                            Text = n.Name
                        }).ToList();
                var tooltip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select manufacturers ---"
                };
                manufacturers.Insert(0, tooltip);
                return new SelectList(manufacturers, "Value", "Text");
            }
        }
    }
}