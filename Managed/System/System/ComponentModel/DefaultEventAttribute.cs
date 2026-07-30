using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x02000259 RID: 601
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to. </param>
		// Token: 0x06001344 RID: 4932 RVA: 0x000512AA File Offset: 0x0004F4AA
		public DefaultEventAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x000512B9 File Offset: 0x0004F4B9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001346 RID: 4934 RVA: 0x000512C4 File Offset: 0x0004F4C4
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001347 RID: 4935 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012A8 RID: 4776
		private readonly string name;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x040012A9 RID: 4777
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
