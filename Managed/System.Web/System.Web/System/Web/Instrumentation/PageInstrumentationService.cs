using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides information about page execution listeners and instrumentation.</summary>
	// Token: 0x0200069E RID: 1694
	public sealed class PageInstrumentationService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageInstrumentationService" /> class.</summary>
		// Token: 0x060047BB RID: 18363 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PageInstrumentationService()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a list of listeners that are subscribed to the page execution process.</summary>
		/// <returns>The list of listeners that are subscribed to the page execution process.</returns>
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IList<PageExecutionListener> ExecutionListeners
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets a value that indicates whether instrumentation is active for the entire application.</summary>
		/// <returns>true to indicate that instrumentation is active for the entire application; otherwise, false.</returns>
		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x000C9D98 File Offset: 0x000C7F98
		// (set) Token: 0x060047BE RID: 18366 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static bool IsEnabled
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
