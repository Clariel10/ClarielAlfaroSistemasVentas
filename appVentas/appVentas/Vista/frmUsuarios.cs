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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        tb_usuarios user = new tb_usuarios();

        void cargarDatos()
        {
            using (sistema_ventasEntities db = new sistema_ventasEntities())
            {

                var mostrar = from us in db.tb_usuarios

                              select new
                              {
                                  ID = us.Id,
                                  EMAIL = us.Email,
                                  CONTRASEÑA = us.Contrasena,

                              };

                dtvUsuario.DataSource = mostrar.ToList();
               
            }
            }

             void Limpiar()
        {
            txtUsuario.Text = "";
            txtContra.Text = "";
        }

            private void btnCargar_Click(object sender, EventArgs e)
        {
            txtContra.Enabled = true;
            txtUsuario.Enabled = true;
            btnGuardar.Enabled = true;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtContra.Text != "" && txtUsuario.Text != "")
            {
                using (sistema_ventasEntities db = new sistema_ventasEntities())
                {



                    user.Email = txtUsuario.Text;
                    user.Contrasena = txtContra.Text;

                    db.tb_usuarios.Add(user);
                    db.SaveChanges();

                }
            }
                
            else
            {
                MessageBox.Show("No se aceptan valores vacios");
            }


            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            cargarDatos();
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtContra.Text != "" && txtUsuario.Text != "")
            {
                using (sistema_ventasEntities db = new sistema_ventasEntities())
                {
                    string id = dtvUsuario.CurrentRow.Cells[0].Value.ToString();
                    int id2 = int.Parse(id);
                    user = db.tb_usuarios.Where(VerificarId => VerificarId.Id == id2).First();
                    db.tb_usuarios.Remove(user);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("No se aceptan valores vacios");
            }


            cargarDatos();
            Limpiar();
        }

        private void dtvUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Email = dtvUsuario.CurrentRow.Cells[1].Value.ToString();
            string Contra = dtvUsuario.CurrentRow.Cells[2].Value.ToString();
            txtUsuario.Text = Email;
            txtContra.Text = Contra;



        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtContra.Text != "" && txtUsuario.Text != "")
            {
                using (sistema_ventasEntities db = new sistema_ventasEntities())
                {
                    string id = dtvUsuario.CurrentRow.Cells[0].Value.ToString();
                    int idc = int.Parse(id);
                    user = db.tb_usuarios.Where(VerificarId => VerificarId.Id == idc).First();
                    user.Email = txtUsuario.Text;
                    user.Contrasena = txtContra.Text;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("No se aceptan valores vacios");
            }
            

            cargarDatos();
            Limpiar();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            cargarDatos();
            txtContra.Enabled = false;
            txtUsuario.Enabled = false;
            btnGuardar.Enabled = false;
           

        }
    }
}
