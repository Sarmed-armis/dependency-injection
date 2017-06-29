<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.Views.WebForm1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>this is report view </title>
</head>
<body>

   <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote">
      
<ServerReport ReportPath="" ReportServerUrl="Employee/GetReportpdf" />
</rsweb:ReportViewer>
</body>
</html>
