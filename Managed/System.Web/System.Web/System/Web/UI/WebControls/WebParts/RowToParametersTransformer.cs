using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Transforms data in a Web Parts connection from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartRow" /> interface to a consumer expecting to receive data from a provider that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebPartParameters" /> interface.</summary>
	// Token: 0x020007BB RID: 1979
	[WebPartTransformer(typeof(IWebPartRow), typeof(IWebPartParameters))]
	public sealed class RowToParametersTransformer : WebPartTransformer, IWebPartParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" /> class. </summary>
		// Token: 0x06004FCF RID: 20431 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public RowToParametersTransformer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the list of names in the consumer that will receive the values from the provider.</summary>
		/// <returns>An array of <see cref="T:System.String" /> values representing the consumer fields.</returns>
		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string[] ConsumerFieldNames
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

		/// <summary>Gets or sets the list of field names from the provider.</summary>
		/// <returns>An array of <see cref="T:System.String" /> values representing the provider fields.</returns>
		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004FD3 RID: 20435 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string[] ProviderFieldNames
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

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0000E80B File Offset: 0x0000CA0B
		PropertyDescriptorCollection IWebPartParameters.get_Schema()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides an ASP.NET control that allows the user to configure a <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" /> transformer in the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> zone.</summary>
		/// <returns>An ASP.NET control that displays a user interface (UI) that allows the user to configure a <see cref="T:System.Web.UI.WebControls.WebParts.RowToParametersTransformer" />.</returns>
		// Token: 0x06004FD5 RID: 20437 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override Control CreateConfigurationControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal override void LoadConfigurationState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal override object SaveConfigurationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value of the data from the connection provider.</summary>
		/// <param name="callback">The delegate instance to be used when retrieving a value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> equals null.</exception>
		// Token: 0x06004FD8 RID: 20440 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IWebPartParameters.GetParametersData(ParametersCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the property descriptors for the properties that the consumer receives when the <see cref="M:System.Web.UI.WebControls.WebParts.IWebPartParameters.GetParametersData(System.Web.UI.WebControls.WebParts.ParametersCallback)" /> method is called.</summary>
		/// <param name="schema">The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object returned by <see cref="P:System.Web.UI.WebControls.WebParts.RowToParametersTransformer.System#Web#UI#WebControls#WebParts#IWebPartParameters#Schema" />.</param>
		// Token: 0x06004FD9 RID: 20441 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IWebPartParameters.SetConsumerSchema(PropertyDescriptorCollection schema)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides an object for transforming the data.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data to be transformed.</returns>
		/// <param name="providerData">The provider data to be transformed.</param>
		// Token: 0x06004FDA RID: 20442 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override object Transform(object providerData)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
