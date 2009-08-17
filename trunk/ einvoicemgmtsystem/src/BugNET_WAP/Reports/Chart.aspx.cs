using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugNET.BusinessLogicLayer;
using ZedGraph;
using System.Security.Cryptography;

namespace BugNET.Reports
{
	/// <summary>
	/// Summary description for Chart.
	/// </summary>
	public partial class Chart : System.Web.UI.Page
	{

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{	
				if (Request.QueryString["pid"] != null)
					ProjectId = Convert.ToInt32(Request.Params["pid"]);

				if (Request.QueryString["id"] != null)
					ChartId = Convert.ToInt32(Request.Params["id"]);
			}
			
		}
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
		int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return 0;
				else
					return (int)ViewState["ProjectId"];
			}
			set { ViewState["ProjectId"] = value; }
		}
        /// <summary>
        /// Gets or sets the chart id.
        /// </summary>
        /// <value>The chart id.</value>
		int ChartId 
		{
			get 
			{
				if (ViewState["ChartId"] == null)
					return 0;
				else
					return (int)ViewState["ChartId"];
			}
			set { ViewState["ChartId"] = value; }
		}

        /// <summary>
        /// Gets the color of the status.
        /// </summary>
        /// <param name="statusId">The status id.</param>
        /// <returns></returns>
		private Color GetStatusColor(int statusId)
		{
			switch(statusId)
			{	
				case 1:
					return Color.Red;
				case 2:
					return Color.Blue;
				case 3:
					return Color.Yellow;
				case 4:
					return Color.Purple;
				case 5:
					return Color.Green;
			}
			return Color.IndianRed;

		}


        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <returns></returns>
		public static Color GetRandomColor()
		{
			KnownColor[] colors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
			return Color.FromKnownColor(colors[GetRandomNumber(colors.Length)]);  
		}


        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
		public static int GetRandomNumber(int maxValue)
		{
			RandomNumberGenerator rng = RNGCryptoServiceProvider.Create();
			byte[] bytes = new byte[4];
			rng.GetBytes(bytes);
			int ranNum = BitConverter.ToInt32(bytes, 0);
			return Math.Abs(ranNum % maxValue);
		}

        /// <summary>
        /// Called when [render graph].
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="pane">The pane.</param>
		private void OnRenderGraph(System.Drawing.Graphics g, ZedGraph.GraphPane pane)
		{
			GraphPane myPane = pane;
			ArrayList al;
			CurveList curves;
			double total;
			TextItem text;
			TextItem text2;

			switch(ChartId)
			{

					
				case 1:
					//Issues By Status Chart

					// Set the GraphPane title
					myPane.Title = Project.GetProjectById(ProjectId).Name + " Issues By Status";
					myPane.FontSpec.IsItalic = true;
					myPane.FontSpec.Size = 24f;
					myPane.FontSpec.Family = "Times";

					// Fill the pane background with a color gradient
					myPane.PaneFill = new Fill( Color.White, Color.Beige, 45.0f );
					// No fill for the axis background
					myPane.AxisFill.Type = FillType.None;

					// Set the legend to an arbitrary location
					myPane.Legend.Position = LegendPos.Float ;
					myPane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction,AlignH.Right, AlignV.Top );
					myPane.Legend.FontSpec.Size = 12f;
					myPane.Legend.IsHStack = false;

					// Add some pie slices
					al = (ArrayList)Bug.GetBugStatusCountByProject(ProjectId);
					foreach(BugCount bc in al)
					{
						PieItem segment = myPane.AddPieSlice(bc.Count,GetStatusColor(bc.Id), Color.White, 45f, 0,bc.Name + " - " + bc.Count);
					}

					// Sum up the pie values                                                               
					curves = myPane.CurveList ;
					total = 0 ;
					for ( int x = 0 ; x <  curves.Count ; x++ )
						total += ((PieItem)curves[x]).Value ;

					// Make a text label to highlight the total value
					text = new TextItem( "Total Issues\n" + total.ToString(),0.9F, 0.40F, CoordType.PaneFraction);
					text.Location.AlignH = AlignH.Center;
					text.Location.AlignV = AlignV.Bottom;
					text.FontSpec.Border.IsVisible = false ;
					text.FontSpec.Fill = new Fill( Color.White, Color.FromArgb( 255, 100, 100 ), 45F );
					text.FontSpec.StringAlignment = StringAlignment.Center ;
					myPane.GraphItemList.Add( text );

					// Create a drop shadow for the total value text item
					text2 = new TextItem( text );
					text2.FontSpec.Fill = new Fill( Color.Black );
					text2.Location.X += 0.008f;
					text2.Location.Y += 0.01f;
					myPane.GraphItemList.Add( text2 );

					// Calculate the Axis Scale Ranges
					myPane.AxisChange( g );
					break;
				case 2:
					//Issues By Version Chart
					// Set the GraphPane title
					myPane.Title = Project.GetProjectById(ProjectId).Name + " Open Issues By Priority";
					myPane.FontSpec.IsItalic = true;
					myPane.FontSpec.Size = 24f;
					myPane.FontSpec.Family = "Times";

					// Fill the pane background with a color gradient
					myPane.PaneFill = new Fill( Color.White, Color.Beige, 45.0f );
					// No fill for the axis background
					myPane.AxisFill.Type = FillType.None;

					// Set the legend to an arbitrary location
					myPane.Legend.Position = LegendPos.Float ;
					myPane.Legend.Location = new Location( 0.95f, 0.15f, CoordType.PaneFraction,AlignH.Right, AlignV.Top );
					myPane.Legend.FontSpec.Size = 12f;
					myPane.Legend.IsHStack = false;

					// Add some pie slices
					al = (ArrayList)Bug.GetBugPriorityCountByProject(ProjectId);
					foreach(BugCount bc in al)
					{
						PieItem segment = myPane.AddPieSlice(bc.Count, GetRandomColor(), Color.White, 45f, 0, bc.Name + " - " + bc.Count);
					}

					// Sum up the pie values                                                               
					curves = myPane.CurveList ;
					total = 0 ;
					for ( int x = 0 ; x <  curves.Count ; x++ )
						total += ((PieItem)curves[x]).Value ;

					// Make a text label to highlight the total value
					text = new TextItem( "Total Issues\n" + total.ToString(),0.9F, 0.40F, CoordType.PaneFraction);
					text.Location.AlignH = AlignH.Center;
					text.Location.AlignV = AlignV.Bottom;
					text.FontSpec.Border.IsVisible = false ;
					text.FontSpec.Fill = new Fill( Color.White, Color.FromArgb( 255, 100, 100 ), 45F );
					text.FontSpec.StringAlignment = StringAlignment.Center ;
					myPane.GraphItemList.Add( text );

					// Create a drop shadow for the total value text item
					text2 = new TextItem( text );
					text2.FontSpec.Fill = new Fill( Color.Black );
					text2.Location.X += 0.008f;
					text2.Location.Y += 0.01f;
					myPane.GraphItemList.Add( text2 );

					// Calculate the Axis Scale Ranges
					myPane.AxisChange( g );
					break;
				case 3:
					//Open Issues By Version

					// Set the title and axis labels
					myPane.Title = Project.GetProjectById(ProjectId).Name + " Open Issues By Version";
					myPane.XAxis.Title = "Version";
					myPane.YAxis.Title = "# Issues";
					   
					// Enter some data values
					al = (ArrayList)Bug.GetBugVersionCountByProject(ProjectId);
					string[] labels = new string[al.Count];
					double[] y = new double[al.Count];
					int i= 0;

					foreach(BugCount bc in al)
					{
						y[i] = Convert.ToDouble(bc.Count);
						labels[i] = bc.Name;
						i+=1;				
					}
					BarItem myBar  = myPane.AddBar("",null,y, Color.Red );
			 
					// Draw the X tics between the labels instead of at the labels
					myPane.XAxis.IsTicsBetweenLabels = true;
					    
					// Set the XAxis to Text type
					myPane.XAxis.Type = AxisType.Text;

					// Set the XAxis labels
					myPane.XAxis.TextLabels = labels;
					myPane.XAxis.ScaleFontSpec.Size = 10.0F ;
					   
					// Make the bars a sorted overlay type so that they are drawn on top of eachother
					// (without summing), and each stack is sorted so the shorter bars are in front
					// of the taller bars
					myPane.BarType = BarType.SortedOverlay;
				   
						// Fill the axis background with a color gradient
					myPane.AxisFill = new Fill( Color.White, Color.LightGoldenrodYellow, 45.0F );
					  
					// Calculate the Axis Scale Ranges
					//Graphics g = this.CreateGraphics();
					myPane.AxisChange( g );
					break;
                case 4:
                    //Issues By Status Chart
                    ZedGraphWeb1.Height = 250;
                    ZedGraphWeb1.Width = 200;

                    // Set the GraphPane title
                    myPane.Title = "User Summary";
                    myPane.FontSpec.IsItalic = false;
                    myPane.FontSpec.IsBold = true;
                    myPane.FontSpec.Size = 36f;
                    myPane.FontSpec.Family = "Times";
                    myPane.Legend.IsVisible = false;
                                 
                    // Add some pie slices
                    al = (ArrayList)Bug.GetBugStatusCountByProject(ProjectId);
                    foreach (BugCount bc in al)
                    {
                        PieItem segment = myPane.AddPieSlice(bc.Count, GetStatusColor(bc.Id), Color.White, 45f, 0, bc.Name);
                        segment.LabelDetail.FontSpec.Size = 20f;
                       
                    }

                    // Sum up the pie values                                                               
                    curves = myPane.CurveList;
                    total = 0;
                    for (int x = 0; x < curves.Count; x++)
                        total += ((PieItem)curves[x]).Value;

                    // Make a text label to highlight the total value
                    text = new TextItem("Total Issues\n" + total.ToString(), 0.2F, 0.30F, CoordType.PaneFraction);
                    text.Location.AlignH = AlignH.Center;
                    text.FontSpec.Size = 24f;
                    text.Location.AlignV = AlignV.Bottom;
                    text.FontSpec.Border.IsVisible = false;
                    text.FontSpec.Fill = new Fill(Color.White, Color.FromArgb(255, 100, 100), 45F);
                    text.FontSpec.StringAlignment = StringAlignment.Center;
                    myPane.GraphItemList.Add(text);

                    // Create a drop shadow for the total value text item
                    text2 = new TextItem(text);
                    text2.FontSpec.Fill = new Fill(Color.Black);
                    text2.Location.X += 0.008f;
                    text2.Location.Y += 0.01f;
                    myPane.GraphItemList.Add(text2);

                    // Calculate the Axis Scale Ranges
                    myPane.AxisChange(g);
                    break;
	
			}
			

		}

		#region Web Form Designer generated code
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ZedGraphWeb1.RenderGraph += new ZedGraph.ZedGraphWebControlEventHandler(this.OnRenderGraph);
		}
		#endregion
	}
}
