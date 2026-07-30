using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.INVOKEKIND" /> instead.</summary>
	// Token: 0x02000905 RID: 2309
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.INVOKEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum INVOKEKIND
	{
		/// <summary>The member is called using a normal function invocation syntax.</summary>
		// Token: 0x04002D58 RID: 11608
		INVOKE_FUNC = 1,
		/// <summary>The function is invoked using a normal property-access syntax.</summary>
		// Token: 0x04002D59 RID: 11609
		INVOKE_PROPERTYGET,
		/// <summary>The function is invoked using a property value assignment syntax.</summary>
		// Token: 0x04002D5A RID: 11610
		INVOKE_PROPERTYPUT = 4,
		/// <summary>The function is invoked using a property reference assignment syntax.</summary>
		// Token: 0x04002D5B RID: 11611
		INVOKE_PROPERTYPUTREF = 8
	}
}
