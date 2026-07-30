using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a provider interface for connecting two server controls using a single field of data. </summary>
	// Token: 0x02000467 RID: 1127
	public interface IWebPartField
	{
		/// <summary>Gets the schema information for a data field that is used to share data between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the schema information for the data field.</returns>
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060033F8 RID: 13304
		PropertyDescriptor Schema { get; }

		/// <summary>Returns the value of the field that is being used by the interface as the basis of a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="callback">A <see cref="T:System.Web.UI.WebControls.WebParts.FieldCallback" /> delegate that contains the address of a method that receives the data.</param>
		// Token: 0x060033F9 RID: 13305
		void GetFieldValue(FieldCallback callback);
	}
}
