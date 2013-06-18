using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GetTopItemEntity;
using TopUtilityTool;
using System.Net;
using System.IO;
using System.Threading;
using System.Data;
using TopEntity;
using System.Text.RegularExpressions;
using ITopItemService = GetTopItemLogic.WcfTopItemService.ITopItemService;
using TopItemServiceClient = GetTopItemLogic.WcfTopItemService.TopItemServiceClient;

namespace GetTopItemLogic
{
    public class TopGetTopItemLogic
    {
        private int step = 0;
        private int savestep = 0;
        private string excelpath = System.Configuration.ConfigurationManager.AppSettings["excelpath"];
        private string curKeyword = null;
        private Queue<string> allKeywords;

        WebBrowser webBrowser = null;
        System.Windows.Forms.Timer timer = null;
        System.Windows.Forms.Timer timer2 = null;

        public TopGetTopItemLogic(WebBrowser browser)
        {
            this.webBrowser = browser;
            this.webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
        }

        public void BeginLoad()
        {
            InilizeTimer();
            InilizeKeywordsQueue();

            //01.加载阿里妈妈
            this.webBrowser.Navigate(GetTopItemUrls.HomeUrl);
            //02.登录
            this.webBrowser.Navigate(GetTopItemUrls.LoginUrl);
        }

        /// <summary>
        /// 初始化Timer
        /// </summary>
        private void InilizeTimer()
        {

            //启动自动模拟操作
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
            }
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);

            // 启动24小时执行一次Timer
            if (timer2 == null)
            {
                timer2 = new System.Windows.Forms.Timer();
            }
            timer2.Interval = 24 * 60 * 60 * 1000;
            timer2.Tick -= new EventHandler(timer2_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
        }

        /// <summary>
        /// 加载关键词队列
        /// </summary>
        private void InilizeKeywordsQueue()
        {
            ITopItemService service = new TopItemServiceClient();
            allKeywords = new Queue<string>(service.GetAllKeywords());
        }

        /// <summary>
        /// 自动下载Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            switch (savestep)
            {
                case 0:
                    timer.Stop();
                    savestep++;
                    SendKeys.SendWait("{Left}");
                    SendKeys.Flush();
                    timer.Start();
                    break;
                case 1:
                    timer.Stop();
                    savestep++;
                    SendKeys.SendWait("{Enter}");
                    SendKeys.Flush();
                    timer.Start();
                    break;
                case 2:
                    timer.Stop();
                    savestep++;
                    //输入路径
                    string path = excelpath;
                    Clipboard.SetDataObject(path);
                    SendKeys.SendWait("^v");
                    SendKeys.Flush();
                    timer.Start();
                    break;
                case 3:
                    timer.Stop();
                    savestep++;
                    SendKeys.SendWait("{Enter}");
                    SendKeys.Flush();
                    timer.Start();
                    break;
                case 4:
                    timer.Stop();
                    savestep++;
                    SendKeys.SendWait("{Enter}");
                    SendKeys.Flush();
                    timer.Start();
                    break;
                case 5:
                    timer.Stop();
                    savestep = 0;
                    // 处理导出的Excel，提取数据，将整合好的数据存库
                    ProcessExcel();
                    // 转到检索页面开始下一次导出
                    webBrowser.Navigate(GetTopItemUrls.UserSelectTaoBaoKeUrl);
                    break;
            }

        }

        /// <summary>
        /// 24小时执行一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            BeginLoad();
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = null;
            switch (e.Url.ToString())
            {
                case GetTopItemUrls.LoginUrl:
                    doc = this.webBrowser.Document;
                    doc.GetElementById("J_logname").SetAttribute("value", "yuweiyuan2004@163.com");
                    doc.GetElementById("J_logpassword").SetAttribute("value", "yuweiyuan518");
                    doc.GetElementById("J_md5logpasswd").SetAttribute("value", TopSecurity.MD5Encoding("yuweiyuan518"));
                    doc.GetElementById("login_form").InvokeMember("submit");
                    break;
                case GetTopItemEntity.GetTopItemUrls.UserDefaultUrl:
                    webBrowser.Navigate(GetTopItemUrls.UserSelectTaoBaoKeUrl);
                    step = 0;
                    if (File.Exists(excelpath))
                    {
                        File.Delete(excelpath);
                    }
                    break;
                case GetTopItemUrls.UserSelectTaoBaoKeUrl:
                    // 填写关键字
                    if (allKeywords != null && allKeywords.Count > 0)
                    {
                        curKeyword = allKeywords.Dequeue();

                        doc = this.webBrowser.Document;
                        doc.GetElementById("q").SetAttribute("value", curKeyword);
                        doc.GetElementById("J_searchForm").InvokeMember("submit");
                    }
                    else
                    {
                        //启动timer2
                        timer2.Start();
                    }
                    break;
                default:
                    if (e.Url.AbsoluteUri.StartsWith(GetTopItemUrls.MerchandisePromotion))
                    {
                        switch (step)
                        {
                            case 0:
                                //排序
                                step = 1;
                                doc = this.webBrowser.Document;
                                var htmlElements = doc.GetElementsByTagName("span");
                                HtmlElement col = (from HtmlElement d in htmlElements where d.GetAttribute("cat") == "totalnum" select d).FirstOrDefault();
                                if (col != null)
                                {
                                    col.InvokeMember("click");
                                }
                                break;
                            case 1:
                                //勾选全部
                                step = 0;
                                doc = this.webBrowser.Document;
                                doc.GetElementById("J_checkAll").InvokeMember("click");
                                //点击下载
                                webBrowser.FileDownload += new EventHandler(webBrowser_FileDownload);
                                doc.GetElementById("J_exportlink_btn").InvokeMember("click");
                                timer.Start();
                                break;
                        }
                    }

                    break;
            }
        }

        void webBrowser_FileDownload(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 处理Excel
        /// </summary>
        private void ProcessExcel()
        {
            //01.读取Excel数据
            ExcelToolWithCom tool = new ExcelToolWithCom();
            DataTable dataTableFromExcel = tool.GetExcelData(excelpath);
            //02.获取网页数据
            List<TopItem> items = this.GetTopItemsFromPage();
            //03.使用Excel数据填充属性
            foreach (var item in items)
            {
                DataRow dr = dataTableFromExcel.AsEnumerable().Where(p => p["宝贝标题"].ToString() == item.Title).FirstOrDefault();
                if (dr != null)
                {
                    item.PicUrl = dr["主图片"].ToString();
                    item.ClickUrl = dr["单品链接"].ToString();
                    item.ShopClickUrl = dr["店铺链接"].ToString();
                }
            }
            //04.调用wcf先删除当前关键字的记录，再将新的保存到数据库

            ITopItemService service = new TopItemServiceClient();
            service.DeleteTopItems(curKeyword);
            service.SaveTopItemList(items);

            //04.删除Excel
            if (File.Exists(excelpath))
            {
                File.Delete(excelpath);
            }
        }

        /// <summary>
        /// 从页面DOM中提取前10个广告项目
        /// </summary>
        /// <returns></returns>
        private List<TopItem> GetTopItemsFromPage()
        {
            List<TopItem> result = new List<TopItem>();

            HtmlDocument doc = this.webBrowser.Document;
            HtmlElement table = doc.GetElementById("J_listMainTable");
            HtmlElement tbody = table.Children[1];
            for (int i = 0; i < tbody.Children.Count; i++)
            {
                if (result.Count == 10)
                {
                    break;
                }
                HtmlElement currenttr = table.Children[1].Children[i];
                if (currenttr.GetAttribute("class") != "downlist")
                {
                    try
                    {
                        TopItem temp = new TopItem();

                        //TODO 提取属性
                        temp.Title = currenttr.Children[1].Children[0].Children[currenttr.Children[1].Children[0].Children.Count - 3].InnerText;
                        temp.Keywords = curKeyword;
                        temp.Nick = GetNick(currenttr.Children[1].Children[0].Children[currenttr.Children[1].Children[0].Children.Count - 2].InnerText);
                        temp.CouponRate = currenttr.Children[2].InnerText;
                        temp.CouponPrice = currenttr.Children[3].InnerText;
                        temp.CommissionRate = currenttr.Children[4].InnerText;
                        temp.Commission = currenttr.Children[5].Children[0].InnerText;
                        temp.CommissionNum = currenttr.Children[6].InnerText;
                        temp.CommissionVolume = currenttr.Children[7].InnerText;
                        result.Add(temp);
                    }
                    catch
                    {
                        continue;
                    }
                }
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

    }
}
