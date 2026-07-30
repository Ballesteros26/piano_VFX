using System;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides a base class for collecting layout scheme characteristics.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000201 RID: 513
	public abstract class LayoutSettings
	{
		/// <summary>Gets the current table layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x000763E0 File Offset: 0x000745E0
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				return null;
			}
		}
	}
}
