using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003E1 RID: 993
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracingShader.h")]
	public sealed class RayTracingShader : Object
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002233 RID: 8755
		public extern float maxRecursionDepth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06002234 RID: 8756
		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x06002235 RID: 8757
		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x06002236 RID: 8758 RVA: 0x00039982 File Offset: 0x00037B82
		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0003998D File Offset: 0x00037B8D
		[FreeFunction(Name = "RayTracingShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x06002238 RID: 8760
		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x06002239 RID: 8761
		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x0600223A RID: 8762
		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x0600223B RID: 8763
		[FreeFunction(Name = "RayTracingShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x0600223C RID: 8764
		[NativeMethod(Name = "RayTracingShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTexture(int nameID, [NotNull] Texture texture);

		// Token: 0x0600223D RID: 8765
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetBuffer(int nameID, [NotNull] ComputeBuffer buffer);

		// Token: 0x0600223E RID: 8766
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetGraphicsBuffer(int nameID, [NotNull] GraphicsBuffer buffer);

		// Token: 0x0600223F RID: 8767
		[NativeMethod(Name = "RayTracingShaderScripting::SetAccelerationStructure", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetAccelerationStructure(int nameID, [NotNull] RayTracingAccelerationStructure accelerationStrucure);

		// Token: 0x06002240 RID: 8768
		[MethodImpl(4096)]
		public extern void SetShaderPass(string passName);

		// Token: 0x06002241 RID: 8769
		[NativeMethod(Name = "RayTracingShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetTextureFromGlobal(int nameID, int globalTextureNameID);

		// Token: 0x06002242 RID: 8770
		[NativeName("DispatchRayTracingShader")]
		[MethodImpl(4096)]
		public extern void Dispatch(string rayGenFunctionName, int width, int height, int depth, Camera camera = null);

		// Token: 0x06002243 RID: 8771 RVA: 0x00039998 File Offset: 0x00037B98
		public void SetBuffer(int nameID, GraphicsBuffer buffer)
		{
			this.SetGraphicsBuffer(nameID, buffer);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private RayTracingShader()
		{
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000399A4 File Offset: 0x00037BA4
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000399B5 File Offset: 0x00037BB5
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000399C6 File Offset: 0x00037BC6
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000399D7 File Offset: 0x00037BD7
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000399E8 File Offset: 0x00037BE8
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000399F9 File Offset: 0x00037BF9
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00039A0A File Offset: 0x00037C0A
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00039A1B File Offset: 0x00037C1B
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00039A27 File Offset: 0x00037C27
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00039A38 File Offset: 0x00037C38
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00039A44 File Offset: 0x00037C44
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00039A5B File Offset: 0x00037C5B
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00039A6D File Offset: 0x00037C6D
		public void SetTexture(string resourceName, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(resourceName), texture);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00039A7E File Offset: 0x00037C7E
		public void SetBuffer(string resourceName, ComputeBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(resourceName), buffer);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00039A8F File Offset: 0x00037C8F
		public void SetBuffer(string resourceName, GraphicsBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(resourceName), buffer);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00039AA0 File Offset: 0x00037CA0
		public void SetAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetAccelerationStructure(Shader.PropertyToID(name), accelerationStructure);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00039AB1 File Offset: 0x00037CB1
		public void SetTextureFromGlobal(string resourceName, string globalTextureName)
		{
			this.SetTextureFromGlobal(Shader.PropertyToID(resourceName), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x06002256 RID: 8790
		[MethodImpl(4096)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x06002257 RID: 8791
		[MethodImpl(4096)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
