using System;

namespace UnityEngine
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public struct LazyLoadReference<T> where T : Object
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0001FF27 File Offset: 0x0001E127
		public bool isSet
		{
			get
			{
				return this.m_InstanceID != 0;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0001FF32 File Offset: 0x0001E132
		public bool isBroken
		{
			get
			{
				return this.m_InstanceID != 0 && !Object.DoesObjectWithInstanceIDExist(this.m_InstanceID);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0001FF50 File Offset: 0x0001E150
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0001FF90 File Offset: 0x0001E190
		public T asset
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				T t;
				if (flag)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)Object.ForceLoadFromInstanceID(this.m_InstanceID));
				}
				return t;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.m_InstanceID = 0;
				}
				else
				{
					bool flag2 = !Object.IsPersistent(value);
					if (flag2)
					{
						throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
					}
					this.m_InstanceID = value.GetInstanceID();
				}
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0001FFEA File Offset: 0x0001E1EA
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0001FFF2 File Offset: 0x0001E1F2
		public int instanceID
		{
			get
			{
				return this.m_InstanceID;
			}
			set
			{
				this.m_InstanceID = value;
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		public LazyLoadReference(T asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				this.m_InstanceID = 0;
			}
			else
			{
				bool flag2 = !Object.IsPersistent(asset);
				if (flag2)
				{
					throw new ArgumentException("Object that does not belong to a persisted asset cannot be set as the target of a LazyLoadReference.");
				}
				this.m_InstanceID = asset.GetInstanceID();
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00020056 File Offset: 0x0001E256
		public LazyLoadReference(int instanceID)
		{
			this.m_InstanceID = instanceID;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00020060 File Offset: 0x0001E260
		public static implicit operator LazyLoadReference<T>(T asset)
		{
			return new LazyLoadReference<T>
			{
				asset = asset
			};
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00020084 File Offset: 0x0001E284
		public static implicit operator LazyLoadReference<T>(int instanceID)
		{
			return new LazyLoadReference<T>
			{
				instanceID = instanceID
			};
		}

		// Token: 0x0400064D RID: 1613
		private const int kInstanceID_None = 0;

		// Token: 0x0400064E RID: 1614
		[SerializeField]
		private int m_InstanceID;
	}
}
