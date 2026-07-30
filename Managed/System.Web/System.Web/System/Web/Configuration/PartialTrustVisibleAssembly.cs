using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures a partial-trust assembly. This class cannot be inherited.</summary>
	// Token: 0x020006AE RID: 1710
	public sealed class PartialTrustVisibleAssembly : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembly" /> class. </summary>
		/// <param name="assemblyName">The assembly name.</param>
		/// <param name="publicKey">The hexadecimal string representation of the public key that is associated with the assembly.</param>
		// Token: 0x06004830 RID: 18480 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PartialTrustVisibleAssembly(string assemblyName, string publicKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the assembly name.</summary>
		/// <returns>The assembly name. </returns>
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004832 RID: 18482 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string AssemblyName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the assembly public key.</summary>
		/// <returns>The hexadecimal string representation of the public key that is associated with the assembly.</returns>
		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004834 RID: 18484 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string PublicKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
