using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class VentanaDeInicio : Form
    {

        CNLogueo fotocopia = new CNLogueo();
        private string? imagePath;

        public VentanaDeInicio()
        {
            InitializeComponent();
            linkLabelSeleccionar.Enabled = false;
            linkLabelSeleccionar.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabelSeleccionar_LinkClicked);
            btnEliminar.Click += new EventHandler(btnEliminar_Click);
            btnBuscar.Click += new EventHandler(btnBuscar_Click);
            btnGuardar.Click += new EventHandler(btnGuardar_Click);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string id = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID del cliente a buscar:", "Buscar Cliente", "", -1, -1);

            if (!string.IsNullOrEmpty(id))
            {
                CNLogueo logueo = new CNLogueo();
                var registro = logueo.BuscarRegistro(id);

                if (registro != null)
                {
                    txtId.Text = registro.Id;
                    txtNombre.Text = registro.Nombre;
                    txtApellido.Text = registro.Apellido;
                    imagePath = registro.ImagePath;

                    // Mostrar la imagen seleccionada en un PictureBox (si tienes uno)
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        pictureBoxFoto.Image = Image.FromFile(imagePath);
                    }

                    MessageBox.Show($"Registro encontrado:\n\nID: {registro.Id}\nNombre: {registro.Nombre}\nApellido: {registro.Apellido}\nImagen: {registro.ImagePath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registro no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID del cliente a eliminar:", "Eliminar Cliente", "", -1, -1);

            if (!string.IsNullOrEmpty(id))
            {
                CNLogueo logueo = new CNLogueo();
                bool resultado = logueo.EliminarRegistro(id);

                if (resultado)
                {
                    MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarRegistros();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GuardarDatos()
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string id = txtId.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("Por favor, complete todos los campos y seleccione una imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CNLogueo logueo = new CNLogueo();
            bool resultado = logueo.GuardarRegistro(id, nombre, apellido, imagePath);

            if (resultado)
            {
                MessageBox.Show("Registro guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarRegistros();
            }
            else
            {
                MessageBox.Show("Error al guardar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        private void LinkLabelSeleccionar_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Seleccionar una imagen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    MessageBox.Show($"Imagen seleccionada: {imagePath}", "Imagen seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtId.Enabled = true;
            linkLabelSeleccionar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void CargarRegistros()
        {
            List<string> registros = fotocopia.ObtenerRegistros();
            listBoxRegistros.Items.Clear();
            foreach (var registro in registros)
            {
                listBoxRegistros.Items.Add(registro);
            }
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            fotocopia.PruebaMysql();
            CargarRegistros();
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
        }
    }
}
