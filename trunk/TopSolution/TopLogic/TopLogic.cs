/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/6/16 17:41:27
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QQBuySdk;
using Top.Api.Domain;
using Top.Api.Response;
using TopEntity;
using WebSharing.DB4ODAL;
using WebSharing.TopTool;
using System.Xml.Linq;

namespace TopLogic
{
    public class TopLogic : LogicBase<TopItem>
    {
        /// <summary>
        /// 广告数据库管理类
        /// </summary>
        private DB4ODALClient adClient = null;

        public TopLogic()
        {
            adClient = GetDbClient(adConn);
        }

        /// <summary>
        /// 获取淘宝客广告列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<TopItem> GetTopItems(string keyword)
        {
            IEnumerable<TopItem> result = null;

            try
            {
                //配置信息为0表示通过开放平台，否则通过本地库
                string topgetway = System.Configuration.ConfigurationManager.AppSettings["topgetway"];
                if (topgetway == "0")
                {
                    TaobaokeItemsCouponGetResponse response = GetTaobaokeItemsCoupon("site", keyword, 0, "volume_desc");
                    result = MappingTaobaokeItemsToTopItems(response.TaobaokeItems);
                }
                else if (topgetway == "1")
                {
                    result = adClient.GetList<TopItem>(p => p.Keywords == keyword).OrderByDescending(p => p.Volume).Take(10);
                }
                else if (topgetway == "2")
                {
                    result = GetFromQQApi(keyword);
                }
            }
            catch (Exception ex)
            {
                result = new List<TopItem>();
                logger.ErrorException("获取广告信息异常", ex);
            }

            return result;
        }

        public override void Delete(TopItem obj)
        {
            adClient.Delete(obj);
        }

        public override List<TopItem> GetList()
        {
            return adClient.GetList<TopItem>();
        }

        public override List<TopItem> GetList(Predicate<TopItem> p)
        {
            return adClient.GetList(p);
        }

        public override void Save(TopItem obj)
        {
            adClient.Save(obj);
        }

        /// <summary>
        /// 保存抓取到的广告信息，并更新想关关键字的获取时间。
        /// </summary>
        /// <param name="topItems"></param>
        public void Save(List<TopItem> topItems)
        {
            foreach (var item in topItems)
            {
                Save(item);
            }
            var keywords = (from d in topItems select d.Keywords).Distinct();
            using (TopKeywordsLogic logic = new TopKeywordsLogic())
            {
                foreach (var item in keywords)
                {
                    TopKeywords curkeywords = logic.GetList(p => p.Keywords == item).FirstOrDefault();
                    if (curkeywords != null)
                    {
                        curkeywords.LastGetTime = DateTime.Now;
                        logic.Save(curkeywords);
                    }
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            adClient.Dispose();
        }


        #region 私有方法

        /// <summary>
        /// 将淘宝开放平台的API实体映射为系统内部的广告实体
        /// </summary>
        /// <param name="taobaokeItems"></param>
        /// <returns></returns>
        private List<TopItem> MappingTaobaokeItemsToTopItems(List<TaobaokeItem> taobaokeItems)
        {
            List<TopItem> result = new List<TopItem>();

            foreach (var item in taobaokeItems)
            {
                TopItem topItem = new TopItem();

                Type typeTopItem = typeof(TopItem);
                Type typeTaobaokeItem = typeof(TaobaokeItem);
                foreach (var pro in typeTopItem.GetProperties())
                {
                    PropertyInfo proTaobaoke = typeTaobaokeItem.GetProperty(pro.Name);
                    if (proTaobaoke != null)
                    {
                        pro.SetValue(topItem, proTaobaoke.GetValue(item, null), null);
                    }
                }

                result.Add(topItem);
            }

            return result;
        }

        /// <summary>
        /// 获取淘宝客打折信息。
        /// </summary>
        /// <param name="cid">标准商品后台类目id。该ID可以通过taobao.itemcats.get接口获取到。</param>
        /// <param name="keyword">商品标题中包含的关键字. 注意:查询时keyword,cid至少选择其中一个参数</param>
        /// <param name="nick"> 推广者的淘宝会员昵称.注:指的是淘宝的会员登录名</param>
        /// <param name="outerCode">自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道</param>
        /// <param name="sort">default(默认排序),  price_desc(折扣价格从高到低),  price_asc(折扣价格从低到高),  credit_desc(信用等级从高到低),  credit_asc(信用等级从低到高),  commissionRate_desc(佣金比率从高到低),  commissionRate_asc(佣金比率从低到高),  volume_desc(成交量成高到低), volume_asc(成交量从低到高)</param>
        /// <returns></returns>
        private TaobaokeItemsCouponGetResponse GetTaobaokeItemsCoupon(string outerCode,
                                                                        string keyword,
                                                                        long? cid,
                                                                        string sort)
        {
            bool isFromCache = false;
            TaobaokeItemsCouponGetResponse result = null;
            try
            {
                string nick = System.Configuration.ConfigurationManager.AppSettings["topusername"];
                string url = System.Configuration.ConfigurationManager.AppSettings["topurl"];
                string appkey = System.Configuration.ConfigurationManager.AppSettings["topappkey"];
                string appsecret = System.Configuration.ConfigurationManager.AppSettings["topappsecret"];

                TopTool tool = new TopTool(url, appkey, appsecret);
                result = tool.TaobaokeItemsCouponGet(nick, outerCode, keyword, cid, sort);
                isFromCache = false;
            }
            catch
            {
                isFromCache = true;

                TaobaokeItemsCouponGetResponse memoryCache = System.Web.HttpContext.Current.Cache.Get(LocalCacheKeys.TaobaokeItemsCouponCache) as TaobaokeItemsCouponGetResponse;
                if (memoryCache != null)
                {
                    result = memoryCache;
                }
                else
                {
                    // 从数据库获取

                }
            }

            if (!isFromCache)
            {
                // Cache
                System.Web.HttpContext.Current.Cache.Insert(LocalCacheKeys.TaobaokeItemsCouponCache, result);
            }

            return result;
        }

        /// <summary>
        /// 从QQ接口获取推广广告数据
        /// </summary>
        /// <returns></returns>
        private List<TopItem> GetFromQQApi(string keyword)
        {
            string strParams =string.Format( "keyWord={0}&userId=58680&orderStyle=89",keyword);

            IDictionary<string, string> txtParam = new Dictionary<string, string>();
            string[] paramArrary = strParams.Split('&');
            foreach (string param in paramArrary)
            {
                string name = param.Substring(0, param.IndexOf('='));
                string value = param.Substring(param.IndexOf('=') + 1);
                txtParam.Add(name, value);
            }

            OpenApiOauth client = new OpenApiOauth("700167258",
                                                    "CnV4NK2VN3DUSNLX",
                                                    long.Parse("1721543717"),
                                                    "e31f72b9b36ad18ce262e3a229a6fb74");
            String result = client.InvokeOpenApi("http://api.paipai.com/cps/cpsCommSearch.xhtml", txtParam, null);

            return GetTopItemsFromQQApiXml(result);
        }

        private List<TopItem> GetTopItemsFromQQApiXml(string strQQApiXml)
        {
            XDocument xdoc = XDocument.Parse(strQQApiXml);
            var nodes = xdoc.Descendants("CpsCommData");

            List<TopItem> result = new List<TopItem>();

            foreach (var item in nodes)
            {
                try
                {
                    TopItem top = new TopItem();
                    top.ClickUrl = item.Element("tagUrl").Value;
                    top.Commission = item.Element("crValue").Value;
                    top.CommissionNum = item.Element("saleNum").Value;
                    top.CommissionRate = item.Element("cvValue").Value;
                    top.CommissionVolume = "0";
                    top.CouponEndTime = "";
                    top.CouponPrice = (double.Parse(item.Element("price").Value) / 100).ToString("F");
                    top.CouponRate = "";
                    top.CouponStartTime = "";
                    top.ItemLocation = "";
                    top.KeywordClickUrl = item.Element("tagUrl").Value;
                    top.Keywords = item.Element("keyWord").Value;
                    top.Nick = item.Element("nickName").Value;
                    //top.NumIid = long.Parse(item.Element("itemId").Value);
                    top.NumIid = 0;
                    top.PicUrl = item.Element("bigUri").Value;
                    top.Price = (double.Parse(item.Element("price").Value) / 100).ToString();
                    top.PromotionPrice = (double.Parse(item.Element("price").Value) / 100).ToString();
                    top.SellerCreditScore = long.Parse(item.Element("level").Value);
                    top.SellerId = long.Parse(item.Element("uin").Value);
                    top.ShopClickUrl = "";
                    top.ShopType = "B";
                    top.TaobaokeCatClickUrl = "";
                    top.Title = item.Element("title").Value;
                    top.TopItemId = item.Element("commId").Value;
                    top.Volume = long.Parse(item.Element("saleNum").Value);

                    result.Add(top);
                }
                catch
                {
                    continue;
                }
            }

            return result;
        }

        #endregion 私有方法
    }
}
