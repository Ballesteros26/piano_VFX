using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/Physics/Rigidbody.h")]
	[RequireComponent(typeof(Transform))]
	public class Rigidbody : Component
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_velocity_Injected(ref value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B00 File Offset: 0x00000D00
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002B16 File Offset: 0x00000D16
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 vector;
				this.get_angularVelocity_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_angularVelocity_Injected(ref value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600007D RID: 125
		// (set) Token: 0x0600007E RID: 126
		public extern float drag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		public extern float angularDrag
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		public extern float mass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000083 RID: 131
		[MethodImpl(4096)]
		public extern void SetDensity(float density);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000084 RID: 132
		// (set) Token: 0x06000085 RID: 133
		public extern bool useGravity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000086 RID: 134
		// (set) Token: 0x06000087 RID: 135
		public extern float maxDepenetrationVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000088 RID: 136
		// (set) Token: 0x06000089 RID: 137
		public extern bool isKinematic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600008A RID: 138
		// (set) Token: 0x0600008B RID: 139
		public extern bool freezeRotation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600008C RID: 140
		// (set) Token: 0x0600008D RID: 141
		public extern RigidbodyConstraints constraints
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600008E RID: 142
		// (set) Token: 0x0600008F RID: 143
		public extern CollisionDetectionMode collisionDetectionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002B20 File Offset: 0x00000D20
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002B36 File Offset: 0x00000D36
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 vector;
				this.get_centerOfMass_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_centerOfMass_Injected(ref value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002B40 File Offset: 0x00000D40
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 vector;
				this.get_worldCenterOfMass_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002B6E File Offset: 0x00000D6E
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_inertiaTensorRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_inertiaTensorRotation_Injected(ref value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002B78 File Offset: 0x00000D78
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002B8E File Offset: 0x00000D8E
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 vector;
				this.get_inertiaTensor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_inertiaTensor_Injected(ref value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		public extern bool detectCollisions
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002B98 File Offset: 0x00000D98
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002BAE File Offset: 0x00000DAE
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				this.get_position_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_position_Injected(ref value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002BCE File Offset: 0x00000DCE
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				this.get_rotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_rotation_Injected(ref value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600009D RID: 157
		// (set) Token: 0x0600009E RID: 158
		public extern RigidbodyInterpolation interpolation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600009F RID: 159
		// (set) Token: 0x060000A0 RID: 160
		public extern int solverIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000A1 RID: 161
		// (set) Token: 0x060000A2 RID: 162
		public extern float sleepThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		public extern float maxAngularVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public void MovePosition(Vector3 position)
		{
			this.MovePosition_Injected(ref position);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public void MoveRotation(Quaternion rot)
		{
			this.MoveRotation_Injected(ref rot);
		}

		// Token: 0x060000A7 RID: 167
		[MethodImpl(4096)]
		public extern void Sleep();

		// Token: 0x060000A8 RID: 168
		[MethodImpl(4096)]
		public extern bool IsSleeping();

		// Token: 0x060000A9 RID: 169
		[MethodImpl(4096)]
		public extern void WakeUp();

		// Token: 0x060000AA RID: 170
		[MethodImpl(4096)]
		public extern void ResetCenterOfMass();

		// Token: 0x060000AB RID: 171
		[MethodImpl(4096)]
		public extern void ResetInertiaTensor();

		// Token: 0x060000AC RID: 172 RVA: 0x00002BEC File Offset: 0x00000DEC
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			Vector3 vector;
			this.GetRelativePointVelocity_Injected(ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002C04 File Offset: 0x00000E04
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			Vector3 vector;
			this.GetPointVelocity_Injected(ref worldPoint, out vector);
			return vector;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000AE RID: 174
		// (set) Token: 0x060000AF RID: 175
		public extern int solverVelocityIterations
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B0 RID: 176
		// (set) Token: 0x060000B1 RID: 177
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.")]
		public extern float sleepVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000B2 RID: 178
		// (set) Token: 0x060000B3 RID: 179
		[Obsolete("The sleepAngularVelocity is no longer supported. Set Use sleepThreshold to specify energy.")]
		public extern float sleepAngularVelocity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002C1C File Offset: 0x00000E1C
		[Obsolete("Use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002C28 File Offset: 0x00000E28
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("Cone friction is no longer supported.")]
		public bool useConeFriction
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C54 File Offset: 0x00000E54
		[Obsolete("Please use Rigidbody.solverIterations instead. (UnityUpgradable) -> solverIterations")]
		public int solverIterationCount
		{
			get
			{
				return this.solverIterations;
			}
			set
			{
				this.solverIterations = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C60 File Offset: 0x00000E60
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002C78 File Offset: 0x00000E78
		[Obsolete("Please use Rigidbody.solverVelocityIterations instead. (UnityUpgradable) -> solverVelocityIterations")]
		public int solverVelocityIterationCount
		{
			get
			{
				return this.solverVelocityIterations;
			}
			set
			{
				this.solverVelocityIterations = value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002C83 File Offset: 0x00000E83
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce_Injected(ref force, mode);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002C8E File Offset: 0x00000E8E
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002C9A File Offset: 0x00000E9A
		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002CAE File Offset: 0x00000EAE
		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			this.AddForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002CC1 File Offset: 0x00000EC1
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce_Injected(ref force, mode);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002CCC File Offset: 0x00000ECC
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public void AddRelativeForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002CEC File Offset: 0x00000EEC
		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			this.AddRelativeForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002CFF File Offset: 0x00000EFF
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque_Injected(ref torque, mode);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002D0A File Offset: 0x00000F0A
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002D16 File Offset: 0x00000F16
		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002D2A File Offset: 0x00000F2A
		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			this.AddTorque(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002D3D File Offset: 0x00000F3D
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque_Injected(ref torque, mode);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002D48 File Offset: 0x00000F48
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002D54 File Offset: 0x00000F54
		public void AddRelativeTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002D68 File Offset: 0x00000F68
		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			this.AddRelativeTorque(x, y, z, ForceMode.Force);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002D76 File Offset: 0x00000F76
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForceAtPosition_Injected(ref force, ref position, mode);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002D83 File Offset: 0x00000F83
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002D90 File Offset: 0x00000F90
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode)
		{
			this.AddExplosionForce_Injected(explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002DB0 File Offset: 0x00000FB0
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Force);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002DC3 File Offset: 0x00000FC3
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			this.Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002DFC File Offset: 0x00000FFC
		private RaycastHit SweepTest(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit)
		{
			RaycastHit raycastHit;
			this.SweepTest_Injected(ref direction, maxDistance, queryTriggerInteraction, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002E18 File Offset: 0x00001018
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag3;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				bool flag2 = false;
				hitInfo = this.SweepTest(vector, maxDistance, queryTriggerInteraction, ref flag2);
				flag3 = flag2;
			}
			else
			{
				hitInfo = default(RaycastHit);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002E6C File Offset: 0x0000106C
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return this.SweepTest(direction, out hitInfo, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002E88 File Offset: 0x00001088
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			return this.SweepTest(direction, out hitInfo, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002EA8 File Offset: 0x000010A8
		[NativeName("SweepTestAll")]
		private RaycastHit[] Internal_SweepTestAll(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction)
		{
			return this.Internal_SweepTestAll_Injected(ref direction, maxDistance, queryTriggerInteraction);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002EB4 File Offset: 0x000010B4
		public RaycastHit[] SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = this.Internal_SweepTestAll(vector, maxDistance, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002EF8 File Offset: 0x000010F8
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
		{
			return this.SweepTestAll(direction, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002F14 File Offset: 0x00001114
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			return this.SweepTestAll(direction, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060000DB RID: 219
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060000DC RID: 220
		[MethodImpl(4096)]
		private extern void set_velocity_Injected(ref Vector3 value);

		// Token: 0x060000DD RID: 221
		[MethodImpl(4096)]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		// Token: 0x060000DE RID: 222
		[MethodImpl(4096)]
		private extern void set_angularVelocity_Injected(ref Vector3 value);

		// Token: 0x060000DF RID: 223
		[MethodImpl(4096)]
		private extern void get_centerOfMass_Injected(out Vector3 ret);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(4096)]
		private extern void set_centerOfMass_Injected(ref Vector3 value);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(4096)]
		private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(4096)]
		private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(4096)]
		private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(4096)]
		private extern void get_inertiaTensor_Injected(out Vector3 ret);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(4096)]
		private extern void set_inertiaTensor_Injected(ref Vector3 value);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(4096)]
		private extern void get_position_Injected(out Vector3 ret);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(4096)]
		private extern void set_position_Injected(ref Vector3 value);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(4096)]
		private extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(4096)]
		private extern void set_rotation_Injected(ref Quaternion value);

		// Token: 0x060000EA RID: 234
		[MethodImpl(4096)]
		private extern void MovePosition_Injected(ref Vector3 position);

		// Token: 0x060000EB RID: 235
		[MethodImpl(4096)]
		private extern void MoveRotation_Injected(ref Quaternion rot);

		// Token: 0x060000EC RID: 236
		[MethodImpl(4096)]
		private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x060000ED RID: 237
		[MethodImpl(4096)]
		private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);

		// Token: 0x060000EE RID: 238
		[MethodImpl(4096)]
		private extern void AddForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000EF RID: 239
		[MethodImpl(4096)]
		private extern void AddRelativeForce_Injected(ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(4096)]
		private extern void AddTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(4096)]
		private extern void AddRelativeTorque_Injected(ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(4096)]
		private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(4096)]
		private extern void AddExplosionForce_Injected(float explosionForce, ref Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(4096)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(4096)]
		private extern void SweepTest_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit, out RaycastHit ret);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(4096)]
		private extern RaycastHit[] Internal_SweepTestAll_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);
	}
}
