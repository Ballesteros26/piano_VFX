using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the context keyword for a class or member. This class cannot be inherited.</summary>
	// Token: 0x02000322 RID: 802
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		// Token: 0x06001979 RID: 6521 RVA: 0x00002C6F File Offset: 0x00000E6F
		public HelpKeywordAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		/// <param name="keyword">The Help keyword value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null.</exception>
		// Token: 0x0600197A RID: 6522 RVA: 0x0006A311 File Offset: 0x00068511
		public HelpKeywordAttribute(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			this.contextKeyword = keyword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class from the given type. </summary>
		/// <param name="t">The type from which the Help keyword will be taken.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is null.</exception>
		// Token: 0x0600197B RID: 6523 RVA: 0x0006A32E File Offset: 0x0006852E
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.contextKeyword = t.FullName;
		}

		/// <summary>Gets the Help keyword supplied by this attribute.</summary>
		/// <returns>The Help keyword supplied by this attribute.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0006A356 File Offset: 0x00068556
		public string HelpKeyword
		{
			get
			{
				return this.contextKeyword;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> is equal to the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> to compare with the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</param>
		// Token: 0x0600197D RID: 6525 RVA: 0x0006A35E File Offset: 0x0006855E
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is HelpKeywordAttribute && ((HelpKeywordAttribute)obj).HelpKeyword == this.HelpKeyword);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</returns>
		// Token: 0x0600197E RID: 6526 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether the Help keyword is null.</summary>
		/// <returns>true if the Help keyword is null; otherwise, false.</returns>
		// Token: 0x0600197F RID: 6527 RVA: 0x0006A389 File Offset: 0x00068589
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HelpKeywordAttribute.Default);
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />. This field is read-only.</summary>
		// Token: 0x04001477 RID: 5239
		public static readonly HelpKeywordAttribute Default = new HelpKeywordAttribute();

		// Token: 0x04001478 RID: 5240
		private string contextKeyword;
	}
}
