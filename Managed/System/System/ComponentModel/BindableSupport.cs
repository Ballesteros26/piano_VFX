using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x02000235 RID: 565
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x04001250 RID: 4688
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x04001251 RID: 4689
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04001252 RID: 4690
		Default
	}
}
