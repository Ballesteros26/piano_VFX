using System;

namespace System.Web.ModelBinding
{
	/// <summary>Enumerates model-binding behavior options.</summary>
	// Token: 0x02000517 RID: 1303
	public enum BindingBehavior
	{
		/// <summary>The property should be model bound if a value is available from the value provider.</summary>
		// Token: 0x04001F42 RID: 8002
		Optional,
		/// <summary>The property should be excluded from model binding.</summary>
		// Token: 0x04001F43 RID: 8003
		Never,
		/// <summary>The property is required for model binding.</summary>
		// Token: 0x04001F44 RID: 8004
		Required
	}
}
