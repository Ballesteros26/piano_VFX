using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the contract a Web Parts control implements to pass a parameter value in a Web Parts connection.</summary>
	// Token: 0x02000468 RID: 1128
	public interface IWebPartParameters
	{
		/// <summary>Gets the property descriptors for the data to be received by the consumer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060033FA RID: 13306
		PropertyDescriptorCollection Schema { get; }

		/// <summary>Gets the value of the data from the connection provider.</summary>
		/// <param name="callback">The method to call to process the data from the provider.</param>
		// Token: 0x060033FB RID: 13307
		void GetParametersData(ParametersCallback callback);

		/// <summary>Sets the property descriptors for the properties that the consumer receives when calling the <see cref="M:System.Web.UI.WebControls.WebParts.IWebPartParameters.GetParametersData(System.Web.UI.WebControls.WebParts.ParametersCallback)" /> method.</summary>
		/// <param name="schema">The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> returned by <see cref="P:System.Web.UI.WebControls.WebParts.IWebPartParameters.Schema" />.</param>
		// Token: 0x060033FC RID: 13308
		void SetConsumerSchema(PropertyDescriptorCollection schema);
	}
}
