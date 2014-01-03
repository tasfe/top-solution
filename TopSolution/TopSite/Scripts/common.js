$(
function () {
    jumpMobile();
    addcopyright()
}
);

// 添加版权信息
function addcopyright() {
    var $article_content = $(".article_content");

    if ($article_content) {
        var $copyright = $("<P style='margin-top:10px;'>本文来自: 美白小妙招(<a href=\"http://localhost:5000\" target=\"_blank\" title=\"美白小妙招\">localhost:5000</a>) 原文章链接：<a href=\"" + location.href + "\" target=\"_blank\">" + location.href + "</a></P>");
        $article_content.append($copyright);
    }
}

// 检测浏览器是否移动，并跳转
function jumpMobile() {
    if (browser.mobile) {
        var weburl = location.href;
        var mobileurl = "http://m.meilimiaozhao.com/?host=www.meilimiaozhao.com&src=" + encodeURI(weburl);
        location.href = mobileurl;
    }
}

var browser = {
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {
            trident: u.indexOf('Trident') > -1, //IE内核                
            presto: u.indexOf('Presto') > -1, //opera内核                
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核                
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核                
            mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端                
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端                
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器                
            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器                
            iPad: u.indexOf('iPad') > -1, //是否iPad                
            webApp: u.indexOf('Safari') == -1//是否web应该程序，没有头部与底部            
        };
    } ()
}