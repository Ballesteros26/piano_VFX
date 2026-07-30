using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for loader resolution events, such as the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, <see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022A RID: 554
	[ComVisible(true)]
	public class ResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve.</summary>
		/// <param name="name">The name of an item to resolve. </param>
		// Token: 0x06001A5B RID: 6747 RVA: 0x000638E3 File Offset: 0x00061AE3
		public ResolveEventArgs(string name)
		{
			this.m_Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="name">The name of an item to resolve. </param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x06001A5C RID: 6748 RVA: 0x000638F2 File Offset: 0x00061AF2
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this.m_Name = name;
			this.m_Requesting = requestingAssembly;
		}

		/// <summary>Gets the name of the item to resolve.</summary>
		/// <returns>The name of the item to resolve.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00063908 File Offset: 0x00061B08
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		/// <summary>Gets the assembly whose dependency is being resolved.</summary>
		/// <returns>The assembly that requested the item specified by the <see cref="P:System.ResolveEventArgs.Name" /> property.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00063910 File Offset: 0x00061B10
		public Assembly RequestingAssembly
		{
			get
			{
				return this.m_Requesting;
			}
		}

		// Token: 0x04000D1A RID: 3354
		private string m_Name;

		// Token: 0x04000D1B RID: 3355
		private Assembly m_Requesting;
	}
}
