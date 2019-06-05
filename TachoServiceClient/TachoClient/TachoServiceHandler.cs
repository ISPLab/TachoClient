using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TachoData;

namespace TachoHelper
{
    /// <summary>
    /// This class is used by the MainActivity to process messages that are sent by the service.
    /// </summary>
    public class TachoServiceHandler : Handler
    {
        static readonly string TAG = "tachotag";
		public EventHandler<P8EventData> P8Event;
		public override void HandleMessage(Message msg)
        {
            try
            {
				//P8.Services.Serializing.P8Serializer serializer = new P8.Services.Serializing.P8Serializer();
                
                int what = msg.What;
				Log.Info(TAG, $"TachoHelper: HandleMessage : {what} ");
                switch (what)
                {
                    case (Constants.TachoPid):
                        Log.Info(TAG, $"TachoHelper: HandleMessage : TachoPid ");
                        P8Event?.Invoke(this, new P8EventData("tachocontroller", "info", "tachopid", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;
                    case (Constants.TachoResult):
						Log.Info(TAG, $"TachoHelper: HandleMessage : TachoResult Description ={msg.Data.GetString("Description")} Argument={msg.Data.GetString("Argument")}");
                     	P8Event?.Invoke(this, new P8EventData("tachocontroller", "info","tachoresult", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;
                       //obsolete
                   /* case (Constants.TachoResultp5):
						Log.Info(TAG, $"TachoHelper: HandleMessage : TachoResultp5 ");
                        P8Event?.Invoke(this, new P8EventData("tachocontroller", "tachoresultp5", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;*/
                    case (Constants.TachoProgress):
						Log.Info(TAG, $"TachoHelper: HandleMessage : TachoProgress ");
                        P8Event?.Invoke(this, new P8EventData("tachocontroller","info", "tachoprogress", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;
                    case (Constants.TachoInfo):
                        Log.Info(TAG, $"TachoHelper: HandleMessage : TachoProgress ");
                        P8Event?.Invoke(this, new P8EventData("tachocontroller","info", "tachoinfo", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;

                    case (Constants.TachoError):
						Log.Info(TAG, $"TachoHelper: HandleMessage : TachoError ");
                        P8Event?.Invoke(this, new P8EventData("tachocontroller", "error","tachoerror", msg.Data.GetString("Description"), msg.Data.GetString("Argument")));
                        break;

                    default:
						Log.Warn(TAG, $"TachoHelper: HandleMessage Unknown msg.what value: {what} . Ignoring this message.");
                        break;

                }
            }catch (Exception ex)
            {
				Log.Error(TAG, "TachoHelper: HandleMessage : " + ex.Message);
            }
        }
    }
}