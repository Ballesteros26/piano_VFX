using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	// Token: 0x0200020D RID: 525
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteDataAccessExtensions
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x00025C9C File Offset: 0x00023E9C
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool flag;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				flag = typeof(T) == typeof(Vector3);
				break;
			case VertexAttribute.Tangent:
				flag = typeof(T) == typeof(Vector4);
				break;
			case VertexAttribute.Color:
				flag = typeof(T) == typeof(Color32);
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				flag = typeof(T) == typeof(Vector2);
				break;
			case VertexAttribute.BlendWeight:
				flag = typeof(T) == typeof(BoneWeight);
				break;
			default:
				throw new InvalidOperationException(string.Format("The requested channel '{0}' is unknown.", channel));
			}
			bool flag2 = !flag;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The requested channel '{0}' does not match the return type {1}.", channel, typeof(T).Name));
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00025DAC File Offset: 0x00023FAC
		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo channelInfo = SpriteDataAccessExtensions.GetChannelInfo(sprite, channel);
			byte* ptr = (byte*)channelInfo.buffer + channelInfo.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)ptr, channelInfo.stride, channelInfo.count);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00025DF3 File Offset: 0x00023FF3
		public static void SetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel, NativeArray<T> src) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteDataAccessExtensions.SetChannelData(sprite, channel, src.GetUnsafeReadOnlyPtr<T>());
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00025E0C File Offset: 0x0002400C
		public static NativeArray<Matrix4x4> GetBindPoses(this Sprite sprite)
		{
			SpriteChannelInfo bindPoseInfo = SpriteDataAccessExtensions.GetBindPoseInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(bindPoseInfo.buffer, bindPoseInfo.count, Allocator.Invalid);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00025E3B File Offset: 0x0002403B
		public static void SetBindPoses(this Sprite sprite, NativeArray<Matrix4x4> src)
		{
			SpriteDataAccessExtensions.SetBindPoseData(sprite, src.GetUnsafeReadOnlyPtr<Matrix4x4>(), src.Length);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00025E54 File Offset: 0x00024054
		public static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo indicesInfo = SpriteDataAccessExtensions.GetIndicesInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(indicesInfo.buffer, indicesInfo.count, Allocator.Invalid);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00025E83 File Offset: 0x00024083
		public static void SetIndices(this Sprite sprite, NativeArray<ushort> src)
		{
			SpriteDataAccessExtensions.SetIndicesData(sprite, src.GetUnsafeReadOnlyPtr<ushort>(), src.Length);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00025E9C File Offset: 0x0002409C
		public static SpriteBone[] GetBones(this Sprite sprite)
		{
			return SpriteDataAccessExtensions.GetBoneInfo(sprite);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00025EB4 File Offset: 0x000240B4
		public static void SetBones(this Sprite sprite, SpriteBone[] src)
		{
			SpriteDataAccessExtensions.SetBoneData(sprite, src);
		}

		// Token: 0x06001753 RID: 5971
		[NativeName("HasChannel")]
		[MethodImpl(4096)]
		public static extern bool HasVertexAttribute([NotNull] this Sprite sprite, VertexAttribute channel);

		// Token: 0x06001754 RID: 5972
		[MethodImpl(4096)]
		public static extern void SetVertexCount([NotNull] this Sprite sprite, int count);

		// Token: 0x06001755 RID: 5973
		[MethodImpl(4096)]
		public static extern int GetVertexCount([NotNull] this Sprite sprite);

		// Token: 0x06001756 RID: 5974 RVA: 0x00025EC0 File Offset: 0x000240C0
		private static SpriteChannelInfo GetBindPoseInfo([NotNull] Sprite sprite)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetBindPoseInfo_Injected(sprite, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001757 RID: 5975
		[MethodImpl(4096)]
		private unsafe static extern void SetBindPoseData([NotNull] Sprite sprite, void* src, int count);

		// Token: 0x06001758 RID: 5976 RVA: 0x00025ED8 File Offset: 0x000240D8
		private static SpriteChannelInfo GetIndicesInfo([NotNull] Sprite sprite)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetIndicesInfo_Injected(sprite, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001759 RID: 5977
		[MethodImpl(4096)]
		private unsafe static extern void SetIndicesData([NotNull] Sprite sprite, void* src, int count);

		// Token: 0x0600175A RID: 5978 RVA: 0x00025EF0 File Offset: 0x000240F0
		private static SpriteChannelInfo GetChannelInfo([NotNull] Sprite sprite, VertexAttribute channel)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetChannelInfo_Injected(sprite, channel, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x0600175B RID: 5979
		[MethodImpl(4096)]
		private unsafe static extern void SetChannelData([NotNull] Sprite sprite, VertexAttribute channel, void* src);

		// Token: 0x0600175C RID: 5980
		[MethodImpl(4096)]
		private static extern SpriteBone[] GetBoneInfo([NotNull] Sprite sprite);

		// Token: 0x0600175D RID: 5981
		[MethodImpl(4096)]
		private static extern void SetBoneData([NotNull] Sprite sprite, SpriteBone[] src);

		// Token: 0x0600175E RID: 5982
		[MethodImpl(4096)]
		internal static extern int GetPrimaryVertexStreamSize(Sprite sprite);

		// Token: 0x0600175F RID: 5983
		[MethodImpl(4096)]
		private static extern void GetBindPoseInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		// Token: 0x06001760 RID: 5984
		[MethodImpl(4096)]
		private static extern void GetIndicesInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		// Token: 0x06001761 RID: 5985
		[MethodImpl(4096)]
		private static extern void GetChannelInfo_Injected(Sprite sprite, VertexAttribute channel, out SpriteChannelInfo ret);
	}
}
