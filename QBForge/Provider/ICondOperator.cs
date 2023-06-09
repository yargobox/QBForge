﻿using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
	public interface ICondOperator : IRenderContext { }

	public delegate ICondOperator UnaryOperator(ICondOperator render, Clause lhs);

	public delegate ICondOperator BinaryOperator(ICondOperator render, Clause lhs, Clause rhs);

	public delegate ICondOperator TernaryOperator(ICondOperator render, Clause lhs, Clause rhs1, Clause rhs2);
}