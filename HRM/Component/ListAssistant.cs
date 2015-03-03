using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// This class provides the methods to pupolate list, get selections and set item selections.
/// the list is the class that listbox, dropdownlist, checkboxlist, radiobuttonlist and bulletedlist derived from
/// </summary>
public class ListAssistant
{
	public ListAssistant()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //bind a list with a table data source
    public void PopulateList(System.Web.UI.WebControls.ListControl list, string valueDataName, string textDataName, DataTable dataTable)
    {
        list.DataSource = dataTable;
        list.DataValueField = valueDataName;
        list.DataTextField = textDataName;
        list.DataBind();
        //DropDownList is a special case, which needs an empty item
        if (list.GetType().ToString().IndexOf("DropDownList")>-1)
        {
            list.Items.Add(new ListItem("", ""));
            list.SelectedIndex = list.Items.IndexOf(list.Items.FindByValue(""));
        }
    }

    //build a comma seperated string for all selections (values or text)
    public string GetListSelections(System.Web.UI.WebControls.ListControl list, string collectValueOrText)
    {
        ListItemCollection ListItems = list.Items;
        string SelectedItems = "";
        foreach (ListItem itm in ListItems)
        {
            if (itm.Selected)
            {
                if (collectValueOrText.ToLower() == "value")
                    SelectedItems += itm.Value + ", ";
                else
                    SelectedItems += itm.Text + ", ";
            }
        }
        if (SelectedItems.Length > 0)
        {
            SelectedItems = SelectedItems.Substring(0, SelectedItems.Length - 2);//remove trailing ", "
        }
        return SelectedItems;
    }

    //Determine item selections in a listcontrol based a comma separated selection value string passed in
    public void SetListSelections(System.Web.UI.WebControls.ListControl list, string csvValueString)
    {
        //make sure no existing selections in the list
        list.ClearSelection();
        //Place values in the array selecteditem
        string[] SelectedItem = csvValueString.Split(new char[] { ',' });
        //Loop through the list to set the selections
        for (int i = 0; i < SelectedItem.Length; i++)
        {
            foreach (ListItem itm in list.Items)
            {
                if (itm.Value.ToLower() == SelectedItem[i].Trim().ToLower())
                {
                    itm.Selected = true;
                }
            }
        }
    }

}
