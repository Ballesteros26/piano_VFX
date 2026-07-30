using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h")]
	public class Texture : Object
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		protected Texture()
		{
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000DA5 RID: 3493
		// (set) Token: 0x06000DA6 RID: 3494
		public static extern int masterTextureLimit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000DA7 RID: 3495
		public extern int mipmapCount
		{
			[NativeName("GetMipmapCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000DA8 RID: 3496
		// (set) Token: 0x06000DA9 RID: 3497
		[NativeProperty("AnisoLimit")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000DAA RID: 3498
		[NativeName("SetGlobalAnisoLimits")]
		[MethodImpl(4096)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000125B4 File Offset: 0x000107B4
		public virtual GraphicsFormat graphicsFormat
		{
			get
			{
				return GraphicsFormatUtility.GetFormat(this);
			}
		}

		// Token: 0x06000DAC RID: 3500
		[MethodImpl(4096)]
		private extern int GetDataWidth();

		// Token: 0x06000DAD RID: 3501
		[MethodImpl(4096)]
		private extern int GetDataHeight();

		// Token: 0x06000DAE RID: 3502
		[MethodImpl(4096)]
		private extern TextureDimension GetDimension();

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000125CC File Offset: 0x000107CC
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000125E4 File Offset: 0x000107E4
		public virtual int width
		{
			get
			{
				return this.GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000125EC File Offset: 0x000107EC
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x000125E4 File Offset: 0x000107E4
		public virtual int height
		{
			get
			{
				return this.GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00012604 File Offset: 0x00010804
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000125E4 File Offset: 0x000107E4
		public virtual TextureDimension dimension
		{
			get
			{
				return this.GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000DB5 RID: 3509
		public virtual extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000DB6 RID: 3510
		// (set) Token: 0x06000DB7 RID: 3511
		public extern TextureWrapMode wrapMode
		{
			[NativeName("GetWrapModeU")]
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000DB8 RID: 3512
		// (set) Token: 0x06000DB9 RID: 3513
		public extern TextureWrapMode wrapModeU
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000DBA RID: 3514
		// (set) Token: 0x06000DBB RID: 3515
		public extern TextureWrapMode wrapModeV
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000DBC RID: 3516
		// (set) Token: 0x06000DBD RID: 3517
		public extern TextureWrapMode wrapModeW
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000DBE RID: 3518
		// (set) Token: 0x06000DBF RID: 3519
		public extern FilterMode filterMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000DC0 RID: 3520
		// (set) Token: 0x06000DC1 RID: 3521
		public extern int anisoLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000DC2 RID: 3522
		// (set) Token: 0x06000DC3 RID: 3523
		public extern float mipMapBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0001261C File Offset: 0x0001081C
		public Vector2 texelSize
		{
			[NativeName("GetNpotTexelSize")]
			get
			{
				Vector2 vector;
				this.get_texelSize_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x06000DC5 RID: 3525
		[MethodImpl(4096)]
		public extern IntPtr GetNativeTexturePtr();

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00012634 File Offset: 0x00010834
		[Obsolete("Use GetNativeTexturePtr instead.", false)]
		public int GetNativeTextureID()
		{
			return (int)this.GetNativeTexturePtr();
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000DC7 RID: 3527
		public extern uint updateCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000DC8 RID: 3528
		[MethodImpl(4096)]
		public extern void IncrementUpdateCount();

		// Token: 0x06000DC9 RID: 3529
		[NativeMethod("GetActiveTextureColorSpace")]
		[MethodImpl(4096)]
		private extern int Internal_GetActiveTextureColorSpace();

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00012654 File Offset: 0x00010854
		internal ColorSpace activeTextureColorSpace
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
			get
			{
				return (this.Internal_GetActiveTextureColorSpace() == 0) ? ColorSpace.Linear : ColorSpace.Gamma;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000DCB RID: 3531
		public static extern ulong totalTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTotalTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000DCC RID: 3532
		public static extern ulong desiredTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetDesiredTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000DCD RID: 3533
		public static extern ulong targetTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTargetTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000DCE RID: 3534
		public static extern ulong currentTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetCurrentTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000DCF RID: 3535
		public static extern ulong nonStreamingTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000DD0 RID: 3536
		public static extern ulong streamingMipmapUploadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingMipmapUploadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000DD1 RID: 3537
		public static extern ulong streamingRendererCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingRendererCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000DD2 RID: 3538
		public static extern ulong streamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000DD3 RID: 3539
		public static extern ulong nonStreamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000DD4 RID: 3540
		public static extern ulong streamingTexturePendingLoadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTexturePendingLoadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000DD5 RID: 3541
		public static extern ulong streamingTextureLoadingCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureLoadingCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000DD6 RID: 3542
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugProperties")]
		[MethodImpl(4096)]
		public static extern void SetStreamingTextureMaterialDebugProperties();

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000DD7 RID: 3543
		// (set) Token: 0x06000DD8 RID: 3544
		public static extern bool streamingTextureForceLoadAll
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetForceLoadAll")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetForceLoadAll")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DD9 RID: 3545
		// (set) Token: 0x06000DDA RID: 3546
		public static extern bool streamingTextureDiscardUnusedMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DDB RID: 3547
		// (set) Token: 0x06000DDC RID: 3548
		public static extern bool allowThreadedTextureCreation
		{
			[FreeFunction(Name = "Texture2DScripting::IsCreateTextureThreadedEnabled")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "Texture2DScripting::EnableCreateTextureThreaded")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000DDD RID: 3549
		[MethodImpl(4096)]
		internal extern int GetPixelDataSize(int mipLevel, int element = 0);

		// Token: 0x06000DDE RID: 3550
		[MethodImpl(4096)]
		internal extern int GetPixelDataOffset(int mipLevel, int element = 0);

		// Token: 0x06000DDF RID: 3551 RVA: 0x00012674 File Offset: 0x00010874
		internal bool ValidateFormat(RenderTextureFormat format)
		{
			bool flag = SystemInfo.SupportsRenderTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("RenderTexture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsRenderTextureFormat' C# API to check format support.", format.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000126B8 File Offset: 0x000108B8
		internal bool ValidateFormat(TextureFormat format)
		{
			bool flag = SystemInfo.SupportsTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = GraphicsFormatUtility.IsCompressedTextureFormat(format);
				if (flag3)
				{
					Debug.LogWarning(string.Format("'{0}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00012728 File Offset: 0x00010928
		internal bool ValidateFormat(GraphicsFormat format, FormatUsage usage)
		{
			bool flag = SystemInfo.IsFormatSupported(format, usage);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported for {1} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", format.ToString(), usage.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00012778 File Offset: 0x00010978
		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException(string.Format("Texture '{0}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x06000DE4 RID: 3556
		[MethodImpl(4096)]
		private extern void get_texelSize_Injected(out Vector2 ret);

		// Token: 0x0400042D RID: 1069
		public static readonly int GenerateAllMips = -1;
	}
}
