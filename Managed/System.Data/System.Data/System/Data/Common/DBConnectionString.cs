using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x000BC3B2 File Offset: 0x000BA5B2
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool useOdbcRules)
			: this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000BC3C9 File Offset: 0x000BA5C9
		internal DBConnectionString(DbConnectionOptions connectionOptions)
			: this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000BC3D8 File Offset: 0x000BA5D8
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool mustCloneDictionary)
		{
			if (behavior <= KeyRestrictionBehavior.PreventUsage)
			{
				this._behavior = behavior;
				this._encryptedUsersConnectionString = connectionOptions.UsersConnectionString(false);
				this._hasPassword = connectionOptions._hasPasswordKeyword;
				this._parsetable = connectionOptions.Parsetable;
				this._keychain = connectionOptions._keyChain;
				if (this._hasPassword && !connectionOptions.HasPersistablePassword)
				{
					if (mustCloneDictionary)
					{
						this._parsetable = new Dictionary<string, string>(this._parsetable);
					}
					if (this._parsetable.ContainsKey("password"))
					{
						this._parsetable["password"] = "*";
					}
					if (this._parsetable.ContainsKey("pwd"))
					{
						this._parsetable["pwd"] = "*";
					}
					this._keychain = connectionOptions.ReplacePasswordPwd(out this._encryptedUsersConnectionString, true);
				}
				if (!string.IsNullOrEmpty(restrictions))
				{
					this._restrictionValues = DBConnectionString.ParseRestrictions(restrictions, synonyms);
					this._restrictions = restrictions;
				}
				return;
			}
			throw ADP.InvalidKeyRestrictionBehavior(behavior);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000BC4D4 File Offset: 0x000BA6D4
		private DBConnectionString(DBConnectionString connectionString, string[] restrictionValues, KeyRestrictionBehavior behavior)
		{
			this._encryptedUsersConnectionString = connectionString._encryptedUsersConnectionString;
			this._parsetable = connectionString._parsetable;
			this._keychain = connectionString._keychain;
			this._hasPassword = connectionString._hasPassword;
			this._restrictionValues = restrictionValues;
			this._restrictions = null;
			this._behavior = behavior;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000BC52C File Offset: 0x000BA72C
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000BC534 File Offset: 0x000BA734
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000BC53C File Offset: 0x000BA73C
		internal bool IsEmpty
		{
			get
			{
				return this._keychain == null;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000BC547 File Offset: 0x000BA747
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000BC550 File Offset: 0x000BA750
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && restrictionValues.Length != 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							if (!string.IsNullOrEmpty(restrictionValues[i]))
							{
								stringBuilder.Append(restrictionValues[i]);
								stringBuilder.Append("=;");
							}
						}
						text = stringBuilder.ToString();
					}
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		// Token: 0x1700071F RID: 1823
		internal string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000BC5C8 File Offset: 0x000BA7C8
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000BC5D8 File Offset: 0x000BA7D8
		internal DBConnectionString Intersect(DBConnectionString entry)
		{
			KeyRestrictionBehavior keyRestrictionBehavior = this._behavior;
			string[] array = null;
			if (entry == null)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
			}
			else if (this._behavior != entry._behavior)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
				if (entry._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (!ADP.IsEmptyArray(this._restrictionValues))
					{
						if (!ADP.IsEmptyArray(entry._restrictionValues))
						{
							array = DBConnectionString.NewRestrictionAllowOnly(entry._restrictionValues, this._restrictionValues);
						}
					}
					else
					{
						array = entry._restrictionValues;
					}
				}
				else if (!ADP.IsEmptyArray(this._restrictionValues))
				{
					if (!ADP.IsEmptyArray(entry._restrictionValues))
					{
						array = DBConnectionString.NewRestrictionAllowOnly(this._restrictionValues, entry._restrictionValues);
					}
					else
					{
						array = this._restrictionValues;
					}
				}
			}
			else if (KeyRestrictionBehavior.PreventUsage == this._behavior)
			{
				if (ADP.IsEmptyArray(this._restrictionValues))
				{
					array = entry._restrictionValues;
				}
				else if (ADP.IsEmptyArray(entry._restrictionValues))
				{
					array = this._restrictionValues;
				}
				else
				{
					array = DBConnectionString.NoDuplicateUnion(this._restrictionValues, entry._restrictionValues);
				}
			}
			else if (!ADP.IsEmptyArray(this._restrictionValues) && !ADP.IsEmptyArray(entry._restrictionValues))
			{
				if (this._restrictionValues.Length <= entry._restrictionValues.Length)
				{
					array = DBConnectionString.NewRestrictionIntersect(this._restrictionValues, entry._restrictionValues);
				}
				else
				{
					array = DBConnectionString.NewRestrictionIntersect(entry._restrictionValues, this._restrictionValues);
				}
			}
			return new DBConnectionString(this, array, keyRestrictionBehavior);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000BC738 File Offset: 0x000BA938
		[Conditional("DEBUG")]
		private void ValidateCombinedSet(DBConnectionString componentSet, DBConnectionString combinedSet)
		{
			if (componentSet != null && combinedSet._restrictionValues != null && componentSet._restrictionValues != null)
			{
				if (componentSet._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
					{
						KeyRestrictionBehavior behavior = combinedSet._behavior;
						return;
					}
				}
				else if (componentSet._behavior == KeyRestrictionBehavior.PreventUsage && combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
				{
					KeyRestrictionBehavior behavior2 = combinedSet._behavior;
				}
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000BC78C File Offset: 0x000BA98C
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000BC7AC File Offset: 0x000BA9AC
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			if (behavior != KeyRestrictionBehavior.AllowOnly)
			{
				if (behavior != KeyRestrictionBehavior.PreventUsage)
				{
					throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
				}
				if (this._restrictionValues != null)
				{
					foreach (string text in this._restrictionValues)
					{
						if (entry.ContainsKey(text))
						{
							return false;
						}
					}
				}
			}
			else
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000BC83C File Offset: 0x000BAA3C
		private static string[] NewRestrictionAllowOnly(string[] allowonly, string[] preventusage)
		{
			List<string> list = null;
			for (int i = 0; i < allowonly.Length; i++)
			{
				if (0 > Array.BinarySearch<string>(preventusage, allowonly[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(allowonly[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000BC88C File Offset: 0x000BAA8C
		private static string[] NewRestrictionIntersect(string[] a, string[] b)
		{
			List<string> list = null;
			for (int i = 0; i < a.Length; i++)
			{
				if (0 <= Array.BinarySearch<string>(b, a[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(a[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000BC8DC File Offset: 0x000BAADC
		private static string[] NoDuplicateUnion(string[] a, string[] b)
		{
			List<string> list = new List<string>(a.Length + b.Length);
			for (int i = 0; i < a.Length; i++)
			{
				list.Add(a[i]);
			}
			for (int j = 0; j < b.Length; j++)
			{
				if (0 > Array.BinarySearch<string>(a, b[j], StringComparer.Ordinal))
				{
					list.Add(b[j]);
				}
			}
			string[] array = list.ToArray();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000BC948 File Offset: 0x000BAB48
		private static string[] ParseRestrictions(string restrictions, Dictionary<string, string> synonyms)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder(restrictions.Length);
			int i = 0;
			int length = restrictions.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(restrictions, num, stringBuilder, false, out text, out text2);
				if (!string.IsNullOrEmpty(text))
				{
					string text3 = ((synonyms != null) ? synonyms[text] : text);
					if (string.IsNullOrEmpty(text3))
					{
						throw ADP.KeywordNotSupported(text);
					}
					list.Add(text3);
				}
			}
			return DBConnectionString.RemoveDuplicates(list.ToArray());
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000BC9C8 File Offset: 0x000BABC8
		internal static string[] RemoveDuplicates(string[] restrictions)
		{
			int num = restrictions.Length;
			if (0 < num)
			{
				Array.Sort<string>(restrictions, StringComparer.Ordinal);
				for (int i = 1; i < restrictions.Length; i++)
				{
					string text = restrictions[i - 1];
					if (text.Length == 0 || text == restrictions[i])
					{
						restrictions[i - 1] = null;
						num--;
					}
				}
				if (restrictions[restrictions.Length - 1].Length == 0)
				{
					restrictions[restrictions.Length - 1] = null;
					num--;
				}
				if (num != restrictions.Length)
				{
					string[] array = new string[num];
					num = 0;
					for (int j = 0; j < restrictions.Length; j++)
					{
						if (restrictions[j] != null)
						{
							array[num++] = restrictions[j];
						}
					}
					restrictions = array;
				}
			}
			return restrictions;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000BCA6C File Offset: 0x000BAC6C
		[Conditional("DEBUG")]
		private static void Verify(string[] restrictionValues)
		{
			if (restrictionValues != null)
			{
				for (int i = 1; i < restrictionValues.Length; i++)
				{
				}
			}
		}

		// Token: 0x04001978 RID: 6520
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x04001979 RID: 6521
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x0400197A RID: 6522
		private readonly NameValuePair _keychain;

		// Token: 0x0400197B RID: 6523
		private readonly bool _hasPassword;

		// Token: 0x0400197C RID: 6524
		private readonly string[] _restrictionValues;

		// Token: 0x0400197D RID: 6525
		private readonly string _restrictions;

		// Token: 0x0400197E RID: 6526
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x0400197F RID: 6527
		private readonly string _encryptedActualConnectionString;

		// Token: 0x0200037C RID: 892
		private static class KEY
		{
			// Token: 0x04001980 RID: 6528
			internal const string Password = "password";

			// Token: 0x04001981 RID: 6529
			internal const string PersistSecurityInfo = "persist security info";

			// Token: 0x04001982 RID: 6530
			internal const string Pwd = "pwd";
		}
	}
}
