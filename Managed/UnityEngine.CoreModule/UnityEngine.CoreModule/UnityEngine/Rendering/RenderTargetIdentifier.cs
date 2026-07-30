using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000332 RID: 818
	public struct RenderTargetIdentifier : IEquatable<RenderTargetIdentifier>
	{
		// Token: 0x06001B0F RID: 6927 RVA: 0x0002C499 File Offset: 0x0002A699
		public RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0002C4D1 File Offset: 0x0002A6D1
		public RenderTargetIdentifier(string name)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0002C50F File Offset: 0x0002A70F
		public RenderTargetIdentifier(string name, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0002C54E File Offset: 0x0002A74E
		public RenderTargetIdentifier(int nameID)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0002C587 File Offset: 0x0002A787
		public RenderTargetIdentifier(int nameID, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0002C5C4 File Offset: 0x0002A7C4
		public RenderTargetIdentifier(RenderTargetIdentifier renderTargetIdentifier, int mipLevel, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = renderTargetIdentifier.m_Type;
			this.m_NameID = renderTargetIdentifier.m_NameID;
			this.m_InstanceID = renderTargetIdentifier.m_InstanceID;
			this.m_BufferPointer = renderTargetIdentifier.m_BufferPointer;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0002C618 File Offset: 0x0002A818
		public RenderTargetIdentifier(Texture tex)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0002C69C File Offset: 0x0002A89C
		public RenderTargetIdentifier(Texture tex, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0002C721 File Offset: 0x0002A921
		public RenderTargetIdentifier(RenderBuffer buf, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.BufferPtr;
			this.m_NameID = -1;
			this.m_InstanceID = buf.m_RenderTextureInstanceID;
			this.m_BufferPointer = buf.m_BufferPtr;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0002C764 File Offset: 0x0002A964
		public static implicit operator RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			return new RenderTargetIdentifier(type);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0002C77C File Offset: 0x0002A97C
		public static implicit operator RenderTargetIdentifier(string name)
		{
			return new RenderTargetIdentifier(name);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0002C794 File Offset: 0x0002A994
		public static implicit operator RenderTargetIdentifier(int nameID)
		{
			return new RenderTargetIdentifier(nameID);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0002C7AC File Offset: 0x0002A9AC
		public static implicit operator RenderTargetIdentifier(Texture tex)
		{
			return new RenderTargetIdentifier(tex);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		public static implicit operator RenderTargetIdentifier(RenderBuffer buf)
		{
			return new RenderTargetIdentifier(buf, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0002C7E0 File Offset: 0x0002A9E0
		public override string ToString()
		{
			return UnityString.Format("Type {0} NameID {1} InstanceID {2}", new object[] { this.m_Type, this.m_NameID, this.m_InstanceID });
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0002C82C File Offset: 0x0002AA2C
		public override int GetHashCode()
		{
			return (this.m_Type.GetHashCode() * 23 + this.m_NameID.GetHashCode()) * 23 + this.m_InstanceID.GetHashCode();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0002C870 File Offset: 0x0002AA70
		public bool Equals(RenderTargetIdentifier rhs)
		{
			return this.m_Type == rhs.m_Type && this.m_NameID == rhs.m_NameID && this.m_InstanceID == rhs.m_InstanceID && this.m_BufferPointer == rhs.m_BufferPointer && this.m_MipLevel == rhs.m_MipLevel && this.m_CubeFace == rhs.m_CubeFace && this.m_DepthSlice == rhs.m_DepthSlice;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is RenderTargetIdentifier);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				RenderTargetIdentifier renderTargetIdentifier = (RenderTargetIdentifier)obj;
				flag2 = this.Equals(renderTargetIdentifier);
			}
			return flag2;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0002C920 File Offset: 0x0002AB20
		public static bool operator ==(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0002C93C File Offset: 0x0002AB3C
		public static bool operator !=(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000962 RID: 2402
		private BuiltinRenderTextureType m_Type;

		// Token: 0x04000963 RID: 2403
		private int m_NameID;

		// Token: 0x04000964 RID: 2404
		private int m_InstanceID;

		// Token: 0x04000965 RID: 2405
		private IntPtr m_BufferPointer;

		// Token: 0x04000966 RID: 2406
		private int m_MipLevel;

		// Token: 0x04000967 RID: 2407
		private CubemapFace m_CubeFace;

		// Token: 0x04000968 RID: 2408
		private int m_DepthSlice;
	}
}
