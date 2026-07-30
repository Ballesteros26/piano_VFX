using System;
using System.Text;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000F RID: 15
	internal class RenderGraphLogger
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000311F File Offset: 0x0000131F
		public void Initialize()
		{
			this.m_Builder.Clear();
			this.m_CurrentIndentation = 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003134 File Offset: 0x00001334
		public void IncrementIndentation(int value)
		{
			this.m_CurrentIndentation += Math.Abs(value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003149 File Offset: 0x00001349
		public void DecrementIndentation(int value)
		{
			this.m_CurrentIndentation = Math.Max(0, this.m_CurrentIndentation - Math.Abs(value));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003164 File Offset: 0x00001364
		public void LogLine(string format, params object[] args)
		{
			for (int i = 0; i < this.m_CurrentIndentation; i++)
			{
				this.m_Builder.Append('\t');
			}
			this.m_Builder.AppendFormat(format, args);
			this.m_Builder.AppendLine();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031AA File Offset: 0x000013AA
		public string GetLog()
		{
			return this.m_Builder.ToString();
		}

		// Token: 0x0400003B RID: 59
		private StringBuilder m_Builder = new StringBuilder();

		// Token: 0x0400003C RID: 60
		private int m_CurrentIndentation;
	}
}
