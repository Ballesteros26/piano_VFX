using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.WriteStreamClosed" /> event.</summary>
	// Token: 0x0200047E RID: 1150
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WriteStreamClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WriteStreamClosedEventArgs" /> class.</summary>
		// Token: 0x06002221 RID: 8737 RVA: 0x0000BE61 File Offset: 0x0000A061
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public WriteStreamClosedEventArgs()
		{
		}

		/// <summary>Gets the error value when a write stream is closed.</summary>
		/// <returns>Returns <see cref="T:System.Exception" />.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x00009E57 File Offset: 0x00008057
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public Exception Error
		{
			get
			{
				return null;
			}
		}
	}
}
