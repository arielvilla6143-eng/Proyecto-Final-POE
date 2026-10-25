// Actividad 2.4 - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
namespace SistemaBiblioteca1
{
    partial class FrmPrestamo
    {

        private void InitializeComponent()
        {
            lblIdUsuarioPrestamo = new Label();
            txtIdEjemplarAgregar = new TextBox();
            lblIdEjemplarPrestamo = new Label();
            txtIdEjemplarPrestamo = new TextBox();
            lblFechaPrestamo = new Label();
            dtpFechaPrestamo = new DateTimePicker();
            lblFechaLimite = new Label();
            dtpFechaLimite = new DateTimePicker();
            chkDevuelto = new CheckBox();
            chkEstadoPrestamo = new CheckBox();
            btnAgregarDetalle = new Button();
            dgvEjemplares = new DataGridView();
            btnEliminarDetalle = new Button();
            btnVaciarDetalle = new Button();
            label1 = new Label();
            lblContadorDetalle = new Label();
            label2 = new Label();
            pnlFormularioBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEjemplares).BeginInit();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Controls.Add(label2);
            pnlFormularioBase.Controls.Add(lblContadorDetalle);
            pnlFormularioBase.Controls.Add(label1);
            pnlFormularioBase.Controls.Add(btnVaciarDetalle);
            pnlFormularioBase.Controls.Add(btnEliminarDetalle);
            pnlFormularioBase.Controls.Add(dgvEjemplares);
            pnlFormularioBase.Controls.Add(btnAgregarDetalle);
            pnlFormularioBase.Controls.Add(lblIdUsuarioPrestamo);
            pnlFormularioBase.Controls.Add(txtIdEjemplarAgregar);
            pnlFormularioBase.Controls.Add(lblIdEjemplarPrestamo);
            pnlFormularioBase.Controls.Add(txtIdEjemplarPrestamo);
            pnlFormularioBase.Controls.Add(lblFechaPrestamo);
            pnlFormularioBase.Controls.Add(dtpFechaPrestamo);
            pnlFormularioBase.Controls.Add(lblFechaLimite);
            pnlFormularioBase.Controls.Add(dtpFechaLimite);
            pnlFormularioBase.Controls.Add(chkDevuelto);
            pnlFormularioBase.Controls.Add(chkEstadoPrestamo);
            pnlFormularioBase.Paint += pnlFormularioBase_Paint;
            pnlFormularioBase.Controls.SetChildIndex(chkEstadoPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(chkDevuelto, 0);
            pnlFormularioBase.Controls.SetChildIndex(dtpFechaLimite, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblFechaLimite, 0);
            pnlFormularioBase.Controls.SetChildIndex(dtpFechaPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblFechaPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtIdEjemplarPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblIdEjemplarPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtIdEjemplarAgregar, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblIdUsuarioPrestamo, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtId, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblId, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnAgregarDetalle, 0);
            pnlFormularioBase.Controls.SetChildIndex(dgvEjemplares, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnEliminarDetalle, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnVaciarDetalle, 0);
            pnlFormularioBase.Controls.SetChildIndex(label1, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblContadorDetalle, 0);
            pnlFormularioBase.Controls.SetChildIndex(label2, 0);
            // 
            // lblId
            // 
            lblId.Location = new Point(13, 16);
            // 
            // txtId
            // 
            txtId.Location = new Point(181, 13);
            txtId.Size = new Size(145, 27);
            // 
            // lblIdUsuarioPrestamo
            // 
            lblIdUsuarioPrestamo.AutoSize = true;
            lblIdUsuarioPrestamo.Location = new Point(16, 47);
            lblIdUsuarioPrestamo.Margin = new Padding(2, 0, 2, 0);
            lblIdUsuarioPrestamo.Name = "lblIdUsuarioPrestamo";
            lblIdUsuarioPrestamo.Size = new Size(81, 20);
            lblIdUsuarioPrestamo.TabIndex = 2;
            lblIdUsuarioPrestamo.Text = "ID Usuario:";
            // 
            // txtIdEjemplarAgregar
            // 
            txtIdEjemplarAgregar.Location = new Point(181, 128);
            txtIdEjemplarAgregar.Margin = new Padding(2);
            txtIdEjemplarAgregar.Name = "txtIdEjemplarAgregar";
            txtIdEjemplarAgregar.Size = new Size(145, 27);
            txtIdEjemplarAgregar.TabIndex = 3;
            txtIdEjemplarAgregar.TextChanged += txtIdEjemplarAgregar_TextChanged;
            // 
            // lblIdEjemplarPrestamo
            // 
            lblIdEjemplarPrestamo.AutoSize = true;
            lblIdEjemplarPrestamo.Location = new Point(11, 131);
            lblIdEjemplarPrestamo.Margin = new Padding(2, 0, 2, 0);
            lblIdEjemplarPrestamo.Name = "lblIdEjemplarPrestamo";
            lblIdEjemplarPrestamo.Size = new Size(90, 20);
            lblIdEjemplarPrestamo.TabIndex = 4;
            lblIdEjemplarPrestamo.Text = "ID Ejemplar:";
            // 
            // txtIdEjemplarPrestamo
            // 
            txtIdEjemplarPrestamo.Location = new Point(181, 55);
            txtIdEjemplarPrestamo.Margin = new Padding(2);
            txtIdEjemplarPrestamo.Name = "txtIdEjemplarPrestamo";
            txtIdEjemplarPrestamo.Size = new Size(145, 27);
            txtIdEjemplarPrestamo.TabIndex = 5;
            // 
            // lblFechaPrestamo
            // 
            lblFechaPrestamo.AutoSize = true;
            lblFechaPrestamo.Location = new Point(340, 16);
            lblFechaPrestamo.Margin = new Padding(2, 0, 2, 0);
            lblFechaPrestamo.Name = "lblFechaPrestamo";
            lblFechaPrestamo.Size = new Size(138, 20);
            lblFechaPrestamo.TabIndex = 6;
            lblFechaPrestamo.Text = "Fecha de préstamo:";
            // 
            // dtpFechaPrestamo
            // 
            dtpFechaPrestamo.Format = DateTimePickerFormat.Short;
            dtpFechaPrestamo.Location = new Point(481, 16);
            dtpFechaPrestamo.Margin = new Padding(2);
            dtpFechaPrestamo.Name = "dtpFechaPrestamo";
            dtpFechaPrestamo.Size = new Size(157, 27);
            dtpFechaPrestamo.TabIndex = 7;
            // 
            // lblFechaLimite
            // 
            lblFechaLimite.AutoSize = true;
            lblFechaLimite.Location = new Point(340, 52);
            lblFechaLimite.Margin = new Padding(2, 0, 2, 0);
            lblFechaLimite.Name = "lblFechaLimite";
            lblFechaLimite.Size = new Size(92, 20);
            lblFechaLimite.TabIndex = 8;
            lblFechaLimite.Text = "Fecha límite:";
            // 
            // dtpFechaLimite
            // 
            dtpFechaLimite.Format = DateTimePickerFormat.Short;
            dtpFechaLimite.Location = new Point(481, 47);
            dtpFechaLimite.Margin = new Padding(2);
            dtpFechaLimite.Name = "dtpFechaLimite";
            dtpFechaLimite.Size = new Size(157, 27);
            dtpFechaLimite.TabIndex = 9;
            // 
            // chkDevuelto
            // 
            chkDevuelto.AutoSize = true;
            chkDevuelto.Location = new Point(387, 89);
            chkDevuelto.Margin = new Padding(2);
            chkDevuelto.Name = "chkDevuelto";
            chkDevuelto.Size = new Size(91, 24);
            chkDevuelto.TabIndex = 10;
            chkDevuelto.Text = "Devuelto";
            chkDevuelto.UseVisualStyleBackColor = true;
            // 
            // chkEstadoPrestamo
            // 
            chkEstadoPrestamo.AutoSize = true;
            chkEstadoPrestamo.Location = new Point(504, 89);
            chkEstadoPrestamo.Margin = new Padding(2);
            chkEstadoPrestamo.Name = "chkEstadoPrestamo";
            chkEstadoPrestamo.Size = new Size(73, 24);
            chkEstadoPrestamo.TabIndex = 11;
            chkEstadoPrestamo.Text = "Activo";
            chkEstadoPrestamo.UseVisualStyleBackColor = true;
            // 
            // btnAgregarDetalle
            // 
            btnAgregarDetalle.Location = new Point(384, 126);
            btnAgregarDetalle.Name = "btnAgregarDetalle";
            btnAgregarDetalle.Size = new Size(151, 29);
            btnAgregarDetalle.TabIndex = 14;
            btnAgregarDetalle.Text = "Agregar";
            btnAgregarDetalle.UseVisualStyleBackColor = true;
            btnAgregarDetalle.Click += btnAgregarDetalle_Click;
            // 
            // dgvEjemplares
            // 
            dgvEjemplares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEjemplares.Location = new Point(3, 166);
            dgvEjemplares.Name = "dgvEjemplares";
            dgvEjemplares.RowHeadersWidth = 51;
            dgvEjemplares.Size = new Size(634, 95);
            dgvEjemplares.TabIndex = 16;
            // 
            // btnEliminarDetalle
            // 
            btnEliminarDetalle.Location = new Point(384, 270);
            btnEliminarDetalle.Name = "btnEliminarDetalle";
            btnEliminarDetalle.Size = new Size(94, 29);
            btnEliminarDetalle.TabIndex = 17;
            btnEliminarDetalle.Text = "Eliminar";
            btnEliminarDetalle.UseVisualStyleBackColor = true;
            btnEliminarDetalle.Click += btnEliminarDetalle_Click;
            // 
            // btnVaciarDetalle
            // 
            btnVaciarDetalle.Location = new Point(504, 270);
            btnVaciarDetalle.Name = "btnVaciarDetalle";
            btnVaciarDetalle.Size = new Size(94, 29);
            btnVaciarDetalle.TabIndex = 18;
            btnVaciarDetalle.Text = "Vaciar";
            btnVaciarDetalle.UseVisualStyleBackColor = true;
            btnVaciarDetalle.Click += btnVaciarDetalle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 279);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 19;
            label1.Text = "Total ítems:";
            // 
            // lblContadorDetalle
            // 
            lblContadorDetalle.AutoSize = true;
            lblContadorDetalle.Location = new Point(94, 279);
            lblContadorDetalle.Name = "lblContadorDetalle";
            lblContadorDetalle.Size = new Size(0, 20);
            lblContadorDetalle.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 299);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 21;
            label2.Text = "Equipo #2";
            // 
            // FrmPrestamo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 360);
            Name = "FrmPrestamo";
            Text = "FrmPrestamo";
            pnlFormularioBase.ResumeLayout(false);
            pnlFormularioBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEjemplares).EndInit();
            ResumeLayout(false);
        }



        private Label lblIdUsuarioPrestamo;
        private TextBox txtIdEjemplarAgregar;
        private Label lblIdEjemplarPrestamo;
        private TextBox txtIdEjemplarPrestamo;
        private Label lblFechaPrestamo;
        private DateTimePicker dtpFechaPrestamo;
        private Label lblFechaLimite;
        private DateTimePicker dtpFechaLimite;
        private CheckBox chkDevuelto;
        private CheckBox chkEstadoPrestamo;
        private Button btnAgregarDetalle;
        private DataGridView dgvEjemplares;
        private Label lblContadorDetalle;
        private Label label1;
        private Button btnVaciarDetalle;
        private Button btnEliminarDetalle;
        private Label label2;
    }
}