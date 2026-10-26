// Actividad 3.3 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
using System;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    public partial class FrmMotivoEliminar : Form
    {
        // Lo que el formulario padre lee después de mostrar dialogo
        public string Motivo
        {
            get { return txtMotivo.Text.Trim(); }
        }

        public FrmMotivoEliminar(string idPrestamo)
        {
            InitializeComponent();
            lblMotivo.Text = "Motivo de eliminar el préstamo " + idPrestamo + ":";
            FormClosing += FrmMotivoEliminar_FormClosing;
        }

        // no se va a cerrar el formulario si el motivo está vacío
        private void FrmMotivoEliminar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Escribe un motivo para continuar.", "Motivo requerido",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                DialogResult = DialogResult.None;
                txtMotivo.Focus();
            }
        }
    }
}