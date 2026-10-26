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
    // 1. Se establece la herencia de FrmBase y el contrato IPanelCRUD
    public partial class FrmAdministrador : FrmBase, IPanelCRUD
    {
        public FrmAdministrador()
        {
            InitializeComponent();

            // ---------- Actividad 2.5: validación de la Vista ----------
            ConfigurarCampoLetras(txtNombreAdmin, "Nombre");
            ConfigurarCampoLetras(txtApellidoAdmin, "Apellido");
            ConfigurarCampoTexto(txtUsuarioAcceso, "Usuario de acceso");
            ConfigurarCampoTexto(txtContrasena, "Contraseña");
            ConfigurarCampoLista(cmbNivelAcceso, "Nivel de acceso");
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
                Administrador admin = CrearAdminDesdeControles();
                admin.InsertarRegistro(admin);

                MessageBox.Show("Administrador guardado correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Administrador admin = new Administrador().ConsultarRegistro(id) as Administrador;

                if (admin == null)
                {
                    alerta.SetError(txtId, "No existe un administrador con ese id.");
                    return;
                }

                // Asignación de datos a los controles definidos en el Designer
                txtId.Text = admin.Id.ToString();
                txtNombreAdmin.Text = admin.Nombre;
                txtApellidoAdmin.Text = admin.Apellido;
                txtUsuarioAcceso.Text = admin.UsuarioAcceso;
                txtContrasena.Text = admin.Contrasena;
                cmbNivelAcceso.Text = admin.NivelAcceso.ToString();
                chkEstadoAdmin.Checked = admin.EsActivo;
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
                Administrador admin = CrearAdminDesdeControles();
                admin.ActualizarRegistro(admin);

                MessageBox.Show("Administrador actualizado correctamente.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                new Administrador().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Administrador eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        // ---------- Métodos auxiliares ----------
        private Administrador CrearAdminDesdeControles()
        {
            int id = LeerEntero(txtId.Text, "El id del administrador");

            // Validamos el nivel de acceso desde el ComboBox
            int nivelAcceso = 1; // Nivel por defecto
            if (!string.IsNullOrEmpty(cmbNivelAcceso.Text))
            {
                nivelAcceso = LeerEntero(cmbNivelAcceso.Text, "El nivel de acceso");
            }

            // Asumiendo que las propiedades de tu modelo se llaman así.
            // Ajusta los nombres de las propiedades si en tu Models/Administrador.cs son diferentes.
            Administrador admin = new Administrador()
            {
                Id = id,
                Nombre = txtNombreAdmin.Text,
                Apellido = txtApellidoAdmin.Text,
                UsuarioAcceso = txtUsuarioAcceso.Text,
                Contrasena = txtContrasena.Text,
                NivelAcceso = nivelAcceso,
                EsActivo = chkEstadoAdmin.Checked
            };

            return admin;
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
            txtNombreAdmin.Clear();
            txtApellidoAdmin.Clear();
            txtUsuarioAcceso.Clear();
            txtContrasena.Clear();
            cmbNivelAcceso.SelectedIndex = -1;
            chkEstadoAdmin.Checked = false;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }
    }
}