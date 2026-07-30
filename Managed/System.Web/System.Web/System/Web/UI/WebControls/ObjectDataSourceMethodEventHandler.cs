using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selecting" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updating" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserting" />, or <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleting" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />. </param>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data. </param>
	// Token: 0x020002F3 RID: 755
	// (Invoke) Token: 0x06001BC1 RID: 7105
	public delegate void ObjectDataSourceMethodEventHandler(object sender, ObjectDataSourceMethodEventArgs e);
}
