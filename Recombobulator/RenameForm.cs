using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recombobulator
{
    public partial class RenameForm : Form
    {
        public string Message { get { return messageLabel.Text; } set { messageLabel.Text = value; } }

        public string NewName { get { return newNameTextBox.Text; } set { newNameTextBox.Text = value; } }

        public RenameForm()
        {
            InitializeComponent();
        }
    }
}
