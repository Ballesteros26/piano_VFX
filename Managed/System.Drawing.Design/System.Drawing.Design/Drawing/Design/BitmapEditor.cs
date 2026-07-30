using System;
using System.IO;

namespace System.Drawing.Design
{
	/// <summary>Provides a user interface for selecting bitmap files in a property browser.</summary>
	// Token: 0x0200000A RID: 10
	public class BitmapEditor : ImageEditor
	{
		/// <summary>Gets the extensions for the file list filter that the bitmap editor will initially use to filter the file list.</summary>
		/// <returns>The default set of file extensions used to filter the file list.</returns>
		// Token: 0x0600000D RID: 13 RVA: 0x00002094 File Offset: 0x00000294
		protected override string[] GetExtensions()
		{
			return new string[] { "*.bmp", "*.gif", "*.jpg", "*.jpeg", "*.png", "*.ico" };
		}

		/// <summary>Gets the description for the default file list filter provided by this editor.</summary>
		/// <returns>The description for the default type of files to filter the file list for.</returns>
		// Token: 0x0600000E RID: 14 RVA: 0x000020CC File Offset: 0x000002CC
		protected override string GetFileDialogDescription()
		{
			return Locale.GetText("All bitmap files");
		}

		/// <summary>Loads an image from the specified stream.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> loaded from the stream.</returns>
		/// <param name="stream">The stream from which to load the image. </param>
		// Token: 0x0600000F RID: 15 RVA: 0x000020D8 File Offset: 0x000002D8
		protected override Image LoadFromStream(Stream stream)
		{
			return new Bitmap(stream);
		}
	}
}
