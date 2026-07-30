using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information that enables a <see cref="T:System.Windows.Forms.Binding" /> to resolve a data binding to either the property of an object or the property of the current object in a list of objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000060 RID: 96
	public struct BindingMemberInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingMemberInfo" /> class.</summary>
		/// <param name="dataMember">A navigation path that resolves to either the property of an object or the property of the current object in a list of objects. </param>
		// Token: 0x060003C5 RID: 965 RVA: 0x00013394 File Offset: 0x00011594
		public BindingMemberInfo(string dataMember)
		{
			if (dataMember != null)
			{
				this.data_member = dataMember;
			}
			else
			{
				this.data_member = string.Empty;
			}
			int num = this.data_member.LastIndexOf('.');
			if (num != -1)
			{
				this.data_field = this.data_member.Substring(num + 1);
				this.data_path = this.data_member.Substring(0, num);
			}
			else
			{
				this.data_field = this.data_member;
				this.data_path = string.Empty;
			}
		}

		/// <summary>Gets the property name of the data-bound object.</summary>
		/// <returns>The property name of the data-bound object. This can be an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00013418 File Offset: 0x00011618
		public string BindingField
		{
			get
			{
				return this.data_field;
			}
		}

		/// <summary>Gets the information that is used to specify the property name of the data-bound object.</summary>
		/// <returns>An empty string (""), a single property name, or a hierarchy of period-delimited property names that resolves to the property name of the final data-bound object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00013420 File Offset: 0x00011620
		public string BindingMember
		{
			get
			{
				return this.data_member;
			}
		}

		/// <summary>Gets the property name, or the period-delimited hierarchy of property names, that comes before the property name of the data-bound object.</summary>
		/// <returns>The property name, or the period-delimited hierarchy of property names, that comes before the data-bound object property name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013428 File Offset: 0x00011628
		public string BindingPath
		{
			get
			{
				return this.data_path;
			}
		}

		/// <summary>Determines whether the specified object is equal to this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <returns>true if <paramref name="otherObject" /> is a <see cref="T:System.Windows.Forms.BindingMemberInfo" /> and both <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings are equal; otherwise false.</returns>
		/// <param name="otherObject">The object to compare for equality.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003C9 RID: 969 RVA: 0x00013430 File Offset: 0x00011630
		public override bool Equals(object otherObject)
		{
			return otherObject is BindingMemberInfo && (this.data_field == ((BindingMemberInfo)otherObject).data_field && this.data_path == ((BindingMemberInfo)otherObject).data_path) && this.data_member == ((BindingMemberInfo)otherObject).data_member;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CA RID: 970 RVA: 0x000134A4 File Offset: 0x000116A4
		public override int GetHashCode()
		{
			return this.data_member.GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are equal.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are equal; otherwise false.</returns>
		/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
		/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
		// Token: 0x060003CB RID: 971 RVA: 0x000134B4 File Offset: 0x000116B4
		public static bool operator ==(BindingMemberInfo a, BindingMemberInfo b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are not equal.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise false.</returns>
		/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
		/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
		// Token: 0x060003CC RID: 972 RVA: 0x000134C4 File Offset: 0x000116C4
		public static bool operator !=(BindingMemberInfo a, BindingMemberInfo b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400063B RID: 1595
		private string data_member;

		// Token: 0x0400063C RID: 1596
		private string data_field;

		// Token: 0x0400063D RID: 1597
		private string data_path;
	}
}
