using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using TachoData;

namespace TachoHelper
{
	public partial class TachoServiceBase
    {
        private static readonly string TAG = "tachotag";
        int TachoservicePid { get; set; }
        protected TachoServiceConnection serviceConnection;
		protected Messenger activityMessenger;
        Intent intentToBindService;
        public event EventHandler<P8EventData> P8Event;
        internal bool isStarting;
        public void GetTachodata(string paramB64)
        {
            if (serviceConnection != null && serviceConnection.IsConnected && serviceConnection.Messenger != null)
            {
                Message msg = Message.Obtain(null, Constants.TachoConrollerOperation);
                try
                {
                    Log.Info("tachotag", "TachoHelper:  Sending cmd GetTachodata...");
                    Bundle dataToSend = new Bundle();
                    dataToSend.PutString("cmd", "gettachodata");
                    dataToSend.PutString("arg", paramB64);
                    msg.Data = dataToSend;
                    msg.ReplyTo = activityMessenger;
                    serviceConnection.Messenger.Send(msg);
                }
                catch (RemoteException ex)
                {
                    Log.Error(TAG, ex, "TachoHelper: There was a error trying to send to tachoservice the message.");
                }
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "App not connected to tachoservice service.", ToastLength.Short).Show();
            }
        }

        public List<TachoLegislation> GetBaseLegislations()
        {

            var Legislations = new List<TachoLegislation>();
            List<LegislationsParam> parms = new List<LegislationsParam>();
            parms.Add(new LegislationsParam("Междугородные перевозки", false));
            parms.Add(new LegislationsParam("Перевозки по п.12 Пр. №15", false));
            parms.Add(new LegislationsParam("Горная местность", false));
            parms.Add(new LegislationsParam("Регулярные городские и пригородные автобусные сообщения", false));
            //Удалено, начиная с редакции Приказа №15 от 03.05.2018
            //parms.Add(new LegislationsParamBase("Регулярные междугородные автобусные маршруты", false));

            string rusact = "Приказ Минтранса России от 20.08.2004 N 15(ред.от 03.05.2018) \"Об утверждении Положения об особенностях режима рабочего времени и времени отдыха водителей автомобилей\")";
            TachoLegislation legislationrus = new TachoLegislation("Poccийское", parms) { ActDescription = rusact };
            Legislations.Add(legislationrus);
            parms = new List<LegislationsParam>();
            parms.Add(new LegislationsParam("Международные нерегулярные пассажирские перевозки", false));
            string euroact = "Европейское соглашение, касающееся работы экипажей транспортных средств, производящих международные автомобильные перевозки (ЕСТР) (Женева, 1 июля 1970 г.)";
            TachoLegislation euro = new TachoLegislation("Европейское", parms) { ActDescription = euroact };
            Legislations.Add(euro);
            return Legislations;
      
        }
        public virtual void StopTachoProcess()
        {
            try
            {
                if (serviceConnection?.Messenger != null)
                {
                    Message msg = Message.Obtain(null, Constants.TachoConrollerOperation);
                    try
                    {
                        Bundle dataToSend = new Bundle();
                        dataToSend.PutString("cmd", "stop");
                        msg.Data = dataToSend;
                        msg.ReplyTo = activityMessenger;
                        serviceConnection.Messenger.Send(msg);
                      
                        //serviceConnection.Messenger.Dispose();
                    }
                    catch (RemoteException ex)
                    {
                        P8Event?.Invoke(this, new P8EventData("tachocontroller", "error", "tachoerror", "There was a error trying to send the message", ""));
                        Log.Error(TAG, ex, "TachoHelper:  There was a error trying to send the message");
                    }
                }
                else
                {
                    P8Event?.Invoke(this, new P8EventData("tachocontroller","error", "tachoerror", "There was a error trying to send the message", ""));
                    Toast.MakeText(Android.App.Application.Context, "TachoService cann't connected", ToastLength.Short).Show();
                }

              /*  if (serviceConnection != null)
                {
                    Android.App.Application.Context.UnbindService(serviceConnection);
                }*/
            }
            catch (Exception ex)
            {
                Log.Warn("tachotag", "TachoHelper:  Exception Stop TachoIntentService for gettahodata Message:" +ex.Message);
            }
        }

        public virtual void GetPid()
        {
            try
            {
                if (serviceConnection?.Messenger != null)
                {
                    Message msg = Message.Obtain(null, Constants.TachoPid);
                    try
                    {
                        Bundle dataToSend = new Bundle();
                        dataToSend.PutString("cmd", "getpid");
                        msg.Data = dataToSend;
                        msg.ReplyTo = activityMessenger;
                        serviceConnection.Messenger.Send(msg);
                        //serviceConnection.Messenger.Dispose();
                    }
                    catch (RemoteException ex)
                    {
                        P8Event?.Invoke(this, new P8EventData("tachocontroller","error", "tachoerror", "There was a error trying to send the message", ""));
                        Log.Error(TAG, ex, "TachoHelper:  There was a error trying to send the message");
                    }
                }
                else
                {
                    P8Event?.Invoke(this, new P8EventData("tachocontroller", "error", "tachoerror", "There was a error trying to send the message", ""));
                    Toast.MakeText(Android.App.Application.Context, "TachoService cann't connected", ToastLength.Short).Show();
                }

                /*  if (serviceConnection != null)
                  {
                      Android.App.Application.Context.UnbindService(serviceConnection);
                  }*/
            }
            catch (Exception ex)
            {
                Log.Warn("tachotag", "TachoHelper:  Exception Stop TachoIntentService for gettahodata Message:" + ex.Message);
            }
        }

        public virtual void UnbindService()
        {
            if(serviceConnection!=null)
              Android.App.Application.Context.UnbindService(serviceConnection);
            Android.OS.Process.KillProcess(TachoservicePid);
        }

        public virtual void StartService()
        {
            Log.Debug(TAG, "TachoHelper: Sending StartService cmd");
            isStarting = false;

            if (!IsTachoServicePackageInstalled())
            {
                Log.Error(TAG, "TachoHelper: TachoService is not installed");
                P8Event?.Invoke(this, new P8EventData("tachocontroller", "error","tachoerror", "TachoService is not installed", "connected false"));
                Toast.MakeText(Android.App.Application.Context.ApplicationContext, "TachoService is not installed on Device", ToastLength.Long).Show();
                return;
            }

            if (HasPermissionToRunTachoService())
            {
                Log.Debug(TAG, "TachoHelper: TachoService is calling...");
                if (activityMessenger == null)
                {
                    var mesr = new TachoServiceHandler();
                    activityMessenger = new Messenger(mesr);
                   
                    mesr.P8Event += (s, e) => {
                        string mes = $"TachoHelper:  mesr.P8Event += EventName: {e.EventName} Description: {e.Description} ";

                        if (e.EventName == "tachopid" && TachoservicePid == 0)
                        {
                            TachoservicePid = Convert.ToInt32(e.Parametr);
                           // Android.OS.Process.KillProcess(TachoservicePid);
                        }
                        Log.Debug("tachotag", mes);
                   
                     P8Event?.Invoke(this, e); };
                }
                if (serviceConnection == null)
                {
                    serviceConnection = new TachoServiceConnection();
                    serviceConnection.P8Event += (s, e) => {

                        if (e.EventName == "tachoinfo" && (string)e.Parametr == "connected")
                            GetPid();

                        P8Event?.Invoke(this, e); };
                }
                if (intentToBindService == null)
                {
                    intentToBindService = CreateIntentToBindService(Android.App.Application.Context);
                }
                Android.App.Application.Context.BindService(intentToBindService, serviceConnection, Bind.AutoCreate);
                isStarting = true;
                Log.Debug(TAG, "TachoHelper: TachoService has been called.");
            }
            else
            {
                Log.Error(TAG, $"TachoHelper: No permissions to use the service {Constants.REMOTE_SERVICE_PACKAGE_NAME}.");
                serviceConnection = null;
                activityMessenger = null;
                Toast.MakeText(Android.App.Application.Context.ApplicationContext, "You must grant permission before this app can use the TachoService", ToastLength.Long).Show();
                P8Event?.Invoke(this, new P8EventData("tachocontroller","error", "tachoerror", "You must grant permission before this app can use the TachoService.", "connected false"));
                return;
            }
        }
    }
}
