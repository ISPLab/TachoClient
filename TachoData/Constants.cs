using System;
namespace TachoData
{
	
	public static class Constants
	{
		public const string REMOTE_SERVICE_COMPONENT_NAME = "com.navis.Tachography";

		public const string REMOTE_SERVICE_PACKAGE_NAME = "com.navis.Tachography";
		public const string TachoService_Permission = "com.navis.Tachography.REQUEST_TACHODATA";
		public const int TachoPid = 19000;
		public const int TachoConrollerOperation = 20000;
		public const int TachoResult = 20010;
		public const int TachoResultp5 = 20011;
		public const int TachoProgress = 20100;
		public const int TachoError = 21000;
		public const int TachoInfo = 21200;

		//TODO: Others Names
		public const int TIMESTAMP_SERVICE_REQUEST_PERMISSION = 8000;
		public const string TIMESTAMP_RESPONSE_KEY = "timestamp_message";
		public const int SAY_HELLO_TO_TIMESTAMP_SERVICE = 9000;
		public const int GET_UTC_TIMESTAMP = 10000;
		public const int START_TIMESTAMP_SERVICE = 10010;
		public const int STOP_TIMESTAMP_SERVICE = 10020;
		public const int GET_UTC_TIMESTAMP_RESPONSE = 10100;
	}
}
