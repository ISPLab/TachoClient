using System;
namespace TachoData
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;
	using System.Reflection;
	using System.Runtime.Serialization;
    using TachoHelper.Services.Serializing;

    public class P8EventData : EventArgs
	{

		[JsonProperty("controller")]
		public string Controller { get;  set; }

		[JsonProperty("eventid")]
		public Guid EventID { get; set; }

		[JsonProperty("eventutctime")]
		public DateTime EventUtcTime { get;  set; }

		[JsonProperty("eventtype")]
		public string DataType { get;  set; }



		[JsonProperty("eventname")]
		public string EventName { get;  set; }

		[JsonProperty("description")]
		public string Description { get;  set; }

		[JsonProperty("parametr")]
		public string Parametr { get;  set; }
		public P8EventData(string controller, string type, string eventname, string description, string parametr)
		{
			//TODO: autogenerate
			Controller = controller;
			DataType = type;
			EventName = eventname;
			Description = (description == null) ? "" : description;
			Parametr = (parametr == null) ? "" : parametr;
			EventID = Guid.NewGuid();
			EventUtcTime = DateTime.UtcNow;
		}
    
        public override string ToString()
		{
			string param = Parametr.ToString();
			if (param != null && param.Length > 10)
				param = (Parametr as string).Substring(0, 9) + "...";
			return $"EventData:{Controller}: {DataType}: {EventName}: {Description}: {param}";
		}

		public static string Serialize(P8EventData data)
		{
			P8Serializer p8Serializer = new P8Serializer();
			var id = data.EventID;
			var name = data.EventName;
			return p8Serializer.SerializeToBase64(data);
		}
	}
}
