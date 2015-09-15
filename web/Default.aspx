<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Безымянная страница</title>
    <script src="JS/jquery-1.4.2.min.js"></script>
    <script src="JS/jquery-ui-1.8.2.custom.min.js"></script>
    <script src="JS/Figures.js"></script>
    <link href="css/css.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="boardDiv" style="text-align:center;" align="center">
        <script language="javascript" type="text/javascript">
            var div = $("#boardDiv");
            var game = new Game();
            game.Init(div);
            game.New();
        </script>
    </div>
    </form>
</body>
</html>
