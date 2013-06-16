<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopList.ascx.cs" Inherits="TopSite.Controls.TopList" %>
<link href="../Styles/TopAd.css" rel="stylesheet" type="text/css" />
<div class="topad">
    <%    
        TopLogic.TopLogic logic = new TopLogic.TopLogic();
        Top.Api.Response.TaobaokeItemsCouponGetResponse response = logic.GetTaobaokeItemsCoupon("site", KeyWords, 0, "volume_desc");

        if (response != null)
        {
            if (response.IsError)
            {
                Response.Write(response.ErrCode + " " + response.ErrMsg);
            }
            else
            {
                for (int i = 0; i < response.TaobaokeItems.Count; i++)
                {
                    var item = response.TaobaokeItems[i];
    %>
    <div class="topaditem">
        <div class="topsdot">
            <h3>
                <a target="_blank" href="<%=item.ClickUrl %>">
                    <%=item.Title%></a>
            </h3>
        </div>
        <div class="topdetail">
            <div class="topimg">
                <a target="_blank" href="<%=item.ClickUrl %>">
                    <img width="315" height="315" border="0" alt="<%=item.Title %>" src="<%=item.PicUrl %>" />
                </a>
            </div>
            <div class="topmeta">
                <div class="S_small_meta S_rank transparent">
                    <span>第<%=i + 1%>名</span> 月销量<span><%=item.Volume%></span>
                </div>
                <div class="S_qg transparent">
                    <div class="jiage">
                        <span class="S_fh">￥</span> <span class="S_je"><strike>
                            <%=item.CouponPrice%></strike> </span>
                    </div>
                    <a href="<%=item.ClickUrl %>" target="_blank">
                        <img class="qgimg" src="/images/qg.gif" alt="抢购<%=item.Title %>" title="抢购排行榜第<%=i+1 %>名产品：<%=item.Title %>" />
                    </a>
                </div>
            </div>
        </div>
    </div>
    <% 
                }
            }
        } %>
</div>
