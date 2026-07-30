using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x020002F9 RID: 761
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">true if the parent should be notified of changes; otherwise, false. </param>
		// Token: 0x0600188C RID: 6284 RVA: 0x00068AEF File Offset: 0x00066CEF
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>true if the parent property should be notified of changes; otherwise, false.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00068AFE File Offset: 0x00066CFE
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <returns>true if the object is the same as this object; otherwise, false.</returns>
		/// <param name="obj">The object to test for equality. </param>
		// Token: 0x0600188E RID: 6286 RVA: 0x00068B06 File Offset: 0x00066D06
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x0600188F RID: 6287 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x06001890 RID: 6288 RVA: 0x00068B2E File Offset: 0x00066D2E
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x0400142F RID: 5167
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04001430 RID: 5168
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x04001431 RID: 5169
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x04001432 RID: 5170
		private bool notifyParent;
	}
}
