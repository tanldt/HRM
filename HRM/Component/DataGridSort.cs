//Hau
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace GridSort
{
	/// <summary>
	/// Summary description for DataGridSort.
	/// </summary>
	/// 
	public enum SORTTYPE
	{
		SingleSort,
		MultiSort,
		AdMultiSort
	};

	public class DataGridSort
	{
		static public String sMulSort = "";

		public DataGridSort()
		{
		}

		static private Boolean ReplaceEnd(ref String Src, String EndStr, String RepStr)
		{
			if(!Src.EndsWith(EndStr))
				return false;
			Src = Src.Remove(Src.Length - EndStr.Length, EndStr.Length) + RepStr;
			return true;
		}

		static private void ColumnSort(DataGrid Grid, DataTable SrcTable, DataGridSortCommandEventArgs e)
		{
			//String sOrder = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				String SortExp = Col.SortExpression;
				String HeaderText = Col.HeaderText;
				if(e.SortExpression.ToLower().Equals(Col.SortExpression.ToLower()))
				{	
					if(ReplaceEnd(ref SortExp, " ASC", " DESC"))
					{
						Col.SortExpression = sMulSort = SortExp;
						if(ReplaceEnd(ref HeaderText, "-a", "-d"))
							Col.HeaderText = HeaderText;
						else
							Col.HeaderText += "-d";
					}
					else
					{
						if(ReplaceEnd(ref SortExp, " DESC", " ASC"))
						{
							Col.SortExpression = sMulSort = SortExp;
							if(ReplaceEnd(ref HeaderText, "-d", "-a"))
								Col.HeaderText = HeaderText;
							else
								Col.HeaderText += "-a";
						}
						else
						{
							sMulSort = Col.SortExpression = e.SortExpression + " ASC";
							Col.HeaderText += "-a";
						}
					}
				}
				else
				{
					if(ReplaceEnd(ref SortExp, " ASC", "")
						|| ReplaceEnd(ref SortExp, " DESC", ""))
						Col.SortExpression = SortExp;

					if(ReplaceEnd(ref HeaderText, "-a", "")
						|| ReplaceEnd(ref HeaderText, "-d", ""))
						Col.HeaderText = HeaderText;
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static private void ColumnSort(DataGrid Grid, DataTable SrcTable, String SortExpression)
		{
			//String sOrder = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				String SortExp = Col.SortExpression;
				String HeaderText = Col.HeaderText;
				if(SortExpression.ToLower().Equals(Col.SortExpression.ToLower()))
				{	
					if(ReplaceEnd(ref SortExp, " ASC", " DESC"))
					{
						Col.SortExpression = sMulSort = SortExp;
						if(ReplaceEnd(ref HeaderText, "-a", "-d"))
							Col.HeaderText = HeaderText;
					}
					else
					{
						if(ReplaceEnd(ref SortExp, " DESC", " ASC"))
						{
							Col.SortExpression = sMulSort = SortExp;
							if(ReplaceEnd(ref HeaderText, "-d", "-a"))
								Col.HeaderText = HeaderText;
						}
						else
						{
							sMulSort = Col.SortExpression = SortExpression + " ASC";
							Col.HeaderText += "-a";
						}
					}
				}
				else
				{
					if(ReplaceEnd(ref SortExp, " ASC", "")
						|| ReplaceEnd(ref SortExp, " DESC", ""))
						Col.SortExpression = SortExp;

					if(ReplaceEnd(ref HeaderText, "-a", "")
						|| ReplaceEnd(ref HeaderText, "-d", ""))
						Col.HeaderText = HeaderText;
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static private Boolean BelongTo(String Src, String SubString)
		{
			//tim trong src sortexpression substring
			if(Src.Equals(SubString)) return true;
			if(Src.StartsWith(SubString + ",")) return true;
			if(Src.EndsWith("," + SubString)) return true;
			if(Src.IndexOf("," + SubString + ",") > 0) return true;
			return false;
		}

		static private void ReplaceSub(ref String Src, String OldStr, String NewStr)
		{
			if(OldStr.Equals(""))
				return;
			if(Src.Equals(OldStr))
				Src = NewStr;
			if(Src.StartsWith(OldStr + ","))
			{
				Src = Src.Remove(0, OldStr.Length);
				if(NewStr.Equals(""))
					Src = Src.Remove(0, 1);	//xoa dau "," o dau
				else
					Src = NewStr + Src;
			}
			if(Src.EndsWith("," + OldStr))
			{
				Src = Src.Remove(Src.Length - OldStr.Length, OldStr.Length);
				if(NewStr.Equals(""))
					ReplaceEnd(ref Src, ",", "");	//xoa dau "," o cuoi
				else
					Src += NewStr;
			}
			if(NewStr.Equals(""))
				Src = Src.Replace("," + OldStr + ",", ",");
			else
				Src = Src.Replace("," + OldStr + ",", "," + NewStr + ",");
		}

		static private void MultiSort(DataGrid Grid, DataTable SrcTable, DataGridSortCommandEventArgs e)
		{
			foreach(DataGridColumn Col in Grid.Columns)
			{
				if(Col.SortExpression.ToLower().Equals(e.SortExpression.ToLower()))
				{
					//if(sMulSort.IndexOf(e.SortExpression) == -1)//sMulSort ko chua e.SortExp
					if(!BelongTo(sMulSort, e.SortExpression))
					{
						if(!sMulSort.Equals(""))
							sMulSort += ",";
						sMulSort += (e.SortExpression + " ASC");
						Col.SortExpression += " ASC";
						Col.HeaderText += "-a";
						break;
					}
					else
					{
						String SortExpression = e.SortExpression;
						String HeaderText = Col.HeaderText;
						if(ReplaceEnd(ref SortExpression, " ASC", " DESC"))
						{
							ReplaceSub(ref sMulSort, e.SortExpression, SortExpression);
							Col.SortExpression = SortExpression;
							if(ReplaceEnd(ref HeaderText, "-a", "-d"))
								Col.HeaderText = HeaderText;
							break;
						}
						else
						{
							if(ReplaceEnd(ref SortExpression, " DESC", ""))
							{
								ReplaceSub(ref sMulSort, e.SortExpression, "");
								Col.SortExpression = SortExpression;
								if(ReplaceEnd(ref HeaderText, "-d", ""))
									Col.HeaderText = HeaderText;
								break;
							}
						}
					}
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static private void MultiSort(DataGrid Grid, DataTable SrcTable, String SortExp)
		{
			foreach(DataGridColumn Col in Grid.Columns)
			{
				if(Col.SortExpression.ToLower().Equals(SortExp.ToLower()))
				{
					//if(sMulSort.IndexOf(e.SortExpression) == -1)//sMulSort ko chua e.SortExp
					if(!BelongTo(sMulSort, SortExp))
					{
						if(!sMulSort.Equals(""))
							sMulSort += ",";
						sMulSort += (SortExp + " ASC");
						Col.SortExpression += " ASC";
						Col.HeaderText += "-a";
						break;
					}
					else
					{
						String SortExpression = SortExp;
						String HeaderText = Col.HeaderText;
						if(ReplaceEnd(ref SortExpression, " ASC", " DESC"))
						{
							ReplaceSub(ref sMulSort, SortExp, SortExpression);
							Col.SortExpression = SortExpression;
							if(ReplaceEnd(ref HeaderText, "-a", "-d"))
								Col.HeaderText = HeaderText;
							break;
						}
						else
						{
							if(ReplaceEnd(ref SortExpression, " DESC", ""))
							{
								ReplaceSub(ref sMulSort, SortExp, "");
								Col.SortExpression = SortExpression;
								if(ReplaceEnd(ref HeaderText, "-d", ""))
									Col.HeaderText = HeaderText;
								break;
							}
						}
					}
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static public void Grid_Sort(DataGrid Grid, DataTable SrcTable, DataGridSortCommandEventArgs e, ColumnList Columns)
		{
			if(!(Columns.bMultiSort || Columns.bAdvMultiSort))
				DataGridSort.ColumnSort(Grid, SrcTable, e);
			else
			{	
				if(Columns.bMultiSort)
					DataGridSort.MultiSort(Grid, SrcTable, e);
			}
		}

		static public void Grid_Sort(DataGrid Grid, DataTable SrcTable, String SortExp, Boolean bMulSort)
		{
			if(bMulSort)
				DataGridSort.MultiSort(Grid, SrcTable, SortExp);
			else
				DataGridSort.ColumnSort(Grid, SrcTable, SortExp);
		}

		static public void AdvancedMultiSort(DataGrid Grid, DataTable SrcTable, DropDownList[] ListColumn)
		{
			sMulSort = "";
			String HeaderText = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				HeaderText = Col.HeaderText;
				if(ReplaceEnd(ref HeaderText, "-a", "") || ReplaceEnd(ref HeaderText, "-d", ""))
					Col.HeaderText = HeaderText;
			}
			for(int i=0; i<ListColumn.Length; i++)
			{
				if(!ListColumn[i].SelectedValue.Equals(""))
				{
					if(sMulSort.Equals(""))
						sMulSort = CorrespondSortExp(Grid, ListColumn[i].SelectedValue);
					else
					{
						HeaderText = CorrespondSortExp(Grid, ListColumn[i].SelectedValue);
						if(!HeaderText.Equals(""))
							sMulSort += ("," + HeaderText);
					}
				}
				foreach(DataGridColumn Col in Grid.Columns)
				{
					if(!(Col.SortExpression.Equals("") || Col.HeaderText.EndsWith("-a")
						|| Col.HeaderText.EndsWith("-d")))
					{
						HeaderText = ListColumn[i].SelectedValue;
						if(ReplaceEnd(ref HeaderText, " ASC", ""))							
						{
							if(HeaderText.Equals(Col.HeaderText))
							{
								Col.HeaderText = HeaderText + "-a";
								break;
							}
						}
						else
						{
							if(ReplaceEnd(ref HeaderText, " DESC", "")
								&& HeaderText.Equals(Col.HeaderText))
							{
								Col.HeaderText = HeaderText + "-d";
								break;
							}
						}
					}
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static public void AdvancedMultiSort(DataGrid Grid, DataTable SrcTable, ColumnList Columns)
		{
			sMulSort = "";
			String HeaderText = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				HeaderText = Col.SortExpression;
				if(ReplaceEnd(ref HeaderText, " ASC", "")
					|| ReplaceEnd(ref HeaderText, " DESC", ""))
					Col.SortExpression = HeaderText;
				HeaderText = Col.HeaderText;
				if(ReplaceEnd(ref HeaderText, "-a", "")
					|| ReplaceEnd(ref HeaderText, "-d", ""))
					Col.HeaderText = HeaderText;				
			}
			for(int i=0; i<Columns.Controls.Count; i++)
			{
				if(Columns.Controls[i] is DropDownList)
				{
					if(!((DropDownList)Columns.Controls[i]).SelectedValue.Equals(""))
					{
						if(sMulSort.Equals(""))
							sMulSort = CorrespondSortExp(Grid, ((DropDownList)Columns.Controls[i]).SelectedValue);
						else
						{
							HeaderText = CorrespondSortExp(Grid, ((DropDownList)Columns.Controls[i]).SelectedValue);
							if(!HeaderText.Equals(""))
								sMulSort += ("," + HeaderText);
						}
					}
					foreach(DataGridColumn Col in Grid.Columns)
					{
						if(!(Col.SortExpression.Equals("") || Col.HeaderText.EndsWith("-a")
							|| Col.HeaderText.EndsWith("-d")))
						{
							HeaderText = ((DropDownList)Columns.Controls[i]).SelectedValue;
							if(ReplaceEnd(ref HeaderText, " ASC", ""))							
							{
								if(HeaderText.Equals(Col.HeaderText))
								{
									Col.HeaderText = HeaderText + "-a";
									Col.SortExpression += " ASC";
									break;
								}
							}
							else
							{
								if(ReplaceEnd(ref HeaderText, " DESC", "")
									&& HeaderText.Equals(Col.HeaderText))
								{
									Col.HeaderText = HeaderText + "-d";
									Col.SortExpression += " DESC";
									break;
								}
							}
						}
					}
				}
			}
			Load_Data(Grid, SrcTable, sMulSort);
		}

		static private void Load_Data(DataGrid Grid, DataTable SrcTable, String sOrder)
		{
			DataView dv;
			dv = new DataView(SrcTable);
			dv.Sort = sOrder;
							
			Grid.DataSource = dv;
			Grid.DataBind();
		}

		static private String CorrespondSortExp(DataGrid Grid, String HeaderText)
		{
			String tmpHeader = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				tmpHeader = HeaderText;
				if(ReplaceEnd(ref tmpHeader, " ASC", "")
					|| ReplaceEnd(ref tmpHeader, " DESC", ""))
				{
					if(Col.HeaderText.Equals(tmpHeader))
						return (Col.SortExpression + HeaderText.Replace(tmpHeader, ""));
				}
			}
			return "";
		}

		static public void Refresh(DataGrid Grid, DataTable SrcTable)
		{
			String SortExpression;
			//if(SortType == SORTTYPE.MultiSort)
			//DataGridSort.sMulSort = "";
			foreach(DataGridColumn Col in Grid.Columns)
			{
				SortExpression = Col.SortExpression;
				if(ReplaceEnd(ref SortExpression, " ASC", "")
					|| ReplaceEnd(ref SortExpression, " DESC", ""))
					Col.SortExpression = SortExpression;
				SortExpression = Col.HeaderText;
				if(ReplaceEnd(ref SortExpression, "-a", "")
					|| ReplaceEnd(ref SortExpression, "-d", ""))
					Col.HeaderText = SortExpression;
			}
			Load_Data(Grid, SrcTable, "");
		}

		/// <summary>
		/// Event sang trang
		/// </summary>
		/// <param name="Grid">DataGrid thể hiện dữ liệu</param>
		/// <param name="SrcTable">Table chứa dữ liệu cần thể hiện trên Grid</param>
		/// <param name="e">Sự kiện phát sinh khi chuyển trang</param>
		/// <param name="bMulSort">Chọn MultiSort</param>
		static public void Grid_IndexChanged(DataGrid Grid, DataTable SrcTable, DataGridPageChangedEventArgs e)
		{
			try
			{
				Grid.CurrentPageIndex = e.NewPageIndex;
				Load_Data(Grid, SrcTable, sMulSort);
			}
			catch(Exception ee)
			{
				Grid.CurrentPageIndex = 0;
				Load_Data(Grid, SrcTable, sMulSort);
			}
		}

		static public void AddItemColumn(DropDownList Column, DataGrid Grid)
		{
			Column.Items.Clear();
			Column.Items.Add("");
			foreach(DataGridColumn Col in Grid.Columns)
			{
				if(!Col.SortExpression.Equals(""))
				{
					Column.Items.Add(Col.HeaderText + " ASC");
					Column.Items.Add(Col.HeaderText + " DESC");
				}
			}
		}

		static public void AddItemColumn(ColumnList Columns, DataGrid Grid)
		{
			for(int i=0; i<Columns.Controls.Count; i++)
			{
				if(Columns.Controls[i] is DropDownList)
				{
					((DropDownList)Columns.Controls[i]).Items.Clear();
					((DropDownList)Columns.Controls[i]).Items.Add("");
					foreach(DataGridColumn Col in Grid.Columns)
					{
						if(!Col.SortExpression.Equals(""))
						{
							((DropDownList)Columns.Controls[i]).Items.Add(Col.HeaderText+" ASC");
							((DropDownList)Columns.Controls[i]).Items.Add(Col.HeaderText+" DESC");
						}
					}
				}
			}
		}
	}
}
