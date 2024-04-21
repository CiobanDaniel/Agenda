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
        ManagerActivitatiFisier managerActivitatiFisier;

        private Label lblActivitate;
        private Label lblData;
        private Label lblTip;
        private Label lblDescriere;

        private TextBox txtActivitate;
        private DateTimePicker dtpData;
        private ComboBox lstTip;
        private TextBox txtTip;
        private TextBox txtDescriere;

        //private Label[] lblsActivitate;
        //private Label[] lblsData;
        //private Label[] lblsTip;
        //private Label[] lblsDescriere;

        private Button btnAdauga;
        private Button btnAfiseaza;

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

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            managerActivitatiFisier = new ManagerActivitatiFisier(caleCompletaFisier);
            int nrActivitatiFisier = 0;
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out nrActivitatiFisier);


            this.Size = new Size(700, 400);
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
            lblData.Top = 3 * DIMENSIUNE_PAS_Y;
            lblData.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblData);

            //adaugare control de tip DateTimePicker pentru 'Data';
            dtpData = new DateTimePicker();
            dtpData.Width = LATIME_CONTROL_TEXT;
            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpData.Left = DIMENSIUNE_PAS_X;
            dtpData.Top = 3 * DIMENSIUNE_PAS_Y;
            this.Controls.Add(dtpData);

            //adaugare control de tip Label pentru 'Tip';
            lblTip = new Label();
            lblTip.Width = LATIME_CONTROL;
            lblTip.Text = "Tip:";
            lblTip.Left = DIMENSIUNE_PAS_LABEL_X;
            lblTip.Top = 2 * DIMENSIUNE_PAS_Y;
            lblTip.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblTip);

            //adaugare control de tip ListBox pentru 'Tip';
            lstTip = new ComboBox();
            lstTip.DataSource = Enum.GetValues(typeof(Activitate.TipActivitate));
            lstTip.DropDownStyle = ComboBoxStyle.DropDownList;
            lstTip.FormattingEnabled = true;
            lstTip.Width = LATIME_CONTROL_TEXT;
            lstTip.Left = DIMENSIUNE_PAS_X;
            lstTip.Top = 2 * DIMENSIUNE_PAS_Y;
            lstTip.ForeColor = Color.DarkBlue;
            this.Controls.Add(lstTip);

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
            txtDescriere.AcceptsReturn = false;
            txtDescriere.Multiline = false;
            txtDescriere.Left = DIMENSIUNE_PAS_X;
            txtDescriere.Top = 4 * DIMENSIUNE_PAS_Y;
            this.Controls.Add(txtDescriere);


            //adaugare control de tip Button pentru 'Adauga';
            btnAdauga = new Button();
            btnAdauga.Width = LATIME_CONTROL;
            btnAdauga.Text = "Adauga";
            btnAdauga.Left = DIMENSIUNE_PAS_LABEL_X;
            btnAdauga.Top = 6 * DIMENSIUNE_PAS_Y;
            btnAdauga.Click += BtnAdauga_Click;
            btnAdauga.ForeColor = Color.DarkBlue;
            this.Controls.Add(btnAdauga);

            //adaugare control de tip Button pentru 'Afiseaza';
            btnAfiseaza = new Button();
            btnAfiseaza.Width = LATIME_CONTROL;
            btnAfiseaza.Text = "Afiseaza";
            btnAfiseaza.Left = 2 * DIMENSIUNE_PAS_X;
            btnAfiseaza.Top = 6 * DIMENSIUNE_PAS_Y;
            btnAfiseaza.Click += BtnAfiseaza_Click;
            btnAfiseaza.ForeColor = Color.DarkBlue;
            this.Controls.Add(btnAfiseaza);
        }

        private void BtnAfiseaza_Click(object sender, EventArgs e)
        {
            AfiseazaActivitati();
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            // Citirea unei activități noi de la utilizator și adăugarea în manager
            Activitate activitateNoua = CitireActivitate();
            if (activitateNoua != null)
            {
                managerActivitatiFisier.SalveazaActivitati(activitateNoua);
            }
            //AfiseazaActivitati();
        }

        private Activitate CitireActivitate()
        {
            // Citirea detaliilor activității din controalele formularului
            if (string.IsNullOrEmpty(txtActivitate.Text))
            {
                MessageBox.Show("Introduceti numele activitatii!");
                lblActivitate.ForeColor = Color.Red;
                return null;
            }
            else
            {
                lblActivitate.ForeColor = Color.DarkBlue;
                string nume = txtActivitate.Text.Trim();
                //string tip = lstTip.SelectedItem.ToString();
                string tip = lstTip.Text;
                DateTime data = dtpData.Value;
                string descriere = txtDescriere.Text.Trim();

                // Crearea unei noi activități cu detaliile citite
                return new Activitate(nume, tip, data, descriere);
            }
        }

        private void AfiseazaActivitati()
        {
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out int nrActivitatiFisier);

            Label[] lblsActivitate = new Label[nrActivitatiFisier];
            Label[] lblsData = new Label[nrActivitatiFisier];
            Label[] lblsTip = new Label[nrActivitatiFisier];
            Label[] lblsDescriere = new Label[nrActivitatiFisier];

            //adaugare control de tip Label pentru 'Activitate';
            lblActivitate = new Label();
            lblActivitate.Width = LATIME_CONTROL;
            lblActivitate.Text = "Nume activitate:";
            lblActivitate.Left = DIMENSIUNE_PAS_LABEL_X;
            lblActivitate.Top = 7 * DIMENSIUNE_PAS_Y;
            lblActivitate.ForeColor = Color.DarkBlue;
            lblActivitate.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblActivitate);

            //adaugare control de tip Label pentru 'Tip';
            lblTip = new Label();
            lblTip.Width = LATIME_CONTROL;
            lblTip.Text = "Tip:";
            lblTip.Left = DIMENSIUNE_PAS_LABEL_X + DIMENSIUNE_PAS_X;
            lblTip.Top = 7 * DIMENSIUNE_PAS_Y;
            lblTip.ForeColor = Color.DarkBlue;
            lblTip.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTip);

            //adaugare control de tip Label pentru 'Data';
            lblData = new Label();
            lblData.Width = LATIME_CONTROL;
            lblData.Text = "Data:";
            lblData.Left = DIMENSIUNE_PAS_LABEL_X + 2 * DIMENSIUNE_PAS_X;
            lblData.Top = 7 * DIMENSIUNE_PAS_Y;
            lblData.ForeColor = Color.DarkBlue;
            lblData.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblData);

            //adaugare control de tip Label pentru 'Descriere';
            lblDescriere = new Label();
            lblDescriere.Width = LATIME_CONTROL;
            lblDescriere.Text = "Descriere:";
            lblDescriere.Left = DIMENSIUNE_PAS_LABEL_X + 3 * DIMENSIUNE_PAS_X;
            lblDescriere.Top = 7 * DIMENSIUNE_PAS_Y;
            lblDescriere.ForeColor = Color.DarkBlue;
            lblDescriere.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblDescriere);

            int i = 0;
            foreach (Activitate activitate in activitatiFisier)
            {
                //adaugare control de tip Label pentru activitati
                lblsActivitate[i] = new Label();
                lblsActivitate[i].Width = LATIME_CONTROL;
                lblsActivitate[i].Text = activitate.Nume;
                lblsActivitate[i].Left = DIMENSIUNE_PAS_LABEL_X;
                lblsActivitate[i].Top = (i + 8) * DIMENSIUNE_PAS_Y;
                lblsActivitate[i].ForeColor = Color.DarkBlue;
                this.Controls.Add(lblsActivitate[i]);

                //adaugare control de tip Label pentru date
                lblsData[i] = new Label();
                lblsData[i].Width = LATIME_CONTROL;
                lblsData[i].Text = $"{DateTime.Parse(activitate.Data.ToString())}";
                lblsData[i].Left = 2 * DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsData[i].Top = (i + 8) * DIMENSIUNE_PAS_Y;
                lblsData[i].ForeColor = Color.DarkBlue;
                this.Controls.Add(lblsData[i]);

                //adaugare control de tip Label pentru tip
                lblsTip[i] = new Label();
                lblsTip[i].Width = LATIME_CONTROL;
                lblsTip[i].Text = activitate.Tip.ToString();
                lblsTip[i].Left = DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsTip[i].Top = (i + 8) * DIMENSIUNE_PAS_Y;
                lblsTip[i].ForeColor = Color.DarkBlue;
                this.Controls.Add(lblsTip[i]);

                //adaugare control de tip Label pentru descriere
                lblsDescriere[i] = new Label();
                lblsDescriere[i].Width = LATIME_CONTROL;
                lblsDescriere[i].Text = activitate.Descriere;
                lblsDescriere[i].Left = 3 * DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsDescriere[i].Top = (i + 8) * DIMENSIUNE_PAS_Y;
                lblsDescriere[i].ForeColor = Color.DarkBlue;
                this.Controls.Add(lblsDescriere[i]);

                i++;
            }
        }
    }
}