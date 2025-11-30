using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVCS.BusinessOjects.Shared.Models.trinm.DTOs
{
    public class StationTriNMDto
    {
        public int? StationTriNMid { get; set; }
        public string ConnectorType { get; set; }
        public decimal? PowerCapacity { get; set; }
        public string? Status { get; set; }
        public DateTime? InstalledDate { get; set; }
        public bool? IsAvailable { get; set; }
        public string? SerialNumber { get; set; }
        public string? FirmwareVersion { get; set; }
        public string? Notes { get; set; }
    }
}
