/*********************************************************
 * copyright xinbohit.com 版权所有 
 * 开发人员：IvanYu
 * 创建时间：2014/1/14 14:35:44
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.Common;
using NLog;

namespace TopUtilityTool
{
    public static class Loger
    {
        /// <summary>
        /// 享日志中写入异常信息,写入文件err.log
        /// </summary>
        /// <param name="ex"></param>
        public static void LogErr(Exception ex)
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            Logger logger = LogManager.GetLogger("error");
            logger.ErrorException(ex.Message, ex);
        }

        /// <summary>
        /// 向日志中写入一般信息，写入文件info.log
        /// </summary>
        /// <param name="info"></param>
        public static void LogInfo(string info)
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            Logger logger = LogManager.GetLogger("info");
            logger.Info(info);
        }

        /// <summary>
        /// 向日志中写入调试信息，写入文件debug.log
        /// </summary>
        /// <param name="debug"></param>
        public static void LogDebug(string debug)
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            Logger logger = LogManager.GetLogger("debug");
            logger.Debug(debug);
        }
    }
}
