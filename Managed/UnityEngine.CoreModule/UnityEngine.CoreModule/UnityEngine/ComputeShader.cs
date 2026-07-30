using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D9 RID: 473
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[UsedByNativeCode]
	public sealed class ComputeShader : Object
	{
		// Token: 0x060014AC RID: 5292
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[RequiredByNativeCode]
		[MethodImpl(4096)]
		public extern int FindKernel(string name);

		// Token: 0x060014AD RID: 5293
		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasKernel(string name);

		// Token: 0x060014AE RID: 5294
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x060014AF RID: 5295
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x060014B0 RID: 5296 RVA: 0x000220DA File Offset: 0x000202DA
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000220E5 File Offset: 0x000202E5
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x060014B2 RID: 5298
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x060014B3 RID: 5299
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x060014B4 RID: 5300
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x060014B5 RID: 5301
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x060014B6 RID: 5302
		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTexture(int kernelIndex, int nameID, [NotNull] Texture texture, int mipLevel);

		// Token: 0x060014B7 RID: 5303
		[NativeMethod(Name = "ComputeShaderScripting::SetRenderTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetRenderTexture(int kernelIndex, int nameID, [NotNull] RenderTexture texture, int mipLevel, RenderTextureSubElement element);

		// Token: 0x060014B8 RID: 5304
		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID);

		// Token: 0x060014B9 RID: 5305
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull] ComputeBuffer buffer);

		// Token: 0x060014BA RID: 5306
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull] GraphicsBuffer buffer);

		// Token: 0x060014BB RID: 5307 RVA: 0x000220F0 File Offset: 0x000202F0
		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000220FD File Offset: 0x000202FD
		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060014BD RID: 5309
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetConstantBuffer(int nameID, [NotNull] ComputeBuffer buffer, int offset, int size);

		// Token: 0x060014BE RID: 5310
		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z);

		// Token: 0x060014BF RID: 5311
		[NativeName("DispatchComputeShader")]
		[MethodImpl(4096)]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x060014C0 RID: 5312
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchIndirect(int kernelIndex, [NotNull] ComputeBuffer argsBuffer, uint argsOffset);

		// Token: 0x060014C1 RID: 5313
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Internal_DispatchIndirectGraphicsBuffer(int kernelIndex, [NotNull] GraphicsBuffer argsBuffer, uint argsOffset);

		// Token: 0x060014C2 RID: 5314
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x060014C3 RID: 5315
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x060014C4 RID: 5316
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x060014C5 RID: 5317
		[FreeFunction("ComputeShaderScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern string[] GetShaderKeywords();

		// Token: 0x060014C6 RID: 5318
		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0002210C File Offset: 0x0002030C
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00022124 File Offset: 0x00020324
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

		// Token: 0x060014C9 RID: 5321 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private ComputeShader()
		{
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0002212F File Offset: 0x0002032F
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00022140 File Offset: 0x00020340
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00022151 File Offset: 0x00020351
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00022162 File Offset: 0x00020362
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00022173 File Offset: 0x00020373
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00022184 File Offset: 0x00020384
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00022195 File Offset: 0x00020395
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000221A6 File Offset: 0x000203A6
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000221B2 File Offset: 0x000203B2
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000221C3 File Offset: 0x000203C3
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000221CF File Offset: 0x000203CF
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000221E6 File Offset: 0x000203E6
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x000221F8 File Offset: 0x000203F8
		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			this.SetTexture(kernelIndex, nameID, texture, 0);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00022206 File Offset: 0x00020406
		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00022219 File Offset: 0x00020419
		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0002222D File Offset: 0x0002042D
		public void SetTexture(int kernelIndex, int nameID, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, nameID, texture, mipLevel, element);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0002223E File Offset: 0x0002043E
		public void SetTexture(int kernelIndex, string name, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel, element);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00022254 File Offset: 0x00020454
		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0002226B File Offset: 0x0002046B
		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0002227D File Offset: 0x0002047D
		public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00022290 File Offset: 0x00020490
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000222DA File Offset: 0x000204DA
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000222E8 File Offset: 0x000204E8
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirectGraphicsBuffer(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00022332 File Offset: 0x00020532
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x060014E2 RID: 5346
		[MethodImpl(4096)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x060014E3 RID: 5347
		[MethodImpl(4096)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
