using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CtrlDataManager.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateProperty(object model, Control control, string propName, ErrorProvider errorProvider)
        {
            bool valid = true;

            var propValue = model.GetType().GetProperty(propName).GetValue(model, null);
            var validationContext = new ValidationContext(model) { MemberName = propName };
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(propValue, validationContext, results))
            {
                if (valid)
                    valid = false;  
                foreach (var error in results)
                {
                    errorProvider.SetError(control, error.ErrorMessage);
                }                
            }
            else
            {
                errorProvider.SetError(control, "");                
            }

            return valid;
        }
    }
}
