using System;

namespace System.Web.UI
{
	/// <summary>Defines an attribute applied to properties that contain ID references. This class cannot be inherited.</summary>
	// Token: 0x0200016D RID: 365
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IDReferencePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class.</summary>
		// Token: 0x06000F5C RID: 3932 RVA: 0x0002B47E File Offset: 0x0002967E
		public IDReferencePropertyAttribute()
			: this(typeof(Control))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> class using the specified type.</summary>
		/// <param name="referencedControlType">A <see cref="T:System.Type" /> that specifies the type of the control represented by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied.</param>
		// Token: 0x06000F5D RID: 3933 RVA: 0x0002B490 File Offset: 0x00029690
		public IDReferencePropertyAttribute(Type referencedControlType)
		{
			this._referencedControlType = referencedControlType;
		}

		/// <summary>Gets the type of the controls allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> attribute is applied.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of control allowed by the property to which the <see cref="T:System.Web.UI.IDReferencePropertyAttribute" /> is applied. The default is <see cref="T:System.Web.UI.Control" />.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0002B49F File Offset: 0x0002969F
		public Type ReferencedControlType
		{
			get
			{
				return this._referencedControlType;
			}
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F5F RID: 3935 RVA: 0x0002B4A7 File Offset: 0x000296A7
		public override int GetHashCode()
		{
			if (!(this.ReferencedControlType != null))
			{
				return 0;
			}
			return this.ReferencedControlType.GetHashCode();
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000F60 RID: 3936 RVA: 0x0002B4C4 File Offset: 0x000296C4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IDReferencePropertyAttribute idreferencePropertyAttribute = obj as IDReferencePropertyAttribute;
			return idreferencePropertyAttribute != null && this.ReferencedControlType == idreferencePropertyAttribute.ReferencedControlType;
		}

		// Token: 0x04001311 RID: 4881
		private Type _referencedControlType;
	}
}
