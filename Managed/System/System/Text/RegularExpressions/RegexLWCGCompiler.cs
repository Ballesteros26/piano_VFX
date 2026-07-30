using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000145 RID: 325
	internal class RegexLWCGCompiler : RegexCompiler
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x00030A93 File Offset: 0x0002EC93
		internal RegexLWCGCompiler()
		{
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00030A9C File Offset: 0x0002EC9C
		internal RegexRunnerFactory FactoryInstanceFromCode(RegexCode code, RegexOptions options)
		{
			this._code = code;
			this._codes = code._codes;
			this._strings = code._strings;
			this._fcPrefix = code._fcPrefix;
			this._bmPrefix = code._bmPrefix;
			this._anchors = code._anchors;
			this._trackcount = code._trackcount;
			this._options = options;
			string text = Interlocked.Increment(ref RegexLWCGCompiler._regexCount).ToString(CultureInfo.InvariantCulture);
			DynamicMethod dynamicMethod = this.DefineDynamicMethod("Go" + text, null, typeof(CompiledRegexRunner));
			base.GenerateGo();
			DynamicMethod dynamicMethod2 = this.DefineDynamicMethod("FindFirstChar" + text, typeof(bool), typeof(CompiledRegexRunner));
			base.GenerateFindFirstChar();
			DynamicMethod dynamicMethod3 = this.DefineDynamicMethod("InitTrackCount" + text, null, typeof(CompiledRegexRunner));
			base.GenerateInitTrackCount();
			return new CompiledRegexRunnerFactory(dynamicMethod, dynamicMethod2, dynamicMethod3);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00030B90 File Offset: 0x0002ED90
		internal DynamicMethod DefineDynamicMethod(string methname, Type returntype, Type hostType)
		{
			MethodAttributes methodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static;
			CallingConventions callingConventions = CallingConventions.Standard;
			DynamicMethod dynamicMethod = new DynamicMethod(methname, methodAttributes, callingConventions, returntype, RegexLWCGCompiler._paramTypes, hostType, false);
			this._ilg = dynamicMethod.GetILGenerator();
			return dynamicMethod;
		}

		// Token: 0x04000E9E RID: 3742
		private static int _regexCount = 0;

		// Token: 0x04000E9F RID: 3743
		private static Type[] _paramTypes = new Type[] { typeof(RegexRunner) };
	}
}
