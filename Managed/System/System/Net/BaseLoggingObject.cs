using System;

namespace System.Net
{
	// Token: 0x0200048E RID: 1166
	internal class BaseLoggingObject
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x000020EB File Offset: 0x000002EB
		internal BaseLoggingObject()
		{
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Flush()
		{
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
