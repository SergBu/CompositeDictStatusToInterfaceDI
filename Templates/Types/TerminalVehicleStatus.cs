using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Templates.CompositeDictionary
{
	/// <summary>
	/// Статус нахождения машины в той или иной зоне
	/// </summary>
	public enum TerminalVehicleStatus : byte
	{
		/// <summary>
		/// По умолчанию
		/// </summary>
		[DataMember]
		None = 0,

		/// <summary>
		/// Разгружен - Произошло событие "покинул Зону Разгрузки" и внесена разгруженная масса
		/// </summary>
		[DataMember]
		Unloaded = 1,

		/// <summary>
		/// Выезд с грузом - Произошло событие "покинул Зону Разгрузки" и НЕ внесена разгруженная масса
		/// </summary>
		[DataMember]
		LeavingWithCargo = 2,

		/// <summary>
		/// В ЗР - Произошло событие "въехал в Зону Разгрузки" и НЕ произошло событие "покинул Зону Разгрузки"
		/// </summary>
		[DataMember]
		InUnloadingArea = 3,

		/// <summary>
		/// В пути к ЗР	- Произошло событие  "а/м попала в Грузовой каркас"
		/// </summary>
		[DataMember]
		OnRouteToUnloadingArea = 4,

		/// <summary>
		/// Въехал в город без вызова - Произошло событие "въехал в Город" и НЕТ отметки о вызове от АТ
		/// </summary>
		[DataMember]
		InCityAreaWithoutCall = 5,

		/// <summary>
		/// В ЗО - Произошло событие "а/м попала в Зону Ожидания"
		/// </summary>
		[DataMember]
		InWaitingArea = 6,

		/// <summary>
		/// Отменен - Таймслот прошел + НЕТ в текущих сутках нового таймслота
		/// </summary>
		[DataMember]
		Cancelled = 7,

		/// <summary>
		/// Погружен - Внесена масса погруженная нетто
		/// </summary>
		[DataMember]
		Loaded = 8,

		/// <summary>
		/// Ожидаются - назначена на таймслот, но не приехала в зону разгрузки
		/// </summary>
		[DataMember]
		Arriving = 9,

		/// <summary>
		/// В ЗГ
		/// </summary>
		[DataMember]
		InCityArea = 10,

		/// <summary>
		///Остановилась в городе 
		/// </summary>
		[DataMember]
		IdleInCity = 11,

		/// <summary>
		/// Покинул зону ожидания
		/// </summary>
		[DataMember]
		LeavingWaitingArea = 12,

		/// <summary>
		/// Удалено
		/// </summary>
		[DataMember]
		Deleted = 13,

		/// <summary>
		/// Возврат
		/// </summary>
		ReturnToLoading = 14,

		/// <summary>
		/// В другой порт
		/// </summary>
		VehicleRedirectedToAnotherTerminal = 15
	}
}