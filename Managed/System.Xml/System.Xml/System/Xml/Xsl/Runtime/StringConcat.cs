using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E3 RID: 1507
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct StringConcat
	{
		// Token: 0x06003B31 RID: 15153 RVA: 0x0014D5E5 File Offset: 0x0014B7E5
		public void Clear()
		{
			this.idxStr = 0;
			this.delimiter = null;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x0014D5F5 File Offset: 0x0014B7F5
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x0014D5FD File Offset: 0x0014B7FD
		public string Delimiter
		{
			get
			{
				return this.delimiter;
			}
			set
			{
				this.delimiter = value;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x0014D606 File Offset: 0x0014B806
		internal int Count
		{
			get
			{
				return this.idxStr;
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x0014D60E File Offset: 0x0014B80E
		public void Concat(string value)
		{
			if (this.delimiter != null && this.idxStr != 0)
			{
				this.ConcatNoDelimiter(this.delimiter);
			}
			this.ConcatNoDelimiter(value);
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x0014D634 File Offset: 0x0014B834
		public string GetResult()
		{
			switch (this.idxStr)
			{
			case 0:
				return string.Empty;
			case 1:
				return this.s1;
			case 2:
				return this.s1 + this.s2;
			case 3:
				return this.s1 + this.s2 + this.s3;
			case 4:
				return this.s1 + this.s2 + this.s3 + this.s4;
			default:
				return string.Concat(this.strList.ToArray());
			}
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x0014D6CC File Offset: 0x0014B8CC
		internal void ConcatNoDelimiter(string s)
		{
			switch (this.idxStr)
			{
			case 0:
				this.s1 = s;
				goto IL_00A8;
			case 1:
				this.s2 = s;
				goto IL_00A8;
			case 2:
				this.s3 = s;
				goto IL_00A8;
			case 3:
				this.s4 = s;
				goto IL_00A8;
			case 4:
			{
				int num = ((this.strList == null) ? 8 : this.strList.Count);
				List<string> list = (this.strList = new List<string>(num));
				list.Add(this.s1);
				list.Add(this.s2);
				list.Add(this.s3);
				list.Add(this.s4);
				break;
			}
			}
			this.strList.Add(s);
			IL_00A8:
			this.idxStr++;
		}

		// Token: 0x040026E3 RID: 9955
		private string s1;

		// Token: 0x040026E4 RID: 9956
		private string s2;

		// Token: 0x040026E5 RID: 9957
		private string s3;

		// Token: 0x040026E6 RID: 9958
		private string s4;

		// Token: 0x040026E7 RID: 9959
		private string delimiter;

		// Token: 0x040026E8 RID: 9960
		private List<string> strList;

		// Token: 0x040026E9 RID: 9961
		private int idxStr;
	}
}
