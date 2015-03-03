using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.Mail;
using Microsoft.VisualBasic ;

namespace Web_DM.Component
{
	/// <summary>
	/// Summary description for clsDataGrid.
	/// </summary>
	public class clsDataGrid
	{
		public clsDataGrid()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public class DataGridTemplate : ITemplate
		{
			ListItemType templateType;
			string columnName;
			string UrlImage;
			string _Type;
			public DataGridTemplate(ListItemType type, string colname)
			{
				templateType = type;
				columnName = colname;				
			}
			public DataGridTemplate(ListItemType type, string colname,string strType,string strUrlImage)
			{
				templateType = type;
				columnName = colname;
				UrlImage = strUrlImage;
				_Type = strType;
			}
			public void InstantiateIn(System.Web.UI.Control container)
			{
				Literal lc = new Literal();
				switch(templateType)
				{
					case ListItemType.Header:
						lc.Text = "<B>" + columnName + "</B>";
						container.Controls.Add(lc);
						break;
					case ListItemType.Item:						
						if(_Type.Trim().ToUpper().Equals("LABEL"))
						{
							Label lbl = new Label();
							container.Controls.Add(lbl);
							container.Controls.Add(lbl);
						}
						else if(_Type.Trim().ToUpper().Equals("CHECKBOX"))
						{
							CheckBox chk = new CheckBox();
							chk.ID = "chkSelect";
							container.Controls.Add(chk);
						}						
						else if(_Type.Trim().ToUpper().Equals("IMAGEBUTTON"))
						{
							ImageButton btnEdit = new ImageButton();
							btnEdit.ImageUrl = UrlImage;
							container.Controls.Add(btnEdit);
						}
						else if(_Type.Trim().ToUpper().Equals("LINKBUTTON"))
						{
							LinkButton btnEdit = new LinkButton();
							btnEdit.Text = "Edit";
							btnEdit.CommandName = "EDIT";
							container.Controls.Add(btnEdit);
						}
						else if(_Type.Trim().ToUpper().Equals("TEXTBOX"))
						{
							TextBox chk = new TextBox();
							chk.ID = "txtText";
							container.Controls.Add(chk);
						}						
						break;
					case ListItemType.EditItem:
						TextBox txt = new TextBox();
						txt.Text = "";						
						container.Controls.Add(txt);
						break;
					case ListItemType.Footer:
						lc.Text = "<I>" + columnName + "</I>";
						container.Controls.Add(lc);
						break;
				}
			}
		}
	}
}
