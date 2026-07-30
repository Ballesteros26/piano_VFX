using System;

namespace System.Web.UI
{
	/// <summary>Allows a <see cref="T:System.Web.UI.TemplateControl" />-derived class to specify the control builder used at the top level of the builder tree when parsing the file. This class cannot be inherited.</summary>
	// Token: 0x02000161 RID: 353
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FileLevelControlBuilderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> class.</summary>
		/// <param name="builderType">The <see cref="T:System.Type" /> of the control builder used when parsing the file.</param>
		// Token: 0x06000F41 RID: 3905 RVA: 0x0002B2BC File Offset: 0x000294BC
		public FileLevelControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control builder used when parsing the file. This property is read-only. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the control builder used when parsing the file.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002B2CB File Offset: 0x000294CB
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002B2D3 File Offset: 0x000294D3
		public override int GetHashCode()
		{
			return this.builderType.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance. </param>
		// Token: 0x06000F44 RID: 3908 RVA: 0x0002B2E0 File Offset: 0x000294E0
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is FileLevelControlBuilderAttribute && ((FileLevelControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		/// <summary>Determines whether the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object is the default.</summary>
		/// <returns>true if the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> is the default; otherwise, false.</returns>
		// Token: 0x06000F45 RID: 3909 RVA: 0x0002B30B File Offset: 0x0002950B
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FileLevelControlBuilderAttribute.Default);
		}

		/// <summary>Specifies the new <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object. By default, the new object is set to null. This field is read-only.</summary>
		// Token: 0x04001245 RID: 4677
		public static readonly FileLevelControlBuilderAttribute Default = new FileLevelControlBuilderAttribute(null);

		// Token: 0x04001246 RID: 4678
		private Type builderType;
	}
}
