<%@ Control Language="c#" Inherits="BugNET.UserControls.PickSingleUser" Codebehind="PickSingleUser.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:DropDownList id="ddlUsers" DataTextField="DisplayName" DataValueField="UserName"  runat="Server" />
<asp:RequiredFieldValidator id="reqVal" Display="dynamic" ControlToValidate="ddlUsers" Text="(required)" Runat="Server" meta:resourcekey="reqVal" />

<ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" PromptPosition="bottom" runat="server"
                TargetControlID="ddlUsers" PromptCssClass="ListSearchExtenderPrompt" >
</ajaxToolkit:ListSearchExtender>
