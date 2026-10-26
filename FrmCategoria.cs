// Actividad 2.4 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
using SistemaBiblioteca1.Models;
using System;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    // Se establece la herencia de FrmBase y el contrato IPanelCRUD
    public partial class FrmCategoria : FrmBase, IPanelCRUD
    {
        public FrmCategoria()
        {
            InitializeComponent();

            // ---------- Actividad 2.5: validación de la Vista ----------
            ConfigurarCampoLetras(txtNombreCategoria, "Nombre de la categoría");
            ConfigurarCampoLetras(txtDescripcionCategoria, "Descripción", false); // opcional en el Modelo
        }

        // ---------- Implementación de IPanelCRUD ----------
        public void EjecutarGuardar()
        {
            // Actividad 2.5: ValidateChildren() + bandera de errores del ErrorProvider.
            // Si la pantalla no está limpia se detiene el proceso y no se empaqueta nada hacia el Modelo.
            if (!ValidarFormulario())
                return;

            try
            {
                Categoria categoria = CrearCategoriaDesdeControles();
                categoria.InsertarRegistro(categoria);

                MessageBox.Show("Categoría guardada correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.SetError(txtId, string.Empty);

            try
            {
                Categoria categoria = new Categoria().ConsultarRegistro(id) as Categoria;

                if (categoria == null)
                {
                    alerta.SetError(txtId, "No existe una categoría con ese id.");
                    return;
                }

                // Asignación de datos a los controles definidos en el Designer
                txtId.Text = categoria.Id.ToString();
                txtNombreCategoria.Text = categoria.Nombre;
                txtDescripcionCategoria.Text = categoria.Descripcion;
                chkRestringidaMenores.Checked = categoria.RestringidaMenores;
                chkEstadoCategoria.Checked = categoria.EsActivo;
            }
            catch (ArgumentException ex)
            {
                alerta.SetError(txtId, ex.Message);
            }
        }

        public void EjecutarActualizar()
        {
            // Actividad 2.5: ValidateChildren() + bandera de errores del ErrorProvider.
            // Si la pantalla no está limpia se detiene el proceso y no se empaqueta nada hacia el Modelo.
            if (!ValidarFormulario())
                return;

            try
            {
                Categoria categoria = CrearCategoriaDesdeControles();
                categoria.ActualizarRegistro(categoria);

                MessageBox.Show("Categoría actualizada correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                new Categoria().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Categoría eliminada correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        // ---------- Métodos auxiliares ----------
        private Categoria CrearCategoriaDesdeControles()
        {
            int id = LeerEntero(txtId.Text, "El id de la categoría");

            // Asumiendo que las propiedades de tu modelo se llaman así.
            // Ajusta los nombres de las propiedades si en tu Models/Categoria.cs son diferentes.
            Categoria categoria = new Categoria()
            {
                Id = id,
                Nombre = txtNombreCategoria.Text,
                Descripcion = txtDescripcionCategoria.Text,
                RestringidaMenores = chkRestringidaMenores.Checked,
                EsActivo = chkEstadoCategoria.Checked
            };

            return categoria;
        }

        private int LeerEntero(string texto, string nombreCampo)
        {
            if (!int.TryParse(texto, out int valor))
                throw new FormatException(nombreCampo + " debe ser un número entero válido.");
            return valor;
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtNombreCategoria.Clear();
            txtDescripcionCategoria.Clear();
            chkRestringidaMenores.Checked = false;
            chkEstadoCategoria.Checked = false;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }
    }
}