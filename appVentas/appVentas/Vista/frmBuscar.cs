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

namespace appVentas.Vista
{
    public partial class frmBuscar : Form
    {
        
        public frmBuscar()
        {
            InitializeComponent();
        }

        private void BUSCAR_Load(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
           

        }

        void enviar()
        {
            String id = dgvBuscar.CurrentRow.Cells[0].Value.ToString();
            String nombre = dgvBuscar.CurrentRow.Cells[1].Value.ToString();
            String precio = dgvBuscar.CurrentRow.Cells[2].Value.ToString();

            frmMenu.ventas.txtCodProducto.Text = id;
            frmMenu.ventas.txtNombreProd.Text = nombre;
            frmMenu.ventas.txtPrecio.Text = precio;

            frmMenu.ventas.txtCantidad.Focus();

            this.Close();
        }

        void filtrar()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                string nombre = txtBuscar.Text;

                var buscar = from pro in bd.producto
                             where pro.nombreProducto.Contains(nombre)

                             select new
                             {
                                 CODIGO = pro.idProducto,
                                 NOMBRE = pro.nombreProducto,
                                 PRECIO = pro.precioProducto
                             };

                dgvBuscar.DataSource = buscar.ToList();


            }
        }

     

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void dgvBuscar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            enviar();
        }

        private void dgvBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enviar();
            }
        }
    }
}
