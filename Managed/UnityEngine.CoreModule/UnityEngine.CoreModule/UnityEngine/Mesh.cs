using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000144 RID: 324
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[RequiredByNativeCode]
	public sealed class Mesh : Object
	{
		// Token: 0x06000C1E RID: 3102
		[FreeFunction("MeshScripting::CreateMesh")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x06000C1F RID: 3103 RVA: 0x0000F71E File Offset: 0x0000D91E
		[RequiredByNativeCode]
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x06000C20 RID: 3104
		[FreeFunction("MeshScripting::MeshFromInstanceId")]
		[MethodImpl(4096)]
		internal static extern Mesh FromInstanceID(int id);

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C21 RID: 3105
		// (set) Token: 0x06000C22 RID: 3106
		public extern IndexFormat indexFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000C23 RID: 3107
		[FreeFunction(Name = "MeshScripting::SetIndexBufferParams", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetIndexBufferParams(int indexCount, IndexFormat format);

		// Token: 0x06000C24 RID: 3108
		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetIndexBufferData(IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000C25 RID: 3109
		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferDataFromArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void InternalSetIndexBufferDataFromArray(Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000C26 RID: 3110
		[FreeFunction(Name = "MeshScripting::SetVertexBufferParams", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes);

		// Token: 0x06000C27 RID: 3111
		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void InternalSetVertexBufferData(int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000C28 RID: 3112
		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferDataFromArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void InternalSetVertexBufferDataFromArray(int stream, Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x06000C29 RID: 3113
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesAlloc", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Array GetVertexAttributesAlloc();

		// Token: 0x06000C2A RID: 3114
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetVertexAttributesArray([NotNull] VertexAttributeDescriptor[] attributes);

		// Token: 0x06000C2B RID: 3115
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesList", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetVertexAttributesList([NotNull] List<VertexAttributeDescriptor> attributes);

		// Token: 0x06000C2C RID: 3116
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetVertexAttributeCountImpl();

		// Token: 0x06000C2D RID: 3117 RVA: 0x0000F730 File Offset: 0x0000D930
		[FreeFunction(Name = "MeshScripting::GetVertexAttributeByIndex", HasExplicitThis = true, ThrowsException = true)]
		public VertexAttributeDescriptor GetVertexAttribute(int index)
		{
			VertexAttributeDescriptor vertexAttributeDescriptor;
			this.GetVertexAttribute_Injected(index, out vertexAttributeDescriptor);
			return vertexAttributeDescriptor;
		}

		// Token: 0x06000C2E RID: 3118
		[FreeFunction(Name = "MeshScripting::GetIndexStart", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern uint GetIndexStartImpl(int submesh);

		// Token: 0x06000C2F RID: 3119
		[FreeFunction(Name = "MeshScripting::GetIndexCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern uint GetIndexCountImpl(int submesh);

		// Token: 0x06000C30 RID: 3120
		[FreeFunction(Name = "MeshScripting::GetTrianglesCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern uint GetTrianglesCountImpl(int submesh);

		// Token: 0x06000C31 RID: 3121
		[FreeFunction(Name = "MeshScripting::GetBaseVertex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern uint GetBaseVertexImpl(int submesh);

		// Token: 0x06000C32 RID: 3122
		[FreeFunction(Name = "MeshScripting::GetTriangles", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int[] GetTrianglesImpl(int submesh, bool applyBaseVertex);

		// Token: 0x06000C33 RID: 3123
		[FreeFunction(Name = "MeshScripting::GetIndices", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int[] GetIndicesImpl(int submesh, bool applyBaseVertex);

		// Token: 0x06000C34 RID: 3124
		[FreeFunction(Name = "SetMeshIndicesFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetIndicesImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x06000C35 RID: 3125
		[FreeFunction(Name = "SetMeshIndicesFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetIndicesNativeArrayImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x06000C36 RID: 3126
		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTrianglesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		// Token: 0x06000C37 RID: 3127
		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray16", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTrianglesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex);

		// Token: 0x06000C38 RID: 3128
		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetIndicesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		// Token: 0x06000C39 RID: 3129
		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray16", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetIndicesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex);

		// Token: 0x06000C3A RID: 3130
		[FreeFunction(Name = "MeshScripting::PrintErrorCantAccessChannel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void PrintErrorCantAccessChannel(VertexAttribute ch);

		// Token: 0x06000C3B RID: 3131
		[FreeFunction(Name = "MeshScripting::HasChannel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool HasVertexAttribute(VertexAttribute attr);

		// Token: 0x06000C3C RID: 3132
		[FreeFunction(Name = "MeshScripting::GetChannelDimension", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetVertexAttributeDimension(VertexAttribute attr);

		// Token: 0x06000C3D RID: 3133
		[FreeFunction(Name = "MeshScripting::GetChannelFormat", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr);

		// Token: 0x06000C3E RID: 3134
		[FreeFunction(Name = "SetMeshComponentFromArrayFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		// Token: 0x06000C3F RID: 3135
		[FreeFunction(Name = "SetMeshComponentFromNativeArrayFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetNativeArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		// Token: 0x06000C40 RID: 3136
		[FreeFunction(Name = "AllocExtractMeshComponentFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Array GetAllocArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim);

		// Token: 0x06000C41 RID: 3137
		[FreeFunction(Name = "ExtractMeshComponentFromScript", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values);

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C42 RID: 3138
		public extern int vertexBufferCount
		{
			[FreeFunction(Name = "MeshScripting::GetVertexBufferCount", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000C43 RID: 3139
		[NativeThrows]
		[FreeFunction(Name = "MeshScripting::GetNativeVertexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern IntPtr GetNativeVertexBufferPtr(int index);

		// Token: 0x06000C44 RID: 3140
		[FreeFunction(Name = "MeshScripting::GetNativeIndexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern IntPtr GetNativeIndexBufferPtr();

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C45 RID: 3141
		public extern int blendShapeCount
		{
			[NativeMethod(Name = "GetBlendShapeChannelCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000C46 RID: 3142
		[FreeFunction(Name = "MeshScripting::ClearBlendShapes", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearBlendShapes();

		// Token: 0x06000C47 RID: 3143
		[FreeFunction(Name = "MeshScripting::GetBlendShapeName", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern string GetBlendShapeName(int shapeIndex);

		// Token: 0x06000C48 RID: 3144
		[FreeFunction(Name = "MeshScripting::GetBlendShapeIndex", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetBlendShapeIndex(string blendShapeName);

		// Token: 0x06000C49 RID: 3145
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern int GetBlendShapeFrameCount(int shapeIndex);

		// Token: 0x06000C4A RID: 3146
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameWeight", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex);

		// Token: 0x06000C4B RID: 3147
		[FreeFunction(Name = "GetBlendShapeFrameVerticesFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		// Token: 0x06000C4C RID: 3148
		[FreeFunction(Name = "AddBlendShapeFrameFromScript", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		// Token: 0x06000C4D RID: 3149
		[NativeMethod("HasBoneWeights")]
		[MethodImpl(4096)]
		private extern bool HasBoneWeights();

		// Token: 0x06000C4E RID: 3150
		[FreeFunction(Name = "MeshScripting::GetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern BoneWeight[] GetBoneWeightsImpl();

		// Token: 0x06000C4F RID: 3151
		[FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetBoneWeightsImpl(BoneWeight[] weights);

		// Token: 0x06000C50 RID: 3152 RVA: 0x0000F747 File Offset: 0x0000D947
		public void SetBoneWeights(NativeArray<byte> bonesPerVertex, NativeArray<BoneWeight1> weights)
		{
			this.InternalSetBoneWeights((IntPtr)bonesPerVertex.GetUnsafeReadOnlyPtr<byte>(), bonesPerVertex.Length, (IntPtr)weights.GetUnsafeReadOnlyPtr<BoneWeight1>(), weights.Length);
		}

		// Token: 0x06000C51 RID: 3153
		[SecurityCritical]
		[FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void InternalSetBoneWeights(IntPtr bonesPerVertex, int bonesPerVertexSize, IntPtr weights, int weightsSize);

		// Token: 0x06000C52 RID: 3154 RVA: 0x0000F778 File Offset: 0x0000D978
		public unsafe NativeArray<BoneWeight1> GetAllBoneWeights()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BoneWeight1>((void*)this.GetAllBoneWeightsArray(), this.GetAllBoneWeightsArraySize(), Allocator.None);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public unsafe NativeArray<byte> GetBonesPerVertex()
		{
			int num = (this.HasBoneWeights() ? this.vertexCount : 0);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)this.GetBonesPerVertexArray(), num, Allocator.None);
		}

		// Token: 0x06000C54 RID: 3156
		[FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArraySize", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetAllBoneWeightsArraySize();

		// Token: 0x06000C55 RID: 3157
		[SecurityCritical]
		[FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetAllBoneWeightsArray();

		// Token: 0x06000C56 RID: 3158
		[SecurityCritical]
		[FreeFunction(Name = "MeshScripting::GetBonesPerVertexArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetBonesPerVertexArray();

		// Token: 0x06000C57 RID: 3159
		[MethodImpl(4096)]
		private extern int GetBindposeCount();

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C58 RID: 3160
		// (set) Token: 0x06000C59 RID: 3161
		[NativeName("BindPosesFromScript")]
		public extern Matrix4x4[] bindposes
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000C5A RID: 3162
		[FreeFunction(Name = "MeshScripting::ExtractBoneWeightsIntoArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetBoneWeightsNonAllocImpl([Out] BoneWeight[] values);

		// Token: 0x06000C5B RID: 3163
		[FreeFunction(Name = "MeshScripting::ExtractBindPosesIntoArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetBindposesNonAllocImpl([Out] Matrix4x4[] values);

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C5C RID: 3164
		public extern bool isReadable
		{
			[NativeMethod("GetIsReadable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C5D RID: 3165
		internal extern bool canAccess
		{
			[NativeMethod("CanAccessFromScript")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C5E RID: 3166
		public extern int vertexCount
		{
			[NativeMethod("GetVertexCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C5F RID: 3167
		// (set) Token: 0x06000C60 RID: 3168
		public extern int subMeshCount
		{
			[NativeMethod(Name = "GetSubMeshCount")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "MeshScripting::SetSubMeshCount", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		[FreeFunction("MeshScripting::SetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMesh_Injected(index, ref desc, flags);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		[FreeFunction("MeshScripting::GetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public SubMeshDescriptor GetSubMesh(int index)
		{
			SubMeshDescriptor subMeshDescriptor;
			this.GetSubMesh_Injected(index, out subMeshDescriptor);
			return subMeshDescriptor;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0000F800 File Offset: 0x0000DA00
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0000F816 File Offset: 0x0000DA16
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
			set
			{
				this.set_bounds_Injected(ref value);
			}
		}

		// Token: 0x06000C65 RID: 3173
		[NativeMethod("Clear")]
		[MethodImpl(4096)]
		private extern void ClearImpl(bool keepVertexLayout);

		// Token: 0x06000C66 RID: 3174
		[NativeMethod("RecalculateBounds")]
		[MethodImpl(4096)]
		private extern void RecalculateBoundsImpl(MeshUpdateFlags flags);

		// Token: 0x06000C67 RID: 3175
		[NativeMethod("RecalculateNormals")]
		[MethodImpl(4096)]
		private extern void RecalculateNormalsImpl(MeshUpdateFlags flags);

		// Token: 0x06000C68 RID: 3176
		[NativeMethod("RecalculateTangents")]
		[MethodImpl(4096)]
		private extern void RecalculateTangentsImpl(MeshUpdateFlags flags);

		// Token: 0x06000C69 RID: 3177
		[NativeMethod("MarkDynamic")]
		[MethodImpl(4096)]
		private extern void MarkDynamicImpl();

		// Token: 0x06000C6A RID: 3178
		[NativeMethod("MarkModified")]
		[MethodImpl(4096)]
		public extern void MarkModified();

		// Token: 0x06000C6B RID: 3179
		[NativeMethod("UploadMeshData")]
		[MethodImpl(4096)]
		private extern void UploadMeshDataImpl(bool markNoLongerReadable);

		// Token: 0x06000C6C RID: 3180
		[FreeFunction(Name = "MeshScripting::GetPrimitiveType", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern MeshTopology GetTopologyImpl(int submesh);

		// Token: 0x06000C6D RID: 3181
		[NativeMethod("GetMeshMetric")]
		[MethodImpl(4096)]
		public extern float GetUVDistributionMetric(int uvSetIndex);

		// Token: 0x06000C6E RID: 3182
		[NativeMethod(Name = "MeshScripting::CombineMeshes", IsFreeFunction = true, ThrowsException = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void CombineMeshesImpl(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData);

		// Token: 0x06000C6F RID: 3183
		[NativeMethod("Optimize")]
		[MethodImpl(4096)]
		private extern void OptimizeImpl();

		// Token: 0x06000C70 RID: 3184
		[NativeMethod("OptimizeIndexBuffers")]
		[MethodImpl(4096)]
		private extern void OptimizeIndexBuffersImpl();

		// Token: 0x06000C71 RID: 3185
		[NativeMethod("OptimizeReorderVertexBuffer")]
		[MethodImpl(4096)]
		private extern void OptimizeReorderVertexBufferImpl();

		// Token: 0x06000C72 RID: 3186 RVA: 0x0000F820 File Offset: 0x0000DA20
		internal static VertexAttribute GetUVChannel(int uvIndex)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentException("GetUVChannel called for bad uvIndex", "uvIndex");
			}
			return VertexAttribute.TexCoord0 + uvIndex;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0000F854 File Offset: 0x0000DA54
		internal static int DefaultDimensionForChannel(VertexAttribute channel)
		{
			bool flag = channel == VertexAttribute.Position || channel == VertexAttribute.Normal;
			int num;
			if (flag)
			{
				num = 3;
			}
			else
			{
				bool flag2 = channel >= VertexAttribute.TexCoord0 && channel <= VertexAttribute.TexCoord7;
				if (flag2)
				{
					num = 2;
				}
				else
				{
					bool flag3 = channel == VertexAttribute.Tangent || channel == VertexAttribute.Color;
					if (!flag3)
					{
						throw new ArgumentException("DefaultDimensionForChannel called for bad channel", "channel");
					}
					num = 4;
				}
			}
			return num;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = this.HasVertexAttribute(channel);
				if (flag)
				{
					return (T[])this.GetAllocArrayFromChannelImpl(channel, format, dim);
				}
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			return new T[0];
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel)
		{
			return this.GetAllocArrayFromChannel<T>(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0000F91C File Offset: 0x0000DB1C
		private void SetSizedArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				bool flag5 = values == null;
				if (flag5)
				{
					valuesStart = 0;
				}
				this.SetArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		private void SetSizedNativeArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				this.SetNativeArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		private void SetArrayForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, format, dim, values, num, 0, num, flags);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		private void SetArrayForChannel<T>(VertexAttribute channel, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), values, num, 0, num, flags);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		private void SetListForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, format, dim, NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0000FB48 File Offset: 0x0000DD48
		private void SetListForChannel<T>(VertexAttribute channel, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0000FB76 File Offset: 0x0000DD76
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim)
		{
			this.GetListForChannel<T>(buffer, capacity, channel, dim, VertexAttributeFormat.Float32);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0000FB88 File Offset: 0x0000DD88
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim, VertexAttributeFormat channelType)
		{
			buffer.Clear();
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			else
			{
				bool flag2 = !this.HasVertexAttribute(channel);
				if (!flag2)
				{
					NoAllocHelpers.EnsureListElemCount<T>(buffer, capacity);
					this.GetArrayFromChannelImpl(channel, channelType, dim, NoAllocHelpers.ExtractArrayFromList(buffer));
				}
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0000FBF9 File Offset: 0x0000DDF9
		public Vector3[] vertices
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Position);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Position, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0000FC08 File Offset: 0x0000DE08
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0000FC21 File Offset: 0x0000DE21
		public Vector3[] normals
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Normal);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Normal, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0000FC30 File Offset: 0x0000DE30
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0000FC49 File Offset: 0x0000DE49
		public Vector4[] tangents
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector4>(VertexAttribute.Tangent);
			}
			set
			{
				this.SetArrayForChannel<Vector4>(VertexAttribute.Tangent, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0000FC58 File Offset: 0x0000DE58
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0000FC71 File Offset: 0x0000DE71
		public Vector2[] uv
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord0);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord0, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0000FC80 File Offset: 0x0000DE80
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0000FC99 File Offset: 0x0000DE99
		public Vector2[] uv2
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord1);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord1, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0000FCC1 File Offset: 0x0000DEC1
		public Vector2[] uv3
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord2);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord2, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0000FCE9 File Offset: 0x0000DEE9
		public Vector2[] uv4
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord3);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord3, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0000FD11 File Offset: 0x0000DF11
		public Vector2[] uv5
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord4);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord4, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0000FD20 File Offset: 0x0000DF20
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0000FD3A File Offset: 0x0000DF3A
		public Vector2[] uv6
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord5);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord5, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0000FD48 File Offset: 0x0000DF48
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0000FD62 File Offset: 0x0000DF62
		public Vector2[] uv7
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord6);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord6, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0000FD70 File Offset: 0x0000DF70
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0000FD8A File Offset: 0x0000DF8A
		public Vector2[] uv8
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord7);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord7, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0000FD98 File Offset: 0x0000DF98
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0000FDB1 File Offset: 0x0000DFB1
		public Color[] colors
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color>(VertexAttribute.Color);
			}
			set
			{
				this.SetArrayForChannel<Color>(VertexAttribute.Color, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0000FDDB File Offset: 0x0000DFDB
		public Color32[] colors32
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}
			set
			{
				this.SetArrayForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public void GetVertices(List<Vector3> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("The result vertices list cannot be null.", "vertices");
			}
			this.GetListForChannel<Vector3>(vertices, this.vertexCount, VertexAttribute.Position, Mesh.DefaultDimensionForChannel(VertexAttribute.Position));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0000FE27 File Offset: 0x0000E027
		public void SetVertices(List<Vector3> inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength<Vector3>(inVertices));
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0000FE39 File Offset: 0x0000E039
		[ExcludeFromDocs]
		public void SetVertices(List<Vector3> inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0000FE47 File Offset: 0x0000E047
		public void SetVertices(List<Vector3> inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Position, inVertices, start, length, flags);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0000FE57 File Offset: 0x0000E057
		public void SetVertices(Vector3[] inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength(inVertices));
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0000FE69 File Offset: 0x0000E069
		[ExcludeFromDocs]
		public void SetVertices(Vector3[] inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0000FE78 File Offset: 0x0000E078
		public void SetVertices(Vector3[] inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Position), inVertices, NoAllocHelpers.SafeLength(inVertices), start, length, flags);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public void SetVertices<T>(NativeArray<T> inVertices) where T : struct
		{
			this.SetVertices<T>(inVertices, 0, inVertices.Length);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0000FEB3 File Offset: 0x0000E0B3
		[ExcludeFromDocs]
		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length) where T : struct
		{
			this.SetVertices<T>(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetVertices with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, (IntPtr)inVertices.GetUnsafeReadOnlyPtr<T>(), inVertices.Length, start, length, flags);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0000FF10 File Offset: 0x0000E110
		public void GetNormals(List<Vector3> normals)
		{
			bool flag = normals == null;
			if (flag)
			{
				throw new ArgumentNullException("The result normals list cannot be null.", "normals");
			}
			this.GetListForChannel<Vector3>(normals, this.vertexCount, VertexAttribute.Normal, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal));
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0000FF4B File Offset: 0x0000E14B
		public void SetNormals(List<Vector3> inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength<Vector3>(inNormals));
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0000FF5D File Offset: 0x0000E15D
		[ExcludeFromDocs]
		public void SetNormals(List<Vector3> inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0000FF6B File Offset: 0x0000E16B
		public void SetNormals(List<Vector3> inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Normal, inNormals, start, length, flags);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0000FF7B File Offset: 0x0000E17B
		public void SetNormals(Vector3[] inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength(inNormals));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0000FF8D File Offset: 0x0000E18D
		[ExcludeFromDocs]
		public void SetNormals(Vector3[] inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0000FF9C File Offset: 0x0000E19C
		public void SetNormals(Vector3[] inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal), inNormals, NoAllocHelpers.SafeLength(inNormals), start, length, flags);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public void SetNormals<T>(NativeArray<T> inNormals) where T : struct
		{
			this.SetNormals<T>(inNormals, 0, inNormals.Length);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0000FFD7 File Offset: 0x0000E1D7
		[ExcludeFromDocs]
		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length) where T : struct
		{
			this.SetNormals<T>(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetNormals with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3, (IntPtr)inNormals.GetUnsafeReadOnlyPtr<T>(), inNormals.Length, start, length, flags);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00010034 File Offset: 0x0000E234
		public void GetTangents(List<Vector4> tangents)
		{
			bool flag = tangents == null;
			if (flag)
			{
				throw new ArgumentNullException("The result tangents list cannot be null.", "tangents");
			}
			this.GetListForChannel<Vector4>(tangents, this.vertexCount, VertexAttribute.Tangent, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent));
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0001006F File Offset: 0x0000E26F
		public void SetTangents(List<Vector4> inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength<Vector4>(inTangents));
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00010081 File Offset: 0x0000E281
		[ExcludeFromDocs]
		public void SetTangents(List<Vector4> inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0001008F File Offset: 0x0000E28F
		public void SetTangents(List<Vector4> inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector4>(VertexAttribute.Tangent, inTangents, start, length, flags);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0001009F File Offset: 0x0000E29F
		public void SetTangents(Vector4[] inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength(inTangents));
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000100B1 File Offset: 0x0000E2B1
		[ExcludeFromDocs]
		public void SetTangents(Vector4[] inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public void SetTangents(Vector4[] inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent), inTangents, NoAllocHelpers.SafeLength(inTangents), start, length, flags);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public void SetTangents<T>(NativeArray<T> inTangents) where T : struct
		{
			this.SetTangents<T>(inTangents, 0, inTangents.Length);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000100FB File Offset: 0x0000E2FB
		[ExcludeFromDocs]
		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length) where T : struct
		{
			this.SetTangents<T>(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0001010C File Offset: 0x0000E30C
		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 16;
			if (flag)
			{
				throw new ArgumentException("SetTangents with NativeArray should use struct type that is 16 bytes (4x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, (IntPtr)inTangents.GetUnsafeReadOnlyPtr<T>(), inTangents.Length, start, length, flags);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00010158 File Offset: 0x0000E358
		public void GetColors(List<Color> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("The result colors list cannot be null.", "colors");
			}
			this.GetListForChannel<Color>(colors, this.vertexCount, VertexAttribute.Color, Mesh.DefaultDimensionForChannel(VertexAttribute.Color));
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00010193 File Offset: 0x0000E393
		public void SetColors(List<Color> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color>(inColors));
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000101A5 File Offset: 0x0000E3A5
		[ExcludeFromDocs]
		public void SetColors(List<Color> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000101B3 File Offset: 0x0000E3B3
		public void SetColors(List<Color> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color>(VertexAttribute.Color, inColors, start, length, flags);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000101C3 File Offset: 0x0000E3C3
		public void SetColors(Color[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000101D5 File Offset: 0x0000E3D5
		[ExcludeFromDocs]
		public void SetColors(Color[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public void SetColors(Color[] inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Color), inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0001020C File Offset: 0x0000E40C
		public void GetColors(List<Color32> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("The result colors list cannot be null.", "colors");
			}
			this.GetListForChannel<Color32>(colors, this.vertexCount, VertexAttribute.Color, 4, VertexAttributeFormat.UNorm8);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00010243 File Offset: 0x0000E443
		public void SetColors(List<Color32> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color32>(inColors));
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00010255 File Offset: 0x0000E455
		[ExcludeFromDocs]
		public void SetColors(List<Color32> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00010263 File Offset: 0x0000E463
		public void SetColors(List<Color32> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, start, length, flags);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00010275 File Offset: 0x0000E475
		public void SetColors(Color32[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00010287 File Offset: 0x0000E487
		[ExcludeFromDocs]
		public void SetColors(Color32[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00010298 File Offset: 0x0000E498
		public void SetColors(Color32[] inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000102BB File Offset: 0x0000E4BB
		public void SetColors<T>(NativeArray<T> inColors) where T : struct
		{
			this.SetColors<T>(inColors, 0, inColors.Length);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000102CE File Offset: 0x0000E4CE
		[ExcludeFromDocs]
		public void SetColors<T>(NativeArray<T> inColors, int start, int length) where T : struct
		{
			this.SetColors<T>(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000102DC File Offset: 0x0000E4DC
		public void SetColors<T>(NativeArray<T> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			bool flag = num != 16 && num != 4;
			if (flag)
			{
				throw new ArgumentException("SetColors with NativeArray should use struct type that is 16 bytes (4x float) or 4 bytes (4x unorm) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Color, (num == 4) ? VertexAttributeFormat.UNorm8 : VertexAttributeFormat.Float32, 4, (IntPtr)inColors.GetUnsafeReadOnlyPtr<T>(), inColors.Length, start, length, flags);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00010338 File Offset: 0x0000E538
		private void SetUvsImpl<T>(int uvIndex, int dim, List<T> uvs, int start, int length, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				Debug.LogError("The uv index is invalid. Must be in the range 0 to 7.");
			}
			else
			{
				this.SetListForChannel<T>(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, start, length, flags);
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00010379 File Offset: 0x0000E579
		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector2>(uvs));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0001038C File Offset: 0x0000E58C
		public void SetUVs(int channel, List<Vector3> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector3>(uvs));
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001039F File Offset: 0x0000E59F
		public void SetUVs(int channel, List<Vector4> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector4>(uvs));
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000103B2 File Offset: 0x0000E5B2
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000103C2 File Offset: 0x0000E5C2
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector2>(channel, 2, uvs, start, length, flags);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000103D4 File Offset: 0x0000E5D4
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector3> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000103E4 File Offset: 0x0000E5E4
		public void SetUVs(int channel, List<Vector3> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector3>(channel, 3, uvs, start, length, flags);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000103F6 File Offset: 0x0000E5F6
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00010406 File Offset: 0x0000E606
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector4>(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00010418 File Offset: 0x0000E618
		private void SetUvsImpl(int uvIndex, int dim, Array uvs, int arrayStart, int arraySize, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("uvIndex", uvIndex, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.SetSizedArrayForChannel(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, NoAllocHelpers.SafeLength(uvs), arrayStart, arraySize, flags);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00010467 File Offset: 0x0000E667
		public void SetUVs(int channel, Vector2[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0001047A File Offset: 0x0000E67A
		public void SetUVs(int channel, Vector3[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0001048D File Offset: 0x0000E68D
		public void SetUVs(int channel, Vector4[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000104A0 File Offset: 0x0000E6A0
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector2[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000104B0 File Offset: 0x0000E6B0
		public void SetUVs(int channel, Vector2[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 2, uvs, start, length, flags);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000104C2 File Offset: 0x0000E6C2
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector3[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000104D2 File Offset: 0x0000E6D2
		public void SetUVs(int channel, Vector3[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 3, uvs, start, length, flags);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000104E4 File Offset: 0x0000E6E4
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector4[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000104F4 File Offset: 0x0000E6F4
		public void SetUVs(int channel, Vector4[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00010506 File Offset: 0x0000E706
		public void SetUVs<T>(int channel, NativeArray<T> uvs) where T : struct
		{
			this.SetUVs<T>(channel, uvs, 0, uvs.Length);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0001051A File Offset: 0x0000E71A
		[ExcludeFromDocs]
		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length) where T : struct
		{
			this.SetUVs<T>(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0001052C File Offset: 0x0000E72C
		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = channel < 0 || channel > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag2 = (num & 3) != 0;
			if (flag2)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is multiple of 4 bytes in size");
			}
			int num2 = num / 4;
			bool flag3 = num2 < 1 || num2 > 4;
			if (flag3)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is 1..4 floats in size");
			}
			this.SetSizedNativeArrayForChannel(Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, num2, (IntPtr)uvs.GetUnsafeReadOnlyPtr<T>(), uvs.Length, start, length, flags);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000105C0 File Offset: 0x0000E7C0
		private void GetUVsImpl<T>(int uvIndex, List<T> uvs, int dim)
		{
			bool flag = uvs == null;
			if (flag)
			{
				throw new ArgumentNullException("The result uvs list cannot be null.", "uvs");
			}
			bool flag2 = uvIndex < 0 || uvIndex > 7;
			if (flag2)
			{
				throw new IndexOutOfRangeException("The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.GetListForChannel<T>(uvs, this.vertexCount, Mesh.GetUVChannel(uvIndex), dim);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00010615 File Offset: 0x0000E815
		public void GetUVs(int channel, List<Vector2> uvs)
		{
			this.GetUVsImpl<Vector2>(channel, uvs, 2);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00010622 File Offset: 0x0000E822
		public void GetUVs(int channel, List<Vector3> uvs)
		{
			this.GetUVsImpl<Vector3>(channel, uvs, 3);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0001062F File Offset: 0x0000E82F
		public void GetUVs(int channel, List<Vector4> uvs)
		{
			this.GetUVsImpl<Vector4>(channel, uvs, 4);
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0001063C File Offset: 0x0000E83C
		public int vertexAttributeCount
		{
			get
			{
				return this.GetVertexAttributeCountImpl();
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00010654 File Offset: 0x0000E854
		public VertexAttributeDescriptor[] GetVertexAttributes()
		{
			return (VertexAttributeDescriptor[])this.GetVertexAttributesAlloc();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00010674 File Offset: 0x0000E874
		public int GetVertexAttributes(VertexAttributeDescriptor[] attributes)
		{
			return this.GetVertexAttributesArray(attributes);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00010690 File Offset: 0x0000E890
		public int GetVertexAttributes(List<VertexAttributeDescriptor> attributes)
		{
			return this.GetVertexAttributesList(attributes);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000106AC File Offset: 0x0000E8AC
		public void SetVertexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferData(stream, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00010748 File Offset: 0x0000E948
		public void SetVertexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException("Array passed to SetVertexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public void SetVertexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetVertexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, NoAllocHelpers.ExtractArrayFromList(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000108BC File Offset: 0x0000EABC
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh mesh)
		{
			return new Mesh.MeshDataArray(mesh);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh[] meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			return new Mesh.MeshDataArray(meshes, meshes.Length);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00010908 File Offset: 0x0000EB08
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(List<Mesh> meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			return new Mesh.MeshDataArray(NoAllocHelpers.ExtractArrayFromListT<Mesh>(meshes), meshes.Count);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00010944 File Offset: 0x0000EB44
		public static Mesh.MeshDataArray AllocateWritableMeshData(int meshCount)
		{
			return new Mesh.MeshDataArray(meshCount);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh mesh, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh", "Mesh is null");
			}
			bool flag2 = data.Length != 1;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length must be 1 to apply to one mesh, was {1}", "MeshDataArray", data.Length));
			}
			data.ApplyToMeshAndDispose(mesh, flags);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh[] meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			bool flag2 = data.Length != meshes.Length;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes array length ({2})", "MeshDataArray", data.Length, meshes.Length));
			}
			data.ApplyToMeshesAndDispose(meshes, flags);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00010A30 File Offset: 0x0000EC30
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, List<Mesh> meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			bool flag2 = data.Length != meshes.Count;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes list length ({2})", "MeshDataArray", data.Length, meshes.Count));
			}
			data.ApplyToMeshesAndDispose(NoAllocHelpers.ExtractArrayFromListT<Mesh>(meshes), flags);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00010AA7 File Offset: 0x0000ECA7
		private void PrintErrorCantAccessIndices()
		{
			Debug.LogError(string.Format("Not allowed to access triangles/indices on mesh '{0}' (isReadable is false; Read/Write must be enabled in import settings)", base.name));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		private bool CheckCanAccessSubmesh(int submesh, bool errorAboutTriangles)
		{
			bool flag = !this.canAccess;
			bool flag2;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
				flag2 = false;
			}
			else
			{
				bool flag3 = submesh < 0 || submesh >= this.subMeshCount;
				if (flag3)
				{
					Debug.LogError(string.Format("Failed getting {0}. Submesh index is out of bounds.", errorAboutTriangles ? "triangles" : "indices"), this);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00010B28 File Offset: 0x0000ED28
		private bool CheckCanAccessSubmeshTriangles(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, true);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00010B44 File Offset: 0x0000ED44
		private bool CheckCanAccessSubmeshIndices(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, false);
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00010B60 File Offset: 0x0000ED60
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00010B94 File Offset: 0x0000ED94
		public int[] triangles
		{
			get
			{
				bool canAccess = this.canAccess;
				int[] array;
				if (canAccess)
				{
					array = this.GetTrianglesImpl(-1, true);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
					array = new int[0];
				}
				return array;
			}
			set
			{
				bool canAccess = this.canAccess;
				if (canAccess)
				{
					this.SetTrianglesImpl(-1, IndexFormat.UInt32, value, NoAllocHelpers.SafeLength(value), 0, NoAllocHelpers.SafeLength(value), true, 0);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
				}
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public int[] GetTriangles(int submesh)
		{
			return this.GetTriangles(submesh, true);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public int[] GetTriangles(int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshTriangles(submesh) ? this.GetTrianglesImpl(submesh, applyBaseVertex) : new int[0];
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00010C17 File Offset: 0x0000EE17
		public void GetTriangles(List<int> triangles, int submesh)
		{
			this.GetTriangles(triangles, submesh, true);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00010C24 File Offset: 0x0000EE24
		public void GetTriangles(List<int> triangles, int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("The result triangles list cannot be null.", "triangles");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(triangles, (int)(3U * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<int>(triangles), submesh, applyBaseVertex);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public void GetTriangles(List<ushort> triangles, int submesh, bool applyBaseVertex = true)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("The result triangles list cannot be null.", "triangles");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(triangles, (int)(3U * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromListT<ushort>(triangles), submesh, applyBaseVertex);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		[ExcludeFromDocs]
		public int[] GetIndices(int submesh)
		{
			return this.GetIndices(submesh, true);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00010D10 File Offset: 0x0000EF10
		public int[] GetIndices(int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshIndices(submesh) ? this.GetIndicesImpl(submesh, applyBaseVertex) : new int[0];
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00010D3B File Offset: 0x0000EF3B
		[ExcludeFromDocs]
		public void GetIndices(List<int> indices, int submesh)
		{
			this.GetIndices(indices, submesh, true);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00010D48 File Offset: 0x0000EF48
		public void GetIndices(List<int> indices, int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("The result indices list cannot be null.", "indices");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<int>(indices), submesh, applyBaseVertex);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public void GetIndices(List<ushort> indices, int submesh, bool applyBaseVertex = true)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("The result indices list cannot be null.", "indices");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromListT<ushort>(indices), submesh, applyBaseVertex);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00010E18 File Offset: 0x0000F018
		public void SetIndexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		public void SetIndexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
				if (flag2)
				{
					throw new ArgumentException("Array passed to SetIndexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00010F3C File Offset: 0x0000F13C
		public void SetIndexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
				if (flag2)
				{
					throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetIndexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(NoAllocHelpers.ExtractArrayFromList(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public uint GetIndexStart(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexStartImpl(submesh);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0001102C File Offset: 0x0000F22C
		public uint GetIndexCount(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexCountImpl(submesh);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00011068 File Offset: 0x0000F268
		public uint GetBaseVertex(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetBaseVertexImpl(submesh);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000110A4 File Offset: 0x0000F2A4
		private void CheckIndicesArrayRange(int valuesLength, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start can't be negative.");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", length, "Mesh indices array length can't be negative.");
			}
			bool flag3 = start >= valuesLength && length != 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start is outside of array size.");
			}
			bool flag4 = start + length > valuesLength;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length", start + length, "Mesh indices array start+count is outside of array size.");
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00011138 File Offset: 0x0000F338
		private void SetTrianglesImpl(int submesh, IndexFormat indicesFormat, Array triangles, int trianglesArrayLength, int start, int length, bool calculateBounds, int baseVertex)
		{
			this.CheckIndicesArrayRange(trianglesArrayLength, start, length);
			this.SetIndicesImpl(submesh, MeshTopology.Triangles, indicesFormat, triangles, start, length, calculateBounds, baseVertex);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00011166 File Offset: 0x0000F366
		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00011174 File Offset: 0x0000F374
		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00011182 File Offset: 0x0000F382
		public void SetTriangles(int[] triangles, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00011198 File Offset: 0x0000F398
		public void SetTriangles(int[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000111C9 File Offset: 0x0000F3C9
		public void SetTriangles(ushort[] triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000111E0 File Offset: 0x0000F3E0
		public void SetTriangles(ushort[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00011211 File Offset: 0x0000F411
		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0001121F File Offset: 0x0000F41F
		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0001122D File Offset: 0x0000F42D
		public void SetTriangles(List<int> triangles, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<int>(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00011244 File Offset: 0x0000F444
		public void SetTriangles(List<int> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, NoAllocHelpers.ExtractArrayFromList(triangles), NoAllocHelpers.SafeLength<int>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0001127A File Offset: 0x0000F47A
		public void SetTriangles(List<ushort> triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<ushort>(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00011290 File Offset: 0x0000F490
		public void SetTriangles(List<ushort> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, NoAllocHelpers.ExtractArrayFromList(triangles), NoAllocHelpers.SafeLength<ushort>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000112C6 File Offset: 0x0000F4C6
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh)
		{
			this.SetIndices(indices, topology, submesh, true, 0);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000112D5 File Offset: 0x0000F4D5
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, bool calculateBounds)
		{
			this.SetIndices(indices, topology, submesh, calculateBounds, 0);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000112E5 File Offset: 0x0000F4E5
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00011300 File Offset: 0x0000F500
		public void SetIndices(int[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001133E File Offset: 0x0000F53E
		public void SetIndices(ushort[] indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00011358 File Offset: 0x0000F558
		public void SetIndices(ushort[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00011396 File Offset: 0x0000F596
		public void SetIndices<T>(NativeArray<T> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			this.SetIndices<T>(indices, 0, indices.Length, topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public void SetIndices<T>(NativeArray<T> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int num = UnsafeUtility.SizeOf<T>();
				bool flag2 = num != 2 && num != 4;
				if (flag2)
				{
					throw new ArgumentException("SetIndices with NativeArray should use type is 2 or 4 bytes in size");
				}
				this.CheckIndicesArrayRange(indices.Length, indicesStart, indicesLength);
				this.SetIndicesNativeArrayImpl(submesh, topology, (num == 2) ? IndexFormat.UInt16 : IndexFormat.UInt32, (IntPtr)indices.GetUnsafeReadOnlyPtr<T>(), indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00011423 File Offset: 0x0000F623
		public void SetIndices(List<int> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<int>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0001143C File Offset: 0x0000F63C
		public void SetIndices(List<int> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				Array array = NoAllocHelpers.ExtractArrayFromList(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<int>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, array, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00011481 File Offset: 0x0000F681
		public void SetIndices(List<ushort> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<ushort>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0001149C File Offset: 0x0000F69C
		public void SetIndices(List<ushort> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				Array array = NoAllocHelpers.ExtractArrayFromList(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<ushort>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, array, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public void GetBindposes(List<Matrix4x4> bindposes)
		{
			bool flag = bindposes == null;
			if (flag)
			{
				throw new ArgumentNullException("The result bindposes list cannot be null.", "bindposes");
			}
			NoAllocHelpers.EnsureListElemCount<Matrix4x4>(bindposes, this.GetBindposeCount());
			this.GetBindposesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(bindposes));
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00011524 File Offset: 0x0000F724
		public void GetBoneWeights(List<BoneWeight> boneWeights)
		{
			bool flag = boneWeights == null;
			if (flag)
			{
				throw new ArgumentNullException("The result boneWeights list cannot be null.", "boneWeights");
			}
			bool flag2 = this.HasBoneWeights();
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<BoneWeight>(boneWeights, this.vertexCount);
			}
			this.GetBoneWeightsNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT<BoneWeight>(boneWeights));
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00011570 File Offset: 0x0000F770
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00011588 File Offset: 0x0000F788
		public BoneWeight[] boneWeights
		{
			get
			{
				return this.GetBoneWeightsImpl();
			}
			set
			{
				this.SetBoneWeightsImpl(value);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00011593 File Offset: 0x0000F793
		public void Clear([DefaultValue("true")] bool keepVertexLayout)
		{
			this.ClearImpl(keepVertexLayout);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001159E File Offset: 0x0000F79E
		[ExcludeFromDocs]
		public void Clear()
		{
			this.ClearImpl(true);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000115A9 File Offset: 0x0000F7A9
		[ExcludeFromDocs]
		public void RecalculateBounds()
		{
			this.RecalculateBounds(MeshUpdateFlags.Default);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000115B4 File Offset: 0x0000F7B4
		[ExcludeFromDocs]
		public void RecalculateNormals()
		{
			this.RecalculateNormals(MeshUpdateFlags.Default);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000115BF File Offset: 0x0000F7BF
		[ExcludeFromDocs]
		public void RecalculateTangents()
		{
			this.RecalculateTangents(MeshUpdateFlags.Default);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000115CC File Offset: 0x0000F7CC
		public void RecalculateBounds([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateBoundsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateBounds() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00011604 File Offset: 0x0000F804
		public void RecalculateNormals([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateNormalsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateNormals() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001163C File Offset: 0x0000F83C
		public void RecalculateTangents([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateTangentsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateTangents() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00011674 File Offset: 0x0000F874
		public void MarkDynamic()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.MarkDynamicImpl();
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00011694 File Offset: 0x0000F894
		public void UploadMeshData(bool markNoLongerReadable)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.UploadMeshDataImpl(markNoLongerReadable);
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public void Optimize()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call Optimize() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000116EC File Offset: 0x0000F8EC
		public void OptimizeIndexBuffers()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeIndexBuffersImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeIndexBuffers() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00011724 File Offset: 0x0000F924
		public void OptimizeReorderVertexBuffer()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeReorderVertexBufferImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeReorderVertexBuffer() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001175C File Offset: 0x0000F95C
		public MeshTopology GetTopology(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			MeshTopology meshTopology;
			if (flag)
			{
				Debug.LogError("Failed getting topology. Submesh index is out of bounds.", this);
				meshTopology = MeshTopology.Triangles;
			}
			else
			{
				meshTopology = this.GetTopologyImpl(submesh);
			}
			return meshTopology;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001179D File Offset: 0x0000F99D
		public void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices, [DefaultValue("false")] bool hasLightmapData)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, hasLightmapData);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000117AC File Offset: 0x0000F9AC
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, false);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000117BA File Offset: 0x0000F9BA
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, true, false);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000117C8 File Offset: 0x0000F9C8
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			this.CombineMeshesImpl(combine, true, true, false);
		}

		// Token: 0x06000D36 RID: 3382
		[MethodImpl(4096)]
		private extern void GetVertexAttribute_Injected(int index, out VertexAttributeDescriptor ret);

		// Token: 0x06000D37 RID: 3383
		[MethodImpl(4096)]
		private extern void SetSubMesh_Injected(int index, ref SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default);

		// Token: 0x06000D38 RID: 3384
		[MethodImpl(4096)]
		private extern void GetSubMesh_Injected(int index, out SubMeshDescriptor ret);

		// Token: 0x06000D39 RID: 3385
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000D3A RID: 3386
		[MethodImpl(4096)]
		private extern void set_bounds_Injected(ref Bounds value);

		// Token: 0x02000145 RID: 325
		[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
		[StaticAccessor("MeshDataBindings", StaticAccessorType.DoubleColon)]
		public struct MeshData
		{
			// Token: 0x06000D3B RID: 3387
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern bool HasVertexAttribute(IntPtr self, VertexAttribute attr);

			// Token: 0x06000D3C RID: 3388
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern int GetVertexAttributeDimension(IntPtr self, VertexAttribute attr);

			// Token: 0x06000D3D RID: 3389
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern VertexAttributeFormat GetVertexAttributeFormat(IntPtr self, VertexAttribute attr);

			// Token: 0x06000D3E RID: 3390
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern int GetVertexCount(IntPtr self);

			// Token: 0x06000D3F RID: 3391
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern int GetVertexBufferCount(IntPtr self);

			// Token: 0x06000D40 RID: 3392
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern IntPtr GetVertexDataPtr(IntPtr self, int stream);

			// Token: 0x06000D41 RID: 3393
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern ulong GetVertexDataSize(IntPtr self, int stream);

			// Token: 0x06000D42 RID: 3394
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern void CopyAttributeIntoPtr(IntPtr self, VertexAttribute attr, VertexAttributeFormat format, int dim, IntPtr dst);

			// Token: 0x06000D43 RID: 3395
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern void CopyIndicesIntoPtr(IntPtr self, int submesh, bool applyBaseVertex, int dstStride, IntPtr dst);

			// Token: 0x06000D44 RID: 3396
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern IndexFormat GetIndexFormat(IntPtr self);

			// Token: 0x06000D45 RID: 3397
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern int GetIndexCount(IntPtr self, int submesh);

			// Token: 0x06000D46 RID: 3398
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern IntPtr GetIndexDataPtr(IntPtr self);

			// Token: 0x06000D47 RID: 3399
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern ulong GetIndexDataSize(IntPtr self);

			// Token: 0x06000D48 RID: 3400
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern int GetSubMeshCount(IntPtr self);

			// Token: 0x06000D49 RID: 3401 RVA: 0x000117D8 File Offset: 0x0000F9D8
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static SubMeshDescriptor GetSubMesh(IntPtr self, int index)
			{
				SubMeshDescriptor subMeshDescriptor;
				Mesh.MeshData.GetSubMesh_Injected(self, index, out subMeshDescriptor);
				return subMeshDescriptor;
			}

			// Token: 0x06000D4A RID: 3402
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			[MethodImpl(4096)]
			private static extern void SetVertexBufferParamsImpl(IntPtr self, int vertexCount, params VertexAttributeDescriptor[] attributes);

			// Token: 0x06000D4B RID: 3403
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern void SetIndexBufferParamsImpl(IntPtr self, int indexCount, IndexFormat indexFormat);

			// Token: 0x06000D4C RID: 3404
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			private static extern void SetSubMeshCount(IntPtr self, int count);

			// Token: 0x06000D4D RID: 3405 RVA: 0x000117EF File Offset: 0x0000F9EF
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static void SetSubMeshImpl(IntPtr self, int index, SubMeshDescriptor desc, MeshUpdateFlags flags)
			{
				Mesh.MeshData.SetSubMeshImpl_Injected(self, index, ref desc, flags);
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000117FC File Offset: 0x0000F9FC
			public int vertexCount
			{
				get
				{
					return Mesh.MeshData.GetVertexCount(this.m_Ptr);
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0001181C File Offset: 0x0000FA1C
			public int vertexBufferCount
			{
				get
				{
					return Mesh.MeshData.GetVertexBufferCount(this.m_Ptr);
				}
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x0001183C File Offset: 0x0000FA3C
			public bool HasVertexAttribute(VertexAttribute attr)
			{
				return Mesh.MeshData.HasVertexAttribute(this.m_Ptr, attr);
			}

			// Token: 0x06000D51 RID: 3409 RVA: 0x0001185C File Offset: 0x0000FA5C
			public int GetVertexAttributeDimension(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeDimension(this.m_Ptr, attr);
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x0001187C File Offset: 0x0000FA7C
			public VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeFormat(this.m_Ptr, attr);
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x0001189A File Offset: 0x0000FA9A
			public void GetVertices(NativeArray<Vector3> outVertices)
			{
				this.CopyAttributeInto<Vector3>(outVertices, VertexAttribute.Position, VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x000118A8 File Offset: 0x0000FAA8
			public void GetNormals(NativeArray<Vector3> outNormals)
			{
				this.CopyAttributeInto<Vector3>(outNormals, VertexAttribute.Normal, VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x000118B6 File Offset: 0x0000FAB6
			public void GetTangents(NativeArray<Vector4> outTangents)
			{
				this.CopyAttributeInto<Vector4>(outTangents, VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x000118C4 File Offset: 0x0000FAC4
			public void GetColors(NativeArray<Color> outColors)
			{
				this.CopyAttributeInto<Color>(outColors, VertexAttribute.Color, VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x000118D2 File Offset: 0x0000FAD2
			public void GetColors(NativeArray<Color32> outColors)
			{
				this.CopyAttributeInto<Color32>(outColors, VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x000118E0 File Offset: 0x0000FAE0
			public void GetUVs(int channel, NativeArray<Vector2> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector2>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 2);
			}

			// Token: 0x06000D59 RID: 3417 RVA: 0x00011924 File Offset: 0x0000FB24
			public void GetUVs(int channel, NativeArray<Vector3> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector3>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x00011968 File Offset: 0x0000FB68
			public void GetUVs(int channel, NativeArray<Vector4> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector4>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x000119AC File Offset: 0x0000FBAC
			public unsafe NativeArray<T> GetVertexData<T>([DefaultValue("0")] int stream = 0) where T : struct
			{
				bool flag = stream < 0 || stream >= this.vertexBufferCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} out of bounds, should be below {1} but was {2}", "stream", this.vertexBufferCount, stream));
				}
				ulong vertexDataSize = Mesh.MeshData.GetVertexDataSize(this.m_Ptr, stream);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag2 = vertexDataSize % num > 0UL;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the vertex buffer. Mesh vertex buffer size is {1} which is not a multiple of type size {2}", "GetVertexData", vertexDataSize, num));
				}
				ulong num2 = vertexDataSize / num;
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetVertexDataPtr(this.m_Ptr, stream), (int)num2, Allocator.None);
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x00011A64 File Offset: 0x0000FC64
			private void CopyAttributeInto<T>(NativeArray<T> buffer, VertexAttribute channel, VertexAttributeFormat format, int dim) where T : struct
			{
				bool flag = !this.HasVertexAttribute(channel);
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh data does not have {0} vertex component", channel));
				}
				bool flag2 = buffer.Length < this.vertexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", this.vertexCount, buffer.Length));
				}
				Mesh.MeshData.CopyAttributeIntoPtr(this.m_Ptr, channel, format, dim, (IntPtr)buffer.GetUnsafePtr<T>());
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x00011AEB File Offset: 0x0000FCEB
			public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
			{
				Mesh.MeshData.SetVertexBufferParamsImpl(this.m_Ptr, vertexCount, attributes);
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x00011AFC File Offset: 0x0000FCFC
			public void SetIndexBufferParams(int indexCount, IndexFormat format)
			{
				Mesh.MeshData.SetIndexBufferParamsImpl(this.m_Ptr, indexCount, format);
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00011B10 File Offset: 0x0000FD10
			public IndexFormat indexFormat
			{
				get
				{
					return Mesh.MeshData.GetIndexFormat(this.m_Ptr);
				}
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x00011B30 File Offset: 0x0000FD30
			public void GetIndices(NativeArray<ushort> outIndices, int submesh, [DefaultValue("true")] bool applyBaseVertex = true)
			{
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 2, (IntPtr)outIndices.GetUnsafePtr<ushort>());
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x00011BD0 File Offset: 0x0000FDD0
			public void GetIndices(NativeArray<int> outIndices, int submesh, [DefaultValue("true")] bool applyBaseVertex = true)
			{
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 4, (IntPtr)outIndices.GetUnsafePtr<int>());
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x00011C70 File Offset: 0x0000FE70
			public unsafe NativeArray<T> GetIndexData<T>() where T : struct
			{
				ulong indexDataSize = Mesh.MeshData.GetIndexDataSize(this.m_Ptr);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag = indexDataSize % num > 0UL;
				if (flag)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the index buffer. Mesh index buffer size is {1} which is not a multiple of type size {2}", "GetIndexData", indexDataSize, num));
				}
				ulong num2 = indexDataSize / num;
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetIndexDataPtr(this.m_Ptr), (int)num2, Allocator.None);
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00011CE4 File Offset: 0x0000FEE4
			// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00011D01 File Offset: 0x0000FF01
			public int subMeshCount
			{
				get
				{
					return Mesh.MeshData.GetSubMeshCount(this.m_Ptr);
				}
				set
				{
					Mesh.MeshData.SetSubMeshCount(this.m_Ptr, value);
				}
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x00011D14 File Offset: 0x0000FF14
			public SubMeshDescriptor GetSubMesh(int index)
			{
				return Mesh.MeshData.GetSubMesh(this.m_Ptr, index);
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x00011D32 File Offset: 0x0000FF32
			public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
			{
				Mesh.MeshData.SetSubMeshImpl(this.m_Ptr, index, desc, flags);
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x00002EC3 File Offset: 0x000010C3
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckReadAccess()
			{
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x00002EC3 File Offset: 0x000010C3
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckWriteAccess()
			{
			}

			// Token: 0x06000D69 RID: 3433
			[MethodImpl(4096)]
			private static extern void GetSubMesh_Injected(IntPtr self, int index, out SubMeshDescriptor ret);

			// Token: 0x06000D6A RID: 3434
			[MethodImpl(4096)]
			private static extern void SetSubMeshImpl_Injected(IntPtr self, int index, ref SubMeshDescriptor desc, MeshUpdateFlags flags);

			// Token: 0x0400041B RID: 1051
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_Ptr;
		}

		// Token: 0x02000146 RID: 326
		[NativeContainer]
		[NativeContainerSupportsMinMaxWriteRestriction]
		[StaticAccessor("MeshDataArrayBindings", StaticAccessorType.DoubleColon)]
		public struct MeshDataArray : IDisposable
		{
			// Token: 0x06000D6B RID: 3435
			[MethodImpl(4096)]
			private unsafe static extern void AcquireReadOnlyMeshData([NotNull] Mesh mesh, IntPtr* datas);

			// Token: 0x06000D6C RID: 3436
			[MethodImpl(4096)]
			private unsafe static extern void AcquireReadOnlyMeshDatas([NotNull] Mesh[] meshes, IntPtr* datas, int count);

			// Token: 0x06000D6D RID: 3437
			[MethodImpl(4096)]
			private unsafe static extern void ReleaseMeshDatas(IntPtr* datas, int count);

			// Token: 0x06000D6E RID: 3438
			[MethodImpl(4096)]
			private unsafe static extern void CreateNewMeshDatas(IntPtr* datas, int count);

			// Token: 0x06000D6F RID: 3439
			[NativeThrows]
			[MethodImpl(4096)]
			private unsafe static extern void ApplyToMeshesImpl([NotNull] Mesh[] meshes, IntPtr* datas, int count, MeshUpdateFlags flags);

			// Token: 0x06000D70 RID: 3440
			[NativeThrows]
			[MethodImpl(4096)]
			private static extern void ApplyToMeshImpl([NotNull] Mesh mesh, IntPtr data, MeshUpdateFlags flags);

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00011D44 File Offset: 0x0000FF44
			public int Length
			{
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x170002A8 RID: 680
			public unsafe Mesh.MeshData this[int index]
			{
				get
				{
					Mesh.MeshData meshData;
					meshData.m_Ptr = this.m_Ptrs[(IntPtr)index * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					return meshData;
				}
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x00011D78 File Offset: 0x0000FF78
			public unsafe void Dispose()
			{
				bool flag = this.m_Length != 0;
				if (flag)
				{
					Mesh.MeshDataArray.ReleaseMeshDatas(this.m_Ptrs, this.m_Length);
					UnsafeUtility.Free((void*)this.m_Ptrs, Allocator.Persistent);
				}
				this.m_Ptrs = null;
				this.m_Length = 0;
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00011DC4 File Offset: 0x0000FFC4
			internal unsafe void ApplyToMeshAndDispose(Mesh mesh, MeshUpdateFlags flags)
			{
				bool flag = !mesh.canAccess;
				if (flag)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				Mesh.MeshDataArray.ApplyToMeshImpl(mesh, *this.m_Ptrs, flags);
				this.Dispose();
			}

			// Token: 0x06000D75 RID: 3445 RVA: 0x00011E10 File Offset: 0x00010010
			internal void ApplyToMeshesAndDispose(Mesh[] meshes, MeshUpdateFlags flags)
			{
				for (int i = 0; i < this.m_Length; i++)
				{
					Mesh mesh = meshes[i];
					bool flag = mesh == null;
					if (flag)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag2 = !mesh.canAccess;
					if (flag2)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				Mesh.MeshDataArray.ApplyToMeshesImpl(meshes, this.m_Ptrs, this.m_Length, flags);
				this.Dispose();
			}

			// Token: 0x06000D76 RID: 3446 RVA: 0x00011EA8 File Offset: 0x000100A8
			internal unsafe MeshDataArray(Mesh mesh)
			{
				bool flag = mesh == null;
				if (flag)
				{
					throw new ArgumentNullException("mesh", "Mesh is null");
				}
				bool flag2 = !mesh.canAccess;
				if (flag2)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				this.m_Length = 1;
				int num = UnsafeUtility.SizeOf<IntPtr>();
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.AcquireReadOnlyMeshData(mesh, this.m_Ptrs);
			}

			// Token: 0x06000D77 RID: 3447 RVA: 0x00011F28 File Offset: 0x00010128
			internal unsafe MeshDataArray(Mesh[] meshes, int meshesCount)
			{
				bool flag = meshes.Length < meshesCount;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Meshes array size ({0}) is smaller than meshes count ({1})", meshes.Length, meshesCount));
				}
				for (int i = 0; i < meshesCount; i++)
				{
					Mesh mesh = meshes[i];
					bool flag2 = mesh == null;
					if (flag2)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag3 = !mesh.canAccess;
					if (flag3)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.AcquireReadOnlyMeshDatas(meshes, this.m_Ptrs, meshesCount);
			}

			// Token: 0x06000D78 RID: 3448 RVA: 0x00011FFC File Offset: 0x000101FC
			internal unsafe MeshDataArray(int meshesCount)
			{
				bool flag = meshesCount < 0;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh count can not be negative (was {0})", meshesCount));
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.CreateNewMeshDatas(this.m_Ptrs, meshesCount);
			}

			// Token: 0x06000D79 RID: 3449 RVA: 0x00002EC3 File Offset: 0x000010C3
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckElementReadAccess(int index)
			{
			}

			// Token: 0x0400041C RID: 1052
			[NativeDisableUnsafePtrRestriction]
			private unsafe IntPtr* m_Ptrs;

			// Token: 0x0400041D RID: 1053
			internal int m_Length;
		}
	}
}
