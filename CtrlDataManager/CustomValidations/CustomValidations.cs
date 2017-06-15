using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlDataManager.CustomValidations
{
    public class IsInteger : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un número entero";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {            
            int number;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null  && value.ToString() != "")
            {
                if (int.TryParse(value.ToString(), out number))
                {                                        
                    prop.SetValue(ctx.ObjectInstance, number);
                    return null;
                }
                else
                {                    
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }

        }
    }

    public class IsPositiveInteger : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un número entero positivo";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {            
            int number;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null  && value.ToString() != "")
            {
                if (int.TryParse(value.ToString(), out number))
                {     
                    if (number >= 0 )
                    {
                        prop.SetValue(ctx.ObjectInstance, number);
                        return null;
                    }
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
                else
                {                    
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }

        }
    }

    public class IsPositiveDouble : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un número positivo";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            double number;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (double.TryParse(value.ToString(), out number))
                {
                    if (number >= 0)
                    {
                        prop.SetValue(ctx.ObjectInstance, number);
                        return null;
                    }
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }

        }
    }

    public class IsCheckBox : ValidationAttribute
    {
        public const string CustomErrorMessage = "Campo Inválido";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            bool boolean;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (bool.TryParse(value.ToString(), out boolean))
                {
                    prop.SetValue(ctx.ObjectInstance, boolean);
                    return null;
                }                
                return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, false);
                return null;
            }

        }
    }

    public class IsDouble : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un número doble";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            double number;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (double.TryParse(value.ToString(), out number))
                {                    
                    prop.SetValue(ctx.ObjectInstance, number);
                    return null;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }            
        }
    }

    public class IsDecimal : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un número decimal";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            decimal number;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (decimal.TryParse(value.ToString(), out number))
                {
                    prop.SetValue(ctx.ObjectInstance, number);
                    return null;
                }
                else
                {                    
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }
        }
    }

    public class IsBoolean : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera un booleano";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            bool boolean;
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (bool.TryParse(value.ToString(), out boolean))
                {                    
                    prop.SetValue(ctx.ObjectInstance, boolean);
                    return null;
                }
                else
                {                    
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, false);
                return null;
            }

        }
    }

    public class IsDate : ValidationAttribute
    {
        public const string CustomErrorMessage = "Se espera una fecha válida";
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            DateTime date;            
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    string fecha = date.ToString("yyyy/MM/dd");
                    prop.SetValue(ctx.ObjectInstance, fecha);
                    return null;
                }
                else
                {                                        
                    return new ValidationResult(ErrorMessage ?? CustomErrorMessage);
                }
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }
        }
    }

    public class EmailAddress : ValidationAttribute
    {
        public override bool IsValid(object value)
        {                        
            if (value == null || value.ToString() == "")
            {
                return true;
            }
            string input = value as string;
            var emailAddressAttribute = new EmailAddressAttribute();

            return (input != null) && (string.IsNullOrEmpty(input) || emailAddressAttribute.IsValid(input));
        }        
    }

    public class UpperCase : ValidationAttribute
    {        
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {            
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            if (value != null && value.ToString() != "")
            {
                prop.SetValue(ctx.ObjectInstance, value.ToString().ToUpper());
                return null;               
            }
            else
            {
                prop.SetValue(ctx.ObjectInstance, null);
                return null;
            }
        }
    }
}
