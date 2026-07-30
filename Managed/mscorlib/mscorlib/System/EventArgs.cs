using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the base class for classes that contain event data, and provides a value to use for events that do not include event data. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015A RID: 346
	[ComVisible(true)]
	[Serializable]
	public class EventArgs
	{
		/// <summary>Provides a value to use with events that do not have event data.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000900 RID: 2304
		public static readonly EventArgs Empty = new EventArgs();
	}
}
