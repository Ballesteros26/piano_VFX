using System;
using System.ComponentModel;

namespace System.Web.UI.Design
{
	/// <summary>Represents a design-time editor sheet for the properties of a resource expression in the UI of a designer host at design time.</summary>
	// Token: 0x0200009F RID: 159
	public class ResourceExpressionEditorSheet : ExpressionEditorSheet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ResourceExpressionEditorSheet" /> class.</summary>
		/// <param name="expression">A resource expression, used to initialize the expression editor sheet.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060004B1 RID: 1201 RVA: 0x000092C0 File Offset: 0x000074C0
		[MonoTODO]
		public ResourceExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the key that matches the filename for the resource in the project's global resource folder.</summary>
		/// <returns>The key for a resource file in the global resource folder. </returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[DefaultValue("")]
		public string ClassKey
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the resource expression string is valid.</summary>
		/// <returns>true if the resource expression string is valid; otherwise false.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the resource, which is used as a key to find the resource value.</summary>
		/// <returns>The name of the resource.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		[DefaultValue("")]
		public string ResourceKey
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a resource expression that is formed by the expression editor sheet property values.</summary>
		/// <returns>The resource expression string for the current settings in the sheet.</returns>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetExpression()
		{
			throw new NotImplementedException();
		}
	}
}
