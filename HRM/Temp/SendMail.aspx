<%@ Page Language="c#" codebehind="SendMail.aspx.cs" autoeventwireup="false" Inherits="filemanagement.SendMail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>Search</title> 
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" >
    <meta content="C#" name="CODE_LANGUAGE" >
    <meta content="JavaScript" name="vs_defaultClientScript" >
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" >
    <link href="main.css" type="text/css" rel="stylesheet" >
  </HEAD>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <div align="center">
            <!-- Header -->
            <!-- Menu Bar -->
            <div class="MainContent" style="WIDTH: 800px; HEIGHT: 420px">
                <fieldset style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; MARGIN: 5px; PADDING-TOP: 5px">
                    <legend>Send Email</legend>
                    <table cellspacing="0" cellpadding="1" border="0">
                        <colgroup>
                            <col width="150" >
                            <col width="500" >
                        </colgroup>
                        <tbody>
                            <tr>
                                <td class="caption">
                                    From</td>
                                <td>
                                    <asp:TextBox id="TextBoxFrom" runat="server" VCARD_NAME="vCard.Email" width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="caption">
                                    To</td>
                                <td>
                                    <asp:TextBox id="TextBoxTo" runat="server" width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="caption">
                                    CC</td>
                                <td>
                                    <asp:TextBox id="TextBoxCC" runat="server" width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="caption">
                                    Subject</td>
                                <td>
                                    <asp:TextBox id="TextBoxSubject" runat="server" width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <!-- Attachment-->
                                <td class="caption">
                                    Attachment</td>
                                <td>
                                    <asp:Label id="LabelAttachment" runat="server"></asp:Label></td>
                            </tr>
                            <!-- body -->
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox id="TextBoxBody" runat="server" Height="220px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <div style="WIDTH: 700px; HEIGHT: 30px; TEXT-ALIGN: right">
                    <input id="Submit1" style="WIDTH: 71px; HEIGHT: 24px" type="submit" size="26" value="Submit" name="Submit1" runat="server" >&nbsp; 
                    <input style="WIDTH: 71px; HEIGHT: 24px" type="reset" size="27" value="Reset" >
                </div>
                <!-- Message Line -->
                <div style="HEIGHT: 20px"><asp:Label id="LabelMsg" runat="server" cssclass="error"></asp:Label>
                </div>
            </div>
            <!-- Footer -->
            <div>&nbsp;
            </div>
        </div>
    </form>
</body>
</HTML>
