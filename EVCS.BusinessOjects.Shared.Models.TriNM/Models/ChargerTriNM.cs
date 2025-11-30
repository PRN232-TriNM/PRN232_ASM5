using System;
using System.Collections.Generic;

namespace EVCS.BusinessOjects.Shared.Models.trinm.Models
{
    public partial class ChargerTriNM
    {
        public int ChargerTriNMId { get; set; }

        public int StationTriNMId { get; set; }

        public string ChargerTriNMType { get; set; }

        public bool IsAvailable { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<ChargingTransaction> ChargingTransactions { get; set; } = new List<ChargingTransaction>();

        public virtual StationTriNM StationTriNM { get; set; }
    }
}

