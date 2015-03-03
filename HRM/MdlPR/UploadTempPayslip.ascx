<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UploadTempPayslip.ascx.cs" Inherits="MdlPR.UploadTempPayslip" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%">
	<TR>
		<TD align="center" colSpan="2"><asp:label id="lblErr" ForeColor="Red" Width="221px" runat="server"></asp:label></TD>
	</TR>
	<tr>
		<td><asp:label id="Label1" CssClass="label" runat="server">File Upload</asp:label></td>
		<td><INPUT id="txtFile" style="HEIGHT: 22px" type="file" size="41" name="inputFile" runat="server"></td>
	</tr>
	<TR>
		<TD colspan="2" align="center">
			<asp:button id="cmdUpload" runat="server" CssClass="btnAddnew" Text="Upload"></asp:button>&nbsp;
			<asp:button id="cmdDelete" runat="server" CssClass="btnAddnew" Text="Delete file"></asp:button>&nbsp;
		</TD>
	</TR>
	<TR>
		<TD colspan="2" align="center">
			<asp:datagrid id="dtgList" runat="server" CssClass="grid" AutoGenerateColumns="False" AllowPaging="false"
				BackColor="White" BorderColor="#3366CC" BorderWidth="1pt" CellPadding="0">
				<AlternatingItemStyle CssClass="gridAlter"></AlternatingItemStyle>
				<ItemStyle CssClass="gridItem"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="gridHeader"></HeaderStyle>
				<FooterStyle HorizontalAlign="Center" CssClass="gridFooter"></FooterStyle>
				<Columns>
					<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="FileName" HeaderText="FileName"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="&lt;b&gt;Seq&lt;/b&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Container.ItemIndex + 1%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="File Name">
						<ItemTemplate>
							<a href='Upload/TemplateReport/PIT/PaySlip/<%# DataBinder.Eval(Container, "DataItem.FileName") %>' target=_blank>
								<%# DataBinder.Eval(Container, "DataItem.FileName") %>
							</a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Delete">
						<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<HeaderTemplate>
							<asp:CheckBox id="chkSelectAll" onclick="CheckAll('_ctl0_dtgList__ctl1_chkSelectAll','_ctl0_dtgList',2,1,'chkSelect')"
								runat="server" CssClass="gridFooter"></asp:CheckBox>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox id="chkSelect" Runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</TD>
	</TR>
</table>
<script>
function CheckFileName(inputFileId)
{
	var obj=document.getElementById(inputFileId);
	if(!(obj.value.length>0))
	{
		alert("Please select file template (Word save as type Web Pape)!");
		return false;
	}
	else 
	{
		var arrName=obj.value.split('.');
		if(arrName.length>0)
		{
			if(arrName[arrName.length-1]=='htm') return true;
			else
			{
				alert("Please select file Work type Web Page!");
				obj.value='';
				return false;
			}
		}
		else
		{
			alert("Please select file Work type Web Page!");
			obj.value='';
			return false;	
		}
	}
}

function CheckSave()
{
	if (document.getElementById('_ctl0_grdList') == null)
	{
		alert("Please select file word and view grid before click save!");
		return false;
	}
	else
	{
		if(GridCheck('_ctl0_grdList',2,1,'chkSelect')==false)
		{
			alert("Please select at least one record!");
			return false;
		}
	}
}
function checkdelete()
{
	
	if(GridCheck('_ctl0_dtgList',2,1,'chkSelect')==false)
	{
		alert("Please select at least one record!");
		return false;
	}
	
	if(confirm(GetAlertText(iTotal,DSAlert,"0002"))==false){
		return false;
	}
	
}
function GridCheck (GridName, BeginIndex, EndIndexExt, CtlCheckName)
{
	var i;
	var count;
	var NoItemCheck;
					
	count = document.getElementById(GridName).rows.length;		
	NoItemCheck = true;		
	if (count >1 )
	{
		for (i = BeginIndex; i <document.getElementById(GridName).rows.length + EndIndexExt ; i++)
		{	if (document.getElementById(GridName + "__ctl" + i + "_" + CtlCheckName).checked == true)
			{	
				NoItemCheck = false;
				break;
			}
		}				
	}
	
	if (NoItemCheck)
	{
		return false;
	}
	else 
		return true;
}
function checkAll(obj)
{
	value = obj.checked;					
	for (j=2; j<_ctl0_grdList.rows.length + 1; j++)
	{
		document.getElementById("_ctl0_grdList" + "__ctl" + j + "_" + "chkSelect").checked = value;
	}
}

</script>
