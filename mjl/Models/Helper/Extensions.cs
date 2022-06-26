using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace mjl.Models
{
    public static class Extensions
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string SystemAutoFormat(this object data)
        {
            string strFormat = "";
            var value = data;
            var type = data.GetType();

            if (data != null)
            {
                if (type.IsGenericType &&
                     type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (type.GetGenericArguments()[0] == typeof(Decimal))
                    {
                        decimal flag = (decimal)value;
                        strFormat = flag.ToNumericFormat();
                    }

                    if (type.GetGenericArguments()[0] == typeof(Int32))
                    {
                        int flag = (int)value;
                        strFormat = flag.ToNumericFormat();
                    }

                    if (type.GetGenericArguments()[0] == typeof(String))
                    {
                        string flag = value.ToString();
                        strFormat = flag.EmptyNull().ToUpper();
                    }

                    if (type.GetGenericArguments()[0] == typeof(DateTime))
                    {
                        DateTime flag = (DateTime)value;
                        strFormat = flag.ToString("MM/dd/yyyy");
                    }
                }
                else
                {
                    if (type == typeof(Decimal))
                    {
                        decimal flag = (decimal)value;
                        strFormat = flag.ToNumericFormat();
                    }

                    if (type == typeof(Int32))
                    {
                        int flag = (int)value;
                        strFormat = flag.ToNumericFormat();
                    }

                    if (type == typeof(String))
                    {
                        string flag = value.ToString();
                        strFormat = flag.EmptyNull().ToUpper();
                    }

                    if (type == typeof(DateTime))
                    {
                        DateTime flag = (DateTime)value;
                        strFormat = flag.ToString("MM/dd/yyyy");
                    }
                }
            }

            return strFormat;
        }

        public static string ToDateTimeSimpleFormat(this DateTime? data)
        {
            string flag = "";

            if (data.HasValue)
                flag = data.Value.ToString("MM/dd/yyyy");
            else
                flag = "--/--/----";

            return flag;
        }

        public static string ToDateTimeFormat(this DateTime? data)
        {
            string flag = "";

            if (data.HasValue)
                flag = data.Value.ToString("MMM dd, yyy");
            else
                flag = "--- --, ----";

            return flag;
        }

        public static string ToNumericFormat(this decimal data)
        {
            string flag = "";
            flag = String.Format("{0:0,0.00}", data);
            return flag;
        }

        public static string ToNumericFormat(this long data)
        {
            string flag = "";
            flag = String.Format("{0:0,0}", data);
            return flag;
        }

        public static string ToNumericFormat(this int data)
        {
            string flag = "";
            flag = String.Format("{0:0,0}", data);
            return flag;
        }

        public static string ToAccountingFormat(this decimal data)
        {
            string flag = "";

            if (data > 0)
            {
                flag = String.Format("{0:0,0.00}", data);
            }

            if (data < 0)
            {
                flag = "(" + String.Format("{0:0,0.00}", Math.Abs(data)) + ")";
            }

            if (data == 0)
            {
                flag = "--";
            }



            return flag;
        }

        public static string ToAccountingFormat(this long data)
        {
            string flag = "";

            if (data > 0)
            {
                flag = String.Format("{0:0,0}", data);
            }

            if (data < 0)
            {
                flag = "(" + String.Format("{0:0,0}", Math.Abs(data)) + ")";
            }

            if (data == 0)
            {
                flag = "--";
            }

            return flag;
        }

        public static string ToAccountingFormat(this int data)
        {
            string flag = "";

            if (data > 0)
            {
                flag = String.Format("{0:0,0}", data);
            }

            if (data < 0)
            {
                flag = "(" + String.Format("{0:0,0}", Math.Abs(data)) + ")";
            }

            if (data == 0)
            {
                flag = "--";
            }



            return flag;
        }


        public static bool HasValue(this string data)
        {
            return !String.IsNullOrEmpty(data);
        }


        public static string EmptyNull(this string data)
        {
            string result = "";
            if (data != null)
                if (!String.IsNullOrEmpty(data.Trim())) result = data;

            if (String.IsNullOrEmpty(result)) result = "";

            return result;
        }

        public static string EmptyNull(this string data, string replace = null)
        {
            string result = "";
            if (data != null)
                if (!String.IsNullOrEmpty(data.Trim())) result = data;

            if (String.IsNullOrEmpty(result) && replace != null) result = replace;

            return result ?? (replace == null ? "" : replace);
        }

        public static decimal ZeroNull(this decimal? data)
        {
            return data.HasValue ? (decimal)data.Value : 0.00m;
        }

        public static long ZeroNull(this long? data)
        {
            return data.HasValue ? (long)data.Value : 0;
        }

        public static int ZeroNull(this int? data)
        {
            return data.HasValue ? (int)data.Value : 0;
        }

        public static string ToString(this bool? data, string strTrue = "", string strFalse = "")
        {
            if (data.HasValue)
            {
                if (data.Value)
                {
                    return strTrue;
                }
                else
                {
                    return strFalse;
                }
            }
            else
            {
                return strFalse;
            }
        }

        public static string ToString(this bool data, string strTrue = "", string strFalse = "")
        {
            if (data)
            {
                return strTrue;
            }
            else
            {
                return strFalse;
            }
        }

        public static bool IsNull(this DateTime? data)
        {
            if (data == null) return true;

            return false;
        }
    }
}