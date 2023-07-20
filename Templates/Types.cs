using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DictStatusToInterfaceDI.Types
{
    public class TimeslotVehicle
    {
		public int TerminalTimeslotVehicleId { get; set; }

		public string TerminalTimeslotVehicleUuid { get; set; }

		public string TTN { get; set; }

		[JsonProperty("TTN_date")]
		public DateTime? TTNDate { get; set; }
		public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public DateTime StatusChangeDateTime { get; set; }
    }
}



