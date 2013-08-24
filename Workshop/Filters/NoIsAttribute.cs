using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Workshop.Filters
{
    public sealed class NoIsAttribute : ValidationAttribute,IClientValidatable
    {
        public string Input { get; set; }

        public NoIsAttribute(string input)
        {
            this.Input = input;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string)
            {
                if (this.Input.Contains(value.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "nois",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };

            rule.ValidationParameters["input"] = Input;
            yield return rule;
        }

        /*前端js 
         在jQuery validator 之後載入
         <script type="text/javascript">
            $.validator.addMethod("nois", function (value, element, param) {
                if (value == false) {
                    return true;
                }
                if (value.indexOf(param) != -1) {
                    return false;
                }
                else {
                    return true;
                }
            });
            $.validator.unobtrusive.adapters.addSingleVal("nois", "input");
        </script>
         
         */
    }
}