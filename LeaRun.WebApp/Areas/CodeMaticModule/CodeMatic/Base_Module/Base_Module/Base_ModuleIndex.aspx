<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="~/UserControl/LoadButton.ascx" TagPrefix="uc1" TagName="LoadButton" %>

<!DOCTYPE html>

<html>
<head >
    <meta name="viewport" content="width=device-width" />
    <title>模块设置表列表</title>
    <!--框架必需start-->
    <link href="/Content/Styles/style.css" rel="stylesheet" />
    <script src="/Content/Scripts/jquery-1.8.2.min.js"></script>
    <script src="/Content/Scripts/framework.js"></script>
    <!--框架必需end-->
    <!--表格组件start-->
    <script src="/Content/Scripts/PqGrid/jquery-ui.min.js"></script>
    <script src="/Content/Scripts/PqGrid/pqgrid.min.js" charset="GBK"></script>
    <link href="/Content/Scripts/PqGrid/pqgrid.min.css" rel="stylesheet" />
    <!--表格组件end-->
    <script type="text/javascript">
        $(function () {
            GetGrid();
        });
        //加载模块设置表列表
        function GetGrid() {
            //url：请求地址
            var url = "/CodeMaticModule/Base_Module/Base_ModuleGrid?Parm_Key_Value=";
            //colM：表头名称
            var colM = [

            ];
            //sort：要显示字段,数组对应
            var sort = [

            ];
            PQLoadGridNoPage("#grid_paging", url, colM, sort)
            $("#grid_paging").pqGrid({ topVisible:false });
            pqGridResize("#grid_paging", -83);
        }
        //刷新
        function windowload() {
            $("#grid_paging").pqGrid("refreshDataAndView");
        }
    </script>
</head>
<body>
</body>
</html>
