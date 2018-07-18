using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace wirelessCharging
{
    class MyJsonConvert
    {
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                ////string a="{"code":1000,"message":"成功","data":[{"sn":"123","data":"{\"Lin50mA\":\"0\",\"Vout50mA\":\"0.00032\",\"Fre50mA\":\"2363.1\",\"Eff50mA\":\"∞\",\"Lin100mA\":\"\",\"Vout100mA\":\"\",\"Fre100mA\":\"\",\"Eff100mA\":\"\",\"Lin150mA\":\"0\",\"Vout150mA\":\"0.00032\",\"Fre150mA\":\"2460\",\"Eff150mA\":\"∞\",\"Lin200mA\":\"\",\"Vout200mA\":\"\",\"Fre200mA\":\"\",\"Eff200mA\":\"\",\"Lin300mA\":\"0\",\"Vout300mA\":\"0.00032\",\"Fre300mA\":\"2417.9\",\"Eff300mA\":\"∞\",\"Lin400mA\":\"\",\"Vout400mA\":\"\",\"Fre400mA\":\"\",\"Eff400mA\":\"\",\"Lin500mA\":\"\",\"Vout500mA\":\"\",\"Fre500mA\":\"\",\"Eff500mA\":\"\",\"Lin600mA\":\"\",\"Vout600mA\":\"\",\"Fre600mA\":\"\",\"Eff600mA\":\"\"}","result":0,"testTime":"2018-06-21 15:14:16"},{"sn":"123","data":"{\"Lin50mA\":\"0\",\"Vout50mA\":\"0.00037\",\"Fre50mA\":\"2927.2\",\"Eff50mA\":\"∞\",\"Lin100mA\":\"\",\"Vout100mA\":\"\",\"Fre100mA\":\"\",\"Eff100mA\":\"\",\"Lin150mA\":\"0\",\"Vout150mA\":\"0.0004\",\"Fre150mA\":\"2765.7\",\"Eff150mA\":\"∞\",\"Lin200mA\":\"\",\"Vout200mA\":\"\",\"Fre200mA\":\"\",\"Eff200mA\":\"\",\"Lin300mA\":\"0\",\"Vout300mA\":\"0.00038\",\"Fre300mA\":\"2569.1\",\"Eff300mA\":\"∞\",\"Lin400mA\":\"\",\"Vout400mA\":\"\",\"Fre400mA\":\"\",\"Eff400mA\":\"\",\"Lin500mA\":\"\",\"Vout500mA\":\"\",\"Fre500mA\":\"\",\"Eff500mA\":\"\",\"Lin600mA\":\"\",\"Vout600mA\":\"\",\"Fre600mA\":\"\",\"Eff600mA\":\"\"}","result":0,"testTime":"2018-06-21 15:21:55"},{"sn":"123","data":"{\"Lin50mA\":\"0\",\"Vout50mA\":\"0.00038\",\"Fre50mA\":\"19685\",\"Eff50mA\":\"∞\",\"Lin100mA\":\"\",\"Vout100mA\":\"\",\"Fre100mA\":\"\",\"Eff100mA\":\"\",\"Lin150mA\":\"0\",\"Vout150mA\":\"0.00039\",\"Fre150mA\":\"13520\",\"Eff150mA\":\"∞\",\"Lin200mA\":\"\",\"Vout200mA\":\"\",\"Fre200mA\":\"\",\"Eff200mA\":\"\",\"Lin300mA\":\"0\",\"Vout300mA\":\"0.00039\",\"Fre300mA\":\"13789\",\"Eff300mA\":\"∞\",\"Lin400mA\":\"\",\"Vout400mA\":\"\",\"Fre400mA\":\"\",\"Eff400mA\":\"\",\"Lin500mA\":\"\",\"Vout500mA\":\"\",\"Fre500mA\":\"\",\"Eff500mA\":\"\",\"Lin600mA\":\"\",\"Vout600mA\":\"\",\"Fre600mA\":\"\",\"Eff600mA\":\"\"}","result":0,"testTime":"2018-06-21 15:23:38"}]}";
                //string a = "123";
                //a=a.Insert(0, "[");
                //a += "]";
                //json=json.Replace("\"code\":1000,\"message\":\"成功\",", "");
                json = json.Insert(0, "[");
                json += "]";
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);


                //ArrayList arrayList  = JsonConvert.DeserializeObject<ArrayList>(json);

                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                if (current != "data")
                                    dataTable.Columns.Add(current, dictionary[current].GetType());
                                else
                                {
                                    ArrayList list = dictionary[current] as ArrayList;
                                    foreach (Dictionary<string, object> dic in list)
                                    {
                                        foreach (string key in dic.Keys)
                                        {
                                            dataTable.Columns.Add(key, dic[key].GetType());
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        //Rows
                        string root = "";
                        foreach (string current in dictionary.Keys)
                        {
                            if (current != "data")
                                root = current;
                            else
                            {
                                ArrayList list = dictionary[current] as ArrayList;
                                foreach (Dictionary<string, object> dic in list)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[root] = dictionary[root];
                                    foreach (string key in dic.Keys)
                                    {
                                        dataRow[key] = dic[key];
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
    }
}
