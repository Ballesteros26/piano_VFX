using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System
{
	/// <summary>Identifies the activation context for the current application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001FF RID: 511
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x0005CE32 File Offset: 0x0005B032
		private ActivationContext(ApplicationIdentity identity)
		{
			this._appid = identity;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0005CE44 File Offset: 0x0005B044
		~ActivationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the form, or store context, for the current application. </summary>
		/// <returns>One of the enumeration values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0005CE74 File Offset: 0x0005B074
		public ActivationContext.ContextForm Form
		{
			get
			{
				return this._form;
			}
		}

		/// <summary>Gets the application identity for the current application.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object that identifies the current application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0005CE7C File Offset: 0x0005B07C
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity.</summary>
		/// <returns>An object with the specified application identity.</returns>
		/// <param name="identity">An object that identifies an application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060017CB RID: 6091 RVA: 0x0005CE84 File Offset: 0x0005B084
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity and array of manifest paths.</summary>
		/// <returns>An object with the specified application identity and array of manifest paths.</returns>
		/// <param name="identity">An object that identifies an application.</param>
		/// <param name="manifestPaths">A string array of manifest paths for the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="manifestPaths" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.-or-<paramref name="identity" /> does not match the identity in the manifests.-or-<paramref name="identity" /> does not have the same number of components as the manifest paths.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060017CC RID: 6092 RVA: 0x0005CE9A File Offset: 0x0005B09A
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (manifestPaths == null)
			{
				throw new ArgumentNullException("manifestPaths");
			}
			return new ActivationContext(identity);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ActivationContext" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017CD RID: 6093 RVA: 0x0005CEBE File Offset: 0x0005B0BE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0005CECD File Offset: 0x0005B0CD
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				this._disposed = true;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The structure for this serialization.</param>
		// Token: 0x060017CF RID: 6095 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ActivationContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the ClickOnce application manifest for the current application.</summary>
		/// <returns>A byte array that contains the ClickOnce application manifest for the application that is associated with this <see cref="T:System.ActivationContext" />.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00032521 File Offset: 0x00030721
		public byte[] ApplicationManifestBytes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the ClickOnce deployment manifest for the current application.</summary>
		/// <returns>A byte array that contains the ClickOnce deployment manifest for the application that is associated with this <see cref="T:System.ActivationContext" />.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00032521 File Offset: 0x00030721
		public byte[] DeploymentManifestBytes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04000C59 RID: 3161
		private ApplicationIdentity _appid;

		// Token: 0x04000C5A RID: 3162
		private ActivationContext.ContextForm _form;

		// Token: 0x04000C5B RID: 3163
		private bool _disposed;

		/// <summary>Indicates the context for a manifest-activated application.</summary>
		// Token: 0x02000200 RID: 512
		public enum ContextForm
		{
			/// <summary>The application is not in the ClickOnce store.</summary>
			// Token: 0x04000C5D RID: 3165
			Loose,
			/// <summary>The application is contained in the ClickOnce store.</summary>
			// Token: 0x04000C5E RID: 3166
			StoreBounded
		}
	}
}
