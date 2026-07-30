using System;

namespace System.Drawing.Text
{
	/// <summary>Represents the fonts installed on the system. This class cannot be inherited. </summary>
	// Token: 0x020000B0 RID: 176
	public sealed class InstalledFontCollection : FontCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.InstalledFontCollection" /> class. </summary>
		// Token: 0x06000A3F RID: 2623 RVA: 0x00016368 File Offset: 0x00014568
		public InstalledFontCollection()
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipNewInstalledFontCollection(out this._nativeFontCollection));
		}
	}
}
