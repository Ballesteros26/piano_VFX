using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x0200027B RID: 635
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public class PlayerLoop
	{
		// Token: 0x06001A60 RID: 6752 RVA: 0x0002B07C File Offset: 0x0002927C
		public static PlayerLoopSystem GetDefaultPlayerLoop()
		{
			PlayerLoopSystemInternal[] defaultPlayerLoopInternal = PlayerLoop.GetDefaultPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(defaultPlayerLoopInternal, ref num);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0002B0A0 File Offset: 0x000292A0
		public static PlayerLoopSystem GetCurrentPlayerLoop()
		{
			PlayerLoopSystemInternal[] currentPlayerLoopInternal = PlayerLoop.GetCurrentPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(currentPlayerLoopInternal, ref num);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0002B0C4 File Offset: 0x000292C4
		public static void SetPlayerLoop(PlayerLoopSystem loop)
		{
			List<PlayerLoopSystemInternal> list = new List<PlayerLoopSystemInternal>();
			PlayerLoop.PlayerLoopSystemToInternal(loop, ref list);
			PlayerLoop.SetPlayerLoopInternal(list.ToArray());
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0002B0F0 File Offset: 0x000292F0
		private static int PlayerLoopSystemToInternal(PlayerLoopSystem sys, ref List<PlayerLoopSystemInternal> internalSys)
		{
			int count = internalSys.Count;
			PlayerLoopSystemInternal playerLoopSystemInternal = new PlayerLoopSystemInternal
			{
				type = sys.type,
				updateDelegate = sys.updateDelegate,
				updateFunction = sys.updateFunction,
				loopConditionFunction = sys.loopConditionFunction,
				numSubSystems = 0
			};
			internalSys.Add(playerLoopSystemInternal);
			bool flag = sys.subSystemList != null;
			if (flag)
			{
				for (int i = 0; i < sys.subSystemList.Length; i++)
				{
					playerLoopSystemInternal.numSubSystems += PlayerLoop.PlayerLoopSystemToInternal(sys.subSystemList[i], ref internalSys);
				}
			}
			internalSys[count] = playerLoopSystemInternal;
			return playerLoopSystemInternal.numSubSystems + 1;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0002B1BC File Offset: 0x000293BC
		private static PlayerLoopSystem InternalToPlayerLoopSystem(PlayerLoopSystemInternal[] internalSys, ref int offset)
		{
			PlayerLoopSystem playerLoopSystem = new PlayerLoopSystem
			{
				type = internalSys[offset].type,
				updateDelegate = internalSys[offset].updateDelegate,
				updateFunction = internalSys[offset].updateFunction,
				loopConditionFunction = internalSys[offset].loopConditionFunction,
				subSystemList = null
			};
			int num = offset;
			offset = num + 1;
			int num2 = num;
			bool flag = internalSys[num2].numSubSystems > 0;
			if (flag)
			{
				List<PlayerLoopSystem> list = new List<PlayerLoopSystem>();
				while (offset <= num2 + internalSys[num2].numSubSystems)
				{
					list.Add(PlayerLoop.InternalToPlayerLoopSystem(internalSys, ref offset));
				}
				playerLoopSystem.subSystemList = list.ToArray();
			}
			return playerLoopSystem;
		}

		// Token: 0x06001A65 RID: 6757
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern PlayerLoopSystemInternal[] GetDefaultPlayerLoopInternal();

		// Token: 0x06001A66 RID: 6758
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern PlayerLoopSystemInternal[] GetCurrentPlayerLoopInternal();

		// Token: 0x06001A67 RID: 6759
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void SetPlayerLoopInternal(PlayerLoopSystemInternal[] loop);
	}
}
