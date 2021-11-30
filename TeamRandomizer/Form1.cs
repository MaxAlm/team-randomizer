using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamRandomizer
{
    public partial class Form1 : Form
    {
        // VARIABLES

        List<string> names = new List<string>();
        Random rnd = new Random();
        
        // CODE START

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button_randomize_Click(object sender, EventArgs e)
        {
            names.Clear();

            // If textBox is readonly...
            if (richTextBox1.ReadOnly == true)
            {
                richTextBox1.Text = "";
                richTextBox1.ReadOnly = false;
                richTextBox1.BackColor = Color.White;

                txtBoxTeams.ReadOnly = false;

                button_randomize.Text = "Randomize";
            }
            else
            {
                int teams; // Stores amount of teams.
                string[] lines = richTextBox1.Text.Split('\n');

                // Filter out empty rows
                foreach (string s in lines)
                {
                    if (s.Trim() != "")
                    {
                        names.Add(s.Trim());
                    }
                }

                // Check if names is empty
                if (names.Count == 0)
                {
                    MessageBox.Show("No names entered.", "Error!");
                    richTextBox1.Text = "";
                    txtBoxTeams.Text = "";
                    return;
                }

                // Check if txtBoxNumber only includes numbers
                if (!int.TryParse(txtBoxTeams.Text, out teams))
                {
                    MessageBox.Show("Nr. of teams can only contain numbers.", "Error!");
                    txtBoxTeams.Text = "";
                    return;
                }

                // Check if amount of teams is greater than amount of names
                if (teams > names.Count)
                {
                    MessageBox.Show("Nr. of teams can not be greater than the amount of names entered.", "Error!");
                    return;
                }

                Queue<string>[] teamArray = new Queue<string>[teams];

                // Randomize teams
                while (names.Count > 0)
                {
                    for (int i = 0; i < teamArray.Length; i++)
                    {
                        if (names.Count == 0)
                            break;

                        if (teamArray[i] == null)
                            teamArray[i] = new Queue<string>();

                        int num = rnd.Next(0, names.Count);

                        teamArray[i].Enqueue(names[num]);
                        names.RemoveAt(num);
                    }
                }

                // Clear textboxes and names list
                richTextBox1.Text = "";
                txtBoxTeams.Text = "";

                // Write out teams
                for (int i = 0; i < teamArray.Length; i++)
                {
                    richTextBox1.Text += String.Format("=== TEAM {0} ==={1}", i + 1, Environment.NewLine);

                    while (teamArray[i].Count != 0)
                    {
                        richTextBox1.Text += teamArray[i].Dequeue() + Environment.NewLine;
                    }

                    // If not last team
                    if (i != teamArray.Length - 1)
                        richTextBox1.Text += Environment.NewLine;
                }

                // Change button text and make textboxes readonly
                button_randomize.Text = "Reset";
                txtBoxTeams.ReadOnly = true;
                richTextBox1.ReadOnly = true;
                richTextBox1.BackColor = Color.FromArgb(255, 240, 240, 240);
            }
        }
    }
}
