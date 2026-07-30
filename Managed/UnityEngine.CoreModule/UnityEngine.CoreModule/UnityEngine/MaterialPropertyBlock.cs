using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000F5 RID: 245
	[NativeHeader("Runtime/Math/SphericalHarmonicsL2.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Shaders/ShaderPropertySheet.h")]
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x0000C83B File Offset: 0x0000AA3B
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000C84C File Offset: 0x0000AA4C
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(int nameID, float value)
		{
			this.SetFloat(nameID, value);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000C858 File Offset: 0x0000AA58
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000C869 File Offset: 0x0000AA69
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(int nameID, Vector4 value)
		{
			this.SetVector(nameID, value);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000C875 File Offset: 0x0000AA75
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000C886 File Offset: 0x0000AA86
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(int nameID, Color value)
		{
			this.SetColor(nameID, value);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000C892 File Offset: 0x0000AA92
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000C8A3 File Offset: 0x0000AAA3
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrix(nameID, value);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000C8AF File Offset: 0x0000AAAF
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(int nameID, Texture value)
		{
			this.SetTexture(nameID, value);
		}

		// Token: 0x060008AB RID: 2219
		[NativeName("GetFloatFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern float GetFloatImpl(int name);

		// Token: 0x060008AC RID: 2220 RVA: 0x0000C8CC File Offset: 0x0000AACC
		[ThreadSafe]
		[NativeName("GetVectorFromScript")]
		private Vector4 GetVectorImpl(int name)
		{
			Vector4 vector;
			this.GetVectorImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		[NativeName("GetColorFromScript")]
		[ThreadSafe]
		private Color GetColorImpl(int name)
		{
			Color color;
			this.GetColorImpl_Injected(name, out color);
			return color;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		[NativeName("GetMatrixFromScript")]
		[ThreadSafe]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			this.GetMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060008AF RID: 2223
		[NativeName("GetTextureFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern Texture GetTextureImpl(int name);

		// Token: 0x060008B0 RID: 2224
		[ThreadSafe]
		[NativeName("SetFloatFromScript")]
		[MethodImpl(4096)]
		private extern void SetFloatImpl(int name, float value);

		// Token: 0x060008B1 RID: 2225 RVA: 0x0000C913 File Offset: 0x0000AB13
		[NativeName("SetVectorFromScript")]
		[ThreadSafe]
		private void SetVectorImpl(int name, Vector4 value)
		{
			this.SetVectorImpl_Injected(name, ref value);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000C91E File Offset: 0x0000AB1E
		[ThreadSafe]
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0000C929 File Offset: 0x0000AB29
		[NativeName("SetMatrixFromScript")]
		[ThreadSafe]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x060008B4 RID: 2228
		[ThreadSafe]
		[NativeName("SetTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetTextureImpl(int name, [NotNull] Texture value);

		// Token: 0x060008B5 RID: 2229
		[ThreadSafe]
		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetRenderTextureImpl(int name, [NotNull] RenderTexture value, RenderTextureSubElement element);

		// Token: 0x060008B6 RID: 2230
		[NativeName("SetBufferFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		// Token: 0x060008B7 RID: 2231
		[ThreadSafe]
		[NativeName("SetGraphicsBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x060008B8 RID: 2232
		[ThreadSafe]
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x060008B9 RID: 2233
		[NativeName("SetConstantGraphicsBufferFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x060008BA RID: 2234
		[ThreadSafe]
		[NativeName("SetFloatArrayFromScript")]
		[MethodImpl(4096)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x060008BB RID: 2235
		[ThreadSafe]
		[NativeName("SetVectorArrayFromScript")]
		[MethodImpl(4096)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x060008BC RID: 2236
		[ThreadSafe]
		[NativeName("SetMatrixArrayFromScript")]
		[MethodImpl(4096)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x060008BD RID: 2237
		[ThreadSafe]
		[NativeName("GetFloatArrayFromScript")]
		[MethodImpl(4096)]
		private extern float[] GetFloatArrayImpl(int name);

		// Token: 0x060008BE RID: 2238
		[ThreadSafe]
		[NativeName("GetVectorArrayFromScript")]
		[MethodImpl(4096)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		// Token: 0x060008BF RID: 2239
		[NativeName("GetMatrixArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		// Token: 0x060008C0 RID: 2240
		[ThreadSafe]
		[NativeName("GetFloatArrayCountFromScript")]
		[MethodImpl(4096)]
		private extern int GetFloatArrayCountImpl(int name);

		// Token: 0x060008C1 RID: 2241
		[ThreadSafe]
		[NativeName("GetVectorArrayCountFromScript")]
		[MethodImpl(4096)]
		private extern int GetVectorArrayCountImpl(int name);

		// Token: 0x060008C2 RID: 2242
		[NativeName("GetMatrixArrayCountFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern int GetMatrixArrayCountImpl(int name);

		// Token: 0x060008C3 RID: 2243
		[NativeName("ExtractFloatArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x060008C4 RID: 2244
		[ThreadSafe]
		[NativeName("ExtractVectorArrayFromScript")]
		[MethodImpl(4096)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x060008C5 RID: 2245
		[ThreadSafe]
		[NativeName("ExtractMatrixArrayFromScript")]
		[MethodImpl(4096)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x060008C6 RID: 2246
		[ThreadSafe]
		[FreeFunction("ConvertAndCopySHCoefficientArraysToPropertySheetFromScript")]
		[MethodImpl(4096)]
		internal static extern void Internal_CopySHCoefficientArraysFrom(MaterialPropertyBlock properties, SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count);

		// Token: 0x060008C7 RID: 2247
		[FreeFunction("CopyProbeOcclusionArrayToPropertySheetFromScript")]
		[ThreadSafe]
		[MethodImpl(4096)]
		internal static extern void Internal_CopyProbeOcclusionArrayFrom(MaterialPropertyBlock properties, Vector4[] occlusionProbes, int sourceStart, int destStart, int count);

		// Token: 0x060008C8 RID: 2248
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Create", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr CreateImpl();

		// Token: 0x060008C9 RID: 2249
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Destroy", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void DestroyImpl(IntPtr mpb);

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008CA RID: 2250
		public extern bool isEmpty
		{
			[NativeName("IsEmpty")]
			[ThreadSafe]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060008CB RID: 2251
		[ThreadSafe]
		[MethodImpl(4096)]
		private extern void Clear(bool keepMemory);

		// Token: 0x060008CC RID: 2252 RVA: 0x0000C934 File Offset: 0x0000AB34
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000C940 File Offset: 0x0000AB40
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

		// Token: 0x060008CE RID: 2254 RVA: 0x0000C994 File Offset: 0x0000AB94
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

		// Token: 0x060008CF RID: 2255 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
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

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000CA3C File Offset: 0x0000AC3C
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000CA94 File Offset: 0x0000AC94
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

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000CAEC File Offset: 0x0000ACEC
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

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000CB41 File Offset: 0x0000AD41
		public MaterialPropertyBlock()
		{
			this.m_Ptr = MaterialPropertyBlock.CreateImpl();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0000CB58 File Offset: 0x0000AD58
		~MaterialPropertyBlock()
		{
			this.Dispose();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0000CB88 File Offset: 0x0000AD88
		private void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MaterialPropertyBlock.DestroyImpl(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000CBCA File Offset: 0x0000ADCA
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000CBDB File Offset: 0x0000ADDB
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000CBE7 File Offset: 0x0000ADE7
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000CBF9 File Offset: 0x0000ADF9
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000CC06 File Offset: 0x0000AE06
		public void SetVector(string name, Vector4 value)
		{
			this.SetVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000CC17 File Offset: 0x0000AE17
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetVectorImpl(nameID, value);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000CC23 File Offset: 0x0000AE23
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000CC51 File Offset: 0x0000AE51
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000CC5D File Offset: 0x0000AE5D
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000CC7A File Offset: 0x0000AE7A
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000CC8B File Offset: 0x0000AE8B
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0000CC97 File Offset: 0x0000AE97
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0000CD0A File Offset: 0x0000AF0A
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0000CD19 File Offset: 0x0000AF19
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0000CD35 File Offset: 0x0000AF35
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000CD8B File Offset: 0x0000AF8B
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000CDB6 File Offset: 0x0000AFB6
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000CE58 File Offset: 0x0000B058
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000CE78 File Offset: 0x0000B078
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000CE94 File Offset: 0x0000B094
		public Vector4 GetVector(string name)
		{
			return this.GetVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		public Vector4 GetVector(int nameID)
		{
			return this.GetVectorImpl(nameID);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000CF48 File Offset: 0x0000B148
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000CF68 File Offset: 0x0000B168
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000CF84 File Offset: 0x0000B184
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000D014 File Offset: 0x0000B214
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0000D034 File Offset: 0x0000B234
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000D059 File Offset: 0x0000B259
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0000D06A File Offset: 0x0000B26A
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000D076 File Offset: 0x0000B276
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000D087 File Offset: 0x0000B287
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000D093 File Offset: 0x0000B293
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), 0, 0, lightProbes.Count);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(lightProbes, 0, 0, lightProbes.Length);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000D116 File Offset: 0x0000B316
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes, int sourceStart, int destStart, int count)
		{
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000D12C File Offset: 0x0000B32C
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = lightProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopySHCoefficientArraysFrom(this, lightProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), 0, 0, occlusionProbes.Count);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(occlusionProbes, 0, 0, occlusionProbes.Length);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000D222 File Offset: 0x0000B422
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes, int sourceStart, int destStart, int count)
		{
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000D238 File Offset: 0x0000B438
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes, int sourceStart, int destStart, int count)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = occlusionProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopyProbeOcclusionArrayFrom(this, occlusionProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000918 RID: 2328
		[MethodImpl(4096)]
		private extern void GetVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000919 RID: 2329
		[MethodImpl(4096)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		// Token: 0x0600091A RID: 2330
		[MethodImpl(4096)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x0600091B RID: 2331
		[MethodImpl(4096)]
		private extern void SetVectorImpl_Injected(int name, ref Vector4 value);

		// Token: 0x0600091C RID: 2332
		[MethodImpl(4096)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		// Token: 0x0600091D RID: 2333
		[MethodImpl(4096)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x040002A5 RID: 677
		internal IntPtr m_Ptr;
	}
}
