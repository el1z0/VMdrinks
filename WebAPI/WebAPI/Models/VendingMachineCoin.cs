using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class VendingMachineCoin
    {
        public int Id { get; set; }
        public int? VendingMashineId { get; set; }
        public int? CoinsId { get; set; }
        public int Count { get; set; }
        public int IsActive { get; set; }

        public virtual Coin? Coins { get; set; }
        public virtual VendingMachine? VendingMachines { get; set; }
    }
}
