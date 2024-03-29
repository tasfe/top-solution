﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopList.ascx.cs" Inherits="TopSite.Controls.TopList" %>
<div class="topad">
    <%    
        TopLogic.TopLogic logic = new TopLogic.TopLogic();
        IEnumerable<TopEntity.TopItem> items = logic.GetTopItems(KeyWords);

        if (items != null)
        {
            int i = 1;
            foreach (var item in items)
            {
		
    %>
    <div class="topaditem">
        <div class="topsdot">
            <h3>
                <a target="_blank" href="<%=item.ClickUrl %>" rel="nofollow">
                    <%=item.Title%></a>
            </h3>
        </div>
        <div class="topdetail">
            <div class="topimg">
                <a target="_blank" href="<%=item.ClickUrl %>" rel="nofollow">
                    <img width="296" height="296" border="0" alt="<%=item.Title %>" src="<%=item.PicUrl %>" />
                </a>
            </div>
            <div class="topmeta">
                <div class="S_small_meta S_rank transparent">
                    <span>第<%=i %>名</span> 月销量<span><%=item.CommissionNum%></span>
                </div>
                <div class="S_qg transparent">
                    <div class="jiage">
                        <span class="S_fh">￥</span> <span class="S_je"><strike>
                            <%=item.CouponPrice%></strike> </span>
                    </div>
                    <a href="<%=item.ClickUrl %>" target="_blank" rel="nofollow">
                        <img class="qgimg" src="/images/qg.gif" alt="抢购<%=item.Title %>" title="抢购排行榜第<%=i+1 %>名产品：<%=item.Title %>" />
                    </a>
                </div>
            </div>
        </div>
    </div>
    <%    i++;
            }
        } %>
</div>
