using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000358 RID: 856
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000307E8 File Offset: 0x0002E9E8
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x00030800 File Offset: 0x0002EA00
		public RenderBufferLoadAction loadAction
		{
			get
			{
				return this.m_LoadAction;
			}
			set
			{
				this.m_LoadAction = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0003080C File Offset: 0x0002EA0C
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x00030824 File Offset: 0x0002EA24
		public RenderBufferStoreAction storeAction
		{
			get
			{
				return this.m_StoreAction;
			}
			set
			{
				this.m_StoreAction = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00030830 File Offset: 0x0002EA30
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x00030848 File Offset: 0x0002EA48
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				this.m_Format = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00030854 File Offset: 0x0002EA54
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x00030871 File Offset: 0x0002EA71
		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.m_Format);
			}
			set
			{
				this.m_Format = GraphicsFormatUtility.GetGraphicsFormat(value, RenderTextureReadWrite.Default);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00030884 File Offset: 0x0002EA84
		// (set) Token: 0x06001D39 RID: 7481 RVA: 0x0003089C File Offset: 0x0002EA9C
		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000308A8 File Offset: 0x0002EAA8
		// (set) Token: 0x06001D3B RID: 7483 RVA: 0x000308C0 File Offset: 0x0002EAC0
		public RenderTargetIdentifier resolveTarget
		{
			get
			{
				return this.m_ResolveTarget;
			}
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x000308CC File Offset: 0x0002EACC
		// (set) Token: 0x06001D3D RID: 7485 RVA: 0x000308E4 File Offset: 0x0002EAE4
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000308F0 File Offset: 0x0002EAF0
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x00030908 File Offset: 0x0002EB08
		public float clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
			set
			{
				this.m_ClearDepth = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x00030914 File Offset: 0x0002EB14
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0003092C File Offset: 0x0002EB2C
		public uint clearStencil
		{
			get
			{
				return this.m_ClearStencil;
			}
			set
			{
				this.m_ClearStencil = value;
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00030938 File Offset: 0x0002EB38
		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0003099C File Offset: 0x0002EB9C
		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000309DA File Offset: 0x0002EBDA
		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0U)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000309FC File Offset: 0x0002EBFC
		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00030A68 File Offset: 0x0002EC68
		public AttachmentDescriptor(RenderTextureFormat format)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00030A68 File Offset: 0x0002EC68
		public AttachmentDescriptor(RenderTextureFormat format, RenderTargetIdentifier target, bool loadExistingContents = false, bool storeResults = false, bool resolve = false)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00030A7C File Offset: 0x0002EC7C
		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00030B18 File Offset: 0x0002ED18
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00030B50 File Offset: 0x0002ED50
		public override int GetHashCode()
		{
			int num = (int)this.m_LoadAction;
			num = (num * 397) ^ (int)this.m_StoreAction;
			num = (num * 397) ^ (int)this.m_Format;
			num = (num * 397) ^ this.m_LoadStoreTarget.GetHashCode();
			num = (num * 397) ^ this.m_ResolveTarget.GetHashCode();
			num = (num * 397) ^ this.m_ClearColor.GetHashCode();
			num = (num * 397) ^ this.m_ClearDepth.GetHashCode();
			return (num * 397) ^ (int)this.m_ClearStencil;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00030BFC File Offset: 0x0002EDFC
		public static bool operator ==(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00030C18 File Offset: 0x0002EE18
		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A2B RID: 2603
		private RenderBufferLoadAction m_LoadAction;

		// Token: 0x04000A2C RID: 2604
		private RenderBufferStoreAction m_StoreAction;

		// Token: 0x04000A2D RID: 2605
		private GraphicsFormat m_Format;

		// Token: 0x04000A2E RID: 2606
		private RenderTargetIdentifier m_LoadStoreTarget;

		// Token: 0x04000A2F RID: 2607
		private RenderTargetIdentifier m_ResolveTarget;

		// Token: 0x04000A30 RID: 2608
		private Color m_ClearColor;

		// Token: 0x04000A31 RID: 2609
		private float m_ClearDepth;

		// Token: 0x04000A32 RID: 2610
		private uint m_ClearStencil;
	}
}
