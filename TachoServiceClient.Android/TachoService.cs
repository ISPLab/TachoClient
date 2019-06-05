using System;
using System.Collections.Generic;
using TachoData;
using TachoHelper;
using TachoHelper.Services.Serializing;
using TachoServiceClient.Droid;
using Xamarin.Forms;


namespace TachoHelper.Services
{
 
    public class TachoService : TachoServiceBase
    {
        private static object syncRoot = new Object();
        private static volatile TachoService instance;
        public static TachoService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new TachoService();
                    }
                }

                return instance;
            }
        }
        private TachoService():base()
        {
          
        }

        public static WeakReference<MainActivity> mainActivity;
        public MainActivity Activity
        {
            get
            {
                
                    MainActivity activity;
                if (mainActivity.TryGetTarget(out activity))
                {
                    return activity;
                }
                return null;
            }
        }




      
		public override void StartService()
		{
			base.StartService();
		}

        public override void UnbindService()
        {
            base.UnbindService();
        }


        public override void StopTachoProcess()
		{
			base.StopTachoProcess();
		}

		public void GetTachodata(TachoParms parms)
		{
            TachoHelper.Services.Serializing.P8Serializer p8Serializer = new TachoHelper.Services.Serializing.P8Serializer();
            string paramB64 = p8Serializer.SerializeToBase64(parms);
			base.GetTachodata(paramB64);
		}

        public List<TachoLegislation> GetLegislations()
        {
            List<TachoLegislation> resb = base.GetBaseLegislations();
            List<TachoLegislation> res = new List<TachoLegislation>();
            foreach(var l in resb)
            {
                List<LegislationsParam> prms = new List<LegislationsParam>();
                foreach (var p in l.Parms)
                    prms.Add(new LegislationsParam(p.Description, false));
                res.Add(new TachoLegislation(l.Country, prms));
            }
            return res;
        }
    }
}