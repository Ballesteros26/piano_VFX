using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides design-time support in a design host for the <see cref="T:System.Web.UI.DataSourceControl" /> class.</summary>
	// Token: 0x0200006B RID: 107
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceDesigner : ControlDesigner, IDataSourceDesigner
	{
		/// <summary>Provides a value that indicates whether two schemas are equal.</summary>
		/// <returns>true if both schemas are equivalent; otherwise, false.</returns>
		/// <param name="schema1">The first schema to compare (derived from the <see cref="T:System.Web.UI.Design.IDataSourceSchema" />).</param>
		/// <param name="schema2">The second schema to compare.</param>
		// Token: 0x06000349 RID: 841 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static bool SchemasEquivalent(IDataSourceSchema schema1, IDataSourceSchema schema2)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a value that determines whether two schema views are equal.</summary>
		/// <returns>true if both views are equivalent; otherwise, false. </returns>
		/// <param name="viewSchema1">The first view to compare (derived from the <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" />).</param>
		/// <param name="viewSchema2">The second view to compare.</param>
		// Token: 0x0600034A RID: 842 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static bool ViewSchemasEquivalent(IDataSourceViewSchema viewSchema1, IDataSourceViewSchema viewSchema2)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when any property of the associated data source changes.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600034C RID: 844 RVA: 0x00008EFC File Offset: 0x000070FC
		// (remove) Token: 0x0600034D RID: 845 RVA: 0x00008F34 File Offset: 0x00007134
		public event EventHandler DataSourceChanged;

		/// <summary>Occurs after the schema has been refreshed.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600034E RID: 846 RVA: 0x00008F6C File Offset: 0x0000716C
		// (remove) Token: 0x0600034F RID: 847 RVA: 0x00008FA4 File Offset: 0x000071A4
		public event EventHandler SchemaRefreshed;

		/// <summary>Gets a list of items that are used to create an action list menu at design time.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object containing the action list items for the control designer.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.DataSourceDesigner.Configure" /> method can be called.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.Design.DataSourceDesigner.Configure" /> can be called; otherwise, false. The default is false.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool CanConfigure
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
		/// <returns>true if the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> can be called; otherwise, false. The default is false.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool CanRefreshSchema
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> event or the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> method occurs.</summary>
		/// <returns>true if events are being suppressed; otherwise, false.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected bool SuppressingDataSourceEvents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Launches the data source configuration utility in the design host.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to invoke this method in the base class.</exception>
		// Token: 0x06000354 RID: 852 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void Configure()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the HTML markup for displaying the associated data source control at design time.</summary>
		/// <returns>The markup for the design-time display.</returns>
		// Token: 0x06000355 RID: 853 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object that is identified by the view name.</summary>
		/// <returns>This implementation always returns null.</returns>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x06000356 RID: 854 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual DesignerDataSourceView GetView(string viewName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an array of the view names that are available in this data source.</summary>
		/// <returns>An array of view names.</returns>
		// Token: 0x06000357 RID: 855 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string[] GetViewNames()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> event when the properties of the data source have changed and the <see cref="P:System.Web.UI.Design.DataSourceDesigner.SuppressingDataSourceEvents" /> value is false.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object provided by the calling object.</param>
		// Token: 0x06000358 RID: 856 RVA: 0x00008FD9 File Offset: 0x000071D9
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			if (this.DataSourceChanged != null)
			{
				this.DataSourceChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.DataSourceDesigner.SchemaRefreshed" /> event when the schema of the data source has changed and the <see cref="P:System.Web.UI.Design.DataSourceDesigner.SuppressingDataSourceEvents" /> value is false.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
		// Token: 0x06000359 RID: 857 RVA: 0x00008FF0 File Offset: 0x000071F0
		protected virtual void OnSchemaRefreshed(EventArgs e)
		{
			if (this.SchemaRefreshed != null)
			{
				this.SchemaRefreshed(this, e);
			}
		}

		/// <summary>Refreshes the schema from the data source, while optionally suppressing events.</summary>
		/// <param name="preferSilent">true to allow events when refreshing the schema; false to disable the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> and <see cref="E:System.Web.UI.Design.DataSourceDesigner.SchemaRefreshed" /> events when refreshing the schema.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to invoke this method in the base class.</exception>
		// Token: 0x0600035A RID: 858 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void RefreshSchema(bool preferSilent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores data source events after the data source events have been suppressed.</summary>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to invoke this method in the base class.</exception>
		// Token: 0x0600035B RID: 859 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void ResumeDataSourceEvents()
		{
			throw new NotImplementedException();
		}

		/// <summary>Postpones all data source events until after the <see cref="M:System.Web.UI.Design.DataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
		// Token: 0x0600035C RID: 860 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SuppressDataSourceEvents()
		{
			throw new NotImplementedException();
		}
	}
}
