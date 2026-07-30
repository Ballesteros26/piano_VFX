using System;
using System.Collections;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x02000064 RID: 100
	internal sealed class X509StoreManager
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00002111 File Offset: 0x00000311
		private X509StoreManager()
		{
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00014FCE File Offset: 0x000131CE
		internal static string CurrentUserPath
		{
			get
			{
				if (X509StoreManager._userPath == null)
				{
					X509StoreManager._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					X509StoreManager._userPath = Path.Combine(X509StoreManager._userPath, "certs");
				}
				return X509StoreManager._userPath;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00015006 File Offset: 0x00013206
		internal static string LocalMachinePath
		{
			get
			{
				if (X509StoreManager._localMachinePath == null)
				{
					X509StoreManager._localMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					X509StoreManager._localMachinePath = Path.Combine(X509StoreManager._localMachinePath, "certs");
				}
				return X509StoreManager._localMachinePath;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001503E File Offset: 0x0001323E
		internal static string NewCurrentUserPath
		{
			get
			{
				if (X509StoreManager._newUserPath == null)
				{
					X509StoreManager._newUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					X509StoreManager._newUserPath = Path.Combine(X509StoreManager._newUserPath, "new-certs");
				}
				return X509StoreManager._newUserPath;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00015076 File Offset: 0x00013276
		internal static string NewLocalMachinePath
		{
			get
			{
				if (X509StoreManager._newLocalMachinePath == null)
				{
					X509StoreManager._newLocalMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					X509StoreManager._newLocalMachinePath = Path.Combine(X509StoreManager._newLocalMachinePath, "new-certs");
				}
				return X509StoreManager._newLocalMachinePath;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000150AE File Offset: 0x000132AE
		public static X509Stores CurrentUser
		{
			get
			{
				if (X509StoreManager._userStore == null)
				{
					X509StoreManager._userStore = new X509Stores(X509StoreManager.CurrentUserPath, false);
				}
				return X509StoreManager._userStore;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000150CC File Offset: 0x000132CC
		public static X509Stores LocalMachine
		{
			get
			{
				if (X509StoreManager._machineStore == null)
				{
					X509StoreManager._machineStore = new X509Stores(X509StoreManager.LocalMachinePath, false);
				}
				return X509StoreManager._machineStore;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000150EA File Offset: 0x000132EA
		public static X509Stores NewCurrentUser
		{
			get
			{
				if (X509StoreManager._newUserStore == null)
				{
					X509StoreManager._newUserStore = new X509Stores(X509StoreManager.NewCurrentUserPath, true);
				}
				return X509StoreManager._newUserStore;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00015108 File Offset: 0x00013308
		public static X509Stores NewLocalMachine
		{
			get
			{
				if (X509StoreManager._newMachineStore == null)
				{
					X509StoreManager._newMachineStore = new X509Stores(X509StoreManager.NewLocalMachinePath, true);
				}
				return X509StoreManager._newMachineStore;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00015126 File Offset: 0x00013326
		public static X509CertificateCollection IntermediateCACertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.IntermediateCA.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.IntermediateCA.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00015157 File Offset: 0x00013357
		public static ArrayList IntermediateCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(X509StoreManager.CurrentUser.IntermediateCA.Crls);
				arrayList.AddRange(X509StoreManager.LocalMachine.IntermediateCA.Crls);
				return arrayList;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00015188 File Offset: 0x00013388
		public static X509CertificateCollection TrustedRootCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.TrustedRoot.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.TrustedRoot.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000151B9 File Offset: 0x000133B9
		public static ArrayList TrustedRootCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(X509StoreManager.CurrentUser.TrustedRoot.Crls);
				arrayList.AddRange(X509StoreManager.LocalMachine.TrustedRoot.Crls);
				return arrayList;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000151EA File Offset: 0x000133EA
		public static X509CertificateCollection UntrustedCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.Untrusted.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.Untrusted.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x04000523 RID: 1315
		private static string _userPath;

		// Token: 0x04000524 RID: 1316
		private static string _localMachinePath;

		// Token: 0x04000525 RID: 1317
		private static string _newUserPath;

		// Token: 0x04000526 RID: 1318
		private static string _newLocalMachinePath;

		// Token: 0x04000527 RID: 1319
		private static X509Stores _userStore;

		// Token: 0x04000528 RID: 1320
		private static X509Stores _machineStore;

		// Token: 0x04000529 RID: 1321
		private static X509Stores _newUserStore;

		// Token: 0x0400052A RID: 1322
		private static X509Stores _newMachineStore;
	}
}
