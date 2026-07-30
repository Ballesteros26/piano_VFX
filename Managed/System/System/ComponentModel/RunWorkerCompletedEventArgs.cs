using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x020002D0 RID: 720
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> class.</summary>
		/// <param name="result">The result of an asynchronous operation.</param>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		// Token: 0x060016FF RID: 5887 RVA: 0x0005BBD4 File Offset: 0x00059DD4
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled)
			: base(error, cancelled, null)
		{
			this.result = result;
		}

		/// <summary>Gets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> is not null. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0005BBE6 File Offset: 0x00059DE6
		public object Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		/// <summary>Gets a value that represents the user state.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the user state.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0005BBF4 File Offset: 0x00059DF4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object UserState
		{
			get
			{
				return base.UserState;
			}
		}

		// Token: 0x040013E5 RID: 5093
		private object result;
	}
}
