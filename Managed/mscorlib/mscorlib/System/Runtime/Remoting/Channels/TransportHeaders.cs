using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores a collection of headers used in the channel sinks.</summary>
	// Token: 0x020007B9 RID: 1977
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> class.</summary>
		// Token: 0x0600500F RID: 20495 RVA: 0x0011F059 File Offset: 0x0011D259
		public TransportHeaders()
		{
			this.hash_table = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
		}

		/// <summary>Gets or sets a transport header that is associated with the given key.</summary>
		/// <returns>A transport header that is associated with the given key, or null if the key was not found.</returns>
		/// <param name="key">The <see cref="T:System.String" /> that the requested header is associated with. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D73 RID: 3443
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				return this.hash_table[key];
			}
			[SecurityCritical]
			set
			{
				this.hash_table[key] = value;
			}
		}

		/// <summary>Returns an enumerator of the stored transport headers.</summary>
		/// <returns>An enumerator of the stored transport headers.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06005012 RID: 20498 RVA: 0x0011F093 File Offset: 0x0011D293
		[SecurityCritical]
		public IEnumerator GetEnumerator()
		{
			return this.hash_table.GetEnumerator();
		}

		// Token: 0x04002A67 RID: 10855
		private Hashtable hash_table;
	}
}
