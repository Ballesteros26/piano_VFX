using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines the lock that implements single-writer/multiple-reader semantics. This is a value type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004A5 RID: 1189
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x060037CF RID: 14287 RVA: 0x000CAD27 File Offset: 0x000C8F27
		internal LockCookie(int thread_id)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = 0;
			this.WriterLocks = 0;
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000CAD3E File Offset: 0x000C8F3E
		internal LockCookie(int thread_id, int reader_locks, int writer_locks)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = reader_locks;
			this.WriterLocks = writer_locks;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060037D1 RID: 14289 RVA: 0x000CAD55 File Offset: 0x000C8F55
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Threading.LockCookie" />.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Threading.LockCookie" /> to compare to the current instance.</param>
		// Token: 0x060037D2 RID: 14290 RVA: 0x000CAD67 File Offset: 0x000C8F67
		public bool Equals(LockCookie obj)
		{
			return this.ThreadId == obj.ThreadId && this.ReaderLocks == obj.ReaderLocks && this.WriterLocks == obj.WriterLocks;
		}

		/// <summary>Indicates whether a specified object is a <see cref="T:System.Threading.LockCookie" /> and is equal to the current instance.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x060037D3 RID: 14291 RVA: 0x000CAD96 File Offset: 0x000C8F96
		public override bool Equals(object obj)
		{
			return obj is LockCookie && obj.Equals(this);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060037D4 RID: 14292 RVA: 0x000CADB3 File Offset: 0x000C8FB3
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060037D5 RID: 14293 RVA: 0x000CADBD File Offset: 0x000C8FBD
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001D3E RID: 7486
		internal int ThreadId;

		// Token: 0x04001D3F RID: 7487
		internal int ReaderLocks;

		// Token: 0x04001D40 RID: 7488
		internal int WriterLocks;
	}
}
