using System;
using System.Runtime.InteropServices;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000B8 RID: 184
	internal class SecKeyChain : INativeObject, IDisposable
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000E021 File Offset: 0x0000C221
		internal SecKeyChain(IntPtr handle, bool owns = false)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Invalid handle");
			}
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E054 File Offset: 0x0000C254
		static SecKeyChain()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				SecKeyChain.MatchLimit = CFObject.GetIntPtr(intPtr, "kSecMatchLimit");
				SecKeyChain.MatchLimitAll = CFObject.GetIntPtr(intPtr, "kSecMatchLimitAll");
				SecKeyChain.MatchLimitOne = CFObject.GetIntPtr(intPtr, "kSecMatchLimitOne");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public static SecIdentity FindIdentity(SecCertificate certificate, bool throwOnError = false)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			SecIdentity secIdentity = SecKeyChain.FindIdentity((SecCertificate cert) => SecCertificate.Equals(certificate, cert));
			if (!throwOnError || secIdentity != null)
			{
				return secIdentity;
			}
			throw new InvalidOperationException(string.Format("Could not find SecIdentity for certificate '{0}' in keychain.", certificate.SubjectSummary));
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E12C File Offset: 0x0000C32C
		private static SecIdentity FindIdentity(Predicate<SecCertificate> filter)
		{
			using (SecRecord secRecord = new SecRecord(SecKind.Identity))
			{
				SecStatusCode secStatusCode;
				INativeObject[] array = SecKeyChain.QueryAsReference(secRecord, -1, out secStatusCode);
				if (secStatusCode != SecStatusCode.Success || array == null)
				{
					return null;
				}
				foreach (SecIdentity secIdentity in array)
				{
					if (filter(secIdentity.Certificate))
					{
						return secIdentity;
					}
				}
			}
			return null;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		private static INativeObject[] QueryAsReference(SecRecord query, int max, out SecStatusCode result)
		{
			if (query == null)
			{
				result = SecStatusCode.Param;
				return null;
			}
			INativeObject[] array;
			using (CFMutableDictionary cfmutableDictionary = query.QueryDict.MutableCopy())
			{
				cfmutableDictionary.SetValue(CFBoolean.True.Handle, SecItem.ReturnRef);
				SecKeyChain.SetLimit(cfmutableDictionary, max);
				array = SecKeyChain.QueryAsReference(cfmutableDictionary, out result);
			}
			return array;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000E208 File Offset: 0x0000C408
		private static INativeObject[] QueryAsReference(CFDictionary query, out SecStatusCode result)
		{
			if (query == null)
			{
				result = SecStatusCode.Param;
				return null;
			}
			IntPtr intPtr;
			result = SecItem.SecItemCopyMatching(query.Handle, out intPtr);
			if (result == SecStatusCode.Success && intPtr != IntPtr.Zero)
			{
				return CFArray.ArrayFromHandle<INativeObject>(intPtr, delegate(IntPtr p)
				{
					IntPtr typeID = CFType.GetTypeID(p);
					if (typeID == SecCertificate.GetTypeID())
					{
						return new SecCertificate(p, true);
					}
					if (typeID == SecKey.GetTypeID())
					{
						return new SecKey(p, true);
					}
					if (typeID == SecIdentity.GetTypeID())
					{
						return new SecIdentity(p, true);
					}
					throw new Exception(string.Format("Unexpected type: 0x{0:x}", typeID));
				});
			}
			return null;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000E268 File Offset: 0x0000C468
		internal static CFNumber SetLimit(CFMutableDictionary dict, int max)
		{
			CFNumber cfnumber = null;
			IntPtr intPtr;
			if (max == -1)
			{
				intPtr = SecKeyChain.MatchLimitAll;
			}
			else if (max == 1)
			{
				intPtr = SecKeyChain.MatchLimitOne;
			}
			else
			{
				cfnumber = CFNumber.FromInt32(max);
				intPtr = cfnumber.Handle;
			}
			dict.SetValue(intPtr, SecKeyChain.MatchLimit);
			return cfnumber;
		}

		// Token: 0x0600045F RID: 1119
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecKeychainCreate(IntPtr pathName, uint passwordLength, IntPtr password, bool promptUser, IntPtr initialAccess, out IntPtr keychain);

		// Token: 0x06000460 RID: 1120 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		internal static SecKeyChain Create(string pathName, string password)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAnsi(pathName);
			IntPtr intPtr2 = Marshal.StringToHGlobalAnsi(password);
			IntPtr intPtr3;
			SecStatusCode secStatusCode = SecKeyChain.SecKeychainCreate(intPtr, (uint)password.Length, intPtr2, false, IntPtr.Zero, out intPtr3);
			if (secStatusCode != SecStatusCode.Success)
			{
				throw new InvalidOperationException(secStatusCode.ToString());
			}
			return new SecKeyChain(intPtr3, true);
		}

		// Token: 0x06000461 RID: 1121
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecKeychainOpen(IntPtr pathName, out IntPtr keychain);

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		internal static SecKeyChain Open(string pathName)
		{
			IntPtr intPtr = IntPtr.Zero;
			SecKeyChain secKeyChain;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(pathName);
				IntPtr intPtr2;
				SecStatusCode secStatusCode = SecKeyChain.SecKeychainOpen(intPtr, out intPtr2);
				if (secStatusCode != SecStatusCode.Success)
				{
					throw new InvalidOperationException(secStatusCode.ToString());
				}
				secKeyChain = new SecKeyChain(intPtr2, true);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return secKeyChain;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000E360 File Offset: 0x0000C560
		internal static SecKeyChain OpenSystemRootCertificates()
		{
			return SecKeyChain.Open("/System/Library/Keychains/SystemRootCertificates.keychain");
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E36C File Offset: 0x0000C56C
		~SecKeyChain()
		{
			this.Dispose(false);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000E39C File Offset: 0x0000C59C
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000E3B3 File Offset: 0x0000C5B3
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000AD4 RID: 2772
		internal static readonly IntPtr MatchLimitAll;

		// Token: 0x04000AD5 RID: 2773
		internal static readonly IntPtr MatchLimitOne;

		// Token: 0x04000AD6 RID: 2774
		internal static readonly IntPtr MatchLimit;

		// Token: 0x04000AD7 RID: 2775
		private IntPtr handle;
	}
}
