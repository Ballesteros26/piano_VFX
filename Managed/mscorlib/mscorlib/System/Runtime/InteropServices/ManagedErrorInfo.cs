using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091E RID: 2334
	internal class ManagedErrorInfo : IErrorInfo
	{
		// Token: 0x060055FF RID: 22015 RVA: 0x0012956B File Offset: 0x0012776B
		public ManagedErrorInfo(Exception e)
		{
			this.m_Exception = e;
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x0012957A File Offset: 0x0012777A
		public Exception Exception
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00129582 File Offset: 0x00127782
		public int GetGUID(out Guid guid)
		{
			guid = Guid.Empty;
			return 0;
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00129590 File Offset: 0x00127790
		public int GetSource(out string source)
		{
			source = this.m_Exception.Source;
			return 0;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x001295A0 File Offset: 0x001277A0
		public int GetDescription(out string description)
		{
			description = this.m_Exception.Message;
			return 0;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x001295B0 File Offset: 0x001277B0
		public int GetHelpFile(out string helpFile)
		{
			helpFile = this.m_Exception.HelpLink;
			return 0;
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x001295C0 File Offset: 0x001277C0
		public int GetHelpContext(out uint helpContext)
		{
			helpContext = 0U;
			return 0;
		}

		// Token: 0x04002DAC RID: 11692
		private Exception m_Exception;
	}
}
