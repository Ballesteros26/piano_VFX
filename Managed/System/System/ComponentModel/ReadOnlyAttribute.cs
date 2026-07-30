using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x020002C5 RID: 709
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">true to show that the property this attribute is bound to is read-only; false to show that the property is read/write. </param>
		// Token: 0x0600166E RID: 5742 RVA: 0x00057B6C File Offset: 0x00055D6C
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.isReadOnly = isReadOnly;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>true if the property this attribute is bound to is read-only; false if the property is read/write.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00057B7B File Offset: 0x00055D7B
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06001670 RID: 5744 RVA: 0x00057B84 File Offset: 0x00055D84
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly == this.IsReadOnly;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x06001671 RID: 5745 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001672 RID: 5746 RVA: 0x00057BB1 File Offset: 0x00055DB1
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		// Token: 0x0400139B RID: 5019
		private bool isReadOnly;

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This static field is read-only.</summary>
		// Token: 0x0400139C RID: 5020
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This static field is read-only.</summary>
		// Token: 0x0400139D RID: 5021
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This static field is read-only.</summary>
		// Token: 0x0400139E RID: 5022
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
