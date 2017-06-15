using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using CtrlDataManager.CustomControls;
using CtrlDataManager.Helpers;

namespace CtrlDataManager.Manager
{
    public class ControlManager
    {
        private ErrorProvider errorProvider = new ErrorProvider();
        public bool loaded;

        public ControlManager(ErrorProvider errorProvider)
        {
            this.errorProvider = errorProvider;
        }        

        public void BindControls(Control.ControlCollection controls, object model)
        {                                    
            foreach (Control control in controls)
            {
                if (control.Tag != null)
                {
                    if (control.Tag.ToString() != "")
                    {
                        BindControl(control, model);
                    }
                    else if (control.HasChildren)
                    {
                        BindControls(control.Controls, model);
                    }
                }
                else if (control.HasChildren)
                {
                    BindControls(control.Controls, model);
                }
            }
        }

        public void BindControl(Control control, object model)
        {
            if (control is MetroTextBox || control is TextBox)
            {
                try
                {
                    control.DataBindings.Add("Text", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }

            }
            else if (control is ComboBox)
            {
                try
                {
                    control.DataBindings.Add("SelectedValue", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.TextChanged += combo_TextChanged;
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is CheckBox)
            {
                try
                {
                    control.DataBindings.Add("Checked", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is RadioGroupBox)
            {
                try
                {
                    control.DataBindings.Add("Selected", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is MetroRadioGroupBox)
            {
                try
                {
                    control.DataBindings.Add("Selected", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is MetroRadioGroupBoxString)
            {
                try
                {
                    control.DataBindings.Add("Selected", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is NumericUpDown)
            {
                try
                {
                    control.DataBindings.Add("Value", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is DateTimePicker)
            {
                try
                {
                    control.DataBindings.Add("Value", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
            else if (control is Label || control is MetroLabel)
            {
                try
                {
                    control.DataBindings.Add("Text", model, control.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    control.Leave += valida;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verificar tag y modelo para el control " + control.Name);
                }
            }
        }

        public bool IsValidModel(Control.ControlCollection controls)
        {
            bool valid = true;
            foreach (Control control in controls)
            {
                foreach (Binding binding in control.DataBindings)
                {                    
                    object model = binding.DataSource;
                    string propName = model.GetType().GetProperty(binding.BindingMemberInfo.BindingMember).Name;
                    if (!ValidationHelper.ValidateProperty(model, binding.Control, propName, errorProvider))
                    {
                        if (valid)
                            valid = false;
                    }
                }
            }
            return valid;
        }        

        public void readValues(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                foreach (Binding binding in control.DataBindings)
                {
                    try
                    {
                        binding.ReadValue();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public void readValue(Control control)
        {
            foreach (Binding binding in control.DataBindings)
            {
                try
                {
                    binding.ReadValue();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool FormHasValues(Control.ControlCollection controls)
        {
            bool hasValues = false;
            foreach (Control control in controls)
            {
                foreach (Binding binding in control.DataBindings)
                {
                    string propName = binding.DataSource.GetType().GetProperty(binding.BindingMemberInfo.BindingMember).Name;
                    object model = binding.DataSource;
                    object value = model.GetType().GetProperty(propName).GetValue(model, null);

                    if (value != null)
                    {
                        hasValues = true;
                        break;
                    }                        
                }
                if (hasValues)
                    break;
            }
            return hasValues;   
        }

        public void CleanErrors(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                errorProvider.SetError(control, "");
            }
        }

        public void CleanError(Control control)
        {
            errorProvider.SetError(control, "");
        }

        private void combo_TextChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedValue == null)            
                combo.SelectedIndex = 0;
        }

        private void valida(object sender, EventArgs e)
        {
            if (loaded)
            {
                Control control = (Control)sender;                
                var a = control.DataBindings[0];
                string propName = a.DataSource.GetType().GetProperty(a.BindingMemberInfo.BindingMember).Name;
                ValidationHelper.ValidateProperty(a.DataSource, a.Control, propName, errorProvider);
                readValue(control);
            }
        }
    }
}
