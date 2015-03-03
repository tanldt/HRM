<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MonthCalendar.ascx.cs" Inherits="iHRPCore.Include.MonthCalendar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table style="BORDER-COLLAPSE: collapse" borderColor="#3366cc" cellSpacing="0" cellPadding="0"
	width="350" bgColor="white" border="1">
	<tr class="gridHeader">
		<td align="center" width="50"><asp:label id="Label1" CssClass="label" runat="server" Width="100%">Mon</asp:label></td>
		<td align="center" width="50"><asp:label id="Label2" CssClass="label" runat="server" Width="100%">Tue</asp:label></td>
		<td align="center" width="50"><asp:label id="Label3" CssClass="label" runat="server" Width="100%">Wed</asp:label></td>
		<td align="center" width="50"><asp:label id="Label4" CssClass="label" runat="server" Width="100%">Thur</asp:label></td>
		<td align="center" width="50"><asp:label id="Label5" CssClass="label" runat="server" Width="100%">Fri</asp:label></td>
		<td align="center" width="50"><asp:label id="Label6" CssClass="label" runat="server" Width="100%">Sat</asp:label></td>
		<td align="center" width="50"><asp:label id="Label7" CssClass="label" runat="server" Width="100%">Sun</asp:label></td>
	</tr>
	<tr>
		<td align="center"><asp:textbox id="Textbox1" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">01</asp:textbox>
			<asp:textbox id="Textbox50" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox2" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">02</asp:textbox>
			<asp:textbox id="Textbox51" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox3" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">03</asp:textbox>
			<asp:textbox id="Textbox52" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox4" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">04</asp:textbox>
			<asp:textbox id="Textbox53" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox5" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">05</asp:textbox>
			<asp:textbox id="Textbox54" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox6" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">06</asp:textbox>
			<asp:textbox id="Textbox55" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox7" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">07</asp:textbox>
			<asp:textbox id="Textbox56" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
	</tr>
	<tr>
		<td align="center"><asp:textbox id="Textbox8" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">08</asp:textbox>
			<asp:textbox id="Textbox57" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox9" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">09</asp:textbox>
			<asp:textbox id="Textbox58" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox10" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">10</asp:textbox>
			<asp:textbox id="Textbox59" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox11" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">11</asp:textbox>
			<asp:textbox id="Textbox60" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox12" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">12</asp:textbox>
			<asp:textbox id="Textbox61" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox13" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">13</asp:textbox>
			<asp:textbox id="Textbox62" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox14" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">14</asp:textbox>
			<asp:textbox id="Textbox63" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
	</tr>
	<tr>
		<td align="center"><asp:textbox id="Textbox15" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">15</asp:textbox>
			<asp:textbox id="Textbox64" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox16" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">16</asp:textbox>
			<asp:textbox id="Textbox65" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox17" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">17</asp:textbox>
			<asp:textbox id="Textbox66" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox18" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">18</asp:textbox>
			<asp:textbox id="Textbox67" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox19" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">19</asp:textbox>
			<asp:textbox id="Textbox68" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox20" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">20</asp:textbox>
			<asp:textbox id="Textbox69" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox21" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">21</asp:textbox>
			<asp:textbox id="Textbox70" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
	</tr>
	<tr>
		<td align="center"><asp:textbox id="Textbox22" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">22</asp:textbox>
			<asp:textbox id="Textbox36" CssClass="input" style="TEXT-ALIGN:center" Width="90%" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox23" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">23</asp:textbox><asp:textbox id="Textbox37" CssClass="input" style="TEXT-ALIGN:center" Width="90%" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox24" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">24</asp:textbox>
			<asp:textbox id="Textbox38" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox25" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">25</asp:textbox>
			<asp:textbox id="Textbox39" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox26" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">26</asp:textbox>
			<asp:textbox id="Textbox40" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox27" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">27</asp:textbox>
			<asp:textbox id="Textbox41" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox28" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">28</asp:textbox>
			<asp:textbox id="Textbox42" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
	</tr>
	<tr>
		<td align="center"><asp:textbox id="Textbox29" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">29</asp:textbox>
			<asp:textbox id="Textbox43" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox30" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">30</asp:textbox>
			<asp:textbox id="Textbox44" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:textbox id="Textbox31" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" ReadOnly="True">31</asp:textbox>
			<asp:textbox id="Textbox45" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server">1</asp:textbox></td>
		<td align="center"><asp:TextBox id="Textbox32" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" Visible="False" ReadOnly="True">1</asp:TextBox>
			<asp:textbox id="Textbox46" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server"
				Visible="False">1</asp:textbox></td>
		<td align="center"><asp:TextBox id="Textbox33" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" Visible="False" ReadOnly="True">1</asp:TextBox>
			<asp:textbox id="Textbox47" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server"
				Visible="False">1</asp:textbox></td>
		<td align="center"><asp:TextBox id="Textbox34" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" Visible="False" ReadOnly="True">1</asp:TextBox>
			<asp:textbox id="Textbox48" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server"
				Visible="False">1</asp:textbox></td>
		<td align="center"><asp:TextBox id="Textbox35" CssClass="input" BorderStyle="None" style="TEXT-ALIGN:center" Width="90%"
				Runat="server" Visible="False" ReadOnly="True">1</asp:TextBox>
			<asp:textbox id="Textbox49" style="TEXT-ALIGN: center" Width="90%" CssClass="input" Runat="server"
				Visible="False">1</asp:textbox></td>
	</tr>
</table>
