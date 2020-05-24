using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appVentas.Vista
{
    public partial class frmMenu : Form
    {

       
        public frmMenu()
        {
            InitializeComponent();
        }

        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ventas.Hide();
            frmROLES roles = new frmROLES();
            roles.MdiParent = this;
            roles.Dock = DockStyle.Fill;
            roles.Show();
        }

        private void uSUARIOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ventas.Hide();
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.MdiParent = this;
            usuarios.Dock = DockStyle.Fill;
            usuarios.Show();

        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Inicio ini = new Inicio();
            ini.MdiParent = this;
            ini.Dock = DockStyle.Fill;
            ini.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public static frmVentas ventas = new frmVentas();
        private void realizarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ventas.MdiParent = this;
            ventas.Dock = DockStyle.Fill;
            ventas.Show();
        }
    }
}
