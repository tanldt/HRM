<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DateCOMM.ascx.cs" Inherits="iHRPCore.Component.DateCOMM.DateCOMM" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:textbox id="txtDate" Width="96px" runat="server" MaxLength="10" BorderStyle="Groove" CssClass="Textbox"></asp:textbox>&nbsp;<A ID="<%=this.UniqueID%>_img" onclick="javascript:popUpCalendar(this,<%= this.txtDate.ClientID %>, 'dd/mm/yyyy');"><IMG style="MARGIN-BOTTOM: -3px; MARGIN-LEFT: 0px" src='<%= Request.ApplicationPath + "/Component/DateCOMM/calendar.gif"%>'></A>