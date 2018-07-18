using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace IMEISNPrint
{
    #region log信息上传
    enum Method
    {
        GET,
        POST,
        NULL,
    }
    class HttpServer
    {
        //private static ResponseInfo1 responseInfo1;
        private static ResponseInfo responseInfo;

        private static ResponseSimpleInfo responseSimpleInfo;
        private static RpSearchSnTimeInfo SearchSnTimeInfo;
        private static RpSearchBoxInfo responseSearchBoxInfo;

        //private static ResponseInfo1 responseInfo1;
        private static string ip = "192.168.1.16";                //IP地址
        private static string port = "8085";                      //端口
        private static string url;                                //获取计划

        public static string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public static string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public static void SetUrlGetPlan()
        {
            url = string.Format("http://{0}:{1}", Ip, Port);
        }

        /// <summary>
        /// 获取URL地址下的信息，将获取的json信息转换为ResponseInfo类的信息
        /// </summary>
        /// <param name="URL">地址</param>
        /// <param name="rpSimpleInfo"></param>
        /// <param name="msg">try,catch出的错误信息</param>
        /// <returns></returns>
        //public static int GetInfo(string URL, out ResponseInfo1 dataInfo, out string msg)
        //{
        //    string httpResponse;
        //    msg = null;
        //    int ret = -1;
        //    dataInfo = null;

        //    if (!string.IsNullOrEmpty(URL))
        //    {
        //        SetUrlGetPlan();
        //        ret = GetHttpResponse(url + URL, Method.GET, out httpResponse, out msg);
        //        //ret = 0;
        //        //httpResponse = "{\"code\": \"1000\",\"message\": \"success\",\"data\": {\"plan\": {\"id\": 2,\"code\": \"4501461344\", \"product\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"CS -C2C-32WFR\",\"createAt\": \"2017 -09-26 15:07:58\",\"updateAt\": \"2017 -09-26 15:07:58\"},\"processScheme\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"SN加密；高清检测；网络检测一；网络检测二；包装检查\",\"createAt\": \"2017 -09-26 15:08:26\",\"updateAt\": \"2017 -09-26 15:08:26\"},\"start\": \"835203727\",\"end\": \"835203826\",\"quantity\": 100,\"createAt\": \"2017-09-26 15:11:11\"}}}";

        //        if (ret != 0)
        //        {
        //            dataInfo = null;
        //            return ret;
        //        }
        //        try
        //        {
        //            ret = -1;
        //            responseSimpleInfo = JsonConvert.DeserializeObject(httpResponse, typeof(ResponseInfo1)) as ResponseInfo1;
        //            if (responseSimpleInfo.code != "1000")
        //            {
        //                msg = responseSimpleInfo.message + "\r\n";
        //                dataInfo = responseSimpleInfo;
        //                return ret;
        //            }
        //            ret = 0;
        //        }
        //        catch (Exception e)
        //        {
        //            msg = e.Message;
        //        }
        //    }
        //    dataInfo = responseSimpleInfo;
        //    return ret;
        //}

        public static int GetSimpleInfo(string URL, out ResponseSimpleInfo rpSimpleInfo, out string msg)
        {
            string httpResponse;
            msg = null;
            int ret = -1;
            rpSimpleInfo = null;

            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();
                ret = GetHttpResponse(url + URL, Method.GET, out httpResponse, out msg);
                //ret = 0;
                //httpResponse = "{\"code\": \"1000\",\"message\": \"success\",\"data\": {\"plan\": {\"id\": 2,\"code\": \"4501461344\", \"product\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"CS -C2C-32WFR\",\"createAt\": \"2017 -09-26 15:07:58\",\"updateAt\": \"2017 -09-26 15:07:58\"},\"processScheme\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"SN加密；高清检测；网络检测一；网络检测二；包装检查\",\"createAt\": \"2017 -09-26 15:08:26\",\"updateAt\": \"2017 -09-26 15:08:26\"},\"start\": \"835203727\",\"end\": \"835203826\",\"quantity\": 100,\"createAt\": \"2017-09-26 15:11:11\"}}}";

                if (ret != 0)
                {
                    rpSimpleInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    responseSimpleInfo = JsonConvert.DeserializeObject(httpResponse, typeof(ResponseSimpleInfo)) as ResponseSimpleInfo;
                    if (responseSimpleInfo.code != 1000)
                    {
                        msg = responseSimpleInfo.message + "\r\n";
                        rpSimpleInfo = responseSimpleInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            rpSimpleInfo = responseSimpleInfo;
            return ret;
        }
        public static int GetSearchSnTimeInfo(string URL, out RpSearchSnTimeInfo rpSeaechSnTimeInfo, out string msg)
        {
            string httpResponse;
            msg = null;
            int ret = -1;
            rpSeaechSnTimeInfo = null;

            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();
                ret = GetHttpResponse(url + URL, Method.GET, out httpResponse, out msg);
                //ret = 0;
                //httpResponse = "{\"code\": \"1000\",\"message\": \"success\",\"data\": {\"plan\": {\"id\": 2,\"code\": \"4501461344\", \"product\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"CS -C2C-32WFR\",\"createAt\": \"2017 -09-26 15:07:58\",\"updateAt\": \"2017 -09-26 15:07:58\"},\"processScheme\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"SN加密；高清检测；网络检测一；网络检测二；包装检查\",\"createAt\": \"2017 -09-26 15:08:26\",\"updateAt\": \"2017 -09-26 15:08:26\"},\"start\": \"835203727\",\"end\": \"835203826\",\"quantity\": 100,\"createAt\": \"2017-09-26 15:11:11\"}}}";

                if (ret != 0)
                {
                    rpSeaechSnTimeInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    SearchSnTimeInfo = JsonConvert.DeserializeObject(httpResponse, typeof(RpSearchSnTimeInfo)) as RpSearchSnTimeInfo;
                    if (SearchSnTimeInfo.code != 1000)
                    {
                        msg = SearchSnTimeInfo.message + "\r\n";
                        rpSeaechSnTimeInfo = SearchSnTimeInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            rpSeaechSnTimeInfo = SearchSnTimeInfo;
            return ret;
        }
        public static int GetSearchBoxInfo(string URL, out RpSearchBoxInfo rpSearchBoxInfo, out string msg)
        {
            string httpResponse;
            msg = null;
            int ret = -1;
            rpSearchBoxInfo = null;

            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();
                ret = GetHttpResponse(url + URL, Method.GET, out httpResponse, out msg);
                //ret = 0;
                //httpResponse = "{\"code\": \"1000\",\"message\": \"success\",\"data\": {\"plan\": {\"id\": 2,\"code\": \"4501461344\", \"product\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"CS -C2C-32WFR\",\"createAt\": \"2017 -09-26 15:07:58\",\"updateAt\": \"2017 -09-26 15:07:58\"},\"processScheme\": {\"id\": 1,\"code\": \"P0001\",\"name\": \"SN加密；高清检测；网络检测一；网络检测二；包装检查\",\"createAt\": \"2017 -09-26 15:08:26\",\"updateAt\": \"2017 -09-26 15:08:26\"},\"start\": \"835203727\",\"end\": \"835203826\",\"quantity\": 100,\"createAt\": \"2017-09-26 15:11:11\"}}}";

                if (ret != 0)
                {
                    rpSearchBoxInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    responseSearchBoxInfo = JsonConvert.DeserializeObject(httpResponse, typeof(RpSearchBoxInfo)) as RpSearchBoxInfo;
                    if (responseSearchBoxInfo.code != 1000)
                    {
                        msg = responseSearchBoxInfo.message + "\r\n";
                        rpSearchBoxInfo = responseSearchBoxInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            rpSearchBoxInfo = responseSearchBoxInfo;
            return ret;
        }
        public static int PostSimpleInfo(string URL, out ResponseSimpleInfo rpSimpleInfo, out string msg, object objPost = null)
        {
            string httpResponse;
            msg = null;
            url = null;
            int ret = -1;
            string post;
            if (objPost is string)
            {
                post = (string)objPost;
            }
            else
            {
                post = JsonConvert.SerializeObject(objPost);
            }


            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();

                ret = PostHttpResponse(url + URL, Method.POST, out httpResponse, out msg, post);
                if (ret != 0)
                {
                    rpSimpleInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    responseSimpleInfo = JsonConvert.DeserializeObject(httpResponse, typeof(ResponseSimpleInfo)) as ResponseSimpleInfo;
                    if (responseSimpleInfo.code != 1000)
                    {
                        msg = responseSimpleInfo.message + "\r\n";
                        rpSimpleInfo = responseSimpleInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    rpSimpleInfo = null;
                    return ret;
                }
            }
            rpSimpleInfo = responseSimpleInfo;
            return ret;
        }
        public static int PostInfo(string URL, out ResponseInfo dataInfo, out string msg, string post = null)
        {
            string httpResponse;
            msg = null;
            url = null;
            int ret = -1;

            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();
                ret = PostHttpResponse(url + URL, Method.POST, out httpResponse, out msg, post);
                if (ret != 0)
                {
                    dataInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    responseInfo = JsonConvert.DeserializeObject(httpResponse, typeof(ResponseInfo)) as ResponseInfo;
                    if (responseInfo.code != "1000")
                    {
                        msg = responseInfo.message + "\r\n";
                        dataInfo = responseInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    dataInfo = null;
                    return ret;
                }
            }
            dataInfo = responseInfo;
            return ret;
        }
        public static int myPostInfo(string URL, out ResponseInfo dataInfo, out string msg, object objPost = null)
        {
            string httpResponse;
            msg = null;
            url = null;
            int ret = -1;
            string post;

            post = JsonConvert.SerializeObject(objPost);

            if (!string.IsNullOrEmpty(URL))
            {
                SetUrlGetPlan();

                ret = PostHttpResponse(url + URL, Method.POST, out httpResponse, out msg, post);
                if (ret != 0)
                {
                    dataInfo = null;
                    return ret;
                }
                try
                {
                    ret = -1;
                    responseInfo = JsonConvert.DeserializeObject(httpResponse, typeof(ResponseInfo)) as ResponseInfo;
                    if (responseInfo.code != "1000")
                    {
                        msg = responseInfo.message + "\r\n";
                        dataInfo = responseInfo;
                        return ret;
                    }
                    ret = 0;
                }
                catch (Exception e)
                {
                    msg = e.Message;
                    dataInfo = null;
                    return ret;
                }
            }
            dataInfo = responseInfo;
            return ret;
        }
        /// <summary>
        /// 获取或传输信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method">与服务器通信的方法，POST或者为GET</param>
        /// <param name="msg">try,catch出的错误信息</param>
        /// <param name="post">POST的数据，json格式</param>
        /// <returns></returns>
        public static int GetHttpResponse(string url, Method method, out string httpRespose, out string msg)
        {
            int ret = -1;
            httpRespose = null;
            msg = null;

            HttpWebResponse httpWebResponse = null;
            int retry = 2;

        REDO:
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ProtocolVersion = new Version(1, 1);
                httpWebRequest.Timeout = 1500;
                switch (method)
                {
                    case Method.GET:
                        httpWebRequest.Method = Method.GET.ToString();
                        break;

                    default:
                        msg = "HTTP未知方法";
                        return ret;
                }

                try
                {
                    retry--;
                    long startTime = GetTimeStamp();
                    //while (GetTimeStamp() - startTime < 5000)
                    //{
                    httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                    //}
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                    msg = e.Message;
                    if (retry >= 0)
                    {
                        goto REDO;//重新尝试
                    }
                    return ret;

                }

                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        httpRespose = streamReader.ReadToEnd();
                        int start = httpRespose.IndexOf("{");
                        int end = httpRespose.LastIndexOf("}");
                        int length = end - start + 1;
                        httpRespose = httpRespose.Substring(start, length);
                        ret = 0;
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return ret;
        }

        public static int PostHttpResponse(string url, Method method, out string httpRespose, out string msg, string post = null)
        {
            int ret = -1;
            httpRespose = null;

            //dataInfo = null;
            msg = null;
            HttpWebResponse httpWebResponse = null;
            int retry = 6;

        REDO:
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ProtocolVersion = new Version(1, 1);
                httpWebRequest.Timeout = 1000;

                switch (method)
                {
                    case Method.POST:
                        httpWebRequest.Method = Method.POST.ToString();
                        if (!string.IsNullOrEmpty(post))
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes(post);
                            httpWebRequest.ContentLength = buffer.Length;
                            httpWebRequest.GetRequestStream().Write(buffer, 0, buffer.Length);
                            ret = 0;
                        }
                        break;
                    default:
                        msg = "HTTP未知方法";
                        //dataInfo = null;
                        return ret;
                }

                try
                {
                    retry--;
                    httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                    msg = e.Message;
                    if (retry >= 0)
                    {
                        goto REDO;
                    }
                    return ret;
                }
                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        httpRespose = streamReader.ReadToEnd();
                        int start = httpRespose.IndexOf("{");
                        int end = httpRespose.LastIndexOf("}");
                        int length = end - start + 1;
                        httpRespose = httpRespose.Substring(start, length);
                        //dataInfo = JsonConvert.DeserializeObject(httpRespose, typeof(ResponseInfo)) as ResponseInfo;
                        ret = 0;
                    }
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return ret;
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return long.Parse(Convert.ToInt64(ts.TotalMilliseconds).ToString());
        }

        private static int ProcessPlan(List<Data> imeiList, out string msg)
        {
            int ret = -1;
            msg = null;

            Data imeiPage = imeiList[0];

            //将计划代码转换成LIST
            List<string> tempPlanCodes = new List<string> { };
            foreach (var item in imeiList)
            {
                tempPlanCodes.Add(item.imeiPage.ToString());
            }

            ret = 0;
            return ret;
        }

    }

    class ResponseInfo
    {
        public string code { get; set; }
        public string message { get; set; }
        public Data_SN data { get; set; }

        //public ImeiPage imeiPage { get; set; }

    }
    #region 查询SN、上报Test、上报包装、按箱号查询数量
    class ResponseSimpleInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public PackData data { get; set; }

    }
    class PackData
    {
        public int count { get; set; }
    }
    #endregion
    #region 按SN/时间查询 RpSearchSnTimeInfo
    class RpSearchSnTimeInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<SnData> data { get; set; }

    }
    class SnData
    {
        public string sn { get; set; }
        public string data { get; set; }
        public int result { get; set; }
        public string Result { get; set; }
        public string testTime { get; set; }

    }
    #endregion
    #region
    class ConvertSearchSnTimeInfo
    {
        public List<ConvertSnData> data { get; set; }
    }
    class ConvertSnData
    {
        public string sn { get; set; }
        public string data { get; set; }
        public string result { get; set; }
        public string testTime { get; set; }

    }
    #endregion
    #region 按箱号查询 RpSearchBoxInfo
    class RpSearchBoxInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<BoxData> data { get; set; }
    }
    class BoxData
    {
        public string sn { get; set; }
        public string packageNumber { get; set; }
        public string packageTime { get; set; }

    }
    #endregion


    [Serializable]
    class ResponseInfo1
    {
        public string code { get; set; }
        public string message { get; set; }
        public Data_SN data { get; set; }
    }

    class PostTestInfo
    {
        public string sn { get; set; }
        public string data { get; set; }
        public int result { get; set; }
    }

    class TestData
    {
        public string Lin50mA { get; set; }
        public string Vout50mA { get; set; }
        public string Fre50mA { get; set; }
        public string Eff50mA { get; set; }

        public string Lin100mA { get; set; }
        public string Vout100mA { get; set; }
        public string Fre100mA { get; set; }
        public string Eff100mA { get; set; }

        public string Lin150mA { get; set; }
        public string Vout150mA { get; set; }
        public string Fre150mA { get; set; }
        public string Eff150mA { get; set; }

        public string Lin200mA { get; set; }
        public string Vout200mA { get; set; }
        public string Fre200mA { get; set; }
        public string Eff200mA { get; set; }


        public string Lin300mA { get; set; }
        public string Vout300mA { get; set; }
        public string Fre300mA { get; set; }
        public string Eff300mA { get; set; }

        public string Lin400mA { get; set; }
        public string Vout400mA { get; set; }
        public string Fre400mA { get; set; }
        public string Eff400mA { get; set; }

        public string Lin500mA { get; set; }
        public string Vout500mA { get; set; }
        public string Fre500mA { get; set; }
        public string Eff500mA { get; set; }

        public string Lin600mA { get; set; }
        public string Vout600mA { get; set; }
        public string Fre600mA { get; set; }
        public string Eff600mA { get; set; }

        public string Lin1000mA { get; set; }
        public string Vout1000mA { get; set; }
        public string Fre1000mA { get; set; }
        public string Eff1000mA { get; set; }

    }




    class Data
    {
        public ImeiPage imeiPage { get; set; }
    }

    class Data_SN
    {

        public string bindPcba { get; set; }
        public Sn_Content SN { get; set; }
        public Plan plan { get; set; }//新增
        public RspEnterfix responseEnterFix { get; set; }//新增
    }
    class RspEnterfix
    {
        public string sn { get; set; }
        public string pcba { get; set; }
        public string product { get; set; }
        public string planCode { get; set; }
        public string processScheme { get; set; }
        public string undesirableProcess { get; set; }
        public string undesirablePhenomena { get; set; }
    }


    class ImeiPage
    {
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
        public int size { get; set; }
        public string numberOfElements { get; set; }
        public bool first { get; set; }
        public bool last { get; set; }
        public List<Content> content { get; set; }
        public List<Sort> sort { get; set; }
    }

    class Sn_Content
    {
        public int id { get; set; }
        public string sn { get; set; }
        public string bind { get; set; }
        //public string ageingNum { get; set; }
        public string pcba { get; set; }

        public int status { get; set; }

        public string statusDescription { get; set; }
        public Plan plan { get; set; }
        public Batch batch { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }
    }
    class Batch
    {
        public int id { get; set; }
        public string number { get; set; }
        public int quantity { get; set; }
        public int status { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }
    }

    class Content
    {
        public int id { get; set; }
        public string imei { get; set; }
        public string sn { get; set; }
        public int print { get; set; }
        public int status { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }
    }

    class Sort
    {
        public string direction { get; set; }
        public string property { get; set; }
        public bool ignoreCase { get; set; }
        public string nullHandling { get; set; }
        public bool descending { get; set; }
        public bool ascending { get; set; }
    }


    //class ResponseInfo_PlanCode
    //{
    //    public string code { get; set; }
    //    public string message { get; set; }
    //    public PlanCode data { get; set; }
    //}

    //[Serializable]

    //class PlanCode
    //{
    //    public Plan plan { get; set; }
    //}
    class Plan//新增
    {
        public int id { get; set; }
        public string code { get; set; }
        public Product product { get; set; }
        public ProcessScheme processScheme { get; set; }

        public string start { get; set; }
        public string end { get; set; }
        public string quantity { get; set; }

    }

    class Product//新增
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }
    }
    class ProcessScheme//新增
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }

    }


    #endregion



    #region 文件上传

    class httpFileUpload
    {
        /// <summary>  
        /// 使用Post方法获取字符串结果  
        /// </summary>  
        /// <param name="url"></param>  
        /// <param name="formItems">Post表单内容</param>  
        /// <param name="cookieContainer"></param>  
        /// <param name="timeOut">默认20秒</param>  
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>  
        /// <returns></returns>  
        public static ResponseInfo PostForm(string url, List<FormDATA> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        {
            ResponseInfo responseInfo = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符  
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流  
            var postStream = new MemoryStream();


            #region 处理Form表单请求内容
            //是否用Form上传文件  
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板  
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板  
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件  
                        formdata = string.Format(fileFormdataTemplate, item.Key, item.FileName);
                    }
                    else
                    {
                        //上传文本  
                        //formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                        #region
                        if (item.Value is long)
                        {
                            formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                            //Type a
                        }
                        else if (item.Value is string)
                        {
                            //上传文本  
                            formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                        }
                        #endregion
                    }

                    //统一处理  
                    byte[] formdataBytes = null;
                    //第一行不需要换行  
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容  
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        using (var stream = item.FileContent)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流
            if (postStream != null)
            {
                try
                {
                    postStream.Position = 0;
                    //直接写入流  
                    Stream requestStream = request.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    ////debug  
                    //postStream.Seek(0, SeekOrigin.Begin);
                    //StreamReader sr = new StreamReader(postStream);
                    //var postStr = sr.ReadToEnd();
                    postStream.Close();//关闭文件访问  
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);

                }
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    responseInfo = JsonConvert.DeserializeObject(retString, typeof(ResponseInfo)) as ResponseInfo;
                    return responseInfo;
                }
            }
        }
        public static ResponseInfo PostForm(string url, List<FormDATA> formItems, out string msg, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 5000)
        {
            ResponseInfo responseInfo = null;
            msg = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符  
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //请求流  
            var postStream = new MemoryStream();


            #region 处理Form表单请求内容
            //是否用Form上传文件  
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板  
                string fileFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板  
                string dataFormdataTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string formdata = null;
                    if (item.IsFile)
                    {
                        //上传文件  
                        formdata = string.Format(fileFormdataTemplate, item.Key, item.FileName);
                    }
                    else
                    {
                        //上传文本  
                        //formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                        #region
                        if (item.Value is long)
                        {
                            formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                            //Type a
                        }
                        else if (item.Value is int)
                        {
                            formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                        }
                        else if (item.Value is string)
                        {
                            //上传文本  
                            formdata = string.Format(dataFormdataTemplate, item.Key, item.Value);
                        }
                        #endregion
                    }

                    //统一处理  
                    byte[] formdataBytes = null;
                    //第一行不需要换行  
                    if (postStream.Length == 0)
                        formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    else
                        formdataBytes = Encoding.UTF8.GetBytes(formdata);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    //写入文件内容  
                    if (item.FileContent != null && item.FileContent.Length > 0)
                    {
                        using (var stream = item.FileContent)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流
            if (postStream != null)
            {
                try
                {
                    postStream.Position = 0;
                    //直接写入流  
                    Stream requestStream = request.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    ////debug  
                    //postStream.Seek(0, SeekOrigin.Begin);
                    //StreamReader sr = new StreamReader(postStream);
                    //var postStr = sr.ReadToEnd();
                    postStream.Close();//关闭文件访问  
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return null ;
                }
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();
                    responseInfo = JsonConvert.DeserializeObject(retString, typeof(ResponseInfo)) as ResponseInfo;
                    return responseInfo;
                }
            }
        }
        /// <summary>  
        /// 表单数据项  
        /// </summary>  
        public class FormDATA
        {

            public string Key { set; get; }
            /// <summary>  
            /// 表单值,上传文件时忽略，request["key"].value  
            /// </summary>  
            public object Value { set; get; }

            /// <summary>  
            /// sn
            /// </summary>  
            //public string snKey { set; get; }
            //public string snValue { set; get; }
            ///// <summary>  
            ///// pcba号
            ///// </summary>  
            //public string pcbaKey { set; get; }
            //public string pcbaValue { set; get; }
            ///// <summary>
            ///// 计划单
            ///// </summary>
            //public string planCodeKey { set; get; }
            //public string planCodeValue { set; get; }
            ///// <summary>
            ///// 工序
            ///// </summary>
            //public string processKey { set; get; }
            //public string processValue { set; get; }
            ///// <summary>
            ///// 工位
            ///// </summary>
            //public string stationKey { set; get; }
            //public string stationValue { set; get; }
            ///// <summary>
            ///// 测试状态
            ///// </summary>
            //public string statusKey { set; get; }
            //public int statusValue { set; get; }
            ///// <summary>
            ///// 测试参数
            ///// </summary>
            //public string testDataKey { set; get; }
            //public string testDataValue { set; get; }
            ///// <summary>
            ///// 测试日志
            ///// </summary>
            //public string logKey { set; get; }
            //public string logValue { set; get; }
            /// <summary>  
            /// 是否是文件  
            /// </summary>  
            public bool IsFile
            {
                get
                {
                    if (FileContent == null || FileContent.Length == 0)
                        return false;

                    if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                        throw new Exception("上传文件时 FileName 属性值不能为空");
                    return true;
                }
            }

            public string FileName { set; get; }
            /// <summary>  
            /// 上传的文件内容  
            /// </summary>  
            public Stream FileContent { set; get; }
        }
    }
    #endregion

}
