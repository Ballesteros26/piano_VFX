using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores sink provider data for sink providers.</summary>
	// Token: 0x020007B8 RID: 1976
	[ComVisible(true)]
	public class SinkProviderData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> class.</summary>
		/// <param name="name">The name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with. </param>
		// Token: 0x0600500B RID: 20491 RVA: 0x0011F01C File Offset: 0x0011D21C
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		/// <summary>Gets a list of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x0011F041 File Offset: 0x0011D241
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		/// <summary>Gets the name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> with the name of the XML node that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600500D RID: 20493 RVA: 0x0011F049 File Offset: 0x0011D249
		public string Name
		{
			get
			{
				return this.sinkName;
			}
		}

		/// <summary>Gets a dictionary through which properties on the sink provider can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink provider can be accessed.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x0011F051 File Offset: 0x0011D251
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002A64 RID: 10852
		private string sinkName;

		// Token: 0x04002A65 RID: 10853
		private ArrayList children;

		// Token: 0x04002A66 RID: 10854
		private Hashtable properties;
	}
}
