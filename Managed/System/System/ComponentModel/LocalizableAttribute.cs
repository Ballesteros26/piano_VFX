using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x020002AC RID: 684
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">true if a property should be localized; otherwise, false. </param>
		// Token: 0x06001524 RID: 5412 RVA: 0x00053C14 File Offset: 0x00051E14
		public LocalizableAttribute(bool isLocalizable)
		{
			this.isLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>true if a property should be localized; otherwise, false.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00053C23 File Offset: 0x00051E23
		public bool IsLocalizable
		{
			get
			{
				return this.isLocalizable;
			}
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001526 RID: 5414 RVA: 0x00053C2B File Offset: 0x00051E2B
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001527 RID: 5415 RVA: 0x00053C40 File Offset: 0x00051E40
		public override bool Equals(object obj)
		{
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			return localizableAttribute != null && localizableAttribute.IsLocalizable == this.isLocalizable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06001528 RID: 5416 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001322 RID: 4898
		private bool isLocalizable;

		/// <summary>Specifies that a property should be localized. This static field is read-only.</summary>
		// Token: 0x04001323 RID: 4899
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This static field is read-only.</summary>
		// Token: 0x04001324 RID: 4900
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04001325 RID: 4901
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
