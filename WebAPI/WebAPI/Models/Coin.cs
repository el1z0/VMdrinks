using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Coin
    {
        public Coin()
        {
            VendingMachineCoins = new HashSet<VendingMachineCoin>();
        }

        public int Id { get; set; }
        public int Denomination { get; set; }
        public virtual ICollection<VendingMachineCoin> VendingMachineCoins { get; set; }
    }
}
