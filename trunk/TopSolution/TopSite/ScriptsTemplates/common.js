document.body.oncopy = function () {
    setTimeout(function () {
        var text = clipboardData.getData("text");
        if (text) {
            text = text + "\r\n本文来自: {sitename}({siteurl}) 原文章链接：" + location.href;
            clipboardData.setData("text", text);
        }
    }, 100)
}