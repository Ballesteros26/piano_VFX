using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000156 RID: 342
	public struct RenderTextureDescriptor
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00014942 File Offset: 0x00012B42
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x0001494A File Offset: 0x00012B4A
		public int width { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00014953 File Offset: 0x00012B53
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x0001495B File Offset: 0x00012B5B
		public int height { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00014964 File Offset: 0x00012B64
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0001496C File Offset: 0x00012B6C
		public int msaaSamples { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00014975 File Offset: 0x00012B75
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0001497D File Offset: 0x00012B7D
		public int volumeDepth { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00014986 File Offset: 0x00012B86
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0001498E File Offset: 0x00012B8E
		public int mipCount { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00014998 File Offset: 0x00012B98
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x000149B0 File Offset: 0x00012BB0
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000149C8 File Offset: 0x00012BC8
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x000149D0 File Offset: 0x00012BD0
		public GraphicsFormat stencilFormat { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000149DC File Offset: 0x00012BDC
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x000149F9 File Offset: 0x00012BF9
		public RenderTextureFormat colorFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB), FormatUsage.Render);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00014A18 File Offset: 0x00012C18
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00014A35 File Offset: 0x00012C35
		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(this.colorFormat, value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00014A4C File Offset: 0x00012C4C
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00014A6C File Offset: 0x00012C6C
		public int depthBufferBits
		{
			get
			{
				return RenderTextureDescriptor.depthFormatBits[this._depthBufferBits];
			}
			set
			{
				bool flag = value <= 0;
				if (flag)
				{
					this._depthBufferBits = 0;
				}
				else
				{
					bool flag2 = value <= 16;
					if (flag2)
					{
						this._depthBufferBits = 1;
					}
					else
					{
						this._depthBufferBits = 2;
					}
				}
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00014AAA File Offset: 0x00012CAA
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00014AB2 File Offset: 0x00012CB2
		public TextureDimension dimension { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00014ABB File Offset: 0x00012CBB
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00014AC3 File Offset: 0x00012CC3
		public ShadowSamplingMode shadowSamplingMode { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00014ACC File Offset: 0x00012CCC
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public VRTextureUsage vrUsage { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00014AE0 File Offset: 0x00012CE0
		public RenderTextureCreationFlags flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00014AF8 File Offset: 0x00012CF8
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00014B00 File Offset: 0x00012D00
		public RenderTextureMemoryless memoryless { get; set; }

		// Token: 0x06000F95 RID: 3989 RVA: 0x00014B09 File Offset: 0x00012D09
		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR), 0, Texture.GenerateAllMips);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00014B21 File Offset: 0x00012D21
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00014B2F File Offset: 0x00012D2F
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(colorFormat, false), FormatUsage.Render), depthBufferBits);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00014B4A File Offset: 0x00012D4A
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00014B5E File Offset: 0x00012D5E
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetCompatibleFormat(GraphicsFormatUtility.GetGraphicsFormat(colorFormat, false), FormatUsage.Render), depthBufferBits, mipCount);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00014B7C File Offset: 0x00012D7C
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthBufferBits = depthBufferBits;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00014BF8 File Offset: 0x00012DF8
		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00014C30 File Offset: 0x00012E30
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00014C4D File Offset: 0x00012E4D
		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00014C5C File Offset: 0x00012E5C
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00014C79 File Offset: 0x00012E79
		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00014C88 File Offset: 0x00012E88
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00014CA6 File Offset: 0x00012EA6
		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00014CB4 File Offset: 0x00012EB4
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00014CD5 File Offset: 0x00012ED5
		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00014CE8 File Offset: 0x00012EE8
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00014D06 File Offset: 0x00012F06
		internal bool createdFromScript
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.CreatedFromScript) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00014D14 File Offset: 0x00012F14
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00014D35 File Offset: 0x00012F35
		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		// Token: 0x0400043E RID: 1086
		private GraphicsFormat _graphicsFormat;

		// Token: 0x04000440 RID: 1088
		private int _depthBufferBits;

		// Token: 0x04000441 RID: 1089
		private static int[] depthFormatBits = new int[]
		{
			default(int),
			16,
			24
		};

		// Token: 0x04000445 RID: 1093
		private RenderTextureCreationFlags _flags;
	}
}
