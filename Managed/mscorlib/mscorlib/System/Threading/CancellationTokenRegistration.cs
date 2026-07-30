using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Represents a callback delegate that has been registered with a <see cref="T:System.Threading.CancellationToken" />. </summary>
	// Token: 0x0200044F RID: 1103
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable
	{
		// Token: 0x060034D6 RID: 13526 RVA: 0x000C3635 File Offset: 0x000C1835
		internal CancellationTokenRegistration(CancellationCallbackInfo callbackInfo, SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo)
		{
			this.m_callbackInfo = callbackInfo;
			this.m_registrationInfo = registrationInfo;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000C3648 File Offset: 0x000C1848
		[FriendAccessAllowed]
		internal bool TryDeregister()
		{
			return this.m_registrationInfo.Source != null && this.m_registrationInfo.Source.SafeAtomicRemove(this.m_registrationInfo.Index, this.m_callbackInfo) == this.m_callbackInfo;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CancellationTokenRegistration" /> class.</summary>
		// Token: 0x060034D8 RID: 13528 RVA: 0x000C369C File Offset: 0x000C189C
		public void Dispose()
		{
			bool flag = this.TryDeregister();
			CancellationCallbackInfo callbackInfo = this.m_callbackInfo;
			if (callbackInfo != null)
			{
				CancellationTokenSource cancellationTokenSource = callbackInfo.CancellationTokenSource;
				if (cancellationTokenSource.IsCancellationRequested && !cancellationTokenSource.IsCancellationCompleted && !flag && cancellationTokenSource.ThreadIDExecutingCallbacks != Thread.CurrentThread.ManagedThreadId)
				{
					cancellationTokenSource.WaitForCallbackToComplete(this.m_callbackInfo);
				}
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal.</summary>
		/// <returns>True if the instances are equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		// Token: 0x060034D9 RID: 13529 RVA: 0x000C36F2 File Offset: 0x000C18F2
		public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are not equal.</summary>
		/// <returns>True if the instances are not equal; otherwise, false.</returns>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		// Token: 0x060034DA RID: 13530 RVA: 0x000C36FC File Offset: 0x000C18FC
		public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return !left.Equals(right);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance is equal to the specified <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>True, if both this and <paramref name="obj" /> are equal. False, otherwise.Two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal if they both refer to the output of a single call to the same Register method of a <see cref="T:System.Threading.CancellationToken" />.</returns>
		/// <param name="obj">The other object to which to compare this instance.</param>
		// Token: 0x060034DB RID: 13531 RVA: 0x000C3709 File Offset: 0x000C1909
		public override bool Equals(object obj)
		{
			return obj is CancellationTokenRegistration && this.Equals((CancellationTokenRegistration)obj);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance is equal to the specified <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>True, if both this and <paramref name="other" /> are equal. False, otherwise. Two <see cref="T:System.Threading.CancellationTokenRegistration" /> instances are equal if they both refer to the output of a single call to the same Register method of a <see cref="T:System.Threading.CancellationToken" />.</returns>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationTokenRegistration" /> to which to compare this instance.</param>
		// Token: 0x060034DC RID: 13532 RVA: 0x000C3724 File Offset: 0x000C1924
		public bool Equals(CancellationTokenRegistration other)
		{
			return this.m_callbackInfo == other.m_callbackInfo && this.m_registrationInfo.Source == other.m_registrationInfo.Source && this.m_registrationInfo.Index == other.m_registrationInfo.Index;
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationTokenRegistration" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationTokenRegistration" /> instance.</returns>
		// Token: 0x060034DD RID: 13533 RVA: 0x000C3780 File Offset: 0x000C1980
		public override int GetHashCode()
		{
			if (this.m_registrationInfo.Source != null)
			{
				return this.m_registrationInfo.Source.GetHashCode() ^ this.m_registrationInfo.Index.GetHashCode();
			}
			return this.m_registrationInfo.Index.GetHashCode();
		}

		// Token: 0x04001C2A RID: 7210
		private readonly CancellationCallbackInfo m_callbackInfo;

		// Token: 0x04001C2B RID: 7211
		private readonly SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> m_registrationInfo;
	}
}
