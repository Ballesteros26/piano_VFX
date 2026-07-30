using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selected" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updated" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserted" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</param>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
	// Token: 0x020002F8 RID: 760
	// (Invoke) Token: 0x06001BD9 RID: 7129
	public delegate void ObjectDataSourceStatusEventHandler(object sender, ObjectDataSourceStatusEventArgs e);
}
