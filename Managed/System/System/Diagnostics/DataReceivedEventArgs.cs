using System;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E7 RID: 487
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x0004716C File Offset: 0x0004536C
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0004717B File Offset: 0x0004537B
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal DataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001112 RID: 4370
		private string data;
	}
}
