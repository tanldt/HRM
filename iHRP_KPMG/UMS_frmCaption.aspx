<%@ Page language="c#" Codebehind="UMS_frmCaption.aspx.cs" AutoEventWireup="false" Inherits="iHRPCore.UMS_frmCaption" ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UMS_frmCaption</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
		A { COLOR: #c00000; TEXT-DECORATION: none }
		A:link { COLOR: #000080 }
		A:visited { COLOR: #000080 }
		A:hover { FONT-WEIGHT: normal; TEXT-TRANSFORM: none; COLOR: #c00000; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal; TEXT-DECORATION: none }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form name="Form1" id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 103; POSITION: absolute; TOP: 8px" cellSpacing="1" cellPadding="1"
				width="300" border="0">
				<TR>
					<TD align="center"></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:label runat="server" Font-Names="Arial" Font-Size="12pt" Font-Bold="True" Height="14px"
							ForeColor="Gray" id="Label1">TRANSLATE MULTILANGUAGE FOR FORMS</asp:label></TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:LinkButton id="LinkButton1" runat="server" Font-Names="Arial" Font-Size="8pt">LinkButton</asp:LinkButton></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:datagrid id="dtgDish" runat="server" AllowPaging="False" PageSize="20" PagerStyle-Mode="NumericPages"
							PagerStyle-Font-Name="arial" PagerStyle-Font-Size="8pt" AllowSorting="True" ShowFooter="True"
							HorizontalAlign="Center" CellPadding="2" AutoGenerateColumns="False" BorderColor="#999999"
							Width="500px" Font-Names="Arial" DataKeyField="AutoID">
							<AlternatingItemStyle Font-Size="8pt" Font-Names="Arial" BackColor="#F6F6F7"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"
								BackColor="#D3D3D7"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Save" CancelText="Cancel" EditText="Edit">
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
								</asp:EditCommandColumn>
								<asp:BoundColumn Visible="False" DataField="FormID" HeaderText="Form ID">
									<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Width="0px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left" Width="50px"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Form ID">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" Width="450px"></ItemStyle>
									<ItemTemplate>
										<asp:label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FormID") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Control ID">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
									<ItemTemplate>
										<asp:label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ControlID") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ColumnIndex">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
									<ItemTemplate>
										<asp:label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ColumnIndex") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Caption VietNam">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
									<ItemTemplate>
										<asp:label id="lblCapVN" Width=200 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CaptionVNCtl") %>'>
										</asp:label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtCapVN Height=20 style="TEXT-ALIGN:Center" runat="server" Font-Name="Arial" Font-Size="8pt" MaxLength=200 Text='<%# DataBinder.Eval(Container, "DataItem.CaptionVNCtl")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Caption English">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial"></ItemStyle>
									<ItemTemplate>
										<asp:label id="lblCapEN" Width=200 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CaptionENCtl") %>'>
										</asp:label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtCapEN" Height=20 style="TEXT-ALIGN:Center" runat="server" Font-Name="Arial" Font-Size="8pt" MaxLength=200 Text='<%# DataBinder.Eval(Container, "DataItem.CaptionENCtl")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Parent">
									<HeaderStyle Width="450px"></HeaderStyle>
									<ItemStyle Font-Size="8pt" Font-Names="Arial" Width="450px"></ItemStyle>
									<ItemTemplate>
										<asp:label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Parent") %>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn Text="Delete" CommandName="Delete">
									<ItemStyle Font-Size="8pt" Font-Names="Arial" Width="50px"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle Font-Size="8pt" Font-Names="arial" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
			<input name="scrollLeft" runat="server" id="scrollLeft" type="hidden" value="0">
			<input name="scrollTop" runat="server" id="scrollTop" type="hidden" value="0">
			<script language="javascript">
  function sstchur_SmartScroller_GetCoords()
  {
    var scrollX, scrollY;
    if (document.all)
    {
      if (!document.documentElement.scrollLeft)
        scrollX = document.body.scrollLeft;
      else
        scrollX = document.documentElement.scrollLeft;

      if (!document.documentElement.scrollTop)
        scrollY = document.body.scrollTop;
      else
        scrollY = document.documentElement.scrollTop;
    }
    else
    {
      scrollX = window.pageXOffset;
      scrollY = window.pageYOffset;
    }
    document.forms["Form1"].scrollLeft.value = scrollX;
    document.forms["Form1"].scrollTop.value = scrollY;
  }


  function sstchur_SmartScroller_Scroll()
  {
    var x = document.forms["Form1"].scrollLeft.value;
    var y = document.forms["Form1"].scrollTop.value;
    window.scrollTo(x, y);
  }  
  window.onload = sstchur_SmartScroller_Scroll;
  window.onscroll = sstchur_SmartScroller_GetCoords;
  window.onclick = sstchur_SmartScroller_GetCoords;
  window.onkeypress = sstchur_SmartScroller_GetCoords;
			</script>
		</form>
	</body>
</HTML>
