using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002C RID: 44
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class SqliteFunctionAttribute : Attribute
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		public SqliteFunctionAttribute()
		{
			this.Name = "";
			this.Arguments = -1;
			this.FuncType = FunctionType.Scalar;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C117 File Offset: 0x0000A317
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C11F File Offset: 0x0000A31F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C128 File Offset: 0x0000A328
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C130 File Offset: 0x0000A330
		public int Arguments
		{
			get
			{
				return this._arguments;
			}
			set
			{
				this._arguments = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000C139 File Offset: 0x0000A339
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000C141 File Offset: 0x0000A341
		public FunctionType FuncType
		{
			get
			{
				return this._functionType;
			}
			set
			{
				this._functionType = value;
			}
		}

		// Token: 0x040000E0 RID: 224
		private string _name;

		// Token: 0x040000E1 RID: 225
		private int _arguments;

		// Token: 0x040000E2 RID: 226
		private FunctionType _functionType;

		// Token: 0x040000E3 RID: 227
		internal Type _instanceType;
	}
}
