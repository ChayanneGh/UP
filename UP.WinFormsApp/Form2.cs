using UP.WinFormsApp.Data;

namespace UP.WinFormsApp
{
    public partial class Form2 : Form
    {
        private readonly UsuarioSesion _sesion;

        // Constructor sin parámetros para que el diseñador de Visual Studio pueda abrir el formulario.
        public Form2() : this(new UsuarioSesion
        {
            UsuarioId = 0,
            Nombre = "Invitado",
            RolId = 2,
            Rol = "Operador"
        })
        {
        }

        public Form2(UsuarioSesion sesion)
        {
            InitializeComponent();

            _sesion = sesion;

            Text = $"Menu - {sesion.Nombre} ({sesion.Rol})";
            groupBox2.Text = "Disponibilidad";
            groupBox4.Text = "Confirma Reserva";
            label1.Visible = false; // el título del GroupBox ya dice "Disponibilidad"

            textBox2.PlaceholderText = "Placa";
            textBox1.PlaceholderText = "ID del espacio";
            textBox3.PlaceholderText = "Placa";

            button2.Click += button2_Click;
            button3.Click += button3_Click;
            listView1.DoubleClick += listView1_DoubleClick;
            Load += Form2_Load;

            // Enter en cada caja ejecuta su acción (sin volver "Aceptar" global,
            // para no disparar una salida por accidente).
            textBox3.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                button2.PerformClick();
            };
            textBox4.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                button3.PerformClick();
            };
            textBox2.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                button1.PerformClick();
            };
            textBox1.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                button1.PerformClick();
            };

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.Columns.Clear();
            listView1.Columns.Add("Reservación", 90);
            listView1.Columns.Add("Espacio", 70);
            listView1.Columns.Add("Usuario", 140);
        }

        private async void Form2_Load(object? sender, EventArgs e)
        {
            await RefrescarAsync();
        }

        private async Task RefrescarAsync()
        {
            try
            {
                UseWaitCursor = true;

                var espacios = await ParqueoApi.ObtenerDisponibilidadAsync();
                listBox1.Items.Clear();
                foreach (var espacio in espacios)
                {
                    var texto = $"Espacio {espacio.EstacionamientoId} - {espacio.Nivel}";
                    if (espacio.Reservado)
                        texto += " (reservado)";
                    listBox1.Items.Add(texto);
                }

                var reservas = await ParqueoApi.ObtenerReservacionesAsync();
                listView1.Items.Clear();
                foreach (var reserva in reservas)
                {
                    var item = new ListViewItem(reserva.ReservacionId.ToString());
                    item.SubItems.Add(reserva.EspacioId.ToString());
                    item.SubItems.Add(reserva.UsuarioNombre);
                    item.Tag = reserva;
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar los datos.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void SetBusy(bool ocupado)
        {
            UseWaitCursor = ocupado;
            button1.Enabled = !ocupado;
            button2.Enabled = !ocupado;
            button3.Enabled = !ocupado;
        }

        // Registra entrada: textBox2 = Placa, textBox1 = ID del espacio
        private async void button1_Click(object sender, EventArgs e)
        {
            var placa = textBox2.Text.Trim();

            if (!int.TryParse(textBox1.Text.Trim(), out var espacioId))
            {
                MessageBox.Show(this, "Escriba el ID numérico del espacio.", "Registrar entrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetBusy(true);
                var entradaId = await ParqueoApi.RegistrarEntradaAsync(placa, espacioId);

                MessageBox.Show(this,
                    $"Entrada registrada correctamente.\n\n" +
                    $"Entrada: {entradaId}\nPlaca: {placa.ToUpperInvariant()}\nEspacio: {espacioId}",
                    "Registrar entrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox2.Clear();
                textBox1.Clear();
                await RefrescarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Registrar entrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Registra salida: textBox3 = Placa
        private async void button2_Click(object? sender, EventArgs e)
        {
            var placa = textBox3.Text.Trim();

            if (placa.Length == 0)
            {
                MessageBox.Show(this, "Escriba la placa del vehículo que sale.", "Registrar salida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetBusy(true);
                var resultado = await ParqueoApi.RegistrarSalidaAsync(placa);

                MessageBox.Show(this,
                    $"Salida registrada correctamente.\n\n" +
                    $"Placa: {resultado.Placa}\nEspacio: {resultado.EspacioId}\n" +
                    $"Entrada: {resultado.HoraEntrada:g}\nSalida: {resultado.HoraSalida:g}\n" +
                    $"Horas cobradas: {resultado.HorasCobradas}\n" +
                    $"Monto: RD$ {resultado.Monto:N2}",
                    "Registrar salida", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox3.Clear();
                await RefrescarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Registrar salida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Botón "Confirmar": usa el ID escrito en textBox4.
        private async void button3_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text.Trim(), out var reservacionId))
            {
                MessageBox.Show(this, "Escriba el ID numérico de la reservación.", "Confirmar reserva",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReservacionItem? reserva = null;
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Tag is ReservacionItem r && r.ReservacionId == reservacionId)
                {
                    reserva = r;
                    break;
                }
            }

            await ConfirmarReservaAsync(reservacionId, reserva);
        }

        // Doble clic sobre una reservación: confirmarla y registrar la entrada.
        private async void listView1_DoubleClick(object? sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            if (listView1.SelectedItems[0].Tag is not ReservacionItem reserva)
                return;

            await ConfirmarReservaAsync(reserva.ReservacionId, reserva);
        }

        private async Task ConfirmarReservaAsync(int reservacionId, ReservacionItem? reserva)
        {
            var detalle = reserva is null
                ? $"¿Confirmar la reservación {reservacionId}?"
                : $"¿Confirmar la reservación {reserva.ReservacionId} del espacio {reserva.EspacioId} " +
                  $"para {reserva.UsuarioNombre}?";

            var respuesta = MessageBox.Show(this, detalle,
                "Confirmar reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                SetBusy(true);
                var entradaId = await ParqueoApi.ConfirmarReservaAsync(reservacionId);

                MessageBox.Show(this,
                    $"Reservación confirmada.\nSe registró la entrada {entradaId}.",
                    "Confirmar reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox4.Clear();
                await RefrescarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Confirmar reserva",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                SetBusy(false);
            }
        }

        // Manejadores que el diseñador ya tenía enlazados y no se usan.
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
