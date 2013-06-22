/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/2/6 6:37:53
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.TA;

namespace WebSharing.DB4ODAL
{
    public class DB4ODALServerHelper
    {
        /// <summary>
        /// 获取数据服务客户端。
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public static DB4ODALClient GetIDALClient(string connectionString)
        {
            string realConnString = connectionString.ToLower().Replace(" ", "");
            string[] strs = realConnString.Trim().Split(';');
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var item in strs)
            {
                string[] temp = item.Split('=');
                if (temp.Length == 2)
                {
                    dic.Add(temp[0].Trim(), temp[1].Trim());
                }
            }

            if (dic["datasource"] == "local" || dic["datasource"] == "." || dic["datasource"] == "127.0.0.1")
            {
                dic["datasource"] = "local";
            }

            bool isLocal = dic["datasource"] == "local" || dic["port"] == "0";

            if (isLocal)
            {
                return DB4OLocalServerHelper.GetInstance(dic["dbpath"]).GetDB4ODALClient();
            }
            else
            {
                return DB4ORemoteServerHelper.GetDB4ODALClient(dic["host"], int.Parse(dic["port"]), dic["username"], dic["password"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupFileName"></param>
        public static void BackupDb(string connectionString,string backupFileName)
        {
            string realConnString = connectionString.ToLower().Replace(" ", "");
            string[] strs = realConnString.Trim().Split(';');
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var item in strs)
            {
                string[] temp = item.Split('=');
                if (temp.Length == 2)
                {
                    dic.Add(temp[0].Trim(), temp[1].Trim());
                }
            }

            if (dic["datasource"] == "local" || dic["datasource"] == "." || dic["datasource"] == "127.0.0.1")
            {
                dic["datasource"] = "local";
            }

            bool isLocal = dic["datasource"] == "local" || dic["port"] == "0";

            if (isLocal)
            {
                DB4OLocalServerHelper.GetInstance(dic["dbpath"]).BackUp(backupFileName);
            }
            else
            {
                
            }
        }
    }

    class DB4OLocalServerHelper
    {
        static readonly object syncHelper = new object();

        private DB4OLocalServerHelper()
        {

        }

        private string dbpath = string.Empty;

        private static DB4OLocalServerHelper _instance = null;

        public static DB4OLocalServerHelper GetInstance(string dbPath)
        {
            if (_instance == null)
            {
                _instance = new DB4OLocalServerHelper();
                _instance.dbpath = System.Web.HttpContext.Current.Server.MapPath(dbPath);
            }
            return _instance;
        }

        private IObjectServer _IObjectServer = null;

        private IObjectServer IObjectserver
        {
            get
            {
                if (_IObjectServer == null)
                {
                    lock (syncHelper)
                    {
                        if (_IObjectServer == null)
                        {
                            IServerConfiguration config = Db4oClientServer.NewServerConfiguration();
                            config.Common.Add(new TransparentPersistenceSupport());
                            config.Common.Add(new TransparentActivationSupport());
                            _IObjectServer = Db4oClientServer.OpenServer(config, dbpath, 0);                            
                        }
                    }
                }
                return _IObjectServer;
            }
        }

        public DB4ODALClient GetDB4ODALClient()
        {
            IObjectContainer o = this.IObjectserver.OpenClient();
            DB4ODALClient client = new DB4ODALClient() { IObjectContainer = o };
            return client;
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path"></param>
        public void BackUp(string path)
        {
            this.IObjectserver.Ext().Backup(path);
        }
    }

    class DB4ORemoteServerHelper
    {
        public static DB4ODALClient GetDB4ODALClient(string host, int port, string userName, string password)
        {
            try
            {
                IObjectContainer o = Db4oClientServer.OpenClient(host, port, userName, password);
                if (o != null)
                {
                    return new DB4ODALClient() { IObjectContainer = o };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
