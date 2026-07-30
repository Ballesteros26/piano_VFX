using System;

namespace System.Runtime
{
	/// <summary>Indicates that the .NET Framework class library method to which this attribute is applied is unlikely to be affected by servicing releases, and therefore is eligible to be inlined across Native Image Generator (NGen) images.</summary>
	// Token: 0x020006B4 RID: 1716
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" /> class.</summary>
		/// <param name="reason">The reason why the method to which the <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" /> attribute is applied is considered to be eligible for inlining across Native Image Generator (NGen) images.</param>
		// Token: 0x06004960 RID: 18784 RVA: 0x001079B3 File Offset: 0x00105BB3
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.m_reason = reason;
		}

		/// <summary>Gets the reason why the method to which this attribute is applied is considered to be eligible for inlining across Native Image Generator (NGen) images.</summary>
		/// <returns>The reason why the method is considered to be eligible for inlining across NGen images.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x001079C2 File Offset: 0x00105BC2
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x00002180 File Offset: 0x00000380
		private TargetedPatchingOptOutAttribute()
		{
		}

		// Token: 0x04002671 RID: 9841
		private string m_reason;
	}
}
