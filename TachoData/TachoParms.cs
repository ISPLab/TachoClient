using System;
using System.Collections.Generic;

namespace TachoData
{

    public class TachoParms
	{
		public DateTime beginreportdatetime { get; set; }
		public DateTime endreportdatetime = DateTime.Now;
		public TimeSpan minrecreationtime { get; set; }
		public TimeSpan maxshifttime = TimeSpan.FromHours(12);
		public TimeSpan maxDrivingDuration;
		public TimeSpan alloweddrivingtime;
		public TimeSpan weeklyrestdurationhours = new TimeSpan(42, 0, 0);
		public string Pin { get; set; }
		public string Command { get; set; }
		public string Params { get; set; }
		public DateTime Endreportdatetime { get; set; }
		public DateTime Beginreportdatetime { get; set; }
		public TachoLegislation Legislation { get; set; }
		public bool CheckSignature { get; set; }
		public int MaxVioaltionNumber { get; set; }
		public int MaxShiftNumber { get; set; }
		public int MaxCarAnalisis { get; set; }
	}

    public class TachoLegislation
    {
        public List<LegislationsParam> Parms { get; private set; }

        public bool Checked { get; set; }
        public String Country { get; set; }
        public String ActDescription { get; set; }
        public TachoLegislation(String country, List<LegislationsParam> parms)
        {
            Country = country;
            Parms = parms;
        }
        public override string ToString()
        {
            return Country;
        }
    }
    public class LegislationsParam
    {
        //  private bool _Checked;
        public string Description { get; set; }
        public bool Checked { get; set; }

        public LegislationsParam(string desc, bool check = false)
        {
            Description = desc;
            Checked = check;
        }
    }
  
}
