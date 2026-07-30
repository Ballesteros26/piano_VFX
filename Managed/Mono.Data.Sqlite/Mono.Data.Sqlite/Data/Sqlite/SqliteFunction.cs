using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000023 RID: 35
	public abstract class SqliteFunction : IDisposable
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000B90F File Offset: 0x00009B0F
		protected SqliteFunction()
		{
			this._contextDataList = new Dictionary<long, SqliteFunction.AggregateData>();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000B922 File Offset: 0x00009B22
		public SqliteConvert SqliteConvert
		{
			get
			{
				return this._base;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B92A File Offset: 0x00009B2A
		public virtual object Invoke(object[] args)
		{
			return null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B92D File Offset: 0x00009B2D
		public virtual void Step(object[] args, int stepNumber, ref object contextData)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B92F File Offset: 0x00009B2F
		public virtual object Final(object contextData)
		{
			return null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B932 File Offset: 0x00009B32
		public virtual int Compare(string param1, string param2)
		{
			return 0;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B938 File Offset: 0x00009B38
		internal object[] ConvertParams(int nArgs, IntPtr argsptr)
		{
			object[] array = new object[nArgs];
			IntPtr[] array2 = new IntPtr[nArgs];
			Marshal.Copy(argsptr, array2, 0, nArgs);
			for (int i = 0; i < nArgs; i++)
			{
				switch (this._base.GetParamValueType(array2[i]))
				{
				case TypeAffinity.Int64:
					array[i] = this._base.GetParamValueInt64(array2[i]);
					break;
				case TypeAffinity.Double:
					array[i] = this._base.GetParamValueDouble(array2[i]);
					break;
				case TypeAffinity.Text:
					array[i] = this._base.GetParamValueText(array2[i]);
					break;
				case TypeAffinity.Blob:
				{
					int num = (int)this._base.GetParamValueBytes(array2[i], 0, null, 0, 0);
					byte[] array3 = new byte[num];
					this._base.GetParamValueBytes(array2[i], 0, array3, 0, num);
					array[i] = array3;
					break;
				}
				case TypeAffinity.Null:
					array[i] = DBNull.Value;
					break;
				case TypeAffinity.DateTime:
					array[i] = this._base.ToDateTime(this._base.GetParamValueText(array2[i]));
					break;
				}
			}
			return array;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000BA64 File Offset: 0x00009C64
		private void SetReturnValue(IntPtr context, object returnValue)
		{
			if (returnValue == null || returnValue == DBNull.Value)
			{
				this._base.ReturnNull(context);
				return;
			}
			Type type = returnValue.GetType();
			if (type == typeof(DateTime))
			{
				this._base.ReturnText(context, this._base.ToString((DateTime)returnValue));
				return;
			}
			Exception ex = returnValue as Exception;
			if (ex != null)
			{
				this._base.ReturnError(context, ex.Message);
				return;
			}
			switch (SqliteConvert.TypeToAffinity(type))
			{
			case TypeAffinity.Int64:
				this._base.ReturnInt64(context, Convert.ToInt64(returnValue, CultureInfo.CurrentCulture));
				return;
			case TypeAffinity.Double:
				this._base.ReturnDouble(context, Convert.ToDouble(returnValue, CultureInfo.CurrentCulture));
				return;
			case TypeAffinity.Text:
				this._base.ReturnText(context, returnValue.ToString());
				return;
			case TypeAffinity.Blob:
				this._base.ReturnBlob(context, (byte[])returnValue);
				return;
			case TypeAffinity.Null:
				this._base.ReturnNull(context);
				return;
			default:
				return;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000BB63 File Offset: 0x00009D63
		internal void ScalarCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			this._context = context;
			this.SetReturnValue(context, this.Invoke(this.ConvertParams(nArgs, argsptr)));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000BB81 File Offset: 0x00009D81
		internal int CompareCallback(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			return this.Compare(SqliteConvert.UTF8ToString(ptr1, len1), SqliteConvert.UTF8ToString(ptr2, len2));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000BB99 File Offset: 0x00009D99
		internal int CompareCallback16(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			return this.Compare(SQLite3_UTF16.UTF16ToString(ptr1, len1), SQLite3_UTF16.UTF16ToString(ptr2, len2));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		internal void StepCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			long num = (long)this._base.AggregateContext(context);
			SqliteFunction.AggregateData aggregateData;
			if (!this._contextDataList.TryGetValue(num, out aggregateData))
			{
				aggregateData = new SqliteFunction.AggregateData();
				this._contextDataList[num] = aggregateData;
			}
			try
			{
				this._context = context;
				this.Step(this.ConvertParams(nArgs, argsptr), aggregateData._count, ref aggregateData._data);
			}
			finally
			{
				aggregateData._count++;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000BC3C File Offset: 0x00009E3C
		internal void FinalCallback(IntPtr context)
		{
			long num = (long)this._base.AggregateContext(context);
			object obj = null;
			if (this._contextDataList.ContainsKey(num))
			{
				obj = this._contextDataList[num]._data;
				this._contextDataList.Remove(num);
			}
			this._context = context;
			this.SetReturnValue(context, this.Final(obj));
			IDisposable disposable = obj as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (KeyValuePair<long, SqliteFunction.AggregateData> keyValuePair in this._contextDataList)
				{
					IDisposable disposable = keyValuePair.Value._data as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this._contextDataList.Clear();
				this._InvokeFunc = null;
				this._StepFunc = null;
				this._FinalFunc = null;
				this._CompareFunc = null;
				this._base = null;
				this._contextDataList = null;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000BD50 File Offset: 0x00009F50
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000BD5C File Offset: 0x00009F5C
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		static SqliteFunction()
		{
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				int num = assemblies.Length;
				AssemblyName name = Assembly.GetCallingAssembly().GetName();
				int i = 0;
				while (i < num)
				{
					bool flag = false;
					Type[] array;
					try
					{
						AssemblyName[] referencedAssemblies = assemblies[i].GetReferencedAssemblies();
						int num2 = referencedAssemblies.Length;
						for (int j = 0; j < num2; j++)
						{
							if (referencedAssemblies[j].Name == name.Name)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							goto IL_0102;
						}
						array = assemblies[i].GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						array = ex.Types;
					}
					goto IL_008C;
					IL_0102:
					i++;
					continue;
					IL_008C:
					int num3 = array.Length;
					for (int k = 0; k < num3; k++)
					{
						if (!(array[k] == null))
						{
							object[] customAttributes = array[k].GetCustomAttributes(typeof(SqliteFunctionAttribute), false);
							int num4 = customAttributes.Length;
							for (int l = 0; l < num4; l++)
							{
								SqliteFunctionAttribute sqliteFunctionAttribute = customAttributes[l] as SqliteFunctionAttribute;
								if (sqliteFunctionAttribute != null)
								{
									sqliteFunctionAttribute._instanceType = array[k];
									SqliteFunction._registeredFunctions.Add(sqliteFunctionAttribute);
								}
							}
						}
					}
					goto IL_0102;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public static void RegisterFunction(Type typ)
		{
			object[] customAttributes = typ.GetCustomAttributes(typeof(SqliteFunctionAttribute), false);
			int num = customAttributes.Length;
			for (int i = 0; i < num; i++)
			{
				SqliteFunctionAttribute sqliteFunctionAttribute = customAttributes[i] as SqliteFunctionAttribute;
				if (sqliteFunctionAttribute != null)
				{
					sqliteFunctionAttribute._instanceType = typ;
					SqliteFunction._registeredFunctions.Add(sqliteFunctionAttribute);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000BF04 File Offset: 0x0000A104
		internal static SqliteFunction[] BindFunctions(SQLiteBase sqlbase)
		{
			List<SqliteFunction> list = new List<SqliteFunction>();
			foreach (SqliteFunctionAttribute sqliteFunctionAttribute in SqliteFunction._registeredFunctions)
			{
				SqliteFunction sqliteFunction = (SqliteFunction)Activator.CreateInstance(sqliteFunctionAttribute._instanceType);
				sqliteFunction._base = sqlbase;
				sqliteFunction._InvokeFunc = ((sqliteFunctionAttribute.FuncType == FunctionType.Scalar) ? new SQLiteCallback(sqliteFunction.ScalarCallback) : null);
				sqliteFunction._StepFunc = ((sqliteFunctionAttribute.FuncType == FunctionType.Aggregate) ? new SQLiteCallback(sqliteFunction.StepCallback) : null);
				sqliteFunction._FinalFunc = ((sqliteFunctionAttribute.FuncType == FunctionType.Aggregate) ? new SQLiteFinalCallback(sqliteFunction.FinalCallback) : null);
				sqliteFunction._CompareFunc = ((sqliteFunctionAttribute.FuncType == FunctionType.Collation) ? new SQLiteCollation(sqliteFunction.CompareCallback) : null);
				sqliteFunction._CompareFunc16 = ((sqliteFunctionAttribute.FuncType == FunctionType.Collation) ? new SQLiteCollation(sqliteFunction.CompareCallback16) : null);
				if (sqliteFunctionAttribute.FuncType != FunctionType.Collation)
				{
					sqlbase.CreateFunction(sqliteFunctionAttribute.Name, sqliteFunctionAttribute.Arguments, sqliteFunction is SqliteFunctionEx, sqliteFunction._InvokeFunc, sqliteFunction._StepFunc, sqliteFunction._FinalFunc);
				}
				else
				{
					sqlbase.CreateCollation(sqliteFunctionAttribute.Name, sqliteFunction._CompareFunc, sqliteFunction._CompareFunc16, IntPtr.Zero);
				}
				list.Add(sqliteFunction);
			}
			SqliteFunction[] array = new SqliteFunction[list.Count];
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040000C6 RID: 198
		internal SQLiteBase _base;

		// Token: 0x040000C7 RID: 199
		private Dictionary<long, SqliteFunction.AggregateData> _contextDataList;

		// Token: 0x040000C8 RID: 200
		private SQLiteCallback _InvokeFunc;

		// Token: 0x040000C9 RID: 201
		private SQLiteCallback _StepFunc;

		// Token: 0x040000CA RID: 202
		private SQLiteFinalCallback _FinalFunc;

		// Token: 0x040000CB RID: 203
		private SQLiteCollation _CompareFunc;

		// Token: 0x040000CC RID: 204
		private SQLiteCollation _CompareFunc16;

		// Token: 0x040000CD RID: 205
		internal IntPtr _context;

		// Token: 0x040000CE RID: 206
		private static List<SqliteFunctionAttribute> _registeredFunctions = new List<SqliteFunctionAttribute>();

		// Token: 0x0200003B RID: 59
		private class AggregateData
		{
			// Token: 0x04000113 RID: 275
			internal int _count = 1;

			// Token: 0x04000114 RID: 276
			internal object _data;
		}
	}
}
