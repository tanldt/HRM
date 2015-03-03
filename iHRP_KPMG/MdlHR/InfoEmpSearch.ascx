<%@ Control Language="c#" AutoEventWireup="false" Codebehind="InfoEmpSearch.ascx.cs" Inherits="iHRPCore.MdlHR.InfoEmpSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="99%" border="0">
	<tr>
		<td width="10%"><asp:label id="Label3" Width="100%" CssClass="label" runat="server">Criteria</asp:label></td>
		<td>
			<%if (strTypeInfo == "optActive"){%>
			<INPUT id="optActive" type="radio" CHECKED value="Active" name="optActive" runat="server"><asp:label id="lblActive" CssClass="label" runat="server">Active</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
			&nbsp; <INPUT id="optResign" type="radio" value="Active" name="optActive" runat="server"><asp:label id="lblResign" CssClass="label" runat="server">Terminal</asp:label>
			<%} else if (strTypeInfo == "optGender"){%>
			<INPUT id="optMale" type="radio" CHECKED value="Gender" name="optGender" runat="server"><asp:label id="lblMale" CssClass="label" runat="server">Male</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
			&nbsp; <INPUT id="optFemale" type="radio" value="Gender" name="optGender" runat="server"><asp:label id="lblFemale" CssClass="label" runat="server">Female</asp:label>
			<%} else if (strTypeInfo == "txtDate"){%>
			<asp:textbox id="txtDate" onblur="CheckDate(this)" Width="80px" CssClass="input" runat="server"
				MaxLength="10"></asp:textbox><asp:imagebutton id="cmdCal" runat="server" ImageUrl="../Images/cal.gif"></asp:imagebutton>
			<%} else if (strTypeInfo == "LS"){%>
			<asp:listbox id="lstList" Width="300" CssClass="ListBox" runat="server" Height="130px" SelectionMode="Multiple"></asp:listbox>
			<%} else{%>
			<asp:textbox id="txtInfo" runat="server" CssClass="input" Width="300" MaxLength="100"></asp:textbox>
			<%}%>
		</td>
	</tr>
	<TR>
		<TD width="10%"></TD>
		<TD align="left">
			<asp:linkbutton id="btnSelect" accessKey="S" CssClass="btn" runat="server" ToolTip="Alt + S">Select</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:linkbutton id="btnClose" accessKey="C" CssClass="btn" runat="server" ToolTip="Alt + C">Close</asp:linkbutton></TD>
	</TR>
</TABLE>
<script language="javascript">
	document.getElementById("lblTitle").value = "";
</script>
<script language="javascript">
	function SelectItems()
	{
		var value = '<%=strTypeInfo%>';
		var strColumn = '<%=strColumnID%>';
		var Result1 = "";
		var Result2 = "";
		var check = 0;
		if (value == "optActive")
		{
			if (document.getElementById("_ctl0_optActive").checked)
			{
				Result1 = document.getElementById("_ctl0_lblActive").innerHTML;
				Result2 = "1"; 
			}
			else
			{
				Result1 = document.getElementById("_ctl0_lblResign").innerHTML;
				Result2 = "0"; 
			}
		}
		
		else if(value == "optGender")
		{
			if (document.getElementById("_ctl0_optMale").checked)
			{
				Result1 = document.getElementById("_ctl0_lblMale").innerHTML;
				Result2 = "1"; 
			}
			else
			{
				Result1 = document.getElementById("_ctl0_lblFemale").innerHTML;
				Result2 = "0"; 
			}
		}
		else if (value == "txtDate")
		{
			Result1 = document.getElementById("_ctl0_txtDate").value;
			Result2 = "''" + ConvertMMDDYYYY(document.getElementById("_ctl0_txtDate")) + "''";
		}
		else if(value == "LS")
		{
			var TextField, ValueField;
			var all = document.getElementById("_ctl0_lstList").length;
			for(i=0; i<all; i++)
			{	
				if (document.getElementById("_ctl0_lstList").options[i].selected == true)
				{
					TextField = document.getElementById("_ctl0_lstList").options[i].text;
					ValueField = document.getElementById("_ctl0_lstList").options[i].value;
					if (trim(Result1) == "")
					{
						check = 1;
						Result1 = TextField;
						Result2 = "''" + ValueField + "''";
					}
					else
					{
						check = check + 1;
						if (check == 2)
						{
							Result1 = "(" + Result1;
							Result2 = "(" + Result2;
						}
						Result1 = Result1 + "," + TextField;
						Result2 = Result2 + "," + "''" + ValueField + "''";
					}
				};
			};
			if (check > 1)
			{
				Result1 = Result1 + ")";
				Result2 = Result2 + ")";
			}
		}
		else
		{
			Result1 = document.getElementById("_ctl0_txtInfo").value;
			Result2 = "''" + document.getElementById("_ctl0_txtInfo").value + "''";
		}
		if (trim(Result1) == "")
		{
			alert("Please input information of filter condition!");
			return false;
		}
		else
		{
			eval('txtInfo = opener.document.forms[0].all("_ctl0_txtInfo")');		
			eval('txtValue = opener.document.forms[0].all("_ctl0_txtValue")');
			eval('btnAddInfo = opener.document.forms[0].all("btnAddInfo")');
			eval('txtColumnName = opener.document.forms[0].all("_ctl0_txtColumnName")');
			eval('txtMultiSelect = opener.document.forms[0].all("_ctl0_txtMultiSelect")');
			txtInfo.value = Result1;
			txtValue.value = Result2;
			txtColumnName.value = strColumn;
			txtMultiSelect.value = check == 0?"":"In";
			btnAddInfo.focus();
			window.close();
		}
	}
</script>
