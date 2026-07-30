using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x06003409 RID: 13321 RVA: 0x000BB825 File Offset: 0x000B9A25
		[SecurityCritical]
		internal CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000BB860 File Offset: 0x000B9A60
		[SecurityCritical]
		internal static string CreateString(string pStrings, uint index)
		{
			if (pStrings[0] == '|')
			{
				return pStrings.Split(CodePageDataItem.sep, StringSplitOptions.RemoveEmptyEntries)[(int)index];
			}
			return pStrings;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000BB87D File Offset: 0x000B9A7D
		public string WebName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000BB8AE File Offset: 0x000B9AAE
		public virtual int UIFamilyCodePage
		{
			get
			{
				return this.m_uiFamilyCodePage;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000BB8B6 File Offset: 0x000B9AB6
		public string HeaderName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_headerName == null)
				{
					this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
				}
				return this.m_headerName;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000BB8E7 File Offset: 0x000B9AE7
		public string BodyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_bodyName == null)
				{
					this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
				}
				return this.m_bodyName;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000BB918 File Offset: 0x000B9B18
		public uint Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001BAD RID: 7085
		internal int m_dataIndex;

		// Token: 0x04001BAE RID: 7086
		internal int m_uiFamilyCodePage;

		// Token: 0x04001BAF RID: 7087
		internal string m_webName;

		// Token: 0x04001BB0 RID: 7088
		internal string m_headerName;

		// Token: 0x04001BB1 RID: 7089
		internal string m_bodyName;

		// Token: 0x04001BB2 RID: 7090
		internal uint m_flags;

		// Token: 0x04001BB3 RID: 7091
		private static readonly char[] sep = new char[] { '|' };
	}
}
