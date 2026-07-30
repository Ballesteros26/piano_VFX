using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000155 RID: 341
	[NativeHeader("Runtime/Graphics/CustomRenderTexture.h")]
	[UsedByNativeCode]
	public sealed class CustomRenderTexture : RenderTexture
	{
		// Token: 0x06000F50 RID: 3920
		[FreeFunction(Name = "CustomRenderTextureScripting::Create")]
		[MethodImpl(4096)]
		private static extern void Internal_CreateCustomRenderTexture([Writable] CustomRenderTexture rt);

		// Token: 0x06000F51 RID: 3921
		[NativeName("TriggerUpdate")]
		[MethodImpl(4096)]
		public extern void Update(int count);

		// Token: 0x06000F52 RID: 3922 RVA: 0x0001483F File Offset: 0x00012A3F
		public void Update()
		{
			this.Update(1);
		}

		// Token: 0x06000F53 RID: 3923
		[NativeName("TriggerInitialization")]
		[MethodImpl(4096)]
		public extern void Initialize();

		// Token: 0x06000F54 RID: 3924
		[MethodImpl(4096)]
		public extern void ClearUpdateZones();

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000F55 RID: 3925
		// (set) Token: 0x06000F56 RID: 3926
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000F57 RID: 3927
		// (set) Token: 0x06000F58 RID: 3928
		public extern Material initializationMaterial
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000F59 RID: 3929
		// (set) Token: 0x06000F5A RID: 3930
		public extern Texture initializationTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000F5B RID: 3931
		[FreeFunction(Name = "CustomRenderTextureScripting::GetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern void GetUpdateZonesInternal([NotNull] object updateZones);

		// Token: 0x06000F5C RID: 3932 RVA: 0x0001484A File Offset: 0x00012A4A
		public void GetUpdateZones(List<CustomRenderTextureUpdateZone> updateZones)
		{
			this.GetUpdateZonesInternal(updateZones);
		}

		// Token: 0x06000F5D RID: 3933
		[FreeFunction(Name = "CustomRenderTextureScripting::SetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetUpdateZonesInternal(CustomRenderTextureUpdateZone[] updateZones);

		// Token: 0x06000F5E RID: 3934 RVA: 0x00014858 File Offset: 0x00012A58
		public void SetUpdateZones(CustomRenderTextureUpdateZone[] updateZones)
		{
			bool flag = updateZones == null;
			if (flag)
			{
				throw new ArgumentNullException("updateZones");
			}
			this.SetUpdateZonesInternal(updateZones);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000F5F RID: 3935
		// (set) Token: 0x06000F60 RID: 3936
		public extern CustomRenderTextureInitializationSource initializationSource
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00014884 File Offset: 0x00012A84
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0001489A File Offset: 0x00012A9A
		public Color initializationColor
		{
			get
			{
				Color color;
				this.get_initializationColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_initializationColor_Injected(ref value);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000F63 RID: 3939
		// (set) Token: 0x06000F64 RID: 3940
		public extern CustomRenderTextureUpdateMode updateMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000F65 RID: 3941
		// (set) Token: 0x06000F66 RID: 3942
		public extern CustomRenderTextureUpdateMode initializationMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000F67 RID: 3943
		// (set) Token: 0x06000F68 RID: 3944
		public extern CustomRenderTextureUpdateZoneSpace updateZoneSpace
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F69 RID: 3945
		// (set) Token: 0x06000F6A RID: 3946
		public extern int shaderPass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F6B RID: 3947
		// (set) Token: 0x06000F6C RID: 3948
		public extern uint cubemapFaceMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F6D RID: 3949
		// (set) Token: 0x06000F6E RID: 3950
		public extern bool doubleBuffered
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F6F RID: 3951
		// (set) Token: 0x06000F70 RID: 3952
		public extern bool wrapUpdateZones
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000148A4 File Offset: 0x00012AA4
		public CustomRenderTexture(int width, int height, RenderTextureFormat format, RenderTextureReadWrite readWrite)
			: this(width, height, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000148B8 File Offset: 0x00012AB8
		public CustomRenderTexture(int width, int height, RenderTextureFormat format)
			: this(width, height, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default))
		{
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000148CB File Offset: 0x00012ACB
		public CustomRenderTexture(int width, int height)
			: this(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR))
		{
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000148DD File Offset: 0x00012ADD
		public CustomRenderTexture(int width, int height, DefaultFormat defaultFormat)
			: this(width, height, SystemInfo.GetGraphicsFormat(defaultFormat))
		{
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000148F0 File Offset: 0x00012AF0
		public CustomRenderTexture(int width, int height, GraphicsFormat format)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				CustomRenderTexture.Internal_CreateCustomRenderTexture(this);
				this.width = width;
				this.height = height;
				base.graphicsFormat = format;
				base.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06000F76 RID: 3958
		[MethodImpl(4096)]
		private extern void get_initializationColor_Injected(out Color ret);

		// Token: 0x06000F77 RID: 3959
		[MethodImpl(4096)]
		private extern void set_initializationColor_Injected(ref Color value);
	}
}
