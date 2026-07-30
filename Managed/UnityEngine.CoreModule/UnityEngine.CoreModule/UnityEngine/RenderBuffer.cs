using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000DC RID: 220
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0000A45D File Offset: 0x0000865D
		[FreeFunction(Name = "RenderBufferScripting::SetLoadAction", HasExplicitThis = true)]
		internal void SetLoadAction(RenderBufferLoadAction action)
		{
			RenderBuffer.SetLoadAction_Injected(ref this, action);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000A466 File Offset: 0x00008666
		[FreeFunction(Name = "RenderBufferScripting::SetStoreAction", HasExplicitThis = true)]
		internal void SetStoreAction(RenderBufferStoreAction action)
		{
			RenderBuffer.SetStoreAction_Injected(ref this, action);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000A46F File Offset: 0x0000866F
		[FreeFunction(Name = "RenderBufferScripting::GetLoadAction", HasExplicitThis = true)]
		internal RenderBufferLoadAction GetLoadAction()
		{
			return RenderBuffer.GetLoadAction_Injected(ref this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000A477 File Offset: 0x00008677
		[FreeFunction(Name = "RenderBufferScripting::GetStoreAction", HasExplicitThis = true)]
		internal RenderBufferStoreAction GetStoreAction()
		{
			return RenderBuffer.GetStoreAction_Injected(ref this);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000A47F File Offset: 0x0000867F
		[FreeFunction(Name = "RenderBufferScripting::GetNativeRenderBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeRenderBufferPtr()
		{
			return RenderBuffer.GetNativeRenderBufferPtr_Injected(ref this);
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0000A488 File Offset: 0x00008688
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000A4A0 File Offset: 0x000086A0
		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return this.GetLoadAction();
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000A4AC File Offset: 0x000086AC
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000A4C4 File Offset: 0x000086C4
		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return this.GetStoreAction();
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		// Token: 0x0600067A RID: 1658
		[MethodImpl(4096)]
		private static extern void SetLoadAction_Injected(ref RenderBuffer _unity_self, RenderBufferLoadAction action);

		// Token: 0x0600067B RID: 1659
		[MethodImpl(4096)]
		private static extern void SetStoreAction_Injected(ref RenderBuffer _unity_self, RenderBufferStoreAction action);

		// Token: 0x0600067C RID: 1660
		[MethodImpl(4096)]
		private static extern RenderBufferLoadAction GetLoadAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x0600067D RID: 1661
		[MethodImpl(4096)]
		private static extern RenderBufferStoreAction GetStoreAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x0600067E RID: 1662
		[MethodImpl(4096)]
		private static extern IntPtr GetNativeRenderBufferPtr_Injected(ref RenderBuffer _unity_self);

		// Token: 0x04000266 RID: 614
		internal int m_RenderTextureInstanceID;

		// Token: 0x04000267 RID: 615
		internal IntPtr m_BufferPtr;
	}
}
