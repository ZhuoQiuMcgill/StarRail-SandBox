using System;
using System.Linq;


namespace MapResources
{
    public enum Resources
    {
        Population,
        Food,
        Metal,
        Energy,
        Technology
    }

    public static class ResourcesExtensions
    {
        public static string ToCustomString(this Resources resource)
        {
            switch (resource)
            {
                case Resources.Population:
                    return "POP";
                case Resources.Food:
                    return "FOD";
                case Resources.Metal:
                    return "MET";
                case Resources.Energy:
                    return "ENR";
                case Resources.Technology:
                    return "TEC";
                default:
                    return resource.ToString();
            }
        }
    }
}

