using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

namespace JiaHao.ExcelHelp
{
    class ClassExcelAndDt
    {
        public DataTable ExcelToDS(string Path, string order)
        {
            string tableName="";
            bool IsContainOrder = false;
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                tableName = schemaTable.Rows[i][2].ToString();//"TABLE NAME"

                if (tableName.Contains(order))
                {
                    IsContainOrder = true;
                    break;
                }
                
            }
            if (IsContainOrder==false)
            {
                return null;
            }
            string strExcel = "";
            strExcel = string.Format("select * from [{0}]", tableName);

            //if (isStandardFormat)
            //{
            //    strExcel = "select * from [sheet1$]";//获取整张表
            //    //strExcel = "select * ";//获取整张表
            //}
            //else
            //{
            //    strExcel = "select * from [sheet$]";//获取整张表
            //}

            OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
            DataTable dt = new DataTable();
            myCommand.Fill(dt);

            conn.Close();
            return dt;

        }

    }
}
