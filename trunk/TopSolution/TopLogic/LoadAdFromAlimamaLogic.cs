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
                loadData = TopHttpWebRequest.GetHtmlData(string.Format(GetTopItemUrls.MerchandisePromotionPageFormat, System.Web.HttpContext.Current.Server.UrlEncode(item)), cookieContainer);
                if ((bool)loadData[0] == false)
                {
                    continue;
                }

                string htmlData = loadData[2].ToString();
                
                List<TopItem> topItems = GetTopItemsFromPage(htmlData);
                string itemIds = "";
                string excelUrl = string.Format(GetTopItemUrls.ExcelUrlFormat, itemIds);
                // 02.下载Excel文件
                

                bool loadResult = TopHttpWebRequest.DowloadCheckImg(excelUrl, cookieContainer, excelpath);

                if (loadResult == false)
                {
                    continue;
                }
                // 03.处理信息存入数据库
                ProcessExcel();
            }
        }

        private List<TopItem> GetTopItemsFromPage(string htmlData)
        {
            List<TopItem> result = new List<TopItem>();

            string pattern = "(?<=<tr zhekou=\".*\" zhekoujia=\".*\">).*?(?=</tr>)";
            MatchCollection matchs = Regex.Matches(htmlData, pattern, RegexOptions.Singleline);
            int count = matchs.Count > 10 ? 10 : matchs.Count;

            for (int i = 0; i < count; i++)
            {
                TopItem temp = new TopItem();

                //TODO 提取属性
                //temp.Title = currenttr.Children[1].Children[0].Children[currenttr.Children[1].Children[0].Children.Count - 3].InnerText;
                //temp.Keywords = curKeyword.Keywords;
                //temp.Nick = GetNick(currenttr.Children[1].Children[0].Children[currenttr.Children[1].Children[0].Children.Count - 2].InnerText);
                //temp.CouponRate = currenttr.Children[2].InnerText;
                //temp.CouponPrice = currenttr.Children[3].InnerText;
                //temp.CommissionRate = currenttr.Children[4].InnerText;
                //temp.Commission = currenttr.Children[5].Children[0].InnerText;
                //temp.CommissionNum = currenttr.Children[6].InnerText;
                //temp.CommissionVolume = currenttr.Children[7].InnerText;
                result.Add(temp);
            }

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

        /// <summary>
        /// 处理Excel
        /// </summary>
        private void ProcessExcel()
        {
            ////01.读取Excel数据
            //ExcelToolWithCom tool = new ExcelToolWithCom();
            //DataTable dataTableFromExcel = tool.GetExcelData(excelpath);
            ////02.获取网页数据
            //List<TopItem> items = this.GetTopItemsFromPage();
            ////03.使用Excel数据填充属性
            //foreach (var item in items)
            //{
            //    DataRow dr = dataTableFromExcel.AsEnumerable().Where(p => p["宝贝标题"].ToString() == item.Title).FirstOrDefault();
            //    if (dr != null)
            //    {
            //        item.PicUrl = dr["主图片"].ToString();
            //        item.ClickUrl = dr["单品链接"].ToString();
            //        item.ShopClickUrl = dr["店铺链接"].ToString();
            //    }
            //}
            ////04.调用wcf先删除当前关键字的记录，再将新的保存到数据库

            //ITopItemService service = new TopItemServiceClient();
            //service.DeleteTopItems(curKeyword.Keywords);
            //service.SaveTopItemList(items);

            ////04.删除Excel
            //if (File.Exists(excelpath))
            //{
            //    File.Delete(excelpath);
            //}
        }
    }
}
