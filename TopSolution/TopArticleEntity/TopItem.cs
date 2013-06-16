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
using System.Text;
using System.Xml.Serialization;

namespace TopEntity
{
    [Serializable]
    public class TopItem
    {
        private string _Keywords;
        /// <summary>
        /// 所属的关键词
        /// </summary>
        [XmlAnyElement("keywords")]
        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }


        private string _ClickUrl;
        /// <summary>
        /// 推广点击url
        /// </summary>
        [XmlElement("click_url")]
        public string ClickUrl
        {
            get { return _ClickUrl; }
            set { _ClickUrl = value; }
        }

        private string _Commission;
        /// <summary>
        /// 淘宝客佣金
        /// </summary>
        [XmlElement("commission")]
        public string Commission
        {
            get { return _Commission; }
            set { _Commission = value; }
        }

        private string _CommissionNum;
        /// <summary>
        /// 累计成交量.注：返回的数据是30天内累计推广量
        /// </summary>
        [XmlElement("commission_num")]
        public string CommissionNum
        {
            get { return _CommissionNum; }
            set { _CommissionNum = value; }
        }

        private string _CommissionRate;
        /// <summary>
        /// 淘宝客佣金比率，比如：1234.00代表12.34%
        /// </summary>
        [XmlElement("commission_rate")]
        public string CommissionRate
        {
            get { return _CommissionRate; }
            set { _CommissionRate = value; }
        }

        private string _CommissionVolume;
        /// <summary>
        /// 累计总支出佣金量
        /// </summary>
        [XmlElement("commission_volume")]
        public string CommissionVolume
        {
            get { return _CommissionVolume; }
            set { _CommissionVolume = value; }
        }

        private string _CouponEndTime;
        /// <summary>
        /// 折扣活动结束时间
        /// </summary>
        [XmlElement("coupon_end_time")]
        public string CouponEndTime
        {
            get { return _CouponEndTime; }
            set { _CouponEndTime = value; }
        }

        private string _CouponPrice;
        /// <summary>
        /// 折扣价格
        /// </summary>
        [XmlElement("coupon_price")]
        public string CouponPrice
        {
            get { return _CouponPrice; }
            set { _CouponPrice = value; }
        }

        private string _CouponRate;
        /// <summary>
        /// 折扣比率
        /// </summary>
        [XmlElement("coupon_rate")]
        public string CouponRate
        {
            get { return _CouponRate; }
            set { _CouponRate = value; }
        }

        private string _CouponStartTime;
        /// <summary>
        /// 折扣活动开始时间
        /// </summary>
        [XmlElement("coupon_start_time")]
        public string CouponStartTime
        {
            get { return _CouponStartTime; }
            set { _CouponStartTime = value; }
        }

        private string _ItemLocation;
        /// <summary>
        /// 商品所在地
        /// </summary>
        [XmlElement("item_location")]
        public string ItemLocation
        {
            get { return _ItemLocation; }
            set { _ItemLocation = value; }
        }

        private string _KeywordClickUrl;
        /// <summary>
        /// 淘宝客关键词搜索URL
        /// </summary>
        [XmlElement("keyword_click_url")]
        public string KeywordClickUrl
        {
            get { return _KeywordClickUrl; }
            set { _KeywordClickUrl = value; }
        }

        private string _Nick;
        /// <summary>
        /// 卖家昵称
        /// </summary>
        [XmlElement("nick")]
        public string Nick
        {
            get { return _Nick; }
            set { _Nick = value; }
        }

        private long _NumIid;
        /// <summary>
        /// 淘宝客商品数字id
        /// </summary>
        [XmlElement("num_iid")]
        public long NumIid
        {
            get { return _NumIid; }
            set { _NumIid = value; }
        }

        private string _PicUrl;
        /// <summary>
        /// 图片url
        /// </summary>
        [XmlElement("pic_url")]
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }

        private string _Price;
        /// <summary>
        /// 商品价格
        /// </summary>
        [XmlElement("price")]
        public string Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private string _PromotionPrice;
        /// <summary>
        /// 促销价格
        /// </summary>
        [XmlElement("promotion_price")]
        public string PromotionPrice
        {
            get { return _PromotionPrice; }
            set { _PromotionPrice = value; }
        }

        private long _SellerCreditScore;
        /// <summary>
        /// 卖家信用等级
        /// </summary>
        [XmlElement("seller_credit_score")]
        public long SellerCreditScore
        {
            get { return _SellerCreditScore; }
            set { _SellerCreditScore = value; }
        }

        private long _SellerId;
        /// <summary>
        /// 卖家id
        /// </summary>
        [XmlElement("seller_id")]
        public long SellerId
        {
            get { return _SellerId; }
            set { _SellerId = value; }
        }

        private string _ShopClickUrl;
        /// <summary>
        /// 商品所在店铺的推广点击url
        /// </summary>
        [XmlElement("shop_click_url")]
        public string ShopClickUrl
        {
            get { return _ShopClickUrl; }
            set { _ShopClickUrl = value; }
        }

        private string _ShopType;
        /// <summary>
        /// 店铺类型:B(商城),C(集市)
        /// </summary>
        [XmlElement("shop_type")]
        public string ShopType
        {
            get { return _ShopType; }
            set { _ShopType = value; }
        }

        private string _TaobaokeCatClickUrl;
        /// <summary>
        /// 淘宝客类目推广URL
        /// </summary>
        [XmlElement("taobaoke_cat_click_url")]
        public string TaobaokeCatClickUrl
        {
            get { return _TaobaokeCatClickUrl; }
            set { _TaobaokeCatClickUrl = value; }
        }

        private string _Title;
        /// <summary>
        /// 商品title 宝贝名称
        /// </summary>
        [XmlElement("title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private long _Volume;
        /// <summary>
        /// 30天内交易量
        /// </summary>
        [XmlElement("volume")]
        public long Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }
    }
}
