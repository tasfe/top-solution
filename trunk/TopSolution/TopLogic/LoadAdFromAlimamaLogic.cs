using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopEntity;
using TopUtilityTool;
using System.Net;
using GetTopItemEntity;

namespace TopLogic
{
    public class LoadAdFromAlimamaLogic : LogicBase<TopItem>
    {
        private CookieContainer cookieContainer = null;
        private string excelpath = System.Configuration.ConfigurationManager.AppSettings["excelpath"];

        public LoadAdFromAlimamaLogic()
        {
            cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// 获取指定关键字的广告并存入数据库
        /// </summary>
        /// <param name="keywords"></param>
        public void LoadAdToDb(List<string> keywords)
        {
            // 01.访问首页
            TopHttpWebRequest.GetHtmlData(GetTopItemUrls.HomeUrl, cookieContainer);
            // 02.传递登陆数据
            TopHttpWebRequest.PostData(GetTopItemUrls.PostInfo, GetTopItemUrls.PostToUrl, cookieContainer);
            // 03.循环访问关键词页面
            foreach (var item in keywords)
            {
                // 00.删除指定名称的Excel文件   

                // 01.抓取前10名的ID，拼接Excel地址

                // 02.下载Excel文件

                // 03.处理信息存入数据库

            }
        }
    }
}
