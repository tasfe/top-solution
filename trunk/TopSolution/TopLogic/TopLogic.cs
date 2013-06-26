using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Response;
using WebSharing.TopTool;
using TopEntity;
using Top.Api.Domain;
using System.Reflection;
using WebSharing.DB4ODAL;

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
        /// 获取淘宝客打折信息。
        /// </summary>
        /// <param name="cid">标准商品后台类目id。该ID可以通过taobao.itemcats.get接口获取到。</param>
        /// <param name="keyword">商品标题中包含的关键字. 注意:查询时keyword,cid至少选择其中一个参数</param>
        /// <param name="nick"> 推广者的淘宝会员昵称.注:指的是淘宝的会员登录名</param>
        /// <param name="outerCode">自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道</param>
        /// <param name="sort">default(默认排序),  price_desc(折扣价格从高到低),  price_asc(折扣价格从低到高),  credit_desc(信用等级从高到低),  credit_asc(信用等级从低到高),  commissionRate_desc(佣金比率从高到低),  commissionRate_asc(佣金比率从低到高),  volume_desc(成交量成高到低), volume_asc(成交量从低到高)</param>
        /// <returns></returns>
        public TaobaokeItemsCouponGetResponse GetTaobaokeItemsCoupon(string outerCode, string keyword, long? cid, string sort)
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
                else
                {
                    result = adClient.GetList<TopItem>(p => p.Keywords == keyword).OrderByDescending(p => p.Volume).Take(10);
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

        #endregion 私有方法
    }
}
