using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F9 RID: 249
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Material.h")]
	public class Material : Object
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x06000A57 RID: 2647
		[FreeFunction("MaterialScripting::CreateWithShader")]
		[MethodImpl(4096)]
		private static extern void CreateWithShader([Writable] Material self, [NotNull] Shader shader);

		// Token: 0x06000A58 RID: 2648
		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		[MethodImpl(4096)]
		private static extern void CreateWithMaterial([Writable] Material self, [NotNull] Material source);

		// Token: 0x06000A59 RID: 2649
		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(4096)]
		private static extern void CreateWithString([Writable] Material self);

		// Token: 0x06000A5A RID: 2650 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000E0FE File Offset: 0x0000C2FE
		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000E110 File Offset: 0x0000C310
		[EditorBrowsable(1)]
		[Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		// Token: 0x06000A5D RID: 2653
		[MethodImpl(4096)]
		internal static extern Material GetDefaultMaterial();

		// Token: 0x06000A5E RID: 2654
		[MethodImpl(4096)]
		internal static extern Material GetDefaultParticleMaterial();

		// Token: 0x06000A5F RID: 2655
		[MethodImpl(4096)]
		internal static extern Material GetDefaultLineMaterial();

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A60 RID: 2656
		// (set) Token: 0x06000A61 RID: 2657
		public extern Shader shader
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0000E124 File Offset: 0x0000C324
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0000E164 File Offset: 0x0000C364
		public Color color
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(firstPropertyNameIdByAttribute);
				}
				else
				{
					color = this.GetColor("_Color");
				}
				return color;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetColor(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetColor("_Color", value);
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public Texture mainTexture
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(firstPropertyNameIdByAttribute);
				}
				else
				{
					texture = this.GetTexture("_MainTex");
				}
				return texture;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTexture(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTexture("_MainTex", value);
				}
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0000E224 File Offset: 0x0000C424
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0000E264 File Offset: 0x0000C464
		public Vector2 mainTextureOffset
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureOffset(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureOffset("_MainTex");
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureOffset(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureOffset("_MainTex", value);
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public Vector2 mainTextureScale
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureScale(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureScale("_MainTex");
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureScale(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureScale("_MainTex", value);
				}
			}
		}

		// Token: 0x06000A6A RID: 2666
		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		[MethodImpl(4096)]
		private extern int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag);

		// Token: 0x06000A6B RID: 2667
		[NativeName("HasPropertyFromScript")]
		[MethodImpl(4096)]
		public extern bool HasProperty(int nameID);

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000E324 File Offset: 0x0000C524
		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A6D RID: 2669
		// (set) Token: 0x06000A6E RID: 2670
		public extern int renderQueue
		{
			[NativeName("GetActualRenderQueue")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetCustomRenderQueue")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A6F RID: 2671
		internal extern int rawRenderQueue
		{
			[NativeName("GetCustomRenderQueue")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000A70 RID: 2672
		[MethodImpl(4096)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06000A71 RID: 2673
		[MethodImpl(4096)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06000A72 RID: 2674
		[MethodImpl(4096)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A73 RID: 2675
		// (set) Token: 0x06000A74 RID: 2676
		public extern MaterialGlobalIlluminationFlags globalIlluminationFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A75 RID: 2677
		// (set) Token: 0x06000A76 RID: 2678
		public extern bool doubleSidedGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A77 RID: 2679
		// (set) Token: 0x06000A78 RID: 2680
		[NativeProperty("EnableInstancingVariants")]
		public extern bool enableInstancing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A79 RID: 2681
		public extern int passCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000A7A RID: 2682
		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetShaderPassEnabled(string passName, bool enabled);

		// Token: 0x06000A7B RID: 2683
		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool GetShaderPassEnabled(string passName);

		// Token: 0x06000A7C RID: 2684
		[MethodImpl(4096)]
		public extern string GetPassName(int pass);

		// Token: 0x06000A7D RID: 2685
		[MethodImpl(4096)]
		public extern int FindPass(string passName);

		// Token: 0x06000A7E RID: 2686
		[MethodImpl(4096)]
		public extern void SetOverrideTag(string tag, string val);

		// Token: 0x06000A7F RID: 2687
		[NativeName("GetTag")]
		[MethodImpl(4096)]
		private extern string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue);

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000E344 File Offset: 0x0000C544
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000E364 File Offset: 0x0000C564
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		// Token: 0x06000A82 RID: 2690
		[NativeThrows]
		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Lerp(Material start, Material end, float t);

		// Token: 0x06000A83 RID: 2691
		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool SetPass(int pass);

		// Token: 0x06000A84 RID: 2692
		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000A85 RID: 2693
		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern string[] GetShaderKeywords();

		// Token: 0x06000A86 RID: 2694
		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0000E388 File Offset: 0x0000C588
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06000A89 RID: 2697
		[MethodImpl(4096)]
		public extern int ComputeCRC();

		// Token: 0x06000A8A RID: 2698
		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern string[] GetTexturePropertyNames();

		// Token: 0x06000A8B RID: 2699
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int[] GetTexturePropertyNameIDs();

		// Token: 0x06000A8C RID: 2700
		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTexturePropertyNamesInternal(object outNames);

		// Token: 0x06000A8D RID: 2701
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTexturePropertyNameIDsInternal(object outNames);

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000E3AB File Offset: 0x0000C5AB
		public void GetTexturePropertyNames(List<string> outNames)
		{
			this.GetTexturePropertyNamesInternal(outNames);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000E3B6 File Offset: 0x0000C5B6
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		// Token: 0x06000A90 RID: 2704
		[NativeName("SetFloatFromScript")]
		[MethodImpl(4096)]
		private extern void SetFloatImpl(int name, float value);

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000E3C1 File Offset: 0x0000C5C1
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000A93 RID: 2707
		[NativeName("SetTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetTextureImpl(int name, Texture value);

		// Token: 0x06000A94 RID: 2708
		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		// Token: 0x06000A95 RID: 2709
		[NativeName("SetBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		// Token: 0x06000A96 RID: 2710
		[NativeName("SetGraphicsBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x06000A97 RID: 2711
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x06000A98 RID: 2712
		[NativeName("SetConstantGraphicsBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x06000A99 RID: 2713
		[NativeName("GetFloatFromScript")]
		[MethodImpl(4096)]
		private extern float GetFloatImpl(int name);

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			Color color;
			this.GetColorImpl_Injected(name, out color);
			return color;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			this.GetMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000A9C RID: 2716
		[NativeName("GetTextureFromScript")]
		[MethodImpl(4096)]
		private extern Texture GetTextureImpl(int name);

		// Token: 0x06000A9D RID: 2717
		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x06000A9E RID: 2718
		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x06000A9F RID: 2719
		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetColorArrayImpl(int name, Color[] values, int count);

		// Token: 0x06000AA0 RID: 2720
		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x06000AA1 RID: 2721
		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float[] GetFloatArrayImpl(int name);

		// Token: 0x06000AA2 RID: 2722
		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		// Token: 0x06000AA3 RID: 2723
		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Color[] GetColorArrayImpl(int name);

		// Token: 0x06000AA4 RID: 2724
		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		// Token: 0x06000AA5 RID: 2725
		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetFloatArrayCountImpl(int name);

		// Token: 0x06000AA6 RID: 2726
		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetVectorArrayCountImpl(int name);

		// Token: 0x06000AA7 RID: 2727
		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetColorArrayCountImpl(int name);

		// Token: 0x06000AA8 RID: 2728
		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetMatrixArrayCountImpl(int name);

		// Token: 0x06000AA9 RID: 2729
		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x06000AAA RID: 2730
		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x06000AAB RID: 2731
		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractColorArrayImpl(int name, [Out] Color[] val);

		// Token: 0x06000AAC RID: 2732
		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000E408 File Offset: 0x0000C608
		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			Vector4 vector;
			this.GetTextureScaleAndOffsetImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0000E41F File Offset: 0x0000C61F
		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			this.SetTextureOffsetImpl_Injected(name, ref offset);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000E42A File Offset: 0x0000C62A
		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			this.SetTextureScaleImpl_Injected(name, ref scale);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0000E438 File Offset: 0x0000C638
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000E48C File Offset: 0x0000C68C
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		private void SetColorArray(int name, Color[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetColorArrayImpl(name, values, count);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000E534 File Offset: 0x0000C734
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000E588 File Offset: 0x0000C788
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000E638 File Offset: 0x0000C838
		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int colorArrayCountImpl = this.GetColorArrayCountImpl(name);
			bool flag2 = colorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, colorArrayCountImpl);
				this.ExtractColorArrayImpl(name, (Color[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000E690 File Offset: 0x0000C890
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000E702 File Offset: 0x0000C902
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000E714 File Offset: 0x0000C914
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000E721 File Offset: 0x0000C921
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000E732 File Offset: 0x0000C932
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000E73E File Offset: 0x0000C93E
		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000E754 File Offset: 0x0000C954
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000E765 File Offset: 0x0000C965
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000E776 File Offset: 0x0000C976
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000E782 File Offset: 0x0000C982
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0000E793 File Offset: 0x0000C993
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000E79F File Offset: 0x0000C99F
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000E7BE File Offset: 0x0000C9BE
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000E7CF File Offset: 0x0000C9CF
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000E7DB File Offset: 0x0000C9DB
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0000E81B File Offset: 0x0000CA1B
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000E82F File Offset: 0x0000CA2F
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000E83E File Offset: 0x0000CA3E
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000E85A File Offset: 0x0000CA5A
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000E871 File Offset: 0x0000CA71
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000E885 File Offset: 0x0000CA85
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000E894 File Offset: 0x0000CA94
		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000E8C7 File Offset: 0x0000CAC7
		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000E8DB File Offset: 0x0000CADB
		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000E8EA File Offset: 0x0000CAEA
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000E906 File Offset: 0x0000CB06
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000E91D File Offset: 0x0000CB1D
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000E931 File Offset: 0x0000CB31
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0000E940 File Offset: 0x0000CB40
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000E95C File Offset: 0x0000CB5C
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000E973 File Offset: 0x0000CB73
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000E987 File Offset: 0x0000CB87
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000E998 File Offset: 0x0000CB98
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000EA30 File Offset: 0x0000CC30
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public Vector4 GetVector(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000EA90 File Offset: 0x0000CC90
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000EB08 File Offset: 0x0000CD08
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000EB70 File Offset: 0x0000CD70
		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000EC25 File Offset: 0x0000CE25
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000EC36 File Offset: 0x0000CE36
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000EC42 File Offset: 0x0000CE42
		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000EC53 File Offset: 0x0000CE53
		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000ECC7 File Offset: 0x0000CEC7
		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.z, textureScaleAndOffsetImpl.w);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.x, textureScaleAndOffsetImpl.y);
		}

		// Token: 0x06000B02 RID: 2818
		[MethodImpl(4096)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		// Token: 0x06000B03 RID: 2819
		[MethodImpl(4096)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x06000B04 RID: 2820
		[MethodImpl(4096)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		// Token: 0x06000B05 RID: 2821
		[MethodImpl(4096)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000B06 RID: 2822
		[MethodImpl(4096)]
		private extern void GetTextureScaleAndOffsetImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000B07 RID: 2823
		[MethodImpl(4096)]
		private extern void SetTextureOffsetImpl_Injected(int name, ref Vector2 offset);

		// Token: 0x06000B08 RID: 2824
		[MethodImpl(4096)]
		private extern void SetTextureScaleImpl_Injected(int name, ref Vector2 scale);
	}
}
