using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment9_yevhenii
{
    public partial class Form1 : Form
    {

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static int numOfSpaces;
        public Form1()
        {
            InitializeComponent();
        }

        public static List<int> findIdxs(string s, char f)
        {
            var foundIndexes = new List<int>();
            for (int i = s.IndexOf(f); i > -1; i = s.IndexOf(f, i + 1))
            {
                foundIndexes.Add(i);
            }
            return foundIndexes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<char, int>> list = getResult(this.inputBox.Text);
            Random rng = new Random();
            this.labelResult.Text += "Num of spaces: " + numOfSpaces + "\n";
            foreach (KeyValuePair<char, int> kvp in list)
            {
                this.labelResult.Text += "Most frequent character is: " + kvp.Key + " ("+kvp.Value+")\n";
                Color color = Color.FromArgb(rng.Next(-16777216, 0)); 
                List<int> idxs = findIdxs(this.inputBox.Text.ToLower(), kvp.Key);
                foreach (int idx in idxs)
                {
                    this.inputBox.Select(idx, 1);
                    this.inputBox.SelectionColor = color;
                    this.inputBox.SelectionFont = new Font(this.inputBox.Font, FontStyle.Bold);
                }
            }
            this.inputBox.Enabled = false;
            this.button1.Enabled = false;
        }

        private static List<KeyValuePair<char, int>> getResult(string str) 
        { 
            Dictionary<char, int> dict = new Dictionary<char, int>();

            numOfSpaces = findIdxs(str, ' ').ToArray().Length;
            
            foreach (char c in sWhitespace.Replace(str.ToLower(), ""))
            {
                try
                {
                    dict[c]++;
                } 
                catch
                {
                    dict[c] = 1;
                }
            }

            List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>();

            int tempInt = 0;
            char tempChar = ' ';


            foreach (KeyValuePair<char, int> kvp in dict)
            {
                if (kvp.Value > tempInt)
                {
                    tempInt = kvp.Value;
                    tempChar = kvp.Key;
                }
            }

            list.Add(new KeyValuePair<char, int>(tempChar, tempInt));

            foreach (KeyValuePair<char, int> kvp in dict)
            {
                if (kvp.Value == list.First().Value && !kvp.Key.Equals(list.First().Key))
                {
                    list.Add(kvp);
                }
            }
            return list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.inputBox.Text = "";
            this.inputBox.Enabled = true;
            this.labelResult.Text = "";
            this.button1.Enabled = true;
        }
    }
}
