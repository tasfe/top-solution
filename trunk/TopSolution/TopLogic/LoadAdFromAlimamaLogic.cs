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

                List<TopItem> topItems = GetTopItemsFromPage(htmlData, item);
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

        /// <summary>
        /// 从html字符串中提取出前十名的广告信息
        /// </summary>
        /// <param name="htmlData"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        private List<TopItem> GetTopItemsFromPage(string htmlData, string keywords)
        {
            List<TopItem> result = new List<TopItem>();

            string pattern = AlimamaFetRegManager.GetItemReg();
            MatchCollection matchs = Regex.Matches(htmlData, pattern, RegexOptions.Singleline);
            int count = matchs.Count > 10 ? 10 : matchs.Count;

            for (int i = 0; i < count; i++)
            {
                TopItem temp = new TopItem();

                string itemHtml = matchs[i].Value;
                //TODO 提取属性
                temp.TopItemId = GetTopItemId(itemHtml);
                temp.Title = GetItemTitle(itemHtml);
                temp.Keywords = keywords;
                temp.Nick = GetNick(itemHtml);
                temp.CouponRate = GetItemCouponRate(itemHtml);
                temp.CouponPrice = GetItemCouponPrice(itemHtml);
                temp.CommissionRate = GetItemCommissionRate(itemHtml);
                temp.Commission = GetItemCommission(itemHtml);
                temp.CommissionNum = GetItemCommissionNum(itemHtml);
                temp.CommissionVolume = GetItemCommissionVolume(itemHtml);
                result.Add(temp);
            }

            return result;
        }

        /// <summary>
        /// 从单条记录中获取项目ID
        /// </summary>
        /// <param name="itemHtml">单条记录html</param>
        /// <returns></returns>
        private string GetTopItemId(string itemHtml)
        {
            string pattern = AlimamaFetRegManager.GetItemIdReg();
            return GetValueByConfigReg(itemHtml, pattern);
        }

        /// <summary>
        /// 获取记录标题
        /// </summary>
        /// <param name="itemHtml">单条记录html</param>
        /// <returns></returns>
        private string GetItemTitle(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemTitleReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 提取淘宝客昵称
        /// </summary>
        /// <param name="itemHtml">要从中提取的原始字符串</param>
        /// <returns></returns>
        private string GetNick(string itemHtml)
        {
            string pattern = AlimamaFetRegManager.GetItemNickReg();
            return GetValueByConfigReg(itemHtml, pattern);
        }

        /// <summary>
        /// 获取折扣比率CouponRate
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCouponRate(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCouponRateReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCouponPrice(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCouponPriceReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 获取折扣比率CouponRate
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCommissionRate(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCommissionRateReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 获取折扣比率CouponRate
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCommission(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCommissionReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 获取折扣比率CouponRate
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCommissionNum(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCommissionNumReg();
            return GetValueByConfigReg(itemHtml, reg);
        }

        /// <summary>
        /// 获取折扣比率CouponRate
        /// </summary>
        /// <param name="itemHtml"></param>
        /// <returns></returns>
        private string GetItemCommissionVolume(string itemHtml)
        {
            string reg = AlimamaFetRegManager.GetItemCommissionVolumeReg();
            return GetValueByConfigReg(itemHtml, reg);
        }
        
        /// <summary>
        /// 根据配置的获取方式获取网页内容
        /// </summary>
        /// <param name="ConfigReg">配置的规则</param>
        /// <param name="oriStr">原始html</param>
        /// <returns></returns>
        private string GetValueByConfigReg(string itemHtml, string ConfigReg)
        {
            if (ConfigReg.ToUpper().StartsWith("XMLNODE|"))
            {
                return XMLTOOL.GetHtmlTextByXmlPath(itemHtml, ConfigReg.Substring(8));
            }
            else
            {
                return TopUtility.GetStringByReg(itemHtml, ConfigReg);
            }
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

    /// <summary>
    /// 获取广告配置文件管理类
    /// </summary>
    class AlimamaFetRegManager
    {
       static readonly string ConfigFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Configurations/AlimamaGetReg.Config");

        /// <summary>
        /// 单条内容正则表达式
        /// </summary>
        private static string _ItemReg = null;

        /// <summary>
        /// 获取单条内容正则表达式
        /// </summary>
        /// <returns></returns>
        public static string GetItemReg()
        {
            if (_ItemReg == null)
            {
                _ItemReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemReg");
            }
            return _ItemReg;
        }

        private static string _ItemIdReg = null;

        /// <summary>
        /// 获取Id
        /// </summary>
        /// <returns></returns>
        public static string GetItemIdReg()
        {
            if (_ItemIdReg == null)
            {
                _ItemIdReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemIdReg");
            }
            return _ItemIdReg;
        }

        private static string _ItemTitleReg = null;

        /// <summary>
        /// 获取标题正则
        /// </summary>
        /// <returns></returns>
        public static string GetItemTitleReg()
        {
            if (_ItemTitleReg == null)
            {
                _ItemTitleReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemTitleReg");
            }
            return _ItemTitleReg;
        }

        private static string _ItemNickReg = null;

        /// <summary>
        /// 获取卖家昵称的正则
        /// </summary>
        /// <returns></returns>
        public static string GetItemNickReg()
        {
            if (_ItemNickReg == null)
            {
                _ItemNickReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemNickReg");
            }
            return _ItemNickReg;
        }

        private static string _ItemCouponRateReg = null;
        /// <summary>
        /// 获取折扣比率配置规则
        /// </summary>
        /// <returns></returns>
        public static string GetItemCouponRateReg()
        {
            if (_ItemCouponRateReg == null)
            {
                _ItemCouponRateReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemCouponRateReg");
            }
            return _ItemCouponRateReg;
        }

        private static string _ItemCouponPriceReg = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetItemCouponPriceReg()
        {
            if (_ItemCouponRateReg == null)
            {
                _ItemCouponPriceReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemCouponPriceReg");
            }
            return _ItemCouponRateReg;
        }

        private static string ItemCommissionRateReg = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetItemCommissionRateReg()
        {
            if (_ItemCouponRateReg == null)
            {
                ItemCommissionRateReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "_ItemCommissionRateReg");
            }
            return _ItemCouponRateReg;
        }

        private static string _ItemCommissionReg = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetItemCommissionReg()
        {
            if (_ItemCouponRateReg == null)
            {
                _ItemCommissionReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemCommissionReg");
            }
            return _ItemCouponRateReg;
        }

        private static string _ItemCommissionNumReg = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetItemCommissionNumReg()
        {
            if (_ItemCouponRateReg == null)
            {
                _ItemCommissionNumReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemCommissionNumReg");
            }
            return _ItemCouponRateReg;
        }

        private static string _ItemCommissionVolumeReg = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetItemCommissionVolumeReg()
        {
            if (_ItemCouponRateReg == null)
            {
                _ItemCommissionVolumeReg = XMLTOOL.GetFirstNodeValue(ConfigFilePath, "ItemCommissionVolumeReg");
            }
            return _ItemCouponRateReg;
        }
    }
}
