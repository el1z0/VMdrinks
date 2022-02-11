using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class VendingMachineDrink
    {
        public int Id { get; set; }
        public int? VendingMashineId { get; set; }
        public int? DrinksId { get; set; }
        public int Count { get; set; }

        public virtual Drink? Drinks { get; set; }
        public virtual VendingMachine? VendingMachines { get; set; }
    }
}
