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
            ExcelToolWithCom tool = new ExcelToolWithCom();
            DataTable dataTableFromExcel = tool.GetExcelData(excelpath);
            //02.获取网页数据
            List<TopItem> items = this.GetTopItemsFromPage();

            //02.实例化需要保存的TopItem，使用Excel数据和网页数据填充属性

            //03.调用wcf先删除当前关键字的记录，再将新的保存到数据库

            //04.删除Excel
            File.Delete(excelpath);
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
                HtmlElement currenttr = table.Children[1].Children[i];
                if (currenttr.GetAttribute("class") != "downlist")
                {
                    TopItem temp = new TopItem();

                    //TODO 提取属性
                    temp.Title = currenttr.Children[1].Children[0].Children[2].InnerText;
                    temp.Keywords = "";
                    temp.Nick = currenttr.Children[1].Children[0].Children[3].InnerText;
                }
            }

            return result;
        }
        /*
                                         <td>
                                    <input name="linkexport" type="checkbox" value="16219426144">
                                </td>
                                <td align="left">
                                    <div class="list-info">
                                                                                <span class="count-label red">[买2送粉扑]</span>
                                                                                <a class="pic" href="http://item.taobao.com:80/item.htm?id=16219426144" target="_blank" lzlinkno="130"><img onerror="this.src='http://img02.taobaocdn.com/tps/i2/T1eUdPXXJCXXXXXXXX-80-80.gif'" src="http://img01.taobaocdn.com/bao/uploaded/i1/15540021148502571/T1Mf01XBVbXXXXXXXX_!!0-item_pic.jpg_sum.jpg"></a>
                                        <a href="http://item.taobao.com:80/item.htm?id=16219426144" target="_blank" lzlinkno="131">水尚全效bb霜 男女士<span>美白</span>裸妆 遮瑕隔离 粉底保湿强 正品买二包邮</a>
                                        <p class="shopkeeper">
											掌柜：水尚海蓝专卖店
											<span class="ww-light ww-small" data-icon="small" data-item="2251644254" data-display="inline" data-nick="水尚海蓝专卖店" data-encode="true"><a title="点此可以直接和卖家交流选好的宝贝，或相互交流网购体验，还支持语音视频噢。" class="ww-inline ww-online" href="http://www.taobao.com/webww/?ver=1&amp;&amp;touid=cntaobao%E6%B0%B4%E5%B0%9A%E6%B5%B7%E8%93%9D%E4%B8%93%E5%8D%96%E5%BA%97&amp;siteid=cntaobao&amp;status=2&amp;portalId=&amp;gid=2251644254&amp;itemsId=" target="_blank"><span>旺旺在线</span></a></span>
										</p>
                                                                                <p><a class="blue-link shop-detail" href=" http://shop100457962.taobao.alimama.com " target="_blank" lzlinkno="132" oid="1057775540" commentcount="97489" biz30day="33342">店铺推广详情&gt;&gt;</a></p>
                                    </div>
                                </td>
                                <td align="right">
                                                                            1.2折
                                                                    </td>
                                <td align="right">9.98元</td>
                                <td align="right">10.0%</td>
                                <td align="right"><span class="ok">1.0元</span></td>
                                <td align="right">11283件</td>
                                <td align="right">10871.11元</td>
                                <td align="center">
                                    <p class="use-now">
                                        <a class="btn btn-blue get-code" href="#" target="_blank" lzlinkno="133" auctionid="16219426144">立即推广</a>
                                        <br>
                                                                                <a class="more-commission" href="javascript:void(0);" lzlinkno="134" did="1057775540">获取更高佣金∧</a>
                                                                            </p>
                                </td>
         */
    }
}
