using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000049 RID: 73
	public class RTHandle
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007DB6 File Offset: 0x00005FB6
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00007DBE File Offset: 0x00005FBE
		public Vector2 scaleFactor { get; internal set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007DC7 File Offset: 0x00005FC7
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007DCF File Offset: 0x00005FCF
		public bool useScaling { get; internal set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007DD8 File Offset: 0x00005FD8
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public Vector2Int referenceSize { get; internal set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007DE9 File Offset: 0x00005FE9
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_Owner.rtHandleProperties;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007DF6 File Offset: 0x00005FF6
		public RenderTexture rt
		{
			get
			{
				return this.m_RT;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007DFE File Offset: 0x00005FFE
		public RenderTargetIdentifier nameID
		{
			get
			{
				return this.m_NameID;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007E06 File Offset: 0x00006006
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007E0E File Offset: 0x0000600E
		internal RTHandle(RTHandleSystem owner)
		{
			this.m_Owner = owner;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007E1D File Offset: 0x0000601D
		public static implicit operator RenderTexture(RTHandle handle)
		{
			return handle.rt;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007E25 File Offset: 0x00006025
		public static implicit operator Texture(RTHandle handle)
		{
			if (!(handle.rt != null))
			{
				return handle.m_ExternalTexture;
			}
			return handle.rt;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007E42 File Offset: 0x00006042
		public static implicit operator RenderTargetIdentifier(RTHandle handle)
		{
			return handle.nameID;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007E4A File Offset: 0x0000604A
		internal void SetRenderTexture(RenderTexture rt)
		{
			this.m_RT = rt;
			this.m_ExternalTexture = null;
			this.m_NameID = new RenderTargetIdentifier(rt);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007E66 File Offset: 0x00006066
		internal void SetTexture(Texture tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = tex;
			this.m_NameID = new RenderTargetIdentifier(tex);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007E82 File Offset: 0x00006082
		public void Release()
		{
			this.m_Owner.Remove(this);
			CoreUtils.Destroy(this.m_RT);
			this.m_NameID = BuiltinRenderTextureType.None;
			this.m_RT = null;
			this.m_ExternalTexture = null;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007EB8 File Offset: 0x000060B8
		public Vector2Int GetScaledSize(Vector2Int refSize)
		{
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(refSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)refSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)refSize.y));
		}

		// Token: 0x04000139 RID: 313
		internal RTHandleSystem m_Owner;

		// Token: 0x0400013A RID: 314
		internal RenderTexture m_RT;

		// Token: 0x0400013B RID: 315
		internal Texture m_ExternalTexture;

		// Token: 0x0400013C RID: 316
		internal RenderTargetIdentifier m_NameID;

		// Token: 0x0400013D RID: 317
		internal bool m_EnableMSAA;

		// Token: 0x0400013E RID: 318
		internal bool m_EnableRandomWrite;

		// Token: 0x0400013F RID: 319
		internal bool m_EnableHWDynamicScale;

		// Token: 0x04000140 RID: 320
		internal string m_Name;

		// Token: 0x04000142 RID: 322
		internal ScaleFunc scaleFunc;
	}
}
