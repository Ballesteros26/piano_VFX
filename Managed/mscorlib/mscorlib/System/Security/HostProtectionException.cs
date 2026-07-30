using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>The exception that is thrown when a denied host resource is detected.</summary>
	// Token: 0x0200053C RID: 1340
	[ComVisible(true)]
	[MonoTODO("Not supported in the runtime")]
	[Serializable]
	public class HostProtectionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with default values. </summary>
		// Token: 0x06003C68 RID: 15464 RVA: 0x000D9764 File Offset: 0x000D7964
		public HostProtectionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message. </summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06003C69 RID: 15465 RVA: 0x000C7E43 File Offset: 0x000C6043
		public HostProtectionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="e">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003C6A RID: 15466 RVA: 0x000C7E4C File Offset: 0x000C604C
		public HostProtectionException(string message, Exception e)
			: base(message, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class with a specified error message, the protected host resources, and the host resources that caused the exception to be thrown.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="protectedResources">A bitwise combination of the enumeration values that specify the host resources that are inaccessible to partially trusted code.</param>
		/// <param name="demandedResources">A bitwise combination of the enumeration values that specify the demanded host resources.</param>
		// Token: 0x06003C6B RID: 15467 RVA: 0x000D976C File Offset: 0x000D796C
		public HostProtectionException(string message, HostProtectionResource protectedResources, HostProtectionResource demandedResources)
			: base(message)
		{
			this._protected = protectedResources;
			this._demanded = demandedResources;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostProtectionException" /> class using the provided serialization information and streaming context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003C6C RID: 15468 RVA: 0x000D9783 File Offset: 0x000D7983
		protected HostProtectionException(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Gets or sets the demanded host protection resources that caused the exception to be thrown.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values identifying the protection resources causing the exception to be thrown. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />. </returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000D9793 File Offset: 0x000D7993
		public HostProtectionResource DemandedResources
		{
			get
			{
				return this._demanded;
			}
		}

		/// <summary>Gets or sets the host protection resources that are inaccessible to partially trusted code.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values identifying the inaccessible host protection categories. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x000D979B File Offset: 0x000D799B
		public HostProtectionResource ProtectedResources
		{
			get
			{
				return this._protected;
			}
		}

		/// <summary>Sets the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the host protection exception.</summary>
		/// <param name="info">The serialized object data about the exception being thrown.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003C6F RID: 15471 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
		[SecurityCritical]
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Returns a string representation of the current host protection exception.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.HostProtectionException" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003C70 RID: 15472 RVA: 0x000D97A3 File Offset: 0x000D79A3
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04001F40 RID: 8000
		private HostProtectionResource _protected;

		// Token: 0x04001F41 RID: 8001
		private HostProtectionResource _demanded;
	}
}
