using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200017C RID: 380
	[NativeHeader("Runtime/Utilities/PlayerPrefs.h")]
	public class PlayerPrefs
	{
		// Token: 0x06001270 RID: 4720
		[NativeMethod("SetInt")]
		[MethodImpl(4096)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x06001271 RID: 4721
		[NativeMethod("SetFloat")]
		[MethodImpl(4096)]
		private static extern bool TrySetFloat(string key, float value);

		// Token: 0x06001272 RID: 4722
		[NativeMethod("SetString")]
		[MethodImpl(4096)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x06001273 RID: 4723 RVA: 0x0001E690 File Offset: 0x0001C890
		public static void SetInt(string key, int value)
		{
			bool flag = !PlayerPrefs.TrySetInt(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001274 RID: 4724
		[MethodImpl(4096)]
		public static extern int GetInt(string key, int defaultValue);

		// Token: 0x06001275 RID: 4725 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key, 0);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		public static void SetFloat(string key, float value)
		{
			bool flag = !PlayerPrefs.TrySetFloat(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001277 RID: 4727
		[MethodImpl(4096)]
		public static extern float GetFloat(string key, float defaultValue);

		// Token: 0x06001278 RID: 4728 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key, 0f);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0001E71C File Offset: 0x0001C91C
		public static void SetString(string key, string value)
		{
			bool flag = !PlayerPrefs.TrySetSetString(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x0600127A RID: 4730
		[MethodImpl(4096)]
		public static extern string GetString(string key, string defaultValue);

		// Token: 0x0600127B RID: 4731 RVA: 0x0001E744 File Offset: 0x0001C944
		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key, "");
		}

		// Token: 0x0600127C RID: 4732
		[MethodImpl(4096)]
		public static extern bool HasKey(string key);

		// Token: 0x0600127D RID: 4733
		[MethodImpl(4096)]
		public static extern void DeleteKey(string key);

		// Token: 0x0600127E RID: 4734
		[NativeMethod("DeleteAllWithCallback")]
		[MethodImpl(4096)]
		public static extern void DeleteAll();

		// Token: 0x0600127F RID: 4735
		[NativeMethod("Sync")]
		[MethodImpl(4096)]
		public static extern void Save();
	}
}
