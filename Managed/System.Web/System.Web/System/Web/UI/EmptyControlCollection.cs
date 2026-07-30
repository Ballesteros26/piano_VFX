using System;

namespace System.Web.UI
{
	/// <summary>Provides standard support for a <see cref="T:System.Web.UI.ControlCollection" /> collection that is always empty.</summary>
	// Token: 0x0200015F RID: 351
	public class EmptyControlCollection : ControlCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.EmptyControlCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this collection as its collection of child controls. </param>
		// Token: 0x06000F36 RID: 3894 RVA: 0x0002B24E File Offset: 0x0002944E
		public EmptyControlCollection(Control owner)
			: base(owner)
		{
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002B257 File Offset: 0x00029457
		private void ThrowNotSupportedException()
		{
			throw new HttpException(global::SR.GetString("'{0}' does not allow child controls.", new object[] { base.Owner.GetType().ToString() }));
		}

		/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
		/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
		// Token: 0x06000F38 RID: 3896 RVA: 0x0002B281 File Offset: 0x00029481
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection, at the specified index position.</summary>
		/// <param name="index">The index at which to add the <see cref="T:System.Web.UI.Control" />. This parameter is always ignored. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
		/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
		// Token: 0x06000F39 RID: 3897 RVA: 0x0002B281 File Offset: 0x00029481
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}
