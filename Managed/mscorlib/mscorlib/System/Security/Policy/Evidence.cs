using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Mono.Security.Authenticode;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Defines the set of information that constitutes input to security policy decisions. This class cannot be inherited.</summary>
	// Token: 0x02000562 RID: 1378
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		/// <summary>Initializes a new empty instance of the <see cref="T:System.Security.Policy.Evidence" /> class.</summary>
		// Token: 0x06003DEA RID: 15850 RVA: 0x00002111 File Offset: 0x00000311
		public Evidence()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from a shallow copy of an existing one.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> instance from which to create the new instance. This instance is not deep-copied. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		// Token: 0x06003DEB RID: 15851 RVA: 0x000DE392 File Offset: 0x000DC592
		public Evidence(Evidence evidence)
		{
			if (evidence != null)
			{
				this.Merge(evidence);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from multiple sets of host and assembly evidence.</summary>
		/// <param name="hostEvidence">The host evidence from which to create the new instance. </param>
		/// <param name="assemblyEvidence">The assembly evidence from which to create the new instance. </param>
		// Token: 0x06003DEC RID: 15852 RVA: 0x000DE3A4 File Offset: 0x000DC5A4
		[Obsolete]
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			if (hostEvidence != null)
			{
				this.HostEvidenceList.AddRange(hostEvidence);
			}
			if (assemblyEvidence != null)
			{
				this.AssemblyEvidenceList.AddRange(assemblyEvidence);
			}
		}

		/// <summary>Gets the number of evidence objects in the evidence set.</summary>
		/// <returns>The number of evidence objects in the evidence set.</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000DE3CC File Offset: 0x000DC5CC
		[Obsolete]
		public int Count
		{
			get
			{
				int num = 0;
				if (this.hostEvidenceList != null)
				{
					num += this.hostEvidenceList.Count;
				}
				if (this.assemblyEvidenceList != null)
				{
					num += this.assemblyEvidenceList.Count;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is read-only.</summary>
		/// <returns>Always false, because read-only evidence sets are not supported.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is thread-safe.</summary>
		/// <returns>Always false because thread-safe evidence sets are not supported.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the evidence is locked.</summary>
		/// <returns>true if the evidence is locked; otherwise, false. The default is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000DE408 File Offset: 0x000DC608
		// (set) Token: 0x06003DF1 RID: 15857 RVA: 0x000DE410 File Offset: 0x000DC610
		public bool Locked
		{
			get
			{
				return this._locked;
			}
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			set
			{
				this._locked = value;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this (Me in Visual Basic), because synchronization of evidence sets is not supported.</returns>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000DE419 File Offset: 0x000DC619
		internal ArrayList HostEvidenceList
		{
			get
			{
				if (this.hostEvidenceList == null)
				{
					this.hostEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.hostEvidenceList;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000DE439 File Offset: 0x000DC639
		internal ArrayList AssemblyEvidenceList
		{
			get
			{
				if (this.assemblyEvidenceList == null)
				{
					this.assemblyEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.assemblyEvidenceList;
			}
		}

		/// <summary>Adds the specified assembly evidence to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000DE459 File Offset: 0x000DC659
		[Obsolete]
		public void AddAssembly(object id)
		{
			this.AssemblyEvidenceList.Add(id);
		}

		/// <summary>Adds the specified evidence supplied by the host to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003DF6 RID: 15862 RVA: 0x000DE468 File Offset: 0x000DC668
		[Obsolete]
		public void AddHost(object id)
		{
			if (this._locked && SecurityManager.SecurityEnabled)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.HostEvidenceList.Add(id);
		}

		/// <summary>Removes the host and assembly evidence from the evidence set.</summary>
		// Token: 0x06003DF7 RID: 15863 RVA: 0x000DE492 File Offset: 0x000DC692
		[ComVisible(false)]
		public void Clear()
		{
			if (this.hostEvidenceList != null)
			{
				this.hostEvidenceList.Clear();
			}
			if (this.assemblyEvidenceList != null)
			{
				this.assemblyEvidenceList.Clear();
			}
		}

		/// <summary>Returns a duplicate copy of this evidence object.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x000DE4BA File Offset: 0x000DC6BA
		[ComVisible(false)]
		public Evidence Clone()
		{
			return new Evidence(this);
		}

		/// <summary>Copies evidence objects to an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy evidence objects. </param>
		/// <param name="index">The zero-based position in the array to which to begin copying evidence objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index " />is outside the range of the target array<paramref name="." /></exception>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000DE4C4 File Offset: 0x000DC6C4
		[Obsolete]
		public void CopyTo(Array array, int index)
		{
			int num = 0;
			if (this.hostEvidenceList != null)
			{
				num = this.hostEvidenceList.Count;
				if (num > 0)
				{
					this.hostEvidenceList.CopyTo(array, index);
				}
			}
			if (this.assemblyEvidenceList != null && this.assemblyEvidenceList.Count > 0)
			{
				this.assemblyEvidenceList.CopyTo(array, index + num);
			}
		}

		/// <summary>Enumerates all evidence in the set, both that provided by the host and that provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by both the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method and the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06003DFA RID: 15866 RVA: 0x000DE520 File Offset: 0x000DC720
		[Obsolete]
		public IEnumerator GetEnumerator()
		{
			IEnumerator enumerator = null;
			if (this.hostEvidenceList != null)
			{
				enumerator = this.hostEvidenceList.GetEnumerator();
			}
			IEnumerator enumerator2 = null;
			if (this.assemblyEvidenceList != null)
			{
				enumerator2 = this.assemblyEvidenceList.GetEnumerator();
			}
			return new Evidence.EvidenceEnumerator(enumerator, enumerator2);
		}

		/// <summary>Enumerates evidence provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06003DFB RID: 15867 RVA: 0x000DE560 File Offset: 0x000DC760
		public IEnumerator GetAssemblyEnumerator()
		{
			return this.AssemblyEvidenceList.GetEnumerator();
		}

		/// <summary>Enumerates evidence supplied by the host.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method.</returns>
		// Token: 0x06003DFC RID: 15868 RVA: 0x000DE56D File Offset: 0x000DC76D
		public IEnumerator GetHostEnumerator()
		{
			return this.HostEvidenceList.GetEnumerator();
		}

		/// <summary>Merges the specified evidence set into the current evidence set.</summary>
		/// <param name="evidence">The evidence set to be merged into the current evidence set. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="P:System.Security.Policy.Evidence.Locked" /> is true, the code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />, and the <paramref name="evidence" /> parameter has a host list that is not empty. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003DFD RID: 15869 RVA: 0x000DE57C File Offset: 0x000DC77C
		public void Merge(Evidence evidence)
		{
			if (evidence != null && evidence.Count > 0)
			{
				if (evidence.hostEvidenceList != null)
				{
					foreach (object obj in evidence.hostEvidenceList)
					{
						this.AddHost(obj);
					}
				}
				if (evidence.assemblyEvidenceList != null)
				{
					foreach (object obj2 in evidence.assemblyEvidenceList)
					{
						this.AddAssembly(obj2);
					}
				}
			}
		}

		/// <summary>Removes the evidence for a given type from the host and assembly enumerations.</summary>
		/// <param name="t">The type of the evidence to be removed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is null.</exception>
		// Token: 0x06003DFE RID: 15870 RVA: 0x000DE634 File Offset: 0x000DC834
		[ComVisible(false)]
		public void RemoveType(Type t)
		{
			for (int i = this.hostEvidenceList.Count; i >= 0; i--)
			{
				if (this.hostEvidenceList.GetType() == t)
				{
					this.hostEvidenceList.RemoveAt(i);
				}
			}
			for (int j = this.assemblyEvidenceList.Count; j >= 0; j--)
			{
				if (this.assemblyEvidenceList.GetType() == t)
				{
					this.assemblyEvidenceList.RemoveAt(j);
				}
			}
		}

		// Token: 0x06003DFF RID: 15871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAuthenticodePresent(Assembly a);

		// Token: 0x06003E00 RID: 15872 RVA: 0x000DE6AC File Offset: 0x000DC8AC
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static Evidence GetDefaultHostEvidence(Assembly a)
		{
			Evidence evidence = new Evidence();
			string escapedCodeBase = a.EscapedCodeBase;
			evidence.AddHost(Zone.CreateFromUrl(escapedCodeBase));
			evidence.AddHost(new Url(escapedCodeBase));
			evidence.AddHost(new Hash(a));
			if (string.Compare("FILE://", 0, escapedCodeBase, 0, 7, true, CultureInfo.InvariantCulture) != 0)
			{
				evidence.AddHost(Site.CreateFromUrl(escapedCodeBase));
			}
			AssemblyName name = a.GetName();
			byte[] publicKey = name.GetPublicKey();
			if (publicKey != null && publicKey.Length != 0)
			{
				StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(publicKey);
				evidence.AddHost(new StrongName(strongNamePublicKeyBlob, name.Name, name.Version));
			}
			if (Evidence.IsAuthenticodePresent(a))
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(a.Location);
				if (authenticodeDeformatter.SigningCertificate != null)
				{
					X509Certificate x509Certificate = new X509Certificate(authenticodeDeformatter.SigningCertificate.RawData);
					if (x509Certificate.GetHashCode() != 0)
					{
						evidence.AddHost(new Publisher(x509Certificate));
					}
				}
			}
			if (a.GlobalAssemblyCache)
			{
				evidence.AddHost(new GacInstalled());
			}
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			if (domainManager != null && (domainManager.HostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				evidence = domainManager.HostSecurityManager.ProvideAssemblyEvidence(a, evidence);
			}
			return evidence;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from multiple sets of host and assembly evidence. </summary>
		/// <param name="hostEvidence">The host evidence from which to create the new instance. </param>
		/// <param name="assemblyEvidence">The assembly evidence from which to create the new instance. </param>
		// Token: 0x06003E01 RID: 15873 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds an evidence object of the specified type to the assembly-supplied evidence list. </summary>
		/// <param name="evidence">The assembly evidence to add.</param>
		/// <typeparam name="T">The type of the object in <paramref name="evidence" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Evidence of type <paramref name="T" /> is already in the list.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="evidence" /> is not serializable.</exception>
		// Token: 0x06003E02 RID: 15874 RVA: 0x0001FB35 File Offset: 0x0001DD35
		[ComVisible(false)]
		public void AddAssemblyEvidence<T>(T evidence)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds host evidence of the specified type to the host evidence collection.</summary>
		/// <param name="evidence">The host evidence to add.</param>
		/// <typeparam name="T">The type of the object in <paramref name="evidence" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">Evidence of type <paramref name="T" /> is already in the list.</exception>
		// Token: 0x06003E03 RID: 15875 RVA: 0x0001FB35 File Offset: 0x0001DD35
		[ComVisible(false)]
		public void AddHostEvidence<T>(T evidence)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets assembly evidence of the specified type from the collection.</summary>
		/// <returns>Evidence of type <paramref name="T" /> in the assembly evidence collection.</returns>
		/// <typeparam name="T">The type of the evidence to get.</typeparam>
		// Token: 0x06003E04 RID: 15876 RVA: 0x000DE7D0 File Offset: 0x000DC9D0
		[ComVisible(false)]
		public T GetAssemblyEvidence<T>()
		{
			ThrowStub.ThrowNotSupportedException();
			return default(T);
		}

		/// <summary>Gets host evidence of the specified type from the collection.</summary>
		/// <returns>Evidence of type <paramref name="T" /> in the host evidence collection.</returns>
		/// <typeparam name="T">The type of the evidence to get.</typeparam>
		// Token: 0x06003E05 RID: 15877 RVA: 0x000DE7EC File Offset: 0x000DC9EC
		[ComVisible(false)]
		public T GetHostEvidence<T>()
		{
			ThrowStub.ThrowNotSupportedException();
			return default(T);
		}

		// Token: 0x04001FD2 RID: 8146
		private bool _locked;

		// Token: 0x04001FD3 RID: 8147
		private ArrayList hostEvidenceList;

		// Token: 0x04001FD4 RID: 8148
		private ArrayList assemblyEvidenceList;

		// Token: 0x02000563 RID: 1379
		private class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06003E06 RID: 15878 RVA: 0x000DE807 File Offset: 0x000DCA07
			public EvidenceEnumerator(IEnumerator hostenum, IEnumerator assemblyenum)
			{
				this.hostEnum = hostenum;
				this.assemblyEnum = assemblyenum;
				this.currentEnum = this.hostEnum;
			}

			// Token: 0x06003E07 RID: 15879 RVA: 0x000DE82C File Offset: 0x000DCA2C
			public bool MoveNext()
			{
				if (this.currentEnum == null)
				{
					return false;
				}
				bool flag = this.currentEnum.MoveNext();
				if (!flag && this.hostEnum == this.currentEnum && this.assemblyEnum != null)
				{
					this.currentEnum = this.assemblyEnum;
					flag = this.assemblyEnum.MoveNext();
				}
				return flag;
			}

			// Token: 0x06003E08 RID: 15880 RVA: 0x000DE884 File Offset: 0x000DCA84
			public void Reset()
			{
				if (this.hostEnum != null)
				{
					this.hostEnum.Reset();
					this.currentEnum = this.hostEnum;
				}
				else
				{
					this.currentEnum = this.assemblyEnum;
				}
				if (this.assemblyEnum != null)
				{
					this.assemblyEnum.Reset();
				}
			}

			// Token: 0x17000A36 RID: 2614
			// (get) Token: 0x06003E09 RID: 15881 RVA: 0x000DE8D1 File Offset: 0x000DCAD1
			public object Current
			{
				get
				{
					return this.currentEnum.Current;
				}
			}

			// Token: 0x04001FD5 RID: 8149
			private IEnumerator currentEnum;

			// Token: 0x04001FD6 RID: 8150
			private IEnumerator hostEnum;

			// Token: 0x04001FD7 RID: 8151
			private IEnumerator assemblyEnum;
		}
	}
}
