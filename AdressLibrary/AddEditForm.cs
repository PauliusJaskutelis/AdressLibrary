using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdressLibrary
{
    public partial class AddEditForm : Form
    {
        
        public AddEditForm()
        {
            InitializeComponent();
        }
        public string getNameText()
        {
            return nameTextBox.Text;
        }
        public string getNumberText()
        {
            return numberTextBox.Text;
        }
        public string getBirthDate()
        {
            return birthDateTimePicker.Value.ToShortDateString();
        }
        public void setNameText(string text)
        {
            nameTextBox.Text = text;
        }
        public void setNumberText(string text)
        {
            numberTextBox.Text = text;
        }
        public void setBirthDate(string text)
        {
            birthDateTimePicker.Value = DateTime.Parse(text);
        }
    }
}
