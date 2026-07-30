using System;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x0200019F RID: 415
	internal class SqlConnectionTimeoutPhaseDuration
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0005F8C4 File Offset: 0x0005DAC4
		internal void StartCapture()
		{
			this._swDuration.Start();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0005F8D1 File Offset: 0x0005DAD1
		internal void StopCapture()
		{
			if (this._swDuration.IsRunning)
			{
				this._swDuration.Stop();
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0005F8EB File Offset: 0x0005DAEB
		internal long GetMilliSecondDuration()
		{
			return this._swDuration.ElapsedMilliseconds;
		}

		// Token: 0x04000D11 RID: 3345
		private Stopwatch _swDuration = new Stopwatch();
	}
}
