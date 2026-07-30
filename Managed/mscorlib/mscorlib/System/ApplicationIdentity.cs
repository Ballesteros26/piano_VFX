using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Provides the ability to uniquely identify a manifest-activated application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000204 RID: 516
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationIdentity" /> class. </summary>
		/// <param name="applicationIdentityFullName">The full name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentityFullName" /> is null.</exception>
		// Token: 0x0600181D RID: 6173 RVA: 0x0005D54F File Offset: 0x0005B74F
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
				return;
			}
			this._fullName = applicationIdentityFullName;
		}

		/// <summary>Gets the location of the deployment manifest as a URL.</summary>
		/// <returns>The URL of the deployment manifest.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0005D58C File Offset: 0x0005B78C
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
		}

		/// <summary>Gets the full name of the application.</summary>
		/// <returns>The full name of the application, also known as the display name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0005D594 File Offset: 0x0005B794
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		/// <summary>Returns the full name of the manifest-activated application.</summary>
		/// <returns>The full name of the manifest-activated application.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001820 RID: 6176 RVA: 0x0005D594 File Offset: 0x0005B794
		public override string ToString()
		{
			return this._fullName;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />) structure for the serialization.</param>
		// Token: 0x06001821 RID: 6177 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x04000C78 RID: 3192
		private string _fullName;

		// Token: 0x04000C79 RID: 3193
		private string _codeBase;
	}
}
