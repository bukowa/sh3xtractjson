namespace sh3xtractjson;

partial class Form1 {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        folderBrowserDialog1 = new FolderBrowserDialog();
        textBoxFolder = new TextBox();
        buttonRun = new Button();
        textBoxSuccess = new RichTextBox();
        textBoxError = new RichTextBox();
        buttonSelectFolder = new Button();
        textBoxThreads = new TextBox();
        labelThreads = new Label();
        progressBarFilesDone = new ProgressBar();
        SuspendLayout();
        // 
        // textBoxFolder
        // 
        textBoxFolder.Location = new Point(64, 24);
        textBoxFolder.Name = "textBoxFolder";
        textBoxFolder.Size = new Size(641, 23);
        textBoxFolder.TabIndex = 0;
        // 
        // buttonRun
        // 
        buttonRun.Location = new Point(64, 105);
        buttonRun.Name = "buttonRun";
        buttonRun.Size = new Size(641, 23);
        buttonRun.TabIndex = 1;
        buttonRun.Text = "Run";
        buttonRun.UseVisualStyleBackColor = true;
        buttonRun.Click += buttonRun_Click;
        // 
        // textBoxSuccess
        // 
        textBoxSuccess.Location = new Point(12, 134);
        textBoxSuccess.Name = "textBoxSuccess";
        textBoxSuccess.Size = new Size(1589, 159);
        textBoxSuccess.TabIndex = 2;
        textBoxSuccess.Text = "";
        // 
        // textBoxError
        // 
        textBoxError.Location = new Point(12, 299);
        textBoxError.Name = "textBoxError";
        textBoxError.Size = new Size(1589, 488);
        textBoxError.TabIndex = 3;
        textBoxError.Text = "";
        // 
        // buttonSelectFolder
        // 
        buttonSelectFolder.Location = new Point(64, 53);
        buttonSelectFolder.Name = "buttonSelectFolder";
        buttonSelectFolder.Size = new Size(641, 23);
        buttonSelectFolder.TabIndex = 4;
        buttonSelectFolder.Text = "Select Folder";
        buttonSelectFolder.UseVisualStyleBackColor = true;
        buttonSelectFolder.Click += buttonSelectFolder_Click;
        // 
        // textBoxThreads
        // 
        textBoxThreads.Location = new Point(766, 54);
        textBoxThreads.Name = "textBoxThreads";
        textBoxThreads.Size = new Size(100, 23);
        textBoxThreads.TabIndex = 5;
        // 
        // labelThreads
        // 
        labelThreads.AutoSize = true;
        labelThreads.Location = new Point(783, 32);
        labelThreads.Name = "labelThreads";
        labelThreads.Size = new Size(46, 15);
        labelThreads.TabIndex = 6;
        labelThreads.Text = "threads";
        // 
        // progressBarFilesDone
        // 
        progressBarFilesDone.Location = new Point(935, 54);
        progressBarFilesDone.Name = "progressBarFilesDone";
        progressBarFilesDone.Size = new Size(473, 23);
        progressBarFilesDone.TabIndex = 7;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1665, 799);
        Controls.Add(progressBarFilesDone);
        Controls.Add(labelThreads);
        Controls.Add(textBoxThreads);
        Controls.Add(buttonSelectFolder);
        Controls.Add(textBoxError);
        Controls.Add(textBoxSuccess);
        Controls.Add(buttonRun);
        Controls.Add(textBoxFolder);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private FolderBrowserDialog folderBrowserDialog1;
    private TextBox textBoxFolder;
    private Button buttonRun;
    private RichTextBox textBoxSuccess;
    private RichTextBox textBoxError;
    private Button buttonSelectFolder;
    private TextBox textBoxThreads;
    private Label labelThreads;
    private ProgressBar progressBarFilesDone;
}