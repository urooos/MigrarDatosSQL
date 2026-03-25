using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace MigrarDatosSQL
{
    // ═════════════════════════════════════════════════════════════════════════
    //  MODELO – Ciudadano con tamaño fijo (60 bytes) usando StructLayout
    //  ID(4) + Nombre(50 bytes ANSI) + Edad(4) + padding(2) = 60 bytes
    //  Idéntico al boilerplate de la práctica.
    // ═════════════════════════════════════════════════════════════════════════
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public record struct Ciudadano
    {
        public int Id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string Nombre;

        public int Edad;

        // Padding de 2 bytes para alcanzar exactamente 60 bytes por registro
        // Id(4) + Nombre(50) + Edad(4) + _pad(2) = 60 bytes
        private short _pad;

        public Ciudadano(int id, string nombre, int edad)
        {
            Id     = id;
            Nombre = nombre;
            Edad   = edad;
            _pad   = 0;
        }

        public static int Size => Marshal.SizeOf<Ciudadano>();
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  NIVEL 1 – GestorArchivos  (clase separada, tal como pide el boilerplate)
    // ═════════════════════════════════════════════════════════════════════════
    public class GestorArchivos
    {
        private readonly string _path = "ciudadanos.bin";

        public void GuardarCiudadano(Ciudadano c, int posicion)
        {
            using var fs     = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write);
            using var writer = new BinaryWriter(fs, Encoding.Latin1, leaveOpen: true);

            // NIVEL 1 – Cálculo del Offset: offset = posicion × tamaño_registro
            long offset = (long)posicion * Ciudadano.Size;
            fs.Seek(offset, SeekOrigin.Begin);

            writer.Write(c.Id);
            // Nombre con relleno fijo a exactamente 50 chars ANSI (1 byte/char)
            writer.Write(c.Nombre.PadRight(50).Substring(0, 50).ToCharArray());
            writer.Write(c.Edad);
            writer.Write((short)0); // padding: 2 bytes → total = 60 bytes
        }

        public Ciudadano? LeerCiudadano(int posicion)
        {
            if (!File.Exists(_path)) return null;

            using var fs     = new FileStream(_path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs, Encoding.Latin1, leaveOpen: true);

            long offset = (long)posicion * Ciudadano.Size;
            if (offset + Ciudadano.Size > fs.Length) return null;

            fs.Seek(offset, SeekOrigin.Begin);
            int    id     = reader.ReadInt32();
            string nombre = new string(reader.ReadChars(50)).TrimEnd();
            int    edad   = reader.ReadInt32();
            reader.ReadInt16(); // padding: 2 bytes descartados
            return new Ciudadano(id, nombre, edad);
        }

        public List<Ciudadano> LeerTodos()
        {
            var lista = new List<Ciudadano>();
            if (!File.Exists(_path)) return lista;

            using var fs     = new FileStream(_path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs, Encoding.Latin1, leaveOpen: true);

            long total = fs.Length / Ciudadano.Size;
            for (long i = 0; i < total; i++)
            {
                int    id     = reader.ReadInt32();
                string nombre = new string(reader.ReadChars(50)).TrimEnd();
                int    edad   = reader.ReadInt32();
                reader.ReadInt16(); // padding: 2 bytes descartados
                lista.Add(new Ciudadano(id, nombre, edad));
            }
            return lista;
        }

        public long ObtenerOffset(int posicion) => (long)posicion * Ciudadano.Size;

        public bool ArchivoExiste() => File.Exists(_path);

        // Devuelve el índice del primer slot vacío (Id==0) o el siguiente al final
        public int SiguientePosicionLibre()
        {
            if (!File.Exists(_path)) return 0;

            using var fs     = new FileStream(_path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs, Encoding.Latin1, leaveOpen: true);

            long total = fs.Length / Ciudadano.Size;
            for (int i = 0; i < total; i++)
            {
                fs.Seek((long)i * Ciudadano.Size, SeekOrigin.Begin);
                int id = reader.ReadInt32();
                if (id == 0) return i; // slot vacío (eliminado)
                reader.ReadBytes(Ciudadano.Size - 4); // saltar resto del registro
            }
            return (int)total; // al final del archivo
        }

        public bool EditarCiudadano(Ciudadano c, long offset)
        {
            if (!File.Exists(_path)) return false;

            using var fs     = new FileStream(_path, FileMode.Open, FileAccess.Write);
            using var writer = new BinaryWriter(fs, Encoding.Latin1, leaveOpen: true);

            fs.Seek(offset, SeekOrigin.Begin);
            writer.Write(c.Id);
            writer.Write(c.Nombre.PadRight(50).Substring(0, 50).ToCharArray());
            writer.Write(c.Edad);
            writer.Write((short)0); // padding: 2 bytes → total = 60 bytes
            return true;
        }

        public bool EliminarCiudadano(long offset)
        {
            if (!File.Exists(_path)) return false;

            // Sobrescribe el slot con ceros (registro vacío)
            using var fs     = new FileStream(_path, FileMode.Open, FileAccess.Write);
            using var writer = new BinaryWriter(fs, Encoding.Latin1, leaveOpen: true);

            fs.Seek(offset, SeekOrigin.Begin);
            writer.Write(new byte[Ciudadano.Size]); // 60 bytes en cero
            return true;
        }
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  NIVEL 3 – MigradorSql  (async Task, igual al boilerplate)
    // ═════════════════════════════════════════════════════════════════════════
    public class MigradorSql
    {
        private readonly string _connectionString;

        public MigradorSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<(int insertados, int omitidos)> MigrarDesdeArchivo(GestorArchivos gestor)
        {
            List<Ciudadano> registros = gestor.LeerTodos();
            int insertados = 0, omitidos = 0;

            // 1. Abrir conexión – instancia SQLEXPRESS
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            Console.WriteLine("Conexión establecida. Iniciando migración...");

            // 2. Bucle que lee el archivo secuencialmente y ejecuta el SqlCommand por registro
            foreach (var c in registros)
            {
                if (c.Id == 0 && string.IsNullOrWhiteSpace(c.Nombre)) continue; // slot vacío

                var query = @"
IF NOT EXISTS (SELECT 1 FROM Ciudadanos WHERE Id = @Id)
    INSERT INTO Ciudadanos (Id, Nombre, Edad) VALUES (@Id, @Nombre, @Edad)";

                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id",     c.Id);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Edad",   c.Edad);

                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0) insertados++;
                else          omitidos++;
            }

            // La conexión se cierra al salir del using
            return (insertados, omitidos);
        }
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  FORMULARIO PRINCIPAL
    // ═════════════════════════════════════════════════════════════════════════
    public partial class Form1 : Form
    {
        // ─── Instancias de las clases del boilerplate ─────────────────────────
        private readonly GestorArchivos _gestor = new GestorArchivos();

        // ─── Cadena de conexión SQL Server ────────────────────────────────────
        private string _connectionString =
            "Server=localhost\\SQLEXPRESS;Database=CiudadanosDB;Integrated Security=true;" +
            "TrustServerCertificate=true;";

        // ─── NIVEL 2 – Índice en memoria: ID → offset en el archivo ──────────
        private Dictionary<int, long> _indice = new Dictionary<int, long>();
        private const string ARCHIVO_INDICE = "ciudadanos.idx";

        public Form1()
        {
            InitializeComponent();
            CargarIndiceDesdeDisco();
        }

        // ═════════════════════════════════════════════════════════════════════
        //  NIVEL 1 – Archivo Binario de Acceso Directo
        // ═════════════════════════════════════════════════════════════════════

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas(out int id, out string nombre, out int edad)) return;

            if (_indice.ContainsKey(id))
            {
                MessageBox.Show($"Ya existe un ciudadano con ID {id}.",
                    "ID duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Posición automática: siguiente slot disponible al final del archivo
                int posicion = _gestor.SiguientePosicionLibre();

                var ciudadano = new Ciudadano(id, nombre, edad);
                _gestor.GuardarCiudadano(ciudadano, posicion);

                long offset = _gestor.ObtenerOffset(posicion);
                _indice[id] = offset;
                GuardarIndice();

                AppendLog($"[NIVEL 1] Ciudadano guardado → Posición: {posicion}, " +
                          $"Offset: {offset} bytes");
                AppendLog($"  Id={id}, Nombre={nombre}, Edad={edad}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLeerID_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show("Ingrese un ID válido en el campo ID Ciudadano.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_indice.TryGetValue(id, out long offset))
            {
                AppendLog($"[NIVEL 1] No existe ningún ciudadano con ID {id}.");
                return;
            }

            try
            {
                int posicion = (int)(offset / Ciudadano.Size);
                Ciudadano? c = _gestor.LeerCiudadano(posicion);
                if (c is null)
                {
                    AppendLog($"[NIVEL 1] No se pudo leer el registro del ID {id}.");
                    return;
                }
                AppendLog($"[NIVEL 1] Lectura por ID={id} (posición {posicion}, offset {offset} bytes):");
                AppendLog($"  Id={c.Value.Id}, Nombre={c.Value.Nombre}, Edad={c.Value.Edad}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        //  NIVEL 2 – Sistema de Indexación y Comparación de Búsquedas
        // ═════════════════════════════════════════════════════════════════════

        private void btnBuscarIndexado_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBuscarID.Text.Trim(), out int idBuscar))
            {
                MessageBox.Show("Ingrese un ID numérico válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ── Búsqueda INDEXADA (Seek() directo gracias al índice) ───────────
            Stopwatch swIdx = Stopwatch.StartNew();
            Ciudadano? cIdx = null;

            if (_indice.TryGetValue(idBuscar, out long offset))
            {
                int posicion = (int)(offset / Ciudadano.Size);
                cIdx = _gestor.LeerCiudadano(posicion);
            }
            swIdx.Stop();

            // ── Búsqueda SECUENCIAL ────────────────────────────────────────────
            Stopwatch swSeq = Stopwatch.StartNew();
            Ciudadano? cSeq = null;

            foreach (var c in _gestor.LeerTodos())
            {
                if (c.Id == idBuscar) { cSeq = c; break; }
            }
            swSeq.Stop();

            AppendLog("─────────────────────────────────────────");
            AppendLog($"[NIVEL 2] Búsqueda de ID = {idBuscar}");
            AppendLog($"  Búsqueda INDEXADA   → {swIdx.Elapsed.TotalMilliseconds:F4} ms " +
                      (cIdx.HasValue
                          ? $"→ {cIdx.Value.Nombre}, Edad {cIdx.Value.Edad}"
                          : "→ No encontrado"));
            AppendLog($"  Búsqueda SECUENCIAL → {swSeq.Elapsed.TotalMilliseconds:F4} ms " +
                      (cSeq.HasValue
                          ? $"→ {cSeq.Value.Nombre}, Edad {cSeq.Value.Edad}"
                          : "→ No encontrado"));

            long ratio = swIdx.ElapsedTicks > 0
                ? swSeq.ElapsedTicks / Math.Max(swIdx.ElapsedTicks, 1) : 0;
            AppendLog($"  El índice fue ~{ratio}x más rápido que la búsqueda secuencial.");
        }

        private void btnVerIndice_Click(object sender, EventArgs e)
        {
            AppendLog("─────────────────────────────────────────");
            AppendLog($"[NIVEL 2] Índice en memoria ({_indice.Count} entradas):");
            foreach (var kv in _indice)
                AppendLog($"  ID={kv.Key} → offset={kv.Value} bytes " +
                          $"(posición {kv.Value / Ciudadano.Size})");
        }

        // ═════════════════════════════════════════════════════════════════════
        //  NIVEL 3 – Migración a SQL Server
        // ═════════════════════════════════════════════════════════════════════

        private void btnConfigurarConexion_Click(object sender, EventArgs e)
        {
            string servidor = txtServer.Text.Trim();
            bool esLocal = servidor.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            if (esLocal)
            {
                _connectionString =
                    $"Server={servidor}\\SQLEXPRESS;" +
                    $"Database={txtDatabase.Text.Trim()};" +
                    "Integrated Security=true;TrustServerCertificate=true;";
                AppendLog("[NIVEL 3] Conexión local configurada (Windows Auth).");
            }
            else
            {
                _connectionString =
                    $"Server={servidor},1433;" +
                    $"Database={txtDatabase.Text.Trim()};" +
                    $"User Id={txtUsuario.Text.Trim()};" +
                    $"Password={txtPassword.Text.Trim()};" +
                    "TrustServerCertificate=true;";
                AppendLog("[NIVEL 3] Conexión remota configurada (SQL Auth).");
            }
        }

        private void btnCrearTabla_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                // Script idéntico al boilerplate de la práctica
                string sql = @"
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Ciudadanos'
)
CREATE TABLE Ciudadanos (
    Id     INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Edad   INT NOT NULL
);";
                using var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                AppendLog("[NIVEL 3] Tabla 'Ciudadanos' verificada/creada en SQL Server.");
            }
            catch (Exception ex)
            {
                AppendLog("[NIVEL 3] ERROR al crear tabla: " + ex.Message);
            }
        }

        // async void es el patrón correcto para event handlers en WinForms
        private async void btnMigrar_Click(object sender, EventArgs e)
        {
            if (!_gestor.ArchivoExiste())
            {
                AppendLog("[NIVEL 3] El archivo binario no existe. Guarde registros primero.");
                return;
            }

            btnMigrar.Enabled = false;
            try
            {
                // MigradorSql.MigrarDesdeArchivo es async Task, como pide el boilerplate
                var migrador = new MigradorSql(_connectionString);
                var (insertados, omitidos) = await migrador.MigrarDesdeArchivo(_gestor);

                AppendLog($"[NIVEL 3] Migración completada. " +
                          $"Insertados: {insertados}, Omitidos (ya existían): {omitidos}");
            }
            catch (Exception ex)
            {
                AppendLog("[NIVEL 3] ERROR en migración: " + ex.Message);
            }
            finally
            {
                btnMigrar.Enabled = true;
            }
        }

        private void btnConsultarSQL_Click(object sender, EventArgs e)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string sql = "SELECT Id, Nombre, Edad FROM Ciudadanos ORDER BY Id";
                using var cmd    = new SqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader(); // SqlDataReader, como pide el boilerplate

                AppendLog("─────────────────────────────────────────");
                AppendLog("[NIVEL 3] Registros en SQL Server:");
                AppendLog($"  {"Id",-6} {"Nombre",-30} {"Edad",-6}");
                AppendLog($"  {"──",-6} {"──────",-30} {"────",-6}");

                int count = 0;
                while (reader.Read())
                {
                    AppendLog($"  {reader.GetInt32(0),-6} " +
                              $"{reader.GetString(1),-30} " +
                              $"{reader.GetInt32(2),-6}");
                    count++;
                }
                AppendLog($"  Total: {count} registro(s).");
            }
            catch (Exception ex)
            {
                AppendLog("[NIVEL 3] ERROR al consultar: " + ex.Message);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        //  NIVEL 2 – Persistencia del índice en disco (.idx)
        // ═════════════════════════════════════════════════════════════════════

        private void GuardarIndice()
        {
            using var sw = new StreamWriter(ARCHIVO_INDICE, append: false);
            foreach (var kv in _indice)
                sw.WriteLine($"{kv.Key},{kv.Value}");
        }

        private void CargarIndiceDesdeDisco()
        {
            _indice.Clear();
            if (!File.Exists(ARCHIVO_INDICE)) return;

            foreach (string line in File.ReadAllLines(ARCHIVO_INDICE))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0],  out int  id)     &&
                    long.TryParse(parts[1], out long offset))
                    _indice[id] = offset;
            }
            AppendLog($"[INICIO] Índice cargado desde disco: {_indice.Count} entrada(s).");
        }

        // ═════════════════════════════════════════════════════════════════════
        //  UTILIDADES
        // ═════════════════════════════════════════════════════════════════════

        private bool ValidarEntradas(out int id, out string nombre, out int edad)
        {
            id = 0; nombre = ""; edad = 0;

            if (!int.TryParse(txtID.Text.Trim(), out id) || id <= 0)
            {
                MessageBox.Show("El ID debe ser un entero positivo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            nombre = txtNombre.Text.Trim();
            if (nombre.Length == 0 || nombre.Length > 50)
            {
                MessageBox.Show("El nombre no puede estar vacío y debe tener máximo 50 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtEdad.Text.Trim(), out edad) || edad < 0 || edad > 150)
            {
                MessageBox.Show("La edad debe ser un número entre 0 y 150.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AppendLog(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void btnLimpiarLog_Click(object sender, EventArgs e)    => txtLog.Clear();
        private void btnLimpiarCampos_Click(object sender, EventArgs e) =>
            (txtID.Text, txtNombre.Text, txtEdad.Text) = ("", "", "");

        // ═════════════════════════════════════════════════════════════════════
        //  ADMINISTRAR – Editar y Eliminar registros del archivo binario
        // ═════════════════════════════════════════════════════════════════════

        private void btnBuscarAdmin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAdminID.Text.Trim(), out int idBuscar) || idBuscar <= 0)
            {
                MessageBox.Show("Ingrese un ID válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_indice.TryGetValue(idBuscar, out long offset))
            {
                MessageBox.Show($"No existe ningún ciudadano con ID {idBuscar}.",
                    "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int posicion = (int)(offset / Ciudadano.Size);
            Ciudadano? c = _gestor.LeerCiudadano(posicion);
            if (c == null) return;

            txtAdminNombre.Text  = c.Value.Nombre;
            txtAdminEdad.Text    = c.Value.Edad.ToString();
            btnEditarAdmin.Enabled  = true;
            btnEliminarAdmin.Enabled = true;
            AppendLog($"[ADMIN] Ciudadano encontrado → ID={c.Value.Id}, Nombre={c.Value.Nombre}, Edad={c.Value.Edad}");
        }

        private void btnEditarAdmin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAdminID.Text.Trim(), out int id) || id <= 0) return;

            string nombre = txtAdminNombre.Text.Trim();
            if (nombre.Length == 0 || nombre.Length > 50)
            {
                MessageBox.Show("El nombre no puede estar vacío y debe tener máximo 50 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAdminEdad.Text.Trim(), out int edad) || edad < 0 || edad > 150)
            {
                MessageBox.Show("La edad debe ser un número entre 0 y 150.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_indice.TryGetValue(id, out long offset)) return;

            var ciudadano = new Ciudadano(id, nombre, edad);
            _gestor.EditarCiudadano(ciudadano, offset);

            AppendLog($"[ADMIN] Ciudadano ID={id} actualizado → Nombre={nombre}, Edad={edad}");
            MessageBox.Show($"Ciudadano ID={id} actualizado correctamente.",
                "Editado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminarAdmin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAdminID.Text.Trim(), out int id) || id <= 0) return;

            var confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea eliminar al ciudadano con ID {id}?\nEsta acción no se puede deshacer.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            if (!_indice.TryGetValue(id, out long offset)) return;

            _gestor.EliminarCiudadano(offset);
            _indice.Remove(id);
            GuardarIndice();

            txtAdminNombre.Text  = "";
            txtAdminEdad.Text    = "";
            btnEditarAdmin.Enabled   = false;
            btnEliminarAdmin.Enabled = false;

            AppendLog($"[ADMIN] Ciudadano ID={id} eliminado del archivo y del índice.");
            MessageBox.Show($"Ciudadano ID={id} eliminado correctamente.",
                "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
