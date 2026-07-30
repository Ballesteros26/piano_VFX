using System;
using System.Windows.Forms;

namespace System.ComponentModel.Design
{
	/// <summary>Displays byte arrays in hexadecimal, ANSI, and Unicode formats.</summary>
	// Token: 0x020000F4 RID: 244
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class ByteViewer : TableLayoutPanel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ByteViewer" /> class.</summary>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0000A74A File Offset: 0x0000894A
		[MonoTODO]
		public ByteViewer()
		{
		}

		/// <summary>Gets the display mode for the control.</summary>
		/// <returns>The display mode that this control uses. The returned value is defined in <see cref="T:System.ComponentModel.Design.DisplayMode" />.</returns>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual DisplayMode GetDisplayMode()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the raw data from the data buffer to a file.</summary>
		/// <param name="path">The file path to save to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file write failed. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SaveToFile(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the bytes in the buffer.</summary>
		/// <returns>The unsigned byte array reference.</returns>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual byte[] GetBytes()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the byte array to display in the viewer.</summary>
		/// <param name="bytes">The byte array to display. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified byte array is null. </exception>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SetBytes(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current display mode.</summary>
		/// <param name="mode">The display mode to set. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified display mode is not from the <see cref="T:System.ComponentModel.Design.DisplayMode" /> enumeration. </exception>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SetDisplayMode(DisplayMode mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the file to display in the viewer.</summary>
		/// <param name="path">The file path to load from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The file load failed. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		// Token: 0x060006EA RID: 1770 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SetFile(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current line for the <see cref="F:System.ComponentModel.Design.DisplayMode.Hexdump" /> view.</summary>
		/// <param name="line">The current line to display from. </param>
		// Token: 0x060006EB RID: 1771 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual void SetStartLine(int line)
		{
			throw new NotImplementedException();
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060006EC RID: 1772 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060006ED RID: 1773 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void OnPaint(PaintEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x060006EE RID: 1774 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void OnLayout(LayoutEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the <see cref="T:System.ComponentModel.Design.ByteViewer" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060006EF RID: 1775 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void ScrollChanged(object source, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
