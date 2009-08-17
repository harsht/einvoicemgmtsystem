<%@ Control Language="C#" AutoEventWireup="true" Inherits="BugNET.UserControls.Banner" Codebehind="Banner.ascx.cs" %>
<%@ Register Src="tabmenu.ascx" TagName="tabmenu" TagPrefix="uc1" %>
<div id="dashboard">
    <asp:LoginView ID="LoginView1"   runat="server">
        <LoggedInTemplate>
            <span style="padding-left:7px"> 
                <asp:LinkButton ID="Profile" runat="server"  OnClick="Profile_Click" CausesValidation="false">
                    <asp:Image ID="EditProfile" runat="server" CssClass="icon" style="padding-right:2px;" ImageUrl="~/images/user.gif" />
                    <asp:LoginName ID="LoginName1" FormatString="{0}" runat="server" />
                </asp:LinkButton> |
                <asp:LoginStatus ID="LoginStatus1" LogoutPageUrl="~/Default.aspx" LogoutAction="Redirect" runat="server" />
            </span>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <span style="padding-left:7px"> 
                <asp:hyperlink id="lnkRegister" NavigateUrl="~/Register.aspx" runat="server" text="Register"></asp:hyperlink> |
                <asp:LoginStatus ID="LoginStatus1" runat="server" />
            </span>
        </AnonymousTemplate>
    </asp:LoginView>
	<p id="search">
	    <asp:textbox id="txtIssueId" Height="12" Width="40" runat="Server"  /> <asp:linkbutton id="btnSearch" OnClick="btnSearch_Click" CausesValidation="false"  runat="server" text="Find" /> |
	</p>
	<p id="help">
	    <a target="_blank" href="http://bugnetproject.com/Documentation/tabid/57/topic/User%20Guide/Default.aspx">Help</a>
	</p>
</div>

<div id="header">
  <asp:HyperLink ID="lnkLogo" runat="server" NavigateUrl="~/Default.aspx"><asp:image CssClass="logo" runat="server" SkinID="Logo" id="Logo" /></asp:HyperLink>
   <div id="tabsB">
      <uc1:tabmenu ID="Tabmenu1" runat="server" />
   </div>
 </div>

<asp:panel id="pnlHeaderNav" CssClass="header-nav" runat="server">
   <span style="font-size:85%;padding-top:2px;padding-right:3px;">Project:</span>	
	<asp:dropdownlist CssClass="header-nav-ddl" id="ddlProject" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" runat="server">
	    <asp:ListItem Text="BugNET" Value="BugNET"></asp:ListItem>
	</asp:dropdownlist>	
</asp:panel>
<div class="breadcrumb">
 <asp:UpdateProgress ID="progress1"  runat="server">
    <ProgressTemplate>
        <div class="progress">
            <asp:image ID="Image1" runat="Server" CssClass="icon" ImageUrl="~/images/indicator.gif" /> Please Wait...
        </div>
    </ProgressTemplate>
  </asp:UpdateProgress>
 <asp:SiteMapPath ID="SiteMapPath1" PathSeparator=" > "  runat="server" /> 
</div>