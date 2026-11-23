using UP.WinFormsApp.Data;

namespace UP.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // textBox2 = Usuario, textBox1 = Contraseña
            textBox1.UseSystemPasswordChar = true;
            AcceptButton = button1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var usuario = textBox2.Text.Trim();
            var contrasena = textBox1.Text;

            if (usuario.Length == 0 || contrasena.Length == 0)
            {
                MessageBox.Show(this, "Escriba el usuario y la contraseña.", "Inicio de sesión",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UseWaitCursor = true;
                button1.Enabled = false;

                var sesion = await ParqueoApi.LoginAsync(usuario, contrasena);

                if (sesion is null)
                {
                    MessageBox.Show(this,
                        "Usuario o contraseña incorrectos, o el usuario está inactivo.",
                        "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Rol 3 = Usuario (cliente). La caja es para Admin/Operador.
                if (sesion.RolId == 3)
                {
                    MessageBox.Show(this,
                        "Este usuario no tiene permisos de operador/cajero.",
                        "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Hide();

                using var menu = new Form2(sesion);
                menu.ShowDialog(this);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"No se pudo conectar con la API ({ApiConfig.BaseUrl}).\n\n" +
                    "Verifique que el proyecto UP.Api esté en ejecución.\n\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                button1.Enabled = true;
            }
        }

        // Manejadores que el diseñador ya tenía enlazados.
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
