namespace iHRPCore.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.Com;

	/// <summary>
	///		Summary description for WFReportList.
	/// </summary>
	public class WFReportList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Image Image76;
		protected System.Web.UI.WebControls.LinkButton btnRpt063;
		protected System.Web.UI.WebControls.Image Image77;
		protected System.Web.UI.WebControls.LinkButton btnRpt062;
		protected System.Web.UI.WebControls.Image Image78;
		protected System.Web.UI.WebControls.LinkButton btnRpt061;
		protected System.Web.UI.WebControls.Image Image79;
		protected System.Web.UI.WebControls.LinkButton btnRpt052;
		protected System.Web.UI.WebControls.Image Image80;
		protected System.Web.UI.WebControls.LinkButton btnRpt051;
		protected System.Web.UI.WebControls.Image Image81;
		protected System.Web.UI.WebControls.LinkButton btnRpt04;
		protected System.Web.UI.WebControls.Image Image82;
		protected System.Web.UI.WebControls.LinkButton btnRpt03;
		protected System.Web.UI.WebControls.Image Image83;
		protected System.Web.UI.WebControls.LinkButton btnRpt0102;
		protected System.Web.UI.WebControls.Image Image84;
		protected System.Web.UI.WebControls.Panel pnlTR;
		protected System.Web.UI.WebControls.ImageButton btnCalToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ImageButton btnCalFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.LinkButton btnRptVoucherParking;
		protected System.Web.UI.WebControls.LinkButton btnRptHealthchecked;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList cboEquipment;
		protected iHRPCore.Include.EmpHeaderSearch EmpHeaderSearch1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			LoadComboEquipment();
		}

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
			this.btnRpt0102.Click += new System.EventHandler(this.btnRpt0102_Click);
			this.btnRpt03.Click += new System.EventHandler(this.btnRpt03_Click);
			this.btnRpt04.Click += new System.EventHandler(this.btnRpt04_Click);
			this.btnRpt051.Click += new System.EventHandler(this.btnRpt051_Click);
			this.btnRpt052.Click += new System.EventHandler(this.btnRpt052_Click);
			this.btnRpt061.Click += new System.EventHandler(this.btnRpt061_Click);
			this.btnRpt062.Click += new System.EventHandler(this.btnRpt062_Click);
			this.btnRptVoucherParking.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnRptHealthchecked.Click += new System.EventHandler(this.btnRptHealthchecked_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LoadComboEquipment()
		{
			txtFromDate.Attributes.Add("onblur","return MyCheckDate('"+txtFromDate.ClientID+"');");
			btnCalFromDate.Attributes.Add("onclick","return MyShowCalendar('"+txtFromDate.ClientID+"');");
			txtToDate.Attributes.Add("onblur","return MyCheckDate('"+txtToDate.ClientID+"');");
			btnCalToDate.Attributes.Add("onclick","return MyShowCalendar('"+txtToDate.ClientID+"');");
			btnRpt0102.Attributes.Add("onclick","return SelectEquipment('"+cboEquipment.ClientID+"');");
			btnRpt03.Attributes.Add("onclick","return SelectEquipment('"+cboEquipment.ClientID+"');");
			
			clsCommon.LoadDropDownListControl(cboEquipment,"LS_spfrmEquipment @Activity='GetDataAll'","LSEquipmentCode","Name",true);
		}

		private void btnRpt0102_Click(object sender, System.EventArgs e)
		{			
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
			Session["EquipmentCode"]=cboEquipment.SelectedValue;
			Session["EquipmentName"]=cboEquipment.SelectedItem.Text;

			string strURL="./Reports/WF/WFReport.aspx?RptName=StockCard";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt03_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
			Session["EquipmentCode"]=cboEquipment.SelectedValue;
			Session["EquipmentName"]=cboEquipment.SelectedItem.Text;

			string strURL="./Reports/WF/WFReport.aspx?RptName=Equipment";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt052_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;

			string strURL="./Reports/WF/WFReport.aspx?RptName=UniformReceipt";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt061_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;

			string strURL="./Reports/WF/WFReport.aspx?RptName=UniformChangeCash";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt062_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;

			string strURL="./Reports/WF/WFReport.aspx?RptName=ParkingChangeCash";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");
		}

		private void btnRpt04_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
		
			string strURL="./Reports/WF/WFReport.aspx?RptName=CardKeyIssue";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");			
		}

		private void btnRpt051_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
		
			string strURL="./Reports/WF/WFReport.aspx?RptName=BenefitEmp";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");	
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			
		}

		private void btnRptHealthchecked_Click(object sender, System.EventArgs e)
		{
			// CRYSTAL REPORT			
			Session["EmpID"]=EmpHeaderSearch1.txtEmpID.Text;
			Session["EmpName"]=EmpHeaderSearch1.txtEmpName.Text;
			Session["JobClass"]=EmpHeaderSearch1.cboJobcode.SelectedValue;
			Session["Location"]=EmpHeaderSearch1.cboLocation.SelectedValue;
			Session["Level1"]=EmpHeaderSearch1.cboLevel1.SelectedValue;
			Session["Level2"]=EmpHeaderSearch1.cboLevel2.SelectedValue;
			Session["Level3"]=EmpHeaderSearch1.cboLevel3.SelectedValue;
			Session["Position"]=EmpHeaderSearch1.cboLSEmpTypeID.SelectedValue;	
			Session["FromDate"]=txtFromDate.Text;
			Session["ToDate"]=txtToDate.Text;
		
			string strURL="./Reports/WF/WFReport.aspx?RptName=HealthChecked";
			Response.Write("<script>window.open('"+strURL+"','VIEW_REPORT','toolbar=0,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,top=10,left=50,width=700,height=550,align=top');</script>");	
		}
	}
}
