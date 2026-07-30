using System;
using System.Reflection;
using System.Threading;

namespace System.Xml.Serialization
{
	/// <summary>An abstract class that is the base class for <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> and <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> and that contains methods common to both of these types.</summary>
	// Token: 0x02000348 RID: 840
	public abstract class XmlSerializationGeneratedCode
	{
		// Token: 0x060020AE RID: 8366 RVA: 0x000B680C File Offset: 0x000B4A0C
		internal void Init(TempAssembly tempAssembly)
		{
			this.tempAssembly = tempAssembly;
			if (tempAssembly != null && tempAssembly.NeedAssembyResolve)
			{
				this.threadCode = Thread.CurrentThread.GetHashCode();
				this.assemblyResolver = new ResolveEventHandler(this.OnAssemblyResolve);
				AppDomain.CurrentDomain.AssemblyResolve += this.assemblyResolver;
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000B685D File Offset: 0x000B4A5D
		internal void Dispose()
		{
			if (this.assemblyResolver != null)
			{
				AppDomain.CurrentDomain.AssemblyResolve -= this.assemblyResolver;
			}
			this.assemblyResolver = null;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000B687E File Offset: 0x000B4A7E
		internal Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (this.tempAssembly != null && Thread.CurrentThread.GetHashCode() == this.threadCode)
			{
				return this.tempAssembly.GetReferencedAssembly(args.Name);
			}
			return null;
		}

		// Token: 0x0400178D RID: 6029
		private TempAssembly tempAssembly;

		// Token: 0x0400178E RID: 6030
		private int threadCode;

		// Token: 0x0400178F RID: 6031
		private ResolveEventHandler assemblyResolver;
	}
}
