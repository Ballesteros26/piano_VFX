using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000F8 RID: 248
	[NativeHeader("Runtime/Shaders/GpuPrograms/ShaderVariantCollection.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManager.h")]
	[NativeHeader("Runtime/Shaders/ShaderNameRegistry.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	public sealed class Shader : Object
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0000D69C File Offset: 0x0000B89C
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x0000D6B3 File Offset: 0x0000B8B3
		[Obsolete("Use Graphics.activeTier instead (UnityUpgradable) -> UnityEngine.Graphics.activeTier", false)]
		public static ShaderHardwareTier globalShaderHardwareTier
		{
			get
			{
				return (ShaderHardwareTier)Graphics.activeTier;
			}
			set
			{
				Graphics.activeTier = (GraphicsTier)value;
			}
		}

		// Token: 0x060009C8 RID: 2504
		[FreeFunction("GetScriptMapper().FindShader")]
		[MethodImpl(4096)]
		public static extern Shader Find(string name);

		// Token: 0x060009C9 RID: 2505
		[FreeFunction("GetBuiltinResource<Shader>")]
		[MethodImpl(4096)]
		internal static extern Shader FindBuiltin(string name);

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009CA RID: 2506
		// (set) Token: 0x060009CB RID: 2507
		[NativeProperty("MaximumShaderLOD")]
		public extern int maximumLOD
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009CC RID: 2508
		// (set) Token: 0x060009CD RID: 2509
		[NativeProperty("GlobalMaximumShaderLOD")]
		public static extern int globalMaximumLOD
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009CE RID: 2510
		public extern bool isSupported
		{
			[NativeMethod("IsSupported")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009CF RID: 2511
		// (set) Token: 0x060009D0 RID: 2512
		public static extern string globalRenderPipeline
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060009D1 RID: 2513
		[FreeFunction("ShaderScripting::EnableKeyword")]
		[MethodImpl(4096)]
		public static extern void EnableKeyword(string keyword);

		// Token: 0x060009D2 RID: 2514
		[FreeFunction("ShaderScripting::DisableKeyword")]
		[MethodImpl(4096)]
		public static extern void DisableKeyword(string keyword);

		// Token: 0x060009D3 RID: 2515
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		[MethodImpl(4096)]
		public static extern bool IsKeywordEnabled(string keyword);

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009D4 RID: 2516
		public extern int renderQueue
		{
			[FreeFunction("ShaderScripting::GetRenderQueue", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009D5 RID: 2517
		internal extern DisableBatchingType disableBatching
		{
			[FreeFunction("ShaderScripting::GetDisableBatchingType", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060009D6 RID: 2518
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern void WarmupAllShaders();

		// Token: 0x060009D7 RID: 2519
		[FreeFunction("ShaderScripting::TagToID")]
		[MethodImpl(4096)]
		internal static extern int TagToID(string name);

		// Token: 0x060009D8 RID: 2520
		[FreeFunction("ShaderScripting::IDToTag")]
		[MethodImpl(4096)]
		internal static extern string IDToTag(int name);

		// Token: 0x060009D9 RID: 2521
		[FreeFunction(Name = "ShaderScripting::PropertyToID", IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int PropertyToID(string name);

		// Token: 0x060009DA RID: 2522
		[MethodImpl(4096)]
		public extern Shader GetDependency(string name);

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009DB RID: 2523
		public extern int passCount
		{
			[FreeFunction(Name = "ShaderScripting::GetPassCount", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public ShaderTagId FindPassTagValue(int passIndex, ShaderTagId tagName)
		{
			bool flag = passIndex < 0 || passIndex >= this.passCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int num = this.Internal_FindPassTagValue(passIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x060009DD RID: 2525
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int Internal_FindPassTagValue(int passIndex, int tagName);

		// Token: 0x060009DE RID: 2526
		[FreeFunction("ShaderScripting::SetGlobalFloat")]
		[MethodImpl(4096)]
		private static extern void SetGlobalFloatImpl(int name, float value);

		// Token: 0x060009DF RID: 2527 RVA: 0x0000D715 File Offset: 0x0000B915
		[FreeFunction("ShaderScripting::SetGlobalVector")]
		private static void SetGlobalVectorImpl(int name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl_Injected(name, ref value);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000D71F File Offset: 0x0000B91F
		[FreeFunction("ShaderScripting::SetGlobalMatrix")]
		private static void SetGlobalMatrixImpl(int name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x060009E1 RID: 2529
		[FreeFunction("ShaderScripting::SetGlobalTexture")]
		[MethodImpl(4096)]
		private static extern void SetGlobalTextureImpl(int name, Texture value);

		// Token: 0x060009E2 RID: 2530
		[FreeFunction("ShaderScripting::SetGlobalRenderTexture")]
		[MethodImpl(4096)]
		private static extern void SetGlobalRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		// Token: 0x060009E3 RID: 2531
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalBufferImpl(int name, ComputeBuffer value);

		// Token: 0x060009E4 RID: 2532
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x060009E5 RID: 2533
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x060009E6 RID: 2534
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		[MethodImpl(4096)]
		private static extern void SetGlobalConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x060009E7 RID: 2535
		[FreeFunction("ShaderScripting::GetGlobalFloat")]
		[MethodImpl(4096)]
		private static extern float GetGlobalFloatImpl(int name);

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000D72C File Offset: 0x0000B92C
		[FreeFunction("ShaderScripting::GetGlobalVector")]
		private static Vector4 GetGlobalVectorImpl(int name)
		{
			Vector4 vector;
			Shader.GetGlobalVectorImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000D744 File Offset: 0x0000B944
		[FreeFunction("ShaderScripting::GetGlobalMatrix")]
		private static Matrix4x4 GetGlobalMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			Shader.GetGlobalMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x060009EA RID: 2538
		[FreeFunction("ShaderScripting::GetGlobalTexture")]
		[MethodImpl(4096)]
		private static extern Texture GetGlobalTextureImpl(int name);

		// Token: 0x060009EB RID: 2539
		[FreeFunction("ShaderScripting::SetGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x060009EC RID: 2540
		[FreeFunction("ShaderScripting::SetGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x060009ED RID: 2541
		[FreeFunction("ShaderScripting::SetGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern void SetGlobalMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x060009EE RID: 2542
		[FreeFunction("ShaderScripting::GetGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern float[] GetGlobalFloatArrayImpl(int name);

		// Token: 0x060009EF RID: 2543
		[FreeFunction("ShaderScripting::GetGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern Vector4[] GetGlobalVectorArrayImpl(int name);

		// Token: 0x060009F0 RID: 2544
		[FreeFunction("ShaderScripting::GetGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern Matrix4x4[] GetGlobalMatrixArrayImpl(int name);

		// Token: 0x060009F1 RID: 2545
		[FreeFunction("ShaderScripting::GetGlobalFloatArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalFloatArrayCountImpl(int name);

		// Token: 0x060009F2 RID: 2546
		[FreeFunction("ShaderScripting::GetGlobalVectorArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalVectorArrayCountImpl(int name);

		// Token: 0x060009F3 RID: 2547
		[FreeFunction("ShaderScripting::GetGlobalMatrixArrayCount")]
		[MethodImpl(4096)]
		private static extern int GetGlobalMatrixArrayCountImpl(int name);

		// Token: 0x060009F4 RID: 2548
		[FreeFunction("ShaderScripting::ExtractGlobalFloatArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x060009F5 RID: 2549
		[FreeFunction("ShaderScripting::ExtractGlobalVectorArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x060009F6 RID: 2550
		[FreeFunction("ShaderScripting::ExtractGlobalMatrixArray")]
		[MethodImpl(4096)]
		private static extern void ExtractGlobalMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000D75C File Offset: 0x0000B95C
		private static void SetGlobalFloatArray(int name, float[] values, int count)
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
			Shader.SetGlobalFloatArrayImpl(name, values, count);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		private static void SetGlobalVectorArray(int name, Vector4[] values, int count)
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
			Shader.SetGlobalVectorArrayImpl(name, values, count);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000D804 File Offset: 0x0000BA04
		private static void SetGlobalMatrixArray(int name, Matrix4x4[] values, int count)
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
			Shader.SetGlobalMatrixArrayImpl(name, values, count);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000D858 File Offset: 0x0000BA58
		private static void ExtractGlobalFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalFloatArrayCountImpl = Shader.GetGlobalFloatArrayCountImpl(name);
			bool flag2 = globalFloatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, globalFloatArrayCountImpl);
				Shader.ExtractGlobalFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		private static void ExtractGlobalVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalVectorArrayCountImpl = Shader.GetGlobalVectorArrayCountImpl(name);
			bool flag2 = globalVectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, globalVectorArrayCountImpl);
				Shader.ExtractGlobalVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000D900 File Offset: 0x0000BB00
		private static void ExtractGlobalMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalMatrixArrayCountImpl = Shader.GetGlobalMatrixArrayCountImpl(name);
			bool flag2 = globalMatrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, globalMatrixArrayCountImpl);
				Shader.ExtractGlobalMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000D953 File Offset: 0x0000BB53
		public static void SetGlobalFloat(string name, float value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000D963 File Offset: 0x0000BB63
		public static void SetGlobalFloat(int nameID, float value)
		{
			Shader.SetGlobalFloatImpl(nameID, value);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public static void SetGlobalInt(string name, int value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000D97F File Offset: 0x0000BB7F
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000D98B File Offset: 0x0000BB8B
		public static void SetGlobalVector(string name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000D99B File Offset: 0x0000BB9B
		public static void SetGlobalVector(int nameID, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		public static void SetGlobalColor(string name, Color value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000D9BB File Offset: 0x0000BBBB
		public static void SetGlobalColor(int nameID, Color value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000D9CB File Offset: 0x0000BBCB
		public static void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000D9DB File Offset: 0x0000BBDB
		public static void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(nameID, value);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000D9E6 File Offset: 0x0000BBE6
		public static void SetGlobalTexture(string name, Texture value)
		{
			Shader.SetGlobalTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		public static void SetGlobalTexture(int nameID, Texture value)
		{
			Shader.SetGlobalTextureImpl(nameID, value);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000DA01 File Offset: 0x0000BC01
		public static void SetGlobalTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000DA12 File Offset: 0x0000BC12
		public static void SetGlobalTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		public static void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000DA2E File Offset: 0x0000BC2E
		public static void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(nameID, value);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000DA39 File Offset: 0x0000BC39
		public static void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000DA49 File Offset: 0x0000BC49
		public static void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public static void SetGlobalConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000DA66 File Offset: 0x0000BC66
		public static void SetGlobalConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000DA73 File Offset: 0x0000BC73
		public static void SetGlobalConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000DA85 File Offset: 0x0000BC85
		public static void SetGlobalConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000DA92 File Offset: 0x0000BC92
		public static void SetGlobalFloatArray(string name, List<float> values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000DAAD File Offset: 0x0000BCAD
		public static void SetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.SetGlobalFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public static void SetGlobalFloatArray(string name, float[] values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		public static void SetGlobalFloatArray(int nameID, float[] values)
		{
			Shader.SetGlobalFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public static void SetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000DAFF File Offset: 0x0000BCFF
		public static void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000DB15 File Offset: 0x0000BD15
		public static void SetGlobalVectorArray(string name, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public static void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000DB36 File Offset: 0x0000BD36
		public static void SetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000DB51 File Offset: 0x0000BD51
		public static void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000DB67 File Offset: 0x0000BD67
		public static void SetGlobalMatrixArray(string name, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000DB7A File Offset: 0x0000BD7A
		public static void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000DB88 File Offset: 0x0000BD88
		public static float GetGlobalFloat(string name)
		{
			return Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public static float GetGlobalFloat(int nameID)
		{
			return Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public static int GetGlobalInt(string name)
		{
			return (int)Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public static int GetGlobalInt(int nameID)
		{
			return (int)Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static Vector4 GetGlobalVector(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		public static Vector4 GetGlobalVector(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public static Color GetGlobalColor(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public static Color GetGlobalColor(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000DC78 File Offset: 0x0000BE78
		public static Matrix4x4 GetGlobalMatrix(string name)
		{
			return Shader.GetGlobalMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public static Matrix4x4 GetGlobalMatrix(int nameID)
		{
			return Shader.GetGlobalMatrixImpl(nameID);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public static Texture GetGlobalTexture(string name)
		{
			return Shader.GetGlobalTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		public static Texture GetGlobalTexture(int nameID)
		{
			return Shader.GetGlobalTextureImpl(nameID);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public static float[] GetGlobalFloatArray(string name)
		{
			return Shader.GetGlobalFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000DD08 File Offset: 0x0000BF08
		public static float[] GetGlobalFloatArray(int nameID)
		{
			return (Shader.GetGlobalFloatArrayCountImpl(nameID) != 0) ? Shader.GetGlobalFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public static Vector4[] GetGlobalVectorArray(string name)
		{
			return Shader.GetGlobalVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public static Vector4[] GetGlobalVectorArray(int nameID)
		{
			return (Shader.GetGlobalVectorArrayCountImpl(nameID) != 0) ? Shader.GetGlobalVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public static Matrix4x4[] GetGlobalMatrixArray(string name)
		{
			return Shader.GetGlobalMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public static Matrix4x4[] GetGlobalMatrixArray(int nameID)
		{
			return (Shader.GetGlobalMatrixArrayCountImpl(nameID) != 0) ? Shader.GetGlobalMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000DDB3 File Offset: 0x0000BFB3
		public static void GetGlobalFloatArray(string name, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
		public static void GetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(nameID, values);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000DDCE File Offset: 0x0000BFCE
		public static void GetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		public static void GetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		public static void GetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000DDF9 File Offset: 0x0000BFF9
		public static void GetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(nameID, values);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private Shader()
		{
		}

		// Token: 0x06000A38 RID: 2616
		[FreeFunction("ShaderScripting::GetPropertyName")]
		[MethodImpl(4096)]
		private static extern string GetPropertyName([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A39 RID: 2617
		[FreeFunction("ShaderScripting::GetPropertyNameId")]
		[MethodImpl(4096)]
		private static extern int GetPropertyNameId([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A3A RID: 2618
		[FreeFunction("ShaderScripting::GetPropertyType")]
		[MethodImpl(4096)]
		private static extern ShaderPropertyType GetPropertyType([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A3B RID: 2619
		[FreeFunction("ShaderScripting::GetPropertyDescription")]
		[MethodImpl(4096)]
		private static extern string GetPropertyDescription([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A3C RID: 2620
		[FreeFunction("ShaderScripting::GetPropertyFlags")]
		[MethodImpl(4096)]
		private static extern ShaderPropertyFlags GetPropertyFlags([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A3D RID: 2621
		[FreeFunction("ShaderScripting::GetPropertyAttributes")]
		[MethodImpl(4096)]
		private static extern string[] GetPropertyAttributes([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000DE04 File Offset: 0x0000C004
		[FreeFunction("ShaderScripting::GetPropertyDefaultValue")]
		private static Vector4 GetPropertyDefaultValue([NotNull] Shader shader, int propertyIndex)
		{
			Vector4 vector;
			Shader.GetPropertyDefaultValue_Injected(shader, propertyIndex, out vector);
			return vector;
		}

		// Token: 0x06000A3F RID: 2623
		[FreeFunction("ShaderScripting::GetPropertyTextureDimension")]
		[MethodImpl(4096)]
		private static extern TextureDimension GetPropertyTextureDimension([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A40 RID: 2624
		[FreeFunction("ShaderScripting::GetPropertyTextureDefaultName")]
		[MethodImpl(4096)]
		private static extern string GetPropertyTextureDefaultName([NotNull] Shader shader, int propertyIndex);

		// Token: 0x06000A41 RID: 2625
		[FreeFunction("ShaderScripting::FindTextureStack")]
		[MethodImpl(4096)]
		private static extern bool FindTextureStackImpl([NotNull] Shader s, int propertyIdx, out string stackName, out int layerIndex);

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000DE1C File Offset: 0x0000C01C
		private static void CheckPropertyIndex(Shader s, int propertyIndex)
		{
			bool flag = propertyIndex < 0 || propertyIndex >= s.GetPropertyCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("propertyIndex");
			}
		}

		// Token: 0x06000A43 RID: 2627
		[MethodImpl(4096)]
		public extern int GetPropertyCount();

		// Token: 0x06000A44 RID: 2628
		[MethodImpl(4096)]
		public extern int FindPropertyIndex(string propertyName);

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public string GetPropertyName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyName(this, propertyIndex);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000DE70 File Offset: 0x0000C070
		public int GetPropertyNameId(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyNameId(this, propertyIndex);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000DE94 File Offset: 0x0000C094
		public ShaderPropertyType GetPropertyType(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyType(this, propertyIndex);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public string GetPropertyDescription(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyDescription(this, propertyIndex);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public ShaderPropertyFlags GetPropertyFlags(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyFlags(this, propertyIndex);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000DF00 File Offset: 0x0000C100
		public string[] GetPropertyAttributes(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyAttributes(this, propertyIndex);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000DF24 File Offset: 0x0000C124
		public float GetPropertyDefaultFloatValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Float && propertyType != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Float or Range.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex)[0];
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000DF74 File Offset: 0x0000C174
		public Vector4 GetPropertyDefaultVectorValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Color && propertyType != ShaderPropertyType.Vector;
			if (flag)
			{
				throw new ArgumentException("Property type is not Color or Vector.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public Vector2 GetPropertyRangeLimits(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Range.");
			}
			Vector4 propertyDefaultValue = Shader.GetPropertyDefaultValue(this, propertyIndex);
			return new Vector2(propertyDefaultValue[1], propertyDefaultValue[2]);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000E010 File Offset: 0x0000C210
		public TextureDimension GetPropertyTextureDimension(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not TexEnv.");
			}
			return Shader.GetPropertyTextureDimension(this, propertyIndex);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000E050 File Offset: 0x0000C250
		public string GetPropertyTextureDefaultName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.GetPropertyTextureDefaultName(this, propertyIndex);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000E090 File Offset: 0x0000C290
		public bool FindTextureStack(int propertyIndex, out string stackName, out int layerIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.FindTextureStackImpl(this, propertyIndex, out stackName, out layerIndex);
		}

		// Token: 0x06000A51 RID: 2641
		[MethodImpl(4096)]
		private static extern void SetGlobalVectorImpl_Injected(int name, ref Vector4 value);

		// Token: 0x06000A52 RID: 2642
		[MethodImpl(4096)]
		private static extern void SetGlobalMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x06000A53 RID: 2643
		[MethodImpl(4096)]
		private static extern void GetGlobalVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000A54 RID: 2644
		[MethodImpl(4096)]
		private static extern void GetGlobalMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000A55 RID: 2645
		[MethodImpl(4096)]
		private static extern void GetPropertyDefaultValue_Injected(Shader shader, int propertyIndex, out Vector4 ret);
	}
}
