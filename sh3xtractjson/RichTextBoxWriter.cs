using System.Text;

namespace sh3xtractjson;

public class RichTextBoxWriter {
    
    private readonly RichTextBox _textBox;
    
    public RichTextBoxWriter(RichTextBox textBox) {
        _textBox = textBox;
    }

    public void WriteLine(string value) {
        _textBox.AppendText(value + Environment.NewLine);
    }

}