using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments. </summary>
	// Token: 0x02000264 RID: 612
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x06001396 RID: 5014 RVA: 0x000519BB File Offset: 0x0004FBBB
		public DisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x06001397 RID: 5015 RVA: 0x000519C8 File Offset: 0x0004FBC8
		public DisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x000519D7 File Offset: 0x0004FBD7
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x000519DF File Offset: 0x0004FBDF
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x000519E7 File Offset: 0x0004FBE7
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		// Token: 0x0600139B RID: 5019 RVA: 0x000519F0 File Offset: 0x0004FBF0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x0600139C RID: 5020 RVA: 0x00051A20 File Offset: 0x0004FC20
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600139D RID: 5021 RVA: 0x00051A2D File Offset: 0x0004FC2D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x040012CA RID: 4810
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

		// Token: 0x040012CB RID: 4811
		private string _displayName;
	}
}
