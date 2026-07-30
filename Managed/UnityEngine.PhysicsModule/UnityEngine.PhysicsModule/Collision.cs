using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class Collision
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000024D0 File Offset: 0x000006D0
		private ContactPoint[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024F8 File Offset: 0x000006F8
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002510 File Offset: 0x00000710
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002528 File Offset: 0x00000728
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002540 File Offset: 0x00000740
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002578 File Offset: 0x00000778
		public GameObject gameObject
		{
			get
			{
				return (this.m_Rigidbody != null) ? this.m_Rigidbody.gameObject : this.m_Collider.gameObject;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000025B0 File Offset: 0x000007B0
		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000025C8 File Offset: 0x000007C8
		public ContactPoint[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002618 File Offset: 0x00000818
		public ContactPoint GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002670 File Offset: 0x00000870
		public int GetContacts(ContactPoint[] contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
			}
			int num = Mathf.Min(this.m_ContactCount, contacts.Length);
			Array.Copy(this.GetContacts_Internal(), contacts, num);
			return num;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026B4 File Offset: 0x000008B4
		public int GetContacts(List<ContactPoint> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			contacts.AddRange(this.GetContacts_Internal());
			return this.contactCount;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026F4 File Offset: 0x000008F4
		[Obsolete("Do not use Collision.GetEnumerator(), enumerate using non-allocating array returned by Collision.GetContacts() or enumerate using Collision.GetContact(index) instead.", false)]
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002714 File Offset: 0x00000914
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000272C File Offset: 0x0000092C
		[Obsolete("Use Collision.relativeVelocity instead.", false)]
		public Vector3 impactForceSum
		{
			get
			{
				return this.relativeVelocity;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002744 File Offset: 0x00000944
		[Obsolete("Will always return zero.", false)]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000275C File Offset: 0x0000095C
		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
		public Component other
		{
			get
			{
				return (this.m_Rigidbody != null) ? this.m_Rigidbody : this.m_Collider;
			}
		}

		// Token: 0x0400004B RID: 75
		internal Vector3 m_Impulse;

		// Token: 0x0400004C RID: 76
		internal Vector3 m_RelativeVelocity;

		// Token: 0x0400004D RID: 77
		internal Rigidbody m_Rigidbody;

		// Token: 0x0400004E RID: 78
		internal Collider m_Collider;

		// Token: 0x0400004F RID: 79
		internal int m_ContactCount;

		// Token: 0x04000050 RID: 80
		internal ContactPoint[] m_ReusedContacts;

		// Token: 0x04000051 RID: 81
		internal ContactPoint[] m_LegacyContacts;
	}
}
