using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;


namespace Proyecto
{
    public partial class frmAltaArticulo : Form
    {
        private Articulos articulos = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulos articulos)
        {
            InitializeComponent();
            this.articulos = articulos;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulos artNuevo = new Articulos();
            ArticulosNegocio negocio = new ArticulosNegocio();
            
            try
            {
                artNuevo.Codigo = tbxCodigo.Text;
                artNuevo.Nombre = tbxNombre.Text;
                artNuevo.Descripcion = tbxDescripcion.Text;
                //artNuevo.Marcas.IdMarca = int.Parse(tbxMarca.Text);
                //artNuevo.Categoria.IdCategoria = int.Parse(tbxCategoria.Text);
                artNuevo.imagenes.ImagenUrl = tbxUrlImagen.Text;

                negocio.Agregar(artNuevo);
                MessageBox.Show("Agregado Exitosamente!!");
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tbxUrlImagen_Leave(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.cargarImagen(tbxUrlImagen.Text);
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            ArticulosNegocio articulosNegocio = new ArticulosNegocio();
            try
            {
                if(articulos != null)
                {
                    tbxCodigo.Text = articulos.Codigo;
                    tbxNombre.Text = articulos.Nombre;
                    tbxDescripcion.Text = articulos.Descripcion;
                    //tbxMarca.Text = articulos.Marcas.IdMarca;
                    //tbxCategoria.Text = articulos.Categoria.IdCategoria;
                    //tbxUrlImagen.Text = articulos.imagenes.ImagenUrl;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
