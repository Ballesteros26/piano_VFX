using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a contract that exposes properties that automatically generate fields that are based on data in a data-bound control.</summary>
	// Token: 0x020002D5 RID: 725
	public interface IFieldControl
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.UI.IAutoFieldGenerator" /> interface, which is the interface that generates fields in a data-bound control.</summary>
		/// <returns>The interface that generates fields in data-bound controls.</returns>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001B75 RID: 7029
		// (set) Token: 0x06001B76 RID: 7030
		IAutoFieldGenerator FieldsGenerator { get; set; }
	}
}
