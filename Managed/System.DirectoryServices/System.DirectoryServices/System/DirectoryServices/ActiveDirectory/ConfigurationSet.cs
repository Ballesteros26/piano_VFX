using System;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> class represents a configuration set for one or more AD LDS instances.</summary>
	// Token: 0x02000095 RID: 149
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ConfigurationSet
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00002644 File Offset: 0x00000844
		internal ConfigurationSet()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a read-only collection of AD LDS instances that are in the configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects that are in the configuration set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstanceCollection AdamInstances
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a read-only collection of application partitions that are in the configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartitionCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects that are in the configuration set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00003C27 File Offset: 0x00001E27
		public ApplicationPartitionCollection ApplicationPartitions
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00003C27 File Offset: 0x00001E27
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the AD LDS instance that is the current owner of the domain naming master role.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents the AD LDS instance that currently holds the domain naming master role.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstance NamingRoleOwner
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the schema object for the configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema for this configuration set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00003C27 File Offset: 0x00001E27
		public ActiveDirectorySchema Schema
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the AD LDS instance that is the current owner of the schema operations master role.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents the AD LDS+ instance that currently holds the schema operations master role.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstance SchemaRoleOwner
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a read-only collection of sites that are in the configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects that are in the configuration set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00003C27 File Offset: 0x00001E27
		public ReadOnlySiteCollection Sites
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases all managed and unmanaged resources that are used by the object.</summary>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00002644 File Offset: 0x00000844
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources that are used by the object and optionally releases managed resources.</summary>
		/// <param name="disposing">A <see cref="T:System.Boolean" /> value that determines if the managed resources should be released. true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00002644 File Offset: 0x00000844
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Finds an AD LDS instance in this configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents an AD LDS instance that was found.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">An AD LDS instance could be not found in this configuration set.</exception>
		// Token: 0x060004C9 RID: 1225 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstance FindAdamInstance()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Finds an AD LDS instance in this configuration set for a given partition name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents an AD LDS instance that was found.</returns>
		/// <param name="partitionName">A <see cref="T:System.String" /> that specifies a partition in which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partitionName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">An AD LDS instance for the specified <paramref name="partitionName" /> could not be found in this configuration set.</exception>
		// Token: 0x060004CA RID: 1226 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstance FindAdamInstance(string partitionName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Finds an AD LDS instance in this configuration set for a given partition name and site name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents an AD LDS instance that was found.</returns>
		/// <param name="partitionName">A <see cref="T:System.String" /> that specifies a partition in which to search.</param>
		/// <param name="siteName">A <see cref="T:System.String" /> that specifies a site in which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">An AD LDS instance for the specified <paramref name="partitionName" /> and <paramref name="siteName" /> parameters could not be found in this configuration set.</exception>
		// Token: 0x060004CB RID: 1227 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstance FindAdamInstance(string partitionName, string siteName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns all AD LDS instances in this configuration set.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects that were found.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004CC RID: 1228 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstanceCollection FindAllAdamInstances()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns all AD LDS instances in this configuration set for a given partition name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects that were found.</returns>
		/// <param name="partitionName">A <see cref="T:System.String" /> that specifies a partition in which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partitionName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004CD RID: 1229 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstanceCollection FindAllAdamInstances(string partitionName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns all AD LDS instances in this configuration set for a given partition name and site name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects that are found.</returns>
		/// <param name="partitionName">A <see cref="T:System.String" /> that specifies a partition in which to search.</param>
		/// <param name="siteName">A <see cref="T:System.String" /> that specifies a partition in which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004CE RID: 1230 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamInstanceCollection FindAllAdamInstances(string partitionName, string siteName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns an <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object for the specified directory context.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object that represents the configuration set for the specified context.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of the context can be an AD LDS instance or keywords that are specified in the serviceConnectionPoint object in the current forest that can identify the configuration set.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A target specified in the <paramref name="context" /> parameter could not be found.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="context" /> parameter is not valid.</exception>
		// Token: 0x060004CF RID: 1231 RVA: 0x00003C27 File Offset: 0x00001E27
		public static ConfigurationSet GetConfigurationSet(DirectoryContext context)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the configuration partition.</summary>
		/// <returns> The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the configuration partition.</returns>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00003C27 File Offset: 0x00001E27
		public DirectoryEntry GetDirectoryEntry()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the AD LDS replication security level.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationSecurityLevel" /> value that indicates  the current AD LDS replication authentication mode.</returns>
		// Token: 0x060004D1 RID: 1233 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public ReplicationSecurityLevel GetSecurityLevel()
		{
			ThrowStub.ThrowNotSupportedException();
			return ReplicationSecurityLevel.NegotiatePassThrough;
		}

		/// <summary>Sets the AD LDS replication security level.</summary>
		/// <param name="securityLevel">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationSecurityLevel" /> value to which to set the AD LDS replication authentication mode.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="securityLevel" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationSecurityLevel" /> value.</exception>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00002644 File Offset: 0x00000844
		public void SetSecurityLevel(ReplicationSecurityLevel securityLevel)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
