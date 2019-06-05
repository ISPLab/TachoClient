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
    public class TachoServiceConnection : Java.Lang.Object, IServiceConnection
    {
        static readonly string TAG = "tachotag";
        public event EventHandler<P8EventData> P8Event;
        public TachoServiceConnection()
        {
            IsConnected = false;
        }

        public bool IsConnected { get; private set; }
        public Messenger Messenger { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Log.Debug(TAG, $"TachoHelper: OnServiceConnected {name.ClassName}");
            Messenger = new Messenger(service);
            IsConnected = this.Messenger != null;
            if (IsConnected)
            {
                P8Event?.Invoke(this, new P8EventData("tachocontroller","info", "tachoinfo", "Tacho Service connected", "connected"));   
               // Toast.MakeText(Android.App.Application.Context, Resource.String.service_started, ToastLength.Short).Show();
            }
            else
            {
                P8Event?.Invoke(this, new P8EventData("tachocontroller","info", "tachoerror", "Tacho Service cann't connected", "connected false"));
                Toast.MakeText(Android.App.Application.Context, "Service cann't connected...", ToastLength.Short).Show();
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            P8Event?.Invoke(this, new P8EventData("tachocontroller","info", "tachoinfo", "Tacho Service disconnected", ""));
            Log.Debug(TAG, $"TachoService disconnected {name.ClassName}");
        }
    }
}