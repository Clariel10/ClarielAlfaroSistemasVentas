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
    public partial class frmROLES : Form
    {
        public frmROLES()
        {
            InitializeComponent();
            txtId.Enabled = false;
            txtRol.Enabled = false;
            btnGuardar.Enabled = false;

        }

        void limpiar()
        {
            txtRol.Text = "";
            txtId.Text = "";
        }

        void cargar()
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {

                var uniones = from usuarios in bd.tb_usuarios
                              from roles in bd.roles_usuarios
                              where usuarios.Id == roles.Id_Usuario

                              select new
                              {

                                  ID = usuarios.Id,
                                  EMAIL = usuarios.Email,
                                  Rol = roles.tipo_rol

                              };

                     dgvVistas.DataSource = uniones.ToList();

            }
        }

        private void frmROLES_Load(object sender, EventArgs e)
        {

            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {

                var uniones = from usuarios in bd.tb_usuarios
                              from roles in bd.roles_usuarios
                              where usuarios.Id == roles.Id_Usuario

                              select new
                              {

                                  ID = roles.Id_Rol_Usuario,
                                  EMAIL = usuarios.Email,
                                  Rol = roles.tipo_rol

                              };

                dgvVistas.DataSource = uniones.ToList();

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvVistas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                
                roles_usuarios rol = new roles_usuarios();

                string id = dgvVistas.CurrentRow.Cells[0].Value.ToString();
                int id2 = int.Parse(id);
                rol = bd.roles_usuarios.Where(verificarId => verificarId.Id_Rol_Usuario == id2).First();
                txtId.Text = Convert.ToString(rol.Id_Usuario);
                txtRol.Text = dgvVistas.CurrentRow.Cells[2].Value.ToString();


            }
           
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            txtRol.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities())
            {
                if (txtId.Text != "" && txtRol.Text != "")
                {
                    roles_usuarios rol = new roles_usuarios();
                    rol.Id_Usuario = int.Parse(txtId.Text);
                    rol.tipo_rol = txtRol.Text;
                    bd.roles_usuarios.Add(rol);
                    bd.SaveChanges();
                }
                else
                {
                    MessageBox.Show("No se aceptan valores vacios");
                }
                cargar();
                limpiar();
                               
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "" && txtRol.Text != "")
            {
                using (sistema_ventasEntities db = new sistema_ventasEntities())
                {
                    roles_usuarios rol = new roles_usuarios();

                    string id = dgvVistas.CurrentRow.Cells[0].Value.ToString();

                    rol = db.roles_usuarios.Find(int.Parse(id));
                    db.roles_usuarios.Remove(rol);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("No se aceptan valores vacios");
            }


            cargar();
            limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "" && txtRol.Text != "")
            {
                using (sistema_ventasEntities db = new sistema_ventasEntities())
                {
                    roles_usuarios rol = new roles_usuarios();
                    string id = dgvVistas.CurrentRow.Cells[0].Value.ToString();
                    int idc = int.Parse(id);
                    rol = db.roles_usuarios.Where(VerificarId => VerificarId.Id_Rol_Usuario == idc).First();
                    rol.Id_Usuario = int.Parse(txtId.Text);
                    rol.tipo_rol = txtRol.Text;
                    db.Entry(rol).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
                
            else
            {
                MessageBox.Show("No se aceptan valores vacios");
            }

            cargar();
            limpiar();
        }
    }
}
