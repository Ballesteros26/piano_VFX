using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FB RID: 507
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformScriptBindings.h")]
	[NativeHeader("Runtime/Transform/Transform.h")]
	[NativeHeader("Configuration/UnityConfigure.h")]
	[RequiredByNativeCode]
	public class Transform : Component, IEnumerable
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x00024B86 File Offset: 0x00022D86
		protected Transform()
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00024B90 File Offset: 0x00022D90
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x00024BA6 File Offset: 0x00022DA6
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00024BB0 File Offset: 0x00022DB0
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x00024BC6 File Offset: 0x00022DC6
		public Vector3 localPosition
		{
			get
			{
				Vector3 vector;
				this.get_localPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_localPosition_Injected(ref value);
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00024BD0 File Offset: 0x00022DD0
		internal Vector3 GetLocalEulerAngles(RotationOrder order)
		{
			Vector3 vector;
			this.GetLocalEulerAngles_Injected(order, out vector);
			return vector;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00024BE7 File Offset: 0x00022DE7
		internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
		{
			this.SetLocalEulerAngles_Injected(ref euler, order);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00024BF2 File Offset: 0x00022DF2
		[NativeConditional("UNITY_EDITOR")]
		internal void SetLocalEulerHint(Vector3 euler)
		{
			this.SetLocalEulerHint_Injected(ref euler);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00024BFC File Offset: 0x00022DFC
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x00024C1C File Offset: 0x00022E1C
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00024C2C File Offset: 0x00022E2C
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x00024C4C File Offset: 0x00022E4C
		public Vector3 localEulerAngles
		{
			get
			{
				return this.localRotation.eulerAngles;
			}
			set
			{
				this.localRotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00024C5C File Offset: 0x00022E5C
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00024C7E File Offset: 0x00022E7E
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00024C94 File Offset: 0x00022E94
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x00024CB6 File Offset: 0x00022EB6
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00024CCC File Offset: 0x00022ECC
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x00024CEE File Offset: 0x00022EEE
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00024D00 File Offset: 0x00022F00
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x00024D16 File Offset: 0x00022F16
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00024D20 File Offset: 0x00022F20
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x00024D36 File Offset: 0x00022F36
		public Quaternion localRotation
		{
			get
			{
				Quaternion quaternion;
				this.get_localRotation_Injected(out quaternion);
				return quaternion;
			}
			set
			{
				this.set_localRotation_Injected(ref value);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00024D40 File Offset: 0x00022F40
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x00024D58 File Offset: 0x00022F58
		[NativeConditional("UNITY_EDITOR")]
		internal RotationOrder rotationOrder
		{
			get
			{
				return (RotationOrder)this.GetRotationOrderInternal();
			}
			set
			{
				this.SetRotationOrderInternal(value);
			}
		}

		// Token: 0x06001671 RID: 5745
		[NativeConditional("UNITY_EDITOR")]
		[NativeMethod("GetRotationOrder")]
		[MethodImpl(4096)]
		internal extern int GetRotationOrderInternal();

		// Token: 0x06001672 RID: 5746
		[NativeMethod("SetRotationOrder")]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		internal extern void SetRotationOrderInternal(RotationOrder rotationOrder);

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00024D64 File Offset: 0x00022F64
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00024D7A File Offset: 0x00022F7A
		public Vector3 localScale
		{
			get
			{
				Vector3 vector;
				this.get_localScale_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_localScale_Injected(ref value);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00024D84 File Offset: 0x00022F84
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00024D9C File Offset: 0x00022F9C
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				bool flag = this is RectTransform;
				if (flag)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00024DCC File Offset: 0x00022FCC
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x00024DE4 File Offset: 0x00022FE4
		internal Transform parentInternal
		{
			get
			{
				return this.GetParent();
			}
			set
			{
				this.SetParent(value);
			}
		}

		// Token: 0x06001679 RID: 5753
		[MethodImpl(4096)]
		private extern Transform GetParent();

		// Token: 0x0600167A RID: 5754 RVA: 0x00024DEF File Offset: 0x00022FEF
		public void SetParent(Transform p)
		{
			this.SetParent(p, true);
		}

		// Token: 0x0600167B RID: 5755
		[FreeFunction("SetParent", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00024DFC File Offset: 0x00022FFC
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_worldToLocalMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00024E14 File Offset: 0x00023014
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_localToWorldMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00024E2A File Offset: 0x0002302A
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			this.SetPositionAndRotation_Injected(ref position, ref rotation);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00024E38 File Offset: 0x00023038
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.World;
			if (flag)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00024E7C File Offset: 0x0002307C
		public void Translate(Vector3 translation)
		{
			this.Translate(translation, Space.Self);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00024E88 File Offset: 0x00023088
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00024E9C File Offset: 0x0002309C
		public void Translate(float x, float y, float z)
		{
			this.Translate(new Vector3(x, y, z), Space.Self);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00024EB0 File Offset: 0x000230B0
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			bool flag = relativeTo;
			if (flag)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00024EF6 File Offset: 0x000230F6
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00024F0C File Offset: 0x0002310C
		public void Rotate(Vector3 eulers, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion quaternion = Quaternion.Euler(eulers.x, eulers.y, eulers.z);
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.localRotation *= quaternion;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * quaternion * this.rotation;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00024F7F File Offset: 0x0002317F
		public void Rotate(Vector3 eulers)
		{
			this.Rotate(eulers, Space.Self);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00024F8B File Offset: 0x0002318B
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00024F9F File Offset: 0x0002319F
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), Space.Self);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00024FB2 File Offset: 0x000231B2
		[NativeMethod("RotateAround")]
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			this.RotateAroundInternal_Injected(ref axis, angle);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00024FC0 File Offset: 0x000231C0
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			bool flag = relativeTo == Space.Self;
			if (flag)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00025001 File Offset: 0x00023201
		public void Rotate(Vector3 axis, float angle)
		{
			this.Rotate(axis, angle, Space.Self);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00025010 File Offset: 0x00023210
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = quaternion * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0002505C File Offset: 0x0002325C
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00025084 File Offset: 0x00023284
		public void LookAt(Transform target)
		{
			bool flag = target;
			if (flag)
			{
				this.LookAt(target.position, Vector3.up);
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000250AE File Offset: 0x000232AE
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			this.Internal_LookAt(worldPosition, worldUp);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000250BA File Offset: 0x000232BA
		public void LookAt(Vector3 worldPosition)
		{
			this.Internal_LookAt(worldPosition, Vector3.up);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000250CA File Offset: 0x000232CA
		[FreeFunction("Internal_LookAt", HasExplicitThis = true)]
		private void Internal_LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			this.Internal_LookAt_Injected(ref worldPosition, ref worldUp);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000250D8 File Offset: 0x000232D8
		public Vector3 TransformDirection(Vector3 direction)
		{
			Vector3 vector;
			this.TransformDirection_Injected(ref direction, out vector);
			return vector;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000250F0 File Offset: 0x000232F0
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00025110 File Offset: 0x00023310
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			Vector3 vector;
			this.InverseTransformDirection_Injected(ref direction, out vector);
			return vector;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00025128 File Offset: 0x00023328
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00025148 File Offset: 0x00023348
		public Vector3 TransformVector(Vector3 vector)
		{
			Vector3 vector2;
			this.TransformVector_Injected(ref vector, out vector2);
			return vector2;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00025160 File Offset: 0x00023360
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00025180 File Offset: 0x00023380
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			Vector3 vector2;
			this.InverseTransformVector_Injected(ref vector, out vector2);
			return vector2;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00025198 File Offset: 0x00023398
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000251B8 File Offset: 0x000233B8
		public Vector3 TransformPoint(Vector3 position)
		{
			Vector3 vector;
			this.TransformPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000251D0 File Offset: 0x000233D0
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000251F0 File Offset: 0x000233F0
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			Vector3 vector;
			this.InverseTransformPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00025208 File Offset: 0x00023408
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00025228 File Offset: 0x00023428
		public Transform root
		{
			get
			{
				return this.GetRoot();
			}
		}

		// Token: 0x0600169F RID: 5791
		[MethodImpl(4096)]
		private extern Transform GetRoot();

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060016A0 RID: 5792
		public extern int childCount
		{
			[NativeMethod("GetChildrenCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060016A1 RID: 5793
		[FreeFunction("DetachChildren", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void DetachChildren();

		// Token: 0x060016A2 RID: 5794
		[MethodImpl(4096)]
		public extern void SetAsFirstSibling();

		// Token: 0x060016A3 RID: 5795
		[MethodImpl(4096)]
		public extern void SetAsLastSibling();

		// Token: 0x060016A4 RID: 5796
		[MethodImpl(4096)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x060016A5 RID: 5797
		[NativeMethod("MoveAfterSiblingInternal")]
		[MethodImpl(4096)]
		internal extern void MoveAfterSibling(Transform transform, bool notifyEditorAndMarkDirty);

		// Token: 0x060016A6 RID: 5798
		[MethodImpl(4096)]
		public extern int GetSiblingIndex();

		// Token: 0x060016A7 RID: 5799
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern Transform FindRelativeTransformWithPath(Transform transform, string path, [DefaultValue("false")] bool isActiveOnly);

		// Token: 0x060016A8 RID: 5800 RVA: 0x00025240 File Offset: 0x00023440
		public Transform Find(string n)
		{
			bool flag = n == null;
			if (flag)
			{
				throw new ArgumentNullException("Name cannot be null");
			}
			return Transform.FindRelativeTransformWithPath(this, n, false);
		}

		// Token: 0x060016A9 RID: 5801
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		internal extern void SendTransformChangedScale();

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00025270 File Offset: 0x00023470
		public Vector3 lossyScale
		{
			[NativeMethod("GetWorldScaleLossy")]
			get
			{
				Vector3 vector;
				this.get_lossyScale_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x060016AB RID: 5803
		[FreeFunction("Internal_IsChildOrSameTransform", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsChildOf([NotNull] Transform parent);

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060016AC RID: 5804
		// (set) Token: 0x060016AD RID: 5805
		[NativeProperty("HasChangedDeprecated")]
		public extern bool hasChanged
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00025288 File Offset: 0x00023488
		[Obsolete("FindChild has been deprecated. Use Find instead (UnityUpgradable) -> Find([mscorlib] System.String)", false)]
		public Transform FindChild(string n)
		{
			return this.Find(n);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000252A4 File Offset: 0x000234A4
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000252BC File Offset: 0x000234BC
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			this.RotateAround_Injected(ref axis, angle);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000252C7 File Offset: 0x000234C7
		[Obsolete("warning use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			this.RotateAroundLocal_Injected(ref axis, angle);
		}

		// Token: 0x060016B2 RID: 5810
		[FreeFunction("GetChild", HasExplicitThis = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		public extern Transform GetChild(int index);

		// Token: 0x060016B3 RID: 5811
		[NativeMethod("GetChildrenCount")]
		[Obsolete("warning use Transform.childCount instead (UnityUpgradable) -> Transform.childCount", false)]
		[MethodImpl(4096)]
		public extern int GetChildCount();

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000252D4 File Offset: 0x000234D4
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000252EC File Offset: 0x000234EC
		public int hierarchyCapacity
		{
			get
			{
				return this.internal_getHierarchyCapacity();
			}
			set
			{
				this.internal_setHierarchyCapacity(value);
			}
		}

		// Token: 0x060016B6 RID: 5814
		[FreeFunction("GetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int internal_getHierarchyCapacity();

		// Token: 0x060016B7 RID: 5815
		[FreeFunction("SetHierarchyCapacity", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void internal_setHierarchyCapacity(int value);

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000252F8 File Offset: 0x000234F8
		public int hierarchyCount
		{
			get
			{
				return this.internal_getHierarchyCount();
			}
		}

		// Token: 0x060016B9 RID: 5817
		[FreeFunction("GetHierarchyCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int internal_getHierarchyCount();

		// Token: 0x060016BA RID: 5818
		[FreeFunction("IsNonUniformScaleTransform", HasExplicitThis = true)]
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		internal extern bool IsNonUniformScaleTransform();

		// Token: 0x060016BB RID: 5819
		[MethodImpl(4096)]
		private extern void get_position_Injected(out Vector3 ret);

		// Token: 0x060016BC RID: 5820
		[MethodImpl(4096)]
		private extern void set_position_Injected(ref Vector3 value);

		// Token: 0x060016BD RID: 5821
		[MethodImpl(4096)]
		private extern void get_localPosition_Injected(out Vector3 ret);

		// Token: 0x060016BE RID: 5822
		[MethodImpl(4096)]
		private extern void set_localPosition_Injected(ref Vector3 value);

		// Token: 0x060016BF RID: 5823
		[MethodImpl(4096)]
		private extern void GetLocalEulerAngles_Injected(RotationOrder order, out Vector3 ret);

		// Token: 0x060016C0 RID: 5824
		[MethodImpl(4096)]
		private extern void SetLocalEulerAngles_Injected(ref Vector3 euler, RotationOrder order);

		// Token: 0x060016C1 RID: 5825
		[MethodImpl(4096)]
		private extern void SetLocalEulerHint_Injected(ref Vector3 euler);

		// Token: 0x060016C2 RID: 5826
		[MethodImpl(4096)]
		private extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x060016C3 RID: 5827
		[MethodImpl(4096)]
		private extern void set_rotation_Injected(ref Quaternion value);

		// Token: 0x060016C4 RID: 5828
		[MethodImpl(4096)]
		private extern void get_localRotation_Injected(out Quaternion ret);

		// Token: 0x060016C5 RID: 5829
		[MethodImpl(4096)]
		private extern void set_localRotation_Injected(ref Quaternion value);

		// Token: 0x060016C6 RID: 5830
		[MethodImpl(4096)]
		private extern void get_localScale_Injected(out Vector3 ret);

		// Token: 0x060016C7 RID: 5831
		[MethodImpl(4096)]
		private extern void set_localScale_Injected(ref Vector3 value);

		// Token: 0x060016C8 RID: 5832
		[MethodImpl(4096)]
		private extern void get_worldToLocalMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060016C9 RID: 5833
		[MethodImpl(4096)]
		private extern void get_localToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060016CA RID: 5834
		[MethodImpl(4096)]
		private extern void SetPositionAndRotation_Injected(ref Vector3 position, ref Quaternion rotation);

		// Token: 0x060016CB RID: 5835
		[MethodImpl(4096)]
		private extern void RotateAroundInternal_Injected(ref Vector3 axis, float angle);

		// Token: 0x060016CC RID: 5836
		[MethodImpl(4096)]
		private extern void Internal_LookAt_Injected(ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x060016CD RID: 5837
		[MethodImpl(4096)]
		private extern void TransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		// Token: 0x060016CE RID: 5838
		[MethodImpl(4096)]
		private extern void InverseTransformDirection_Injected(ref Vector3 direction, out Vector3 ret);

		// Token: 0x060016CF RID: 5839
		[MethodImpl(4096)]
		private extern void TransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		// Token: 0x060016D0 RID: 5840
		[MethodImpl(4096)]
		private extern void InverseTransformVector_Injected(ref Vector3 vector, out Vector3 ret);

		// Token: 0x060016D1 RID: 5841
		[MethodImpl(4096)]
		private extern void TransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x060016D2 RID: 5842
		[MethodImpl(4096)]
		private extern void InverseTransformPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x060016D3 RID: 5843
		[MethodImpl(4096)]
		private extern void get_lossyScale_Injected(out Vector3 ret);

		// Token: 0x060016D4 RID: 5844
		[MethodImpl(4096)]
		private extern void RotateAround_Injected(ref Vector3 axis, float angle);

		// Token: 0x060016D5 RID: 5845
		[MethodImpl(4096)]
		private extern void RotateAroundLocal_Injected(ref Vector3 axis, float angle);

		// Token: 0x020001FC RID: 508
		private class Enumerator : IEnumerator
		{
			// Token: 0x060016D6 RID: 5846 RVA: 0x00025310 File Offset: 0x00023510
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00025328 File Offset: 0x00023528
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x060016D8 RID: 5848 RVA: 0x0002534C File Offset: 0x0002354C
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < childCount;
			}

			// Token: 0x060016D9 RID: 5849 RVA: 0x0002537E File Offset: 0x0002357E
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000707 RID: 1799
			private Transform outer;

			// Token: 0x04000708 RID: 1800
			private int currentIndex = -1;
		}
	}
}
