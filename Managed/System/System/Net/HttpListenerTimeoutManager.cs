using System;

namespace System.Net
{
	/// <summary>The timeout manager to use for an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000528 RID: 1320
	public class HttpListenerTimeoutManager
	{
		/// <summary>Gets or sets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public TimeSpan EntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public TimeSpan DrainEntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public TimeSpan RequestQueue
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06002873 RID: 10355 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public TimeSpan IdleConnection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public TimeSpan HeaderWait
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the minimum send rate, in bytes-per-second, for the response. </summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x00004239 File Offset: 0x00002439
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public long MinSendBytesPerSecond
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
