using System;

namespace System.Web.UI
{
	/// <summary>Defines a metadata attribute that is used to specify the number of allowed instances of a template. This class cannot be inherited.</summary>
	// Token: 0x02000195 RID: 405
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TemplateInstanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class with the specified <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</summary>
		/// <param name="instances">A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</param>
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002B70B File Offset: 0x0002990B
		public TemplateInstanceAttribute(TemplateInstance instances)
		{
			this._instances = instances;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</summary>
		/// <returns>A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0002B71A File Offset: 0x0002991A
		public TemplateInstance Instances
		{
			get
			{
				return this._instances;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>true if value is both a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object; otherwise false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002B724 File Offset: 0x00029924
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			TemplateInstanceAttribute templateInstanceAttribute = obj as TemplateInstanceAttribute;
			return templateInstanceAttribute != null && templateInstanceAttribute.Instances == this.Instances;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>The hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</returns>
		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002B751 File Offset: 0x00029951
		public override int GetHashCode()
		{
			return this._instances.GetHashCode();
		}

		/// <summary>Returns a value indicating if the current <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object is the same as the default <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>true if the value of the current instance of <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> is the default; otherwise, false.</returns>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002B764 File Offset: 0x00029964
		public override bool IsDefaultAttribute()
		{
			return this.Equals(TemplateInstanceAttribute.Default);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated multiple times. This field is read-only.</summary>
		// Token: 0x0400132F RID: 4911
		public static readonly TemplateInstanceAttribute Multiple = new TemplateInstanceAttribute(TemplateInstance.Multiple);

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated a single time. This field is read-only.</summary>
		// Token: 0x04001330 RID: 4912
		public static readonly TemplateInstanceAttribute Single = new TemplateInstanceAttribute(TemplateInstance.Single);

		/// <summary>Defines the default value for the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class. This field is read-only. </summary>
		// Token: 0x04001331 RID: 4913
		public static readonly TemplateInstanceAttribute Default = TemplateInstanceAttribute.Multiple;

		// Token: 0x04001332 RID: 4914
		private TemplateInstance _instances;
	}
}
