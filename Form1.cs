using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExeToByteArr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.AllowDrop = true;
            this.textBox2.DragOver += new DragEventHandler(textBox2_DragOver);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!File.Exists(textBox2.Text))
            {
                MessageBox.Show("Target File does not exists");
                return;
            }
            string name = textBox1.Text;
            byte[] Reading = File.ReadAllBytes(textBox2.Text);
            string[] output = new string  [Reading.Length + 10];
            output[0] = "byte[] " + name + " = new byte[" + Reading.Length + "];";
            for(int i = 0; i < Reading.Length; i++)
            {
                output[i + 1] = name + "[" + i + "] = " + Reading[i].ToString() + ";";
            }

            richTextBox1.Text = string.Join("\n", output);
            if(checkBox1.Checked)
            {
                Clipboard.SetText(string.Join("\n", output));
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string InfoText = "This Tool can be used to Create a byte[] out of a .exe\n You might wonder why you might need it:\n " +
                "For Example you can use the byte[] to execute the Assembly out of memory\n" +
                "Or you can use it to replace Embedded Resources\n" +
                "^^this is ment Protection wise\n\n\n" +
                "How to run an byte[] from memory(has to be .net):\n\n\n" +
                "Assembly exeAssembly = Assembly.Load(" + textBox1.Text + ");\n" +
                "var commandargs = new string[0];\n" +
                "exeAssembly.EntryPoint.Invoke(null, new object[] { commandargs });\n\n\n" +
                "How to extract the byte[] into a .exe :\n\n\n" +
                "File.WriteAllBytes(\"Location/name.exe\", " + textBox1.Text + ");\n\n\n\n\n" +
                "After I made this i realised that its Pretty useless\n" +
                "Visual studio will Fuck up if you add an Array the size of a couple thousand lines\n" +
                ":)";

            richTextBox1.Text = InfoText;
        }


        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.Directory.Exists(files[0]))
                    this.textBox1.Text = files[0];
            }
        }
        void textBox2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
