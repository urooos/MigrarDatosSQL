namespace MigrarDatosSQL
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // ── Controles declarados ──────────────────────────────────────────
            this.tabControl1           = new System.Windows.Forms.TabControl();
            this.tabNivel1             = new System.Windows.Forms.TabPage();
            this.tabNivel2             = new System.Windows.Forms.TabPage();
            this.tabNivel3             = new System.Windows.Forms.TabPage();
            this.tabAdmin              = new System.Windows.Forms.TabPage();
            // tabLog eliminado – el log ahora vive fuera del TabControl

            // Nivel 1 controles
            this.grpDatos              = new System.Windows.Forms.GroupBox();
            this.lblID                 = new System.Windows.Forms.Label();
            this.txtID                 = new System.Windows.Forms.TextBox();
            this.lblNombre             = new System.Windows.Forms.Label();
            this.txtNombre             = new System.Windows.Forms.TextBox();
            this.lblEdad               = new System.Windows.Forms.Label();
            this.txtEdad               = new System.Windows.Forms.TextBox();
            this.btnGuardar            = new System.Windows.Forms.Button();
            this.btnLeerID             = new System.Windows.Forms.Button();
            this.btnLimpiarCampos      = new System.Windows.Forms.Button();
            this.lblInfoFormula        = new System.Windows.Forms.Label();

            // Nivel 2 controles
            this.grpIndice             = new System.Windows.Forms.GroupBox();
            this.lblBuscarID           = new System.Windows.Forms.Label();
            this.txtBuscarID           = new System.Windows.Forms.TextBox();
            this.btnBuscarIndexado     = new System.Windows.Forms.Button();
            this.btnVerIndice          = new System.Windows.Forms.Button();
            this.lblInfoIndice         = new System.Windows.Forms.Label();

            // Nivel 3 controles
            this.grpSQL                = new System.Windows.Forms.GroupBox();
            this.lblServer             = new System.Windows.Forms.Label();
            this.txtServer             = new System.Windows.Forms.TextBox();
            this.lblDatabase           = new System.Windows.Forms.Label();
            this.txtDatabase           = new System.Windows.Forms.TextBox();
            this.lblUsuario            = new System.Windows.Forms.Label();
            this.txtUsuario            = new System.Windows.Forms.TextBox();
            this.lblPassword           = new System.Windows.Forms.Label();
            this.txtPassword           = new System.Windows.Forms.TextBox();
            this.btnConfigurarConexion = new System.Windows.Forms.Button();
            this.btnCrearTabla         = new System.Windows.Forms.Button();
            this.btnMigrar             = new System.Windows.Forms.Button();
            this.btnConsultarSQL       = new System.Windows.Forms.Button();
            this.lblInfoSQL            = new System.Windows.Forms.Label();

            // Administrar controles
            this.grpAdmin              = new System.Windows.Forms.GroupBox();
            this.lblAdminID            = new System.Windows.Forms.Label();
            this.txtAdminID            = new System.Windows.Forms.TextBox();
            this.lblAdminNombre        = new System.Windows.Forms.Label();
            this.txtAdminNombre        = new System.Windows.Forms.TextBox();
            this.lblAdminEdad          = new System.Windows.Forms.Label();
            this.txtAdminEdad          = new System.Windows.Forms.TextBox();
            this.btnBuscarAdmin        = new System.Windows.Forms.Button();
            this.btnEditarAdmin        = new System.Windows.Forms.Button();
            this.btnEliminarAdmin      = new System.Windows.Forms.Button();

            // Log controles – ahora van directo al Form, fuera del tabControl
            this.pnlLog                = new System.Windows.Forms.Panel();
            this.lblLogTitulo          = new System.Windows.Forms.Label();
            this.txtLog                = new System.Windows.Forms.RichTextBox();
            this.btnLimpiarLog         = new System.Windows.Forms.Button();

            this.tabControl1.SuspendLayout();
            this.tabNivel1.SuspendLayout();
            this.tabNivel2.SuspendLayout();
            this.tabNivel3.SuspendLayout();
            this.tabAdmin.SuspendLayout();
            this.SuspendLayout();

            // ═══════════════════════════════════════════════════════════════
            // tabControl1  – ahora ocupa solo la parte superior del Form
            // ═══════════════════════════════════════════════════════════════
            this.tabControl1.Controls.Add(this.tabNivel1);
            this.tabControl1.Controls.Add(this.tabNivel2);
            this.tabControl1.Controls.Add(this.tabNivel3);
            this.tabControl1.Controls.Add(this.tabAdmin);
            this.tabControl1.Dock     = System.Windows.Forms.DockStyle.None;
            this.tabControl1.Font     = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name     = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size     = new System.Drawing.Size(760, 370);
            this.tabControl1.TabIndex = 0;

            // ═══════════════════════════════════════════════════════════════
            // TAB NIVEL 1 – Archivo Binario
            // ═══════════════════════════════════════════════════════════════
            this.tabNivel1.BackColor = System.Drawing.SystemColors.Control;
            this.tabNivel1.Controls.Add(this.grpDatos);
            this.tabNivel1.Location  = new System.Drawing.Point(4, 24);
            this.tabNivel1.Name      = "tabNivel1";
            this.tabNivel1.Padding   = new System.Windows.Forms.Padding(10);
            this.tabNivel1.Size      = new System.Drawing.Size(752, 342);
            this.tabNivel1.TabIndex  = 0;
            this.tabNivel1.Text      = "Nivel 1 - Archivo Binario";

            // grpDatos
            this.grpDatos.Location  = new System.Drawing.Point(15, 15);
            this.grpDatos.Name      = "grpDatos";
            this.grpDatos.Size      = new System.Drawing.Size(720, 310);
            this.grpDatos.TabIndex  = 0;
            this.grpDatos.TabStop   = false;
            this.grpDatos.Text      = "Datos del Ciudadano - Acceso Directo (Registros de Tamano Fijo)";

            // Info formula
            this.lblInfoFormula.AutoSize  = false;
            this.lblInfoFormula.Location  = new System.Drawing.Point(15, 25);
            this.lblInfoFormula.Name      = "lblInfoFormula";
            this.lblInfoFormula.Size      = new System.Drawing.Size(685, 50);
            this.lblInfoFormula.Text      =
                "Formula de offset:  offset = posicion x tamano_registro\n" +
                "Tamano de registro: Id(4) + Nombre(50 ANSI) + Edad(4) + Padding(2) = 60 bytes";
            this.lblInfoFormula.Font      =
                new System.Drawing.Font("Courier New", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblInfoFormula.ForeColor = System.Drawing.Color.DarkBlue;
            this.grpDatos.Controls.Add(this.lblInfoFormula);

            int col1 = 20, col2 = 120, fila = 85, paso = 40;

            // ID
            this.lblID.Location  = new System.Drawing.Point(col1, fila);
            this.lblID.Name      = "lblID";
            this.lblID.Size      = new System.Drawing.Size(90, 23);
            this.lblID.Text      = "ID Ciudadano:";
            this.txtID.Location  = new System.Drawing.Point(col2, fila - 2);
            this.txtID.Name      = "txtID";
            this.txtID.Size      = new System.Drawing.Size(120, 23);
            this.grpDatos.Controls.Add(this.lblID);
            this.grpDatos.Controls.Add(this.txtID);
            fila += paso;

            // Nombre
            this.lblNombre.Location = new System.Drawing.Point(col1, fila);
            this.lblNombre.Name     = "lblNombre";
            this.lblNombre.Size     = new System.Drawing.Size(90, 23);
            this.lblNombre.Text     = "Nombre:";
            this.txtNombre.Location = new System.Drawing.Point(col2, fila - 2);
            this.txtNombre.Name     = "txtNombre";
            this.txtNombre.Size     = new System.Drawing.Size(280, 23);
            this.grpDatos.Controls.Add(this.lblNombre);
            this.grpDatos.Controls.Add(this.txtNombre);
            fila += paso;

            // Edad
            this.lblEdad.Location = new System.Drawing.Point(col1, fila);
            this.lblEdad.Name     = "lblEdad";
            this.lblEdad.Size     = new System.Drawing.Size(90, 23);
            this.lblEdad.Text     = "Edad:";
            this.txtEdad.Location = new System.Drawing.Point(col2, fila - 2);
            this.txtEdad.Name     = "txtEdad";
            this.txtEdad.Size     = new System.Drawing.Size(80, 23);
            this.grpDatos.Controls.Add(this.lblEdad);
            this.grpDatos.Controls.Add(this.txtEdad);
            fila += paso + 10;

            // Botones Nivel 1
            this.btnGuardar.Location = new System.Drawing.Point(col1, fila);
            this.btnGuardar.Name     = "btnGuardar";
            this.btnGuardar.Size     = new System.Drawing.Size(130, 32);
            this.btnGuardar.Text     = "Guardar en Binario";
            this.btnGuardar.Click   += new System.EventHandler(this.btnGuardar_Click);
            this.grpDatos.Controls.Add(this.btnGuardar);

            this.btnLeerID.Location = new System.Drawing.Point(col1 + 145, fila);
            this.btnLeerID.Name     = "btnLeerID";
            this.btnLeerID.Size     = new System.Drawing.Size(130, 32);
            this.btnLeerID.Text     = "Leer por ID";
            this.btnLeerID.Click   += new System.EventHandler(this.btnLeerID_Click);
            this.grpDatos.Controls.Add(this.btnLeerID);

            this.btnLimpiarCampos.Location = new System.Drawing.Point(col1 + 290, fila);
            this.btnLimpiarCampos.Name     = "btnLimpiarCampos";
            this.btnLimpiarCampos.Size     = new System.Drawing.Size(110, 32);
            this.btnLimpiarCampos.Text     = "Limpiar Campos";
            this.btnLimpiarCampos.Click   += new System.EventHandler(this.btnLimpiarCampos_Click);
            this.grpDatos.Controls.Add(this.btnLimpiarCampos);

            // ═══════════════════════════════════════════════════════════════
            // TAB NIVEL 2 – Indexacion
            // ═══════════════════════════════════════════════════════════════
            this.tabNivel2.BackColor = System.Drawing.SystemColors.Control;
            this.tabNivel2.Controls.Add(this.grpIndice);
            this.tabNivel2.Location  = new System.Drawing.Point(4, 24);
            this.tabNivel2.Name      = "tabNivel2";
            this.tabNivel2.Padding   = new System.Windows.Forms.Padding(10);
            this.tabNivel2.Size      = new System.Drawing.Size(752, 342);
            this.tabNivel2.TabIndex  = 1;
            this.tabNivel2.Text      = "Nivel 2 - Indexacion";

            this.grpIndice.Location  = new System.Drawing.Point(15, 15);
            this.grpIndice.Name      = "grpIndice";
            this.grpIndice.Size      = new System.Drawing.Size(720, 310);
            this.grpIndice.TabIndex  = 0;
            this.grpIndice.TabStop   = false;
            this.grpIndice.Text      = "Busqueda Indexada vs Secuencial";

            this.lblInfoIndice.AutoSize  = false;
            this.lblInfoIndice.Location  = new System.Drawing.Point(15, 25);
            this.lblInfoIndice.Name      = "lblInfoIndice";
            this.lblInfoIndice.Size      = new System.Drawing.Size(685, 50);
            this.lblInfoIndice.Text      =
                "El indice almacena:  ID -> offset en bytes  (acceso O(1))\n" +
                "La busqueda secuencial recorre el archivo registro a registro  (acceso O(n))";
            this.lblInfoIndice.Font      = new System.Drawing.Font("Courier New", 8.5F);
            this.lblInfoIndice.ForeColor = System.Drawing.Color.DarkGreen;
            this.grpIndice.Controls.Add(this.lblInfoIndice);

            this.lblBuscarID.Location = new System.Drawing.Point(20, 90);
            this.lblBuscarID.Name     = "lblBuscarID";
            this.lblBuscarID.Size     = new System.Drawing.Size(65, 23);
            this.lblBuscarID.Text     = "Buscar ID:";
            this.txtBuscarID.Location = new System.Drawing.Point(90, 88);
            this.txtBuscarID.Name     = "txtBuscarID";
            this.txtBuscarID.Size     = new System.Drawing.Size(120, 23);
            this.grpIndice.Controls.Add(this.lblBuscarID);
            this.grpIndice.Controls.Add(this.txtBuscarID);

            this.btnBuscarIndexado.Location = new System.Drawing.Point(20, 125);
            this.btnBuscarIndexado.Name     = "btnBuscarIndexado";
            this.btnBuscarIndexado.Size     = new System.Drawing.Size(200, 32);
            this.btnBuscarIndexado.Text     = "Buscar (Indexado + Secuencial)";
            this.btnBuscarIndexado.Click   += new System.EventHandler(this.btnBuscarIndexado_Click);
            this.grpIndice.Controls.Add(this.btnBuscarIndexado);

            this.btnVerIndice.Location = new System.Drawing.Point(235, 125);
            this.btnVerIndice.Name     = "btnVerIndice";
            this.btnVerIndice.Size     = new System.Drawing.Size(130, 32);
            this.btnVerIndice.Text     = "Ver Indice Completo";
            this.btnVerIndice.Click   += new System.EventHandler(this.btnVerIndice_Click);
            this.grpIndice.Controls.Add(this.btnVerIndice);

            // ═══════════════════════════════════════════════════════════════
            // TAB NIVEL 3 – SQL Server
            // ═══════════════════════════════════════════════════════════════
            this.tabNivel3.BackColor = System.Drawing.SystemColors.Control;
            this.tabNivel3.Controls.Add(this.grpSQL);
            this.tabNivel3.Location  = new System.Drawing.Point(4, 24);
            this.tabNivel3.Name      = "tabNivel3";
            this.tabNivel3.Padding   = new System.Windows.Forms.Padding(10);
            this.tabNivel3.Size      = new System.Drawing.Size(752, 342);
            this.tabNivel3.TabIndex  = 2;
            this.tabNivel3.Text      = "Nivel 3 - SQL Server";

            this.grpSQL.Location = new System.Drawing.Point(15, 15);
            this.grpSQL.Name     = "grpSQL";
            this.grpSQL.Size     = new System.Drawing.Size(720, 310);
            this.grpSQL.TabIndex = 0;
            this.grpSQL.TabStop  = false;
            this.grpSQL.Text     = "Migracion a Base de Datos Relacional - Microsoft.Data.SqlClient";

            this.lblInfoSQL.AutoSize  = false;
            this.lblInfoSQL.Location  = new System.Drawing.Point(15, 25);
            this.lblInfoSQL.Name      = "lblInfoSQL";
            this.lblInfoSQL.Size      = new System.Drawing.Size(685, 45);
            this.lblInfoSQL.Text      =
                "Configure la conexion, cree la tabla y migre los registros del archivo binario.\n" +
                "Paso 1: Configurar -> Paso 2: Crear Tabla -> Paso 3: Migrar -> Paso 4: Consultar";
            this.lblInfoSQL.Font      = new System.Drawing.Font("Courier New", 8.5F);
            this.lblInfoSQL.ForeColor = System.Drawing.Color.DarkRed;
            this.grpSQL.Controls.Add(this.lblInfoSQL);

            int sqlFila = 85, sqlCol1 = 20, sqlCol2 = 110;

            this.lblServer.Location = new System.Drawing.Point(sqlCol1, sqlFila);
            this.lblServer.Name     = "lblServer";
            this.lblServer.Size     = new System.Drawing.Size(75, 23);
            this.lblServer.Text     = "Servidor:";
            this.txtServer.Location = new System.Drawing.Point(sqlCol2, sqlFila - 2);
            this.txtServer.Name     = "txtServer";
            this.txtServer.Size     = new System.Drawing.Size(200, 23);
            this.txtServer.Text     = "localhost";
            this.grpSQL.Controls.Add(this.lblServer);
            this.grpSQL.Controls.Add(this.txtServer);
            sqlFila += 40;

            this.lblDatabase.Location = new System.Drawing.Point(sqlCol1, sqlFila);
            this.lblDatabase.Name     = "lblDatabase";
            this.lblDatabase.Size     = new System.Drawing.Size(85, 23);
            this.lblDatabase.Text     = "Base de Datos:";
            this.txtDatabase.Location = new System.Drawing.Point(sqlCol2, sqlFila - 2);
            this.txtDatabase.Name     = "txtDatabase";
            this.txtDatabase.Size     = new System.Drawing.Size(200, 23);
            this.txtDatabase.Text     = "CiudadanosDB";
            this.grpSQL.Controls.Add(this.lblDatabase);
            this.grpSQL.Controls.Add(this.txtDatabase);
            sqlFila += 40;

            this.lblUsuario.Location = new System.Drawing.Point(sqlCol1, sqlFila);
            this.lblUsuario.Name     = "lblUsuario";
            this.lblUsuario.Size     = new System.Drawing.Size(85, 23);
            this.lblUsuario.Text     = "Usuario:";
            this.txtUsuario.Location = new System.Drawing.Point(sqlCol2, sqlFila - 2);
            this.txtUsuario.Name     = "txtUsuario";
            this.txtUsuario.Size     = new System.Drawing.Size(200, 23);
            this.txtUsuario.Text     = "";
            this.grpSQL.Controls.Add(this.lblUsuario);
            this.grpSQL.Controls.Add(this.txtUsuario);
            sqlFila += 40;

            this.lblPassword.Location = new System.Drawing.Point(sqlCol1, sqlFila);
            this.lblPassword.Name     = "lblPassword";
            this.lblPassword.Size     = new System.Drawing.Size(85, 23);
            this.lblPassword.Text     = "Contraseña:";
            this.txtPassword.Location = new System.Drawing.Point(sqlCol2, sqlFila - 2);
            this.txtPassword.Name     = "txtPassword";
            this.txtPassword.Size     = new System.Drawing.Size(200, 23);
            this.txtPassword.Text     = "";
            this.txtPassword.PasswordChar = '*';
            this.grpSQL.Controls.Add(this.lblPassword);
            this.grpSQL.Controls.Add(this.txtPassword);
            sqlFila += 50;

            this.btnConfigurarConexion.Location = new System.Drawing.Point(sqlCol1, sqlFila);
            this.btnConfigurarConexion.Name     = "btnConfigurarConexion";
            this.btnConfigurarConexion.Size     = new System.Drawing.Size(160, 32);
            this.btnConfigurarConexion.Text     = "1. Configurar Conexion";
            this.btnConfigurarConexion.Click   += new System.EventHandler(this.btnConfigurarConexion_Click);
            this.grpSQL.Controls.Add(this.btnConfigurarConexion);

            this.btnCrearTabla.Location = new System.Drawing.Point(sqlCol1 + 170, sqlFila);
            this.btnCrearTabla.Name     = "btnCrearTabla";
            this.btnCrearTabla.Size     = new System.Drawing.Size(160, 32);
            this.btnCrearTabla.Text     = "2. Crear/Verificar Tabla";
            this.btnCrearTabla.Click   += new System.EventHandler(this.btnCrearTabla_Click);
            this.grpSQL.Controls.Add(this.btnCrearTabla);

            this.btnMigrar.Location = new System.Drawing.Point(sqlCol1 + 340, sqlFila);
            this.btnMigrar.Name     = "btnMigrar";
            this.btnMigrar.Size     = new System.Drawing.Size(160, 32);
            this.btnMigrar.Text     = "3. Migrar Datos";
            this.btnMigrar.Click   += new System.EventHandler(this.btnMigrar_Click);
            this.grpSQL.Controls.Add(this.btnMigrar);

            this.btnConsultarSQL.Location = new System.Drawing.Point(sqlCol1 + 510, sqlFila);
            this.btnConsultarSQL.Name     = "btnConsultarSQL";
            this.btnConsultarSQL.Size     = new System.Drawing.Size(160, 32);
            this.btnConsultarSQL.Text     = "4. Consultar SQL";
            this.btnConsultarSQL.Click   += new System.EventHandler(this.btnConsultarSQL_Click);
            this.grpSQL.Controls.Add(this.btnConsultarSQL);

            // ═══════════════════════════════════════════════════════════════
            // TAB ADMINISTRAR – Editar y Eliminar
            // ═══════════════════════════════════════════════════════════════
            this.tabAdmin.BackColor = System.Drawing.SystemColors.Control;
            this.tabAdmin.Controls.Add(this.grpAdmin);
            this.tabAdmin.Location  = new System.Drawing.Point(4, 24);
            this.tabAdmin.Name      = "tabAdmin";
            this.tabAdmin.Padding   = new System.Windows.Forms.Padding(10);
            this.tabAdmin.Size      = new System.Drawing.Size(752, 342);
            this.tabAdmin.TabIndex  = 3;
            this.tabAdmin.Text      = "Administrar";

            this.grpAdmin.Location = new System.Drawing.Point(15, 15);
            this.grpAdmin.Name     = "grpAdmin";
            this.grpAdmin.Size     = new System.Drawing.Size(720, 310);
            this.grpAdmin.TabIndex = 0;
            this.grpAdmin.TabStop  = false;
            this.grpAdmin.Text     = "Editar / Eliminar Ciudadano";

            int aCol1 = 20, aCol2 = 120, aFila = 30, aPaso = 40;

            this.lblAdminID.Location = new System.Drawing.Point(aCol1, aFila);
            this.lblAdminID.Name     = "lblAdminID";
            this.lblAdminID.Size     = new System.Drawing.Size(90, 23);
            this.lblAdminID.Text     = "Buscar por ID:";
            this.txtAdminID.Location = new System.Drawing.Point(aCol2, aFila - 2);
            this.txtAdminID.Name     = "txtAdminID";
            this.txtAdminID.Size     = new System.Drawing.Size(120, 23);
            this.grpAdmin.Controls.Add(this.lblAdminID);
            this.grpAdmin.Controls.Add(this.txtAdminID);
            aFila += aPaso;

            this.btnBuscarAdmin.Location = new System.Drawing.Point(aCol1, aFila);
            this.btnBuscarAdmin.Name     = "btnBuscarAdmin";
            this.btnBuscarAdmin.Size     = new System.Drawing.Size(130, 32);
            this.btnBuscarAdmin.Text     = "Buscar";
            this.btnBuscarAdmin.Click   += new System.EventHandler(this.btnBuscarAdmin_Click);
            this.grpAdmin.Controls.Add(this.btnBuscarAdmin);
            aFila += aPaso + 10;

            this.lblAdminNombre.Location = new System.Drawing.Point(aCol1, aFila);
            this.lblAdminNombre.Name     = "lblAdminNombre";
            this.lblAdminNombre.Size     = new System.Drawing.Size(90, 23);
            this.lblAdminNombre.Text     = "Nombre:";
            this.txtAdminNombre.Location = new System.Drawing.Point(aCol2, aFila - 2);
            this.txtAdminNombre.Name     = "txtAdminNombre";
            this.txtAdminNombre.Size     = new System.Drawing.Size(280, 23);
            this.txtAdminNombre.Enabled  = true;
            this.grpAdmin.Controls.Add(this.lblAdminNombre);
            this.grpAdmin.Controls.Add(this.txtAdminNombre);
            aFila += aPaso;

            this.lblAdminEdad.Location = new System.Drawing.Point(aCol1, aFila);
            this.lblAdminEdad.Name     = "lblAdminEdad";
            this.lblAdminEdad.Size     = new System.Drawing.Size(90, 23);
            this.lblAdminEdad.Text     = "Edad:";
            this.txtAdminEdad.Location = new System.Drawing.Point(aCol2, aFila - 2);
            this.txtAdminEdad.Name     = "txtAdminEdad";
            this.txtAdminEdad.Size     = new System.Drawing.Size(80, 23);
            this.txtAdminEdad.Enabled  = true;
            this.grpAdmin.Controls.Add(this.lblAdminEdad);
            this.grpAdmin.Controls.Add(this.txtAdminEdad);
            aFila += aPaso + 10;

            this.btnEditarAdmin.Location = new System.Drawing.Point(aCol1, aFila);
            this.btnEditarAdmin.Name     = "btnEditarAdmin";
            this.btnEditarAdmin.Size     = new System.Drawing.Size(130, 32);
            this.btnEditarAdmin.Text     = "Guardar Cambios";
            this.btnEditarAdmin.Enabled  = false;
            this.btnEditarAdmin.Click   += new System.EventHandler(this.btnEditarAdmin_Click);
            this.grpAdmin.Controls.Add(this.btnEditarAdmin);

            this.btnEliminarAdmin.Location = new System.Drawing.Point(aCol1 + 145, aFila);
            this.btnEliminarAdmin.Name     = "btnEliminarAdmin";
            this.btnEliminarAdmin.Size     = new System.Drawing.Size(130, 32);
            this.btnEliminarAdmin.Text     = "Eliminar Registro";
            this.btnEliminarAdmin.Enabled  = false;
            this.btnEliminarAdmin.BackColor = System.Drawing.Color.MistyRose;
            this.btnEliminarAdmin.Click   += new System.EventHandler(this.btnEliminarAdmin_Click);
            this.grpAdmin.Controls.Add(this.btnEliminarAdmin);

            // ═══════════════════════════════════════════════════════════════
            // PANEL LOG – visible siempre, debajo del tabControl
            // ═══════════════════════════════════════════════════════════════
            this.pnlLog.Location  = new System.Drawing.Point(0, 372);
            this.pnlLog.Name      = "pnlLog";
            this.pnlLog.Size      = new System.Drawing.Size(760, 218);
            this.pnlLog.TabIndex  = 1;

            this.lblLogTitulo.Location  = new System.Drawing.Point(5, 2);
            this.lblLogTitulo.Name      = "lblLogTitulo";
            this.lblLogTitulo.Size      = new System.Drawing.Size(200, 18);
            this.lblLogTitulo.Text      = "Resultados / Log";
            this.lblLogTitulo.Font      = new System.Drawing.Font("Microsoft Sans Serif", 8.5F,
                                              System.Drawing.FontStyle.Bold);
            this.lblLogTitulo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.pnlLog.Controls.Add(this.lblLogTitulo);

            this.txtLog.Location  = new System.Drawing.Point(5, 22);
            this.txtLog.Name      = "txtLog";
            this.txtLog.ReadOnly  = true;
            this.txtLog.Font      = new System.Drawing.Font("Courier New", 8.5F);
            this.txtLog.Size      = new System.Drawing.Size(624, 188);
            this.txtLog.TabIndex  = 0;
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.pnlLog.Controls.Add(this.txtLog);

            this.btnLimpiarLog.Location = new System.Drawing.Point(638, 22);
            this.btnLimpiarLog.Name     = "btnLimpiarLog";
            this.btnLimpiarLog.Size     = new System.Drawing.Size(115, 28);
            this.btnLimpiarLog.Text     = "Limpiar Log";
            this.btnLimpiarLog.Click   += new System.EventHandler(this.btnLimpiarLog_Click);
            this.pnlLog.Controls.Add(this.btnLimpiarLog);

            // ═══════════════════════════════════════════════════════════════
            // Form1
            // ═══════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize     = new System.Drawing.Size(760, 595);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.Name            = "Form1";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "Sistema de Gestion de Ciudadanos - Archivos -> Indice -> SQL Server";

            this.tabControl1.ResumeLayout(false);
            this.tabNivel1.ResumeLayout(false);
            this.tabNivel2.ResumeLayout(false);
            this.tabNivel3.ResumeLayout(false);
            this.tabAdmin.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // ── Controles ────────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl  tabControl1;
        private System.Windows.Forms.TabPage     tabNivel1, tabNivel2, tabNivel3, tabAdmin;
        private System.Windows.Forms.GroupBox    grpDatos, grpIndice, grpSQL, grpAdmin;
        private System.Windows.Forms.Label       lblID, lblNombre, lblEdad;
        private System.Windows.Forms.Label       lblInfoFormula, lblInfoIndice, lblInfoSQL;
        private System.Windows.Forms.Label       lblBuscarID, lblServer, lblDatabase, lblUsuario, lblPassword;
        private System.Windows.Forms.Label       lblAdminID, lblAdminNombre, lblAdminEdad;
        private System.Windows.Forms.TextBox     txtID, txtNombre, txtEdad;
        private System.Windows.Forms.TextBox     txtBuscarID, txtServer, txtDatabase, txtUsuario, txtPassword;
        private System.Windows.Forms.TextBox     txtAdminID, txtAdminNombre, txtAdminEdad;
        private System.Windows.Forms.Button      btnGuardar, btnLeerID, btnLimpiarCampos;
        private System.Windows.Forms.Button      btnBuscarIndexado, btnVerIndice;
        private System.Windows.Forms.Button      btnConfigurarConexion, btnCrearTabla;
        private System.Windows.Forms.Button      btnMigrar, btnConsultarSQL;
        private System.Windows.Forms.Button      btnBuscarAdmin, btnEditarAdmin, btnEliminarAdmin;
        private System.Windows.Forms.Panel       pnlLog;
        private System.Windows.Forms.Label       lblLogTitulo;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button      btnLimpiarLog;
    }
}
