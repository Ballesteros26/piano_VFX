using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents an item that is saved in the <see cref="T:System.Web.UI.StateBag" /> class when view state information is persisted between Web requests. This class cannot be inherited.</summary>
	// Token: 0x02000229 RID: 553
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateItem
	{
		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.StateItem" /> object has been modified.</summary>
		/// <returns>true if the stored <see cref="T:System.Web.UI.StateItem" /> object has been modified; otherwise, false.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0003D1BC File Offset: 0x0003B3BC
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
		public bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.StateItem" /> object that is stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Web.UI.StateItem" /> stored in the <see cref="T:System.Web.UI.StateBag" />.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0003D1CD File Offset: 0x0003B3CD
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x0003D1D5 File Offset: 0x0003B3D5
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00002050 File Offset: 0x00000250
		private StateItem()
		{
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0003D1DE File Offset: 0x0003B3DE
		internal StateItem(object value)
		{
			this._value = value;
		}

		// Token: 0x0400157F RID: 5503
		private bool _isDirty;

		// Token: 0x04001580 RID: 5504
		private object _value;
	}
}
