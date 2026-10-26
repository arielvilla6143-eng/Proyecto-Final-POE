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
    public partial class FrmEditorialcs : FrmBase, IPanelCRUD
    {
        public FrmEditorialcs()
        {
            InitializeComponent();

            // ---------- Actividad 2.5: validación de la Vista ----------
            ConfigurarCampoTexto(txtNombreEditorial, "Nombre de la editorial");
            ConfigurarCampoLetras(txtPaisOrigen, "País de origen");
            ConfigurarCampoEntero(txtAnioFundacion, "Año de fundación", ValidarAnioFundacion);
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
                Editorial editorial = CrearEditorialDesdeControles();
                editorial.InsertarRegistro(editorial);

                MessageBox.Show("Editorial guardada correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Editorial editorial = new Editorial().ConsultarRegistro(id) as Editorial;

                if (editorial == null)
                {
                    alerta.SetError(txtId, "No existe una editorial con ese id.");
                    return;
                }

                // Asignación de datos a los controles definidos en el Designer
                txtId.Text = editorial.Id.ToString();
                txtNombreEditorial.Text = editorial.Nombre;
                txtPaisOrigen.Text = editorial.PaisOrigen;
                txtAnioFundacion.Text = editorial.AnioFundacion.ToString();
                chkEstadoEditorial.Checked = editorial.EsActivo;
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
                Editorial editorial = CrearEditorialDesdeControles();
                editorial.ActualizarRegistro(editorial);

                MessageBox.Show("Editorial actualizada correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                new Editorial().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Editorial eliminada correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        // ---------- Métodos auxiliares ----------
        private Editorial CrearEditorialDesdeControles()
        {
            int id = LeerEntero(txtId.Text, "El id de la editorial");
            int anioFundacion = LeerEntero(txtAnioFundacion.Text, "El año de fundación");

            // Asumiendo que las propiedades de tu modelo se llaman así.
            Editorial editorial = new Editorial()
            {
                Id = id,
                Nombre = txtNombreEditorial.Text,
                PaisOrigen = txtPaisOrigen.Text,
                AnioFundacion = anioFundacion,
                EsActivo = chkEstadoEditorial.Checked
            };

            return editorial;
        }

        // Regla adicional del Modelo: el año de fundación debe estar entre 1400 y el año actual
        private string? ValidarAnioFundacion(string texto)
        {
            if (!int.TryParse(texto, out int anio) || anio < 1400 || anio > DateTime.Now.Year)
                return "El año debe estar entre 1400 y " + DateTime.Now.Year + ".";
            return null;
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
            txtNombreEditorial.Clear();
            txtPaisOrigen.Clear();
            txtAnioFundacion.Clear();
            chkEstadoEditorial.Checked = false;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }
    }
}