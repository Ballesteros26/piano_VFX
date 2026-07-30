using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides properties and methods to add a license to a component and to manage a <see cref="T:System.ComponentModel.LicenseProvider" />. This class cannot be inherited.</summary>
	// Token: 0x0200029F RID: 671
	[HostProtection(SecurityAction.LinkDemand, ExternalProcessMgmt = true)]
	public sealed class LicenseManager
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x000020EB File Offset: 0x000002EB
		private LicenseManager()
		{
		}

		/// <summary>Gets or sets the current <see cref="T:System.ComponentModel.LicenseContext" />, which specifies when you can use the licensed object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property is currently locked and cannot be changed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00053244 File Offset: 0x00051444
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x000532A4 File Offset: 0x000514A4
		public static LicenseContext CurrentContext
		{
			get
			{
				if (LicenseManager.context == null)
				{
					object obj = LicenseManager.internalSyncObject;
					lock (obj)
					{
						if (LicenseManager.context == null)
						{
							LicenseManager.context = new RuntimeLicenseContext();
						}
					}
				}
				return LicenseManager.context;
			}
			set
			{
				object obj = LicenseManager.internalSyncObject;
				lock (obj)
				{
					if (LicenseManager.contextLockHolder != null)
					{
						throw new InvalidOperationException(global::SR.GetString("The CurrentContext property of the LicenseManager is currently locked and cannot be changed."));
					}
					LicenseManager.context = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.LicenseUsageMode" /> which specifies when you can use the licensed object for the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values, as specified in the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000532FC File Offset: 0x000514FC
		public static LicenseUsageMode UsageMode
		{
			get
			{
				if (LicenseManager.context != null)
				{
					return LicenseManager.context.UsageMode;
				}
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00053318 File Offset: 0x00051518
		private static void CacheProvider(Type type, LicenseProvider provider)
		{
			if (LicenseManager.providers == null)
			{
				LicenseManager.providers = new Hashtable();
			}
			LicenseManager.providers[type] = provider;
			if (provider != null)
			{
				if (LicenseManager.providerInstances == null)
				{
					LicenseManager.providerInstances = new Hashtable();
				}
				LicenseManager.providerInstances[provider.GetType()] = provider;
			}
		}

		/// <summary>Creates an instance of the specified type, given a context in which you can use the licensed instance.</summary>
		/// <returns>An instance of the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance. </param>
		// Token: 0x060014D4 RID: 5332 RVA: 0x00053373 File Offset: 0x00051573
		public static object CreateWithContext(Type type, LicenseContext creationContext)
		{
			return LicenseManager.CreateWithContext(type, creationContext, new object[0]);
		}

		/// <summary>Creates an instance of the specified type with the specified arguments, given a context in which you can use the licensed instance.</summary>
		/// <returns>An instance of the specified type with the given array of arguments.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance. </param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> that represents the arguments for the type. </param>
		// Token: 0x060014D5 RID: 5333 RVA: 0x00053384 File Offset: 0x00051584
		public static object CreateWithContext(Type type, LicenseContext creationContext, object[] args)
		{
			object obj = null;
			object obj2 = LicenseManager.internalSyncObject;
			lock (obj2)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = creationContext;
					LicenseManager.LockContext(LicenseManager.selfLock);
					try
					{
						obj = SecurityUtils.SecureCreateInstance(type, args);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				finally
				{
					LicenseManager.UnlockContext(LicenseManager.selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return obj;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00053410 File Offset: 0x00051610
		private static bool GetCachedNoLicenseProvider(Type type)
		{
			return LicenseManager.providers != null && LicenseManager.providers.ContainsKey(type);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0005342A File Offset: 0x0005162A
		private static LicenseProvider GetCachedProvider(Type type)
		{
			if (LicenseManager.providers != null)
			{
				return (LicenseProvider)LicenseManager.providers[type];
			}
			return null;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00053449 File Offset: 0x00051649
		private static LicenseProvider GetCachedProviderInstance(Type providerType)
		{
			if (LicenseManager.providerInstances != null)
			{
				return (LicenseProvider)LicenseManager.providerInstances[providerType];
			}
			return null;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00053468 File Offset: 0x00051668
		private static IntPtr GetLicenseInteropHelperType()
		{
			return typeof(LicenseManager.LicenseInteropHelper).TypeHandle.Value;
		}

		/// <summary>Returns whether the given type has a valid license.</summary>
		/// <returns>true if the given type is licensed; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to find a valid license for. </param>
		// Token: 0x060014DA RID: 5338 RVA: 0x0005348C File Offset: 0x0005168C
		public static bool IsLicensed(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified type.</summary>
		/// <returns>true if a valid license can be granted; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the <see cref="T:System.ComponentModel.License" />. </param>
		// Token: 0x060014DB RID: 5339 RVA: 0x000534B0 File Offset: 0x000516B0
		public static bool IsValid(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified instance of the type. This method creates a valid <see cref="T:System.ComponentModel.License" />.</summary>
		/// <returns>true if a valid <see cref="T:System.ComponentModel.License" /> can be granted; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <param name="instance">An object of the specified type or a type derived from the specified type. </param>
		/// <param name="license">A <see cref="T:System.ComponentModel.License" /> that is a valid license, or null if a valid license cannot be granted. </param>
		// Token: 0x060014DC RID: 5340 RVA: 0x000534D2 File Offset: 0x000516D2
		public static bool IsValid(Type type, object instance, out License license)
		{
			return LicenseManager.ValidateInternal(type, instance, false, out license);
		}

		/// <summary>Prevents changes being made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to lock. </param>
		/// <exception cref="T:System.InvalidOperationException">The context is already locked.</exception>
		// Token: 0x060014DD RID: 5341 RVA: 0x000534E0 File Offset: 0x000516E0
		public static void LockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != null)
				{
					throw new InvalidOperationException(global::SR.GetString("The CurrentContext property of the LicenseManager is already locked by another user."));
				}
				LicenseManager.contextLockHolder = contextUser;
			}
		}

		/// <summary>Allows changes to be made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to unlock. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextUser" /> represents a different user than the one specified in a previous call to <see cref="M:System.ComponentModel.LicenseManager.LockContext(System.Object)" />. </exception>
		// Token: 0x060014DE RID: 5342 RVA: 0x00053538 File Offset: 0x00051738
		public static void UnlockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != contextUser)
				{
					throw new ArgumentException(global::SR.GetString("The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser."));
				}
				LicenseManager.contextLockHolder = null;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00053590 File Offset: 0x00051790
		private static bool ValidateInternal(Type type, object instance, bool allowExceptions, out License license)
		{
			string text;
			return LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, type, instance, allowExceptions, out license, out text);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000535B0 File Offset: 0x000517B0
		private static bool ValidateInternalRecursive(LicenseContext context, Type type, object instance, bool allowExceptions, out License license, out string licenseKey)
		{
			LicenseProvider licenseProvider = LicenseManager.GetCachedProvider(type);
			if (licenseProvider == null && !LicenseManager.GetCachedNoLicenseProvider(type))
			{
				LicenseProviderAttribute licenseProviderAttribute = (LicenseProviderAttribute)Attribute.GetCustomAttribute(type, typeof(LicenseProviderAttribute), false);
				if (licenseProviderAttribute != null)
				{
					Type licenseProvider2 = licenseProviderAttribute.LicenseProvider;
					licenseProvider = LicenseManager.GetCachedProviderInstance(licenseProvider2);
					if (licenseProvider == null)
					{
						licenseProvider = (LicenseProvider)SecurityUtils.SecureCreateInstance(licenseProvider2);
					}
				}
				LicenseManager.CacheProvider(type, licenseProvider);
			}
			license = null;
			bool flag = true;
			licenseKey = null;
			if (licenseProvider != null)
			{
				license = licenseProvider.GetLicense(context, type, instance, allowExceptions);
				if (license == null)
				{
					flag = false;
				}
				else
				{
					licenseKey = license.LicenseKey;
				}
			}
			if (flag && instance == null)
			{
				Type baseType = type.BaseType;
				if (baseType != typeof(object) && baseType != null)
				{
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
					string text;
					flag = LicenseManager.ValidateInternalRecursive(context, baseType, null, allowExceptions, out license, out text);
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether a license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <exception cref="T:System.ComponentModel.LicenseException">A <see cref="T:System.ComponentModel.License" /> cannot be granted. </exception>
		// Token: 0x060014E1 RID: 5345 RVA: 0x00053698 File Offset: 0x00051898
		public static void Validate(Type type)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, null, true, out license))
			{
				throw new LicenseException(type);
			}
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
		}

		/// <summary>Determines whether a license can be granted for the instance of the specified type.</summary>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license. </param>
		/// <param name="instance">An <see cref="T:System.Object" /> of the specified type or a type derived from the specified type. </param>
		/// <exception cref="T:System.ComponentModel.LicenseException">The type is licensed, but a <see cref="T:System.ComponentModel.License" /> cannot be granted. </exception>
		// Token: 0x060014E2 RID: 5346 RVA: 0x000536C4 File Offset: 0x000518C4
		public static License Validate(Type type, object instance)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, instance, true, out license))
			{
				throw new LicenseException(type, instance);
			}
			return license;
		}

		// Token: 0x040012F4 RID: 4852
		private static readonly object selfLock = new object();

		// Token: 0x040012F5 RID: 4853
		private static volatile LicenseContext context = null;

		// Token: 0x040012F6 RID: 4854
		private static object contextLockHolder = null;

		// Token: 0x040012F7 RID: 4855
		private static volatile Hashtable providers;

		// Token: 0x040012F8 RID: 4856
		private static volatile Hashtable providerInstances;

		// Token: 0x040012F9 RID: 4857
		private static object internalSyncObject = new object();

		// Token: 0x020002A0 RID: 672
		private class LicenseInteropHelper
		{
			// Token: 0x060014E4 RID: 5348 RVA: 0x0005370C File Offset: 0x0005190C
			private static object AllocateAndValidateLicense(RuntimeTypeHandle rth, IntPtr bstrKey, int fDesignTime)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				LicenseManager.LicenseInteropHelper.CLRLicenseContext clrlicenseContext = new LicenseManager.LicenseInteropHelper.CLRLicenseContext((fDesignTime != 0) ? LicenseUsageMode.Designtime : LicenseUsageMode.Runtime, typeFromHandle);
				if (fDesignTime == 0 && bstrKey != (IntPtr)0)
				{
					clrlicenseContext.SetSavedLicenseKey(typeFromHandle, Marshal.PtrToStringBSTR(bstrKey));
				}
				object obj;
				try
				{
					obj = LicenseManager.CreateWithContext(typeFromHandle, clrlicenseContext);
				}
				catch (LicenseException ex)
				{
					throw new COMException(ex.Message, -2147221230);
				}
				return obj;
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x00053778 File Offset: 0x00051978
			private static int RequestLicKey(RuntimeTypeHandle rth, ref IntPtr pbstrKey)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				License license;
				string text;
				if (!LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, typeFromHandle, null, false, out license, out text))
				{
					return -2147483640;
				}
				if (text == null)
				{
					return -2147483640;
				}
				pbstrKey = Marshal.StringToBSTR(text);
				if (license != null)
				{
					license.Dispose();
					license = null;
				}
				return 0;
			}

			// Token: 0x060014E6 RID: 5350 RVA: 0x000537C4 File Offset: 0x000519C4
			private void GetLicInfo(RuntimeTypeHandle rth, ref int pRuntimeKeyAvail, ref int pLicVerified)
			{
				pRuntimeKeyAvail = 0;
				pLicVerified = 0;
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				if (this.helperContext == null)
				{
					this.helperContext = new DesigntimeLicenseContext();
				}
				else
				{
					this.helperContext.savedLicenseKeys.Clear();
				}
				License license;
				string text;
				if (LicenseManager.ValidateInternalRecursive(this.helperContext, typeFromHandle, null, false, out license, out text))
				{
					if (this.helperContext.savedLicenseKeys.Contains(typeFromHandle.AssemblyQualifiedName))
					{
						pRuntimeKeyAvail = 1;
					}
					if (license != null)
					{
						license.Dispose();
						license = null;
						pLicVerified = 1;
					}
				}
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x00053840 File Offset: 0x00051A40
			private void GetCurrentContextInfo(ref int fDesignTime, ref IntPtr bstrKey, RuntimeTypeHandle rth)
			{
				this.savedLicenseContext = LicenseManager.CurrentContext;
				this.savedType = Type.GetTypeFromHandle(rth);
				if (this.savedLicenseContext.UsageMode == LicenseUsageMode.Designtime)
				{
					fDesignTime = 1;
					bstrKey = (IntPtr)0;
					return;
				}
				fDesignTime = 0;
				string savedLicenseKey = this.savedLicenseContext.GetSavedLicenseKey(this.savedType, null);
				bstrKey = Marshal.StringToBSTR(savedLicenseKey);
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x0005389C File Offset: 0x00051A9C
			private void SaveKeyInCurrentContext(IntPtr bstrKey)
			{
				if (bstrKey != (IntPtr)0)
				{
					this.savedLicenseContext.SetSavedLicenseKey(this.savedType, Marshal.PtrToStringBSTR(bstrKey));
				}
			}

			// Token: 0x040012FA RID: 4858
			private const int S_OK = 0;

			// Token: 0x040012FB RID: 4859
			private const int E_NOTIMPL = -2147467263;

			// Token: 0x040012FC RID: 4860
			private const int CLASS_E_NOTLICENSED = -2147221230;

			// Token: 0x040012FD RID: 4861
			private const int E_FAIL = -2147483640;

			// Token: 0x040012FE RID: 4862
			private DesigntimeLicenseContext helperContext;

			// Token: 0x040012FF RID: 4863
			private LicenseContext savedLicenseContext;

			// Token: 0x04001300 RID: 4864
			private Type savedType;

			// Token: 0x020002A1 RID: 673
			internal class CLRLicenseContext : LicenseContext
			{
				// Token: 0x060014EA RID: 5354 RVA: 0x000538C3 File Offset: 0x00051AC3
				public CLRLicenseContext(LicenseUsageMode usageMode, Type type)
				{
					this.usageMode = usageMode;
					this.type = type;
				}

				// Token: 0x17000459 RID: 1113
				// (get) Token: 0x060014EB RID: 5355 RVA: 0x000538D9 File Offset: 0x00051AD9
				public override LicenseUsageMode UsageMode
				{
					get
					{
						return this.usageMode;
					}
				}

				// Token: 0x060014EC RID: 5356 RVA: 0x000538E1 File Offset: 0x00051AE1
				public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
				{
					if (!(type == this.type))
					{
						return null;
					}
					return this.key;
				}

				// Token: 0x060014ED RID: 5357 RVA: 0x000538F9 File Offset: 0x00051AF9
				public override void SetSavedLicenseKey(Type type, string key)
				{
					if (type == this.type)
					{
						this.key = key;
					}
				}

				// Token: 0x04001301 RID: 4865
				private LicenseUsageMode usageMode;

				// Token: 0x04001302 RID: 4866
				private Type type;

				// Token: 0x04001303 RID: 4867
				private string key;
			}
		}
	}
}
