using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x020002F8 RID: 760
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>true if an obfuscation tool should remove the attribute after processing; otherwise, false. The default is true.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007ED25 File Offset: 0x0007CF25
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x0007ED2D File Offset: 0x0007CF2D
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>true if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, false. The default is true.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x0007ED36 File Offset: 0x0007CF36
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x0007ED3E File Offset: 0x0007CF3E
		public bool Exclude
		{
			get
			{
				return this.m_exclude;
			}
			set
			{
				this.m_exclude = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>true if the attribute is to apply to the members of the type; otherwise, false. The default is true.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0007ED47 File Offset: 0x0007CF47
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x0007ED4F File Offset: 0x0007CF4F
		public bool ApplyToMembers
		{
			get
			{
				return this.m_applyToMembers;
			}
			set
			{
				this.m_applyToMembers = value;
			}
		}

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options. </summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0007ED58 File Offset: 0x0007CF58
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0007ED60 File Offset: 0x0007CF60
		public string Feature
		{
			get
			{
				return this.m_feature;
			}
			set
			{
				this.m_feature = value;
			}
		}

		// Token: 0x0400128D RID: 4749
		private bool m_strip = true;

		// Token: 0x0400128E RID: 4750
		private bool m_exclude = true;

		// Token: 0x0400128F RID: 4751
		private bool m_applyToMembers = true;

		// Token: 0x04001290 RID: 4752
		private string m_feature = "all";
	}
}
