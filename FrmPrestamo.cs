// Actividad 2.7 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
using SistemaBiblioteca1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    public partial class FrmPrestamo : FrmBase, IPanelCRUD
    {
        public FrmPrestamo()
        {
            InitializeComponent();

            // ---------- Actividad 2.5: validación de la Vista ----------
            ConfigurarCampoEntero(txtIdEjemplarAgregar, "ID del usuario");
            ConfigurarCampoEntero(txtIdEjemplarPrestamo, "ID del ejemplar");
            // Los botones del detalle no deben quedar bloqueados por el Validating de los campos
            btnAgregarDetalle.CausesValidation = false;
            btnEliminarDetalle.CausesValidation = false;
            btnVaciarDetalle.CausesValidation = false;
            ConfigurarGrid();
            ActualizarContador();
        }

        public void EjecutarGuardar()
        {
            // Actividad 2.5: ValidateChildren() + bandera de errores del ErrorProvider.
            // Si la pantalla no está limpia se detiene el proceso y no se empaqueta nada hacia el Modelo.
            if (!ValidarFormulario())
                return;

            try
            {
                Prestamo prestamo = CrearPrestamoDesdeControles();
                prestamo.InsertarRegistro(prestamo);

                MessageBox.Show("Préstamo guardado correctamente.", "Guardar",
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
                Prestamo prestamo = new Prestamo().ConsultarRegistro(id) as Prestamo;

                if (prestamo == null)
                {
                    alerta.SetError(txtId, "No existe un préstamo con ese id.");
                    return;
                }

                txtId.Text = prestamo.Id.ToString();
                txtIdEjemplarAgregar.Text = prestamo.IdUsuario.ToString();
                txtIdEjemplarPrestamo.Text = prestamo.IdEjemplar.ToString();
                dtpFechaPrestamo.Value = prestamo.FechaPrestamo;
                dtpFechaLimite.Value = prestamo.FechaLimite;
                chkDevuelto.Checked = prestamo.Devuelto;
                chkEstadoPrestamo.Checked = prestamo.EsActivo;
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
                Prestamo prestamo = CrearPrestamoDesdeControles();

                // Conservar los datos originales
                Prestamo existente = new Prestamo().ConsultarRegistro(txtId.Text) as Prestamo;
                if (existente != null)
                {
                    prestamo.FechaRegistro = existente.FechaRegistro;

                    // Si ya estaba devuelto conserva la fecha de devolución original
                    if (chkDevuelto.Checked && existente.Devuelto)
                        prestamo.FechaDevolucionReal = existente.FechaDevolucionReal;
                }

                prestamo.ActualizarRegistro(prestamo);

                MessageBox.Show("Préstamo actualizado correctamente.", "Actualizar",
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
                new Prestamo().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Préstamo eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        // Métodos auxiliares 
        private Prestamo CrearPrestamoDesdeControles()
        {
            int id = LeerEntero(txtId.Text, "El id");
            int idUsuario = LeerEntero(txtIdEjemplarAgregar.Text, "El id del usuario");
            int idEjemplar = LeerEntero(txtIdEjemplarPrestamo.Text, "El id del ejemplar");

            Prestamo prestamo = new Prestamo(id, idUsuario, idEjemplar, dtpFechaPrestamo.Value,
                                             dtpFechaLimite.Value, chkEstadoPrestamo.Checked);
            prestamo.Devuelto = chkDevuelto.Checked;
            return prestamo;
        }

        private int LeerEntero(string texto, string nombreCampo)
        {
            if (!int.TryParse(texto, out int valor))
                throw new FormatException(nombreCampo + " debe ser un número entero.");
            return valor;
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtIdEjemplarAgregar.Clear();
            txtIdEjemplarPrestamo.Clear();
            dtpFechaPrestamo.Value = DateTime.Now;
            dtpFechaLimite.Value = DateTime.Now;
            chkDevuelto.Checked = false;
            chkEstadoPrestamo.Checked = false;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }

        private void pnlFormularioBase_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtIdEjemplarAgregar_TextChanged(object sender, EventArgs e)
        {

        }
        private void ConfigurarGrid()
        {
            if (dgvEjemplares.Columns.Count == 0)
            {
                dgvEjemplares.Columns.Add("IdEjemplar", "ID Ejemplar");
                dgvEjemplares.AllowUserToAddRows = false;
                dgvEjemplares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }
        private void FrmPrestamo_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            ActualizarContador();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvEjemplares.Columns.Count == 0)
                if (dgvEjemplares.Columns.Count == 0)
                {
                    ConfigurarGrid();
                }

            if (string.IsNullOrWhiteSpace(txtIdEjemplarAgregar.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del ejemplar.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdEjemplarAgregar.Focus();
                return;
            }

            if (!int.TryParse(txtIdEjemplarAgregar.Text, out _))
            {
                MessageBox.Show("El ID del ejemplar debe contener únicamente números.", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIdEjemplarAgregar.SelectAll();
                txtIdEjemplarAgregar.Focus();
                return;
            }

            foreach (DataGridViewRow fila in dgvEjemplares.Rows)
            {
                if (fila.Cells["IdEjemplar"].Value != null && fila.Cells["IdEjemplar"].Value.ToString() == txtIdEjemplarAgregar.Text.Trim())
                {
                    MessageBox.Show("Este ejemplar ya se encuentra agregado en la lista.", "Ejemplar duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdEjemplarAgregar.SelectAll();
                    txtIdEjemplarAgregar.Focus();
                    return;
                }
            }

            dgvEjemplares.Rows.Add(txtIdEjemplarAgregar.Text.Trim());
            txtIdEjemplarAgregar.Clear();
            txtIdEjemplarAgregar.Focus();
            ActualizarContador();
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvEjemplares.Rows.Count == 0)
            {
                MessageBox.Show("No hay ejemplares en la lista para eliminar.", "Lista vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvEjemplares.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la lista para eliminar el ejemplar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvEjemplares.Rows.RemoveAt(dgvEjemplares.SelectedRows[0].Index);
            ActualizarContador();
        }

        private void btnVaciarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvEjemplares.Rows.Count == 0)
            {
                MessageBox.Show("La lista de ejemplares ya está vacía.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea remover todos los ejemplares de la lista?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                dgvEjemplares.Rows.Clear();
                ActualizarContador();
            }
        }
        private void ActualizarContador()
        {
            lblContadorDetalle.Text = dgvEjemplares.Rows.Count.ToString();
        }
    }
}