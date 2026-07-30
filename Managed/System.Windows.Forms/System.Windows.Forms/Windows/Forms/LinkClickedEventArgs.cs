using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000208 RID: 520
	[ComVisible(true)]
	public class LinkClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> class.</summary>
		/// <param name="linkText">The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
		// Token: 0x0600200A RID: 8202 RVA: 0x00078274 File Offset: 0x00076474
		public LinkClickedEventArgs(string linkText)
		{
			this.link_text = linkText;
		}

		/// <summary>Gets the text of the link being clicked.</summary>
		/// <returns>The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00078284 File Offset: 0x00076484
		public string LinkText
		{
			get
			{
				return this.link_text;
			}
		}

		// Token: 0x04001170 RID: 4464
		private string link_text;
	}
}
