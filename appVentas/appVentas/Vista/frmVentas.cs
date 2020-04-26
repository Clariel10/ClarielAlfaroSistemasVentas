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
    public partial class frmVentas : Form
    {
        private decimal SubTotal = 0;
        private decimal Total = 0;
        
        public frmVentas()
        {
            InitializeComponent();
            lblSubtotal.Text = "0.00";
            lblTotal.Text = "0.00";
                        
        }



        void RetornarId()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var lista = bd.tb_venta;

                foreach (var iteracion in lista)
                {
                    lblNumVenta.Text = iteracion.idVenta.ToString();
                }
            }
        }

        void calculo()
        {
            decimal contador = 0;
            try
            {
                int cantidad = int.Parse(txtCantidad.Text);
                decimal precio = decimal.Parse(txtPrecio.Text);
                decimal subtotal = (cantidad * precio);
               


                txtTotal.Text = Convert.ToString(subtotal);

                SubTotal = SubTotal + (decimal.Parse(txtTotal.Text));
                Total = SubTotal;
                lblSubtotal.Text = SubTotal.ToString();
                lblTotal.Text = Total.ToString();


               
            }
            catch (Exception ex)
            {

            }


        }

        void limpiar()
        {
            txtCantidad.Text = "";
            txtCodProducto.Text = "";
            txtNombreProd.Text = "";
            txtPrecio.Text = "";
            txtTotal.Text = "";
            
        }
        void cargarDocumentos()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var documentos = bd.tb_documento.ToList();

                if (documentos.Count() > 0)
                {
                    cmbDocumento.DataSource = documentos;
                    cmbDocumento.DisplayMember = "nombreDocumento";
                    cmbDocumento.ValueMember = "iDDocumento";
                    cmbDocumento.SelectedIndex = -1;

                }

            }

        }

        void cargarClientes()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var clientes = bd.tb_cliente.ToList();

                if (clientes.Count() > 0)
                {
                    cmbClientes.DataSource = clientes;
                    cmbClientes.DisplayMember = "nombreCliente";
                    cmbClientes.ValueMember = "iDCliente";
                    cmbClientes.SelectedIndex = -1;
                    
                }

              
               
            }

            
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            RetornarId();
            cargarClientes();
            cargarDocumentos();
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var productos = bd.producto.ToList();

                if (productos.Count() > 0)
                {
                    dgvVentas.Rows[e.RowIndex].Cells[1].Value = productos;
                    


                }



            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscar b = new frmBuscar();
            b.Show();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try {
                calculo();
                
            }
            catch (Exception ex)
            {

            }
            dgvVentas.Rows.Add(txtCodProducto.Text, txtNombreProd.Text, txtPrecio.Text, txtCantidad.Text, txtTotal.Text);
            



            limpiar();
            btnBuscar.Focus();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
                       
                //calculo();
                       
            
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dgvVentas.Rows)
            {

            }
        }
    }
}
