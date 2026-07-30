using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> events of a <see cref="T:System.Web.UI.WebControls.DataList" /> control. </summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains the event data. </param>
	// Token: 0x0200029A RID: 666
	// (Invoke) Token: 0x06001AA5 RID: 6821
	public delegate void DataListCommandEventHandler(object source, DataListCommandEventArgs e);
}
