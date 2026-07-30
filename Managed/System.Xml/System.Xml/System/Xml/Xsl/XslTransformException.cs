using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	// Token: 0x020004D3 RID: 1235
	[Serializable]
	internal class XslTransformException : XsltException
	{
		// Token: 0x0600324C RID: 12876 RVA: 0x001230B7 File Offset: 0x001212B7
		protected XslTransformException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x001230C1 File Offset: 0x001212C1
		public XslTransformException(Exception inner, string res, params string[] args)
			: base(XslTransformException.CreateMessage(res, args), inner)
		{
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x001230D1 File Offset: 0x001212D1
		public XslTransformException(string message)
			: base(XslTransformException.CreateMessage(message, null), null)
		{
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x001230E1 File Offset: 0x001212E1
		internal XslTransformException(string res, params string[] args)
			: this(null, res, args)
		{
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x001230EC File Offset: 0x001212EC
		internal static string CreateMessage(string res, params string[] args)
		{
			string text = null;
			try
			{
				text = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (text != null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(res);
			if (args != null && args.Length != 0)
			{
				stringBuilder.Append('(');
				stringBuilder.Append(args[0]);
				for (int i = 1; i < args.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(args[i]);
				}
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x00123174 File Offset: 0x00121374
		internal virtual string FormatDetailedMessage()
		{
			return this.Message;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0012317C File Offset: 0x0012137C
		public override string ToString()
		{
			string text = base.GetType().FullName;
			string text2 = this.FormatDetailedMessage();
			if (text2 != null && text2.Length > 0)
			{
				text = text + ": " + text2;
			}
			if (base.InnerException != null)
			{
				text = string.Concat(new string[]
				{
					text,
					" ---> ",
					base.InnerException.ToString(),
					Environment.NewLine,
					"   ",
					XslTransformException.CreateMessage("--- End of inner exception stack trace ---", Array.Empty<string>())
				});
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
