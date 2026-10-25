// Actividad 2.4 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
using SistemaBiblioteca1;
using SistemaBiblioteca1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaBiblioteca1
{
    public partial class FrmUsuario : FrmBase, IPanelCRUD
    {
        public FrmUsuario()
        {
            InitializeComponent();

            // ---------- Actividad 2.5: validación de la Vista ----------
            ConfigurarCampoLetras(txtNombreUsuario, "Nombre");
            ConfigurarCampoLetras(txtApellidoUsuario, "Apellido");
            ConfigurarCampoTexto(txtEmailUsuario, "Correo electrónico", t => (!t.Contains('@') || !t.Contains('.')) ? "El correo debe contener '@' y un punto." : null);
            ConfigurarCampoEntero(txtTelefonoUsuario, "Teléfono", t => t.Length != 10 ? "El teléfono debe tener exactamente 10 dígitos." : null);
            ConfigurarCampoTexto(txtIdUniversitarioUsuario, "ID universitario");
        }

        public void EjecutarGuardar()
        {
            // Actividad 2.5: ValidateChildren() + bandera de errores del ErrorProvider.
            // Si la pantalla no está limpia se detiene el proceso y no se empaqueta nada hacia el Modelo.
            if (!ValidarFormulario())
                return;

            try
            {
                Usuario usuario = CrearUsuarioDesdeControles();
                usuario.InsertarRegistro(usuario);

                MessageBox.Show("Usuario guardado correctamente.", "Guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.SetError(txtId, string.Empty);

            try
            {
                Usuario usuario = new Usuario().ConsultarRegistro(id) as Usuario;

                if (usuario == null)
                {
                    alerta.SetError(txtId, "No existe un usuario con ese id.");
                    return;
                }

                txtId.Text = usuario.Id.ToString();
                txtNombreUsuario.Text = usuario.Nombre;
                txtApellidoUsuario.Text = usuario.Apellido;
                txtEmailUsuario.Text = usuario.Email;
                txtTelefonoUsuario.Text = usuario.Telefono;
                txtIdUniversitarioUsuario.Text = usuario.IdUniversitario;
                chkEstadoUsuario.Checked = usuario.EsActivo;
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
                Usuario usuario = CrearUsuarioDesdeControles();

                // Conserva los datos que el formulario no edita
                Usuario existente = new Usuario().ConsultarRegistro(txtId.Text) as Usuario;
                if (existente != null)
                {
                    usuario.ContadorSanciones = existente.ContadorSanciones;
                    usuario.EstaBaneado = existente.EstaBaneado;
                    usuario.FechaRegistro = existente.FechaRegistro;
                }

                usuario.ActualizarRegistro(usuario);

                MessageBox.Show("Usuario actualizado correctamente.", "Actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            try
            {
                new Usuario().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Usuario eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        // Métodos auxiliares 
        private Usuario CrearUsuarioDesdeControles()
        {
            if (!int.TryParse(txtId.Text, out int id))
                throw new FormatException("El id debe ser un número entero.");

            return new Usuario(id, txtNombreUsuario.Text, txtApellidoUsuario.Text, txtEmailUsuario.Text,
                               txtTelefonoUsuario.Text, txtIdUniversitarioUsuario.Text, chkEstadoUsuario.Checked);
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtNombreUsuario.Clear();
            txtApellidoUsuario.Clear();
            txtEmailUsuario.Clear();
            txtTelefonoUsuario.Clear();
            txtIdUniversitarioUsuario.Clear();
            chkEstadoUsuario.Checked = false;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }

        private void lblTelefonoUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}