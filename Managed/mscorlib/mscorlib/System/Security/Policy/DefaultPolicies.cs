using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200055F RID: 1375
	internal static class DefaultPolicies
	{
		// Token: 0x06003DD7 RID: 15831 RVA: 0x000DDD4C File Offset: 0x000DBF4C
		public static PermissionSet GetSpecialPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2314740779U)
			{
				if (num != 734303062U)
				{
					if (num != 753551658U)
					{
						if (num == 2314740779U)
						{
							if (name == "LocalIntranet")
							{
								return DefaultPolicies.LocalIntranet;
							}
						}
					}
					else if (name == "Nothing")
					{
						return DefaultPolicies.Nothing;
					}
				}
				else if (name == "FullTrust")
				{
					return DefaultPolicies.FullTrust;
				}
			}
			else if (num <= 3132872517U)
			{
				if (num != 2939433820U)
				{
					if (num == 3132872517U)
					{
						if (name == "SkipVerification")
						{
							return DefaultPolicies.SkipVerification;
						}
					}
				}
				else if (name == "Internet")
				{
					return DefaultPolicies.Internet;
				}
			}
			else if (num != 3650199797U)
			{
				if (num == 4030759744U)
				{
					if (name == "Everything")
					{
						return DefaultPolicies.Everything;
					}
				}
			}
			else if (name == "Execution")
			{
				return DefaultPolicies.Execution;
			}
			return null;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x000DDE5F File Offset: 0x000DC05F
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000DDE77 File Offset: 0x000DC077
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000DDE8F File Offset: 0x000DC08F
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000DDEA7 File Offset: 0x000DC0A7
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000DDEBF File Offset: 0x000DC0BF
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000DDED7 File Offset: 0x000DC0D7
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000DDEEF File Offset: 0x000DC0EF
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000DDF08 File Offset: 0x000DC108
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					strongNamePublicKeyBlob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				strongNamePublicKeyBlob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("4.0.0.0");
			}
			return new StrongNameMembershipCondition(strongNamePublicKeyBlob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000DDF82 File Offset: 0x000DC182
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000DDF90 File Offset: 0x000DC190
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000DE040 File Offset: 0x000DC240
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000DE0B9 File Offset: 0x000DC2B9
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000DE0D3 File Offset: 0x000DC2D3
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000DE0ED File Offset: 0x000DC2ED
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000DE0FC File Offset: 0x000DC2FC
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000DE25A File Offset: 0x000DC45A
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x000DE292 File Offset: 0x000DC492
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
				0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
				82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
				1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
				163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
				253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
				76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
				220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
				11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
				16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
				146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
				232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
				40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
				61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
				15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
				24, 32, 109, 192, 147, 52, 77, 90, 210, 147
			};
		}

		// Token: 0x04001FB1 RID: 8113
		private const string DnsPermissionClass = "System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FB2 RID: 8114
		private const string EventLogPermissionClass = "System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FB3 RID: 8115
		private const string PrintingPermissionClass = "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001FB4 RID: 8116
		private const string SocketPermissionClass = "System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FB5 RID: 8117
		private const string WebPermissionClass = "System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FB6 RID: 8118
		private const string PerformanceCounterPermissionClass = "System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FB7 RID: 8119
		private const string DirectoryServicesPermissionClass = "System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001FB8 RID: 8120
		private const string MessageQueuePermissionClass = "System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001FB9 RID: 8121
		private const string ServiceControllerPermissionClass = "System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001FBA RID: 8122
		private const string OleDbPermissionClass = "System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FBB RID: 8123
		private const string SqlClientPermissionClass = "System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001FBC RID: 8124
		private static Version _fxVersion;

		// Token: 0x04001FBD RID: 8125
		private static byte[] _ecmaKey;

		// Token: 0x04001FBE RID: 8126
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04001FBF RID: 8127
		private static byte[] _msFinalKey;

		// Token: 0x04001FC0 RID: 8128
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04001FC1 RID: 8129
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04001FC2 RID: 8130
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04001FC3 RID: 8131
		private static NamedPermissionSet _internet;

		// Token: 0x04001FC4 RID: 8132
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04001FC5 RID: 8133
		private static NamedPermissionSet _execution;

		// Token: 0x04001FC6 RID: 8134
		private static NamedPermissionSet _nothing;

		// Token: 0x04001FC7 RID: 8135
		private static NamedPermissionSet _everything;

		// Token: 0x02000560 RID: 1376
		public static class ReservedNames
		{
			// Token: 0x06003DE9 RID: 15849 RVA: 0x000DE2C0 File Offset: 0x000DC4C0
			public static bool IsReserved(string name)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2314740779U)
				{
					if (num != 734303062U)
					{
						if (num != 753551658U)
						{
							if (num != 2314740779U)
							{
								return false;
							}
							if (!(name == "LocalIntranet"))
							{
								return false;
							}
						}
						else if (!(name == "Nothing"))
						{
							return false;
						}
					}
					else if (!(name == "FullTrust"))
					{
						return false;
					}
				}
				else if (num <= 3132872517U)
				{
					if (num != 2939433820U)
					{
						if (num != 3132872517U)
						{
							return false;
						}
						if (!(name == "SkipVerification"))
						{
							return false;
						}
					}
					else if (!(name == "Internet"))
					{
						return false;
					}
				}
				else if (num != 3650199797U)
				{
					if (num != 4030759744U)
					{
						return false;
					}
					if (!(name == "Everything"))
					{
						return false;
					}
				}
				else if (!(name == "Execution"))
				{
					return false;
				}
				return true;
			}

			// Token: 0x04001FC8 RID: 8136
			public const string FullTrust = "FullTrust";

			// Token: 0x04001FC9 RID: 8137
			public const string LocalIntranet = "LocalIntranet";

			// Token: 0x04001FCA RID: 8138
			public const string Internet = "Internet";

			// Token: 0x04001FCB RID: 8139
			public const string SkipVerification = "SkipVerification";

			// Token: 0x04001FCC RID: 8140
			public const string Execution = "Execution";

			// Token: 0x04001FCD RID: 8141
			public const string Nothing = "Nothing";

			// Token: 0x04001FCE RID: 8142
			public const string Everything = "Everything";
		}

		// Token: 0x02000561 RID: 1377
		public enum Key
		{
			// Token: 0x04001FD0 RID: 8144
			Ecma,
			// Token: 0x04001FD1 RID: 8145
			MsFinal
		}
	}
}
