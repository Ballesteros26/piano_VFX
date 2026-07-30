using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public class WWWForm
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000023CC File Offset: 0x000005CC
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023E4 File Offset: 0x000005E4
		public WWWForm()
		{
			this.formData = new List<byte[]>();
			this.fieldNames = new List<string>();
			this.fileNames = new List<string>();
			this.types = new List<string>();
			this.boundary = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				this.boundary[i] = (byte)num;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000247C File Offset: 0x0000067C
		public void AddField(string fieldName, string value)
		{
			this.AddField(fieldName, value, Encoding.UTF8);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002490 File Offset: 0x00000690
		public void AddField(string fieldName, string value, Encoding e)
		{
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(null);
			this.formData.Add(e.GetBytes(value));
			this.types.Add("text/plain; charset=\"" + e.WebName + "\"");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024EC File Offset: 0x000006EC
		public void AddField(string fieldName, int i)
		{
			this.AddField(fieldName, i.ToString());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024FE File Offset: 0x000006FE
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents)
		{
			this.AddBinaryData(fieldName, contents, null, null);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000250C File Offset: 0x0000070C
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents, string fileName)
		{
			this.AddBinaryData(fieldName, contents, fileName, null);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000251C File Offset: 0x0000071C
		public void AddBinaryData(string fieldName, byte[] contents, [DefaultValue("null")] string fileName, [DefaultValue("null")] string mimeType)
		{
			this.containsFiles = true;
			bool flag = contents.Length > 8 && contents[0] == 137 && contents[1] == 80 && contents[2] == 78 && contents[3] == 71 && contents[4] == 13 && contents[5] == 10 && contents[6] == 26 && contents[7] == 10;
			bool flag2 = fileName == null;
			if (flag2)
			{
				fileName = fieldName + (flag ? ".png" : ".dat");
			}
			bool flag3 = mimeType == null;
			if (flag3)
			{
				bool flag4 = flag;
				if (flag4)
				{
					mimeType = "image/png";
				}
				else
				{
					mimeType = "application/octet-stream";
				}
			}
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(fileName);
			this.formData.Add(contents);
			this.types.Add(mimeType);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000025EC File Offset: 0x000007EC
		public Dictionary<string, string> headers
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				bool flag = this.containsFiles;
				if (flag)
				{
					dictionary["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString(this.boundary, 0, this.boundary.Length) + "\"";
				}
				else
				{
					dictionary["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return dictionary;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002658 File Offset: 0x00000858
		public byte[] data
		{
			get
			{
				bool flag = this.containsFiles;
				if (flag)
				{
					byte[] bytes = WWWForm.DefaultEncoding.GetBytes("--");
					byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("\r\n");
					byte[] bytes3 = WWWForm.DefaultEncoding.GetBytes("Content-Type: ");
					byte[] bytes4 = WWWForm.DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");
					byte[] bytes5 = WWWForm.DefaultEncoding.GetBytes("\"");
					byte[] bytes6 = WWWForm.DefaultEncoding.GetBytes("; filename=\"");
					using (MemoryStream memoryStream = new MemoryStream(1024))
					{
						for (int i = 0; i < this.formData.Count; i++)
						{
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes, 0, bytes.Length);
							memoryStream.Write(this.boundary, 0, this.boundary.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes3, 0, bytes3.Length);
							byte[] bytes7 = Encoding.UTF8.GetBytes(this.types[i]);
							memoryStream.Write(bytes7, 0, bytes7.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes4, 0, bytes4.Length);
							string headerName = Encoding.UTF8.HeaderName;
							string text = this.fieldNames[i];
							bool flag2 = !WWWTranscoder.SevenBitClean(text, Encoding.UTF8) || text.IndexOf("=?") > -1;
							if (flag2)
							{
								text = string.Concat(new string[]
								{
									"=?",
									headerName,
									"?Q?",
									WWWTranscoder.QPEncode(text, Encoding.UTF8),
									"?="
								});
							}
							byte[] bytes8 = Encoding.UTF8.GetBytes(text);
							memoryStream.Write(bytes8, 0, bytes8.Length);
							memoryStream.Write(bytes5, 0, bytes5.Length);
							bool flag3 = this.fileNames[i] != null;
							if (flag3)
							{
								string text2 = this.fileNames[i];
								bool flag4 = !WWWTranscoder.SevenBitClean(text2, Encoding.UTF8) || text2.IndexOf("=?") > -1;
								if (flag4)
								{
									text2 = string.Concat(new string[]
									{
										"=?",
										headerName,
										"?Q?",
										WWWTranscoder.QPEncode(text2, Encoding.UTF8),
										"?="
									});
								}
								byte[] bytes9 = Encoding.UTF8.GetBytes(text2);
								memoryStream.Write(bytes6, 0, bytes6.Length);
								memoryStream.Write(bytes9, 0, bytes9.Length);
								memoryStream.Write(bytes5, 0, bytes5.Length);
							}
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							byte[] array = this.formData[i];
							memoryStream.Write(array, 0, array.Length);
						}
						memoryStream.Write(bytes2, 0, bytes2.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(this.boundary, 0, this.boundary.Length);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(bytes2, 0, bytes2.Length);
						return memoryStream.ToArray();
					}
				}
				byte[] bytes10 = WWWForm.DefaultEncoding.GetBytes("&");
				byte[] bytes11 = WWWForm.DefaultEncoding.GetBytes("=");
				byte[] array5;
				using (MemoryStream memoryStream2 = new MemoryStream(1024))
				{
					for (int j = 0; j < this.formData.Count; j++)
					{
						byte[] array2 = WWWTranscoder.DataEncode(Encoding.UTF8.GetBytes(this.fieldNames[j]));
						byte[] array3 = this.formData[j];
						byte[] array4 = WWWTranscoder.DataEncode(array3);
						bool flag5 = j > 0;
						if (flag5)
						{
							memoryStream2.Write(bytes10, 0, bytes10.Length);
						}
						memoryStream2.Write(array2, 0, array2.Length);
						memoryStream2.Write(bytes11, 0, bytes11.Length);
						memoryStream2.Write(array4, 0, array4.Length);
					}
					array5 = memoryStream2.ToArray();
				}
				return array5;
			}
		}

		// Token: 0x04000002 RID: 2
		private List<byte[]> formData;

		// Token: 0x04000003 RID: 3
		private List<string> fieldNames;

		// Token: 0x04000004 RID: 4
		private List<string> fileNames;

		// Token: 0x04000005 RID: 5
		private List<string> types;

		// Token: 0x04000006 RID: 6
		private byte[] boundary;

		// Token: 0x04000007 RID: 7
		private bool containsFiles = false;
	}
}
