$(
function () {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P>本文来自: {sitename}(<a href=\"http://{siteurl}\" target=\"_blank\" title=\"{sitename}\">{siteurl}</a>) 原文章链接：<a href=\"" + location.href + "\" target=\"_blank\">" + location.href + "</a></P>");
        $article_content.append($copyright);
    }
}
);