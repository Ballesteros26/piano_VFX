using System;

namespace UnityEngine.Assertions
{
	// Token: 0x020003E3 RID: 995
	public class AssertionException : Exception
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x0003A3A7 File Offset: 0x000385A7
		public AssertionException(string message, string userMessage)
			: base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x0003A3BC File Offset: 0x000385BC
		public override string Message
		{
			get
			{
				string text = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					text = text + "\n" + this.m_UserMessage;
				}
				return text;
			}
		}

		// Token: 0x04000D00 RID: 3328
		private string m_UserMessage;
	}
}
