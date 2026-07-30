using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x02000265 RID: 613
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x0600139F RID: 5023 RVA: 0x00051A46 File Offset: 0x0004FC46
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		/// <summary>Gets a value that represents the argument of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the argument of an asynchronous operation.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00051A55 File Offset: 0x0004FC55
		[SRDescription("Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync.")]
		public object Argument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00051A5D File Offset: 0x0004FC5D
		// (set) Token: 0x060013A2 RID: 5026 RVA: 0x00051A65 File Offset: 0x0004FC65
		[SRDescription("Result from the worker function.")]
		public object Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x040012CC RID: 4812
		private object result;

		// Token: 0x040012CD RID: 4813
		private object argument;
	}
}
