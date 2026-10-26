// Actividad 2.4 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    /// <summary>
    /// Formulario base de todas las vistas CRUD.
    /// Centraliza la validación de la capa Vista:
    ///   1) KeyPress  -> filtros sanitarios (solo letras, solo números, decimal con un único punto)
    ///   2) Validating -> campo obligatorio / ComboBox sin selección (índice -1)
    ///   3) ErrorProvider -> SetError(control, mensaje) al fallar y SetError(control, "") al corregir
    ///   4) ValidarFormulario() -> ValidateChildren() + bandera de errores activos (se usa al guardar)
    /// Los formularios hijos solo indican qué tipo de dato lleva cada control.
    /// </summary>
    public partial class FrmBase : Form
    {
        // Tipos de dato que puede capturar un campo
        private enum TipoCampo
        {
            Texto,      // cualquier texto, solo se exige que no esté vacío
            Letras,     // solo letras y espacios
            Entero,     // solo dígitos
            Decimal     // dígitos y un único punto decimal
        }

        // Regla de validación asociada a cada control
        private class ReglaCampo
        {
            public string Nombre = "";
            public TipoCampo Tipo = TipoCampo.Texto;
            public bool Obligatorio = true;
            public Func<string, string?>? ReglaExtra = null; // devuelve el mensaje de error o null si es válido
        }

        private readonly Dictionary<Control, ReglaCampo> reglas = new Dictionary<Control, ReglaCampo>();

        public FrmBase()
        {
            InitializeComponent();

            // El Id es numérico y obligatorio en todas las vistas heredadas
            ConfigurarCampoEntero(txtId, "ID");
        }

        // =====================================================================
        //  Configuración de campos (los hijos las llaman después de InitializeComponent)
        // =====================================================================

        /// <summary>Campo de texto libre que solo admite letras (nombres, apellidos, descripciones...).</summary>
        protected void ConfigurarCampoLetras(TextBox txt, string nombreCampo, bool obligatorio = true)
        {
            RegistrarRegla(txt, nombreCampo, TipoCampo.Letras, obligatorio, null);
            txt.KeyPress += SoloLetras_KeyPress;
            txt.Validating += Campo_Validating;
            txt.TextChanged += Campo_Cambio;
        }

        /// <summary>Campo de texto sin restricción de caracteres, solo obligatorio.</summary>
        protected void ConfigurarCampoTexto(TextBox txt, string nombreCampo, Func<string, string?>? reglaExtra = null)
        {
            RegistrarRegla(txt, nombreCampo, TipoCampo.Texto, true, reglaExtra);
            txt.Validating += Campo_Validating;
            txt.TextChanged += Campo_Cambio;
        }

        /// <summary>Campo numérico entero (cantidades, ids, años...).</summary>
        protected void ConfigurarCampoEntero(TextBox txt, string nombreCampo, Func<string, string?>? reglaExtra = null)
        {
            RegistrarRegla(txt, nombreCampo, TipoCampo.Entero, true, reglaExtra);
            txt.KeyPress += SoloNumeros_KeyPress;
            txt.Validating += Campo_Validating;
            txt.TextChanged += Campo_Cambio;
        }

        /// <summary>Campo de precisión (precios, montos, porcentajes) con un único punto decimal.</summary>
        protected void ConfigurarCampoDecimal(TextBox txt, string nombreCampo, Func<string, string?>? reglaExtra = null)
        {
            RegistrarRegla(txt, nombreCampo, TipoCampo.Decimal, true, reglaExtra);
            txt.KeyPress += NumeroDecimal_KeyPress;
            txt.Validating += Campo_Validating;
            txt.TextChanged += Campo_Cambio;
        }

        /// <summary>Lista desplegable obligatoria: no puede quedarse en el índice -1.</summary>
        protected void ConfigurarCampoLista(ComboBox cmb, string nombreCampo)
        {
            // DropDownList garantiza que SelectedIndex refleje lo que ve el usuario
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            RegistrarRegla(cmb, nombreCampo, TipoCampo.Texto, true, null);
            cmb.Validating += Campo_Validating;
            cmb.SelectedIndexChanged += Campo_Cambio;
        }

        private void RegistrarRegla(Control control, string nombreCampo, TipoCampo tipo,
                                    bool obligatorio, Func<string, string?>? reglaExtra)
        {
            ReglaCampo regla = new ReglaCampo();
            regla.Nombre = nombreCampo;
            regla.Tipo = tipo;
            regla.Obligatorio = obligatorio;
            regla.ReglaExtra = reglaExtra;
            reglas[control] = regla;
        }

        // =====================================================================
        //  1) Evento KeyPress: filtros sanitarios
        // =====================================================================

        /// <summary>Solo letras, espacio y teclas de control (retroceso, etc.).</summary>
        private void SoloLetras_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;               // descarta la pulsación
                SystemSounds.Beep.Play();       // sonido del sistema
            }
        }

        /// <summary>Solo dígitos y teclas de control.</summary>
        private void SoloNumeros_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        /// <summary>Dígitos, teclas de control y un único punto decimal.</summary>
        private void NumeroDecimal_KeyPress(object? sender, KeyPressEventArgs e)
        {
            TextBox? txt = sender as TextBox;
            if (txt == null)
                return;

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                return;

            if (e.KeyChar == '.')
            {
                // Se bloquea el punto si ya existe uno (salvo que el punto esté seleccionado y vaya a reemplazarse)
                bool yaTienePunto = txt.Text.Contains('.') && !txt.SelectedText.Contains('.');
                if (!yaTienePunto)
                    return;
            }

            e.Handled = true;
            SystemSounds.Beep.Play();
        }

        // =====================================================================
        //  2) y 3) Evento Validating + ErrorProvider
        // =====================================================================

        private void Campo_Validating(object? sender, CancelEventArgs e)
        {
            Control? control = sender as Control;
            if (control == null)
                return;

            string? mensaje = ObtenerMensajeError(control);

            if (mensaje != null)
            {
                epValidaciones.SetError(control, mensaje);  // dibuja el icono de advertencia
                e.Cancel = true;                            // el cursor no sale del control hasta corregir
            }
            else
            {
                epValidaciones.SetError(control, "");       // limpieza obligatoria del error
            }
        }

        /// <summary>
        /// En cuanto el usuario corrige el dato (escribe o selecciona algo válido)
        /// se quita el icono sin esperar a que el control pierda el enfoque.
        /// </summary>
        private void Campo_Cambio(object? sender, EventArgs e)
        {
            Control? control = sender as Control;
            if (control == null)
                return;

            if (epValidaciones.GetError(control) != "" && ObtenerMensajeError(control) == null)
                epValidaciones.SetError(control, "");
        }

        /// <summary>Evalúa el control con su regla. Devuelve el mensaje de error o null si el dato es consistente.</summary>
        private string? ObtenerMensajeError(Control control)
        {
            if (!reglas.TryGetValue(control, out ReglaCampo? regla))
                return null;

            // ComboBox: el índice por defecto (-1) significa que no se eligió nada
            if (control is ComboBox combo)
            {
                if (combo.SelectedIndex == -1)
                    return "Seleccione una opción en '" + regla.Nombre + "'.";
                return null;
            }

            // Cadena vacía
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                if (regla.Obligatorio)
                    return "El campo '" + regla.Nombre + "' es obligatorio.";
                return null;
            }

            string texto = control.Text.Trim();

            // Se revisa el contenido completo (el KeyPress no detecta texto pegado con Ctrl+V)
            if (regla.Tipo == TipoCampo.Letras)
            {
                foreach (char c in texto)
                {
                    if (!char.IsLetter(c) && c != ' ')
                        return "'" + regla.Nombre + "' solo admite letras.";
                }
            }
            else if (regla.Tipo == TipoCampo.Entero)
            {
                foreach (char c in texto)
                {
                    if (!char.IsDigit(c))
                        return "'" + regla.Nombre + "' solo admite números.";
                }
            }
            else if (regla.Tipo == TipoCampo.Decimal)
            {
                int puntos = 0;
                int digitos = 0;
                foreach (char c in texto)
                {
                    if (c == '.')
                        puntos++;
                    else if (char.IsDigit(c))
                        digitos++;
                    else
                        return "'" + regla.Nombre + "' solo admite números y un punto decimal.";
                }
                if (puntos > 1 || digitos == 0)
                    return "'" + regla.Nombre + "' no es un número decimal válido.";
            }

            // Regla adicional del formulario hijo (longitud, formato, rango...)
            if (regla.ReglaExtra != null)
                return regla.ReglaExtra(texto);

            return null;
        }

        // =====================================================================
        //  4) Orquestación al guardar / actualizar
        // =====================================================================

        /// <summary>
        /// Fuerza el evento Validating de TODOS los controles con this.ValidateChildren() y revisa con
        /// una bandera si el ErrorProvider tiene algún error activo. Devuelve false si la pantalla no está limpia.
        /// </summary>
        protected bool ValidarFormulario()
        {
            bool hayErrores = false;

            if (!this.ValidateChildren())
                hayErrores = true;

            Control? primerConError = BuscarControlConError(this);
            if (primerConError != null)
                hayErrores = true;

            if (hayErrores)
            {
                MessageBox.Show("Hay campos con errores. Corríjalos antes de continuar.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                primerConError?.Focus();
                return false;
            }

            return true;
        }

        /// <summary>Recorre los controles buscando alguno que tenga un error activo en el ErrorProvider.</summary>
        private Control? BuscarControlConError(Control contenedor)
        {
            foreach (Control hijo in contenedor.Controls)
            {
                if (epValidaciones.GetError(hijo) != "")
                    return hijo;

                Control? interno = BuscarControlConError(hijo);
                if (interno != null)
                    return interno;
            }
            return null;
        }

        /// <summary>Quita todos los iconos de error (se usa al limpiar la pantalla).</summary>
        protected void LimpiarErrores()
        {
            epValidaciones.Clear();
        }

        private void pnlFormularioBase_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}