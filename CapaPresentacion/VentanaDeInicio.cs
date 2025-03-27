namespace CapaPresentacion
{
    public partial class VentanaDeInicio : Form
    {
        private string? imagePath;
        CNLogueo fotocopia = new CNLogueo();

        public VentanaDeInicio()
        {
            InitializeComponent();
            linkLabelSeleccionar.Enabled = false; // Deshabilitar el LinkLabel inicialmente
            linkLabelSeleccionar.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabelSeleccionar_LinkClicked);
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
            linkLabelSeleccionar.Enabled = true; // Habilitar el LinkLabel cuando se hace clic en el botón
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
        }
    }
}
