using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000010 RID: 16
	internal class ControlDataObject : IDataObject
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002694 File Offset: 0x00000894
		public ControlDataObject()
		{
			this._data = null;
			this._format = null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000026AA File Offset: 0x000008AA
		public ControlDataObject(Control control)
		{
			this.SetData(control);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000026AA File Offset: 0x000008AA
		public ControlDataObject(Control[] controls)
		{
			this.SetData(controls);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000026B9 File Offset: 0x000008B9
		public object GetData(Type format)
		{
			return this.GetData(format.ToString());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000026C7 File Offset: 0x000008C7
		public object GetData(string format)
		{
			return this.GetData(format, true);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000026D1 File Offset: 0x000008D1
		public object GetData(string format, bool autoConvert)
		{
			if (format == this._format)
			{
				return this._data;
			}
			return null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000026E9 File Offset: 0x000008E9
		public bool GetDataPresent(Type format)
		{
			return this.GetDataPresent(format.ToString());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000026F7 File Offset: 0x000008F7
		public bool GetDataPresent(string format)
		{
			return this.GetDataPresent(format, true);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002701 File Offset: 0x00000901
		public bool GetDataPresent(string format, bool autoConvert)
		{
			return format == this._format;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002714 File Offset: 0x00000914
		public string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000271D File Offset: 0x0000091D
		public string[] GetFormats(bool autoConvert)
		{
			return new string[]
			{
				typeof(Control).ToString(),
				typeof(Control[]).ToString()
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002749 File Offset: 0x00000949
		public void SetData(object data)
		{
			if (data is Control)
			{
				this.SetData(typeof(Control), data);
				return;
			}
			if (data is Control[])
			{
				this.SetData(typeof(Control[]), data);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000277E File Offset: 0x0000097E
		public void SetData(Type format, object data)
		{
			this.SetData(format.ToString(), data);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000278D File Offset: 0x0000098D
		public void SetData(string format, object data)
		{
			this.SetData(format, true, data);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002798 File Offset: 0x00000998
		public void SetData(string format, bool autoConvert, object data)
		{
			if (this.ValidateFormat(format))
			{
				this._data = data;
				this._format = format;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000027B4 File Offset: 0x000009B4
		private bool ValidateFormat(string format)
		{
			bool flag = false;
			string[] formats = this.GetFormats();
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == format)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x0400001F RID: 31
		private object _data;

		// Token: 0x04000020 RID: 32
		private string _format;
	}
}
