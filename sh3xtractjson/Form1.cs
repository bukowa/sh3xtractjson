namespace sh3xtractjson;

public partial class Form1 : Form
{

    private RichTextBoxWriter errorWriter;
    private RichTextBoxWriter successWriter;

    public Form1()
    {
        InitializeComponent();
        errorWriter   = new RichTextBoxWriter(textBoxError);
        successWriter = new RichTextBoxWriter(textBoxSuccess);
    }

    private void buttonSelectFolder_Click(object sender, EventArgs e)
    {
        folderBrowserDialog1.SelectedPath = textBoxFolder.Text;
        folderBrowserDialog1.ShowDialog(this);
        textBoxFolder.Text = folderBrowserDialog1.SelectedPath;
    }

    private void buttonRun_Click(object sender, EventArgs e) {
        Task.Run(() => {
            Runner.Run(textBoxFolder.Text, successWriter, errorWriter);
            Invoke(() => {
                MessageBox.Show("Done!");
            });
        });
    }
}