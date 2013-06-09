using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GetTopItemEntity;
using TopUtilityTool;

namespace GetTopItemLogic
{
    public class TopGetTopItemLogic
    {
        WebBrowser webBrowser = null;

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


            //03.转到获取界面

            //04.勾选获取

            //05.点击下载
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
                    //doc.GetElementById("J_submit").InvokeMember("click");
                    doc.GetElementById("login_form").InvokeMember("submit");
                    break;
                case GetTopItemEntity.GetTopItemUrls.UserDefaultUrl:
                    webBrowser.Navigate(GetTopItemUrls.UserSelectTaoBaoKeUrl);
                    break;
                case GetTopItemUrls.UserSelectTaoBaoKeUrl:
                    // 填写关键字
                    doc = this.webBrowser.Document;
                    doc.GetElementById("q").SetAttribute("value", "美白");
                    doc.GetElementById("J_searchForm").InvokeMember("submit");

                    break;
            }
        }
    }
}
