using System;
using System.CodeDom;
using System.Reflection;

namespace System.Windows.Forms.Design
{
	/// <summary>Represents a parameter of a method of a hosted ActiveX control.</summary>
	// Token: 0x02000007 RID: 7
	public class AxParameterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified parameter information.</summary>
		/// <param name="info">A <see cref="T:System.Reflection.ParameterInfo" /> indicating the parameter information to use. </param>
		// Token: 0x06000019 RID: 25 RVA: 0x0000235A File Offset: 0x0000055A
		[MonoTODO]
		public AxParameterData(ParameterInfo info)
			: this(info, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified parameter information and whether to ignore by reference parameters.</summary>
		/// <param name="info">A <see cref="T:System.Reflection.ParameterInfo" /> indicating the parameter information to use. </param>
		/// <param name="ignoreByRefs">A value indicating whether to ignore parameters passed by reference. </param>
		// Token: 0x0600001A RID: 26 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public AxParameterData(ParameterInfo info, bool ignoreByRefs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified name and type name.</summary>
		/// <param name="inname">The name of the parameter. </param>
		/// <param name="typeName">The name of the type of the parameter. </param>
		// Token: 0x0600001B RID: 27 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public AxParameterData(string inname, string typeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified name and type.</summary>
		/// <param name="inname">The name of the parameter. </param>
		/// <param name="type">The type of the parameter. </param>
		// Token: 0x0600001C RID: 28 RVA: 0x00002364 File Offset: 0x00000564
		[MonoTODO]
		public AxParameterData(string inname, Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the specified parameter information to an <see cref="T:System.Windows.Forms.Design.AxParameterData" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.Design.AxParameterData" /> objects representing the specified array of <see cref="T:System.Reflection.ParameterInfo" /> objects.</returns>
		/// <param name="infos">An array of <see cref="T:System.Reflection.ParameterInfo" /> objects to convert. </param>
		// Token: 0x0600001D RID: 29 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static AxParameterData[] Convert(ParameterInfo[] infos)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the specified parameter information to an <see cref="T:System.Windows.Forms.Design.AxParameterData" /> object, according to the specified value indicating whether to ignore by reference parameters.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.Design.AxParameterData" /> objects representing the specified array of <see cref="T:System.Reflection.ParameterInfo" /> objects.</returns>
		/// <param name="infos">An array of <see cref="T:System.Reflection.ParameterInfo" /> objects to convert. </param>
		/// <param name="ignoreByRefs">A value indicating whether to ignore parameters passed by reference. </param>
		// Token: 0x0600001E RID: 30 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public static AxParameterData[] Convert(ParameterInfo[] infos, bool ignoreByRefs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates the direction of assignment fields.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> indicating the direction of assignment fields.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002371 File Offset: 0x00000571
		public FieldDirection Direction
		{
			get
			{
				if (this.IsOut)
				{
					return FieldDirection.Out;
				}
				if (this.IsByRef)
				{
					return FieldDirection.Ref;
				}
				return FieldDirection.In;
			}
		}

		/// <summary>Indicates whether the parameter data is passed by reference.</summary>
		/// <returns>true if the parameter data is by reference; otherwise, false.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002388 File Offset: 0x00000588
		public bool IsByRef
		{
			get
			{
				return this.isByRef;
			}
		}

		/// <summary>Indicates whether the parameter data is in.</summary>
		/// <returns>true if the parameter data is in; otherwise, false.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002390 File Offset: 0x00000590
		public bool IsIn
		{
			get
			{
				return this.isIn;
			}
		}

		/// <summary>Indicates whether the parameter data is optional.</summary>
		/// <returns>true if the parameter data is optional; otherwise, false.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002398 File Offset: 0x00000598
		public bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		/// <summary>Indicates whether the parameter data is out.</summary>
		/// <returns>true if the parameter data is out; otherwise, false.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023A0 File Offset: 0x000005A0
		public bool IsOut
		{
			get
			{
				return this.isOut;
			}
		}

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023A8 File Offset: 0x000005A8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000234B File Offset: 0x0000054B
		public string Name
		{
			get
			{
				return this.name;
			}
			[MonoTODO]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type expected by the parameter.</summary>
		/// <returns>The type expected by the parameter.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023B0 File Offset: 0x000005B0
		public Type ParameterType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the name of the type expected by the parameter.</summary>
		/// <returns>The name of the type expected by the parameter.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public string TypeName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000013 RID: 19
		private bool isByRef;

		// Token: 0x04000014 RID: 20
		private bool isIn;

		// Token: 0x04000015 RID: 21
		private bool isOptional;

		// Token: 0x04000016 RID: 22
		private bool isOut;

		// Token: 0x04000017 RID: 23
		private string name;

		// Token: 0x04000018 RID: 24
		private Type type;
	}
}
