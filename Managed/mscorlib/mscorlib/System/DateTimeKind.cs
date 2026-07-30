using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether a <see cref="T:System.DateTime" /> object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000148 RID: 328
	[ComVisible(true)]
	[Serializable]
	public enum DateTimeKind
	{
		/// <summary>The time represented is not specified as either local time or Coordinated Universal Time (UTC).</summary>
		// Token: 0x040008B9 RID: 2233
		Unspecified,
		/// <summary>The time represented is UTC.</summary>
		// Token: 0x040008BA RID: 2234
		Utc,
		/// <summary>The time represented is local time.</summary>
		// Token: 0x040008BB RID: 2235
		Local
	}
}
