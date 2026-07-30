using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member can be further detected by using an enumeration.</summary>
	// Token: 0x0200032D RID: 813
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		// Token: 0x06001EAD RID: 7853 RVA: 0x0009F79F File Offset: 0x0009D99F
		public XmlChoiceIdentifierAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		/// <param name="name">The member name that returns the enumeration used to detect a choice. </param>
		// Token: 0x06001EAE RID: 7854 RVA: 0x000A72D5 File Offset: 0x000A54D5
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the name of the field that returns the enumeration to use when detecting types.</summary>
		/// <returns>The name of a field that returns an enumeration.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000A72E4 File Offset: 0x000A54E4
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000A72FA File Offset: 0x000A54FA
		public string MemberName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000A7303 File Offset: 0x000A5503
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x000A730B File Offset: 0x000A550B
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x0400172F RID: 5935
		private string name;

		// Token: 0x04001730 RID: 5936
		private MemberInfo memberInfo;
	}
}
