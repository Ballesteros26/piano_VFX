using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x02000239 RID: 569
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">true if a property or event can be modified at design time; otherwise, false. The default is true. </param>
		// Token: 0x06001280 RID: 4736 RVA: 0x0004DDEE File Offset: 0x0004BFEE
		public BrowsableAttribute(bool browsable)
		{
			this.browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>true if the object is browsable; otherwise, false.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004DE04 File Offset: 0x0004C004
		public bool Browsable
		{
			get
			{
				return this.browsable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001282 RID: 4738 RVA: 0x0004DE0C File Offset: 0x0004C00C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			return browsableAttribute != null && browsableAttribute.Browsable == this.browsable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001283 RID: 4739 RVA: 0x0004DE39 File Offset: 0x0004C039
		public override int GetHashCode()
		{
			return this.browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001284 RID: 4740 RVA: 0x0004DE46 File Offset: 0x0004C046
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		/// <summary>Specifies that a property or event can be modified at design time. This static field is read-only.</summary>
		// Token: 0x04001263 RID: 4707
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This static field is read-only.</summary>
		// Token: 0x04001264 RID: 4708
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This static field is read-only.</summary>
		// Token: 0x04001265 RID: 4709
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;

		// Token: 0x04001266 RID: 4710
		private bool browsable = true;
	}
}
