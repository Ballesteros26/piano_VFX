using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000850 RID: 2128
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		/// <summary>Gets or sets a value that indicates whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>true if exceptions that do not derive from the <see cref="T:System.Exception" /> class should appear wrapped with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object; otherwise, false.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x00126E23 File Offset: 0x00125023
		// (set) Token: 0x06005401 RID: 21505 RVA: 0x00126E2B File Offset: 0x0012502B
		public bool WrapNonExceptionThrows
		{
			get
			{
				return this.m_wrapNonExceptionThrows;
			}
			set
			{
				this.m_wrapNonExceptionThrows = value;
			}
		}

		// Token: 0x04002BA1 RID: 11169
		private bool m_wrapNonExceptionThrows;
	}
}
