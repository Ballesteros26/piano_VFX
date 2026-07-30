using System;

namespace UnityEngine.Playables
{
	// Token: 0x020003AA RID: 938
	public static class PlayableOutputExtensions
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x00037738 File Offset: 0x00035938
		public static bool IsOutputNull<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsNull();
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00037760 File Offset: 0x00035960
		public static bool IsOutputValid<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsValid();
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00037788 File Offset: 0x00035988
		public static Object GetReferenceObject<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetReferenceObject();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000377B0 File Offset: 0x000359B0
		public static void SetReferenceObject<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetReferenceObject(value);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000377D8 File Offset: 0x000359D8
		public static Object GetUserData<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetUserData();
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00037800 File Offset: 0x00035A00
		public static void SetUserData<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetUserData(value);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00037828 File Offset: 0x00035A28
		public static Playable GetSourcePlayable<U>(this U output) where U : struct, IPlayableOutput
		{
			return new Playable(output.GetHandle().GetSourcePlayable());
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00037854 File Offset: 0x00035A54
		public static void SetSourcePlayable<U, V>(this U output, V value) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), output.GetSourceOutputPort<U>());
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0003788C File Offset: 0x00035A8C
		public static void SetSourcePlayable<U, V>(this U output, V value, int port) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), port);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000378C0 File Offset: 0x00035AC0
		public static int GetSourceOutputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000378E8 File Offset: 0x00035AE8
		public static float GetWeight<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetWeight();
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x00037910 File Offset: 0x00035B10
		public static void SetWeight<U>(this U output, float value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetWeight(value);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00037938 File Offset: 0x00035B38
		public static void PushNotification<U>(this U output, Playable origin, INotification notification, object context = null) where U : struct, IPlayableOutput
		{
			output.GetHandle().PushNotification(origin.GetHandle(), notification, context);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00037968 File Offset: 0x00035B68
		public static INotificationReceiver[] GetNotificationReceivers<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetNotificationReceivers();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00037990 File Offset: 0x00035B90
		public static void AddNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().AddNotificationReceiver(receiver);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000379B8 File Offset: 0x00035BB8
		public static void RemoveNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().RemoveNotificationReceiver(receiver);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000379E0 File Offset: 0x00035BE0
		[Obsolete("Method GetSourceInputPort has been renamed to GetSourceOutputPort (UnityUpgradable) -> GetSourceOutputPort<U>(*)", false)]
		public static int GetSourceInputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00037A07 File Offset: 0x00035C07
		[Obsolete("Method SetSourceInputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceInputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00037A07 File Offset: 0x00035C07
		[Obsolete("Method SetSourceOutputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceOutputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}
	}
}
