using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the <see cref="T:System.Net.HttpListener" /> timeouts element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020007D0 RID: 2000
	public sealed class HttpListenerTimeoutsElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerTimeoutsElement" /> class.</summary>
		// Token: 0x06004009 RID: 16393 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public HttpListenerTimeoutsElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000E0C84 File Offset: 0x000DEE84
		public TimeSpan DrainEntityBody
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x000E0CA0 File Offset: 0x000DEEA0
		public TimeSpan EntityBody
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000E0CBC File Offset: 0x000DEEBC
		public TimeSpan HeaderWait
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x000E0CD8 File Offset: 0x000DEED8
		public TimeSpan IdleConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}

		/// <summary>Gets the minimum send rate, in bytes-per-second, for the response.</summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000E0CF4 File Offset: 0x000DEEF4
		public long MinSendBytesPerSecond
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x000E0D10 File Offset: 0x000DEF10
		public TimeSpan RequestQueue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
		}
	}
}
