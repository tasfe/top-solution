document.body.oncopy = function () {
    setTimeout(function () {
        var text = clipboardData.getData("text");
        if (text) {
            text = text + "\r\n本文来自: 美丽妙招(www.meilimiaozhao.com) 原文章链接：" + location.href;
            clipboardData.setData("text", text);
        }
    }, 100)
}