using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a provider interface for connecting two server controls using a single field of data.</summary>
	// Token: 0x02000469 RID: 1129
	public interface IWebPartRow
	{
		/// <summary>Gets the schema information for a data row that is used to share data between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060033FD RID: 13309
		PropertyDescriptorCollection Schema { get; }

		/// <summary>Returns the data for the row that is being used by the interface as the basis of a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="callback">A <see cref="T:System.Web.UI.WebControls.WebParts.RowCallback" /> delegate that contains the address of a method that receives the data.</param>
		// Token: 0x060033FE RID: 13310
		void GetRowData(RowCallback callback);
	}
}
