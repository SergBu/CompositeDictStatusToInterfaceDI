using DictStatusToInterfaceDI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.CompositeDictionary;

namespace DictStatusToInterfaceDI.CalculateStatusRules
{
	public class OnRouteToTerminalTerminalTimeslotVehicleStatusRule : ICalculateTerminalTimeslotVehicleStatusService
	{
		public List<TimeslotVehicle> Calculate(List<TimeslotVehicle> vehicleNotUnloadNotLeavingWithCargoNotInUnloadingArea)
		{

			//var vehiclesInCargoFrame = (from a in vehicleNotUnloadNotLeavingWithCargoNotInUnloadingArea
			//							let inCargoFrameEvent = a.EventVehicles
			//							.Where(x => (x.AreaType == AreaType.CargoFrame
			//							&& x.EventType == EventType.VehicleInArea) || (x.AreaType == AreaType.City
			//							&& x.EventType == EventType.VehicleInArea)).FirstOrDefault()
			//							where inCargoFrameEvent != null
			//							select new TimeslotVehicle
			//							{
			//								TerminalTimeslotVehicleId = a.TerminalTimeslotVehicleId,
			//								Status = TerminalVehicleStatus.OnRouteToUnloadingArea,
			//								StatusChangeDateTime = inCargoFrameEvent.Date

			//							}).ToList();

			//return vehiclesInCargoFrame;
			return new List<TimeslotVehicle> { new TimeslotVehicle() { TerminalTimeslotVehicleId = 12 }};

		}
	}
}
