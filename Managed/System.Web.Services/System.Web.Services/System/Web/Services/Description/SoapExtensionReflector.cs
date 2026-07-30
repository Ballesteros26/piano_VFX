using System;
using System.Security.Permissions;

namespace System.Web.Services.Description
{
	/// <summary>Provides a common interface and functionality for classes to add SOAP extension information to a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object on a per-method basis.</summary>
	// Token: 0x0200011E RID: 286
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public abstract class SoapExtensionReflector
	{
		/// <summary>Abstract method that a derived class must implement to add SOAP extension information to a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object on a per-method basis.</summary>
		// Token: 0x0600089E RID: 2206
		public abstract void ReflectMethod();

		/// <summary>Generates service-specific description information that gets placed in a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object corresponding to a binding.</summary>
		// Token: 0x0600089F RID: 2207 RVA: 0x0000210D File Offset: 0x0000030D
		public virtual void ReflectDescription()
		{
		}

		/// <summary>Gets or sets the instance of a class derived from the abstract <see cref="T:System.Web.Services.Description.ProtocolReflector" /> class that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionReflector.ReflectMethod" /> method.</summary>
		/// <returns>The instance of a class derived from the abstract <see cref="T:System.Web.Services.Description.ProtocolReflector" /> class that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionReflector.ReflectMethod" /> method.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0003C404 File Offset: 0x0003A604
		public ProtocolReflector ReflectionContext
		{
			get
			{
				return this.protocolReflector;
			}
			set
			{
				this.protocolReflector = value;
			}
		}

		// Token: 0x04000528 RID: 1320
		private ProtocolReflector protocolReflector;
	}
}
