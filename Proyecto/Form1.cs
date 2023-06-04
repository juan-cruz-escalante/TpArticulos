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
    public partial class Form1 : Form
    {
        private List<Articulos> listaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        //ASI ESTA EN LA CLASE PERO NO FUNCIONA

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.imagenes.ImagenUrl);
        }

        public void Cargar()
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
                cargarImagen(listaArticulos[0].imagenes.ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["imagenes"].Visible = false;
        }


        public void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            Cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulos seleccionado;
            seleccionado = new Articulos();

            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            try
            {
                DialogResult respuesta= MessageBox.Show("¿Eliminar?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.Eliminar(seleccionado.IdArticulo);
                    Cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btbBuscar_Click(object sender, EventArgs e)
        {
            List<Articulos> listaFiltrada;

            string filtro = tbxFiltro.Text;
            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre == tbxFiltro.Text);
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }
    }
}
