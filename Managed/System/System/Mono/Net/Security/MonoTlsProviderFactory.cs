using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;
using Mono.Unity;

namespace Mono.Net.Security
{
	// Token: 0x0200007A RID: 122
	internal static class MonoTlsProviderFactory
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00008270 File Offset: 0x00006470
		internal static MonoTlsProvider GetProviderInternal()
		{
			object obj = MonoTlsProviderFactory.locker;
			MonoTlsProvider monoTlsProvider;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeInternal();
				monoTlsProvider = MonoTlsProviderFactory.defaultProvider;
			}
			return monoTlsProvider;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static void InitializeInternal()
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (!MonoTlsProviderFactory.initialized)
				{
					MonoTlsProviderFactory.InitializeProviderRegistration();
					MonoTlsProvider monoTlsProvider;
					try
					{
						monoTlsProvider = MonoTlsProviderFactory.CreateDefaultProviderImpl();
					}
					catch (Exception ex)
					{
						throw new NotSupportedException("TLS Support not available.", ex);
					}
					if (monoTlsProvider == null)
					{
						throw new NotSupportedException("TLS Support not available.");
					}
					if (!MonoTlsProviderFactory.providerCache.ContainsKey(monoTlsProvider.ID))
					{
						MonoTlsProviderFactory.providerCache.Add(monoTlsProvider.ID, monoTlsProvider);
					}
					X509Helper2.Initialize();
					MonoTlsProviderFactory.defaultProvider = monoTlsProvider;
					MonoTlsProviderFactory.initialized = true;
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00008364 File Offset: 0x00006564
		internal static void InitializeInternal(string provider)
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (MonoTlsProviderFactory.initialized)
				{
					throw new NotSupportedException("TLS Subsystem already initialized.");
				}
				MonoTlsProviderFactory.defaultProvider = MonoTlsProviderFactory.LookupProvider(provider, true);
				X509Helper2.Initialize();
				MonoTlsProviderFactory.initialized = true;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000083C8 File Offset: 0x000065C8
		private static Type LookupProviderType(string name, bool throwOnError)
		{
			object obj = MonoTlsProviderFactory.locker;
			Type type;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				Tuple<Guid, string> tuple;
				if (!MonoTlsProviderFactory.providerRegistration.TryGetValue(name, out tuple))
				{
					if (throwOnError)
					{
						throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
					}
					type = null;
				}
				else
				{
					Type type2 = Type.GetType(tuple.Item2, false);
					if (type2 == null && throwOnError)
					{
						throw new NotSupportedException(string.Format("Could not find TLS Provider: `{0}'.", tuple.Item2));
					}
					type = type2;
				}
			}
			return type;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000845C File Offset: 0x0000665C
		private static MonoTlsProvider LookupProvider(string name, bool throwOnError)
		{
			object obj = MonoTlsProviderFactory.locker;
			MonoTlsProvider monoTlsProvider;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				Tuple<Guid, string> tuple;
				MonoTlsProvider monoTlsProvider2;
				if (!MonoTlsProviderFactory.providerRegistration.TryGetValue(name, out tuple))
				{
					if (throwOnError)
					{
						throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
					}
					monoTlsProvider = null;
				}
				else if (MonoTlsProviderFactory.providerCache.TryGetValue(tuple.Item1, out monoTlsProvider2))
				{
					monoTlsProvider = monoTlsProvider2;
				}
				else
				{
					Type type = Type.GetType(tuple.Item2, false);
					if (type == null && throwOnError)
					{
						throw new NotSupportedException(string.Format("Could not find TLS Provider: `{0}'.", tuple.Item2));
					}
					try
					{
						monoTlsProvider2 = (MonoTlsProvider)Activator.CreateInstance(type, true);
					}
					catch (Exception ex)
					{
						throw new NotSupportedException(string.Format("Unable to instantiate TLS Provider `{0}'.", type), ex);
					}
					if (monoTlsProvider2 == null)
					{
						if (throwOnError)
						{
							throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
						}
						monoTlsProvider = null;
					}
					else
					{
						MonoTlsProviderFactory.providerCache.Add(tuple.Item1, monoTlsProvider2);
						monoTlsProvider = monoTlsProvider2;
					}
				}
			}
			return monoTlsProvider;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008578 File Offset: 0x00006778
		[Conditional("MONO_TLS_DEBUG")]
		private static void InitializeDebug()
		{
			if (Environment.GetEnvironmentVariable("MONO_TLS_DEBUG") != null)
			{
				MonoTlsProviderFactory.enableDebug = true;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000858C File Offset: 0x0000678C
		[Conditional("MONO_TLS_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
			if (MonoTlsProviderFactory.enableDebug)
			{
				Console.Error.WriteLine(message, args);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000085A4 File Offset: 0x000067A4
		private static void InitializeProviderRegistration()
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (MonoTlsProviderFactory.providerRegistration == null)
				{
					MonoTlsProviderFactory.providerRegistration = new Dictionary<string, Tuple<Guid, string>>();
					MonoTlsProviderFactory.providerCache = new Dictionary<Guid, MonoTlsProvider>();
					if (UnityTls.IsSupported)
					{
						Tuple<Guid, string> tuple = new Tuple<Guid, string>(MonoTlsProviderFactory.UnityTlsId, "Mono.Unity.UnityTlsProvider");
						MonoTlsProviderFactory.providerRegistration.Add("default", tuple);
						MonoTlsProviderFactory.providerRegistration.Add("unitytls", tuple);
					}
					else
					{
						Tuple<Guid, string> tuple2 = new Tuple<Guid, string>(MonoTlsProviderFactory.AppleTlsId, "Mono.AppleTls.AppleTlsProvider");
						Tuple<Guid, string> tuple3 = new Tuple<Guid, string>(MonoTlsProviderFactory.LegacyId, "Mono.Net.Security.LegacyTlsProvider");
						MonoTlsProviderFactory.providerRegistration.Add("legacy", tuple3);
						Tuple<Guid, string> tuple4 = null;
						if (Platform.IsMacOS)
						{
							MonoTlsProviderFactory.providerRegistration.Add("default", tuple2);
						}
						else if (tuple4 != null)
						{
							MonoTlsProviderFactory.providerRegistration.Add("default", tuple4);
						}
						else
						{
							MonoTlsProviderFactory.providerRegistration.Add("default", tuple3);
						}
						MonoTlsProviderFactory.providerRegistration.Add("apple", tuple2);
					}
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000086C0 File Offset: 0x000068C0
		private static MonoTlsProvider CreateDefaultProviderImpl()
		{
			string text = Environment.GetEnvironmentVariable("MONO_TLS_PROVIDER");
			if (string.IsNullOrEmpty(text))
			{
				text = "default";
			}
			return MonoTlsProviderFactory.LookupProvider(text, true);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000086ED File Offset: 0x000068ED
		internal static MonoTlsProvider GetProvider()
		{
			MonoTlsProvider providerInternal = MonoTlsProviderFactory.GetProviderInternal();
			if (providerInternal == null)
			{
				throw new NotSupportedException("No TLS Provider available.");
			}
			return providerInternal;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00008704 File Offset: 0x00006904
		internal static bool IsProviderSupported(string name)
		{
			object obj = MonoTlsProviderFactory.locker;
			bool flag2;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				flag2 = MonoTlsProviderFactory.providerRegistration.ContainsKey(name);
			}
			return flag2;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008750 File Offset: 0x00006950
		internal static MonoTlsProvider GetProvider(string name)
		{
			return MonoTlsProviderFactory.LookupProvider(name, false);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000875C File Offset: 0x0000695C
		internal static bool IsInitialized
		{
			get
			{
				object obj = MonoTlsProviderFactory.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = MonoTlsProviderFactory.initialized;
				}
				return flag2;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000879C File Offset: 0x0000699C
		internal static void Initialize()
		{
			MonoTlsProviderFactory.InitializeInternal();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000087A3 File Offset: 0x000069A3
		internal static void Initialize(string provider)
		{
			MonoTlsProviderFactory.InitializeInternal(provider);
		}

		// Token: 0x040007EF RID: 2031
		private static object locker = new object();

		// Token: 0x040007F0 RID: 2032
		private static bool initialized;

		// Token: 0x040007F1 RID: 2033
		private static MonoTlsProvider defaultProvider;

		// Token: 0x040007F2 RID: 2034
		private static Dictionary<string, Tuple<Guid, string>> providerRegistration;

		// Token: 0x040007F3 RID: 2035
		private static Dictionary<Guid, MonoTlsProvider> providerCache;

		// Token: 0x040007F4 RID: 2036
		private static bool enableDebug;

		// Token: 0x040007F5 RID: 2037
		internal static readonly Guid UnityTlsId = new Guid("06414A97-74F6-488F-877B-A6CA9BBEB82E");

		// Token: 0x040007F6 RID: 2038
		internal static readonly Guid AppleTlsId = new Guid("981af8af-a3a3-419a-9f01-a518e3a17c1c");

		// Token: 0x040007F7 RID: 2039
		internal static readonly Guid BtlsId = new Guid("432d18c9-9348-4b90-bfbf-9f2a10e1f15b");

		// Token: 0x040007F8 RID: 2040
		internal static readonly Guid LegacyId = new Guid("809e77d5-56cc-4da8-b9f0-45e65ba9cceb");
	}
}
