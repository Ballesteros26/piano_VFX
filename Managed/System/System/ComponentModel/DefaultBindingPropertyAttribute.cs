using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default binding property for a component. This class cannot be inherited.</summary>
	// Token: 0x02000258 RID: 600
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using no parameters. </summary>
		// Token: 0x0600133E RID: 4926 RVA: 0x0005124D File Offset: 0x0004F44D
		public DefaultBindingPropertyAttribute()
		{
			this.name = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The name of the default binding property.</param>
		// Token: 0x0600133F RID: 4927 RVA: 0x0005125C File Offset: 0x0004F45C
		public DefaultBindingPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</summary>
		/// <returns>The name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0005126B File Offset: 0x0004F46B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance</param>
		// Token: 0x06001341 RID: 4929 RVA: 0x00051274 File Offset: 0x0004F474
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return defaultBindingPropertyAttribute != null && defaultBindingPropertyAttribute.Name == this.name;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001342 RID: 4930 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012A6 RID: 4774
		private readonly string name;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class.</summary>
		// Token: 0x040012A7 RID: 4775
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();
	}
}
