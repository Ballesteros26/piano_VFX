using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005BD RID: 1469
	[Serializable]
	internal class XPathCompileException : XslLoadException
	{
		// Token: 0x06003A61 RID: 14945 RVA: 0x0014A0A0 File Offset: 0x001482A0
		protected XPathCompileException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.queryString = (string)info.GetValue("QueryString", typeof(string));
			this.startChar = (int)info.GetValue("StartChar", typeof(int));
			this.endChar = (int)info.GetValue("EndChar", typeof(int));
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x0014A115 File Offset: 0x00148315
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("QueryString", this.queryString);
			info.AddValue("StartChar", this.startChar);
			info.AddValue("EndChar", this.endChar);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x0014A152 File Offset: 0x00148352
		internal XPathCompileException(string queryString, int startChar, int endChar, string resId, params string[] args)
			: base(resId, args)
		{
			this.queryString = queryString;
			this.startChar = startChar;
			this.endChar = endChar;
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x0014A173 File Offset: 0x00148373
		internal XPathCompileException(string resId, params string[] args)
			: base(resId, args)
		{
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x0014A180 File Offset: 0x00148380
		private static void AppendTrimmed(StringBuilder sb, string value, int startIndex, int count, XPathCompileException.TrimType trimType)
		{
			if (count <= 32)
			{
				sb.Append(value, startIndex, count);
				return;
			}
			switch (trimType)
			{
			case XPathCompileException.TrimType.Left:
				sb.Append("...");
				sb.Append(value, startIndex + count - 32, 32);
				return;
			case XPathCompileException.TrimType.Right:
				sb.Append(value, startIndex, 32);
				sb.Append("...");
				return;
			case XPathCompileException.TrimType.Middle:
				sb.Append(value, startIndex, 16);
				sb.Append("...");
				sb.Append(value, startIndex + count - 16, 16);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x0014A210 File Offset: 0x00148410
		internal string MarkOutError()
		{
			if (this.queryString == null || this.queryString.Trim(new char[] { ' ' }).Length == 0)
			{
				return null;
			}
			int num = this.endChar - this.startChar;
			StringBuilder stringBuilder = new StringBuilder();
			XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, 0, this.startChar, XPathCompileException.TrimType.Left);
			if (num > 0)
			{
				stringBuilder.Append(" -->");
				XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, this.startChar, num, XPathCompileException.TrimType.Middle);
			}
			stringBuilder.Append("<-- ");
			XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, this.endChar, this.queryString.Length - this.endChar, XPathCompileException.TrimType.Right);
			return stringBuilder.ToString();
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x0014A2C8 File Offset: 0x001484C8
		internal override string FormatDetailedMessage()
		{
			string text = this.Message;
			string text2 = this.MarkOutError();
			if (text2 != null && text2.Length > 0)
			{
				if (text.Length > 0)
				{
					text += Environment.NewLine;
				}
				text += text2;
			}
			return text;
		}

		// Token: 0x040025EE RID: 9710
		public string queryString;

		// Token: 0x040025EF RID: 9711
		public int startChar;

		// Token: 0x040025F0 RID: 9712
		public int endChar;

		// Token: 0x020005BE RID: 1470
		private enum TrimType
		{
			// Token: 0x040025F2 RID: 9714
			Left,
			// Token: 0x040025F3 RID: 9715
			Right,
			// Token: 0x040025F4 RID: 9716
			Middle
		}
	}
}
