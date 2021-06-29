using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalrRWinForms
{
    public partial class Form2 : Form
    {
        public Form2(string nombre)
        {
            InitializeComponent();
            
            lblNombre.Text = nombre;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
