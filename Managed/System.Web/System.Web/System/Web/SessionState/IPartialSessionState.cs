using System;
using System.Collections.Generic;

namespace System.Web.SessionState
{
	/// <summary>When implemented in a type, returns a list of zero or more session keys that indicate to a session-state provider which session-state items have to be retrieved.</summary>
	// Token: 0x020006E8 RID: 1768
	public interface IPartialSessionState
	{
		/// <summary>Gets a list of keys that are associated with session-state values.</summary>
		/// <returns>A generic list of strings that serve as keys for session-state values. </returns>
		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06004AD9 RID: 19161
		IList<string> PartialSessionStateKeys { get; }
	}
}
