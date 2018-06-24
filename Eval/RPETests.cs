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
        [TestCase("100500/100500", 1)]
        public void RPEParser_MustCalculateSimpleExpression(string expression, decimal expected)
        {
            var postfixNotation = new PostfixNotationExpression();

            postfixNotation.result(expression).Should().Be(expected);
        }
    }
}
