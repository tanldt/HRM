<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ColumnList.ascx.cs" Inherits="GridSort.ColumnList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" debug="True"%>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD width="32%">
			<asp:Label id="lblPageRows" runat="server" CssClass="label">Page rows</asp:Label>
			<asp:TextBox id="tbPageRows" onkeypress="PageRow_Enter(this)" Width="40px" runat="server" MaxLength="4"
				CssClass="input">50</asp:TextBox>
			<asp:Label id="lblTotalRows" runat="server" CssClass="label"> Total</asp:Label>
			<asp:Label id="lblTotalRow" runat="server" CssClass="label"></asp:Label></TD>
		<TD align="left" width="*">
			<asp:CheckBox id="chkMultiSort" onclick="MultiSort_CheckChanged()" runat="server" Text="Multi Sort"
				CssClass="label" Visible="False"></asp:CheckBox>
			<asp:CheckBox id="chk_AdvMultiSort" onclick="AdMultiSort_CheckChanged()" runat="server" Text="Adv. Sort"
				CssClass="label" Visible="False"></asp:CheckBox></TD>
		<TD id="tdColumns" width="40%" nowrap>
			<asp:DropDownList onkeypress="Sort_Enter()" id="DropDownList1" Width="90px" runat="server" CssClass="comboSort"
				Visible="False"></asp:DropDownList>
			<asp:DropDownList onkeypress="Sort_Enter()" id="DropDownList2" Width="90px" runat="server" Height="14px"
				CssClass="comboSort" Visible="False"></asp:DropDownList>
			<asp:DropDownList onkeypress="Sort_Enter()" id="DropDownList3" Width="90px" runat="server" Height="14px"
				CssClass="comboSort" Visible="False"></asp:DropDownList></TD>
	</TR>
</TABLE>
<input id="btnSort" name="btnSort" type="button" value="Sort" runat="server" style="DISPLAY:none">
<input id="btnPRChanged" name="btnPRChanged" type="button" value="PRChanged" runat="server"
	style="DISPLAY:none">
<script language="javascript">
	function MultiSort_CheckChanged()
	{
		
		var obj = document.getElementById('_ctl0_uctrlColumns_chkMultiSort');
		if(obj != null)
		{
			if(obj.checked)
			{
				
				//document.getElementById('_ctl0_uctrlColumns_chk_AdvMultiSort').checked = false;
				document.getElementById('tdColumns').style.display = "none";
			}
		}
	}
	function AdMultiSort_CheckChanged()
	{
		var obj = document.getElementById('_ctl0_uctrlColumns_chk_AdvMultiSort');
		if(obj != null)
		{
			if(obj.checked)
			{
				document.getElementById('_ctl0_uctrlColumns_chkMultiSort').checked = false;
				document.getElementById('tdColumns').style.display = "";
			}
			else
				document.getElementById('tdColumns').style.display = "none";
		}
	}
	function Sort_Enter()
	{
		if(window.event.keyCode == 13){
			var obj = document.getElementById('_ctl0_uctrlColumns_btnSort');
			if(obj != null)
			{
				obj.click();
			}
		}
	}
	function PageRow_Enter(field)
	{
		var str = field.value;
		//checkInteger(field, 9999);
		if (!checkInt(field, 9999))
			return;

		field.value = str;
			
		if(window.event.keyCode == 13){
			var obj = document.getElementById('_ctl0_uctrlColumns_btnPRChanged');
			if(obj != null)
			{
				obj.click();
			}
		}
	}
	
</script>
<script language="javascript">
	var obj = document.getElementById('_ctl0_uctrlColumns_chk_AdvMultiSort');
	if(obj != null)
	{
		var obj1 = document.getElementById('tdColumns');
		if(obj.checked)
		{	
			if(obj1 != null)
				obj1.style.display = "";
		}
		else
		{
			if(obj1 != null)
				obj1.style.display = "none";
		}
	}
</script>
