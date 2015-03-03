<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LeftMenu1.ascx.cs" Inherits="iHRPCore.Pagelets.LeftMenu1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="include/menuStyle.css" type="text/css" rel="stylesheet">
<ComponentArt:Menu id="Menu1" Orientation="vertical" CssClass="TopGroup" DefaultGroupCssClass="Group" DefaultItemLookId="DefaultItemLook"
	DefaultSelectedItemLookId="SelectedItemLook" DefaultChildSelectedItemLookId="ChildSelectedItemLook"
	DefaultGroupItemSpacing="1px" ImagesBaseUrl="images/" EnableViewState="False" runat="server" TopGroupItemSpacing="1px"
	ClientTarget="Auto" ScrollUpLookId="DefaultItemLook" ClientObjectId="_ctl0_Menu1" ScrollDownLookId="DefaultItemLook"
	Width="100%">
	<ItemLooks>
		<componentart:ItemLook HoverCssClass="ItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px" LabelPaddingBottom="2px"
			LeftIconUrl="spacer.gif" ExpandedCssClass="ItemExpanded" LeftIconWidth="10px" LookId="DefaultItemLook"
			CssClass="Item"></componentart:ItemLook>
		<componentart:ItemLook HoverCssClass="SelectedItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px"
			LabelPaddingBottom="2px" LeftIconUrl="spacer.gif" ExpandedCssClass="SelectedItemExpanded" LeftIconWidth="10px"
			LookId="SelectedItemLook" CssClass="SelectedItem"></componentart:ItemLook>
		<componentart:ItemLook HoverCssClass="ChildSelectedItemHover" LabelPaddingTop="2px" LabelPaddingRight="10px"
			LabelPaddingBottom="2px" ExpandedCssClass="ChildSelectedItemExpanded" LabelPaddingLeft="10px" LookId="ChildSelectedItemLook"
			CssClass="ChildSelectedItem"></componentart:ItemLook>
		<componentart:ItemLook HoverCssClass="ItemHover" LabelPaddingTop="2px" RightIconUrl="arrow.gif" LabelPaddingRight="10px"
			HoverRightIconUrl="arrow_hover.gif" LabelPaddingBottom="2px" RightIconWidth="15px" LeftIconUrl="spacer.gif"
			ExpandedCssClass="ItemExpanded" LeftIconWidth="10px" LookId="ExpandableItemLook" CssClass="Item"></componentart:ItemLook>
		<componentart:ItemLook HoverCssClass="SelectedItemHover" LabelPaddingTop="2px" RightIconUrl="arrow_hover.gif"
			LabelPaddingRight="10px" LabelPaddingBottom="2px" RightIconWidth="15px" ExpandedCssClass="SelectedItemExpanded"
			LookId="ExpandableSelectedItemLook" CssClass="SelectedItem"></componentart:ItemLook>
		<componentart:ItemLook HoverCssClass="ChildSelectedItemHover" LabelPaddingTop="2px" RightIconUrl="arrow_hover.gif"
			LabelPaddingRight="10px" LabelPaddingBottom="2px" RightIconWidth="15px" ExpandedCssClass="ChildSelectedItemExpanded"
			LookId="ExpandableChildSelectedItemLook" CssClass="ChildSelectedItem"></componentart:ItemLook>
	</ItemLooks>
</ComponentArt:Menu>
<script type="text/javascript">
    // Preload CSS-referenced images 
    var img1 = new Image();
    img1.src = '/images/item_bg.gif';
    var img2 = new Image();
    img2.src = '/images/group_bg.gif';
</script>
