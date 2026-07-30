using System;
using System.Collections;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for creating services for administering a Web site at design time.</summary>
	// Token: 0x02000095 RID: 149
	public interface IWebAdministrationService
	{
		/// <summary>Starts the Web administration facility in the design host.</summary>
		/// <param name="arguments">An <see cref="T:System.Collections.IDictionary" />.</param>
		// Token: 0x0600048E RID: 1166
		void Start(IDictionary arguments);
	}
}
