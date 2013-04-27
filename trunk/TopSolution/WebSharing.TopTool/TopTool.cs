using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace WebSharing.TopTool
{
    public class TopTool
    {
        string _url = "http://gw.api.taobao.com/router/rest";
        string _appkey = "21462832";
        string _appsecret = "ab747724b1ca591006221f397933ab91";

        public TopTool()
        {

        }

        public TopTool(string url, string appkey, string appsecret)
        {
            _url = url;
            _appkey = appkey;
            _appsecret = appsecret;
        }

        /// <summary>
        /// 查询淘客折扣商品
        /// </summary>
        /// <param name="cid">标准商品后台类目id。该ID可以通过taobao.itemcats.get接口获取到。</param>
        /// <param name="keyword">商品标题中包含的关键字. 注意:查询时keyword,cid至少选择其中一个参数</param>
        /// <param name="nick"> 推广者的淘宝会员昵称.注:指的是淘宝的会员登录名</param>
        /// <param name="outerCode">自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道</param>
        /// <param name="sort">default(默认排序),  price_desc(折扣价格从高到低),  price_asc(折扣价格从低到高),  credit_desc(信用等级从高到低),  credit_asc(信用等级从低到高),  commissionRate_desc(佣金比率从高到低),  commissionRate_asc(佣金比率从低到高),  volume_desc(成交量成高到低), volume_asc(成交量从低到高)</param>
        public TaobaokeItemsCouponGetResponse TaobaokeItemsCouponGet(string nick, string outerCode, string keyword, long? cid, string sort)
        {
            ITopClient client = new DefaultTopClient(_url, _appkey, _appsecret);
            TaobaokeItemsCouponGetRequest req = new TaobaokeItemsCouponGetRequest();
            //req.Pid = 123456L;
            req.Nick = nick;
            req.OuterCode = outerCode;
            //req.IsMobile = true;
            req.Keyword = keyword;
            req.CouponType = 1L;
            req.Cid = cid;
            req.ShopType = "all";
            req.Sort = sort;
            //req.StartCouponRate = 7000L;
            //req.EndCouponRate = 8000L;
            //req.StartCredit = "3diamond";
            //req.EndCredit = "4diamond";
            //req.StartCommissionRate = 1234L;
            //req.EndCommissionRate = 2345L;
            //req.StartCommissionVolume = 100L;
            //req.EndCommissionVolume = 200L;
            //req.StartCommissionNum = 100L;
            //req.EndCommissionNum = 101L;
            //req.StartVolume = 100L;
            //req.EndVolume = 101L;
            //req.Area = "杭州";
            req.Fields = "num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume,coupon_price,coupon_rate,coupon_start_time,coupon_end_time,shop_type";
            req.PageNo = 1L;
            req.PageSize = 10L;
            req.ReferType = 1L;
            TaobaokeItemsCouponGetResponse response = client.Execute(req);
            return response;
        }
    }
}
