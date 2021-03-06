﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchCounter
{
    public partial class Main : Form
    {
        string textString = ""; //Takes value from txt_text (the textbox next to "Text: ")
        int counterNum = 0; //Takes value from num_counter (the numbox next to "Counter: ")
        string output = ""; //Takes both previous values and combines them into the string to be output to the text file.

        //output currently set to desktop as counter_output.txt
        string output_path = (Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\counter_output.txt");


        public Main()
        {
            InitializeComponent();
        }

        private void ioErrorMessage() //outputs an IO error message
        {
            MessageBox.Show("File could not be written to.", "IO Error", MessageBoxButtons.OK);
        }

        private bool checkOutput() //checks to see if output file exists. If not, create it.
        {
            try //if output_text already exists
            {
                StreamReader outRead = new StreamReader(output_path);
                outRead.Close();

                return true;

                //reads text in file and sets it to lbl_preview then closes the file
            }
            catch //if output_text does not exist
            {
                StreamWriter outWrite = new StreamWriter(output_path, false, Encoding.UTF8);
                outWrite.Close();

                return false;

                //creates the file then closes it
            }
        }

        private void writeOutput(string o) //writes to output_text.txt
        {
            try
            {
                StreamWriter outWrite = new StreamWriter(output_path, false);

                outWrite.WriteLine(o);
                lbl_preview.Text = o;
                
                //MessageBox.Show(o, "Debug", MessageBoxButtons.OK);
                
                outWrite.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Error", MessageBoxButtons.OK);
            }
            
        }


        private void Main_Load(object sender, EventArgs e) //on load
        {
            alwaysOnTopToolStripMenuItem.Checked = false; //initializes always on top to false

            checkOutput();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //When update button is clicked, it converts the counted number to a string,
            //and combines the two to output to preview and file.
            textString = txt_text.Text;
            counterNum = Convert.ToInt32(num_counter.Value);

            output = textString + " " + counterNum.ToString();

            //actual writing to file and preview

            bool result = false; //initialize to false
            for (int i = 0; i < 3; i++) //checks three times to avoid infinite loop
            {
                result = checkOutput();

                if (result == true) //if file exists, write output
                {
                    writeOutput(output);
                    i = 3; //end loop
                }
               else//if file doesn't exist, create it and try again
                {
                    if (i == 3)//if the loop is supposed to end and file still cannot be written, output error
                    {
                        ioErrorMessage();
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //closes the application
        {
            Application.Exit(); 
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) //shows the about page/form
        {
            Form aboutPage = new About();
            aboutPage.Show(); 
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e) //shows the help page/form
        {
            Form helpPage = new Help();
            helpPage.Show();
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e) //checks if always on top is checked or not and stores the result in checkBool.
        {
            
            bool checkBool;
            checkBool = alwaysOnTopToolStripMenuItem.Checked; 

            
            if (checkBool == false) //if always on top is not checked
            {
                //change always on top to true
                alwaysOnTopToolStripMenuItem.Checked = true;
                this.TopMost = true;
            }
            else //if always on top is checked
            {
                //change always on top to false
                alwaysOnTopToolStripMenuItem.Checked = false;
                this.TopMost = false;
            }
            
        }
    }
}
