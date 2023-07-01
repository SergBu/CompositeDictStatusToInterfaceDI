using DictStatusToInterfaceDI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.CompositeDictionary;

namespace DictStatusToInterfaceDI.CalculateStatusRules
{ 
	public class CancelledTerminalTimeslotVehicleStatusRule : ICalculateTerminalTimeslotVehicleStatusService
	{
		//private readonly TerminalTimeslotHelper _terminalTimeslotHelper;

		public CancelledTerminalTimeslotVehicleStatusRule()
		{
			//_terminalTimeslotHelper = terminalTimeslotHelper;
		}
		public List<TimeslotVehicle> Calculate(List<TimeslotVehicle> vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotInWaitingArea)
		{
			//var canceledVehicles = new List<TimeslotVehicle>();

			//foreach (var timeslotVehicle in vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotInWaitingArea)
			//{
			//	var inWaitingAreaEvent = timeslotVehicle.EventVehicles
			//	.Where(p => p.AreaType == AreaType.Waiting)
			//	.Where(p => p.EventType == EventType.VehicleInArea)
			//	.FirstOrDefault();

			//	var inUnloadingAreaEvent = timeslotVehicle.EventVehicles
			//	.Where(p => p.AreaType == AreaType.Unloading)
			//	.Where(p => p.EventType == EventType.VehicleInArea)
			//	.FirstOrDefault();

			//	var inCargoFrameAreaEvent = timeslotVehicle.EventVehicles
			//	.Where(p => p.AreaType == AreaType.CargoFrame)
			//	.Where(p => p.EventType == EventType.VehicleInArea)
			//	.FirstOrDefault();

			//	var inCityAreaEvent = timeslotVehicle.EventVehicles
			//	.Where(p => p.AreaType == AreaType.City)
			//	.Where(p => p.EventType == EventType.VehicleInArea)
			//	.FirstOrDefault();

			//	var weightUnloadedEvent = timeslotVehicle.EventTimeslotVehicles
			//		.Where(x => x.Parameters.WeightUnloaded > 0)
			//		.Where(x => x.EventType == EventType.DriverLoadingCompleted)
			//		.FirstOrDefault();


			//	var endTimeTimeslot = _terminalTimeslotHelper.EndDateTimeslot(timeslotVehicle);
			//	if (endTimeTimeslot < DateTime.UtcNow.AddHours(timeslotVehicle.Timeslot.Setting.Terminal?.TimeZoneOffset ?? 0))
			//	{
			//		var nextTimeslotVehicle = allTerminalTimeslotVehicles
			//			.Where(p => p.Vehicle.VehicleId == timeslotVehicle.Vehicle.VehicleId)
			//			.Where(p => p.Timeslot.TerminalTimeslotId != timeslotVehicle.Timeslot.TerminalTimeslotId)
			//			.FirstOrDefault();

			//		var endTimeNextTimeslot = _terminalTimeslotHelper.EndDateTimeslot(nextTimeslotVehicle);

			//		if (nextTimeslotVehicle == null || endTimeNextTimeslot < DateTime.UtcNow.AddHours(timeslotVehicle.Timeslot.Setting.Terminal?.TimeZoneOffset ?? 0))
			//		{
			//			if (inWaitingAreaEvent == null && inUnloadingAreaEvent == null && inCargoFrameAreaEvent == null && inCityAreaEvent == null && weightUnloadedEvent == null)
			//			{
			//				timeslotVehicle.Status = TerminalVehicleStatus.Cancelled;
			//				timeslotVehicle.StatusChangeDateTime = endTimeTimeslot;
			//				canceledVehicles.Add(timeslotVehicle);
			//			}

			//		}
			//	}
			//}

			return new List<TimeslotVehicle> { new TimeslotVehicle() { TerminalTimeslotVehicleId = 3 }};
		}


	}
}
