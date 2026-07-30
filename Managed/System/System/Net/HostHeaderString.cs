using System;
using System.Text;

namespace System.Net
{
	// Token: 0x0200046C RID: 1132
	internal class HostHeaderString
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x00081F9E File Offset: 0x0008019E
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00081FAD File Offset: 0x000801AD
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00081FBC File Offset: 0x000801BC
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00081FD4 File Offset: 0x000801D4
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x0008203D File Offset: 0x0008023D
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x00082045 File Offset: 0x00080245
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x0008204E File Offset: 0x0008024E
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0008205E File Offset: 0x0008025E
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0008206C File Offset: 0x0008026C
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x04001E53 RID: 7763
		private bool m_Converted;

		// Token: 0x04001E54 RID: 7764
		private string m_String;

		// Token: 0x04001E55 RID: 7765
		private byte[] m_Bytes;
	}
}
