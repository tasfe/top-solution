// 添加版权信息
function addcopyright() {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P style='margin-top:10px;'>本文来自: {sitename}(<a href=\"http://{siteurl}\" target=\"_blank\" title=\"{sitename}\">{siteurl}</a>)&nbsp;&nbsp;&nbsp;&nbsp; <a href=\"" + location.href + "\" target=\"_blank\">查看原文</a></P>");
        $article_content.append($copyright);
    }
}

// 获取右侧广告信息
function showAd(keyword, container) {
    $.post("/GetAdsHandler.ashx", { keyword: keyword }, function (data) {
        $("#" + container).html(data);
    }, 'html');
}
