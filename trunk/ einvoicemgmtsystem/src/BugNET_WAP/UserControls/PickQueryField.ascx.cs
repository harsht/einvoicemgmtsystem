namespace BugNET.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugNET.BusinessLogicLayer;

	/// <summary>
    ///		This user control displays the query clause used in the QueryDetail.aspx page.
    /// 
	/// </summary>
	public partial class PickQueryField : System.Web.UI.UserControl
	{
        //add an instance field to store whether this is a custom field query.
        protected bool isCustomField;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

        /// <summary>
        /// The ProjectId property is used to retrieve the proper status, milestone,
        /// and priority values for the current project.
        /// </summary>
        /// <value>The project id.</value>
		public int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return 0;
				else
					return (int)ViewState["ProjectId"];
			}
			set {ViewState["ProjectId"] = value; }
		}

        /// <summary>
        /// When the user changes the selected field type, show the corresponding list
        /// of possible values.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void dropFieldSelectedIndexChanged(Object s, EventArgs e) 
		{
			dropValue.Items.Clear();
			switch (dropField.SelectedValue) 
			{
                case "IssueId":
                    dropValue.Visible = false;
                    txtValue.Visible = true;
                    break;
                case "IssueTitle":
                    dropValue.Visible = false;
                    txtValue.Visible = true;
                    break;
				case "IssuePriorityId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Priority.GetPrioritiesByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueMilestoneId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Milestone.GetMilestoneByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueCategoryId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					CategoryTree objCats = new CategoryTree();
					dropValue.DataSource = objCats.GetCategoryTreeByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueStatusId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Status.GetStatusByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueAssignedId" :
					dropValue.Visible = true;
					txtValue.Visible = false;                  
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
				case "IssueOwnerId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
				case "IssueCreatorId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
                case "DateCreated":
                    dropValue.Visible = false;
                    txtValue.Visible = true;
                    break;
                case "CustomFieldName":
                    dropValue.Visible = false;
                    txtValue.Visible = true;  //show the text value field. Not needed.
                    //dropValue.Visible = true;
                    //dropField.DataSource = null;
                    if (CustomField.GetCustomFieldsByProjectId(ProjectId).Count > 0 )
                    { 
                        dropField.DataSource = CustomField.GetCustomFieldsByProjectId(ProjectId);
                        dropField.DataTextField = "Name";
                        dropField.DataValueField = "Name";
                        dropField.DataBind();// bind to the new datasource.
                        dropField.Items.Add("--Reset Fields--");
                        dropField.Items.Insert(0, new ListItem("-- Select Custom Field --","0"));
                        isCustomField = true;
                        txtIsCustomQuery.Text = "true";
                    } 
                    break;
				default :
                    if (dropField.SelectedItem.Text.Equals("-- Select Custom Field --".ToString()))
                        return;
                    // The user decides to reset the fields
                    if (dropField.SelectedItem.Text.Equals("--Reset Fields--".ToString()))
                    {
                        dropField.DataSource = null;
                        dropField.DataSource = RequiredField.GetRequiredFields();
                        dropField.DataTextField = "Name";
                        dropField.DataValueField = "Value";
                        dropField.DataBind();
                        txtIsCustomQuery.Text = "false";
                        isCustomField = false;
                    }
                    //RW Once the list is populated with any varying type of name,
                    //we just default to this case, because we know it is not a standard field.	
                    else
                    {
                        //check the type of this custom field and load the appropriate values.
                        CustomField cf = CustomField.GetCustomFieldsByProjectId(ProjectId).Find(delegate(CustomField  c) { return c.Name == dropField.SelectedValue; });
                        if (cf == null)
                            return;

                        if (cf.FieldType == CustomField.CustomFieldType.DropDownList)
                        {
                            txtValue.Visible = false;
                            dropValue.Visible = true;
                            dropValue.DataSource = CustomFieldSelection.GetCustomFieldsSelectionsByCustomFieldId(cf.Id);
                            dropValue.DataTextField = "Name";
                            dropValue.DataValueField = "Value";
                        }
                        else
                        {
                            dropValue.Visible = false;
                            txtValue.Visible = true;
                        }
                    }
					break;
			}

			dropValue.DataBind();
		}

        /// <summary>
        /// This property represents the AND, OR, AND NOT, OR NOT values. Notice
        /// that we check whether the posted value actually exists in the drop down list.
        ///  We do this to prevent SQL Injection Attacks.
        /// </summary>
        /// <value>The boolean operator.</value>
		public string BooleanOperator 
		{
			get 
			{
				if (dropBooleanOperator.Items.FindByValue(dropBooleanOperator.SelectedValue) == null)
					throw new Exception( "Invalid Boolean Operator" );

				return dropBooleanOperator.SelectedValue;
			}
		}

        /// <summary>
        /// This property represents the name of the field. Notice
        /// that we check whether the posted value actually exists in the drop down list.
        /// We do this to prevent SQL Injection Attacks.
        /// </summary>
        /// <value>The name of the field.</value>
		public string FieldName 
		{
			get 
			{
				if (dropField.Items.FindByValue(dropField.SelectedValue) == null)
					throw new Exception( "Invalid Field Name" );

				return dropField.SelectedValue;
			}
		}

        /// <summary>
        /// This property represents the type of comparison. Notice
        /// that we check whether the posted value actually exists in the drop down list.
        /// We do this to prevent SQL Injection Attacks.
        /// </summary>
        /// <value>The comparison operator.</value>
		public string ComparisonOperator 
		{
			get 
			{
				if (dropComparisonOperator.Items.FindByValue(dropComparisonOperator.SelectedValue) == null)
					throw new Exception( "Invalid Comparison Operator" );

				return dropComparisonOperator.SelectedValue;
			}
		}

        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <value>The field value.</value>
		public string FieldValue 
		{
			get 
			{

				switch (dropField.SelectedValue) 
				{
					case "IssueCategoryId":
					case "IssuePriorityId":
					case "IssueStatusId":
					case "IssueMilestoneId":
					case "IssueAssignedId":
					case "IssueOwnerId":
					case "IssueCreatorId":
						return dropValue.SelectedValue;
						break;            
					default:
                        if (isCustomField)
                        {
                            CustomField cf = CustomField.GetCustomFieldsByProjectId(ProjectId).Find(delegate(CustomField  c) { return c.Name == dropField.SelectedValue; });
                            if (cf.FieldType == CustomField.CustomFieldType.DropDownList)
                                return dropValue.SelectedValue;
                        }
                        return txtValue.Text;
                        break;

				}

			}
		}



        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
		public SqlDbType DataType 
		{
			get 
			{

				switch (dropField.SelectedValue) 
				{
					case "IssueId":
					case "IssueCategoryId":
					case "IssuePriorityId":
					case "IssueStatusId":
					case "IssueMilestoneId":
					case "IssueAssignedId":
					case "IssueOwnerId":
					case "IssueCreatorId":
						return SqlDbType.Int;
						break;
					case "DateCreated":
						return SqlDbType.DateTime;
						break;
					default:
						return SqlDbType.NVarChar;
						break;
				}

			}
		}

        /// <summary>
        /// This property contains represents the SQL clause used when building the query.
        /// </summary>
        /// <value>The query clause.</value>
		public QueryClause QueryClause 
		{
			get 
			{
				if (dropField.SelectedValue == "0")
					return null;

                return new QueryClause(BooleanOperator, FieldName, ComparisonOperator, FieldValue, DataType, CustomFieldQuery);
			}
		}


        /// <summary>
        /// This method clears the list of field values.
        /// </summary>
		public void Clear() 
		{
			dropBooleanOperator.SelectedIndex = -1;
			dropField.SelectedIndex = -1;
			dropComparisonOperator.SelectedIndex = -1;
			dropValue.Items.Clear();
			txtValue.Text = String.Empty;
			dropValue.Visible = true;
			txtValue.Visible = false;
		}

       


        //RW identify whether each control is a custom field builder.


        /// <summary>
        /// identify whether each control is a custom field builder.
        /// </summary>
        /// <value><c>true</c> if [custom field query]; otherwise, <c>false</c>.</value>
        public bool CustomFieldQuery
        {

            get
            {

                if (txtIsCustomQuery.Text.Equals("true"))
                    isCustomField = true;
                else
                    isCustomField = false;

                return isCustomField;
            }
           set { isCustomField = value; }
        }


	
	}
}
