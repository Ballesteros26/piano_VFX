using System;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for adding and editing data fields in a data-bound control, such as <see cref="T:System.Web.UI.WebControls.GridView" /> or <see cref="T:System.Web.UI.WebControls.DetailsView" />.</summary>
	// Token: 0x02000189 RID: 393
	public abstract class DataControlFieldDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataControlFieldDesigner" /> class. </summary>
		// Token: 0x06000B3A RID: 2874 RVA: 0x00009519 File Offset: 0x00007719
		protected DataControlFieldDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets the default text that is displayed for the data field in the fields editor.</summary>
		/// <returns>The default text that is displayed for the data field in the fields editor.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B3B RID: 2875
		public abstract string DefaultNodeText { get; }

		/// <summary>Gets the service provider implementation that is used by the fields editor.</summary>
		/// <returns>The service provider implementation, typically provided by the design host, which can be used to obtain additional design-time services.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0000970B File Offset: 0x0000790B
		protected IServiceProvider ServiceProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether schema information is used to load the data fields.</summary>
		/// <returns>true if schema information is used to load the data fields; otherwise, false.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B3D RID: 2877
		public abstract bool UsesSchema { get; }

		/// <summary>When overridden in a derived class, creates an empty field object.</summary>
		/// <returns>An empty field object.</returns>
		// Token: 0x06000B3E RID: 2878
		public abstract DataControlField CreateField();

		/// <summary>When overridden in a derived class, creates a new field object using the specified data field information.</summary>
		/// <returns>A new field object.</returns>
		/// <param name="fieldSchema">Schema information that contains the structure of a data field.</param>
		// Token: 0x06000B3F RID: 2879
		public abstract DataControlField CreateField(IDataSourceFieldSchema fieldSchema);

		/// <summary>When overridden in a derived class, creates a <see cref="T:System.Web.UI.WebControls.TemplateField" /> field for the specified data field.</summary>
		/// <returns>The new template field.</returns>
		/// <param name="dataControlField">The data field.</param>
		/// <param name="dataBoundControl">The data-bound control that contains the data field.</param>
		// Token: 0x06000B40 RID: 2880
		public abstract TemplateField CreateTemplateField(DataControlField dataControlField, DataBoundControl dataBoundControl);

		/// <summary>Gets an unique ID for a control that is created when a data field is converted into a <see cref="T:System.Web.UI.WebControls.TemplateField" />.</summary>
		/// <returns>A unique ID for the control.</returns>
		/// <param name="controlType">The type of the control that will be created.</param>
		/// <param name="mode">The data entry mode for the control.</param>
		// Token: 0x06000B41 RID: 2881 RVA: 0x0000970B File Offset: 0x0000790B
		protected string GetNewDataSourceName(Type controlType, DataBoundControlMode mode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, gets the name that is displayed for the data field in the fields editor.</summary>
		/// <returns>The name that is displayed for the data field in the fields editor.</returns>
		/// <param name="dataControlField">The data field.</param>
		// Token: 0x06000B42 RID: 2882
		public abstract string GetNodeText(DataControlField dataControlField);

		/// <summary>Gets the service object of the specified type.</summary>
		/// <returns>The service object of the specified type.</returns>
		/// <param name="serviceType">The type of service object to get.</param>
		// Token: 0x06000B43 RID: 2883 RVA: 0x0000970B File Offset: 0x0000790B
		protected object GetService(Type serviceType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.ITemplate" /> object that contains the template markup for the specified data-bound control.</summary>
		/// <returns>An object that contains the template markup for the specified data-bound control.</returns>
		/// <param name="control">The data-bound control.</param>
		/// <param name="templateContent">The template markup.</param>
		// Token: 0x06000B44 RID: 2884 RVA: 0x0000970B File Offset: 0x0000790B
		protected ITemplate GetTemplate(DataBoundControl control, string templateContent)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object for the specified data field.</summary>
		/// <returns>The new template field.</returns>
		/// <param name="dataControlField">The data field.</param>
		/// <param name="dataBoundControl">The data-bound control that contains the data field.</param>
		// Token: 0x06000B45 RID: 2885 RVA: 0x0000970B File Offset: 0x0000790B
		protected TemplateField GetTemplateField(DataControlField dataControlField, DataBoundControl dataBoundControl)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the data field is enabled in the fields editor.</summary>
		/// <returns>A value that indicates whether the data field is enabled in the fields editor.</returns>
		/// <param name="parent">The data-bound control that contains the data field.</param>
		// Token: 0x06000B46 RID: 2886
		public abstract bool IsEnabled(DataBoundControl parent);
	}
}
