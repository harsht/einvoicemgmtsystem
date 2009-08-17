<%@ Control Language="c#" Inherits="BugNET.Administration.Projects.UserControls.ProjectCategories" Codebehind="ProjectCategories.ascx.cs" AutoEventWireup="True" %>
<%@ Register TagPrefix="it" TagName="PickCategory" Src="~/UserControls/PickCategory.ascx" %>
<%@ Register Src="~/UserControls/CategoryTreeView.ascx" TagName="CategoryTreeView" TagPrefix="it" %>

<script type="text/javascript" src="<%=ResolveUrl("~/js/lib/adapter/ext/ext-base.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/js/lib/ext-all.js")%>"></script>
<link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/js/lib/resources/css/ext-all.css")%>" />
<style type="text/css">
    #tree-div .root-node .x-tree-node-icon {
       background-image:url(../../images/plugin_disabled.gif);
    }
    #tree-div .category-node .x-tree-node-icon{
      background-image:url(../../images/plugin.gif);
    } 
</style>
<script type="text/javascript">
    var tree;
     /*seeds for the new node suffix*/
    var cseed = 0;
    Ext.BLANK_IMAGE_URL = '<%=ResolveUrl("~/js/lib/resources/images/default/s.gif")%>';
    Ext.util.CSS.swapStyleSheet('theme', "<%=ResolveUrl("~/js/lib/resources/css/xtheme-gray.css")%>");

    Ext.onReady(function(){
        // shorthand
        var Tree = Ext.tree;
       
        tree = new Tree.TreePanel('tree-div', {
            animate:true, 
            loader: new Tree.TreeLoader({dataUrl:'<%=ResolveUrl("~/UserControls/TreeHandler.ashx?id=")%><%=ProjectId %>'}),
            enableDD:true,
            containerScroll: true,
            dropConfig: {appendOnly:true}
        });

        // add a tree sorter in folder mode
        new Tree.TreeSorter(tree, {folderSort:true});
                
        // set the root node
        var root = new Tree.AsyncTreeNode({
            text: 'Root Category',
            cls: 'root-node',
            draggable:false,
            disabled:true,
            id:'0'
        });
        
        tree.loader.requestMethod = "GET";
        
        var ge = new Ext.tree.TreeEditor(tree, {
            allowBlank:false,
            blankText:'A name is required',
            selectOnFocus:true
        });
        tree.Editor=ge;
        
        tree.addListener("textchange", textChanged);
        tree.addListener("move", move);
        tree.addListener("beforeappend",beforeappend);
        tree.addListener("remove",remove);
        
        tree.setRootNode(root);

        // render the tree
        tree.render();
        root.expand(false, /*no anim*/ false);
        
        //Events

        //node removed
        function remove(tree, parentNode, node)
        {  
            if(node && node.attributes.allowDelete)
            {
                BugNET.Webservices.BugNetServices.DeleteComponent(node.id);    
            }
        }
        
        //before node append
        function beforeappend(tree,parentNode,node,index)
        {
            //check if node is new
            if(node.attributes.isNew)
            {
                //add this new node to the database and set the new id
               BugNET.Webservices.BugNetServices.AddCategory(<%=ProjectId %>,node.text,parentNode.id,function(result){
                 node.id = result;
                 node.attributes.isNew = false;
               });   
            }
        }
        
        //node text changed
        function textChanged(node, text, oldText) 
        { 
            //PageMethods.ChangeTreeNode(node.id,text,oldText);
            BugNET.Webservices.BugNetServices.ChangeTreeNode(node.id,text,oldText);
        }
        
        //node moved
        function move(tree,node,oldParent,newParent,index)
        {
            //alert("Id:" + node.id + " Old Parent: " + oldParent.id + " New Parent: " + newParent.id);
            //PageMethods.MoveNode(node.id,oldParent.id,newParent.id,index); 
            BugNET.Webservices.BugNetServices.MoveNode(node.id,oldParent.id,newParent.id,index,SucceededCallback,OnError);
        }
       
        // This is the callback function that
        // processes the Web Service return value.
        function SucceededCallback(result)
        {
           return;
        }
        
        function OnError(result)
        {
          alert("Error: " + result.get_message());
        }   
    });
   
  
    function AddCategory()
    {
        if(tree.getSelectionModel().getSelectedNode())
        {
            var text = 'Category '+(++cseed);
            var newNode = new Ext.tree.TreeNode({id: "new"+ cseed, text: text,cls:"category-node",isNew: true, leaf: false});
            tree.getSelectionModel().getSelectedNode().appendChild(newNode);
            newNode.ensureVisible(); 
            tree.Editor.triggerEdit(newNode);
        }
    }
    
    function DeleteCategory()
    {
        var selectedNode = tree.getSelectionModel().getSelectedNode();
        if(selectedNode && selectedNode.id == 0)
	    {
	        Ext.Msg.alert('Status', 'You cannot delete the root category.');
	    }
	    else
        {
            if(selectedNode.lastChild)
		    {
                Ext.Msg.alert('Status', 'You must delete all child categories of this parent first.');
            }
            else
            {
              $find('DeleteCategoryMP').show();
            }
        }  
        
    }
    
    function onOk(sender,e) 
    {
        var selectedNode = tree.getSelectionModel().getSelectedNode();       
        document.getElementById('<%=HiddenField1.ClientID %>').value = selectedNode.id;
        __doPostBack(sender,e); 
     
    }
 </script>
 <div>
    <h4>Categories</h4>
    <br />
    <asp:CustomValidator  Display="dynamic" Text="You must add at least one category." Runat="server" OnServerValidate="CategoryValidation_Validate" id="ComponentValidation" />
    <p>
        When you create an issue, you assign the issue a category. Add categories by 
	    clicking the add category button and entering the name of the category. You can add sub-categories by selecting a parent 
	    category from the tree view below then entering the child category in the textbox and clicking the add button. To delete a category select the category then click the delete button.
    </p>
     <asp:HiddenField ID="HiddenField1" runat="server" />
     <br />
     <BN:Message ID="Message1" runat="server" /> 
     <br />
     <img alt="Add Category" type="image" src="../../images/plugin_add.gif" onclick="AddCategory();" class="icon" />
     <a href="javascript:AddCategory();">Add Category</a>
     &nbsp;
     <asp:LinkButton ID="LinkButton1" runat="server" Text="Not Used" style="display:none;" ></asp:LinkButton>
     <img alt="Delete Category" type="image" src="../../images/plugin_delete.gif" onclick="DeleteCategory();" class="icon" />
     <a href="javascript:DeleteCategory();">Delete Category</a>
     <br />
     <br />
     <div id="tree-div"></div>
</div>

 <asp:Panel ID="pnlDeleteCategory" runat="server" CssClass="ModalPopup" style="display:none;">
    <asp:panel ID="pnlHeader" runat="server" CssClass="ModalHeader">Delete Category</asp:panel>	
    <div class="ModalContainer">
        Please select between the following options:
        <br />
        <br />
        <table cellspacing="10" style="margin-left:10px;text-align:left;" >
            <tr>
                <td><asp:RadioButton ID="RadioButton1" GroupName="DeleteCategory" runat="server" Height="30px" Text="&nbsp;&nbsp;Delete this category and all assigned issues." /></td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton2" GroupName="DeleteCategory" runat="server"  Height="30px" Text="&nbsp;&nbsp;Assign all issues to an existing category." />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<it:PickCategory id="DropCategory" DisplayDefault="true" Required="false" Runat="Server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton3" GroupName="DeleteCategory" runat="server" Height="30px" Text="&nbsp;&nbsp;Assign all issues to a new category. " />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="NewCategoryTextBox" runat="server"  Text=""></asp:TextBox>
                </td>
            </tr>
        </table>
        <p style="text-align: center;">
            <asp:LinkButton ID="OkButton" runat="server" OnClick="OkButton_Click"  Text="OK" />
            <asp:LinkButton ID="CancelButton" runat="server" Text="Cancel" />
        </p>
    </div> 
  </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
        TargetControlID="LinkButton1" 
        PopupControlID="pnlDeleteCategory" 
        BackgroundCssClass="modalBackground" 
        OkControlID="OkButton"
        CancelControlID="CancelButton"  
        DropShadow="false"
        BehaviorID="DeleteCategoryMP"
        PopupDragHandleControlID="pnlHeader" />
        
    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server"
        TargetControlID="NewCategoryTextBox"
        WatermarkText="Enter a new category"
        WatermarkCssClass="watermarked" />

