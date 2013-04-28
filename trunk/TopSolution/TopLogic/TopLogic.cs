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
        /// <param name="nick"></param>
        /// <param name="outerCode"></param>
        /// <param name="keyword"></param>
        /// <param name="cid"></param>
        /// <param name="sort"></param>
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
