using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x02000261 RID: 609
	internal static class ODBC32
	{
		// Token: 0x06001AE6 RID: 6886 RVA: 0x00086EDE File Offset: 0x000850DE
		internal static string RetcodeToString(ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.INVALID_HANDLE:
				return "INVALID_HANDLE";
			case ODBC32.RetCode.ERROR:
				break;
			case ODBC32.RetCode.SUCCESS:
				return "SUCCESS";
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return "SUCCESS_WITH_INFO";
			default:
				if (retcode == ODBC32.RetCode.NO_DATA)
				{
					return "NO_DATA";
				}
				break;
			}
			return "ERROR";
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00086F1D File Offset: 0x0008511D
		internal static OdbcErrorCollection GetDiagErrors(string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			OdbcErrorCollection odbcErrorCollection = new OdbcErrorCollection();
			ODBC32.GetDiagErrors(odbcErrorCollection, source, hrHandle, retcode);
			return odbcErrorCollection;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00086F30 File Offset: 0x00085130
		internal static void GetDiagErrors(OdbcErrorCollection errors, string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				short num = 0;
				short num2 = 0;
				StringBuilder stringBuilder = new StringBuilder(1024);
				bool flag = true;
				while (flag)
				{
					num += 1;
					string text;
					int num3;
					retcode = hrHandle.GetDiagnosticRecord(num, out text, stringBuilder, out num3, out num2);
					if (ODBC32.RetCode.SUCCESS_WITH_INFO == retcode && stringBuilder.Capacity - 1 < (int)num2)
					{
						stringBuilder.Capacity = (int)(num2 + 1);
						retcode = hrHandle.GetDiagnosticRecord(num, out text, stringBuilder, out num3, out num2);
					}
					flag = retcode == ODBC32.RetCode.SUCCESS || retcode == ODBC32.RetCode.SUCCESS_WITH_INFO;
					if (flag)
					{
						errors.Add(new OdbcError(source, stringBuilder.ToString(), text, num3));
					}
				}
			}
		}

		// Token: 0x04001338 RID: 4920
		internal const short SQL_COMMIT = 0;

		// Token: 0x04001339 RID: 4921
		internal const short SQL_ROLLBACK = 1;

		// Token: 0x0400133A RID: 4922
		internal static readonly IntPtr SQL_AUTOCOMMIT_OFF = ADP.PtrZero;

		// Token: 0x0400133B RID: 4923
		internal static readonly IntPtr SQL_AUTOCOMMIT_ON = new IntPtr(1);

		// Token: 0x0400133C RID: 4924
		private const int SIGNED_OFFSET = -20;

		// Token: 0x0400133D RID: 4925
		private const int UNSIGNED_OFFSET = -22;

		// Token: 0x0400133E RID: 4926
		internal const short SQL_ALL_TYPES = 0;

		// Token: 0x0400133F RID: 4927
		internal static readonly IntPtr SQL_HANDLE_NULL = ADP.PtrZero;

		// Token: 0x04001340 RID: 4928
		internal const int SQL_NULL_DATA = -1;

		// Token: 0x04001341 RID: 4929
		internal const int SQL_NO_TOTAL = -4;

		// Token: 0x04001342 RID: 4930
		internal const int SQL_DEFAULT_PARAM = -5;

		// Token: 0x04001343 RID: 4931
		internal const int COLUMN_NAME = 4;

		// Token: 0x04001344 RID: 4932
		internal const int COLUMN_TYPE = 5;

		// Token: 0x04001345 RID: 4933
		internal const int DATA_TYPE = 6;

		// Token: 0x04001346 RID: 4934
		internal const int COLUMN_SIZE = 8;

		// Token: 0x04001347 RID: 4935
		internal const int DECIMAL_DIGITS = 10;

		// Token: 0x04001348 RID: 4936
		internal const int NUM_PREC_RADIX = 11;

		// Token: 0x04001349 RID: 4937
		internal static readonly IntPtr SQL_OV_ODBC3 = new IntPtr(3);

		// Token: 0x0400134A RID: 4938
		internal const int SQL_NTS = -3;

		// Token: 0x0400134B RID: 4939
		internal static readonly IntPtr SQL_CP_OFF = new IntPtr(0);

		// Token: 0x0400134C RID: 4940
		internal static readonly IntPtr SQL_CP_ONE_PER_DRIVER = new IntPtr(1);

		// Token: 0x0400134D RID: 4941
		internal static readonly IntPtr SQL_CP_ONE_PER_HENV = new IntPtr(2);

		// Token: 0x0400134E RID: 4942
		internal const int SQL_CD_TRUE = 1;

		// Token: 0x0400134F RID: 4943
		internal const int SQL_CD_FALSE = 0;

		// Token: 0x04001350 RID: 4944
		internal const int SQL_DTC_DONE = 0;

		// Token: 0x04001351 RID: 4945
		internal const int SQL_IS_POINTER = -4;

		// Token: 0x04001352 RID: 4946
		internal const int SQL_IS_PTR = 1;

		// Token: 0x04001353 RID: 4947
		internal const int MAX_CONNECTION_STRING_LENGTH = 1024;

		// Token: 0x04001354 RID: 4948
		internal const short SQL_DIAG_SQLSTATE = 4;

		// Token: 0x04001355 RID: 4949
		internal const short SQL_RESULT_COL = 3;

		// Token: 0x02000262 RID: 610
		internal enum SQL_HANDLE : short
		{
			// Token: 0x04001357 RID: 4951
			ENV = 1,
			// Token: 0x04001358 RID: 4952
			DBC,
			// Token: 0x04001359 RID: 4953
			STMT,
			// Token: 0x0400135A RID: 4954
			DESC
		}

		// Token: 0x02000263 RID: 611
		public enum RETCODE
		{
			// Token: 0x0400135C RID: 4956
			SUCCESS,
			// Token: 0x0400135D RID: 4957
			SUCCESS_WITH_INFO,
			// Token: 0x0400135E RID: 4958
			ERROR = -1,
			// Token: 0x0400135F RID: 4959
			INVALID_HANDLE = -2,
			// Token: 0x04001360 RID: 4960
			NO_DATA = 100
		}

		// Token: 0x02000264 RID: 612
		internal enum RetCode : short
		{
			// Token: 0x04001362 RID: 4962
			SUCCESS,
			// Token: 0x04001363 RID: 4963
			SUCCESS_WITH_INFO,
			// Token: 0x04001364 RID: 4964
			ERROR = -1,
			// Token: 0x04001365 RID: 4965
			INVALID_HANDLE = -2,
			// Token: 0x04001366 RID: 4966
			NO_DATA = 100
		}

		// Token: 0x02000265 RID: 613
		internal enum SQL_CONVERT : ushort
		{
			// Token: 0x04001368 RID: 4968
			BIGINT = 53,
			// Token: 0x04001369 RID: 4969
			BINARY,
			// Token: 0x0400136A RID: 4970
			BIT,
			// Token: 0x0400136B RID: 4971
			CHAR,
			// Token: 0x0400136C RID: 4972
			DATE,
			// Token: 0x0400136D RID: 4973
			DECIMAL,
			// Token: 0x0400136E RID: 4974
			DOUBLE,
			// Token: 0x0400136F RID: 4975
			FLOAT,
			// Token: 0x04001370 RID: 4976
			INTEGER,
			// Token: 0x04001371 RID: 4977
			LONGVARCHAR,
			// Token: 0x04001372 RID: 4978
			NUMERIC,
			// Token: 0x04001373 RID: 4979
			REAL,
			// Token: 0x04001374 RID: 4980
			SMALLINT,
			// Token: 0x04001375 RID: 4981
			TIME,
			// Token: 0x04001376 RID: 4982
			TIMESTAMP,
			// Token: 0x04001377 RID: 4983
			TINYINT,
			// Token: 0x04001378 RID: 4984
			VARBINARY,
			// Token: 0x04001379 RID: 4985
			VARCHAR,
			// Token: 0x0400137A RID: 4986
			LONGVARBINARY
		}

		// Token: 0x02000266 RID: 614
		[Flags]
		internal enum SQL_CVT
		{
			// Token: 0x0400137C RID: 4988
			CHAR = 1,
			// Token: 0x0400137D RID: 4989
			NUMERIC = 2,
			// Token: 0x0400137E RID: 4990
			DECIMAL = 4,
			// Token: 0x0400137F RID: 4991
			INTEGER = 8,
			// Token: 0x04001380 RID: 4992
			SMALLINT = 16,
			// Token: 0x04001381 RID: 4993
			FLOAT = 32,
			// Token: 0x04001382 RID: 4994
			REAL = 64,
			// Token: 0x04001383 RID: 4995
			DOUBLE = 128,
			// Token: 0x04001384 RID: 4996
			VARCHAR = 256,
			// Token: 0x04001385 RID: 4997
			LONGVARCHAR = 512,
			// Token: 0x04001386 RID: 4998
			BINARY = 1024,
			// Token: 0x04001387 RID: 4999
			VARBINARY = 2048,
			// Token: 0x04001388 RID: 5000
			BIT = 4096,
			// Token: 0x04001389 RID: 5001
			TINYINT = 8192,
			// Token: 0x0400138A RID: 5002
			BIGINT = 16384,
			// Token: 0x0400138B RID: 5003
			DATE = 32768,
			// Token: 0x0400138C RID: 5004
			TIME = 65536,
			// Token: 0x0400138D RID: 5005
			TIMESTAMP = 131072,
			// Token: 0x0400138E RID: 5006
			LONGVARBINARY = 262144,
			// Token: 0x0400138F RID: 5007
			INTERVAL_YEAR_MONTH = 524288,
			// Token: 0x04001390 RID: 5008
			INTERVAL_DAY_TIME = 1048576,
			// Token: 0x04001391 RID: 5009
			WCHAR = 2097152,
			// Token: 0x04001392 RID: 5010
			WLONGVARCHAR = 4194304,
			// Token: 0x04001393 RID: 5011
			WVARCHAR = 8388608,
			// Token: 0x04001394 RID: 5012
			GUID = 16777216
		}

		// Token: 0x02000267 RID: 615
		internal enum STMT : short
		{
			// Token: 0x04001396 RID: 5014
			CLOSE,
			// Token: 0x04001397 RID: 5015
			DROP,
			// Token: 0x04001398 RID: 5016
			UNBIND,
			// Token: 0x04001399 RID: 5017
			RESET_PARAMS
		}

		// Token: 0x02000268 RID: 616
		internal enum SQL_MAX
		{
			// Token: 0x0400139B RID: 5019
			NUMERIC_LEN = 16
		}

		// Token: 0x02000269 RID: 617
		internal enum SQL_IS
		{
			// Token: 0x0400139D RID: 5021
			POINTER = -4,
			// Token: 0x0400139E RID: 5022
			INTEGER = -6,
			// Token: 0x0400139F RID: 5023
			UINTEGER,
			// Token: 0x040013A0 RID: 5024
			SMALLINT = -8
		}

		// Token: 0x0200026A RID: 618
		internal enum SQL_HC
		{
			// Token: 0x040013A2 RID: 5026
			OFF,
			// Token: 0x040013A3 RID: 5027
			ON
		}

		// Token: 0x0200026B RID: 619
		internal enum SQL_NB
		{
			// Token: 0x040013A5 RID: 5029
			OFF,
			// Token: 0x040013A6 RID: 5030
			ON
		}

		// Token: 0x0200026C RID: 620
		internal enum SQL_CA_SS
		{
			// Token: 0x040013A8 RID: 5032
			BASE = 1200,
			// Token: 0x040013A9 RID: 5033
			COLUMN_HIDDEN = 1211,
			// Token: 0x040013AA RID: 5034
			COLUMN_KEY,
			// Token: 0x040013AB RID: 5035
			VARIANT_TYPE = 1215,
			// Token: 0x040013AC RID: 5036
			VARIANT_SQL_TYPE,
			// Token: 0x040013AD RID: 5037
			VARIANT_SERVER_TYPE
		}

		// Token: 0x0200026D RID: 621
		internal enum SQL_SOPT_SS
		{
			// Token: 0x040013AF RID: 5039
			BASE = 1225,
			// Token: 0x040013B0 RID: 5040
			HIDDEN_COLUMNS = 1227,
			// Token: 0x040013B1 RID: 5041
			NOBROWSETABLE
		}

		// Token: 0x0200026E RID: 622
		internal enum SQL_TRANSACTION
		{
			// Token: 0x040013B3 RID: 5043
			READ_UNCOMMITTED = 1,
			// Token: 0x040013B4 RID: 5044
			READ_COMMITTED,
			// Token: 0x040013B5 RID: 5045
			REPEATABLE_READ = 4,
			// Token: 0x040013B6 RID: 5046
			SERIALIZABLE = 8,
			// Token: 0x040013B7 RID: 5047
			SNAPSHOT = 32
		}

		// Token: 0x0200026F RID: 623
		internal enum SQL_PARAM
		{
			// Token: 0x040013B9 RID: 5049
			INPUT = 1,
			// Token: 0x040013BA RID: 5050
			INPUT_OUTPUT,
			// Token: 0x040013BB RID: 5051
			OUTPUT = 4,
			// Token: 0x040013BC RID: 5052
			RETURN_VALUE
		}

		// Token: 0x02000270 RID: 624
		internal enum SQL_API : ushort
		{
			// Token: 0x040013BE RID: 5054
			SQLCOLUMNS = 40,
			// Token: 0x040013BF RID: 5055
			SQLEXECDIRECT = 11,
			// Token: 0x040013C0 RID: 5056
			SQLGETTYPEINFO = 47,
			// Token: 0x040013C1 RID: 5057
			SQLPROCEDURECOLUMNS = 66,
			// Token: 0x040013C2 RID: 5058
			SQLPROCEDURES,
			// Token: 0x040013C3 RID: 5059
			SQLSTATISTICS = 53,
			// Token: 0x040013C4 RID: 5060
			SQLTABLES
		}

		// Token: 0x02000271 RID: 625
		internal enum SQL_DESC : short
		{
			// Token: 0x040013C6 RID: 5062
			COUNT = 1001,
			// Token: 0x040013C7 RID: 5063
			TYPE,
			// Token: 0x040013C8 RID: 5064
			LENGTH,
			// Token: 0x040013C9 RID: 5065
			OCTET_LENGTH_PTR,
			// Token: 0x040013CA RID: 5066
			PRECISION,
			// Token: 0x040013CB RID: 5067
			SCALE,
			// Token: 0x040013CC RID: 5068
			DATETIME_INTERVAL_CODE,
			// Token: 0x040013CD RID: 5069
			NULLABLE,
			// Token: 0x040013CE RID: 5070
			INDICATOR_PTR,
			// Token: 0x040013CF RID: 5071
			DATA_PTR,
			// Token: 0x040013D0 RID: 5072
			NAME,
			// Token: 0x040013D1 RID: 5073
			UNNAMED,
			// Token: 0x040013D2 RID: 5074
			OCTET_LENGTH,
			// Token: 0x040013D3 RID: 5075
			ALLOC_TYPE = 1099,
			// Token: 0x040013D4 RID: 5076
			CONCISE_TYPE = 2,
			// Token: 0x040013D5 RID: 5077
			DISPLAY_SIZE = 6,
			// Token: 0x040013D6 RID: 5078
			UNSIGNED = 8,
			// Token: 0x040013D7 RID: 5079
			UPDATABLE = 10,
			// Token: 0x040013D8 RID: 5080
			AUTO_UNIQUE_VALUE,
			// Token: 0x040013D9 RID: 5081
			TYPE_NAME = 14,
			// Token: 0x040013DA RID: 5082
			TABLE_NAME,
			// Token: 0x040013DB RID: 5083
			SCHEMA_NAME,
			// Token: 0x040013DC RID: 5084
			CATALOG_NAME,
			// Token: 0x040013DD RID: 5085
			BASE_COLUMN_NAME = 22,
			// Token: 0x040013DE RID: 5086
			BASE_TABLE_NAME
		}

		// Token: 0x02000272 RID: 626
		internal enum SQL_COLUMN
		{
			// Token: 0x040013E0 RID: 5088
			COUNT,
			// Token: 0x040013E1 RID: 5089
			NAME,
			// Token: 0x040013E2 RID: 5090
			TYPE,
			// Token: 0x040013E3 RID: 5091
			LENGTH,
			// Token: 0x040013E4 RID: 5092
			PRECISION,
			// Token: 0x040013E5 RID: 5093
			SCALE,
			// Token: 0x040013E6 RID: 5094
			DISPLAY_SIZE,
			// Token: 0x040013E7 RID: 5095
			NULLABLE,
			// Token: 0x040013E8 RID: 5096
			UNSIGNED,
			// Token: 0x040013E9 RID: 5097
			MONEY,
			// Token: 0x040013EA RID: 5098
			UPDATABLE,
			// Token: 0x040013EB RID: 5099
			AUTO_INCREMENT,
			// Token: 0x040013EC RID: 5100
			CASE_SENSITIVE,
			// Token: 0x040013ED RID: 5101
			SEARCHABLE,
			// Token: 0x040013EE RID: 5102
			TYPE_NAME,
			// Token: 0x040013EF RID: 5103
			TABLE_NAME,
			// Token: 0x040013F0 RID: 5104
			OWNER_NAME,
			// Token: 0x040013F1 RID: 5105
			QUALIFIER_NAME,
			// Token: 0x040013F2 RID: 5106
			LABEL
		}

		// Token: 0x02000273 RID: 627
		internal enum SQL_GROUP_BY
		{
			// Token: 0x040013F4 RID: 5108
			NOT_SUPPORTED,
			// Token: 0x040013F5 RID: 5109
			GROUP_BY_EQUALS_SELECT,
			// Token: 0x040013F6 RID: 5110
			GROUP_BY_CONTAINS_SELECT,
			// Token: 0x040013F7 RID: 5111
			NO_RELATION,
			// Token: 0x040013F8 RID: 5112
			COLLATE
		}

		// Token: 0x02000274 RID: 628
		internal enum SQL_SQL92_RELATIONAL_JOIN_OPERATORS
		{
			// Token: 0x040013FA RID: 5114
			CORRESPONDING_CLAUSE = 1,
			// Token: 0x040013FB RID: 5115
			CROSS_JOIN,
			// Token: 0x040013FC RID: 5116
			EXCEPT_JOIN = 4,
			// Token: 0x040013FD RID: 5117
			FULL_OUTER_JOIN = 8,
			// Token: 0x040013FE RID: 5118
			INNER_JOIN = 16,
			// Token: 0x040013FF RID: 5119
			INTERSECT_JOIN = 32,
			// Token: 0x04001400 RID: 5120
			LEFT_OUTER_JOIN = 64,
			// Token: 0x04001401 RID: 5121
			NATURAL_JOIN = 128,
			// Token: 0x04001402 RID: 5122
			RIGHT_OUTER_JOIN = 256,
			// Token: 0x04001403 RID: 5123
			UNION_JOIN = 512
		}

		// Token: 0x02000275 RID: 629
		internal enum SQL_OJ_CAPABILITIES
		{
			// Token: 0x04001405 RID: 5125
			LEFT = 1,
			// Token: 0x04001406 RID: 5126
			RIGHT,
			// Token: 0x04001407 RID: 5127
			FULL = 4,
			// Token: 0x04001408 RID: 5128
			NESTED = 8,
			// Token: 0x04001409 RID: 5129
			NOT_ORDERED = 16,
			// Token: 0x0400140A RID: 5130
			INNER = 32,
			// Token: 0x0400140B RID: 5131
			ALL_COMPARISON_OPS = 64
		}

		// Token: 0x02000276 RID: 630
		internal enum SQL_UPDATABLE
		{
			// Token: 0x0400140D RID: 5133
			READONLY,
			// Token: 0x0400140E RID: 5134
			WRITE,
			// Token: 0x0400140F RID: 5135
			READWRITE_UNKNOWN
		}

		// Token: 0x02000277 RID: 631
		internal enum SQL_IDENTIFIER_CASE
		{
			// Token: 0x04001411 RID: 5137
			UPPER = 1,
			// Token: 0x04001412 RID: 5138
			LOWER,
			// Token: 0x04001413 RID: 5139
			SENSITIVE,
			// Token: 0x04001414 RID: 5140
			MIXED
		}

		// Token: 0x02000278 RID: 632
		internal enum SQL_INDEX : short
		{
			// Token: 0x04001416 RID: 5142
			UNIQUE,
			// Token: 0x04001417 RID: 5143
			ALL
		}

		// Token: 0x02000279 RID: 633
		internal enum SQL_STATISTICS_RESERVED : short
		{
			// Token: 0x04001419 RID: 5145
			QUICK,
			// Token: 0x0400141A RID: 5146
			ENSURE
		}

		// Token: 0x0200027A RID: 634
		internal enum SQL_SPECIALCOLS : ushort
		{
			// Token: 0x0400141C RID: 5148
			BEST_ROWID = 1,
			// Token: 0x0400141D RID: 5149
			ROWVER
		}

		// Token: 0x0200027B RID: 635
		internal enum SQL_SCOPE : ushort
		{
			// Token: 0x0400141F RID: 5151
			CURROW,
			// Token: 0x04001420 RID: 5152
			TRANSACTION,
			// Token: 0x04001421 RID: 5153
			SESSION
		}

		// Token: 0x0200027C RID: 636
		internal enum SQL_NULLABILITY : ushort
		{
			// Token: 0x04001423 RID: 5155
			NO_NULLS,
			// Token: 0x04001424 RID: 5156
			NULLABLE,
			// Token: 0x04001425 RID: 5157
			UNKNOWN
		}

		// Token: 0x0200027D RID: 637
		internal enum SQL_SEARCHABLE
		{
			// Token: 0x04001427 RID: 5159
			UNSEARCHABLE,
			// Token: 0x04001428 RID: 5160
			LIKE_ONLY,
			// Token: 0x04001429 RID: 5161
			ALL_EXCEPT_LIKE,
			// Token: 0x0400142A RID: 5162
			SEARCHABLE
		}

		// Token: 0x0200027E RID: 638
		internal enum SQL_UNNAMED
		{
			// Token: 0x0400142C RID: 5164
			NAMED,
			// Token: 0x0400142D RID: 5165
			UNNAMED
		}

		// Token: 0x0200027F RID: 639
		internal enum HANDLER
		{
			// Token: 0x0400142F RID: 5167
			IGNORE,
			// Token: 0x04001430 RID: 5168
			THROW
		}

		// Token: 0x02000280 RID: 640
		internal enum SQL_STATISTICSTYPE
		{
			// Token: 0x04001432 RID: 5170
			TABLE_STAT,
			// Token: 0x04001433 RID: 5171
			INDEX_CLUSTERED,
			// Token: 0x04001434 RID: 5172
			INDEX_HASHED,
			// Token: 0x04001435 RID: 5173
			INDEX_OTHER
		}

		// Token: 0x02000281 RID: 641
		internal enum SQL_PROCEDURETYPE
		{
			// Token: 0x04001437 RID: 5175
			UNKNOWN,
			// Token: 0x04001438 RID: 5176
			PROCEDURE,
			// Token: 0x04001439 RID: 5177
			FUNCTION
		}

		// Token: 0x02000282 RID: 642
		internal enum SQL_C : short
		{
			// Token: 0x0400143B RID: 5179
			CHAR = 1,
			// Token: 0x0400143C RID: 5180
			WCHAR = -8,
			// Token: 0x0400143D RID: 5181
			SLONG = -16,
			// Token: 0x0400143E RID: 5182
			SSHORT,
			// Token: 0x0400143F RID: 5183
			REAL = 7,
			// Token: 0x04001440 RID: 5184
			DOUBLE,
			// Token: 0x04001441 RID: 5185
			BIT = -7,
			// Token: 0x04001442 RID: 5186
			UTINYINT = -28,
			// Token: 0x04001443 RID: 5187
			SBIGINT = -25,
			// Token: 0x04001444 RID: 5188
			UBIGINT = -27,
			// Token: 0x04001445 RID: 5189
			BINARY = -2,
			// Token: 0x04001446 RID: 5190
			TIMESTAMP = 11,
			// Token: 0x04001447 RID: 5191
			TYPE_DATE = 91,
			// Token: 0x04001448 RID: 5192
			TYPE_TIME,
			// Token: 0x04001449 RID: 5193
			TYPE_TIMESTAMP,
			// Token: 0x0400144A RID: 5194
			NUMERIC = 2,
			// Token: 0x0400144B RID: 5195
			GUID = -11,
			// Token: 0x0400144C RID: 5196
			DEFAULT = 99,
			// Token: 0x0400144D RID: 5197
			ARD_TYPE = -99
		}

		// Token: 0x02000283 RID: 643
		internal enum SQL_TYPE : short
		{
			// Token: 0x0400144F RID: 5199
			CHAR = 1,
			// Token: 0x04001450 RID: 5200
			VARCHAR = 12,
			// Token: 0x04001451 RID: 5201
			LONGVARCHAR = -1,
			// Token: 0x04001452 RID: 5202
			WCHAR = -8,
			// Token: 0x04001453 RID: 5203
			WVARCHAR = -9,
			// Token: 0x04001454 RID: 5204
			WLONGVARCHAR = -10,
			// Token: 0x04001455 RID: 5205
			DECIMAL = 3,
			// Token: 0x04001456 RID: 5206
			NUMERIC = 2,
			// Token: 0x04001457 RID: 5207
			SMALLINT = 5,
			// Token: 0x04001458 RID: 5208
			INTEGER = 4,
			// Token: 0x04001459 RID: 5209
			REAL = 7,
			// Token: 0x0400145A RID: 5210
			FLOAT = 6,
			// Token: 0x0400145B RID: 5211
			DOUBLE = 8,
			// Token: 0x0400145C RID: 5212
			BIT = -7,
			// Token: 0x0400145D RID: 5213
			TINYINT,
			// Token: 0x0400145E RID: 5214
			BIGINT,
			// Token: 0x0400145F RID: 5215
			BINARY = -2,
			// Token: 0x04001460 RID: 5216
			VARBINARY = -3,
			// Token: 0x04001461 RID: 5217
			LONGVARBINARY = -4,
			// Token: 0x04001462 RID: 5218
			TYPE_DATE = 91,
			// Token: 0x04001463 RID: 5219
			TYPE_TIME,
			// Token: 0x04001464 RID: 5220
			TIMESTAMP = 11,
			// Token: 0x04001465 RID: 5221
			TYPE_TIMESTAMP = 93,
			// Token: 0x04001466 RID: 5222
			GUID = -11,
			// Token: 0x04001467 RID: 5223
			SS_VARIANT = -150,
			// Token: 0x04001468 RID: 5224
			SS_UDT = -151,
			// Token: 0x04001469 RID: 5225
			SS_XML = -152,
			// Token: 0x0400146A RID: 5226
			SS_UTCDATETIME = -153,
			// Token: 0x0400146B RID: 5227
			SS_TIME_EX = -154
		}

		// Token: 0x02000284 RID: 644
		internal enum SQL_ATTR
		{
			// Token: 0x0400146D RID: 5229
			APP_ROW_DESC = 10010,
			// Token: 0x0400146E RID: 5230
			APP_PARAM_DESC,
			// Token: 0x0400146F RID: 5231
			IMP_ROW_DESC,
			// Token: 0x04001470 RID: 5232
			IMP_PARAM_DESC,
			// Token: 0x04001471 RID: 5233
			METADATA_ID,
			// Token: 0x04001472 RID: 5234
			ODBC_VERSION = 200,
			// Token: 0x04001473 RID: 5235
			CONNECTION_POOLING,
			// Token: 0x04001474 RID: 5236
			AUTOCOMMIT = 102,
			// Token: 0x04001475 RID: 5237
			TXN_ISOLATION = 108,
			// Token: 0x04001476 RID: 5238
			CURRENT_CATALOG,
			// Token: 0x04001477 RID: 5239
			LOGIN_TIMEOUT = 103,
			// Token: 0x04001478 RID: 5240
			QUERY_TIMEOUT = 0,
			// Token: 0x04001479 RID: 5241
			CONNECTION_DEAD = 1209,
			// Token: 0x0400147A RID: 5242
			SQL_COPT_SS_BASE = 1200,
			// Token: 0x0400147B RID: 5243
			SQL_COPT_SS_ENLIST_IN_DTC = 1207,
			// Token: 0x0400147C RID: 5244
			SQL_COPT_SS_TXN_ISOLATION = 1227
		}

		// Token: 0x02000285 RID: 645
		internal enum SQL_INFO : ushort
		{
			// Token: 0x0400147E RID: 5246
			DATA_SOURCE_NAME = 2,
			// Token: 0x0400147F RID: 5247
			SERVER_NAME = 13,
			// Token: 0x04001480 RID: 5248
			DRIVER_NAME = 6,
			// Token: 0x04001481 RID: 5249
			DRIVER_VER,
			// Token: 0x04001482 RID: 5250
			ODBC_VER = 10,
			// Token: 0x04001483 RID: 5251
			SEARCH_PATTERN_ESCAPE = 14,
			// Token: 0x04001484 RID: 5252
			DBMS_VER = 18,
			// Token: 0x04001485 RID: 5253
			DBMS_NAME = 17,
			// Token: 0x04001486 RID: 5254
			IDENTIFIER_CASE = 28,
			// Token: 0x04001487 RID: 5255
			IDENTIFIER_QUOTE_CHAR,
			// Token: 0x04001488 RID: 5256
			CATALOG_NAME_SEPARATOR = 41,
			// Token: 0x04001489 RID: 5257
			DRIVER_ODBC_VER = 77,
			// Token: 0x0400148A RID: 5258
			GROUP_BY = 88,
			// Token: 0x0400148B RID: 5259
			KEYWORDS,
			// Token: 0x0400148C RID: 5260
			ORDER_BY_COLUMNS_IN_SELECT,
			// Token: 0x0400148D RID: 5261
			QUOTED_IDENTIFIER_CASE = 93,
			// Token: 0x0400148E RID: 5262
			SQL_OJ_CAPABILITIES_30 = 115,
			// Token: 0x0400148F RID: 5263
			SQL_OJ_CAPABILITIES_20 = 65003,
			// Token: 0x04001490 RID: 5264
			SQL_SQL92_RELATIONAL_JOIN_OPERATORS = 161
		}

		// Token: 0x02000286 RID: 646
		internal enum SQL_DRIVER
		{
			// Token: 0x04001492 RID: 5266
			NOPROMPT,
			// Token: 0x04001493 RID: 5267
			COMPLETE,
			// Token: 0x04001494 RID: 5268
			PROMPT,
			// Token: 0x04001495 RID: 5269
			COMPLETE_REQUIRED
		}

		// Token: 0x02000287 RID: 647
		internal enum SQL_PRIMARYKEYS : short
		{
			// Token: 0x04001497 RID: 5271
			COLUMNNAME = 4
		}

		// Token: 0x02000288 RID: 648
		internal enum SQL_STATISTICS : short
		{
			// Token: 0x04001499 RID: 5273
			INDEXNAME = 6,
			// Token: 0x0400149A RID: 5274
			ORDINAL_POSITION = 8,
			// Token: 0x0400149B RID: 5275
			COLUMN_NAME
		}

		// Token: 0x02000289 RID: 649
		internal enum SQL_SPECIALCOLUMNSET : short
		{
			// Token: 0x0400149D RID: 5277
			COLUMN_NAME = 2
		}
	}
}
