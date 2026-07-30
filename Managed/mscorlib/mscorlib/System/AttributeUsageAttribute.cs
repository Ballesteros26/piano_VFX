using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the usage of another attribute class. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000130 RID: 304
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AttributeUsageAttribute" /> class with the specified list of <see cref="T:System.AttributeTargets" />, the <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> value, and the <see cref="P:System.AttributeUsageAttribute.Inherited" /> value.</summary>
		/// <param name="validOn">The set of values combined using a bitwise OR operation to indicate which program elements are valid. </param>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x00033A2A File Offset: 0x00031C2A
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this.m_attributeTarget = validOn;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00033A4B File Offset: 0x00031C4B
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this.m_attributeTarget = validOn;
			this.m_allowMultiple = allowMultiple;
			this.m_inherited = inherited;
		}

		/// <summary>Gets a set of values identifying which program elements that the indicated attribute can be applied to.</summary>
		/// <returns>One or several <see cref="T:System.AttributeTargets" /> values. The default is All.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00033A7A File Offset: 0x00031C7A
		public AttributeTargets ValidOn
		{
			get
			{
				return this.m_attributeTarget;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether more than one instance of the indicated attribute can be specified for a single program element.</summary>
		/// <returns>true if more than one instance is allowed to be specified; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00033A82 File Offset: 0x00031C82
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00033A8A File Offset: 0x00031C8A
		public bool AllowMultiple
		{
			get
			{
				return this.m_allowMultiple;
			}
			set
			{
				this.m_allowMultiple = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the indicated attribute can be inherited by derived classes and overriding members.</summary>
		/// <returns>true if the attribute can be inherited by derived classes and overriding members; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00033A93 File Offset: 0x00031C93
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00033A9B File Offset: 0x00031C9B
		public bool Inherited
		{
			get
			{
				return this.m_inherited;
			}
			set
			{
				this.m_inherited = value;
			}
		}

		// Token: 0x040007B7 RID: 1975
		internal AttributeTargets m_attributeTarget = AttributeTargets.All;

		// Token: 0x040007B8 RID: 1976
		internal bool m_allowMultiple;

		// Token: 0x040007B9 RID: 1977
		internal bool m_inherited = true;

		// Token: 0x040007BA RID: 1978
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
