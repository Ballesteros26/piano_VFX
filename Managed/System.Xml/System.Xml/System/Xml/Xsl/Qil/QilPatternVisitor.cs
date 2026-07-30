using System;
using System.Collections;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200063D RID: 1597
	internal abstract class QilPatternVisitor : QilReplaceVisitor
	{
		// Token: 0x06003F61 RID: 16225 RVA: 0x0015893D File Offset: 0x00156B3D
		public QilPatternVisitor(QilPatternVisitor.QilPatterns patterns, QilFactory f)
			: base(f)
		{
			this.Patterns = patterns;
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x00158958 File Offset: 0x00156B58
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x00158960 File Offset: 0x00156B60
		public QilPatternVisitor.QilPatterns Patterns
		{
			get
			{
				return this.patterns;
			}
			set
			{
				this.patterns = value;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x00158969 File Offset: 0x00156B69
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x00158971 File Offset: 0x00156B71
		public int Threshold
		{
			get
			{
				return this.threshold;
			}
			set
			{
				this.threshold = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x0015897A File Offset: 0x00156B7A
		public int ReplacementCount
		{
			get
			{
				return this.replacementCnt;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x00158982 File Offset: 0x00156B82
		public int LastReplacement
		{
			get
			{
				return this.lastReplacement;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0015898A File Offset: 0x00156B8A
		public bool Matching
		{
			get
			{
				return this.ReplacementCount < this.Threshold;
			}
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0015899A File Offset: 0x00156B9A
		protected virtual bool AllowReplace(int pattern, QilNode original)
		{
			if (this.Matching)
			{
				this.replacementCnt++;
				this.lastReplacement = pattern;
				return true;
			}
			return false;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x001589BC File Offset: 0x00156BBC
		protected virtual QilNode Replace(int pattern, QilNode original, QilNode replacement)
		{
			replacement.SourceLine = original.SourceLine;
			return replacement;
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode NoReplace(QilNode node)
		{
			return node;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x001589CB File Offset: 0x00156BCB
		protected override QilNode Visit(QilNode node)
		{
			if (node == null)
			{
				return this.VisitNull();
			}
			node = this.VisitChildren(node);
			return base.Visit(node);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitQilExpression(QilExpression n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFunctionList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitGlobalVariableList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitGlobalParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitActualParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFormalParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSortKeyList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitBranchList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitOptimizeBarrier(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitUnknown(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDataSource(QilDataSource n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNop(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitError(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitWarning(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFor(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitForReference(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLet(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLetReference(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitParameter(QilParameter n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitParameterReference(QilParameter n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitPositionOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitTrue(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFalse(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralString(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralInt32(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralInt64(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralDouble(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralDecimal(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralQName(QilName n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralType(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLiteralObject(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAnd(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitOr(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNot(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitConditional(QilTernary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitChoice(QilChoice n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLength(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSequence(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitUnion(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitIntersection(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDifference(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAverage(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitMinimum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitMaximum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNegate(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAdd(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSubtract(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitMultiply(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDivide(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitModulo(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitStrLength(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitStrConcat(QilStrConcat n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitStrParseQName(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitEq(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitGt(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitGe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLt(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitIs(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAfter(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitBefore(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLoop(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFilter(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSort(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitSortKey(QilSortKey n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDocOrderDistinct(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFunction(QilFunction n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFunctionReference(QilFunction n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitInvoke(QilInvoke n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitContent(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAttribute(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitParent(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitRoot(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXmlContext(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDescendant(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDescendantOrSelf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAncestor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAncestorOrSelf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitPreceding(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitFollowingSibling(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitPrecedingSibling(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNodeRange(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDeref(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitElementCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitAttributeCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitCommentCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitPICtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitTextCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitRawTextCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitDocumentCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNamespaceDecl(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitRtfCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNameOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitLocalNameOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitNamespaceUriOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitPrefixOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitTypeAssert(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitIsType(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitIsEmpty(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXPathNodeValue(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXPathFollowing(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXPathPreceding(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXPathNamespace(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltGenerateId(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltInvokeLateBound(QilInvokeLateBound n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltCopy(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltCopyOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x001589E7 File Offset: 0x00156BE7
		protected override QilNode VisitXsltConvert(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x040028C0 RID: 10432
		private QilPatternVisitor.QilPatterns patterns;

		// Token: 0x040028C1 RID: 10433
		private int replacementCnt;

		// Token: 0x040028C2 RID: 10434
		private int lastReplacement;

		// Token: 0x040028C3 RID: 10435
		private int threshold = int.MaxValue;

		// Token: 0x0200063E RID: 1598
		internal sealed class QilPatterns
		{
			// Token: 0x06003FDC RID: 16348 RVA: 0x001589F0 File Offset: 0x00156BF0
			private QilPatterns(QilPatternVisitor.QilPatterns toCopy)
			{
				this.bits = new BitArray(toCopy.bits);
			}

			// Token: 0x06003FDD RID: 16349 RVA: 0x00158A09 File Offset: 0x00156C09
			public QilPatterns(int szBits, bool allSet)
			{
				this.bits = new BitArray(szBits, allSet);
			}

			// Token: 0x06003FDE RID: 16350 RVA: 0x00158A1E File Offset: 0x00156C1E
			public QilPatternVisitor.QilPatterns Clone()
			{
				return new QilPatternVisitor.QilPatterns(this);
			}

			// Token: 0x06003FDF RID: 16351 RVA: 0x00158A26 File Offset: 0x00156C26
			public void ClearAll()
			{
				this.bits.SetAll(false);
			}

			// Token: 0x06003FE0 RID: 16352 RVA: 0x00158A34 File Offset: 0x00156C34
			public void Add(int i)
			{
				this.bits.Set(i, true);
			}

			// Token: 0x06003FE1 RID: 16353 RVA: 0x00158A43 File Offset: 0x00156C43
			public bool IsSet(int i)
			{
				return this.bits[i];
			}

			// Token: 0x040028C4 RID: 10436
			private BitArray bits;
		}
	}
}
