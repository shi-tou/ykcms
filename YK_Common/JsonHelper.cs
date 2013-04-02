using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using YK.Common;

namespace YK.Common
{
    /// <summary>
    /// 不使用json序列化，Json转换
    /// 整理人：杨良斌
    /// </summary>
    public class JsonHelper
    {       
        /// <summary>
        /// 将datatable数据转换成JSON数据
        /// </summary>
        public static string ToJson(DataTable dt,string fields)
        {
            if (fields == "")
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (fields != "") fields += ",";
                    fields += dc.ColumnName;
                }
            }

            string[] fieldsArr = fields.Split(",".ToCharArray());
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < fieldsArr.Length; j++)
                    {
                        Json.Append("\"" + fieldsArr[j] + "\":\"" + dt.Rows[i][fieldsArr[j]].ToString() + "\"");
                        if (j < fieldsArr.Length - 1)
                            Json.Append(",");
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        /// <summary>
        /// 将datatable数据转换成JSON数据
        /// </summary>
        public static string ToJson(DataRow[] drs, string fields)
        {
            if (fields == "")
            {
                foreach (DataColumn dc in drs[0].Table.Columns)
                {
                    if (fields != "") fields += ",";
                    fields += dc.ColumnName;
                }
            }

            string[] fieldsArr = fields.Split(",".ToCharArray());
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < fieldsArr.Length; j++)
                    {
                        Json.Append("\"" + fieldsArr[j] + "\":\"" + drs[i][fieldsArr[j]].ToString() + "\"");
                        if (j < fieldsArr.Length - 1)
                            Json.Append(",");
                    }
                    Json.Append("}");
                    if (i < drs.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        /// <summary>
        /// 将DataRow转换成JSON
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string ToJson(DataRow row,string fields)
        {
            if (fields == "")
            {
                foreach (DataColumn dc in row.Table.Columns)
                {
                    if (fields != "") fields += ",";
                    fields += dc.ColumnName;
                }
            }

            StringBuilder Json = new StringBuilder();
            string[] fieldsArr = fields.Split(",".ToCharArray());
            Json.Append("{");
            for (int j = 0; j < fieldsArr.Length; j++)
            {
                if (Json.ToString() != "{") Json.Append(",");
                Json.Append("\"" + fieldsArr[j] + "\":\"" + row[fieldsArr[j]].ToString() + "\"");
            }
            Json.Append("}");

            return Json.ToString();
        }
        /// <summary>
        /// 将datatable数据转换成JSON数据,转换文章内容的特殊字符
        /// </summary>
        public static string ToJsonForNews(DataTable dt, string fields)
        {
            if (fields == "")
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (fields != "") fields += ",";
                    fields += dc.ColumnName;
                }
            }

            string[] fieldsArr = fields.Split(",".ToCharArray());
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < fieldsArr.Length; j++)
                    {
                        if (fieldsArr[j] == "Content")
                        {
                            Json.Append("\"" + fieldsArr[j] + "\":\"" + StringPlus.HtmlFilter(dt.Rows[i][fieldsArr[j]].ToString()) + "\"");
                        }
                        else
                        {
                            Json.Append("\"" + fieldsArr[j] + "\":\"" + dt.Rows[i][fieldsArr[j]].ToString() + "\"");
                        }
                        if (j < fieldsArr.Length - 1)
                            Json.Append(",");
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }
        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="strJson">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string strJson)
        {
            strJson = strJson.Replace("\\", "");
            strJson = strJson.Replace("\b", "");
            strJson = strJson.Replace("\t", "");
            strJson = strJson.Replace("\n", "");
            strJson = strJson.Replace("\f", "");
            strJson = strJson.Replace("\r", "");
            strJson = strJson.Replace("\"","\\\"");
            return strJson;
        }
    }
}

