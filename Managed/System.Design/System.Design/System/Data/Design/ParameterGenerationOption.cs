using System;

namespace System.Data.Design
{
	/// <summary>Sets the type of parameters that are generated in a typed <see cref="T:System.Data.DataSet" /> class.</summary>
	// Token: 0x020000EB RID: 235
	public enum ParameterGenerationOption
	{
		/// <summary>Parameters in the typed dataset are CLR types.</summary>
		// Token: 0x0400015C RID: 348
		ClrTypes,
		/// <summary>Parameters in the typed dataset are Sql types.</summary>
		// Token: 0x0400015D RID: 349
		SqlTypes,
		/// <summary>Parameters in the typed dataset are all of <see cref="T:System.Object" />.</summary>
		// Token: 0x0400015E RID: 350
		Objects
	}
}
