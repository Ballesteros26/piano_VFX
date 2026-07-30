using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class Collision2D
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000593C File Offset: 0x00003B3C
		private ContactPoint2D[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005964 File Offset: 0x00003B64
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005988 File Offset: 0x00003B88
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000059AC File Offset: 0x00003BAC
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000059D0 File Offset: 0x00003BD0
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000059F4 File Offset: 0x00003BF4
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005A2C File Offset: 0x00003C2C
		public GameObject gameObject
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005A64 File Offset: 0x00003C64
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005A7C File Offset: 0x00003C7C
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00005A98 File Offset: 0x00003C98
		public ContactPoint2D[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint2D[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005B00 File Offset: 0x00003D00
		public ContactPoint2D GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005B58 File Offset: 0x00003D58
		public int GetContacts(ContactPoint2D[] contacts)
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

		// Token: 0x06000205 RID: 517 RVA: 0x00005B9C File Offset: 0x00003D9C
		public int GetContacts(List<ContactPoint2D> contacts)
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

		// Token: 0x04000048 RID: 72
		internal int m_Collider;

		// Token: 0x04000049 RID: 73
		internal int m_OtherCollider;

		// Token: 0x0400004A RID: 74
		internal int m_Rigidbody;

		// Token: 0x0400004B RID: 75
		internal int m_OtherRigidbody;

		// Token: 0x0400004C RID: 76
		internal Vector2 m_RelativeVelocity;

		// Token: 0x0400004D RID: 77
		internal int m_Enabled;

		// Token: 0x0400004E RID: 78
		internal int m_ContactCount;

		// Token: 0x0400004F RID: 79
		internal ContactPoint2D[] m_ReusedContacts;

		// Token: 0x04000050 RID: 80
		internal ContactPoint2D[] m_LegacyContacts;
	}
}
