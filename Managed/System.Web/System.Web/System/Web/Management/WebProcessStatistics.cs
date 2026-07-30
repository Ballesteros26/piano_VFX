using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information for assessing the health of a running process.</summary>
	// Token: 0x02000759 RID: 1881
	public class WebProcessStatistics
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebProcessStatistics" /> class. </summary>
		// Token: 0x06004CF1 RID: 19697 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebProcessStatistics()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of application domains in the process.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x000CB0D8 File Offset: 0x000C92D8
		public int AppDomainCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the size of the managed heap.</summary>
		/// <returns>The size of the managed heap.</returns>
		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x000CB0F4 File Offset: 0x000C92F4
		public long ManagedHeapSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the peak working set for the lifetime of the process to date.</summary>
		/// <returns>The peak working set of the process.</returns>
		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x000CB110 File Offset: 0x000C9310
		public long PeakWorkingSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the time when the process started.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> when the process started.</returns>
		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x000CB12C File Offset: 0x000C932C
		public DateTime ProcessStartTime
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the number of requests currently executing.</summary>
		/// <returns>The number of requests that the process is currently executing.</returns>
		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06004CF6 RID: 19702 RVA: 0x000CB148 File Offset: 0x000C9348
		public int RequestsExecuting
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of requests waiting to be processed.</summary>
		/// <returns>The number of requests waiting to be processed.</returns>
		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x000CB164 File Offset: 0x000C9364
		public int RequestsQueued
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of rejected requests.</summary>
		/// <returns>The number of rejected requests.</returns>
		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x000CB180 File Offset: 0x000C9380
		public int RequestsRejected
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total count of the process threads.</summary>
		/// <returns>The total count of the process threads.</returns>
		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x000CB19C File Offset: 0x000C939C
		public int ThreadCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the working set for the process.</summary>
		/// <returns>The working set for the process.</returns>
		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x000CB1B8 File Offset: 0x000C93B8
		public long WorkingSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Formats the process statistics.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004CFB RID: 19707 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
