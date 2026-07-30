using System;

namespace System.Web.UI
{
	/// <summary>Specifies a <see cref="T:System.Web.UI.ControlBuilder" /> class for building a custom control within the ASP.NET parser. This class cannot be inherited.</summary>
	// Token: 0x02000157 RID: 343
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ControlBuilderAttribute : Attribute
	{
		/// <summary>Specifies the control builder for a custom control.</summary>
		/// <param name="builderType">The control builder type </param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x0002B0DA File Offset: 0x000292DA
		public ControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control associated with the attribute. This property is read-only.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the control associated with the attribute.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002B0E9 File Offset: 0x000292E9
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. </summary>
		/// <returns>A 32-bit signed integer representing the hash code; otherwise, 0, if the <see cref="P:System.Web.UI.ControlBuilderAttribute.BuilderType" /> is null.</returns>
		// Token: 0x06000F24 RID: 3876 RVA: 0x0002B0F1 File Offset: 0x000292F1
		public override int GetHashCode()
		{
			if (!(this.BuilderType != null))
			{
				return 0;
			}
			return this.BuilderType.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.UI.ControlBuilderAttribute" /> is identical to the specified object. </summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Web.UI.ControlBuilderAttribute" /> and is identical to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />.</param>
		// Token: 0x06000F25 RID: 3877 RVA: 0x0002B10E File Offset: 0x0002930E
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is ControlBuilderAttribute && ((ControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		/// <summary>Determines whether the current control builder is the default.</summary>
		/// <returns>true if the current control builder is the default.</returns>
		// Token: 0x06000F26 RID: 3878 RVA: 0x0002B139 File Offset: 0x00029339
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ControlBuilderAttribute.Default);
		}

		/// <summary>Specifies the new <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. By default, the new object is set to null. This field is read-only.</summary>
		// Token: 0x0400122F RID: 4655
		public static readonly ControlBuilderAttribute Default = new ControlBuilderAttribute(null);

		// Token: 0x04001230 RID: 4656
		private Type builderType;
	}
}
