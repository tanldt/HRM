namespace iHRPCore.MdlHR
{
	#region USING DIRECTIVES
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using iHRPCore.HRComponent;
	using iHRPCore.Com;
	#endregion


	public class Relation : System.Web.UI.UserControl
	{		
		#region STATIC GLOBAL VARIABLES
		public string strLanguage = "VN";				
		static DataTable m_dt=new DataTable();
		protected System.Web.UI.HtmlControls.HtmlTable tblInfo;
		static int iCurrentRelativeIndex=0;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.DropDownList cboGender;
		protected System.Web.UI.WebControls.Label lblRelationStatus;
		protected System.Web.UI.WebControls.DropDownList cboStatus;
		protected System.Web.UI.WebControls.Label lblIDCardNo;
		protected System.Web.UI.WebControls.TextBox txtIDNo;
		protected System.Web.UI.WebControls.TextBox txtDOB;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblAge;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblContact;
		protected System.Web.UI.WebControls.Label lblNote;
		protected System.Web.UI.WebControls.TextBox txtNote;		
		#endregion
		
		#region Web Form Designer generated code

		protected System.Web.UI.WebControls.Label lblErr;
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.Label lblGender;
		protected System.Web.UI.WebControls.Label lblRelationID;
		protected System.Web.UI.WebControls.Label lblRelationIn ;
		protected System.Web.UI.WebControls.TextBox txtAge;
		protected System.Web.UI.HtmlControls.HtmlTableRow trEmpID;
		protected System.Web.UI.WebControls.DataGrid dtgList;
		protected System.Web.UI.WebControls.CheckBox chkShowGrid;
		protected System.Web.UI.WebControls.LinkButton btnAddNew;
		protected System.Web.UI.WebControls.LinkButton btnSave;
		protected System.Web.UI.WebControls.LinkButton btnDelete;
		protected System.Web.UI.WebControls.LinkButton btnExport;
		protected System.Web.UI.WebControls.CheckBox chkSameCompany;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.DropDownList cboLSRelationshipID;
		protected System.Web.UI.WebControls.TextBox txtRelativeEmpID;
		protected System.Web.UI.WebControls.TextBox txtBefore75;
		protected System.Web.UI.WebControls.TextBox txtAfter75;
		protected System.Web.UI.WebControls.TextBox txtOccupation;
		protected System.Web.UI.WebControls.TextBox txtWorkPlace;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.LinkButton btnList;
		protected System.Web.UI.WebControls.TextBox txtRelationshipID;
		protected System.Web.UI.WebControls.TextBox txtRelativeID;
		protected System.Web.UI.WebControls.TextBox txtRelationType;		
		protected System.Web.UI.HtmlControls.HtmlSelect lstDiffAges;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtEmpIDHidden;
		protected System.Web.UI.WebControls.Button btnEmpPopup;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtYYYY;
		protected System.Web.UI.WebControls.TextBox txtContact;
		protected System.Web.UI.WebControls.CheckBox chkAdBefore;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdBefore;
		protected System.Web.UI.WebControls.TextBox txtRelativeEmpCode;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.DropDownList cboLSBloodTypeID;
		protected System.Web.UI.WebControls.TextBox txtTelephone;
		protected System.Web.UI.WebControls.TextBox txtReductionFrom;
		protected System.Web.UI.WebControls.CheckBox chkReductFamily;
		protected System.Web.UI.WebControls.TextBox txtReductionTo;
		protected System.Web.UI.HtmlControls.HtmlTableRow trReduction;
		protected System.Web.UI.WebControls.TextBox txtIssueDate_IDNo;
		protected System.Web.UI.WebControls.TextBox txtPassNo;
		protected System.Web.UI.WebControls.TextBox txtIssueDate_Pass;
		protected System.Web.UI.WebControls.TextBox txtIssuePlace_IDNo;
		protected System.Web.UI.WebControls.TextBox txtEffectiveDate_Pass;
		protected System.Web.UI.WebControls.TextBox txtExpiredDate_Pass;		
		protected iHRPCore.Include.EmpHeader HR_EmpHeader;
		

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
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnList.Click += new System.EventHandler(this.btnList_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			this.dtgList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgList_ItemCommand);
			this.dtgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgList_PageIndexChanged);
			this.btnSubmit.ServerClick += new System.EventHandler(this.btnSubmit_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{	
			strLanguage = Session["LangID"] != null?Session["LangID"].ToString().Trim():"EN";
			Session["CurFunction"] = Request.Params["FunctionID"].Trim();	
			if(!IsPostBack)
			{	
				Session["AddNew"]="true";				
					LoadRelative();						
					LoadCombos();
					btnAddNew_Click(null,null);
			}
						
			// SOME ATTRIBUTES
			chkSameCompany.Attributes.Add("onclick","return ShowEmpCompany()");		
			chkReductFamily.Attributes.Add("onclick","return ShowReduction()");		
			btnEmpPopup.Attributes.Add("onclick","return MyOpenWindowEmp('EmpID');");				
			txtDOB.Attributes.Add("onblur","return MyCheckDate('"+txtDOB.ClientID+"','"+DateTime.Now.ToShortDateString()+"','"+txtAge.ClientID+"','"+lstDiffAges.ClientID+"','"+cboLSRelationshipID.ClientID+"')");
			btnSave.Attributes.Add("onclick","return ValidForm('"+txtDOB.ClientID+"','"+DateTime.Now.ToShortDateString()+"','"+txtAge.ClientID+"','"+lstDiffAges.ClientID+"');");
			btnDelete.Attributes.Add("onclick","return checkdelete()");
			// CURRENT FUNCTION
		}		
		private void LoadCombos()
		{
			string strTextField = strLanguage == "VN"?"VNName":"Name";						
			clsCommon.LoadDropDownListControl(cboLSRelationshipID,"sp_GetDataCombo @TableName='LS_tblRelationship',@Fields='LSRelationshipID as [ID]," + strTextField + " as Name'","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboGender,"HR_spfrmEMPCV @Activity='getGender',@languageID='" + strLanguage + "' ","ID","Name",true);
			clsCommon.LoadDropDownListControl(cboStatus,"HR_spfrmEMPCV @Activity='getStatus',@languageID='" + strLanguage + "' ","ID","Name",false);
			if (Session["EmpID"]!=null)
			txtYYYY.Value = clsHRRelation.GetDOB(Session["EmpID"]);			
			
			
			// DIFF AGES COMBO
			if (Session["EmpID"]!=null)
			lstDiffAges.DataSource=clsHRRelation.LoadDiffAges(Session["EmpID"]);
			lstDiffAges.DataTextField="DiffAgeAndDOB";
			lstDiffAges.DataValueField="Code";
			lstDiffAges.DataBind();
		}
		private void LoadRelative()
		{
			#region LOAD RELATIVES GRID BY EMPID
			m_dt=clsHRRelation.LoadGridRelative(Session["EmpID"],strLanguage);			
							
			if((dtgList.Items.Count % dtgList.PageSize == 1) && (dtgList.CurrentPageIndex == dtgList.PageCount - 1) && (dtgList.CurrentPageIndex != 0))
			{
				dtgList.CurrentPageIndex = dtgList.CurrentPageIndex -1;
			}
			clsCommon.setGridPageIndex(dtgList);
			dtgList.DataSource=m_dt;
			dtgList.DataBind();
			
			#endregion

			if(m_dt.Rows.Count>iCurrentRelativeIndex)
			{
				#region LOAD INFOR. TO TEXT CONTROLS
				txtLastName.Text=m_dt.Rows[iCurrentRelativeIndex]["LastName"].ToString();						
				txtFirstName.Text=m_dt.Rows[iCurrentRelativeIndex]["FirstName"].ToString();				
				txtOccupation.Text=m_dt.Rows[iCurrentRelativeIndex]["Occupation"].ToString();				
				txtAddress.Text=m_dt.Rows[iCurrentRelativeIndex]["Address"].ToString();				
				txtBefore75.Text=m_dt.Rows[iCurrentRelativeIndex]["Before75"].ToString();
				txtAfter75.Text=m_dt.Rows[iCurrentRelativeIndex]["After75"].ToString();
				txtContact.Text=m_dt.Rows[iCurrentRelativeIndex]["Contact"].ToString();
				txtWorkPlace.Text=m_dt.Rows[iCurrentRelativeIndex]["WorkPlace"].ToString();
				txtIDNo.Text=m_dt.Rows[iCurrentRelativeIndex]["IDNo"].ToString();
				txtNote.Text=m_dt.Rows[iCurrentRelativeIndex]["Note"].ToString();	
				txtIssueDate_IDNo.Text = m_dt.Rows[iCurrentRelativeIndex]["IssueDate_IDNo"].ToString();
				txtIssuePlace_IDNo.Text = m_dt.Rows[iCurrentRelativeIndex]["IssuePlace_IDNo"].ToString();
				txtPassNo.Text = m_dt.Rows[iCurrentRelativeIndex]["PassNo"].ToString();
				txtIssueDate_Pass.Text = m_dt.Rows[iCurrentRelativeIndex]["IssueDate_Pass"].ToString();
				txtEffectiveDate_Pass.Text = m_dt.Rows[iCurrentRelativeIndex]["EffectiveDate_Pass"].ToString();
				txtExpiredDate_Pass.Text = m_dt.Rows[iCurrentRelativeIndex]["ExpiredDate_Pass"].ToString();
				chkReductFamily.Checked = m_dt.Rows[iCurrentRelativeIndex]["ReductFamily"].ToString()=="True"?true:false;
				txtReductionFrom.Text = m_dt.Rows[iCurrentRelativeIndex]["ReductionFrom"].ToString();
				txtReductionTo.Text = m_dt.Rows[iCurrentRelativeIndex]["ReductionTo"].ToString();
				txtTelephone.Text=m_dt.Rows[iCurrentRelativeIndex]["Telephone"].ToString();
				//cboLSBloodTypeID.SelectedValue= m_dt.Rows[iCurrentRelativeIndex]["LSBloodTypeID"].ToString();

				if(m_dt.Rows[iCurrentRelativeIndex].IsNull("DOB")==false)
					txtDOB.Text=DateTime.Parse(m_dt.Rows[iCurrentRelativeIndex]["DOB"].ToString()).ToString("dd/MM/yyyy");
				if (m_dt.Rows[iCurrentRelativeIndex].IsNull("RelativeEmpID")==false)
				{
					txtRelativeEmpID.Text=m_dt.Rows[iCurrentRelativeIndex]["RelativeEmpID"].ToString();
					chkSameCompany.Checked=true;
				}
				else
				{
					txtRelativeEmpID.Text="";
					chkSameCompany.Checked=false;
				}
				ShowControls(chkSameCompany.Checked);
				
				#endregion

				#region CALCULATE AGE
				if(txtDOB.Text.Length>0)
				{
					DateTime dob=DateTime.Parse(m_dt.Rows[iCurrentRelativeIndex]["DOB"].ToString());
					DateTime dnow=DateTime.Now;					
					int iAge=(dnow.Year-dob.Year);
					txtAge.Text=iAge.ToString();					
					txtDOB.Text=dob.ToString("yyyy");
				}
				#endregion

				
				cboGender.SelectedValue=m_dt.Rows[iCurrentRelativeIndex]["Gender"].ToString();											
				cboStatus.SelectedValue=m_dt.Rows[iCurrentRelativeIndex]["Status"].ToString();				
				

				#region SET VALUES FOR HIDDEN CONTROLS - USE FOR CLSCOMMON.IMPACTDB

				cboLSRelationshipID.SelectedValue=m_dt.Rows[iCurrentRelativeIndex]["Code"].ToString();	
				txtRelativeID.Text=m_dt.Rows[iCurrentRelativeIndex]["RelativeID"].ToString();	
				txtRelationshipID.Text=cboLSRelationshipID.SelectedValue;	
				Session["RelativeID"]=txtRelativeID.Text;				
				#endregion

				#region INITIALIZE VARIABLES
				Session["AddNew"]="false";
				#endregion
			}								
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			#region CLEAR CONTROLS
			Session["AddNew"]="true";								
			clsCommon.ClearControlValue(this);	
			lblErr.Text="";
			cboGender.Enabled=true;
			cboStatus.Enabled=true;
			txtDOB.Enabled=true;
			ShowControls(chkSameCompany.Checked);
			txtRelativeID.Text="";
			#endregion
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{			
			if(txtDOB.Text.Length>0)
			{
				string sDOB="01/01/" + txtDOB.Text;				
				string[] arrStr=sDOB.Split('/');				
				
				if(Convert.ToInt32(txtYYYY.Value.Trim())==0)
				{
					clsChangeLang.popupWindow(this.Parent,"The YOB of this employee not existed. Return Personal Information function","",0);
					return;
				}

				// KIEM TRA NAM SINH CUA CHA ME
				if(cboLSRelationshipID.SelectedValue.Trim()=="MR" ||cboLSRelationshipID.SelectedValue.Trim()=="FR")
				{
					if(Convert.ToInt32(arrStr[2]) >= Convert.ToInt32(txtYYYY.Value.Trim()))
					{												
						clsChangeLang.popupWindow(this.Parent,clsChangeLang.getStringAlert("RE_0005",strLanguage).Replace("@",cboLSRelationshipID.Items[cboLSRelationshipID.SelectedIndex].Text),"",0);
						return;
					}
				}
					// KIEM TRA NAM SINH CUA CON CAI
				else if(cboLSRelationshipID.SelectedValue.Trim()=="CH" ||cboLSRelationshipID.SelectedValue.Trim()=="DA")
				{
					if(Convert.ToInt32(arrStr[2]) <= Convert.ToInt32(txtYYYY.Value.Trim()))
					{						
						clsChangeLang.popupWindow(this.Parent,clsChangeLang.getStringAlert("RE_0006",strLanguage).Replace("@",cboLSRelationshipID.Items[cboLSRelationshipID.SelectedIndex].Text),"",0);
						return;
					}
				}
				//Kiểm tra Cho phép chọn chính nhân viên đó làm mối liên hệ gia đình
				if( txtRelativeEmpID.Text!="")
				{
					if (Session["EmpID"].ToString()==txtRelativeEmpID.Text)
					{					
						clsChangeLang.popupWindow(this.Parent,"RE_0007",strLanguage,"",0);	
						return;
					}
				}
				if (!clsHRRelation.CheckIdentityCard(txtIDNo.Text,txtRelativeID.Text,Session["EmpID"].ToString()))
				{					
					clsChangeLang.popupWindow(this.Parent,"RE_0008",strLanguage,"",0);
					return;
				}

				//THANHND END EDIT	
			
				//KIỆM TRA GIỚI TINH CỦA NHỮNG NGƯỜI TRONG GIA ĐÌNH
				int iReturnValue = clsHRRelation.GetForce(cboLSRelationshipID.SelectedValue.Trim());
				if (iReturnValue !=2)
					if (cboGender.SelectedValue != iReturnValue.ToString())
					{						
						clsChangeLang.popupWindow(this.Parent,"RE_0009",strLanguage,"",0);
						return;
					}
				//KIỂM TRA 1 NGƯỜI CHỈ ĐƯỢC QUAN HỆ 1 LẦN đối với 1 người
						
					string strErr=clsHRRelation.CheckEmpRelative(Session["EmpID"].ToString(),txtRelativeEmpID.Text,txtRelativeID.Text,strLanguage);
				if (strErr!="") 
				{
					clsChangeLang.popupWindow(this.Parent,"RE_0012",strLanguage,"",0);
					return;
				}
					
						
				

				
				//KIỂM TRA SỐ TỐI DA CÓ THỂ TRONG QUAN HỆ GIA ĐÌNH
				if(Session["AddNew"].ToString()=="true")
				{					
					if (!clsHRRelation.CheckRelationshipAvaible(Session["EmpID"].ToString(),cboLSRelationshipID.SelectedValue,1))
					{						
						clsChangeLang.popupWindow(this.Parent,clsChangeLang.getStringAlert("RE_0010",strLanguage).Replace("@",cboLSRelationshipID.SelectedItem.Text),"",0);
						return;						
					}
				}
				else//SỬ DỤNG CHO PHẦN EDIT
				{
					if (!clsHRRelation.CheckRelationshipAvaible(Session["EmpID"].ToString(),cboLSRelationshipID.SelectedValue,Session["CurrentRel"].ToString(),1))
					{						
						clsChangeLang.popupWindow(this.Parent,clsChangeLang.getStringAlert("RE_0010",strLanguage).Replace("@",cboLSRelationshipID.SelectedItem.Text),"",0);
						return;
					}
				}
				

				//CANGTT END EDIT
				if(Session["AddNew"].ToString()=="true")
				{				
					#region CHECK IF MAXIMUM REACHED AND ADD NEW
						if(clsCommon.ImpactDB(Session["EmpID"].ToString(),"Save",this,"HR_spfrmRelative")!=true)								
							clsChangeLang.popupWindow(this,"RE_0028",strLanguage,"",0);
						else
						{
							lblErr.Text="";						
							LoadRelative();
							Session["AddNew"]="false";
							btnAddNew_Click(btnAddNew,null);
						}					
					#endregion
				}	
				else // EDIT
				{
					#region UPDATE
					if(clsCommon.ImpactDB(Session["EmpID"].ToString(),"Update",this,"HR_spfrmRelative")!=true)
						clsChangeLang.popupWindow(this,"RE_0028",strLanguage,"",0);
					else
					{
						lblErr.Text="";					
						LoadRelative();
						Session["AddNew"]="false";
						btnAddNew_Click(btnAddNew,null);
					}
					#endregion
				}
			}
			else clsChangeLang.popupWindow(this,"RE_0011",strLanguage,"",0);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			string strID="";
			for(int i=0;i<dtgList.Items.Count;i++)
			{
				if(((CheckBox)dtgList.Items[i].FindControl("chkSelect")).Checked==true)
				{
					strID += dtgList.Items[i].Cells[0].Text.Trim() + "$";
				}
			}
			clsCommon.DeleteListRecord("HR_spfrmRelative","RelativeID",SqlDbType.NVarChar,12,strID);			
			LoadRelative();			
		}

		private void dtgList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName=="LoadRelative")
			{
				txtRelativeID.Text=dtgList.Items[e.Item.ItemIndex].Cells[0].Text.Trim();
				DataRow drData = clsHRRelation.GetDataByID(txtRelativeID.Text);
				
				txtLastName.Text=drData["LastName"].ToString();						
				txtFirstName.Text=drData["FirstName"].ToString();				
				txtOccupation.Text=drData["Occupation"].ToString();
				txtAddress.Text=drData["Address"].ToString();				
				txtBefore75.Text=drData["Before75"].ToString();
				txtAfter75.Text=drData["After75"].ToString();
				txtContact.Text=drData["Contact"].ToString();
				txtWorkPlace.Text=drData["WorkPlace"].ToString();
				txtIDNo.Text=drData["IDNo"].ToString();				
				txtNote.Text=drData["Note"].ToString();					
				txtDOB.Text=drData["DOB"].ToString();
				txtRelativeEmpID.Text=drData["RelativeEmpID"].ToString();
				txtRelativeEmpCode.Text=drData["RelativeEmpCode"].ToString();
				cboGender.SelectedValue=drData["Gender"].ToString();
				cboStatus.SelectedValue=drData["Status"].ToString();

				txtIssueDate_IDNo.Text = drData["IssueDate_IDNo"].ToString();
				txtIssuePlace_IDNo.Text = drData["IssuePlace_IDNo"].ToString();
				txtPassNo.Text = drData["PassNo"].ToString();
				txtIssueDate_Pass.Text = drData["IssueDate_Pass"].ToString();
				txtEffectiveDate_Pass.Text = drData["EffectiveDate_Pass"].ToString();
				txtExpiredDate_Pass.Text = drData["ExpiredDate_Pass"].ToString();
				chkReductFamily.Checked = drData["ReductFamily"].ToString()=="True"?true:false;
				txtReductionFrom.Text = drData["ReductionFrom"].ToString();
				txtReductionTo.Text = drData["ReductionTo"].ToString();
				txtTelephone.Text = drData["Telephone"].ToString();
				cboLSBloodTypeID.SelectedValue= drData["LSBloodTypeID"].ToString();


				if (!txtRelativeEmpID.Text.Equals(""))
					chkSameCompany.Checked=true;
				else
					chkSameCompany.Checked=false;
				
				ShowControls(chkSameCompany.Checked);
								
				if(txtDOB.Text.Length>0)
				{
					DateTime dob=DateTime.Parse(txtDOB.Text);
					DateTime dnow=DateTime.Now;					
					int iAge=(dnow.Year-dob.Year);
					txtAge.Text=iAge.ToString();					
					txtDOB.Text=dob.ToString("yyyy");
					
				}
								
				cboLSRelationshipID.SelectedValue=drData["LSRelationshipID"].ToString();							
				txtRelativeID.Text=drData["RelativeID"].ToString();	
				Session["AddNew"]="false";
				Session["CurrentRel"]=drData["LSRelationshipID"].ToString();	
				
			}
		}
		private void LoadDataDefault(string strEmpID)
		{
			DataTable dt=clsHRRelation.LoadEmpByID(Session["EmpID"]);
			if(dt.Rows.Count>0)
			{
				txtLastName.Text=dt.Rows[0]["VLastName"].ToString();
				txtFirstName.Text=dt.Rows[0]["VFirstName"].ToString();
				txtIDNo.Text=dt.Rows[0]["IDNo"].ToString();				
				if(Session["RelativeID"]!=null)
					txtRelativeID.Text=Session["RelativeID"].ToString();

				if(dt.Rows[0]["Gender"].ToString()=="False")
					cboGender.SelectedValue="0";
				else cboGender.SelectedValue="1";

				if(dt.Rows[0].IsNull("DOB")==false)
					txtDOB.Text=dt.Rows[0]["DOB"].ToString();
								
				if(txtDOB.Text.Length>0)
				{
					DateTime dob=DateTime.Parse(txtDOB.Text);
					DateTime dnow=DateTime.Now;					
					int iAge=(dnow.Year-dob.Year);
					txtAge.Text=iAge.ToString();					
					txtDOB.Text=dob.ToString("dd/MM/yyy");
				}					
			}
		}

		private void dtgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dtgList.CurrentPageIndex=e.NewPageIndex;
			if(e.NewPageIndex*dtgList.PageSize>m_dt.Rows.Count)
				dtgList.CurrentPageIndex=0;

			dtgList.DataSource=m_dt;
			dtgList.DataBind();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			iHRPCore.DataGridExcelExporter myExcelXport=new DataGridExcelExporter(dtgList);
			myExcelXport.Export("");
			myExcelXport = null;
		}
		private void FillRelativeInPru(string strEmpID)
		{
			DataTable dt=clsHRRelation.LoadEmpByID(strEmpID);
			if(dt.Rows.Count>0)
			{
				txtLastName.Text=dt.Rows[0]["VLastName"].ToString();
				txtFirstName.Text=dt.Rows[0]["VFirstName"].ToString();
				txtIDNo.Text=dt.Rows[0]["IDNo"].ToString();
				txtRelativeEmpID.Text=strEmpID;
				/*if(Session["RelativeID"]!=null)
					txtRelativeID.Text=Session["RelativeID"].ToString();*/
				
				cboGender.SelectedValue=dt.Rows[0]["Gender"].ToString();
				cboStatus.SelectedValue="1";
				
				if(dt.Rows[0].IsNull("DOB")==false)
					txtDOB.Text=dt.Rows[0]["DOB"].ToString();
								
				if(txtDOB.Text.Length>0)
				{
					DateTime dob=DateTime.Parse(txtDOB.Text);
					DateTime dnow=DateTime.Now;					
					int iAge=(dnow.Year-dob.Year);
					txtAge.Text=iAge.ToString();					
					txtDOB.Text=dob.ToString("yyyy");
				}				
				
				txtRelationshipID.Text=cboLSRelationshipID.SelectedValue;	

				ShowControls(chkSameCompany.Checked);
			}
		}
		private void btnSubmit_ServerClick(object sender, System.EventArgs e)
		{
			FillRelativeInPru(txtEmpIDHidden.Value);
		}
		private void ShowControls(bool bType)
		{
			if (bType)
			{
				// SHOW CONTROLS
				trEmpID.Style.Add("DISPLAY","block");						

				txtLastName.Enabled=false;
				txtFirstName.Enabled=false;
				txtIDNo.Enabled=false;
				cboGender.Enabled=false;
				txtDOB.Enabled=false;
				cboStatus.Enabled=false;
				txtIDNo.Enabled=false;
				////////////////
			}
			else
			{
				trEmpID.Style.Add("DISPLAY","none");					

				txtLastName.Enabled=true;
				txtFirstName.Enabled=true;
				cboGender.Enabled=true;
				txtDOB.Enabled=true;
				cboStatus.Enabled=true;
				txtIDNo.Enabled=true;
			}		
					
		}

		private void btnList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.ApplicationPath.Trim() + "/Editpage.aspx?ModuleID=HR&ParentID=8&FunctionID=14&Ascx=MdlHR/EmpList.ascx");
		}
	}
}
