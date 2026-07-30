using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Defines methods for retrieving data-store schema information.</summary>
	// Token: 0x02000171 RID: 369
	public interface IDesignerDataSchema
	{
		/// <summary>Gets a collection of specified schema items.</summary>
		/// <returns>A collection of schema objects of the specified type.</returns>
		/// <param name="schemaClass">The schema objects to return.</param>
		// Token: 0x06000AFE RID: 2814
		ICollection GetSchemaItems(DesignerDataSchemaClass schemaClass);

		/// <summary>Returns a value indicating whether the data store contains the specified data-schema object.</summary>
		/// <returns>true if the data store supports the specified data-schema object; otherwise, false.</returns>
		/// <param name="schemaClass">The schema objects to return.</param>
		// Token: 0x06000AFF RID: 2815
		bool SupportsSchemaClass(DesignerDataSchemaClass schemaClass);
	}
}
