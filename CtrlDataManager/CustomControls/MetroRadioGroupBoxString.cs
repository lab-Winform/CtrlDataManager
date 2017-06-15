using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CtrlDataManager.CustomControls
{
    public class MetroRadioGroupBoxString : GroupBox
    {
        public event EventHandler SelectedChanged = delegate { };

        string _selected;
        public string Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                //string val = value.ToString();
                _selected = value;
                var radioButton = this.Controls.OfType<MetroRadioButton>().FirstOrDefault(radio => radio.Tag != null && radio.Tag.ToString() == value);
                if (radioButton != null && _selected != null)
                {
                    radioButton.Checked = true;
                    _selected = value;
                }                
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            var radioButton = e.Control as MetroRadioButton;
            if (radioButton != null)
                radioButton.CheckedChanged += radioButton_CheckedChanged;

        }

        void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (MetroRadioButton)sender;            
            if (radio.Checked && radio.Tag != null)
            {
                _selected = radio.Tag.ToString();
                SelectedChanged(this, new EventArgs());
            }
        }
    }
}
