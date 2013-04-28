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
        /// <param name="nick">淘宝会员名</param>
        /// <param name="outerCode">输出码</param>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="cid">类目CID</param>
        /// <param name="sort">排序方式</param>
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
