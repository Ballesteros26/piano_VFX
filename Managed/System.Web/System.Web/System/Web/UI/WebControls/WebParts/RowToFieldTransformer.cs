using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Transforms data in a Web Parts connection from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartRow" /> interface to a consumer expecting data through the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartField" /> interface.</summary>
	// Token: 0x020007B9 RID: 1977
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartField))]
	public sealed class RowToFieldTransformer : WebPartTransformer, IWebPartField
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" /> class. </summary>
		// Token: 0x06004FC1 RID: 20417 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RowToFieldTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the value to transform.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the field to transform.</returns>
		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004FC3 RID: 20419 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string FieldName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0000E80B File Offset: 0x0000CA0B
		PropertyDescriptor IWebPartField.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Displays an ASP.NET control that configures a <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" /> transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that configures a <see cref="T:System.Web.UI.WebControls.WebParts.RowToFieldTransformer" />.</returns>
		// Token: 0x06004FC5 RID: 20421 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal override object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the value of the field that is being used by the interface as the basis of a connection between two Web Parts controls.</summary>
		/// <param name="callback">The delegate instance to be used when retrieving a value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> equals null.</exception>
		// Token: 0x06004FC8 RID: 20424 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IWebPartField.GetFieldValue(FieldCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x06004FC9 RID: 20425 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override object Transform(object providerData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
