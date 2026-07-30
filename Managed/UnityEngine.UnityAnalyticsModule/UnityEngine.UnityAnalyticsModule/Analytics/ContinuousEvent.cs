using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x02000007 RID: 7
	[ExcludeFromDocs]
	[RequiredByNativeCode]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[NativeHeader("Modules/UnityAnalytics/ContinuousEvent/Manager.h")]
	public class ContinuousEvent
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002ACC File Offset: 0x00000CCC
		public static AnalyticsResult RegisterCollector<T>(string metricName, Func<T> del) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(metricName);
			if (flag)
			{
				throw new ArgumentException("Cannot set metric name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalRegisterCollector(typeof(T).ToString(), metricName, del);
			}
			return analyticsResult;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B1C File Offset: 0x00000D1C
		public static AnalyticsResult SetEventHistogramThresholds<T>(string eventName, int count, T[] data, int ver = 1, string prefix = "") where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalSetEventHistogramThresholds(typeof(T).ToString(), eventName, count, data, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B70 File Offset: 0x00000D70
		public static AnalyticsResult SetCustomEventHistogramThresholds<T>(string eventName, int count, T[] data) where T : struct, IComparable<T>, IEquatable<T>
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalSetCustomEventHistogramThresholds(typeof(T).ToString(), eventName, count, data);
			}
			return analyticsResult;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static AnalyticsResult ConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled = true)
		{
			bool flag = string.IsNullOrEmpty(customEventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalConfigureCustomEvent(customEventName, metricName, interval, period, enabled);
			}
			return analyticsResult;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C04 File Offset: 0x00000E04
		public static AnalyticsResult ConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled = true, int ver = 1, string prefix = "")
		{
			bool flag = string.IsNullOrEmpty(eventName);
			if (flag)
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			bool flag2 = !ContinuousEvent.IsInitialized();
			AnalyticsResult analyticsResult;
			if (flag2)
			{
				analyticsResult = AnalyticsResult.NotInitialized;
			}
			else
			{
				analyticsResult = ContinuousEvent.InternalConfigureEvent(eventName, metricName, interval, period, enabled, ver, prefix);
			}
			return analyticsResult;
		}

		// Token: 0x06000067 RID: 103
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern AnalyticsResult InternalRegisterCollector(string type, string metricName, object collector);

		// Token: 0x06000068 RID: 104
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern AnalyticsResult InternalSetEventHistogramThresholds(string type, string eventName, int count, object data, int ver, string prefix);

		// Token: 0x06000069 RID: 105
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern AnalyticsResult InternalSetCustomEventHistogramThresholds(string type, string eventName, int count, object data);

		// Token: 0x0600006A RID: 106
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern AnalyticsResult InternalConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled);

		// Token: 0x0600006B RID: 107
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern AnalyticsResult InternalConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled, int ver, string prefix);

		// Token: 0x0600006C RID: 108 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal static bool IsInitialized()
		{
			return Analytics.IsInitialized();
		}
	}
}
