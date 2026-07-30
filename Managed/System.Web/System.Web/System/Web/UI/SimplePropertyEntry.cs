using System;
using Unity;

namespace System.Web.UI
{
	/// <summary>Represents the definition of the control property and its value.</summary>
	// Token: 0x02000225 RID: 549
	public class SimplePropertyEntry : PropertyEntry
	{
		// Token: 0x06001668 RID: 5736 RVA: 0x0002C94E File Offset: 0x0002AB4E
		internal SimplePropertyEntry()
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> method should be called for the property during code creation.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> should be called; otherwise, false.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0003C0CA File Offset: 0x0003A2CA
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0003C0D2 File Offset: 0x0003A2D2
		public bool UseSetAttribute
		{
			get
			{
				return this.useSetAttribute;
			}
			set
			{
				this.useSetAttribute = value;
			}
		}

		/// <summary>Gets or sets the value of the property entry.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the property entry.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0003C0DB File Offset: 0x0003A2DB
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0003C0E3 File Offset: 0x0003A2E3
		public object Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		/// <summary>Gets or sets the formatted string representation of the property entry.</summary>
		/// <returns>A <see cref="T:System.String" /> pertaining to the property entry.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string PersistedValue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400156A RID: 5482
		private bool useSetAttribute;

		// Token: 0x0400156B RID: 5483
		private object val;
	}
}
