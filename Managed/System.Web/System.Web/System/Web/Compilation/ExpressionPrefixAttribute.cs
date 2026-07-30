using System;

namespace System.Web.Compilation
{
	/// <summary>Specifies the prefix attribute to use for the expression builder. This class cannot be inherited. </summary>
	// Token: 0x02000605 RID: 1541
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionPrefixAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionPrefixAttribute" /> class.</summary>
		/// <param name="expressionPrefix">The prefix of the current <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expressionPrefix" /> is null or an empty string ("").</exception>
		// Token: 0x060042A5 RID: 17061 RVA: 0x000AFB41 File Offset: 0x000ADD41
		public ExpressionPrefixAttribute(string expressionPrefix)
		{
			if (string.IsNullOrEmpty(expressionPrefix))
			{
				throw new ArgumentNullException("expressionPrefix");
			}
			this._expressionPrefix = expressionPrefix;
		}

		/// <summary>Gets the prefix value for the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
		/// <returns>The expression prefix for the configured <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</returns>
		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000AFB63 File Offset: 0x000ADD63
		public string ExpressionPrefix
		{
			get
			{
				return this._expressionPrefix;
			}
		}

		// Token: 0x040023B0 RID: 9136
		private string _expressionPrefix;
	}
}
