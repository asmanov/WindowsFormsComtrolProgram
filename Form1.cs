using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsComtrolProgram
{
    public partial class Form1 : Form
    {
        Process[] processes;
        Dictionary<string, int> map = new Dictionary<string, int>();
        Model.BlockList blockList = new Model.BlockList();

        public Form1()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            map.Clear();
            foreach (var proc in processes)
            {
                map[proc.ProcessName] = 1;
            }
            checkedListBox1.Items.Clear();
            foreach (var item in map)
            {
                checkedListBox1.Items.Add(item.Key);
            }
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string[] tempName = (openFileDialog1.FileName).Split('\\');
            string[] temp = tempName.LastOrDefault().Split('.');
            listBox1.Items.Add(temp[0]);
            Model.Program program = new Model.Program(temp[0]);
            blockList.blockProgram.Add(program);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Model.Program item in blockList.blockProgram)
            {
                Model.Program.BlockProcess(item.ProgramName);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            listBox1.Items.Add(checkedListBox1.SelectedItem);
            Model.Program program = new Model.Program((string)checkedListBox1.SelectedItem);
            blockList.blockProgram.Add(program);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var prog = listBox1.SelectedItem;
            listBox1.Items.Remove(prog);
            Model.Program temp = blockList.blockProgram.FirstOrDefault(x => x.ProgramName == prog);
            blockList.blockProgram.Remove(temp);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var prog = listBox1.SelectedItem;
            var temp1 = blockList.blockProgram.FirstOrDefault(x => x.ProgramName == prog);
            temp1.TimeLife = (int)numericUpDown1.Value * 3600 * 1000;
        }
    }
}
