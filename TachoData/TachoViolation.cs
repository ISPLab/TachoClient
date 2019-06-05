using System;
using System.ComponentModel;

namespace TachoData
{



	public class TachoViolation 
	{
		public const string fnBeginTime = "beginTime";
		public const string fnEndTime = "endTime";
		public const string fnDescription = "description";
		public const string fnRegimentedValue = "regimentedValue";
		public const string fnActualValue = "actualValue";
		public const string fnArticleReference = "articleReference";
		public const string fnLegislation = "legislation";
		public const string fnReportIntervalType = "reportIntervalType";
		public const string fnViolationType = "violationType";
		public const string fnViolationCode = "violationCode";

		/// <summary>
		/// Дата и время начала нарушения
		/// </summary>
		public DateTime BeginTime { get; set; }

		/// <summary>
		/// Дата и время окончания нарушения
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// Описание нарушения
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Регламентированное значение
		/// </summary>
		public string RegimentedValue { get; set; }

		/// <summary>
		/// Фактическое значение
		/// </summary>
		public string ActualValue { get; set; }

		/// <summary>
		/// Фактическое значение
		/// </summary>
		public double TotalHours { get; set; }

		/// <summary>
		/// Основание для квалификации факта, как нарушения (регламентирующий документ)
		/// </summary>
		public string ArticleReference { get; set; }

		/// <summary>
		/// Законодательство
		/// </summary>
		public Legislation Legislation { get; set; }

		/// <summary>
		/// Тип интервала в отчёте
		/// </summary>
		public ReportIntervalType ReportIntervalType { get; set; }

		/// <summary>
		/// Агрегированный Вид нарушения
		/// </summary>
		public WorkAndRestViolationType ViolationType { get; set; }

		/// <summary>
		/// Код нарушения
		/// </summary>
		public WorkAndRestViolationCode ViolationCode { get; set; }

		/// <summary>
		/// заданный отдых между сменами 
		/// </summary>
		public string CommentAfterAnalyze { get; set; }
		public Guid Guid { get; set; }
		public string ReportInterval { get; set; }
		public string ViolationTypeDesc { get; set; }

		public TachoViolation()
		{
			ViolationCode = WorkAndRestViolationCode.Unknown;
		}

	}

    public enum Legislation : short
    {
        Unknown = 0,

        //[Description("ЕСТР")]
        European = 1,

        //http://www.rg.ru/2014/06/24/mintrans-dok.html
        //http://www.rg.ru/2004/11/10/voditeli-doc.html
        //[Description("Приказ №15 Министерства транспорта (с изменениями от 10 июня 2014 г.)")]
        RussianAct15 = 2
    }

    public enum ReportIntervalType
    {
        [Description("Не определенный")]
        Unknown = 0,

        [Description("Смена")]
        WorkShift = 1,

        [Description("Междусменный отдых")]
        RecreationPeriod = 2,

        [Description("Одна неделя")]
        OneWeek = 3,

        [Description("Две недели")]
        TwoWeeks = 4,

        [Description("Интервал управления")]
        DrivingInterval = 5,

        [Description("Календарный год")]
        CalendarYear = 6,

        [Description("Календарный месяц")]
        CalendarMonth = 7,

        [Description("Календарные сутки")]
        CalendarDay = 8,

        [Description("24 часа")]
        Hours24 = 9,

        [Description("30 часов")]
        Hours30 = 10,

        [Description("48 часов")]
        Hours48 = 11,

        [Description("Рабочая неделя")]
        WorkWeek = 12,

        [Description("Интервал компенсации")]
        CompensationInterval = 13,

        [Description("Календарный квартал")]
        CalendarQuarter = 14,

        [Description("Календарные полгода")]
        CalendarSixMonths = 15,

        [Description("Учётный период")]
        AccountPeriod = 16
    }

    public enum WorkAndRestViolationType
    {
        [Description("Неизвестное нарушение")]
        Unknown = 0,
        [Description("Превышение предельного времени работы за смену")]
        ExceedingLimitWorkingTimeShift = 0x01,
        [Description("Превышение предельного времени управления")]
        ExceedingLimitDrivingTime = 0x02,

        [Description("Превышение предельного времени управления в неделю")]
        ExceedingLimitDrivingTimeInOneWeek = 0x03,

        [Description("Превышение предельного времени управления за две недели")]
        ExceedingLimitDrivingTimeInTwoWeeks = 0x04,


        [Description("Нарушение длительности перерывов за смену")]
        ViolationDurationBreakForShift = 0x05,

        [Description("Нарушение интервалов управления в смене")]
        ViolationIntervalsContinuousDrivingInShift = 0x06,

        [Description("Нарушение длительности междусменного отдыха")]
        ViolationDurationRestBetweenShifts = 0x07,
        //[Description("Нарушение правил применения отложенного еженедельного отдыха")]
        //ViolationRulesApplicationDeferredWeekRest = 0x08,
        [Description("Нарушение правил использования еженедельных отдыхов")]
        ViolationRulesUseWeeklyRest = 0x09,
        [Description("Некомпенсированное сокращение отдыха")]
        UncompensatedReductionVacation = 0x0A,
        [Description("Наличие неопределённой деятельности")]
        HaveUncertainActivity = 0x0B,
        [Description("Нарушение порядка выполнения сверхурочных работ")]
        ViolationOrderDoingOvertime = 0x0C,
        [Description("Нарушение оформления документов на выполнение сверхурочных работ")]
        ViolationPaperworkPerformOvertimeWork = 0x0D,
    }
    public enum WorkAndRestViolationCode
    {
        Unknown = 0x00,
        [Description("Превышение предельного времени работы за смену при неизвестной системе учёта")]
        ExceedingLimitWorkingTimeShift_UnknownSystemAccount = 0x00010001, //РФ
        [Description("Превышение предельного времени работы за смену при суммированной системе учёта")]
        ExceedingLimitWorkingTimeShift_SummarySystemAccount = 0x00010002, //РФ
        [Description("Превышение предельного времени работы за смену на междугородних перевозках")]
        ExceedingLimitWorkingTimeShift_Intercity = 0x00010003, //РФ
        [Description("Превышение предельного времени работы за смену для категории п.12")]
        ExceedingLimitWorkingTimeShift_12Article = 0x00010004, //РФ
        [Description("Превышение нормальной продолжительности рабочего времени в день")]
        ExceedingLimitWorkingTimeShift_ForDayFiveDaysDaily = 0x00010005, //РФ
        [Description("Превышение нормальной продолжительности рабочего времени в день")]
        ExceedingLimitWorkingTimeShift_ForDaySixDaysDaily = 0x00010006, //РФ
        [Description("Превышение продолжительности ежедневной работы")]
        ExceedingLimitWorkingTimeShift_ForDaySummary = 0x00010007, //РФ        
        [Description("Превышение продолжительности ежедневной работы")]
        ExceedingLimitWorkingTimeShift_UrbanAndSuburbanBusRoutes = 0x00010008, //РФ для городских и пригородных автобусных сообщений

        [Description("Превышение предельного времени управления за смену не более 10 часов")]
        ExceedingLimitDrivingTime_10hours_ESTR = 0x00020001, //ЕСТР
        [Description("Превышение предельного времени управления за смену не более 10 часов при неизвестной системе учёта")]
        ExceedingLimitDrivingTime_10hours_UnknownSystemAccount = 0x00020002, //РФ
        [Description("Превышение предельного времени управления за смену не более 10 часов при суммированной системе учёта")]
        ExceedingLimitDrivingTime_10hours_SummarySystemAccount = 0x00020003, //РФ
        [Description("Превышение предельного времени управления за смену не более 9 часов для категории п.12")]
        ExceedingLimitDrivingTime_9hours_12Article = 0x00020004, //РФ
        [Description("Превышение предельного времени управления за смену не более 9 часов при ежедневной системе учёта")]
        ExceedingLimitDrivingTime_9hours_Daily = 0x00020005, //РФ
        [Description("Увеличение времени управления до 10 часов не более 2-х раз в неделю")]
        ExceedingLimitDrivingTime_9_10twice_ESTR = 0x00020006, //ЕСТР
        [Description("Увеличение времени управления до 10 часов не более 2-х раз в неделю при неизвестной системе учёта")]
        ExceedingLimitDrivingTime_9_10twice_UnknownSystemAccount = 0x00020007, //РФ
        [Description("Увеличение времени управления до 10 часов не более 2-х раз в неделю при суммированной системе учёта")]
        ExceedingLimitDrivingTime_9_10twice_SummarySystemAccount = 0x00020008, //РФ
        [Description("Превышение предельного времени управления за смену не более 8 часов в горной местности")]
        ExceedingLimitDrivingTime_8hours_Highlands = 0x00020009, //РФ

        [Description("Превышение еженедельной продолжительности управления не более 56 часов")]
        ExceedingLimitDrivingTimeInOneWeek_56hoursPer1Week = 0x00030001, //ЕСТР
        [Description("Превышение еженедельной продолжительности управления не более 56 часов при неизвестной системе учёта")]
        ExceedingLimitDrivingTimeInOneWeek_56hoursPer1Week_UnknownSystemAccount = 0x00030002, //РФ
        [Description("Превышение еженедельной продолжительности управления не более 56 часов при суммированной системе учёта")]
        ExceedingLimitDrivingTimeInOneWeek_56hoursPer1Week_SummarySystemAccount = 0x00030003, //РФ
        [Description("Превышение еженедельной продолжительности управления не более 56 часов для городских и пригородных автобусных сообщений")]
        ExceedingLimitDrivingTimeInOneWeek_56hoursPer1Week_UrbanAndSuburbanBusRoutes = 0x00030004, //РФ не используется с 03.05.2018
        [Description("Превышение еженедельной продолжительности управления не более 56 часов при ежедневной системе учёта")]
        ExceedingLimitDrivingTimeInOneWeek_56hoursPer1Week_Daily = 0x00030005, //РФ

        [Description("Превышение предельного времени управления не более 90 часов за 2 недели")]
        ExceedingLimitDrivingTimeInTwoWeeks_90hoursPer2Weeks_ESTR = 0x00040001, //ЕСТР
        [Description("Превышение предельного времени управления не более 90 часов за 2 недели при неизвестной системе учёта")]
        ExceedingLimitDrivingTimeInTwoWeeks_90hoursPer2Weeks_UnknownSystemAccount = 0x00040002, //РФ
        [Description("Превышение предельного времени управления не более 90 часов за 2 недели при суммированной системе учёта")]
        ExceedingLimitDrivingTimeInTwoWeeks_90hoursPer2Weeks_SummarySystemAccount = 0x00040003, //РФ
        [Description("Превышение предельного времени управления не более 90 часов за 2 недели для городских и пригородных автобусных сообщений")]
        ExceedingLimitDrivingTimeInTwoWeeks_90hoursPer2Weeks_UrbanAndSuburbanBusRoutes = 0x00040004, //РФ не используется с 03.05.2018
        [Description("Превышение предельного времени управления не более 90 часов за 2 недели при ежедневной системе учёта")]
        ExceedingLimitDrivingTimeInTwoWeeks_90hoursPer2Weeks_Daily = 0x00040005, //РФ

        [Description("Предельно малая длительность перерыва для отдыха и питания за смену")]
        ViolationDurationBreakForShift_MinDinnerDuration = 0x00050001, //РФ
        [Description("Предельно малая длительность перерыва для отдыха и питания за смену для регулярных междугородных автобусных маршрутов")]
        ViolationDurationBreakForShift_MinDinnerDuration_IntercityBusTransportation = 0x00050002, //РФ
        [Description("Превышение предельной длительности перерыва для отдыха и питания за смену")]
        ViolationDurationBreakForShift_MaxDinnerDuration = 0x00050003, //РФ
        [Description("Превышение предельной длительности перерыва для отдыха и питания за смену для регулярных междугородных автобусных маршрутов")]
        ViolationDurationBreakForShift_MaxDinnerDuration_IntercityBusTransportation = 0x00050004, //РФ

        [Description("Нарушен первый интервал управления в смене")]
        ViolationIntervalsContinuousDrivingInShift_FirstInterval = 0x00060001, //РФ
        [Description("Нарушен повторный интервал управления в смене")]
        ViolationIntervalsContinuousDrivingInShift_SecondInterval = 0x00060002, //РФ

        [Description("Отсутствие регламентированного перерыва после 4 часов 30 минут управления")]
        ViolationIntervalsContinuousDrivingInShift_4h30min = 0x00060003, //ЕСТР
        [Description("Отсутствие регламентированного перерыва после 3 часов управления для нерегулярных международных пассажирских перевозок")]
        ViolationIntervalsContinuousDrivingInShift_3hNight = 0x00060004, //ЕСТР

        [Description("Нарушение длительности междусменного отдыха не менее 12 часов")]
        ViolationDurationRestBetweenShifts_12hours = 0x00070001, //РФ
        [Description("Нарушение длительности междусменного отдыха не менее 8 часов")]
        ViolationDurationRestBetweenShifts_8hours = 0x00070002, //РФ
        [Description("Нарушение длительности междусменного отдыха не менее 9 часов")]
        ViolationDurationRestBetweenShifts_9hours = 0x00070003, //РФ
        [Description("Сокращение междусменного отдыха до 9 часов не более 3-х раз в неделю")]
        ViolationDurationRestBetweenShifts_9hours3times = 0x00070004, //РФ
        [Description("Нарушение длительности междусменного отдыха не менее 8 часов для Экипажа за 30 часов")]
        ViolationDurationRestBetweenShifts_8hours_30Hours_Crew = 0x00070005, //РФ
        [Description("Отдых для Экипажа должен быть не менее 9 часов в течение 30 часов с момента окончания предыдущего отдыха")]
        ViolationDurationRestBetweenShifts_9hours_30Hours_Crew = 0x00070006, //ЕСТР
        [Description("Продолжительность междусменного отдыха вместе с временем перерыва для отдыха и питания должна быть не менее двойной продолжительности времени работы в предшествующий отдыху рабочий день (смену)")]
        ViolationDurationRestBetweenShifts_RecreationWithDinnerLessThanDoubleWorkTime = 0x00070007, //РФ

        [Description("Междусменный отдых должен быть в течение 24 часов с момента окончания предыдущего межсменного отдыха")]
        ViolationDurationRestBetweenShifts_BetweenShiftRest_24Hours = 0x00070008, //ЕСТР
        [Description("Должно быть не более трёх сокращённых ежедневных периодов отдыха между любыми двумя еженедельными периодами отдыха")]
        ViolationDurationRestBetweenShifts_9hours3times_ESTR = 0x00070009, //ЕСТР

        [Description("Некомпенсированное сокращение междусменного отдыха следующим увеличенным до 48-ми часов")]
        ViolationDurationRestBetweenShifts_From12To9WithoutNext48 = 0x0007000A, //РФ

        [Description("Контроль числа выходных в текущем месяце, не менее числа полных недель этого месяца")]
        ViolationRulesUseWeeklyRest_CountFullWeekInMonth = 0x00090001, //РФ
        [Description("За две последовательные недели не был использован нормальный еженедельный отдых")]
        ViolationRulesUseWeeklyRest_OneNormalWeeklyRestNotUseOnTwoWeeks = 0x00090002, //ЕСТР
        [Description("Превышен интервал между соседними еженедельными отдыхами")]
        ViolationRulesUseWeeklyRest_ExceededIntervalBetweenWeeklyRests = 0x00090003, //ЕСТР
        [Description("Превышен интервал между соседними еженедельными отдыхами")]
        ViolationRulesUseWeeklyRest_ExceededIntervalBetweenWeeklyRests_NotScheduledPassengerTransportation = 0x00090004, //ЕСТР
        [Description("Предельно малая длительность отложенного еженедельного отдыха")]
        ViolationRulesUseWeeklyRest_SmallIntervalDeferredRest_NotScheduledPassengerTransportation = 0x00090005, //ЕСТР
        [Description("Нарушение длительности еженедельного непрерывного отдыха")]
        ViolationRulesUseWeeklyRest_RecreationPresentInCalendarWeek = 0x00090006, //РФ

        [Description("Некомпенсированное сокращение отдыха до конца следующей недели")]
        UncompensatedReductionVacation_NextOneWeek = 0x000A0001, //РФ
        [Description("Сокращение еженедельного отдыха не компенсировано эквивалентным периодом отдыха до конца третьей недели")]
        UncompensatedReductionVacation_WeekRestOnNext3Weeks = 0x000A0002, //ЕСТР

        [Description("Наличие неопределённой деятельности")]
        HaveUncertainActivity = 0x000B0001, //РФ //ЕСТР

        [Description("Превышение продолжительности рабочего времени c учётом сверхурочных работ в течение дня (по документам на Предприятии)")]
        ViolationOrderDoingOvertime_ForDayWithOvertimeWorkFiveDaysDaily = 0x000C0001, //РФ
        [Description("Превышение продолжительности рабочего времени c учётом сверхурочных работ в течение дня (по документам на Предприятии)")]
        ViolationOrderDoingOvertime_ForDayWithOvertimeWorkSixDaysDaily = 0x000C0002, //РФ
        [Description("Контроль сверхурочных работ в текущий день при суммированной системе учёта")]
        ViolationOrderDoingOvertime_ForDayWithOvertimeWorkSummary = 0x000C0003, //РФ

        [Description("Превышение нормальной продолжительности рабочего времени за учётный период")]
        ViolationOrderDoingOvertime_ForAccountPeriod = 0x000C0004, //РФ //TODO: м.б. вынести в отдельную группу?
        [Description("Превышение продолжительности рабочего времени за учётный период c учётом сверхурочных работ (по документам на Предприятии)")]
        ViolationOrderDoingOvertime_ForAccountPeriodWithOvertimeWork = 0x000C0005, //РФ

        [Description("Превышение предельного времени сверхурочных работ в двух смежных днях при пятидневной неделе")]
        ViolationOrderDoingOvertime_ForTwoDaysFiveDaysDaily = 0x000C0006, //РФ
        [Description("Превышение предельного времени сверхурочных работ в двух смежных днях при шестидневной неделе")]
        ViolationOrderDoingOvertime_ForTwoDaysSixDaysDaily = 0x000C0007, //РФ


        [Description("Превышение предельного времени сверхурочных работ в календарный год при пятидневной неделе")]
        ViolationOrderDoingOvertime_ForYearFiveDaysDaily = 0x000C0008, //РФ
        [Description("Превышение предельного времени сверхурочных работ в календарный год при шестидневной неделе")]
        ViolationOrderDoingOvertime_ForYearSixDaysDaily = 0x000C0009, //РФ
        [Description("Превышение предельного времени сверхурочных работ в календарный год при суммированной системе учёта")]
        ViolationOrderDoingOvertime_ForYearSummary = 0x000C000A, //РФ


        [Description("Превышение предельного времени сверхурочных работ в двух смежных днях по документам предприятия при пятидневной неделе")]
        ViolationPaperworkPerformOvertimeWork_ForTwoDaysFiveDaysDaily = 0x000D0001, //РФ
        [Description("Превышение предельного времени сверхурочных работ в двух смежных днях по документам предприятия при шестидневной неделе")]
        ViolationPaperworkPerformOvertimeWork_ForTwoDaysSixDaysDaily = 0x000D0002, //РФ
        [Description("Превышение предельного времени сверхурочных работ в двух смежных днях по документам предприятия при суммированной системе учёта")]
        ViolationPaperworkPerformOvertimeWork_ForTwoDaysSummary = 0x000D0003, //РФ
    }
}
