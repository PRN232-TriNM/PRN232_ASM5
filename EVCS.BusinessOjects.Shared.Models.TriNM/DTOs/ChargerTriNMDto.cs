using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVCS.BusinessOjects.Shared.Models.trinm.DTOs
{
    public class ChargerTriNMDto
    {
        public int? ChargerTriNMId { get; set; }
        public int StationTriNMId { get; set; }
        public string? ChargerTriNMType { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageURL { get; set; }
    }
}

