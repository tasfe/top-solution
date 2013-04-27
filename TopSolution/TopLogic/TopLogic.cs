using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Response;
using WebSharing.TopTool;

namespace TopLogic
{
    public class TopLogic
    {
        public TaobaokeItemsCouponGetResponse GetTaobaokeItemsCoupon(string nick, string outerCode, string keyword, long? cid, string sort)
        {
            TaobaokeItemsCouponGetResponse result = null;
            try
            {
                TopTool tool=new TopTool();
                result= tool.TaobaokeItemsCouponGet(nick,outerCode,keyword,cid,sort);
                // Cache
                System.Web.HttpContext.Current.Cache.Insert("lastcacheoftaobaoke", result);
            }
            catch
            {
                
            }

            return result;
        }
    }
}
