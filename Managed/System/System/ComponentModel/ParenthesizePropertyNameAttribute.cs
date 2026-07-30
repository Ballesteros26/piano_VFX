using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x020002FA RID: 762
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x06001892 RID: 6290 RVA: 0x00068B5D File Offset: 0x00066D5D
		public ParenthesizePropertyNameAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">true if the name should be enclosed in parentheses; otherwise, false. </param>
		// Token: 0x06001893 RID: 6291 RVA: 0x00068B66 File Offset: 0x00066D66
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>true if the property is displayed with parentheses; otherwise, false.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00068B75 File Offset: 0x00066D75
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <returns>true if equal; otherwise, false.</returns>
		/// <param name="o">The object to be compared. </param>
		// Token: 0x06001895 RID: 6293 RVA: 0x00068B7D File Offset: 0x00066D7D
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06001896 RID: 6294 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x06001897 RID: 6295 RVA: 0x00068B9C File Offset: 0x00066D9C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x04001433 RID: 5171
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x04001434 RID: 5172
		private bool needParenthesis;
	}
}
