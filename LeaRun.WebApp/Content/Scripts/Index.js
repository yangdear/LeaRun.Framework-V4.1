$(function () {
    $(window).load(function () {
        window.setTimeout(function () {
            $('#ajax-loader').fadeOut();
        }, 200);
    });
});
//图标闪乐
function IconSong(id) {
    var $obj = $("#" + id);
    if (!$obj.hasClass(id + '_ok')) {
        $obj.addClass(id + '_ok');
        $obj.hide();
    } else {
        $obj.removeClass(id + '_ok');
        $obj.show();
    }
}
//服务器当前日期
function ServerCurrentTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu + ":" + sec;
    $("#CurrentTime").text(time);
    var timer = setTimeout("ServerCurrentTime()", 1000);
}
//样式
function readyIndex() {
    $(".main_menu li div").click(function () {
        $(".main_menu li div").removeClass('main_menu leftselected');
        $(this).addClass('main_menu leftselected');
    }).hover(function () {
        $(this).addClass("hoverleftselected");
    }, function () {
        $(this).removeClass("hoverleftselected");
    });
    //点击TOP按钮显示标签
    $("#topnav .droppopup a").hover(function () {
        $("#topnav .droppopup a").removeClass('onnav');
        $(this).addClass('onnav');
        var Y = $(this).attr("offsetLeft");
        $(this).find('.popup').show().css('top', ($(this).offset().top + 71)).css('left', $(this).offset().left - ($(this).find('.popup').width() / 2 - 36));
    }, function () {
        $("#topnav .droppopup a").removeClass('onnav');
        $(this).find('.popup').hide();
    });
    $(".popup li").click(function () {
        $('.popup').hide();
    })
}
/**安全退出**/
function IndexOut() {
    var msg = "<div class='ui_alert'>确认要退出 LeaRun.信息化快速开发框架？</div>"
    top.$.dialog({
        id: "confirmDialog",
        lock: true,
        icon: "hits.png",
        content: msg,
        title: "力软提示",
        button: [
        {
            name: '退出',
            callback: function () {
                Loading(true, "正在退出系统...");
                window.setTimeout(function () {
                    getAjax("/Login/OutLogin", "", function (data) {
                        window.opener = null;
                        var wind = window.open('', '_self');
                        wind.close();
                    })
                }, 200);
            }
        },
        {
            name: '注销',
            callback: function () {
                Loading(true, "正在注销系统...");
                window.setTimeout(function () {
                    getAjax("/Login/OutLogin", "", function (data) {
                        window.location.href = '../Login/Index';
                    })
                }, 200);
            }
        },
        {
            name: '取消'
        }
        ]
    });
}
//个人中心
function PersonCenter() {
    var url = "/CommonModule/User/PersonCenter";
    Dialog(url, "ResetPassword", "个人中心", 750, 400);
}
//快捷方式列表
function ShortcutsList() {
    $("#Shortcuts").html('');
    AjaxJson("/Home/ShortcutsListJson", {}, function (DataJson) {
        $.each(DataJson, function (i) {
            $("#Shortcuts").append("<li onclick=\"AddTabMenu('" + DataJson[i].ModuleId + "', '" + DataJson[i].Location + "', '" + DataJson[i].FullName + "',  '" + DataJson[i].Icon + "','true');\"><img src=\"../Content/Images/Icon16/" + DataJson[i].Icon + "\" />" + DataJson[i].FullName + "</li>");
        });
    });
    $(".popup li").click(function () {
        $('.popup').hide();
    })
}
//快捷方式设置
function Shortcuts() {
    var url = "/Home/Shortcuts";
    openDialog(url, "Shortcut", "快捷方式设置", 500, 300, function (iframe) {
        top.frames[iframe].AcceptClick()
    });
}
//页面关闭事件
function PageClose() {
    var n = window.event.screenX - window.screenLeft;
    var b = n > document.documentElement.scrollWidth - 20;
    if (b && window.event.clientY < 0 || window.event.altKey) {
        window.location.href = "/Login/OutLogin";
    }
}
//技术支持
function Support() {
    Dialog("/Home/SupportPage", "Support", "7 × 24 技术支持服务", 600, 275);
}
//关于我们
function About() {
    alertDialog("信息化系统快速开发框架LR-FDMS<br>版本4.1<br>信息技术有限公司<br>保留所有权利", 0);
}
//个性化皮肤设置
function SkinIndex() {
    Dialog("/Home/SkinIndex", "SkinIndex", "个性化设置", 580, 350);
}