<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopMenu.ascx.cs" Inherits="iHRPCore.Pagelets.TopMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="include/menuStyle.css" type="text/css" rel="stylesheet">
<ComponentArt:Menu id="Menu1" 
	CssClass="TopGroup" 
	DefaultGroupCssClass="Group" 
	DefaultItemLookId="DefaultItemLook"
	DefaultSelectedItemLookId="SelectedItemLook" 
	DefaultChildSelectedItemLookId="ChildSelectedItemLook"
	DefaultGroupItemSpacing="0px" 
	ImagesBaseUrl="images/" 
	EnableViewState="False" 
	runat="server" 
	TopGroupItemSpacing="0px"
	ClientTarget="Auto" 
	ScrollUpLookId="DefaultItemLook" 
	ClientObjectId="_ctl0_Menu1" 
	ScrollDownLookId="DefaultItemLook"
	ExpandDelay="100"
    ExpandOnClick="true"
	Width="100%" Height="20px">
	<ItemLooks>
		<ComponentArt:ItemLook LookID="TopItemLook" CssClass="TopMenuItem" HoverCssClass="TopMenuItemHover" LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4" />
		<componentart:ItemLook LookId="DefaultItemLook"	CssClass="Item" HoverCssClass="ItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px" LabelPaddingBottom="2px" LeftIconUrl="spacer.gif" ExpandedCssClass="ItemExpanded" LeftIconWidth="10px"></componentart:ItemLook>
		<componentart:ItemLook LookId="SelectedItemLook" CssClass="SelectedItem" HoverCssClass="SelectedItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px" LabelPaddingBottom="2px" LeftIconUrl="spacer.gif" ExpandedCssClass="SelectedItemExpanded" LeftIconWidth="10px"></componentart:ItemLook>
		<componentart:ItemLook LookId="ChildSelectedItemLook" CssClass="ChildSelectedItem" HoverCssClass="ChildSelectedItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px"	LabelPaddingBottom="2px" ExpandedCssClass="ChildSelectedItemExpanded" LabelPaddingLeft="10px"></componentart:ItemLook>
		<componentart:ItemLook LookId="ExpandableItemLook" CssClass="Item" HoverCssClass="ItemHover" LabelPaddingTop="2px" RightIconUrl="arrow.gif" LabelPaddingRight="10px" HoverRightIconUrl="arrow_hover.gif" LabelPaddingBottom="2px" RightIconWidth="15px" LeftIconUrl="spacer.gif" ExpandedCssClass="ItemExpanded" LeftIconWidth="10px"></componentart:ItemLook>
		<componentart:ItemLook LookId="ExpandableSelectedItemLook" CssClass="SelectedItem" HoverCssClass="SelectedItemHover" LabelPaddingTop="2px" RightIconUrl="arrow_hover.gif" LabelPaddingRight="10px" LabelPaddingBottom="2px" RightIconWidth="15px" ExpandedCssClass="SelectedItemExpanded"></componentart:ItemLook>
		<componentart:ItemLook LookId="ExpandableChildSelectedItemLook" CssClass="ChildSelectedItem" HoverCssClass="ChildSelectedItemHover" LabelPaddingTop="2px" RightIconUrl="arrow_hover.gif" LabelPaddingRight="10px" LabelPaddingBottom="2px" RightIconWidth="15px" ExpandedCssClass="ChildSelectedItemExpanded"></componentart:ItemLook>
	</ItemLooks>
</ComponentArt:Menu>
<script type="text/javascript">
    // Preload CSS-referenced images 
    var img1 = new Image();
    img1.src = '/images/item_bg.gif';
    var img2 = new Image();
    img2.src = '/images/group_bg.gif';
</script>
