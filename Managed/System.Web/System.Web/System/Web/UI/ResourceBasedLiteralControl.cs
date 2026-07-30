using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x02000221 RID: 545
	internal class ResourceBasedLiteralControl : LiteralControl
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x0003BB7B File Offset: 0x00039D7B
		public ResourceBasedLiteralControl(IntPtr ptr, int length)
		{
			this.EnableViewState = false;
			base.AutoID = false;
			this.ptr = ptr;
			this.length = length;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0003BBA0 File Offset: 0x00039DA0
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0003BBE7 File Offset: 0x00039DE7
		public override string Text
		{
			get
			{
				if (this.length == -1)
				{
					return base.Text;
				}
				byte[] array = new byte[this.length];
				Marshal.Copy(this.ptr, array, 0, this.length);
				return Encoding.UTF8.GetString(array);
			}
			set
			{
				this.length = -1;
				base.Text = value;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0003BBF8 File Offset: 0x00039DF8
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.length == -1)
			{
				writer.Write(base.Text);
				return;
			}
			HttpWriter httpWriter = writer.GetHttpWriter();
			if (httpWriter == null || httpWriter.Response.ContentEncoding.CodePage != 65001)
			{
				byte[] array = new byte[this.length];
				Marshal.Copy(this.ptr, array, 0, this.length);
				writer.Write(Encoding.UTF8.GetString(array));
				return;
			}
			httpWriter.WriteUTF8Ptr(this.ptr, this.length);
		}

		// Token: 0x04001564 RID: 5476
		private IntPtr ptr;

		// Token: 0x04001565 RID: 5477
		private int length;
	}
}
