using DictStatusToInterfaceDI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.CompositeDictionary
{
	public class CompositeCalculateTerminalTimeslotVehicleStatusService : ICalculateTerminalTimeslotVehicleStatusService
	{
		private readonly Dictionary<TerminalVehicleStatus, ICalculateTerminalTimeslotVehicleStatusService> _rulesDictionary;

		public CompositeCalculateTerminalTimeslotVehicleStatusService(
			Dictionary<TerminalVehicleStatus, ICalculateTerminalTimeslotVehicleStatusService> rulesDictionary)
		{
			_rulesDictionary = rulesDictionary;
		}
		public List<TimeslotVehicle> Calculate(List<TimeslotVehicle> terminalTimeslotVehicles, string terminalSetting = null, List<string> allTerminalTimeslotVehicles = null)
		{
			var res = new List<TimeslotVehicle>();

			var deletedVehicle = _rulesDictionary[TerminalVehicleStatus.Deleted].Calculate(terminalTimeslotVehicles);

			res.AddRange(deletedVehicle);

			var deletedIds = deletedVehicle.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var notDeletedVehicle = terminalTimeslotVehicles.Where(x => !deletedIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var redirectedToAnotherTerminalVehicles = _rulesDictionary[TerminalVehicleStatus.VehicleRedirectedToAnotherTerminal].Calculate(notDeletedVehicle);

			res.AddRange(redirectedToAnotherTerminalVehicles);

			var redirectedToAnotherTerminalVehiclesIds = redirectedToAnotherTerminalVehicles.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var nonDeletedNonRedirectedVehicles = notDeletedVehicle.Where(x => !redirectedToAnotherTerminalVehiclesIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var returnToLoadingVehicles = _rulesDictionary[TerminalVehicleStatus.ReturnToLoading].Calculate(nonDeletedNonRedirectedVehicles);

			res.AddRange(returnToLoadingVehicles);

			var returnToLoadingVehiclesIds = returnToLoadingVehicles.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var nonDeletedNonRedirectedVehiclesNonReturnToLoading = nonDeletedNonRedirectedVehicles.Where(x => !returnToLoadingVehiclesIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var vehiclesWithOutUnloadingAreaEvent = (from a in nonDeletedNonRedirectedVehiclesNonReturnToLoading
													 //from b in a.EventVehicles
													 //where b.AreaType == AreaType.Unloading && b.EventType == EventType.VehicleOutArea
													 select a).ToList();


			var unloadedVehicles = _rulesDictionary[TerminalVehicleStatus.Unloaded]
				.Calculate(vehiclesWithOutUnloadingAreaEvent);

			res.AddRange(unloadedVehicles);

			var leavingWithCargoVehicles = _rulesDictionary[TerminalVehicleStatus.LeavingWithCargo]
				.Calculate(vehiclesWithOutUnloadingAreaEvent);

			res.AddRange(leavingWithCargoVehicles);

			var notUnloadedAndNotLeavingWithCargoVehiclesIds = res.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var notUnloadedAndNotLeavingWithCargoVehicles = notDeletedVehicle
				.Where(x => !notUnloadedAndNotLeavingWithCargoVehiclesIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var vehiclesInUnloadingArea = _rulesDictionary[TerminalVehicleStatus.InUnloadingArea]
				.Calculate(notUnloadedAndNotLeavingWithCargoVehicles);

			res.AddRange(vehiclesInUnloadingArea);

			var vehiclesInUnloadingAreaIds = vehiclesInUnloadingArea.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingArea = notUnloadedAndNotLeavingWithCargoVehicles
				.Where(x => !vehiclesInUnloadingAreaIds.Contains(x.TerminalTimeslotVehicleId)).ToList();



			var vehiclesInCityAreaWithoutCall = _rulesDictionary[TerminalVehicleStatus.InCityAreaWithoutCall]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingArea);

			res.AddRange(vehiclesInCityAreaWithoutCall);

			var vehiclesInCityAreaWithoutCallIds = vehiclesInCityAreaWithoutCall.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCall =
				vehicleNotUnloadNotLeavingWithCargoNotInUnloadingArea.Where(x => !vehiclesInCityAreaWithoutCallIds.Contains(x.TerminalTimeslotVehicleId)).ToList();


			var vehiclesIdleInCity = _rulesDictionary[TerminalVehicleStatus.IdleInCity]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCall);

			res.AddRange(vehiclesIdleInCity);

			var vehiclesIdleInCityIds = vehiclesIdleInCity.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCity =
				vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCall.Where(x => !vehiclesIdleInCityIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var vehiclesInCargoFrame = _rulesDictionary[TerminalVehicleStatus.OnRouteToUnloadingArea]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCity);

			res.AddRange(vehiclesInCargoFrame);

			var vehiclesInCargoFrameIds = vehiclesInCargoFrame.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehiclesNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCityNotInCargoFrame = vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCity
				.Where(x => !vehiclesInCargoFrameIds.Contains(x.TerminalTimeslotVehicleId)).ToList();



			var vehiclesOutWaitingArea = _rulesDictionary[TerminalVehicleStatus.LeavingWaitingArea]
				.Calculate(vehiclesNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCityNotInCargoFrame);

			res.AddRange(vehiclesOutWaitingArea);

			var vehiclesOutWaitingAreaIds = vehiclesOutWaitingArea.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotIdleInCityOutWaitingArea =
				vehiclesNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCityWithoutCallNotIdleInCityNotInCargoFrame
				.Where(x => !vehiclesOutWaitingAreaIds.Contains(x.TerminalTimeslotVehicleId)).ToList();


			var vehiclesInWaitingArea = _rulesDictionary[TerminalVehicleStatus.InWaitingArea]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotIdleInCityOutWaitingArea);


			res.AddRange(vehiclesInWaitingArea);

			var vehiclesInWaitingAreaIds = vehiclesInWaitingArea.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotOutWaitingNotInWaitingArea =
				vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotIdleInCityOutWaitingArea
				.Where(x => !vehiclesInWaitingAreaIds.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var canceledVehicles = _rulesDictionary[TerminalVehicleStatus.Cancelled]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotOutWaitingNotInWaitingArea);

			res.AddRange(canceledVehicles);

			var canceledVehiclesId = canceledVehicles.Select(x => x.TerminalTimeslotVehicleId).ToList();

			var vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotInWaitingAreaNotCanceled =
				vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotOutWaitingNotInWaitingArea.Where(x => !canceledVehiclesId.Contains(x.TerminalTimeslotVehicleId)).ToList();

			var loadedVehicles = _rulesDictionary[TerminalVehicleStatus.Loaded]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotInWaitingAreaNotCanceled);

			res.AddRange(loadedVehicles);

			var arrivedVehicles = _rulesDictionary[TerminalVehicleStatus.Arriving]
				.Calculate(vehicleNotUnloadNotLeavingWithCargoNotInUnloadingAreaNotInCargoFrameNotInCityWithoutCallNotInWaitingAreaNotCanceled);

			res.AddRange(arrivedVehicles);

			//res.ForEach(x => x.StatusChangeDateTime = x.StatusChangeDateTime?.AddHours(terminalSetting.Terminal?.TimeZoneOffset ?? 0));

			return res;
		}

        public List<TimeslotVehicle> Calculate(List<TimeslotVehicle> terminalTimeslotVehicles)
        {
			return Calculate(terminalTimeslotVehicles, null, null);
		}
    }
}
