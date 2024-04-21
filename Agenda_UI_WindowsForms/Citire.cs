//Laborator 7

using System;
using System.Windows.Forms;
using Obiect;
using ManagerDate;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_UI_WindowsForms
{
    public partial class Citire : Form
    {
        private ManagerActivitati managerActivitati = new ManagerActivitati();
        private Activitate activitateNoua = new Activitate();

        private Label lblActivitate;
        private Label lblData;
        private Label lblTip;
        private Label lblDescriere;

        private TextBox txtActivitate;
        private DateTimePicker dtpData;
        private TextBox txtTip;
        private TextBox txtDescriere;

        private Label[] lblsActivitate;
        private Label[] lblsData;
        private Label[] lblsTip;
        private Label[] lblsDescriere;

        private const int LATIME_CONTROL = 100;
        private const int LATIME_CONTROL_TEXT = 400;
        private const int LATIME_CONTROL_DESCRIERE = 400;
        private const int LUNGIME_CONTROL_DESCRIERE = 100;
        private const int DIMENSIUNE_PAS_LABEL_X = 40;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 160;

        public Citire()
        {
            InitializeComponent();
            string numeFisier = "Activitati.txt";
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            Activitate activitatenoua = new Activitate();
            int nrActivitati = 0;
            int nrActivitatiFisier = 0;

            ManagerActivitatiFisier managerActivitatiFisier = new ManagerActivitatiFisier(caleCompletaFisier);
            ManagerActivitati managerActivitati = new ManagerActivitati();

            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Aquamarine;
            this.Text = "Agenda";

            //adaugare control de tip Label pentru 'Activitate';
            lblActivitate = new Label();
            lblActivitate.Width = LATIME_CONTROL;
            lblActivitate.Text = "Nume activitate:";
            lblActivitate.Left = DIMENSIUNE_PAS_LABEL_X;
            lblActivitate.Top = DIMENSIUNE_PAS_Y;
            lblActivitate.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblActivitate);

            //adaugare control de tip TextBox pentru 'Activitate';
            txtActivitate = new TextBox();
            txtActivitate.Width = LATIME_CONTROL_TEXT;
            txtActivitate.Left = DIMENSIUNE_PAS_X;
            txtActivitate.Top = DIMENSIUNE_PAS_Y;
            this.Controls.Add(txtActivitate);

            //adaugare control de tip Label pentru 'Data';
            lblData = new Label();
            lblData.Width = LATIME_CONTROL;
            lblData.Text = "Data:";
            lblData.Left = DIMENSIUNE_PAS_LABEL_X;
            lblData.Top = 2 * DIMENSIUNE_PAS_Y;
            lblData.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblData);

            //adaugare control de tip DateTimePicker pentru 'Data';
            dtpData = new DateTimePicker();
            dtpData.Width = LATIME_CONTROL_TEXT;
            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpData.Left = DIMENSIUNE_PAS_X;
            dtpData.Top = 2 * DIMENSIUNE_PAS_Y;
            this.Controls.Add(dtpData);

            //adaugare control de tip Label pentru 'Tip';
            lblTip = new Label();
            lblTip.Width = LATIME_CONTROL;
            lblTip.Text = "Tip:";
            lblTip.Left = DIMENSIUNE_PAS_LABEL_X;
            lblTip.Top = 3 * DIMENSIUNE_PAS_Y;
            lblTip.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblTip);

            //adaugare control de tip TextBox pentru 'Tip';
            txtTip = new TextBox();
            txtTip.Width = LATIME_CONTROL_TEXT;
            txtTip.Left = DIMENSIUNE_PAS_X;
            txtTip.Top = 3 * DIMENSIUNE_PAS_Y;
            this.Controls.Add(txtTip);

            //adaugare control de tip Label pentru 'Descriere';
            lblDescriere = new Label();
            lblDescriere.Width = LATIME_CONTROL;
            lblDescriere.Text = "Descriere:";
            lblDescriere.Left = DIMENSIUNE_PAS_LABEL_X;
            lblDescriere.Top = 4 * DIMENSIUNE_PAS_Y;
            lblDescriere.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblDescriere);

            //adaugare control de tip TextBox pentru 'Descriere';
            txtDescriere = new TextBox();
            txtDescriere.Width = LATIME_CONTROL_DESCRIERE;
            txtDescriere.Height = LUNGIME_CONTROL_DESCRIERE;
            txtDescriere.WordWrap = true;
            txtDescriere.AcceptsReturn = true;
            txtDescriere.Multiline = true;
            txtDescriere.Left = DIMENSIUNE_PAS_X;
            txtDescriere.Top = 4 * DIMENSIUNE_PAS_Y;
            this.Controls.Add(txtDescriere);

        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Afișează activitățile din fișier la încărcarea formularului
        //    RefreshActivitati();
        //}

        //private void RefreshActivitati()
        //{
        //    // Folosește managerul pentru a obține activitățile și le afișează în ListBox
        //    listBoxActivitati.Items.Clear();
        //    Activitate[] activitati = managerActivitati.GetActivitati(out int nrActivitati);
        //    for (int i = 0; i < nrActivitati; i++)
        //    {
        //        listBoxActivitati.Items.Add(activitati[i].Detalii());
        //    }
        //}

        //private void buttonAdauga_Click(object sender, EventArgs e)
        //{
        //    // Citirea unei activități noi de la utilizator și adăugarea în manager
        //    activitateNoua = CitireActivitate();
        //    managerActivitati.AdaugaActivitate(activitateNoua);
        //    RefreshActivitati();
        //}

        //private Activitate CitireActivitate()
        //{
        //    // Citirea detaliilor activității din controalele formularului
        //    string nume = textBoxNume.Text.Trim();
        //    string tip = textBoxTip.Text.Trim();
        //    DateTime data = dateTimePickerData.Value;
        //    string descriere = textBoxDescriere.Text.Trim();

        //    // Crearea unei noi activități cu detaliile citite
        //    return new Activitate(nume, tip, data, descriere);
        //}

        //private void listBoxActivitati_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Afișează detalii despre activitatea selectată în ListBox
        //    if (listBoxActivitati.SelectedIndex != -1)
        //    {
        //        string detalii = listBoxActivitati.SelectedItem.ToString();
        //        MessageBox.Show(detalii, "Detalii activitate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void buttonSalveazaFisier_Click(object sender, EventArgs e)
        //{
        //    // Salvarea activității curente în fișier
        //    managerActivitati.SalveazaInFisier(activitateNoua);
        //    RefreshActivitati();
        //}
    }
}