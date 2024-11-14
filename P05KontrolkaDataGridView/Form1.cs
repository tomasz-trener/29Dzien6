﻿using P04Zawodnicy.Shared.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P05KontrolkaDataGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnWyslij_Click(object sender, EventArgs e)
        {
            PolaczenieZBaza pzb; 
            if(string.IsNullOrWhiteSpace(txtParametryPolaczenia.Text))
            {
                var d= MessageBox.Show("Nie podano parametrow polaczenia. Czy chcesz uzyc domylsnych","Pytanie",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (d == DialogResult.Yes)
                    pzb = new PolaczenieZBaza();
                else
                    return;
            }
            else
                pzb = new PolaczenieZBaza(txtParametryPolaczenia.Text);

            if (string.IsNullOrWhiteSpace(txtPolecenieSQL.Text))
            {
                MessageBox.Show("Nie podano polecenia SQL","Ostrzezenie",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; 
            }

            try
            {
                (string[] naglowki, object[][] wynik)=  pzb.WykonajPolecenieSQLPlusNaglowki(txtPolecenieSQL.Text);

                dgvDane.Rows.Clear();
                if (wynik.Length > 0)
                {
                    dgvDane.ColumnCount = wynik[0].Length;

                    for (int i = 0; i < naglowki.Length; i++)
                        dgvDane.Columns[i].HeaderText = naglowki[i];

                    foreach (var wiersz in wynik)
                        dgvDane.Rows.Add(wiersz);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Bład bazy danych");
            }
            catch (Exception)
            {
                MessageBox.Show("nieznany bład", "bład aplikacji");
            }
          

        }
    }
}
