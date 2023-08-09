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
                    return "人口";
                case Resources.Food:
                    return "食物";
                case Resources.Metal:
                    return "金属";
                case Resources.Energy:
                    return "能量";
                case Resources.Technology:
                    return "技术";
                default:
                    return resource.ToString();
            }
        }
    }
}

