using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Response;
using WebSharing.TopTool;
using TopArticleEntity;

namespace TopLogic
{
    public class TopLogic
    {
        /// <summary>
        /// 获取淘宝客打折信息。
        /// </summary>
        /// <param name="cid">标准商品后台类目id。该ID可以通过taobao.itemcats.get接口获取到。</param>
        /// <param name="keyword">商品标题中包含的关键字. 注意:查询时keyword,cid至少选择其中一个参数</param>
        /// <param name="nick"> 推广者的淘宝会员昵称.注:指的是淘宝的会员登录名</param>
        /// <param name="outerCode">自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道</param>
        /// <param name="sort">default(默认排序),  price_desc(折扣价格从高到低),  price_asc(折扣价格从低到高),  credit_desc(信用等级从高到低),  credit_asc(信用等级从低到高),  commissionRate_desc(佣金比率从高到低),  commissionRate_asc(佣金比率从低到高),  volume_desc(成交量成高到低), volume_asc(成交量从低到高)</param>
        /// <returns></returns>
        public TaobaokeItemsCouponGetResponse GetTaobaokeItemsCoupon(string nick, string outerCode, string keyword, long? cid, string sort)
        {
            bool isFromCache = false;
            TaobaokeItemsCouponGetResponse result = null;
            try
            {
                TopTool tool = new TopTool();
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


    }
}
