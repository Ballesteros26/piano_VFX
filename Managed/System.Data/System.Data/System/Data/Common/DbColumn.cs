using System;

namespace System.Data.Common
{
	// Token: 0x0200033D RID: 829
	public abstract class DbColumn
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000AD96B File Offset: 0x000ABB6B
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x000AD973 File Offset: 0x000ABB73
		public bool? AllowDBNull { get; protected set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000AD97C File Offset: 0x000ABB7C
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x000AD984 File Offset: 0x000ABB84
		public string BaseCatalogName { get; protected set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x000AD98D File Offset: 0x000ABB8D
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x000AD995 File Offset: 0x000ABB95
		public string BaseColumnName { get; protected set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000AD99E File Offset: 0x000ABB9E
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x000AD9A6 File Offset: 0x000ABBA6
		public string BaseSchemaName { get; protected set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000AD9AF File Offset: 0x000ABBAF
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x000AD9B7 File Offset: 0x000ABBB7
		public string BaseServerName { get; protected set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000AD9C0 File Offset: 0x000ABBC0
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
		public string BaseTableName { get; protected set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000AD9D1 File Offset: 0x000ABBD1
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x000AD9D9 File Offset: 0x000ABBD9
		public string ColumnName { get; protected set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000AD9E2 File Offset: 0x000ABBE2
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x000AD9EA File Offset: 0x000ABBEA
		public int? ColumnOrdinal { get; protected set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000AD9F3 File Offset: 0x000ABBF3
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x000AD9FB File Offset: 0x000ABBFB
		public int? ColumnSize { get; protected set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000ADA04 File Offset: 0x000ABC04
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x000ADA0C File Offset: 0x000ABC0C
		public bool? IsAliased { get; protected set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000ADA15 File Offset: 0x000ABC15
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x000ADA1D File Offset: 0x000ABC1D
		public bool? IsAutoIncrement { get; protected set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000ADA26 File Offset: 0x000ABC26
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x000ADA2E File Offset: 0x000ABC2E
		public bool? IsExpression { get; protected set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000ADA37 File Offset: 0x000ABC37
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x000ADA3F File Offset: 0x000ABC3F
		public bool? IsHidden { get; protected set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000ADA48 File Offset: 0x000ABC48
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x000ADA50 File Offset: 0x000ABC50
		public bool? IsIdentity { get; protected set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000ADA59 File Offset: 0x000ABC59
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x000ADA61 File Offset: 0x000ABC61
		public bool? IsKey { get; protected set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000ADA6A File Offset: 0x000ABC6A
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x000ADA72 File Offset: 0x000ABC72
		public bool? IsLong { get; protected set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000ADA7B File Offset: 0x000ABC7B
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x000ADA83 File Offset: 0x000ABC83
		public bool? IsReadOnly { get; protected set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000ADA8C File Offset: 0x000ABC8C
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x000ADA94 File Offset: 0x000ABC94
		public bool? IsUnique { get; protected set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x000ADA9D File Offset: 0x000ABC9D
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x000ADAA5 File Offset: 0x000ABCA5
		public int? NumericPrecision { get; protected set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000ADAAE File Offset: 0x000ABCAE
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x000ADAB6 File Offset: 0x000ABCB6
		public int? NumericScale { get; protected set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x000ADABF File Offset: 0x000ABCBF
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x000ADAC7 File Offset: 0x000ABCC7
		public string UdtAssemblyQualifiedName { get; protected set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000ADAD0 File Offset: 0x000ABCD0
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		public Type DataType { get; protected set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000ADAE1 File Offset: 0x000ABCE1
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x000ADAE9 File Offset: 0x000ABCE9
		public string DataTypeName { get; protected set; }

		// Token: 0x170006A8 RID: 1704
		public virtual object this[string property]
		{
			get
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(property);
				if (num <= 2477638934U)
				{
					if (num <= 1067318116U)
					{
						if (num <= 687909556U)
						{
							if (num != 405521230U)
							{
								if (num == 687909556U)
								{
									if (property == "ColumnOrdinal")
									{
										return this.ColumnOrdinal;
									}
								}
							}
							else if (property == "DataTypeName")
							{
								return this.DataTypeName;
							}
						}
						else if (num != 720006947U)
						{
							if (num != 1005639113U)
							{
								if (num == 1067318116U)
								{
									if (property == "ColumnName")
									{
										return this.ColumnName;
									}
								}
							}
							else if (property == "IsHidden")
							{
								return this.IsHidden;
							}
						}
						else if (property == "IsLong")
						{
							return this.IsLong;
						}
					}
					else if (num <= 2215472237U)
					{
						if (num != 1154057342U)
						{
							if (num != 1309233724U)
							{
								if (num == 2215472237U)
								{
									if (property == "DataType")
									{
										return this.DataType;
									}
								}
							}
							else if (property == "IsKey")
							{
								return this.IsKey;
							}
						}
						else if (property == "ColumnSize")
						{
							return this.ColumnSize;
						}
					}
					else if (num != 2239129947U)
					{
						if (num != 2380251540U)
						{
							if (num == 2477638934U)
							{
								if (property == "IsUnique")
								{
									return this.IsUnique;
								}
							}
						}
						else if (property == "NumericPrecision")
						{
							return this.NumericPrecision;
						}
					}
					else if (property == "IsExpression")
					{
						return this.IsExpression;
					}
				}
				else if (num <= 3042527364U)
				{
					if (num <= 2711511624U)
					{
						if (num != 2504653387U)
						{
							if (num != 2586490225U)
							{
								if (num == 2711511624U)
								{
									if (property == "BaseServerName")
									{
										return this.BaseServerName;
									}
								}
							}
							else if (property == "UdtAssemblyQualifiedName")
							{
								return this.UdtAssemblyQualifiedName;
							}
						}
						else if (property == "IsIdentity")
						{
							return this.IsIdentity;
						}
					}
					else if (num != 2741140585U)
					{
						if (num != 2757192823U)
						{
							if (num == 3042527364U)
							{
								if (property == "BaseCatalogName")
								{
									return this.BaseCatalogName;
								}
							}
						}
						else if (property == "BaseTableName")
						{
							return this.BaseTableName;
						}
					}
					else if (property == "BaseColumnName")
					{
						return this.BaseColumnName;
					}
				}
				else if (num <= 3656290791U)
				{
					if (num != 3115085976U)
					{
						if (num != 3173893005U)
						{
							if (num == 3656290791U)
							{
								if (property == "IsReadOnly")
								{
									return this.IsReadOnly;
								}
							}
						}
						else if (property == "AllowDBNull")
						{
							return this.AllowDBNull;
						}
					}
					else if (property == "BaseSchemaName")
					{
						return this.BaseSchemaName;
					}
				}
				else if (num != 3912158903U)
				{
					if (num != 3938522122U)
					{
						if (num == 4233439846U)
						{
							if (property == "IsAliased")
							{
								return this.IsAliased;
							}
						}
					}
					else if (property == "NumericScale")
					{
						return this.NumericScale;
					}
				}
				else if (property == "IsAutoIncrement")
				{
					return this.IsAutoIncrement;
				}
				return null;
			}
		}
	}
}
