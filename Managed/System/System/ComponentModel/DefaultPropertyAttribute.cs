using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x0200025A RID: 602
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to. </param>
		// Token: 0x06001349 RID: 4937 RVA: 0x000512FB File Offset: 0x0004F4FB
		public DefaultPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0005130A File Offset: 0x0004F50A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600134B RID: 4939 RVA: 0x00051314 File Offset: 0x0004F514
		public override bool Equals(object obj)
		{
			DefaultPropertyAttribute defaultPropertyAttribute = obj as DefaultPropertyAttribute;
			return defaultPropertyAttribute != null && defaultPropertyAttribute.Name == this.name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600134C RID: 4940 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012AA RID: 4778
		private readonly string name;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x040012AB RID: 4779
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
