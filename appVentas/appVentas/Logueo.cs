using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appVentas.Model;
using appVentas.Vista;

namespace appVentas
{
    public partial class Logueo : Form
    {
        public Logueo()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities()) {

                var lista = from usuario in db.tb_usuarios
                            where usuario.Email == txtUsuario.Text
                            && usuario.Contrasena == txtContra.Text
                            select usuario;

                if (lista.Count() > 0)
                {

                    frmMenu menu = new frmMenu();
                    menu.ShowDialog();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("El Usuario no existe");
                }

            }
        }
    }
}
