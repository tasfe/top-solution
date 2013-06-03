$(
function () {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P>本文来自: 美丽妙招(<a href=\"http://www.meilimiaozhao.com\" target=\"_blank\" title=\"美丽妙招\">www.meilimiaozhao.com</a>) 原文章链接：<a href=\"" + location.href + "\" target=\"_blank\">" + location.href + "</a></P>");
        $article_content.append($copyright);
    }
}
);