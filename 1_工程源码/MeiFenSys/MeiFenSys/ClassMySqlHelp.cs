using System;
using System.Collections;
using System.Configuration;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace JiaHao.MySqlHelp
{
    /// <summary>
    ///MYSQLHelper 的摘要说明
    /// </summary>
    public class ClassMySqlHelp
    {
        public static string strConn = "server=192.168.1.4;user=ailink;password=ailink2017;database=hikvision_customization;charset=utf8;";//10.42.1.4
        //private static MySqlConnection mysqlconn;192.168.1.4
        private static MySqlConnection mysqlconn = new MySqlConnection(strConn);
        
        //建立数据库连接
        //public static int InitDataBase()
        //{
        //    int result = -1;

        //    StringBuilder tempStringBuilder = new StringBuilder();
        //    string strCurrentDirectory = "";
        //    strCurrentDirectory = System.Environment.CurrentDirectory;
        //    strCurrentDirectory = strCurrentDirectory + "\\SetUp.ini";

        //    //数据库登录信息
        //    Win.GetPrivateProfileString("DataBase", "Server", "", tempStringBuilder, 256, strCurrentDirectory);
        //    strSqlServer = tempStringBuilder.ToString();
        //    Win.GetPrivateProfileString("DataBase", "DataBase", "", tempStringBuilder, 256, strCurrentDirectory);
        //    strDataBase = tempStringBuilder.ToString();
        //    //Win.GetPrivateProfileString("DataBase", "Table", "", tempStringBuilder, 256, strCurrentDirectory);
        //    //strTable = tempStringBuilder.ToString();
        //    Win.GetPrivateProfileString("DataBase", "UserId", "", tempStringBuilder, 256, strCurrentDirectory);
        //    strUserId = tempStringBuilder.ToString();
        //    Win.GetPrivateProfileString("DataBase", "PassWords", "", tempStringBuilder, 256, strCurrentDirectory);
        //    strPassWord = tempStringBuilder.ToString();

        //    result = ConnectDatabase();
        //    //result = 0;


        //    return result;
        //}
        
        //public static int ConnectDatabase()
        //{
        //    int result = -1;

        //    try
        //    {
        //        sqlConnection = new SqlConnection(
        //            "server=" + strSqlServer +
        //            ";database=" + strDataBase +
        //            ";uid=" + strUserId +
        //            ";pwd=" + strPassWord);

        //        sqlConnection.Open();
        //        result = 0;
        //    }
        //    catch (SqlException)
        //    {
        //        MessageBox.Show("数据库连接失败！", "提示", MessageBoxButtons.OK);
        //        Environment.Exit(0);
        //    }
        //    return result;
        //}

        //mysqlconn.Close();
        public ClassMySqlHelp()
        {
            //mysqlconn = new MySqlConnection(strConn);
            if (mysqlconn.State != ConnectionState.Open)
            {
                try
                {
                    mysqlconn.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public MySqlConnection GetConn()
        {
            //mysqlconn = new MySqlConnection(strConn);

            mysqlconn.Open();

            return mysqlconn;
        }
        public void CloseConn()
        {
            if (mysqlconn.State == ConnectionState.Open)
            {
                try
                {
                    mysqlconn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //1.查询数据库
        //public DataSet Select(string sql)
        //{
        //    MySqlConnection mysqlconn = null;
        //    MySqlDataAdapter sda = null;
        //    DataTable dt = null;
        //    try
        //    {
        //        mysqlconn = this.GetConn();
        //        sda = new MySqlDataAdapter(sql, mysqlconn);
        //        dt = new DataTable();
        //        DataSet ds = new DataSet();
        //        sda.Fill(ds);
        //        //sda.Fill(dt);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public DataTable Select(string sql, MySqlConnection mysqlconn)
        {
            //MySqlConnection mysqlconn = null;
            MySqlDataAdapter sda = null;
            DataTable dt = null;
            try
            {
                //mysqlconn = this.GetConn();
                sda = new MySqlDataAdapter(sql, mysqlconn);
                dt = new DataTable();
                //DataSet ds = new DataSet();
                //sda.Fill(ds);
                sda.Fill(dt);
                //mysqlconn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //2.修改数据库
        public int Change(string sql, MySqlConnection mysqlconn)
        {
            //MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                //conn = this.GetConn();
                //mysqlconn.Open();
                cmd = new MySqlCommand(sql, mysqlconn);
                int i = cmd.ExecuteNonQuery();
                //mysqlconn.Close();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //3.插入数据库
        public int Add(string sql, MySqlConnection mysqlconn)
        {
            //MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                //mysqlconn = this.GetConn();
                //mysqlconn.Open();
                cmd = new MySqlCommand(sql, mysqlconn);
                int i = cmd.ExecuteNonQuery();
                //mysqlconn.Close();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //4.删除数据库
        public int Delete(string sql, MySqlConnection mysqlconn)
        {
            //MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                //conn = this.GetConn();
                //mysqlconn.Open();
                cmd = new MySqlCommand(sql, mysqlconn);
                int i = cmd.ExecuteNonQuery();
                mysqlconn.Close();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetSnTab()
        {
            string sql = "select *from sn;";

            //this.GetConn();

            DataTable dt = Select(sql, mysqlconn);
            return dt;
            //DataTable dt = Select(sql);
            //return dt;

        }


        public DataTable GetLogTab()
        {
            string sql = "select *from log;";

            //this.GetConn();

            DataTable dt = Select(sql, mysqlconn);
            return dt;
            //DataTable dt = Select(sql);
            //return dt;

        }








        public int Import(DataTable dtFromExcel, string order, string data, out string returnSn)
        {
            MySqlTransaction trans;
            returnSn = "";
            int ret = -1;
            string sqlSelect;
            string sqlInsert;
            int excelRowCount = dtFromExcel.Rows.Count;//不用减去第一行            

            trans = mysqlconn.BeginTransaction(); //创建事务
            for (int i = 0; i < excelRowCount; i++)
            {
                sqlSelect = "SELECT * FROM sn WHERE `sn` = '{0}';";
                sqlInsert = "INSERT INTO sn (`sn`, `uid`, `order`,`create_time`)VALUES ('{0}', '{1}', '{2}','{3}');";
                string sn = dtFromExcel.Rows[i][0].ToString().Trim();
                //取表格中sn的后6位
                sn = sn.Substring(sn.Length - 6);
                //HV+填写+sn的后6位
                sn = string.Format("HV{0}{1}", data, sn);


                string uid = dtFromExcel.Rows[i][2].ToString();

                sqlSelect = string.Format(sqlSelect, sn);

                sqlInsert = string.Format(sqlInsert, sn, uid, order, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));//yyyy-MM-dd hh:mm:ss
                #region

                try
                {
                    DataTable dt = Select(sqlSelect, mysqlconn);

                    int rowCount = dt.Rows.Count;
                    if (rowCount == 0)//未查询到
                    {

                        //插入sn-uid-order
                        Add(sqlInsert, mysqlconn);
                    }
                    else //查询到了相同的
                    {
                        trans.Rollback();
                        returnSn = sn;
                        return ret;
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return ret;
                }
                #endregion

            }
            //数据插入完了要提交。
            trans.Commit();
            ret = 0;
            return ret;
        }

        public int SelectAndInsert(string sn, string uid, string station, out string errorLog)
        {
            MySqlTransaction trans;
            errorLog = "";
            int ret = -1;
            string selectSql = "SELECT `sn`, `uid`, `order` FROM sn WHERE `sn` = '{0}';";
            string selectLogSql = "SELECT * FROM log WHERE `sn` = '{0}' AND `result` = '{1}';";
            string insertSql = "INSERT INTO log (`sn`, `uid`, `result`, `station`, `content`,`create_time`)VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}');";
            selectSql = string.Format(selectSql, sn);
            selectLogSql = string.Format(selectLogSql, sn, 1);//查询结果为1的sn
            DataTable dtLog = Select(selectLogSql, mysqlconn);
            int rowLogCount = dtLog.Rows.Count;
            if (rowLogCount != 0)
            {
                errorLog = string.Format("SN:{0}重号或重复测试，请处理！", sn);
                return ret;
            }
            try
            {
                DataTable dt = Select(selectSql, mysqlconn);
                int rowCount = dt.Rows.Count;
                if (rowCount != 1)
                {
                    if (rowCount == 0)
                    {
                        errorLog = string.Format("SN:{0}不存在", sn);
                    }
                    if (rowCount > 1)
                    {
                        errorLog = string.Format("表格中SN:{0}存在多个", sn);
                    }
                    return ret;
                }
                string getUid = dt.Rows[0][1].ToString().Trim();
                int result = 0;
                if (uid != getUid)
                {
                    result = 0;
                    errorLog = string.Format("SN:{0} 和 UID:{1}不是对应关系", sn, uid);
                    insertSql = string.Format(insertSql, sn, uid, result, station, errorLog, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                    //return ret;
                }
                else
                {
                    result = 1;
                    errorLog = string.Format("SN:{0} 和 UID:{1}是对应关系", sn, uid);
                    insertSql = string.Format(insertSql, sn, uid, result, station, errorLog, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    ret = 0;

                    //return ret;
                }
                //都要插入数据
                trans = mysqlconn.BeginTransaction(); //创建事务
                try
                {
                    Add(insertSql, mysqlconn);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorLog = ex.Message;
                    return ret;
                }
                return ret;


            }
            catch (Exception ex)
            {
                errorLog = ex.Message;
                return ret;
            }

        }

        public int InBoxSelectAndInsert(string[] snArry, string BoxNum, string station, out string errorLog)
        {
            MySqlTransaction trans;
            bool errorFlag = false;
            errorLog = "";
            int ret = -1;
            string uid;
            trans = mysqlconn.BeginTransaction(); //创建事务
            foreach (var eachSn in snArry)
            {
                string selectSql = "SELECT * FROM sn WHERE `sn` = '{0}';";
                selectSql = string.Format(selectSql, eachSn);
                DataTable dt = Select(selectSql, mysqlconn);
                
                int rowCount = dt.Rows.Count;
                //查询sn标准表，看是否存在
                if (rowCount == 0)
                {
                    errorLog += string.Format("失败—SN:{0}不存在\r\n", eachSn);
                    errorFlag = true;
                    continue;
                }
                string packageNum = dt.Rows[0][4].ToString();
                //查询到包装箱号,说明此sn已经入了包装箱,
                if (!string.IsNullOrEmpty(packageNum))
                {
                    errorLog += string.Format("失败—SN:{0}已经入了包装箱：{1}，请处理！\r\n", eachSn, packageNum);
                    errorFlag = true;
                    continue;
                }

                //未查询到的，继续查漏
                string selectLogSql = "SELECT * FROM log WHERE `sn` = '{0}' AND `result` = '{1}';";
                selectLogSql = string.Format(selectLogSql, eachSn, 1);//查询结果为1的sn
                DataTable dtLog = Select(selectLogSql, mysqlconn);
                int rowLogCount = dtLog.Rows.Count;
                //包装检查没有过站
                if (rowLogCount == 0)
                {
                    errorLog += string.Format("失败—SN:{0}包装检查未过站，请处理！\r\n", eachSn);
                    errorFlag = true;
                    continue;
                }
                uid = dt.Rows[0][2].ToString();
                

                //查重查漏通过，继续插入
                string insertLogSql = "INSERT INTO log (`sn`, `uid`,`result`, `station`, `content`,`create_time`)VALUES ('{0}', '{1}',{2}, '{3}', '{4}', '{5}');";

                string passLog = string.Format("OK—SN:{0}装入箱号:{1}\r\n", eachSn, BoxNum);
                errorLog += passLog;
                insertLogSql = string.Format(insertLogSql, eachSn, uid, 2, station, passLog, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                string updataSnSql = "UPDATE sn SET `package`='{0}' WHERE `sn` = '{1}';";
                updataSnSql = string.Format(updataSnSql, BoxNum, eachSn);

                try
                {
                    Add(insertLogSql, mysqlconn);
                    Change(updataSnSql, mysqlconn);//更新箱号
                }
                catch (Exception ex)
                {
                    errorFlag = true;
                    errorLog += ex.Message;
                    break;
                }
            }
            if (errorFlag)
            {
                trans.Rollback();
                return ret;
            }
            trans.Commit();
            ret = 0;
            return ret;

        }
        /// <summary>
        /// 测试数据导入MySql
        /// </summary>
        //public void Import()
        //{
        //    MySqlTransaction trans;

        //    int ret = -1;
        //    string sql = "SELECT * FROM sn WHERE `sn` = 'ooo';";
        //    //this.GetConn();
        //    trans = mysqlconn.BeginTransaction(); //创建事务
        //    for (int i = 0; i < 5; i++)
        //    {
        //        string[] sqlSelectArry ={
        //        "SELECT * FROM sn WHERE `sn` = 'aa';",
        //        "SELECT * FROM sn WHERE `sn` = 'ab';",
        //        "SELECT * FROM sn WHERE `sn` = 'ac';",
        //        "SELECT * FROM sn WHERE `sn` = 'aa';",
        //        "SELECT * FROM sn WHERE `sn` = 'ac';"
        //                                };
        //        string[] sqlInsertArry ={
        //        "INSERT INTO sn (`sn`, `uid`, `order`)VALUES ('InserTestSn1', 'InserTestUid1', 'dd1');",
        //        "INSERT INTO sn (`sn`, `uid`, `order`)VALUES ('InserTestSn2', 'InserTestUid2', 'dd1');",
        //        "INSERT INTO sn (`sn`, `uid`, `order`)VALUES ('InserTestSn3', 'InserTestUid3', 'dd1');",
        //        "INSERT INTO sn (`sn`, `uid`, `order`)VALUES ('InserTestSn4', 'InserTestUid4', 'dd1');",
        //        "INSERT INTO sn (`sn`, `uid`, `order`)VALUES ('InserTestSn5', 'InserTestUid5', 'dd1');"
        //                                };
        //        #region

        //        try
        //        {

        //            DataTable dt = Select(sqlSelectArry[i], mysqlconn);

        //            int rowCount = dt.Rows.Count;
        //            if (rowCount == 0)//未查询到
        //            {

        //                //插入sn-uid-order
        //                //this.GetConn();
        //                Add(sqlInsertArry[i], mysqlconn);
        //                //trans.Commit();
        //                ret = 0;
        //                //return ret;
        //            }
        //            else //查询到了相同的
        //            {
        //                //throw new Exception("查询到数据库已有SN存在");
        //                trans.Rollback();
        //                return;
        //                //break;
        //                //break;
        //            }

        //            //return ret;
        //        }
        //        catch (Exception)
        //        {
        //            trans.Rollback();
        //            return;
        //            //throw ex;
        //        }
        //        #endregion
        //    }
        //    //插入完了也要提交，使回滚失效。
        //    trans.Commit();

        //    //#region
        //    //try
        //    //{

        //    //    DataTable dt = Select(sql, mysqlconn);

        //    //    int rowCount = dt.Rows.Count;
        //    //    if (rowCount == 0)//未查询到
        //    //    {

        //    //        //插入sn-uid-order

        //    //        trans.Commit();
        //    //        ret = 0;
        //    //        return ret;
        //    //    }
        //    //    else //查询到了相同的
        //    //    {
        //    //        //throw new Exception("查询到数据库已有SN存在");
        //    //        trans.Rollback();
        //    //        //break;
        //    //    }

        //    //    return ret;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    trans.Rollback();
        //    //    throw ex;
        //    //}
        //    //#endregion
        //}



        //public DataSet Test()
        //{
        //    string sql = "select *from sn;";
        //    DataSet ds = Select(sql);
        //    return ds;
        //    //DataTable dt = Select(sql);
        //    //return dt;

        //}

#if NOTBAD
        //应读配置文件
        //数据库连接字符串
        //public static string Conn = "Database='数据库名';Data Source='数据库服务器地址';User Id='数据库用户名';Password='密码';charset='utf8';pooling=true";
        private static string mysqlString = "server=localhost;User Id=ailink;password=ailink2017;Database=hikvision_customization";

        //建立mysql数据库链接
        public MySqlConnection getMySqlConn()
        {
            //string constr = "server=localhost;User Id=root;password=123456;Database=qqmusicdistinct";
            string constr = mysqlString;
            MySqlConnection mycon = new MySqlConnection(constr);
            return mycon;
        }
        //建立执行命令语句对象
         public MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
         {
             MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
             return mySqlCommand;
         }
         //编写sql语句，对数据做相应处理
         public void setData()
         {
             MySqlConnection mysql = getMySqlConn();
             //查询sql
             String sqlSearch = "select * from sn";
             //插入sql
             //String sqlInsert = "insert into student values (12,'张三',25,'大专')";
             //修改sql
             //String sqlUpdate = "update student set name='李四' where id= 3";
             //删除sql
             //String sqlDel = "delete from student where id = 12";

             //四种语句对象
             MySqlCommand mySqlCommand = getSqlCommand(sqlSearch, mysql);
             //MySqlCommand mySqlCommand = getSqlCommand(sqlInsert, mysql);
             //MySqlCommand mySqlCommand = getSqlCommand(sqlUpdate, mysql);
             //MySqlCommand mySqlCommand = getSqlCommand(sqlDel, mysql);

             mysql.Open();

             getResultset(mySqlCommand);
             //getInsert(mySqlCommand);
             //getUpdate(mySqlCommand);
             //getDel(mySqlCommand);
             //记得关闭
             mysql.Close();
         }

         //查询并获得结果集并遍历
         public void getResultset(MySqlCommand mySqlCommand)
         {
             MySqlDataReader reader = mySqlCommand.ExecuteReader();
             try
             {
                 while (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         //MessageBox.Show("歌曲名:" + reader.GetString(1) + "|歌手:" + reader.GetString(2));
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 reader.Close();
             }
         }
         //添加数据
         public static void getInsert(MySqlCommand mySqlCommand)
         {
             try
             {
                 mySqlCommand.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 String message = ex.Message;
                 Console.WriteLine("插入数据失败了！" + message);
                 throw ex;
             }

         }

         //修改数据
         public static void getUpdate(MySqlCommand mySqlCommand)
         {
             try
             {
                 mySqlCommand.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 String message = ex.Message;
                 Console.WriteLine("修改数据失败了！" + message);
                 throw ex;
             }
         }

         //删除数据
         public static void getDel(MySqlCommand mySqlCommand)
         {
             try
             {
                 mySqlCommand.ExecuteNonQuery();
             }
             catch (Exception ex)
             {
                 String message = ex.Message;
                 Console.WriteLine("删除数据失败了！" + message);
                 throw ex;
             }
         }
#endif

#if Detail
        //数据库连接字符串
        public static string Conn = "Database='wp';Data Source='localhost';User Id='root';Password='root';charset='utf8';pooling=true";

        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());



        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:
        ///  MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，

            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个存在的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            MySqlCommand cmd = new MySqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="commandParameters">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 找回缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
#endif
    }
}



