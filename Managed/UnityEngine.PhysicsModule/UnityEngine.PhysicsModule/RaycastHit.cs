using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[UsedByNativeCode]
	[NativeHeader("Modules/Physics/RaycastHit.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/IRaycast.h")]
	public struct RaycastHit
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002838 File Offset: 0x00000A38
		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002874 File Offset: 0x00000A74
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002880 File Offset: 0x00000A80
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002898 File Offset: 0x00000A98
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000028A4 File Offset: 0x00000AA4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000028EE File Offset: 0x00000AEE
		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002900 File Offset: 0x00000B00
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002918 File Offset: 0x00000B18
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002924 File Offset: 0x00000B24
		public int triangleIndex
		{
			get
			{
				return (int)this.m_FaceID;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000293C File Offset: 0x00000B3C
		[FreeFunction]
		private static Vector2 CalculateRaycastTexCoord(Collider collider, Vector2 uv, Vector3 pos, uint face, int textcoord)
		{
			Vector2 vector;
			RaycastHit.CalculateRaycastTexCoord_Injected(collider, ref uv, ref pos, face, textcoord, out vector);
			return vector;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000295C File Offset: 0x00000B5C
		public Vector2 textureCoord
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000298C File Offset: 0x00000B8C
		public Vector2 textureCoord2
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000029BC File Offset: 0x00000BBC
		[Obsolete("Use textureCoord2 instead. (UnityUpgradable) -> textureCoord2")]
		public Vector2 textureCoord1
		{
			get
			{
				return this.textureCoord2;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000029D4 File Offset: 0x00000BD4
		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform transform;
				if (flag)
				{
					transform = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						transform = this.collider.transform;
					}
					else
					{
						transform = null;
					}
				}
				return transform;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002A20 File Offset: 0x00000C20
		public Rigidbody rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002A50 File Offset: 0x00000C50
		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 vector = RaycastHit.CalculateRaycastTexCoord(this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				bool flag = this.collider.GetComponent<Renderer>() != null;
				if (flag)
				{
					Vector4 lightmapScaleOffset = this.collider.GetComponent<Renderer>().lightmapScaleOffset;
					vector.x = vector.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
					vector.y = vector.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
				}
				return vector;
			}
		}

		// Token: 0x06000078 RID: 120
		[MethodImpl(4096)]
		private static extern void CalculateRaycastTexCoord_Injected(Collider collider, ref Vector2 uv, ref Vector3 pos, uint face, int textcoord, out Vector2 ret);

		// Token: 0x0400006A RID: 106
		[NativeName("point")]
		internal Vector3 m_Point;

		// Token: 0x0400006B RID: 107
		[NativeName("normal")]
		internal Vector3 m_Normal;

		// Token: 0x0400006C RID: 108
		[NativeName("faceID")]
		internal uint m_FaceID;

		// Token: 0x0400006D RID: 109
		[NativeName("distance")]
		internal float m_Distance;

		// Token: 0x0400006E RID: 110
		[NativeName("uv")]
		internal Vector2 m_UV;

		// Token: 0x0400006F RID: 111
		[NativeName("collider")]
		internal int m_Collider;
	}
}
