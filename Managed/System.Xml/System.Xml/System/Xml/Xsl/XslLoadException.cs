using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	// Token: 0x020004D4 RID: 1236
	[Serializable]
	internal class XslLoadException : XslTransformException
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x00123220 File Offset: 0x00121420
		protected XslLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if ((bool)info.GetValue("hasLineInfo", typeof(bool)))
			{
				string text = (string)info.GetValue("Uri", typeof(string));
				int num = (int)info.GetValue("StartLine", typeof(int));
				int num2 = (int)info.GetValue("StartPos", typeof(int));
				int num3 = (int)info.GetValue("EndLine", typeof(int));
				int num4 = (int)info.GetValue("EndPos", typeof(int));
				this.lineInfo = new SourceLineInfo(text, num, num2, num3, num4);
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x001232F0 File Offset: 0x001214F0
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hasLineInfo", this.lineInfo != null);
			if (this.lineInfo != null)
			{
				info.AddValue("Uri", this.lineInfo.Uri);
				info.AddValue("StartLine", this.lineInfo.Start.Line);
				info.AddValue("StartPos", this.lineInfo.Start.Pos);
				info.AddValue("EndLine", this.lineInfo.End.Line);
				info.AddValue("EndPos", this.lineInfo.End.Pos);
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x001230E1 File Offset: 0x001212E1
		internal XslLoadException(string res, params string[] args)
			: base(null, res, args)
		{
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001233B2 File Offset: 0x001215B2
		internal XslLoadException(Exception inner, ISourceLineInfo lineInfo)
			: base(inner, "XSLT compile error.", null)
		{
			this.SetSourceLineInfo(lineInfo);
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x001233C8 File Offset: 0x001215C8
		internal XslLoadException(CompilerError error)
			: base("{0}", new string[] { error.ErrorText })
		{
			int line = error.Line;
			int num = error.Column;
			if (line == 0)
			{
				num = 0;
			}
			else if (num == 0)
			{
				num = 1;
			}
			this.SetSourceLineInfo(new SourceLineInfo(error.FileName, line, num, line, num));
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x0012341E File Offset: 0x0012161E
		internal void SetSourceLineInfo(ISourceLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x00123427 File Offset: 0x00121627
		public override string SourceUri
		{
			get
			{
				if (this.lineInfo == null)
				{
					return null;
				}
				return this.lineInfo.Uri;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x00123440 File Offset: 0x00121640
		public override int LineNumber
		{
			get
			{
				if (this.lineInfo == null)
				{
					return 0;
				}
				return this.lineInfo.Start.Line;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x0012346C File Offset: 0x0012166C
		public override int LinePosition
		{
			get
			{
				if (this.lineInfo == null)
				{
					return 0;
				}
				return this.lineInfo.Start.Pos;
			}
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x00123498 File Offset: 0x00121698
		private static string AppendLineInfoMessage(string message, ISourceLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				string fileName = SourceLineInfo.GetFileName(lineInfo.Uri);
				string text = XslTransformException.CreateMessage("An error occurred at {0}({1},{2}).", new string[]
				{
					fileName,
					lineInfo.Start.Line.ToString(CultureInfo.InvariantCulture),
					lineInfo.Start.Pos.ToString(CultureInfo.InvariantCulture)
				});
				if (text != null && text.Length > 0)
				{
					if (message.Length > 0 && !XmlCharType.Instance.IsWhiteSpace(message[message.Length - 1]))
					{
						message += " ";
					}
					message += text;
				}
			}
			return message;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x00123552 File Offset: 0x00121752
		internal static string CreateMessage(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			return XslLoadException.AppendLineInfoMessage(XslTransformException.CreateMessage(res, args), lineInfo);
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x00123561 File Offset: 0x00121761
		internal override string FormatDetailedMessage()
		{
			return XslLoadException.AppendLineInfoMessage(this.Message, this.lineInfo);
		}

		// Token: 0x040020CD RID: 8397
		private ISourceLineInfo lineInfo;
	}
}
