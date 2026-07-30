using System;
using System.Collections;
using System.Globalization;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000013 RID: 19
	public class LdapCompareAttrNames : IComparer
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004AE9 File Offset: 0x00002CE9
		private void InitBlock()
		{
			this.location = CultureInfo.CurrentCulture;
			this.collator = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004B06 File Offset: 0x00002D06
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004B0E File Offset: 0x00002D0E
		public virtual CultureInfo Locale
		{
			get
			{
				return this.location;
			}
			set
			{
				this.collator = value.CompareInfo;
				this.location = value;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004B23 File Offset: 0x00002D23
		public LdapCompareAttrNames(string attrName)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B5B File Offset: 0x00002D5B
		public LdapCompareAttrNames(string attrName, bool ascendingFlag)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = ascendingFlag;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004B94 File Offset: 0x00002D94
		public LdapCompareAttrNames(string[] attrNames)
		{
			this.InitBlock();
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[attrNames.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = true;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004BEC File Offset: 0x00002DEC
		public LdapCompareAttrNames(string[] attrNames, bool[] ascendingFlags)
		{
			this.InitBlock();
			if (attrNames.Length != ascendingFlags.Length)
			{
				throw new LdapException("UNEQUAL_LENGTHS", 18, null);
			}
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[ascendingFlags.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = ascendingFlags[i];
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C5C File Offset: 0x00002E5C
		public virtual int Compare(object object1, object object2)
		{
			LdapEntry ldapEntry = (LdapEntry)object1;
			LdapEntry ldapEntry2 = (LdapEntry)object2;
			int num = 0;
			if (this.collator == null)
			{
				this.collator = CultureInfo.CurrentCulture.CompareInfo;
			}
			int num2;
			do
			{
				LdapAttribute attribute = ldapEntry.getAttribute(this.sortByNames[num]);
				LdapAttribute attribute2 = ldapEntry2.getAttribute(this.sortByNames[num]);
				if (attribute != null && attribute2 != null)
				{
					string[] stringValueArray = attribute.StringValueArray;
					string[] stringValueArray2 = attribute2.StringValueArray;
					num2 = this.collator.Compare(stringValueArray[0], stringValueArray2[0]);
				}
				else if (attribute != null)
				{
					num2 = -1;
				}
				else if (attribute2 != null)
				{
					num2 = 1;
				}
				else
				{
					num2 = 0;
				}
				num++;
			}
			while (num2 == 0 && num < this.sortByNames.Length);
			if (this.sortAscending[num - 1])
			{
				return num2;
			}
			return -num2;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004D1C File Offset: 0x00002F1C
		public override bool Equals(object comparator)
		{
			if (!(comparator is LdapCompareAttrNames))
			{
				return false;
			}
			LdapCompareAttrNames ldapCompareAttrNames = (LdapCompareAttrNames)comparator;
			if (ldapCompareAttrNames.sortByNames.Length != this.sortByNames.Length || ldapCompareAttrNames.sortAscending.Length != this.sortAscending.Length)
			{
				return false;
			}
			for (int i = 0; i < this.sortByNames.Length; i++)
			{
				if (ldapCompareAttrNames.sortAscending[i] != this.sortAscending[i])
				{
					return false;
				}
				if (!ldapCompareAttrNames.sortByNames[i].ToUpper().Equals(this.sortByNames[i].ToUpper()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000070 RID: 112
		private string[] sortByNames;

		// Token: 0x04000071 RID: 113
		private bool[] sortAscending;

		// Token: 0x04000072 RID: 114
		private CultureInfo location;

		// Token: 0x04000073 RID: 115
		private CompareInfo collator;
	}
}
