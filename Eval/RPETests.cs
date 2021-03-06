﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PostfixNotation;

namespace EvalTask
{
    [TestFixture]
    class RPEParser_Should
    {
        [Test]
        [TestCase("1+2*10", 21)]
        [TestCase("2*10", 20)]
        [TestCase("100500/100500", 1)]
        [TestCase("(14+2)*2", 32)]
        [TestCase("((14+2)*2+4)*2", 72)]
        [TestCase("2*2+1+2/2", 6)]
        [TestCase("3,14*2", 3.14 * 2)]
        [TestCase("((((((1+1)*2)*2)*2)*2)*2)*2", 128)]
        [TestCase("4", 4)]
        [TestCase("1/2", 0.5)]
        [TestCase("1,2", 1.2)]
        [TestCase("-2*3", -6)]
        public void RPEParser_MustCalculateSimpleExpression(string expression, decimal expected)
        {
            var postfixNotation = new PostfixNotationExpression();

            postfixNotation.result(expression)
                .Should()
                .Be(expected);
        }

        [Test]
        [TestCase("4/0", 0)]
        [TestCase(" ", 0)]
        [TestCase("", 0)]
        [TestCase("+++", 0)]
        [TestCase("+*/", 0)]
        [TestCase("+12/", 0)]
        [TestCase("@#!", 0)]
        [TestCase("abc", 0)]
        public void RPEParser_MustNotCalculateSimpleExpression(string expression, decimal expected)
        {
            var postfixNotation = new PostfixNotationExpression();

            postfixNotation.result(expression)
                .Should()
                .NotBeOfType(typeof(decimal));
        }
    }
}
