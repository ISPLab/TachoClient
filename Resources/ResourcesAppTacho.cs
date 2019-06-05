using P8.Services.Resourses;
using System;
using System.Globalization;

namespace Resources
{

    public class ResourcesAppTacho : IResourcesApp
    {
        private CultureInfo culture;

        public ResourcesAppTacho(CultureInfo culture)
        {
            this.culture = culture;
        }

        public string GetString(string key)
        {
           return TachoResources.Properties.Resources.ResourceManager.GetString(key, culture);
        }
    }
}
