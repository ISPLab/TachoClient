using System;
using System.Collections.Generic;
using System.Reflection;

namespace TachoData
{
    [ObfuscationAttribute(Exclude = true)]
    public class TachoResult
	{
		public string HolderSurname { get; set; }
		public string HolderFirstNames { get; set; }
		public List<TachoLegislation> AllLegislations { get; set; }
		public TachoLegislation Legislation { get; set; }
		public DateTime CardHolderBirthDate { get; set; }
		public string CardNumber { get; set; }
		public string DrivingLicenceIssuingNation { get; set; }
		public string DrivingLicenceNumber { get; set; }
		public DateTime CardExpiryDate { get; set; }
		public DateTime CardIssueDate { get; set; }
		public DateTime CardValidityBegin { get; set; }
		public List<TachoViolation> Violations { get; set; }
		public string DrivingLicenceIssuingAuthority { get; set; }

		public string Dddfilename { get; set; }
		public byte[] dddfile { get; set; }

		public DateTime Beginreportdatetime { get; set; }
		public DateTime Endreportdatetime { get; set; }
		public string DriverWorkSettings { get; set; }
		public DateTime VehicleLastUsed { get; set; }
		public string VehicalNumber { get; set; }
		public Guid Guid { get; set; }
		public DDDSignResult DDDSignResult { get; set; }
		public List<CardVehicle> VehiclesUsed { get; set; }
		public List<Shift> Shifts { get; set; }

	}

    [ObfuscationAttribute(Exclude = true)]
    public enum P8SignatureCheckResult
    {
        Unknown = 0,
        OK = 1,
        Error = 2
    }
    [ObfuscationAttribute(Exclude = true)]
    public class CheckedFiles
    {
        public P8SignatureCheckResult Result { get; set; }
        public string FileName { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
    }

    [ObfuscationAttribute(Exclude = true)]
    public class DDDSignResult
    {
        public P8SignatureCheckResult CommonResult { get; set; }
        public List<CheckedFiles> Files { get; set; }
        public string Description { get; set; }
    }

    [ObfuscationAttribute(Exclude = true)]
    public class Activity
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public P8DriverActivity ActivityType { get; set; }
        public P8DrivingStatus DrivingStatus { get; set; }
        public string DurationText { get; set; }
        public bool IsFerryCrossing { get; set; }
        public bool IsRecreation { get; set; }
        public string StatusText { get; set; }
    }

    [ObfuscationAttribute(Exclude = true)]
    public enum P8DriverActivity : byte
    {
        BreakOrRest = 0,
        Availability = 1,
        OtherWork = 2,
        Driving = 3,
        Unknown = 4,
        NotAvailable = 255
    }
    [ObfuscationAttribute(Exclude = true)]
    public enum P8DrivingStatus : byte
    {
        Single = 0,
        Crew = 1,
        Known = 2,
        Unknown = 2,
        NotAvailable = 255
    }
    [ObfuscationAttribute(Exclude = true)]
    public class Shift
    {
        public List<Activity> Activities = new List<Activity>();
        public string SummaryAvailabilityDurationText { get; set; }
        public string SummaryBreakOrRestDurationText { get; set; } //Отдых в смене и в МСО
        public string SummaryDrivingDurationText { get; set; }
        public string SummaryNoDataDurationText { get; set; } //Нет данных в смене и в МСО
        public string SummaryOtherWorkDurationText { get; set; }
        public string SummaryWorkTimeDurationText { get; set; }
        public string SummaryBreakOrRestOnShiftDurationText { get; set; } //Отдых только в смене
        public string SummaryNoDataOnShiftDurationText { get; set; } //Нет данных только в смене     
        public string SummaryRecreationDurationText { get; set; } //Отдых + Нет данных только в МСО
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string RecreationInfo { get; set; }
        //public int RecreationInfoTotalHours { get;  set; }
    }
    [ObfuscationAttribute(Exclude = true)]
    public class CardVehicle
    {
        public int vehicleOdometerBegin { get; set; }
        public int vehicleOdometerEnd { get; set; }
        public DateTime vehicleFirstUse { get; set; }
        public DateTime vehicleLastUse { get; set; }
        public string vehicleRegistrationNation { get; set; }
        public string vehicleRegistrationNumber { get; set; }
    }
}
