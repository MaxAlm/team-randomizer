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
        // METHODS


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
            // Clear names list
            names.Clear();

            if (richTextBox1.ReadOnly == true)
            {
                // Make textbox not readonly
                richTextBox1.Text = "";
                richTextBox1.ReadOnly = false;
                richTextBox1.BackColor = Color.White;

                // Make txtBoxTeams not readonly
                txtBoxTeams.ReadOnly = false;

                // Change button text
                button_randomize.Text = "Randomize";
            }
            else
            {
                int teams; // Stores amount of teams.

                // Get names from textbox
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

                // Create teams array
                List<string>[] teamArray = new List<string>[teams];

                // Randomize teams
                while (names.Count > 0)
                {
                    for (int i = 0; i < teamArray.Length; i++)
                    {
                        // Get random 'names' index
                        int num = rnd.Next(0, names.Count);

                        // Check if list exist. If not, add list
                        if (teamArray[i] == null) teamArray[i] = new List<string>();

                        // Add name to team
                        teamArray[i].Add(names[num]);
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

                    for (int j = 0; j < teamArray[i].Count; j++)
                    {
                        richTextBox1.Text += teamArray[i][j] + Environment.NewLine;
                    }

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
