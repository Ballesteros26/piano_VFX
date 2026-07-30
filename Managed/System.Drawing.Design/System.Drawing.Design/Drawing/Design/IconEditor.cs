using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> for visually choosing an icon.</summary>
	// Token: 0x02000016 RID: 22
	public class IconEditor : UITypeEditor
	{
		/// <summary>Creates a string representing the valid file extensions for icons.</summary>
		/// <returns>A string containing the icon file extensions, or null if <paramref name="extensions" /> is null or empty.</returns>
		/// <param name="extensions">An array of strings holding valid file extensions.</param>
		/// <param name="sep">A string that specifies the separator character.</param>
		// Token: 0x0600003B RID: 59 RVA: 0x0000322C File Offset: 0x0000142C
		protected static string CreateExtensionsString(string[] extensions, string sep)
		{
			if (extensions.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(extensions[0]);
				for (int i = 1; i < extensions.Length - 1; i++)
				{
					stringBuilder.Append(sep);
					stringBuilder.Append(extensions[i]);
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		/// <summary>Creates a filter string for the file dialog box.</summary>
		/// <returns>The filter string, created from the string returned by <see cref="M:System.Drawing.Design.IconEditor.CreateExtensionsString(System.String[],System.String)" />.</returns>
		/// <param name="e">The <see cref="T:System.Drawing.Design.IconEditor" /> for which the filter string will be created.</param>
		// Token: 0x0600003C RID: 60 RVA: 0x0000327C File Offset: 0x0000147C
		protected static string CreateFilterEntry(IconEditor e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = IconEditor.CreateExtensionsString(e.GetExtensions(), ";");
			stringBuilder.Append(e.GetFileDialogDescription());
			stringBuilder.Append(" (" + text + ")|");
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		/// <summary>Edits the given object value using the editor style provided by the <see cref="M:System.Drawing.Design.IconEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x0600003D RID: 61 RVA: 0x000032D0 File Offset: 0x000014D0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.openDialog = new OpenFileDialog();
			this.openDialog.Title = Locale.GetText("Open image file");
			this.openDialog.CheckFileExists = true;
			this.openDialog.CheckPathExists = true;
			this.openDialog.Filter = IconEditor.CreateFilterEntry(this);
			this.openDialog.Multiselect = false;
			if (this.openDialog.ShowDialog() == 1)
			{
				return this.LoadFromStream(this.openDialog.OpenFile());
			}
			return value;
		}

		/// <summary>Retrieves the editing style of the <see cref="Overload:System.Drawing.Design.IconEditor.EditValue" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the provided editing style.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		// Token: 0x0600003E RID: 62 RVA: 0x00003188 File Offset: 0x00001388
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>Retrieves an array of valid file extensions for icons.</summary>
		/// <returns>An array of valid file extensions for icons.</returns>
		// Token: 0x0600003F RID: 63 RVA: 0x00003353 File Offset: 0x00001553
		protected virtual string[] GetExtensions()
		{
			return new string[] { "*.ico" };
		}

		/// <summary>Gets the description for the default file list filter provided by this editor.</summary>
		/// <returns>The description for the default type of files to filter the file list for.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x00003363 File Offset: 0x00001563
		protected virtual string GetFileDialogDescription()
		{
			return Locale.GetText("Icon files");
		}

		/// <summary>Determines if this editor supports the painting of a representation of an object's value.</summary>
		/// <returns>true if <see cref="Overload:System.Drawing.Design.UITypeEditor.PaintValue" /> is implemented; otherwise, false.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		// Token: 0x06000041 RID: 65 RVA: 0x0000245B File Offset: 0x0000065B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Icon" /> from the given stream.</summary>
		/// <returns>The newly created <see cref="T:System.Drawing.Icon" />.</returns>
		/// <param name="stream">The source stream from which the icon will be created.</param>
		// Token: 0x06000042 RID: 66 RVA: 0x0000336F File Offset: 0x0000156F
		protected virtual Icon LoadFromStream(Stream stream)
		{
			return new Icon(stream);
		}

		/// <summary>Paints a representative value of the given object to the provided canvas.</summary>
		/// <param name="e">What to paint and where to paint it. </param>
		// Token: 0x06000043 RID: 67 RVA: 0x00003378 File Offset: 0x00001578
		public override void PaintValue(PaintValueEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (e.Value != null)
			{
				Image image = ((Icon)e.Value).ToBitmap();
				graphics.DrawImage(image, e.Bounds);
				image.Dispose();
			}
			graphics.DrawRectangle(Pens.Black, e.Bounds);
		}

		// Token: 0x0400003A RID: 58
		private OpenFileDialog openDialog;
	}
}
