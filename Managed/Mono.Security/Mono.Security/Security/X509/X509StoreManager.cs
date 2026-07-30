using System;
using System.Collections;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x0200001B RID: 27
	public sealed class X509StoreManager
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0000B1DA File Offset: 0x000093DA
		private X509StoreManager()
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000B1E2 File Offset: 0x000093E2
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000B21A File Offset: 0x0000941A
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B252 File Offset: 0x00009452
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000B28A File Offset: 0x0000948A
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B2C2 File Offset: 0x000094C2
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000B2E0 File Offset: 0x000094E0
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000B2FE File Offset: 0x000094FE
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000B31C File Offset: 0x0000951C
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000B33A File Offset: 0x0000953A
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000B36B File Offset: 0x0000956B
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000B39C File Offset: 0x0000959C
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B3CD File Offset: 0x000095CD
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000B3FE File Offset: 0x000095FE
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

		// Token: 0x040000C6 RID: 198
		private static string _userPath;

		// Token: 0x040000C7 RID: 199
		private static string _localMachinePath;

		// Token: 0x040000C8 RID: 200
		private static string _newUserPath;

		// Token: 0x040000C9 RID: 201
		private static string _newLocalMachinePath;

		// Token: 0x040000CA RID: 202
		private static X509Stores _userStore;

		// Token: 0x040000CB RID: 203
		private static X509Stores _machineStore;

		// Token: 0x040000CC RID: 204
		private static X509Stores _newUserStore;

		// Token: 0x040000CD RID: 205
		private static X509Stores _newMachineStore;
	}
}
