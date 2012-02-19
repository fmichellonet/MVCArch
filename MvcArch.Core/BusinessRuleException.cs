using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcArch.Core
{
    public class BusinessRuleException : Exception
    {
        private readonly string _ruleName;

        public string RuleName
        {
            get { return _ruleName; }
        }
        
        public BusinessRuleException(string ruleName)
        {
            _ruleName = ruleName;
        }

        public BusinessRuleException(string ruleName, string message) : base(message)
        {
            _ruleName = ruleName;
        }

        public BusinessRuleException(IEnumerable<ValidationResult> rules) : this(rules.Aggregate(string.Empty, (current, result) => current + string.Format("{0}\n", result.ErrorMessage)))
        {}
    }
}
