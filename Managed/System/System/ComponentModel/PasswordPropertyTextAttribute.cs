using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that an object's text representation is obscured by characters such as asterisks. This class cannot be inherited.</summary>
	// Token: 0x020002BA RID: 698
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class. </summary>
		// Token: 0x060015F5 RID: 5621 RVA: 0x00056AEA File Offset: 0x00054CEA
		public PasswordPropertyTextAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class, optionally showing password text. </summary>
		/// <param name="password">true to indicate that the property should be shown as password text; otherwise, false. The default is false.</param>
		// Token: 0x060015F6 RID: 5622 RVA: 0x00056AF3 File Offset: 0x00054CF3
		public PasswordPropertyTextAttribute(bool password)
		{
			this._password = password;
		}

		/// <summary>Gets a value indicating if the property for which the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is defined should be shown as password text.</summary>
		/// <returns>true if the property should be shown as password text; otherwise, false.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00056B02 File Offset: 0x00054D02
		public bool Password
		{
			get
			{
				return this._password;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is equal to the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> to compare with the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</param>
		// Token: 0x060015F8 RID: 5624 RVA: 0x00056B0A File Offset: 0x00054D0A
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this._password;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</returns>
		// Token: 0x060015F9 RID: 5625 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns an indication whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x060015FA RID: 5626 RVA: 0x00056B29 File Offset: 0x00054D29
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PasswordPropertyTextAttribute.Default);
		}

		/// <summary>Specifies that a text property is used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x04001380 RID: 4992
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		/// <summary>Specifies that a text property is not used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x04001381 RID: 4993
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</summary>
		// Token: 0x04001382 RID: 4994
		public static readonly PasswordPropertyTextAttribute Default = PasswordPropertyTextAttribute.No;

		// Token: 0x04001383 RID: 4995
		private bool _password;
	}
}
