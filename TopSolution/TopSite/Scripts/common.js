$(
function () {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P>本文来自: 美丽小妙招(<a href=\"http://www.meibaimiaozhao.com\" target=\"_blank\" title=\"美丽小妙招\">www.meibaimiaozhao.com</a>) 原文章链接：<a href=\"" + location.href + "\" target=\"_blank\">" + location.href + "</a></P>");
        $article_content.append($copyright);
    }
}
);