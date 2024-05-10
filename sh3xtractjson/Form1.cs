using System.Collections.Concurrent;
using System.ComponentModel;
using SH3Textractor;

namespace sh3xtractjson;

public partial class Form1 : Form {
    private RichTextBoxWriter errorWriter;
    private RichTextBoxWriter successWriter;

    public Form1() {
        InitializeComponent();

        errorWriter   = new RichTextBoxWriter(textBoxError);
        successWriter = new RichTextBoxWriter(textBoxSuccess);

        textBoxThreads.Text = Environment.ProcessorCount.ToString();
    }

    private void buttonSelectFolder_Click(object sender, EventArgs e) {
        folderBrowserDialog1.SelectedPath = textBoxFolder.Text;
        folderBrowserDialog1.ShowDialog(this);
        textBoxFolder.Text = folderBrowserDialog1.SelectedPath;
    }

    private void buttonRun_Click(object sender, EventArgs e) {
        // read all files
        var fileList = Files.RecursiveFileSearch(textBoxFolder.Text);
        fileList = fileList.Where(s =>
                                      s.EndsWith(".dat")
                                      || s.EndsWith(".sim")
                                      || s.EndsWith(".zon")
                                      || s.EndsWith(".val")
                                      || s.EndsWith(".cam")
                                      || s.EndsWith(".dsd")
                                      || s.EndsWith(".anm")
                                      || s.EndsWith(".sdl")
                                      || s.EndsWith(".off"))
                           .ToList();
        progressBarFilesDone.Minimum = 0;
        progressBarFilesDone.Value   = 0;
        progressBarFilesDone.Step    = 1;
        progressBarFilesDone.Maximum = fileList.Count;

        // run on background thread
        var worker = new BackgroundWorker();
        var que    = new ConcurrentQueue<object>();
        var sw     = new SpinWait();
        var ct     = new CancellationTokenSource();
        worker.DoWork += (o, args) => {
            Runner.Run(fileList, successWriter, errorWriter, int.Parse(textBoxThreads.Text), que);
            ct.Cancel();
        };
        worker.RunWorkerCompleted += (o, args) => {
            if (args.Error != null) {
                MessageBox.Show(args.Error.Message);
            }

            MessageBox.Show("Done!");
        };
        worker.RunWorkerAsync();
        Task.Run(() => {
            while (que.Count > 0 || !ct.IsCancellationRequested) {
                if (que.TryDequeue(out var obj)) {
                    Invoke(() => { progressBarFilesDone.PerformStep(); });
                    sw.SpinOnce();
                }

                sw.SpinOnce();
            }

            return Task.CompletedTask;
        });
    }
}