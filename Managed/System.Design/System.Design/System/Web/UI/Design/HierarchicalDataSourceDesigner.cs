using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
	// Token: 0x0200007E RID: 126
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HierarchicalDataSourceDesigner : ControlDesigner, IHierarchicalDataSourceDesigner
	{
		/// <summary>Occurs when any property of the associated data source changes.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000401 RID: 1025 RVA: 0x000091AC File Offset: 0x000073AC
		// (remove) Token: 0x06000402 RID: 1026 RVA: 0x000091E4 File Offset: 0x000073E4
		public event EventHandler DataSourceChanged;

		/// <summary>Occurs after the schema has been refreshed.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000403 RID: 1027 RVA: 0x0000921C File Offset: 0x0000741C
		// (remove) Token: 0x06000404 RID: 1028 RVA: 0x00009254 File Offset: 0x00007454
		public event EventHandler SchemaRefreshed;

		/// <summary>Gets the action list collection for the control designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object that contains the <see cref="T:System.ComponentModel.Design.DesignerActionList" /> items for the control designer.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" /> method can be called.</summary>
		/// <returns>This implementation always returns false.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool CanConfigure
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
		/// <returns>This implementation always returns false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual bool CanRefreshSchema
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Indicates whether data source events have been disabled.</summary>
		/// <returns>true if the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.DataSourceChanged" /> or <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.SchemaRefreshed" /> event has been disabled; otherwise, false.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected bool SuppressingDataSourceEvents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Launches the configuration wizard for the underlying data source.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06000409 RID: 1033 RVA: 0x00009289 File Offset: 0x00007489
		public virtual void Configure()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the HTML markup that is used to represent the control at design time.</summary>
		/// <returns>The HTML markup used to represent the control at design time.</returns>
		// Token: 0x0600040A RID: 1034 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetDesignTimeHtml()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> object.</returns>
		/// <param name="viewPath">The unique path to the block of data to use in creating the view.</param>
		// Token: 0x0600040B RID: 1035 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual DesignerHierarchicalDataSourceView GetView(string viewPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.DataSourceChanged" /> event when the properties of the data source have changed and the <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property value is false.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object provided by the calling object.</param>
		// Token: 0x0600040C RID: 1036 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.SchemaRefreshed" /> event when the schema of the data source has changed and the <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property value is false.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
		// Token: 0x0600040D RID: 1037 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void OnSchemaRefreshed(EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the schema of the data.</summary>
		/// <param name="preferSilent">This parameter is not used in this implementation. However, it should be supported in derived classes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600040E RID: 1038 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void RefreshSchema(bool preferSilent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores data source events after they have been suppressed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property is false.</exception>
		// Token: 0x0600040F RID: 1039 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void ResumeDataSourceEvents()
		{
			throw new NotImplementedException();
		}

		/// <summary>Postpones all data source events until after the <see cref="M:System.Web.UI.Design.HierarchicalDataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
		// Token: 0x06000410 RID: 1040 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SuppressDataSourceEvents()
		{
			throw new NotImplementedException();
		}
	}
}
