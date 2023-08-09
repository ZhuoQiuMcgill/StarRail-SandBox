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
                    return "�˿�";
                case Resources.Food:
                    return "ʳ��";
                case Resources.Metal:
                    return "����";
                case Resources.Energy:
                    return "����";
                case Resources.Technology:
                    return "����";
                default:
                    return resource.ToString();
            }
        }
    }
}

