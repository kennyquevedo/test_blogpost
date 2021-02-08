using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Common
{
    public static class StatusValues
    {

        public const int Approved = 1;
        public const int Rejected = 2;
        public const int Review = 3;
        public const int Published = 4;

        public static string GetDescription(this int value)
        {
            return GetDescriptionFromValue(value);
        }

        public static string GetDescriptionFromValue(int value)
        {
            string description = "";

            switch (value)
            {
                case 1:
                    return "Approved";
                case 2:
                    return "Rejected";
                case 3:
                    return "Needs Review";
                case 4:
                    return "Published";
            }

            return description;
        }

    }
}
