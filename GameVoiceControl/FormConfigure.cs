/**
*
	Copyright (c) 2020 Chay Palton

	Permission is hereby granted, free of charge, to any person obtaining
	a copy of this software and associated documentation files (the "Software"),
	to deal in the Software without restriction, including without limitation
	the rights to use, copy, modify, merge, publish, distribute, sublicense,
	and/or sell copies of the Software, and to permit persons to whom the Software
	is furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in
	all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
	IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
	CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
	TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
	OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameVoiceControl
{
    public partial class FormConfigure : Form
    {

        //GVCommand gvcommand = null;
        Color colorTitle = Color.FromArgb(112, 125, 240);
        Color colorBG = Color.FromArgb(2, 65, 180);
        Color colorValueBG = Color.FromArgb(30, 90,170);
        Color colorHeadingBG = Color.FromArgb(2, 55, 160);
        Color colourHeadingFG = Color.FromArgb(140, 189, 249);
        Color colourSelectFG = Color.FromArgb(255, 255, 255);
        Color colourSelectBG = Color.FromArgb(40, 100, 200);
        // Color colourBG= Color.FromArgb(112, 60, 160);

        public FormConfigure()
        {
            InitializeComponent();
        }

        public void LoadData()
        {

            //gvcommand = gvc;
            /*
            int groupid = -1;
            int len = gvcommand.commands.Count();

            for (var n = 0; n < len; n++)
            {
                string ks = "";

                for (var k = 0; k < gvcommand.commands[n].key.Count(); k++)
                {
                    if (k > 0) ks += " ; ";
                     ks += gvcommand.commands[n].key[k];
                }

                if(gvcommand.commands[n].groupid != groupid)
                {
                    // ADD TITLE 
                    dataGridView1.Rows.Add("TITLE","", "");
                }

                dataGridView1.Rows.Add(gvcommand.commands[n].commandName, 
                  ks, gvcommand.commands[n].word);
            }*/
        }



        private void FormConfigure_Load(object sender, EventArgs e)
        {
  
            string groupid = "";
            int len = GVCommand.commands.Count();
            int row = 0;

            BackColor = colorBG;
            // Set a cell padding to provide space for the top of the focus 
            // rectangle and for the content that spans multiple columns. 
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = colorBG;
            dataGridView1.GridColor = colorBG;

            Padding newPadding = new Padding(2, 2, 2, 2);
            this.dataGridView1.RowTemplate.DefaultCellStyle.Padding = newPadding;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns[0].Width = 380;
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Selected = false;
            dataGridView1.Columns[0].DividerWidth = 4;
            dataGridView1.Columns[1].DividerWidth = 4;
            dataGridView1.Columns[2].DividerWidth = 4;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;

            DataGridViewCellStyle rowsDefaultCellStyle = dataGridView1.RowsDefaultCellStyle.Clone();
            rowsDefaultCellStyle.Font = new Font(rowsDefaultCellStyle.Font.Name, rowsDefaultCellStyle.Font.Size +4);


            for (var n = 0; n < len; n++)
            {
                string ks = "";

                for (var k = 0; k < GVCommand.commands[n].key.Count(); k++)
                {
                    if (k > 0) ks += " ; ";
                    ks += GVCommand.commands[n].key[k];
                }

                if (GVCommand.commands[n].groupid != groupid)
                {
                    // ADD TITLE 

                    row = dataGridView1.Rows.Add(GVCommand.commands[n].groupid, "", "");
                    groupid = GVCommand.commands[n].groupid;
                
                    dataGridView1.Rows[row].Height = 70;
                    dataGridView1.Rows[row].DividerHeight = 4;
                    dataGridView1.Rows[row].Cells[0].Selected = false;

                    dataGridView1.Rows[row].Cells[0].Style = new DataGridViewCellStyle(rowsDefaultCellStyle);
                    dataGridView1.Rows[row].Cells[0].Style.SelectionBackColor = colorBG;
                    dataGridView1.Rows[row].Cells[0].Style.SelectionForeColor = colorTitle;
                    dataGridView1.Rows[row].Cells[0].Style.BackColor = colorBG;
                    dataGridView1.Rows[row].Cells[0].Style.ForeColor = colorTitle;
                    //dataGridView1.Rows[row].Cells[0].Style. = colourHeadingFG;

                    // dataGridView1.Rows[n].Height = 40;
                    // dataGridView1.Rows[n].DividerHeight = 4;
                    dataGridView1.Rows[row].Cells[1].Style.SelectionBackColor = colorBG;
                    dataGridView1.Rows[row].Cells[1].Style.SelectionForeColor = colorBG;
                    dataGridView1.Rows[row].Cells[1].Style.BackColor = colorBG;
                    dataGridView1.Rows[row].Cells[1].Style.ForeColor = colorBG;

                    //  dataGridView1.Rows[n].Height = 40;
                    //  dataGridView1.Rows[n].DividerHeight = 4;
                    dataGridView1.Rows[row].Cells[2].Style.SelectionBackColor = colorBG;
                    dataGridView1.Rows[row].Cells[2].Style.SelectionForeColor = colorBG;
                    dataGridView1.Rows[row].Cells[2].Style.BackColor = colorBG;
                    dataGridView1.Rows[row].Cells[2].Style.ForeColor = colorBG;
                }

                row =  dataGridView1.Rows.Add(GVCommand.commands[n].action,
                  ks, GVCommand.commands[n].word);

                //  for (var n = 0; n < dataGridView1.Rows.Count; n++)
                //{
                dataGridView1.Rows[row].Height = 40;
                dataGridView1.Rows[row].DividerHeight = 4;
                dataGridView1.Rows[row].Cells[0].Style.SelectionBackColor = colorHeadingBG;
                dataGridView1.Rows[row].Cells[0].Style.SelectionForeColor = colourHeadingFG;
                dataGridView1.Rows[row].Cells[0].Style.BackColor = colorHeadingBG;
                dataGridView1.Rows[row].Cells[0].Style.ForeColor = colourHeadingFG;

               // dataGridView1.Rows[n].Height = 40;
               // dataGridView1.Rows[n].DividerHeight = 4;
                dataGridView1.Rows[row].Cells[1].Style.SelectionBackColor = colourSelectBG;
                dataGridView1.Rows[row].Cells[1].Style.SelectionForeColor = colourSelectFG;
                dataGridView1.Rows[row].Cells[1].Style.BackColor = colorValueBG;
                dataGridView1.Rows[row].Cells[1].Style.ForeColor = colourHeadingFG;

              //  dataGridView1.Rows[n].Height = 40;
              //  dataGridView1.Rows[n].DividerHeight = 4;
                dataGridView1.Rows[row].Cells[2].Style.SelectionBackColor = colourSelectBG;
                dataGridView1.Rows[row].Cells[2].Style.SelectionForeColor = colourSelectFG;
                dataGridView1.Rows[row].Cells[2].Style.BackColor = colorValueBG;
                dataGridView1.Rows[row].Cells[2].Style.ForeColor = colourHeadingFG;
           }
        }
    }
}
