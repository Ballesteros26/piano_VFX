using System;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Configuration
{
	// Token: 0x0200004B RID: 75
	internal abstract class InternalConfigurationHost : IInternalConfigHost
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00007D8A File Offset: 0x00005F8A
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return null;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00007D8A File Offset: 0x00005F8A
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return null;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007D8D File Offset: 0x00005F8D
		public virtual void DeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00007D95 File Offset: 0x00005F95
		string IInternalConfigHost.DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.DecryptSection(encryptedXml, protectionProvider);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007D9F File Offset: 0x00005F9F
		string IInternalConfigHost.EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.EncryptSection(clearXml, protectionProvider);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00007D24 File Offset: 0x00005F24
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return configPath;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00007DAC File Offset: 0x00005FAC
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				type = Type.GetType(typeName + ",System");
			}
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type '" + typeName + "' not found.");
			}
			return type;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007DFC File Offset: 0x00005FFC
		public virtual string GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00003727 File Offset: 0x00001927
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000271 RID: 625
		public abstract string GetStreamName(string configPath);

		// Token: 0x06000272 RID: 626
		public abstract void Init(IInternalConfigRoot root, params object[] hostInitParams);

		// Token: 0x06000273 RID: 627
		public abstract void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams);

		// Token: 0x06000274 RID: 628 RVA: 0x00007E04 File Offset: 0x00006004
		[MonoNotSupported("mono does not support remote configuration")]
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			throw new NotSupportedException("mono does not support remote configuration");
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003727 File Offset: 0x00001927
		public virtual object GetStreamVersion(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003727 File Offset: 0x00001927
		public virtual IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsAboveApplication(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007E10 File Offset: 0x00006010
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition != ConfigurationAllowDefinition.MachineOnly)
			{
				return allowDefinition != ConfigurationAllowDefinition.MachineToApplication || configPath == "machine" || configPath == "exe";
			}
			return configPath == "machine";
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsFile(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsLocationApplicable(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsRemote
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsSecondaryRoot(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000281 RID: 641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06000282 RID: 642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_app_config();

		// Token: 0x06000283 RID: 643 RVA: 0x00007E48 File Offset: 0x00006048
		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (string.CompareOrdinal(streamName, RuntimeEnvironment.SystemConfigurationFile) == 0)
			{
				string bundled_machine_config = InternalConfigurationHost.get_bundled_machine_config();
				if (bundled_machine_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_machine_config));
				}
			}
			if (string.CompareOrdinal(streamName, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) == 0)
			{
				string bundled_app_config = InternalConfigurationHost.get_bundled_app_config();
				if (bundled_app_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_app_config));
				}
			}
			if (!File.Exists(streamName))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00003727 File Offset: 0x00001927
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007EC0 File Offset: 0x000060C0
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			string directoryName = Path.GetDirectoryName(streamName);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			return new FileStream(streamName, FileMode.Create, FileAccess.Write);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003727 File Offset: 0x00001927
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00003727 File Offset: 0x00001927
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00003727 File Offset: 0x00001927
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00003727 File Offset: 0x00001927
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00003727 File Offset: 0x00001927
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007EF3 File Offset: 0x000060F3
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (!this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				throw new ConfigurationErrorsException("The section can't be defined in this file (the allowed definition context is '" + allowDefinition + "').", errorInfo.Filename, errorInfo.LineNumber);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000023B9 File Offset: 0x000005B9
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000023B9 File Offset: 0x000005B9
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual bool SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual bool SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual bool SupportsRefresh
		{
			get
			{
				return false;
			}
		}
	}
}
