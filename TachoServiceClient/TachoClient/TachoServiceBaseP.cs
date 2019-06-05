using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.Content;
using TachoData;

namespace TachoHelper
{
    /// <summary>
    /// Various  methods to help client apps consume the Tacho service.
    /// </summary>
    public partial class TachoServiceBase 
	{
     
		/// <summary>
		/// This will check to see if the current Context has been granted the custom permission to use the 
		/// TachoSerice
		/// </summary>
		/// <returns><c>true</c>, if the current app has been granted permission to use the TachoService, <c>false</c> otherwise.</returns>
		/// <param name="context">Context.</param>
		private bool HasPermissionToRunTachoService()
		{
     		Permission permissionCheck = ContextCompat.CheckSelfPermission(Application.Context.ApplicationContext, Constants.TachoService_Permission);
            return permissionCheck == Permission.Granted;
		}
     

        /// <summary>
        /// Creates an explicit intent that can be used to start the TachoService.
        /// </summary>
        /// <returns>The intent to bind service.</returns>
        private Intent CreateIntentToBindService(Context context)
		{
			if (IsTachoServicePackageInstalled())
			{
				return CreateIntentForServiceInternal();
			}
			return null;
		}

        /// <summary>
        /// This method will create an explicit intent for starting the TachoService (or checking to see if it
        /// is installed).
        /// </summary>
        /// <returns></returns>
        private Intent CreateIntentForServiceInternal()
		{
			ComponentName cn = new ComponentName(Constants.REMOTE_SERVICE_PACKAGE_NAME, Constants.REMOTE_SERVICE_COMPONENT_NAME);
			Intent serviceToStart = new Intent();
			serviceToStart.SetComponent(cn);
			return serviceToStart;
		}

        /// <summary>
        /// Checks to see if the TachoService has been installed on the device.
        /// </summary>
        /// <returns><c>true</c>, if TachoService package is installed, <c>false</c> otherwise.</returns>
        /// <param name="context">Context.</param>
        private bool IsTachoServicePackageInstalled()
		{
            Intent intent = CreateIntentForServiceInternal();
    	    IList<ResolveInfo> list = Application.Context.ApplicationContext.PackageManager.QueryIntentServices(intent, Android.Content.PM.PackageInfoFlags.MatchDefaultOnly);
    	    return list.Count > 0;
		}
	}
}
