using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020F RID: 527
	[ComVisible(true)]
	public class LinkLabelLinkClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link.</summary>
		/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
		// Token: 0x0600208F RID: 8335 RVA: 0x00079A08 File Offset: 0x00077C08
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link)
		{
			this.button = MouseButtons.Left;
			this.link = link;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link and the specified mouse button.</summary>
		/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</param>
		// Token: 0x06002090 RID: 8336 RVA: 0x00079A24 File Offset: 0x00077C24
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link, MouseButtons button)
		{
			this.button = button;
			this.link = link;
		}

		/// <summary>Gets the mouse button that causes the link to be clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00079A3C File Offset: 0x00077C3C
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</summary>
		/// <returns>A link on the <see cref="T:System.Windows.Forms.LinkLabel" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x00079A44 File Offset: 0x00077C44
		public LinkLabel.Link Link
		{
			get
			{
				return this.link;
			}
		}

		// Token: 0x04001196 RID: 4502
		private MouseButtons button;

		// Token: 0x04001197 RID: 4503
		private LinkLabel.Link link;
	}
}
