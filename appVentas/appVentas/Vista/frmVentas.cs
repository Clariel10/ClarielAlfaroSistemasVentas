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
           
            lblTotal.Text = "0.00";
                        
        }

        

        void RetornarId()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                var lista = bd.tb_venta;

                foreach (var iteracion in lista)
                {
                    string id = iteracion.idVenta.ToString();
                    int idConvertido = Convert.ToInt32(id);
                    int suma = idConvertido + 1;
                    lblNumVenta.Text = suma.ToString();
                }
            }
        }

        void calculo()
        {
            
            try
            {
                int cantidad = int.Parse(txtCantidad.Text);
                decimal precio = decimal.Parse(txtPrecio.Text);
                decimal subtotal = (cantidad * precio);
               


                txtTotal.Text = Convert.ToString(subtotal);

                SubTotal = SubTotal + (decimal.Parse(txtTotal.Text));
                Total = SubTotal;
                
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

                if (txtCantidad.Text != "" && txtCodProducto.Text != "" && txtNombreProd.Text != "" && txtPrecio.Text != "") 
                {
                    calculo();
                    dgvVentas.Rows.Add(txtCodProducto.Text, txtNombreProd.Text, txtPrecio.Text,
                txtCantidad.Text, txtTotal.Text);
                }
                else
                {
                    MessageBox.Show("Datos vacios");
                }
                

            }
            catch (Exception ex)
            {
                
            }
            




            limpiar();
           
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
                       
                                      
            
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                try
                {
                    tb_venta tbV = new tb_venta();
                    tbV.idDocumento = Convert.ToInt32(cmbDocumento.SelectedValue);
                    tbV.iDCliente = Convert.ToInt32(cmbClientes.SelectedValue);
                    tbV.iDUsuario = 1;
                    tbV.totalVenta = Convert.ToDecimal(lblTotal.Text);
                    tbV.fecha = Convert.ToDateTime(dtpFecha.Text);
                    bd.tb_venta.Add(tbV);
                    bd.SaveChanges();

                    detalleVenta dete = new detalleVenta();


                    for (int i = 0; i < dgvVentas.RowCount; i++)
                    {
                        string idProducto = dgvVentas.Rows[i].Cells[0].Value.ToString();
                        int idConvertido = Convert.ToInt32(idProducto);

                        string Cantidad = dgvVentas.Rows[i].Cells[3].Value.ToString();
                        int CantidadConvertido = Convert.ToInt32(Cantidad);

                        string Precio = dgvVentas.Rows[i].Cells[2].Value.ToString();
                        decimal PrecioConvertido = Convert.ToDecimal(Precio);

                        string Total = dgvVentas.Rows[i].Cells[4].Value.ToString();
                        decimal TotalConvertido = Convert.ToDecimal(Total);


                        dete.idVenta = Convert.ToInt32(lblNumVenta.Text);
                        dete.idProducto = idConvertido;
                        dete.cantidad = CantidadConvertido;
                        dete.precio = PrecioConvertido;
                        dete.total = TotalConvertido;
                        bd.detalleVenta.Add(dete);
                        bd.SaveChanges();
                    }

                    limpiar();
                    dgvVentas.Rows.Clear();
                    RetornarId();
                    cargarClientes();
                    cargarDocumentos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se han definido algunos valores");
                }
                            
               

            }
        }

        private void txtBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBuscarProducto.Text == "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnBuscar.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                using (sistema_ventasEntities bd = new sistema_ventasEntities())
                {

                    producto pr = new producto();
                    int buscar = int.Parse(txtBuscarProducto.Text);
                    pr = bd.producto.Where(idBuscar => idBuscar.idProducto == buscar).First();
                    txtCodProducto.Text = Convert.ToString(pr.idProducto);
                    txtNombreProd.Text = pr.nombreProducto;
                    txtPrecio.Text = Convert.ToString(pr.precioProducto);
                    txtCantidad.Focus();
                    txtBuscarProducto.Text = "";

                }


            }
        }

        int intentos = 1;

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (intentos == 2)
                {
                    btnAgregar.PerformClick();
                    txtBuscarProducto.Focus();
                    txtCodProducto.Text = "";
                    txtNombreProd.Text = "";
                    txtPrecio.Text = "";
                    txtTotal.Text = "";
                    intentos = 0;
                    txtCantidad.Text = "1";
                    txtBuscarProducto.Focus();
                }
                intentos += 1;
            }
        }
    }
}
