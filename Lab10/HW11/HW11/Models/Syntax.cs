﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Sprache;

namespace HW11.Models
{
    internal class Syntax
    {
        public static readonly Parser<Expression<Func<double>>> ParseLambda =
            from body in Parse.Ref(() => Expr).End()
            select Expression.Lambda<Func<double>>(body);

        private static readonly Parser<ExpressionType> Operator =
            Parse.Chars('*', '×', '⋅').Return(ExpressionType.Multiply)
                .Or(Parse.Chars('/', '÷').Return(ExpressionType.Divide))
                .Or(Parse.Char('%').Return(ExpressionType.Modulo))
                .Or(Parse.Char('^').Return(ExpressionType.Power))
                .Or(Parse.Char('+').Return(ExpressionType.Add))
                .Or(Parse.Char('-').Return(ExpressionType.Subtract))
                .Token();

        private static readonly Parser<Expression> Operand =
            Parse.Ref(() => Number)
                .Or(Parse.Ref(() => Constant))
                .Or(Parse.Ref(() => Parenthesized))
                .Or(Parse.Ref(() => Function))
                .Or(Parse.Ref(() => Negate))
                .Token();

        private static readonly Parser<Expression> Expr =
            Parse.ChainOperator(Operator, Operand, Expression.MakeBinary);

        private static readonly Parser<Expression> Constant =
            Parse.String("pi").Return(Expression.Constant(Math.PI))
                .Or(Parse.Char('e').Return(Expression.Constant(Math.E)));

        private static readonly Parser<Expression> Number =
            from number in Parse.Decimal
            select Expression.Constant(Convert.ToDouble(number));

        private static readonly Parser<Expression> Parenthesized =
            from openParen in Parse.Char('(')
            from operand in Parse.Ref(() => Expr)
            from closeParen in Parse.Char(')')
            select operand;

        private static readonly Parser<Expression> Negate =
            from negative in Parse.Char('-')
            from expression in Parse.Ref(() => Operand)
            select Expression.Negate(expression);

        private static readonly Parser<Expression> Function =
            from name in Parse.Letter.AtLeastOnce().Text()
            from openParen in Parse.Char('(')
            from args in Parse.Ref(() => Expr).DelimitedBy(Parse.Char(','))
            from closeParen in Parse.Char(')')
            select Expression.Call(typeof(Math), name, new Type[0], args.ToArray());
    }
}