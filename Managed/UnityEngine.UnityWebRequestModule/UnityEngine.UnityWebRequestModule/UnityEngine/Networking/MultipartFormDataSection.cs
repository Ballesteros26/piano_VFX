using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x02000006 RID: 6
	public class MultipartFormDataSection : IMultipartFormSection
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000031E4 File Offset: 0x000013E4
		public MultipartFormDataSection(string name, byte[] data, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			this.name = name;
			this.data = data;
			this.content = contentType;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000322A File Offset: 0x0000142A
		public MultipartFormDataSection(string name, byte[] data)
			: this(name, data, null)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003237 File Offset: 0x00001437
		public MultipartFormDataSection(byte[] data)
			: this(null, data)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003244 File Offset: 0x00001444
		public MultipartFormDataSection(string name, string data, Encoding encoding, string contentType)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			byte[] bytes = encoding.GetBytes(data);
			this.name = name;
			this.data = bytes;
			bool flag2 = contentType != null && !contentType.Contains("encoding=");
			if (flag2)
			{
				contentType = contentType.Trim() + "; encoding=" + encoding.WebName;
			}
			this.content = contentType;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000032C2 File Offset: 0x000014C2
		public MultipartFormDataSection(string name, string data, string contentType)
			: this(name, data, Encoding.UTF8, contentType)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000032D4 File Offset: 0x000014D4
		public MultipartFormDataSection(string name, string data)
			: this(name, data, "text/plain")
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032E5 File Offset: 0x000014E5
		public MultipartFormDataSection(string data)
			: this(null, data)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000032F4 File Offset: 0x000014F4
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000330C File Offset: 0x0000150C
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003324 File Offset: 0x00001524
		public string fileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003338 File Offset: 0x00001538
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x04000011 RID: 17
		private string name;

		// Token: 0x04000012 RID: 18
		private byte[] data;

		// Token: 0x04000013 RID: 19
		private string content;
	}
}
