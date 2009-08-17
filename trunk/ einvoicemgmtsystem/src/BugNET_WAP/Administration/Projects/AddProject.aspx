<%@ Page language="c#" Inherits="BugNET.Administration.Projects.AddProject" MasterPageFile="~/Shared/FullWidth.master" Title="Add Project" Codebehind="AddProject.aspx.cs" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <h1>New Project Wizard (<asp:Literal id="lblStepNumber" runat="Server" />)</h1>
	<div style="width:600px;padding-top:10px;">
		<asp:PlaceHolder id="plhWizardStep" runat="Server" />
		<br/>
		<br/>
		<div style="float:left">
			<asp:Button Text="Cancel"  CssClass="button" CausesValidation="false" runat="server" id="btnCancel" />
		</div>
		<div style="float:right">
			<asp:Button id="btnBack"  CssClass="button" Text="Back" CausesValidation="false" runat="Server" />
			&nbsp;
			<asp:Button id="btnNext"  CssClass="button" Text="Next" runat="Server" />
		</div>	
	</div>	



</asp:Content>
