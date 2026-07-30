using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a provider interface for connecting two server controls using an entire table of data.</summary>
	// Token: 0x0200046A RID: 1130
	public interface IWebPartTable
	{
		/// <summary>Gets the schema information for a data table that is used to share data between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060033FF RID: 13311
		PropertyDescriptorCollection Schema { get; }

		/// <summary>Returns the data for the table that is being used by the interface as the basis of a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="callback">A <see cref="T:System.Web.UI.WebControls.WebParts.TableCallback" /> delegate that contains the address of a method that receives the data.</param>
		// Token: 0x06003400 RID: 13312
		void GetTableData(TableCallback callback);
	}
}
