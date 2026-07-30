using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x020002BB RID: 699
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x060015FC RID: 5628 RVA: 0x00056B58 File Offset: 0x00054D58
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00056B6E File Offset: 0x00054D6E
		[SRDescription("Percentage progress made in operation.")]
		public int ProgressPercentage
		{
			get
			{
				return this.progressPercentage;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00056B76 File Offset: 0x00054D76
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04001384 RID: 4996
		private readonly int progressPercentage;

		// Token: 0x04001385 RID: 4997
		private readonly object userState;
	}
}
