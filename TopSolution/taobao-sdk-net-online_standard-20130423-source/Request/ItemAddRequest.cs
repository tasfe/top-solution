using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.item.add
    /// </summary>
    public class ItemAddRequest : ITopUploadRequest<ItemAddResponse>
    {
        /// <summary>
        /// 售后说明模板id
        /// </summary>
        public Nullable<long> AfterSaleId { get; set; }

        /// <summary>
        /// 商品上传后的状态。可选值:onsale(出售中),instock(仓库中);默认值:onsale
        /// </summary>
        public string ApproveStatus { get; set; }

        /// <summary>
        /// 商品的积分返点比例。如:5,表示:返点比例0.5%. 注意：返点比例必须是>0的整数，而且最大是90,即为9%.B商家在发布非虚拟商品时，返点必须是 5的倍数，即0.5%的倍数。其它是1的倍数，即0.1%的倍数。无名良品商家发布商品时，复用该字段记录积分宝返点比例，返点必须是对应类目的返点步长的整数倍，默认是5，即0.5%。注意此时该字段值依旧必须是>0的整数，最高值不超过500，即50%
        /// </summary>
        public Nullable<long> AuctionPoint { get; set; }

        /// <summary>
        /// 代充商品类型。在代充商品的类目下，不传表示不标记商品类型（交易搜索中就不能通过标记搜到相关的交易了）。可选类型：  no_mark(不做类型标记)  time_card(点卡软件代充)  fee_card(话费软件代充)
        /// </summary>
        public string AutoFill { get; set; }

        /// <summary>
        /// 商品基础色，数据格式为：pid:vid:rvid1,rvid2,rvid3;pid:vid:rvid1
        /// </summary>
        public string ChangeProp { get; set; }

        /// <summary>
        /// 叶子类目id
        /// </summary>
        public Nullable<long> Cid { get; set; }

        /// <summary>
        /// 此为货到付款运费模板的ID，对应的JAVA类型是long,如果COD卖家应用了货到付款运费模板，此值要进行设置。
        /// </summary>
        public Nullable<long> CodPostageId { get; set; }

        /// <summary>
        /// 宝贝描述。字数要大于5个字符，小于25000个字符，受违禁词控制
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// ems费用。取值范围:0.01-999.00;精确到2位小数;单位:元。如:25.07，表示:25元7分
        /// </summary>
        public string EmsFee { get; set; }

        /// <summary>
        /// 快递费用。取值范围:0.01-999.00;精确到2位小数;单位:元。如:15.07，表示:15元7分
        /// </summary>
        public string ExpressFee { get; set; }

        /// <summary>
        /// 厂家联系方式
        /// </summary>
        public string FoodSecurityContact { get; set; }

        /// <summary>
        /// 产品标准号
        /// </summary>
        public string FoodSecurityDesignCode { get; set; }

        /// <summary>
        /// 厂名
        /// </summary>
        public string FoodSecurityFactory { get; set; }

        /// <summary>
        /// 厂址
        /// </summary>
        public string FoodSecurityFactorySite { get; set; }

        /// <summary>
        /// 食品添加剂
        /// </summary>
        public string FoodSecurityFoodAdditive { get; set; }

        /// <summary>
        /// 配料表
        /// </summary>
        public string FoodSecurityMix { get; set; }

        /// <summary>
        /// 保质期
        /// </summary>
        public string FoodSecurityPeriod { get; set; }

        /// <summary>
        /// 储藏方法
        /// </summary>
        public string FoodSecurityPlanStorage { get; set; }

        /// <summary>
        /// 生产许可证号
        /// </summary>
        public string FoodSecurityPrdLicenseNo { get; set; }

        /// <summary>
        /// 生产结束日期,格式必须为yyyy-MM-dd
        /// </summary>
        public string FoodSecurityProductDateEnd { get; set; }

        /// <summary>
        /// 生产开始日期，格式必须为yyyy-MM-dd
        /// </summary>
        public string FoodSecurityProductDateStart { get; set; }

        /// <summary>
        /// 进货结束日期，要在生产日期之后，格式必须为yyyy-MM-dd
        /// </summary>
        public string FoodSecurityStockDateEnd { get; set; }

        /// <summary>
        /// 进货开始日期，要在生产日期之后，格式必须为yyyy-MM-dd
        /// </summary>
        public string FoodSecurityStockDateStart { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public string FoodSecuritySupplier { get; set; }

        /// <summary>
        /// 运费承担方式。可选值:seller（卖家承担）,buyer(买家承担);默认值:seller。卖家承担不用设置邮费和postage_id.买家承担的时候，必填邮费和postage_id   如果用户设置了运费模板会优先使用运费模板，否则要同步设置邮费（post_fee,express_fee,ems_fee）
        /// </summary>
        public string FreightPayer { get; set; }

        /// <summary>
        /// 针对全球购卖家的库存类型业务，  有两种库存类型：现货和代购  参数值为1时代表现货，值为2时代表代购  如果传值为这两个值之外的值，会报错;  如果不是全球购卖家，这两个值即使设置也不会处理
        /// </summary>
        public string GlobalStockType { get; set; }

        /// <summary>
        /// 支持会员打折。可选值:true,false;默认值:false(不打折)
        /// </summary>
        public Nullable<bool> HasDiscount { get; set; }

        /// <summary>
        /// 是否有发票。可选值:true,false (商城卖家此字段必须为true);默认值:false(无发票)
        /// </summary>
        public Nullable<bool> HasInvoice { get; set; }

        /// <summary>
        /// 橱窗推荐。可选值:true,false;默认值:false(不推荐)
        /// </summary>
        public Nullable<bool> HasShowcase { get; set; }

        /// <summary>
        /// 是否有保修。可选值:true,false;默认值:false(不保修)
        /// </summary>
        public Nullable<bool> HasWarranty { get; set; }

        /// <summary>
        /// 商品主图片。类型:JPG,GIF;最大长度:500K
        /// </summary>
        public FileItem Image { get; set; }

        /// <summary>
        /// 加价(降价)幅度。如果为0，代表系统代理幅度。对于增价拍和荷兰拍来说是加价幅度，对于降价拍来说是降价幅度。
        /// </summary>
        public string Increment { get; set; }

        /// <summary>
        /// 用户自行输入的类目属性ID串。结构："pid1,pid2,pid3"，如："20000"（表示品牌） 注：通常一个类目下用户可输入的关键属性不超过1个。
        /// </summary>
        public string InputPids { get; set; }

        /// <summary>
        /// 用户自行输入的子属性名和属性值，结构:"父属性值;一级子属性名;一级子属性值;二级子属性名;自定义输入值,....",如：“耐克;耐克系列;科比系列;科比系列;2K5,Nike乔丹鞋;乔丹系列;乔丹鞋系列;乔丹鞋系列;json5”，多个自定义属性用','分割，input_str需要与input_pids一一对应，注：通常一个类目下用户可输入的关键属性不超过1个。所有属性别名加起来不能超过3999字节
        /// </summary>
        public string InputStr { get; set; }

        /// <summary>
        /// 是否是3D
        /// </summary>
        public Nullable<bool> Is3D { get; set; }

        /// <summary>
        /// 是否在外店显示
        /// </summary>
        public Nullable<bool> IsEx { get; set; }

        /// <summary>
        /// 实物闪电发货
        /// </summary>
        public Nullable<bool> IsLightningConsignment { get; set; }

        /// <summary>
        /// 是否在淘宝上显示（如果传FALSE，则在淘宝主站无法显示该商品）
        /// </summary>
        public Nullable<bool> IsTaobao { get; set; }

        /// <summary>
        /// 商品是否为新品。只有在当前类目开通新品,并且当前用户拥有该类目下发布新品权限时才能设置is_xinpin为true，否则设置true后会返回错误码:isv.invalid-permission:add-xinpin。同时只有一口价全新的宝贝才能设置为新品，否则会返回错误码：isv.invalid-parameter:xinpin。不设置该参数值或设置为false效果一致。
        /// </summary>
        public Nullable<bool> IsXinpin { get; set; }

        /// <summary>
        /// 表示商品的体积，如果需要使用按体积计费的运费模板，一定要设置这个值。该值的单位为立方米（m3），如果是其他单位，请转换成成立方米。  该值支持两种格式的设置：格式1：bulk:3,单位为立方米(m3),表示直接设置为商品的体积。格式2：length:10;breadth:10;height:10，单位为米（m）。体积和长宽高都支持小数类型。  在传入体积或长宽高时候，不能带单位。体积的单位默认为立方米（m3），长宽高的单位默认为米(m)  该值支持两种格式的设置：格式1：bulk:3,单位为立方米(m3),表示直接设置为商品的体积。格式2：length:10;breadth:10;height:10，单位为米（m）
        /// </summary>
        public string ItemSize { get; set; }

        /// <summary>
        /// 商品的重量，用于按重量计费的运费模板。注意：单位为kg。  只能传入数值类型（包含小数），不能带单位，单位默认为kg。
        /// </summary>
        public string ItemWeight { get; set; }

        /// <summary>
        /// 商品文字的字符集。繁体传入"zh_HK"，简体传入"zh_CN"，不传默认为简体
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// 定时上架时间。(时间格式：yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public Nullable<DateTime> ListTime { get; set; }

        /// <summary>
        /// 发布电子凭证宝贝时候表示是否使用邮寄 0: 代表不使用邮寄； 1：代表使用邮寄；如果不设置这个值，代表不使用邮寄
        /// </summary>
        public string LocalityLifeChooseLogis { get; set; }

        /// <summary>
        /// 本地生活电子交易凭证业务，目前此字段只涉及到的信息为有效期;  如果有效期为起止日期类型，此值为2012-08-06,2012-08-16  如果有效期为【购买成功日 至】类型则格式为2012-08-16  如果有效期为天数类型则格式为15
        /// </summary>
        public string LocalityLifeExpirydate { get; set; }

        /// <summary>
        /// 码商信息，格式为 码商id:nick
        /// </summary>
        public string LocalityLifeMerchant { get; set; }

        /// <summary>
        /// 网点ID
        /// </summary>
        public string LocalityLifeNetworkId { get; set; }

        /// <summary>
        /// 电子凭证售中自动退款比例，百分比%前的数字，介于1-100之间的整数
        /// </summary>
        public Nullable<long> LocalityLifeOnsaleAutoRefundRatio { get; set; }

        /// <summary>
        /// 退款比例，  百分比%前的数字,1-100的正整数值
        /// </summary>
        public Nullable<long> LocalityLifeRefundRatio { get; set; }

        /// <summary>
        /// 核销打款   1代表核销打款 0代表非核销打款
        /// </summary>
        public string LocalityLifeVerification { get; set; }

        /// <summary>
        /// 所在地城市。如杭州 。可以通过http://dl.open.taobao.com/sdk/商品城市列表.rar查询
        /// </summary>
        public string LocationCity { get; set; }

        /// <summary>
        /// 所在地省份。如浙江，具体可以下载http://dl.open.taobao.com/sdk/商品城市列表.rar  取到
        /// </summary>
        public string LocationState { get; set; }

        /// <summary>
        /// 商品数量，取值范围:0-999999的整数。且需要等于Sku所有数量的和。  拍卖商品中增加拍只能为1，荷兰拍要在[2,500)范围内。
        /// </summary>
        public Nullable<long> Num { get; set; }

        /// <summary>
        /// 商品外部编码，该字段的最大长度是512个字节
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// 拍卖宝贝的保证金。对于增价拍和荷兰拍来说保证金有两种模式：淘宝默认模式（首次出价金额的10%），自定义固定保证金（固定冻结金额只能输入不超过30万的正整数），并且保证金只冻结1次。对于降价拍来说保证金只有淘宝默认的（竞拍价格的10% * 竞拍数量），并且每次出价都需要冻结保证金。  对于拍卖宝贝来说，保证金是必须的，但是默认使用淘宝默认保证金模式，只有用户需要使用自定义固定保证金的时候才需要使用到这个参数，如果该参数不传或传入0则代表使用默认。
        /// </summary>
        public Nullable<long> PaimaiInfoDeposit { get; set; }

        /// <summary>
        /// 降价拍宝贝的降价周期(分钟)。降价拍宝贝的价格每隔paimai_info.interval时间会下降一次increment。
        /// </summary>
        public Nullable<long> PaimaiInfoInterval { get; set; }

        /// <summary>
        /// 拍卖商品选择的拍卖类型，拍卖类型包括三种：增价拍(1)，荷兰拍(2)和降价拍(3)。
        /// </summary>
        public Nullable<long> PaimaiInfoMode { get; set; }

        /// <summary>
        /// 降价拍宝贝的保留价。对于降价拍来说，paimai_info.reserve必须大于0，且小于price-increment，而且（price-paimai_info.reserve）/increment的计算结果必须为整数
        /// </summary>
        public string PaimaiInfoReserve { get; set; }

        /// <summary>
        /// 自定义销售周期的小时数。拍卖宝贝可以自定义销售周期，这里指定销售周期的小时数。注意，该参数只作为输入参数，不能通过taobao.item.get接口获取。
        /// </summary>
        public Nullable<long> PaimaiInfoValidHour { get; set; }

        /// <summary>
        /// 自定义销售周期的分钟数。拍卖宝贝可以自定义销售周期，这里是指定销售周期的分钟数。自定义销售周期的小时数。拍卖宝贝可以自定义销售周期，这里指定销售周期的小时数。注意，该参数只作为输入参数，不能通过taobao.item.get接口获取。
        /// </summary>
        public Nullable<long> PaimaiInfoValidMinute { get; set; }

        /// <summary>
        /// 商品主图需要关联的图片空间的相对url。这个url所对应的图片必须要属于当前用户。pic_path和image只需要传入一个,如果两个都传，默认选择pic_path
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 平邮费用。取值范围:0.01-999.00;精确到2位小数;单位:元。如:5.07，表示:5元7分. 注:post_fee,express_fee,ems_fee需要一起填写
        /// </summary>
        public string PostFee { get; set; }

        /// <summary>
        /// 宝贝所属的运费模板ID。取值范围：整数且必须是该卖家的运费模板的ID（可通过taobao.delivery.template.get获得当前会话用户的所有邮费模板）
        /// </summary>
        public Nullable<long> PostageId { get; set; }

        /// <summary>
        /// 商品价格。取值范围:0-100000000;精确到2位小数;单位:元。如:200.07，表示:200元7分。需要在正确的价格区间内。  拍卖商品对应的起拍价。
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 商品所属的产品ID(B商家发布商品需要用)
        /// </summary>
        public Nullable<long> ProductId { get; set; }

        /// <summary>
        /// 属性值别名。如pid:vid:别名;pid1:vid1:别名1 ，其中：pid是属性id vid是属性值id。总长度不超过511字节
        /// </summary>
        public string PropertyAlias { get; set; }

        /// <summary>
        /// 商品属性列表。格式:pid:vid;pid:vid。属性的pid调用taobao.itemprops.get取得，属性值的vid用taobao.itempropvalues.get取得vid。 如果该类目下面没有属性，可以不用填写。如果有属性，必选属性必填，其他非必选属性可以选择不填写.属性不能超过35对。所有属性加起来包括分割符不能超过549字节，单个属性没有限制。 如果有属性是可输入的话，则用字段input_str填入属性的值
        /// </summary>
        public string Props { get; set; }

        /// <summary>
        /// 景区门票在选择订金支付时候，需要交的预订费。传入的值是1到20之间的数值，小数点后最多可以保留两位（多余的部分将做四舍五入的处理）。这个数值表示的是预订费的比例，最终的预订费为 scenic_ticket_book_cost乘一口价除以100
        /// </summary>
        public string ScenicTicketBookCost { get; set; }

        /// <summary>
        /// 景区门票类宝贝发布时候，当卖家签订了支付宝代扣协议时候，需要选择支付方式：全额支付和订金支付。当scenic_ticket_pay_way为1时表示全额支付，为2时表示订金支付
        /// </summary>
        public Nullable<long> ScenicTicketPayWay { get; set; }

        /// <summary>
        /// 是否承诺退换货服务!虚拟商品无须设置此项!
        /// </summary>
        public Nullable<bool> SellPromise { get; set; }

        /// <summary>
        /// 商品所属的店铺类目列表。按逗号分隔。结构:",cid1,cid2,...,"，如果店铺类目存在二级类目，必须传入子类目cids。
        /// </summary>
        public string SellerCids { get; set; }

        /// <summary>
        /// Sku的外部id串，结构如：1234,1342,…  sku_properties, sku_quantities, sku_prices, sku_outer_ids在输入数据时要一一对应，如果没有sku_outer_ids也要写上这个参数，入参是","(这个是两个sku的示列，逗号数应该是sku个数减1)；该参数最大长度是512个字节
        /// </summary>
        public string SkuOuterIds { get; set; }

        /// <summary>
        /// Sku的价格串，结构如：10.00,5.00,… 精确到2位小数;单位:元。如:200.07，表示:200元7分
        /// </summary>
        public string SkuPrices { get; set; }

        /// <summary>
        /// 更新的Sku的属性串，调用taobao.itemprops.get获取类目属性，如果属性是销售属性，再用taobao.itempropvalues.get取得vid。格式:pid:vid;pid:vid,多个sku之间用逗号分隔。该字段内的销售属性（自定义的除外）也需要在props字段填写。sku的销售属性需要一同选取，如:颜色，尺寸。如果新增商品包含了sku，则此字段一定要传入。这个字段的长度要控制在512个字节以内。  如果有自定义销售属性，则格式为pid:vid;pid2:vid2;$pText:vText , 其中$pText:vText为自定义属性。限制：其中$pText的’$’前缀不能少，且pText和vText文本中不可以存在冒号:和分号;以及逗号，
        /// </summary>
        public string SkuProperties { get; set; }

        /// <summary>
        /// Sku的数量串，结构如：num1,num2,num3 如：2,3
        /// </summary>
        public string SkuQuantities { get; set; }

        /// <summary>
        /// 新旧程度。可选值：new(新)，second(二手)，unused(闲置)。B商家不能发布二手商品。  如果是二手商品，特定类目下属性里面必填新旧成色属性
        /// </summary>
        public string StuffStatus { get; set; }

        /// <summary>
        /// 商品是否支持拍下减库存:1支持;2取消支持(付款减库存);0(默认)不更改 集市卖家默认拍下减库存; 商城卖家默认付款减库存
        /// </summary>
        public Nullable<long> SubStock { get; set; }

        /// <summary>
        /// 宝贝标题。不能超过60字符，受违禁词控制
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发布类型。可选值:fixed(一口价),auction(拍卖)。B商家不能发布拍卖商品，而且拍卖商品是没有SKU的。  拍卖商品发布时需要附加拍卖商品信息：拍卖类型(paimai_info.mode，拍卖类型包括三种：增价拍[1]，荷兰拍[2]以及降价拍[3])，商品数量(num)，起拍价(price)，价格幅度(increament)，保证金(paimai_info.deposit)。另外拍卖商品支持自定义销售周期，通过paimai_info.valid_hour和paimai_info.valid_minute来指定。对于降价拍来说需要设置降价周期(paimai_info.interval)和拍卖保留价(paimai_info.reserve)。  注意：通过taobao.item.get接口获取拍卖信息时，会返回除了valid_hour和valid_minute之外的所有拍卖信息。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 有效期。可选值:7,14;单位:天;默认值:14
        /// </summary>
        public Nullable<long> ValidThru { get; set; }

        /// <summary>
        /// 商品的重量(商超卖家专用字段)
        /// </summary>
        public Nullable<long> Weight { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.item.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("after_sale_id", this.AfterSaleId);
            parameters.Add("approve_status", this.ApproveStatus);
            parameters.Add("auction_point", this.AuctionPoint);
            parameters.Add("auto_fill", this.AutoFill);
            parameters.Add("change_prop", this.ChangeProp);
            parameters.Add("cid", this.Cid);
            parameters.Add("cod_postage_id", this.CodPostageId);
            parameters.Add("desc", this.Desc);
            parameters.Add("ems_fee", this.EmsFee);
            parameters.Add("express_fee", this.ExpressFee);
            parameters.Add("food_security.contact", this.FoodSecurityContact);
            parameters.Add("food_security.design_code", this.FoodSecurityDesignCode);
            parameters.Add("food_security.factory", this.FoodSecurityFactory);
            parameters.Add("food_security.factory_site", this.FoodSecurityFactorySite);
            parameters.Add("food_security.food_additive", this.FoodSecurityFoodAdditive);
            parameters.Add("food_security.mix", this.FoodSecurityMix);
            parameters.Add("food_security.period", this.FoodSecurityPeriod);
            parameters.Add("food_security.plan_storage", this.FoodSecurityPlanStorage);
            parameters.Add("food_security.prd_license_no", this.FoodSecurityPrdLicenseNo);
            parameters.Add("food_security.product_date_end", this.FoodSecurityProductDateEnd);
            parameters.Add("food_security.product_date_start", this.FoodSecurityProductDateStart);
            parameters.Add("food_security.stock_date_end", this.FoodSecurityStockDateEnd);
            parameters.Add("food_security.stock_date_start", this.FoodSecurityStockDateStart);
            parameters.Add("food_security.supplier", this.FoodSecuritySupplier);
            parameters.Add("freight_payer", this.FreightPayer);
            parameters.Add("global_stock_type", this.GlobalStockType);
            parameters.Add("has_discount", this.HasDiscount);
            parameters.Add("has_invoice", this.HasInvoice);
            parameters.Add("has_showcase", this.HasShowcase);
            parameters.Add("has_warranty", this.HasWarranty);
            parameters.Add("increment", this.Increment);
            parameters.Add("input_pids", this.InputPids);
            parameters.Add("input_str", this.InputStr);
            parameters.Add("is_3D", this.Is3D);
            parameters.Add("is_ex", this.IsEx);
            parameters.Add("is_lightning_consignment", this.IsLightningConsignment);
            parameters.Add("is_taobao", this.IsTaobao);
            parameters.Add("is_xinpin", this.IsXinpin);
            parameters.Add("item_size", this.ItemSize);
            parameters.Add("item_weight", this.ItemWeight);
            parameters.Add("lang", this.Lang);
            parameters.Add("list_time", this.ListTime);
            parameters.Add("locality_life.choose_logis", this.LocalityLifeChooseLogis);
            parameters.Add("locality_life.expirydate", this.LocalityLifeExpirydate);
            parameters.Add("locality_life.merchant", this.LocalityLifeMerchant);
            parameters.Add("locality_life.network_id", this.LocalityLifeNetworkId);
            parameters.Add("locality_life.onsale_auto_refund_ratio", this.LocalityLifeOnsaleAutoRefundRatio);
            parameters.Add("locality_life.refund_ratio", this.LocalityLifeRefundRatio);
            parameters.Add("locality_life.verification", this.LocalityLifeVerification);
            parameters.Add("location.city", this.LocationCity);
            parameters.Add("location.state", this.LocationState);
            parameters.Add("num", this.Num);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("paimai_info.deposit", this.PaimaiInfoDeposit);
            parameters.Add("paimai_info.interval", this.PaimaiInfoInterval);
            parameters.Add("paimai_info.mode", this.PaimaiInfoMode);
            parameters.Add("paimai_info.reserve", this.PaimaiInfoReserve);
            parameters.Add("paimai_info.valid_hour", this.PaimaiInfoValidHour);
            parameters.Add("paimai_info.valid_minute", this.PaimaiInfoValidMinute);
            parameters.Add("pic_path", this.PicPath);
            parameters.Add("post_fee", this.PostFee);
            parameters.Add("postage_id", this.PostageId);
            parameters.Add("price", this.Price);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("property_alias", this.PropertyAlias);
            parameters.Add("props", this.Props);
            parameters.Add("scenic_ticket_book_cost", this.ScenicTicketBookCost);
            parameters.Add("scenic_ticket_pay_way", this.ScenicTicketPayWay);
            parameters.Add("sell_promise", this.SellPromise);
            parameters.Add("seller_cids", this.SellerCids);
            parameters.Add("sku_outer_ids", this.SkuOuterIds);
            parameters.Add("sku_prices", this.SkuPrices);
            parameters.Add("sku_properties", this.SkuProperties);
            parameters.Add("sku_quantities", this.SkuQuantities);
            parameters.Add("stuff_status", this.StuffStatus);
            parameters.Add("sub_stock", this.SubStock);
            parameters.Add("title", this.Title);
            parameters.Add("type", this.Type);
            parameters.Add("valid_thru", this.ValidThru);
            parameters.Add("weight", this.Weight);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateMinValue("cid", this.Cid, 0);
            RequestValidator.ValidateRequired("desc", this.Desc);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 200000);
            RequestValidator.ValidateMaxLength("image", this.Image, 524288);
            RequestValidator.ValidateRequired("location.city", this.LocationCity);
            RequestValidator.ValidateRequired("location.state", this.LocationState);
            RequestValidator.ValidateRequired("num", this.Num);
            RequestValidator.ValidateMaxValue("num", this.Num, 999999);
            RequestValidator.ValidateMinValue("num", this.Num, 0);
            RequestValidator.ValidateMaxValue("paimai_info.interval", this.PaimaiInfoInterval, 60);
            RequestValidator.ValidateMinValue("paimai_info.interval", this.PaimaiInfoInterval, 1);
            RequestValidator.ValidateMaxValue("paimai_info.mode", this.PaimaiInfoMode, 3);
            RequestValidator.ValidateMinValue("paimai_info.mode", this.PaimaiInfoMode, 1);
            RequestValidator.ValidateMaxValue("paimai_info.valid_hour", this.PaimaiInfoValidHour, 48);
            RequestValidator.ValidateMinValue("paimai_info.valid_hour", this.PaimaiInfoValidHour, 1);
            RequestValidator.ValidateMaxValue("paimai_info.valid_minute", this.PaimaiInfoValidMinute, 59);
            RequestValidator.ValidateMinValue("paimai_info.valid_minute", this.PaimaiInfoValidMinute, 0);
            RequestValidator.ValidateRequired("price", this.Price);
            RequestValidator.ValidateMaxLength("property_alias", this.PropertyAlias, 511);
            RequestValidator.ValidateMaxListSize("seller_cids", this.SellerCids, 10);
            RequestValidator.ValidateRequired("stuff_status", this.StuffStatus);
            RequestValidator.ValidateRequired("title", this.Title);
            RequestValidator.ValidateMaxLength("title", this.Title, 60);
            RequestValidator.ValidateRequired("type", this.Type);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }
    }
}
