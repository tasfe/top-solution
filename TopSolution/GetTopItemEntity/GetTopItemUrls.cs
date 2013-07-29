using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetTopItemEntity
{
    public class GetTopItemUrls
    {
        /// <summary>
        /// 首页
        /// </summary>
        public const string HomeUrl = "http://www.alimama.com/";
        /// <summary>
        /// 登录页面
        /// </summary>
        public const string LoginUrl = "http://www.alimama.com/member/minilogin.htm?style=mini&proxy=http://u.alimama.com/union/proxy.htm&redirect=http://u.alimama.com";
        /// <summary>
        /// 用户中心首页
        /// </summary>
        public const string UserDefaultUrl = "http://u.alimama.com/membersvc/index.htm";
        /// <summary>
        /// 查询页面
        /// </summary>
        public const string UserSelectTaoBaoKeUrl = "http://u.alimama.com/union/spread/selfservice/taokeSearch.htm";
        /// <summary>
        /// 商品搜索页面基础地址
        /// </summary>
        public const string MerchandisePromotion = "http://u.alimama.com/union/spread/selfservice/merchandisePromotion.htm";
        /// <summary>
        /// 网站搜索页面地址格式化字符串，获取真正地址时传入关键字即可
        /// </summary>
        public const string MerchandisePromotionPageFormat = "http://u.alimama.com/union/spread/selfservice/merchandisePromotion.htm?cat=&discountId=&pidvid=&_fmu.a._0.t=1&_fmu.a._0.pe=40&_fmu.a._0.l=&_fmu.a._0.so=_totalnum&c=&rewrite=&cat=&mid=&searchType=0&q={0}&_fmu.a._0.u=&_fmu.a._0.s=&_fmu.a._0.sta=&_fmu.a._0.end=&_fmu.a._0.st=&_fmu.a._0.en=&_fmu.a._0.star=0&loc=#";
        /// <summary>
        /// 下载Excel的地址模板，将ID集合拼接好即可
        /// </summary>
        public const string ExcelUrlFormat = "http://u.alimama.com/union/spread/selfservice/getAuctionsCommission.do?nid={0}";
        /// <summary>
        /// post发送的信息
        /// </summary>
        public const string PostInfo = "_tb_token_={0}&style=&redirect=&proxy=http%3A%2F%2Fwww.alimama.com%2Fproxy.htm&logname=yuweiyuan2004%40163.com&originalLogpasswd=yuweiyuan518&logpasswd=f565015d95bef352fed1429937e79cb0";
        /// <summary>
        /// 提交到的URL
        /// </summary>
        public const string PostToUrl = "http://www.alimama.com/member/minilogin_act.htm";
    }
}
