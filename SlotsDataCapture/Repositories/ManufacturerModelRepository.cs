using SlotsDataCapture.ENTITIES;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SlotsDataCapture.Repositories
{
    public class ManufacturerModelRepository
    {
        public IEnumerable<SelectListItem> GetManufacturerModels()
        {
            List<SelectListItem> manufacturermodels = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return manufacturermodels;
        }

        public IEnumerable<SelectListItem> GetManufacturerModels(int manufacturerId)
        {
            if (manufacturerId > 0)
            {
                using (var context = new SDCEntities())
                {
                    IEnumerable<SelectListItem> manufacturermodels = context.ManufacturerModels.AsNoTracking()
                        .OrderBy(n => n.Name)
                        .Where(n => n.ManufacturerID == manufacturerId)
                        .Select(n =>
                            new SelectListItem
                            {
                                Value = n.ManufacturerID.ToString(),
                                Text = n.Name
                            }).ToList();
                    return new SelectList(manufacturermodels, "Value", "Text");
                }
            }
            return null;
        }

    }
}