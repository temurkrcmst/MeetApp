using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ServiceTemplate.Extensions
{
    public static class valueConverter
    {

        public static object convertValue(this object value, string type)
        {
            try
            {
                if (type == "string")
                {
                    return Convert.ToString(value);

                    // return (string)value; // = value.ToString(); // ._toString();
                }
                else if (type == "int32")
                {
                    return Convert.ToInt32(value);
                }
                else if (type == "int64")
                {
                    return Convert.ToInt64(value);
                }
                else if (type == "float")
                {
                    return Convert.ToDouble(value);
                }
                else if (type == "double")
                {
                    if (value is double)
                    {
                        return value;
                    }

                    var tempval = value.ToString();
                    NumberFormatInfo nfi = new NumberFormatInfo();

                    if (tempval.IndexOf('.') >= 0)
                    {
                        nfi.CurrencyDecimalSeparator = ".";
                    }
                    else
                    {
                        nfi.CurrencyDecimalSeparator = ",";
                    }

                    return Convert.ToDouble(value, nfi);
                }
                else
                {
                    return Convert.ToString(value);
                }
            }
            catch
            {
                // var t = type + " türündeki " + value + " değeri için "; // + ex.Message;
                return "*INCORRECT";
            }
        }

        public static string _toString(this string str)
        {
            if (String.IsNullOrEmpty(str))
                str = "";

            return Convert.ToString(str);
        }

        public static Int32 _toInt32(this String str)
        {
            // hata vermesi için string değer veriliyor.
            if (String.IsNullOrEmpty(str))
                str = "x";
            return Convert.ToInt32(str);
        }


        public static Int64 _toInt64(this String str)
        {
            // hata vermesi için string değer veriliyor.
            if (String.IsNullOrEmpty(str))
                str = "x";

            return Convert.ToInt64(str);
        }

        public static double _toDouble(this String str)
        {
            // hata vermesi için string değer veriliyor.
            if (String.IsNullOrEmpty(str))
                str = "x";
            return Convert.ToDouble(str);
        }

        public static float _toFloat(this String str)
        {
            // hata vermesi için string değer veriliyor.
            if (String.IsNullOrEmpty(str))
                str = "x";
            return float.Parse(str);
        }

        public static bool _toBool(this String str)
        {
            if (String.IsNullOrEmpty(str))
                str = "";

            return Convert.ToBoolean(str);
        }


        public static decimal _toDecimal(this String str)
        {
            if (String.IsNullOrEmpty(str))
                str = "";

            return Convert.ToDecimal(str);
        }



        public static DateTime _toDateTime(this String str)
        {
            if (String.IsNullOrEmpty(str))
                str = "";

            return Convert.ToDateTime(str);
        }
    }
}