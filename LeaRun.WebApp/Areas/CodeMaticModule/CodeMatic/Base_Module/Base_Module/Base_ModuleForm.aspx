<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head >
    <meta name="viewport" content="width=device-width" />
    <title>模块设置表表单</title>
    <!--框架必需start-->
    <link href="/Content/Styles/style.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery-1.8.2.min.js"></script>
    <script src="/Content/Scripts/framework.js"></script>
    <!--框架必需end-->
    <!--表单验证组件start-->
    <script src="/Content/Scripts/JValidator.js"></script>
    <!--表单验证组件end-->
    <script type="text/javascript">
        $(function () {
            if (IsNullOrEmpty(GetQuery('key'))) {
                InitControl()
            }
        })
        //保存事件
        function AcceptClick() {
            if (!CheckDataValid('#form1')) {
                return false;
            }
            var postData = GetWebControls("#form1");
            getAjax("/CodeMaticModule/Base_Module/SubmitBase_ModuleForm?key=" + GetQuery('key'), postData, function (data) {
                if (data.toLocaleLowerCase() == 'true') {
                    top.jBox.tip('恭喜您，操作成功', 'success');
                    top.jBox.close();//关闭当前窗口
                    top.$('#' + TabIframeId())[0].contentWindow.windowload();//刷新父页面
                } else {
                    top.jBox.tip('很抱歉，操作失败', 'error');
                }
            });
        }
        //得到一个对象实体
        function InitControl() {
            getAjax("/CodeMaticModule/Base_Module/GetBase_ModuleForm", { key: GetQuery('key') }, function (data) {
                var data = eval("(" + data + ")");
                SetWebControls(data);
            });
        }
    </script>
</head>
<body>
    <form id="form1">
        <table border="0" cellpadding="0" cellspacing="0" class="" style="padding-top: 20px;">
        </table>
    </form>
</body>
</html>
