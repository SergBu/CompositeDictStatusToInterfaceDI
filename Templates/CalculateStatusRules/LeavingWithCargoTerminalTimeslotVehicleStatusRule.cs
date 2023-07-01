using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.CompositeDictionary;
using DictStatusToInterfaceDI.Types;

namespace CompositeDictStatusToInterfaceDI.CalculateStatusRules
{
	public class LeavingWithCargoTerminalTimeslotVehicleStatusRule : ICalculateTerminalTimeslotVehicleStatusService
	{
		public List<TimeslotVehicle> Calculate(List<TimeslotVehicle> vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrame)
		{
			//return (from a in vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrame
			//		let inCityEvent = a.EventVehicles
			//		.Where(x => x.AreaType == AreaType.City
			//		&& x.EventType == EventType.VehicleInArea).FirstOrDefault()
			//		let call = a.Calls
			//		.Where(x => x.AreaType == AreaType.Unloading
			//		&& x.EventType == EventType.DriverInArea)
			//		where inCityEvent != null && !call.Any()
			//		select new TimeslotVehicle
			//		{
			//			TerminalTimeslotVehicleId = a.TerminalTimeslotVehicleId,
			//			Status = TerminalVehicleStatus.InCityAreaWithoutCall,
			//			StatusChangeDateTime = inCityEvent.Date

			//		}).ToList();

			return new List<TimeslotVehicle> { new TimeslotVehicle() { TerminalTimeslotVehicleId = 10 }};
		}
	}
}