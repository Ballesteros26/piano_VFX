using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies that the assembly is not fully signed when created.</summary>
	// Token: 0x020002CE RID: 718
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> class.</summary>
		/// <param name="delaySign">true if the feature this attribute represents is activated; otherwise, false. </param>
		// Token: 0x06002039 RID: 8249 RVA: 0x0007DF1B File Offset: 0x0007C11B
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.m_delaySign = delaySign;
		}

		/// <summary>Gets a value indicating the state of the attribute.</summary>
		/// <returns>true if this assembly has been built as delay-signed; otherwise, false.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0007DF2A File Offset: 0x0007C12A
		public bool DelaySign
		{
			get
			{
				return this.m_delaySign;
			}
		}

		// Token: 0x0400116E RID: 4462
		private bool m_delaySign;
	}
}
