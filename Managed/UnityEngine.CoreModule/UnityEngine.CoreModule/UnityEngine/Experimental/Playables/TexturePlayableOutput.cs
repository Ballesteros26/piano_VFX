using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003CC RID: 972
	[NativeHeader("Runtime/Graphics/Director/TexturePlayableOutput.h")]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[StaticAccessor("TexturePlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/TexturePlayableOutput.bindings.h")]
	public struct TexturePlayableOutput : IPlayableOutput
	{
		// Token: 0x060021C0 RID: 8640 RVA: 0x000394C0 File Offset: 0x000376C0
		public static TexturePlayableOutput Create(PlayableGraph graph, string name, RenderTexture target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !TexturePlayableGraphExtensions.InternalCreateTextureOutput(ref graph, name, out playableOutputHandle);
			TexturePlayableOutput texturePlayableOutput;
			if (flag)
			{
				texturePlayableOutput = TexturePlayableOutput.Null;
			}
			else
			{
				TexturePlayableOutput texturePlayableOutput2 = new TexturePlayableOutput(playableOutputHandle);
				texturePlayableOutput2.SetTarget(target);
				texturePlayableOutput = texturePlayableOutput2;
			}
			return texturePlayableOutput;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00039500 File Offset: 0x00037700
		internal TexturePlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<TexturePlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TexturePlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0003953C File Offset: 0x0003773C
		public static TexturePlayableOutput Null
		{
			get
			{
				return new TexturePlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00039558 File Offset: 0x00037758
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00039570 File Offset: 0x00037770
		public static implicit operator PlayableOutput(TexturePlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00039590 File Offset: 0x00037790
		public static explicit operator TexturePlayableOutput(PlayableOutput output)
		{
			return new TexturePlayableOutput(output.GetHandle());
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000395B0 File Offset: 0x000377B0
		public RenderTexture GetTarget()
		{
			return TexturePlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000395CD File Offset: 0x000377CD
		public void SetTarget(RenderTexture value)
		{
			TexturePlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060021C8 RID: 8648
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern RenderTexture InternalGetTarget(ref PlayableOutputHandle output);

		// Token: 0x060021C9 RID: 8649
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, RenderTexture target);

		// Token: 0x04000C46 RID: 3142
		private PlayableOutputHandle m_Handle;
	}
}
