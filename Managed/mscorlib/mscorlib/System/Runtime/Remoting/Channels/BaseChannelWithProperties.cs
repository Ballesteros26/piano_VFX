using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation for channels that want to expose a dictionary interface to their properties.</summary>
	// Token: 0x02000795 RID: 1941
	[ComVisible(true)]
	public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
	{
		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x0011DCBA File Offset: 0x0011BEBA
		public override IDictionary Properties
		{
			get
			{
				if (this.SinksWithProperties == null || this.SinksWithProperties.Properties == null)
				{
					return base.Properties;
				}
				return new AggregateDictionary(new IDictionary[]
				{
					base.Properties,
					this.SinksWithProperties.Properties
				});
			}
		}

		/// <summary>Indicates the top channel sink in the channel sink stack.</summary>
		// Token: 0x04002A44 RID: 10820
		protected IChannelSinkBase SinksWithProperties;
	}
}
