$(
function () {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P style='margin-top:10px;'>本文来自: 美白小妙招(<a href=\"http://localhost:5000\" target=\"_blank\" title=\"美白小妙招\">localhost:5000</a>) 原文章链接：<a href=\"" + location.href + "\" target=\"_blank\">" + location.href + "</a></P>");
        $article_content.append($copyright);
    }
}
);