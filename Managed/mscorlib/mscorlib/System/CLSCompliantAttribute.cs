using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates whether a program element is compliant with the Common Language Specification (CLS). This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000139 RID: 313
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.CLSCompliantAttribute" /> class with a Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <param name="isCompliant">true if CLS-compliant; otherwise, false. </param>
		// Token: 0x06000B94 RID: 2964 RVA: 0x00035ADB File Offset: 0x00033CDB
		public CLSCompliantAttribute(bool isCompliant)
		{
			this.m_compliant = isCompliant;
		}

		/// <summary>Gets the Boolean value indicating whether the indicated program element is CLS-compliant.</summary>
		/// <returns>true if the program element is CLS-compliant; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00035AEA File Offset: 0x00033CEA
		public bool IsCompliant
		{
			get
			{
				return this.m_compliant;
			}
		}

		// Token: 0x040007D4 RID: 2004
		private bool m_compliant;
	}
}
