using DictStatusToInterfaceDI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.CompositeDictionary
{
	public interface ICalculateTerminalTimeslotVehicleStatusService
	{
		List<TimeslotVehicle> Calculate(List<TimeslotVehicle> terminalTimeslotVehicles);
	}
}
