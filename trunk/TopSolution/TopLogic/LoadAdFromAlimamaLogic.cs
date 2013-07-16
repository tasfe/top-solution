using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopEntity;
using TopUtilityTool;
using System.Net;
using GetTopItemEntity;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

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
            ArrayList loadData = TopHttpWebRequest.GetHtmlData(GetTopItemUrls.HomeUrl, cookieContainer);

            if ((bool)loadData[0] == false)
            {
                return;
            }

            // 02.传递登陆数据
            loadData = TopHttpWebRequest.PostData(GetTopItemUrls.PostInfo, GetTopItemUrls.PostToUrl, cookieContainer);
            if ((bool)loadData[0] == false)
            {
                return;
            }

            // 03.循环访问关键词页面
            foreach (var item in keywords)
            {
                // 00.删除指定名称的Excel文件   
                if (File.Exists(excelpath))
                {
                    File.Delete(excelpath);
                }

                // 01.抓取前10名的ID，拼接Excel地址
                loadData=TopHttpWebRequest.GetHtmlData(string.Format(GetTopItemUrls.MerchandisePromotionPageFormat,System.Web.HttpContext.Current.Server.UrlEncode(item)),cookieContainer);
                if ((bool)loadData[0] == false)
                {
                    return;
                }

                string htmlData = loadData[2].ToString();

                // 02.下载Excel文件

                // 03.处理信息存入数据库

            }
        }

        private List<TopItem> GetTopItemsFromPage(string htmlData)
        {
            List<TopItem> result = new List<TopItem>();

            string pattern = "(?<=掌柜：).*?(?= )";
            MatchCollection matchs = Regex.Matches(htmlData, pattern, RegexOptions.Singleline);


            return result;
        }

        /// <summary>
        /// 提取淘宝客昵称
        /// </summary>
        /// <param name="oriStr">要从中提取的原始字符串</param>
        /// <returns></returns>
        private string GetNick(string oriStr)
        {
            string pattern = "(?<=掌柜：).*?(?= )";
            return Regex.Match(oriStr, pattern).Value;
        }
    }
}
