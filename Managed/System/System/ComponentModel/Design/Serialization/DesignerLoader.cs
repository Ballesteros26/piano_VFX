using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a basic designer loader interface that can be used to implement a custom designer loader.</summary>
	// Token: 0x0200034B RID: 843
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DesignerLoader
	{
		/// <summary>Gets a value indicating whether the loader is currently loading a document.</summary>
		/// <returns>true if the loader is currently loading a document; otherwise, false.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins loading a designer.</summary>
		/// <param name="host">The loader host through which this loader loads components. </param>
		// Token: 0x06001A53 RID: 6739
		public abstract void BeginLoad(IDesignerLoaderHost host);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		// Token: 0x06001A54 RID: 6740
		public abstract void Dispose();

		/// <summary>Writes cached changes to the location that the designer was loaded from.</summary>
		// Token: 0x06001A55 RID: 6741 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Flush()
		{
		}
	}
}
