using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F9 RID: 761
	internal class BasicFieldNameLookup
	{
		// Token: 0x060021C7 RID: 8647 RVA: 0x0009DACD File Offset: 0x0009BCCD
		public BasicFieldNameLookup(string[] fieldNames)
		{
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0009DAEC File Offset: 0x0009BCEC
		public BasicFieldNameLookup(ReadOnlyCollection<string> columnNames)
		{
			int count = columnNames.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = columnNames[i];
			}
			this._fieldNames = array;
			this.GenerateLookup();
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0009DB30 File Offset: 0x0009BD30
		public BasicFieldNameLookup(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0009DB70 File Offset: 0x0009BD70
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw ADP.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (-1 == num)
			{
				throw ADP.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0009DBA0 File Offset: 0x0009BDA0
		public int IndexOfName(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0009DBD0 File Offset: 0x0009BDD0
		public int IndexOf(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (-1 == num)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0009DC13 File Offset: 0x0009BE13
		protected virtual CompareInfo GetCompareInfo()
		{
			return CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0009DC20 File Offset: 0x0009BE20
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			if (this._compareInfo == null)
			{
				this._compareInfo = this.GetCompareInfo();
			}
			int num = this._fieldNames.Length;
			for (int i = 0; i < num; i++)
			{
				if (this._compareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0009DC80 File Offset: 0x0009BE80
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(num);
			int num2 = num - 1;
			while (0 <= num2)
			{
				string text = this._fieldNames[num2];
				dictionary[text] = num2;
				num2--;
			}
			this._fieldNameLookup = dictionary;
		}

		// Token: 0x040016AF RID: 5807
		private Dictionary<string, int> _fieldNameLookup;

		// Token: 0x040016B0 RID: 5808
		private readonly string[] _fieldNames;

		// Token: 0x040016B1 RID: 5809
		private CompareInfo _compareInfo;
	}
}
