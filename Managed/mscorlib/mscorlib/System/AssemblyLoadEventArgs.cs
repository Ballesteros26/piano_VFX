using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000206 RID: 518
	[ComVisible(true)]
	public class AssemblyLoadEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AssemblyLoadEventArgs" /> class using the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="loadedAssembly">An instance that represents the currently loaded assembly. </param>
		// Token: 0x0600182F RID: 6191 RVA: 0x0005D6E3 File Offset: 0x0005B8E3
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.m_loadedAssembly = loadedAssembly;
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</summary>
		/// <returns>An instance of <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0005D6F2 File Offset: 0x0005B8F2
		public Assembly LoadedAssembly
		{
			get
			{
				return this.m_loadedAssembly;
			}
		}

		// Token: 0x04000C7E RID: 3198
		private Assembly m_loadedAssembly;
	}
}
