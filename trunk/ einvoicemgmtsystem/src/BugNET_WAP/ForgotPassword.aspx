<%@ Page Language="C#" MasterPageFile="~/Shared/FullWidth.master" AutoEventWireup="true" Inherits="BugNET.ForgotPassword" Title="Forgot Password" Codebehind="ForgotPassword.aspx.cs" meta:resourcekey="Page" %>
<%@ Register TagPrefix="cc2" Namespace="Clearscreen.SharpHIP" Assembly="Clearscreen.SharpHIP" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <table cellpadding="0" border="0" style="width:400px;">
	    <tr>
		    <td align="left" colspan="2" style="font-size:18px;font-weight:bold;height:50px;">
                <asp:Label ID="lblTitle" runat="server" Text="Forgot Your Password?" meta:resourcekey="lblTitle" ></asp:Label>
		    </td>
	    </tr>
    	<tr><td colspan="2"><BN:Message ID="Message1" runat="server" visible="False"  /> </td></tr>
	    <tr>
		    <td align="center" colspan="2" style="height:35px;">
                <asp:Label ID="lblInstructions" runat="server" Text="Enter your Username to receive your password." meta:resourcekey="lblInstructions" />
            </td>
	    </tr>
	    <tr>
		    <td align="right"><asp:Label id="lblUserName" AssociatedControlID="UserName" runat='server' 
                    Text="<%$ Resources:CommonTerms, Username %>">Username:</asp:Label></td>
		    <td><asp:TextBox ID="UserName" runat="server" Width="160px" />
		    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                    ControlToValidate="UserName" ErrorMessage="Username is required" 
                    Display="Dynamic" Text="<%$ Resources:CommonTerms, UsernameRequired.ErrorMessage %>">*</asp:RequiredFieldValidator></td>
	    </tr>
	    <tr>
	       <td colspan="2">
	       <br />
	       <cc2:hipcontrol id="CapchaTest" runat="server" 
                   TrustAuthenticatedUsers="False" AutoRedirect="False" 
                JavascriptURLDetection="False" ValidationMode="ViewState" Width="400px" 
                   BackgroundColor="White" BackgroundPattern="Wave" 
                   BackgroundPatternColor="LightGray" CodeLength="6" 
                   ControlMessage="Enter the code you see"  
                   FontSize="15" ImageBorderColor="Black" ImageBorderWidth="0" ImageHeight="36" 
                   ImageWidth="138" meta:resourcekey="CapchaTest" 
                   PersistDotTextComments="True" RandomCodeLength="True" TextColor="Black" 
                   TextPattern="Weave" TextPatternColor="DarkGray" ValidationIgnoreCase="False" 
                   ValidationTimeout="300">
                
                </cc2:hipcontrol></td>
	    </tr>
	    <tr>
		    <td align="right" colspan="2"> <asp:Button ID="submit" OnClick="Submit_Click" 
                    Text="<%$ Resources:CommonTerms, Submit %>" runat="Server" /></td>
	    </tr>
	    
    </table>
</asp:Content>

