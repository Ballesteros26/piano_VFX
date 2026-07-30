using System;
using System.IO;

namespace System.Drawing.Text
{
	/// <summary>Provides a collection of font families built from font files that are provided by the client application.</summary>
	// Token: 0x020000B2 RID: 178
	public sealed class PrivateFontCollection : FontCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.PrivateFontCollection" /> class. </summary>
		// Token: 0x06000A40 RID: 2624 RVA: 0x00016380 File Offset: 0x00014580
		public PrivateFontCollection()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipNewPrivateFontCollection(out this._nativeFontCollection));
		}

		/// <summary>Adds a font from the specified file to this <see cref="T:System.Drawing.Text.PrivateFontCollection" />. </summary>
		/// <param name="filename">A <see cref="T:System.String" /> that contains the file name of the font to add. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified font is not supported or the font file cannot be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000A41 RID: 2625 RVA: 0x00016398 File Offset: 0x00014598
		public void AddFontFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			string fullPath = Path.GetFullPath(filename);
			if (!File.Exists(fullPath))
			{
				throw new FileNotFoundException();
			}
			GDIPlus.CheckStatus(GDIPlus.GdipPrivateAddFontFile(this._nativeFontCollection, fullPath));
		}

		/// <summary>Adds a font contained in system memory to this <see cref="T:System.Drawing.Text.PrivateFontCollection" />.</summary>
		/// <param name="memory">The memory address of the font to add. </param>
		/// <param name="length">The memory length of the font to add. </param>
		// Token: 0x06000A42 RID: 2626 RVA: 0x000163D9 File Offset: 0x000145D9
		public void AddMemoryFont(IntPtr memory, int length)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipPrivateAddMemoryFont(this._nativeFontCollection, memory, length));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000163ED File Offset: 0x000145ED
		protected override void Dispose(bool disposing)
		{
			if (this._nativeFontCollection != IntPtr.Zero)
			{
				GDIPlus.GdipDeletePrivateFontCollection(ref this._nativeFontCollection);
				this._nativeFontCollection = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}
	}
}
