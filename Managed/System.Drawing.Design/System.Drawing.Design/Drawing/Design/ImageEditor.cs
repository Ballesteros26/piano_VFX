using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Provides a user interface for selecting an image for a property in a property grid.</summary>
	// Token: 0x02000017 RID: 23
	public class ImageEditor : UITypeEditor
	{
		/// <summary>Edits the specified object value using the edit style provided by the <see cref="M:System.Drawing.Design.ImageEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the new value. If the value of the object has not changed, <see cref="M:System.Drawing.Design.ImageEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> returns the object that was passed to it.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> through which editing services can be obtained. </param>
		/// <param name="value">An <see cref="T:System.Object" /> being edited. </param>
		// Token: 0x06000045 RID: 69 RVA: 0x000033CC File Offset: 0x000015CC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.openDialog = new OpenFileDialog();
			this.openDialog.Title = Locale.GetText("Open image file");
			this.openDialog.CheckFileExists = true;
			this.openDialog.CheckPathExists = true;
			this.openDialog.Filter = ImageEditor.CreateFilterEntry(this);
			this.openDialog.Multiselect = false;
			if (this.openDialog.ShowDialog() == 1)
			{
				return this.LoadFromStream(this.openDialog.OpenFile());
			}
			return value;
		}

		/// <summary>Gets the editing style of the <see cref="M:System.Drawing.Design.ImageEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the supported editing style.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000046 RID: 70 RVA: 0x00003188 File Offset: 0x00001388
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>Gets a value indicating whether this editor supports painting a representation of an object's value.</summary>
		/// <returns>true if <see cref="M:System.Drawing.Design.ImageEditor.PaintValue(System.Drawing.Design.PaintValueEventArgs)" /> is implemented; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000047 RID: 71 RVA: 0x0000245B File Offset: 0x0000065B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Paints a value indicated by the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs" />.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> indicating what to paint and where to paint it. </param>
		// Token: 0x06000048 RID: 72 RVA: 0x00003450 File Offset: 0x00001650
		public override void PaintValue(PaintValueEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (e.Value != null)
			{
				Image image = (Image)e.Value;
				graphics.DrawImage(image, e.Bounds);
			}
			graphics.DrawRectangle(Pens.Black, e.Bounds);
		}

		/// <summary>Creates a string of file name extensions using the specified array of file extensions and the specified separator.</summary>
		/// <returns>A string containing the specified file name extensions, each separated by the specified separator.</returns>
		/// <param name="extensions">The extensions to filter for. </param>
		/// <param name="sep">The separator to use. </param>
		// Token: 0x06000049 RID: 73 RVA: 0x00003498 File Offset: 0x00001698
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

		/// <summary>Creates a filter entry for a file dialog box's file list.</summary>
		/// <returns>The new filter entry string.</returns>
		/// <param name="e">An <see cref="T:System.Drawing.Design.ImageEditor" /> to get the filter entry from.</param>
		// Token: 0x0600004A RID: 74 RVA: 0x000034E8 File Offset: 0x000016E8
		protected static string CreateFilterEntry(ImageEditor e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = ImageEditor.CreateExtensionsString(e.GetExtensions(), ";");
			stringBuilder.Append(e.GetFileDialogDescription());
			stringBuilder.Append(" (" + text + ")|");
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		/// <summary>Gets the extensions for the file-list filter that this editor initially uses to filter the file list.</summary>
		/// <returns>A set of file extensions used to filter the file list.</returns>
		// Token: 0x0600004B RID: 75 RVA: 0x0000353C File Offset: 0x0000173C
		protected virtual string[] GetExtensions()
		{
			return new string[] { "*.bmp", "*.gif", "*.jpg", "*.jpeg", "*.png", "*.ico", "*.emf", "*.wmf" };
		}

		/// <summary>Gets the description for the default file-list filter provided by this editor.</summary>
		/// <returns>The description for the default file-list filter.</returns>
		// Token: 0x0600004C RID: 76 RVA: 0x0000358F File Offset: 0x0000178F
		protected virtual string GetFileDialogDescription()
		{
			return Locale.GetText("All image files");
		}

		/// <summary>Loads an image from the specified stream.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that has been loaded.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the image to load.</param>
		// Token: 0x0600004D RID: 77 RVA: 0x000020D8 File Offset: 0x000002D8
		protected virtual Image LoadFromStream(Stream stream)
		{
			return new Bitmap(stream);
		}

		/// <summary>Gets an array of supported image types.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> representing supported image types.</returns>
		// Token: 0x0600004E RID: 78 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		protected virtual Type[] GetImageExtenders()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400003B RID: 59
		private OpenFileDialog openDialog;
	}
}
