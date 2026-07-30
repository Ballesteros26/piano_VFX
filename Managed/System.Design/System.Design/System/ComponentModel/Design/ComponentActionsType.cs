using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the type of object-bound smart tag with respect to how it was associated with the component.</summary>
	// Token: 0x020000FC RID: 252
	public enum ComponentActionsType
	{
		/// <summary>Both types of smart tags.</summary>
		// Token: 0x0400017E RID: 382
		All,
		/// <summary>Pull model smart tags only.</summary>
		// Token: 0x0400017F RID: 383
		Component,
		/// <summary>Push model smart tags only.</summary>
		// Token: 0x04000180 RID: 384
		Service
	}
}
