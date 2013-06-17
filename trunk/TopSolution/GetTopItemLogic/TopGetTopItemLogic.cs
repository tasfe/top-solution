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

namespace GetTopItemLogic
{
    public class TopGetTopItemLogic
    {
        private int step = 0;
        private int savestep = 0;
        private string excelpath = System.Configuration.ConfigurationManager.AppSettings["excelpath"];

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
            //01.加载阿里妈妈
            this.webBrowser.Navigate(GetTopItemUrls.HomeUrl);
            //02.登录
            this.webBrowser.Navigate(GetTopItemUrls.LoginUrl);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += new EventHandler(timer_Tick);

            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000;
            timer2.Tick += new EventHandler(timer2_Tick);

            //03.转到获取界面

            //04.勾选获取

            //05.点击下载
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
                    break;
                case 4:
                    timer.Stop();
                    savestep = 0;
                    SendKeys.SendWait("{Enter}");
                    SendKeys.Flush();
                    // 处理导出的Excel，提取数据，将整合好的数据存库

                    // 转到检索页面开始下一次导出
                    webBrowser.Navigate(GetTopItemUrls.UserSelectTaoBaoKeUrl);
                    break;
            }

        }

        void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            SendKeys.SendWait("{Enter}");
            SendKeys.Flush();
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
                    break;
                case GetTopItemUrls.UserSelectTaoBaoKeUrl:
                    // 填写关键字
                    doc = this.webBrowser.Document;
                    doc.GetElementById("q").SetAttribute("value", "美白");
                    doc.GetElementById("J_searchForm").InvokeMember("submit");
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
            DataTable dataTable = null;
            using (Stream stream = File.Open(excelpath, FileMode.Open))
            {
                ExcelTool tool = new ExcelTool();
                dataTable = tool.ReadExcel("table1", stream);
            }
            //02.实例化需要保存的TopItem，使用Excel数据和网页数据填充属性

            //03.调用wcf先删除当前关键字的记录，再将新的保存到数据库

            //04.删除Excel

        }
    }
}
