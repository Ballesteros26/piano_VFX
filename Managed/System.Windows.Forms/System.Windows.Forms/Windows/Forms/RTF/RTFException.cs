using System;
using System.Text;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200002C RID: 44
	internal class RTFException : ApplicationException
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public RTFException(RTF rtf, string error_message)
		{
			this.pos = rtf.LinePos;
			this.line = rtf.LineNumber;
			this.token_class = rtf.TokenClass;
			this.major = rtf.Major;
			this.minor = rtf.Minor;
			this.param = rtf.Param;
			this.text = rtf.Text;
			this.error_message = error_message;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000CE30 File Offset: 0x0000B030
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.error_message);
				stringBuilder.Append("\n");
				stringBuilder.Append(string.Concat(new object[] { "RTF Stream Info: Pos:", this.pos, " Line:", this.line }));
				stringBuilder.Append("\n");
				stringBuilder.Append("TokenClass:" + this.token_class + ", ");
				stringBuilder.Append("Major:" + string.Format("{0}", (int)this.major) + ", ");
				stringBuilder.Append("Minor:" + string.Format("{0}", (int)this.minor) + ", ");
				stringBuilder.Append("Param:" + string.Format("{0}", this.param) + ", ");
				stringBuilder.Append("Text:" + this.text);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0400036C RID: 876
		private int pos;

		// Token: 0x0400036D RID: 877
		private int line;

		// Token: 0x0400036E RID: 878
		private TokenClass token_class;

		// Token: 0x0400036F RID: 879
		private Major major;

		// Token: 0x04000370 RID: 880
		private Minor minor;

		// Token: 0x04000371 RID: 881
		private int param;

		// Token: 0x04000372 RID: 882
		private string text;

		// Token: 0x04000373 RID: 883
		private string error_message;
	}
}
