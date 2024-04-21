//Laborator 6

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
    public partial class Afisare : Form
    {

        ManagerActivitatiFisier managerActivitatiFisier;

        private Label lblTitlu;

        private Label lblActivitate;
        private Label lblData;
        private Label lblTip;
        private Label lblDescriere;

        private Label[] lblsActivitate;
        private Label[] lblsData;
        private Label[] lblsTip;
        private Label[] lblsDescriere;

        private const int LATIME_CONTROL = 100;
        private const int DIMENSIUNE_PAS_LABEL_X = 40;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 160;

        public Afisare()
        {

            InitializeComponent();
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = locatieFisierSolutie + "\\" + numeFisier;

            managerActivitatiFisier = new ManagerActivitatiFisier(caleCompletaFisier);
            int nrActivitatiFisier = 0;
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out nrActivitatiFisier);

            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.Aquamarine;
            this.Text = "Agenda";

            //adaugare control de tip Label pentru 'Titlu';
            lblTitlu = new Label();
            lblTitlu.Text = "ACTIVITĂȚI";
            lblTitlu.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            lblTitlu.ForeColor = Color.DarkBlue;
            lblTitlu.TextAlign = ContentAlignment.MiddleCenter;
            lblTitlu.Anchor = AnchorStyles.Top;
            lblTitlu.AutoSize = true;
            lblTitlu.BorderStyle = BorderStyle.Fixed3D;
            lblTitlu.BackColor = Color.Aquamarine;
            lblTitlu.Left = (this.ClientSize.Width - lblTitlu.Width) / 2;
            this.Controls.Add(lblTitlu);

            //adaugare control de tip Label pentru 'Activitate';
            lblActivitate = new Label();
            lblActivitate.Width = LATIME_CONTROL;
            lblActivitate.Text = "Nume activitate:";
            lblActivitate.Left = DIMENSIUNE_PAS_LABEL_X;
            lblActivitate.Top =  DIMENSIUNE_PAS_Y;
            lblActivitate.ForeColor = Color.DarkBlue;
            lblActivitate.BackColor = Color.Aquamarine;
            lblActivitate.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblActivitate);

            //adaugare control de tip Label pentru 'Tip';
            lblTip = new Label();
            lblTip.Width = LATIME_CONTROL;
            lblTip.Text = "Tip:";
            lblTip.Left = DIMENSIUNE_PAS_LABEL_X + DIMENSIUNE_PAS_X;
            lblTip.Top = DIMENSIUNE_PAS_Y;
            lblTip.ForeColor = Color.DarkBlue;
            lblTip.BackColor = Color.Aquamarine;
            lblTip.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTip);

            //adaugare control de tip Label pentru 'Data';
            lblData = new Label();
            lblData.Width = LATIME_CONTROL;
            lblData.Text = "Data:";
            lblData.Left = DIMENSIUNE_PAS_LABEL_X + 2 * DIMENSIUNE_PAS_X;
            lblData.Top = DIMENSIUNE_PAS_Y;
            lblData.ForeColor = Color.DarkBlue;
            lblData.BackColor = Color.Aquamarine;
            lblData.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblData);

            //adaugare control de tip Label pentru 'Descriere';
            lblDescriere = new Label();
            lblDescriere.Width = LATIME_CONTROL;
            lblDescriere.Text = "Descriere:";
            lblDescriere.Left = DIMENSIUNE_PAS_LABEL_X + 3 * DIMENSIUNE_PAS_X;
            lblDescriere.Top = DIMENSIUNE_PAS_Y;
            lblDescriere.ForeColor = Color.DarkBlue;
            lblDescriere.BackColor = Color.Aquamarine;
            lblDescriere.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblDescriere);


            AfiseazaActivitati();
        }

        private void Afisare_Load(object sender, EventArgs e)
        {
            AfiseazaActivitati();
        }

        private void AfiseazaActivitati()
        {
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out int nrActivitatiFisier);

            Label[] lblsActivitate = new Label[nrActivitatiFisier];
            Label[] lblsData = new Label[nrActivitatiFisier];
            Label[] lblsTip = new Label[nrActivitatiFisier];
            Label[] lblsDescriere = new Label[nrActivitatiFisier];

            int i = 0;
            foreach (Activitate activitate in activitatiFisier)
            {
                //adaugare control de tip Label pentru activitati
                lblsActivitate[i] = new Label();
                lblsActivitate[i].Width = LATIME_CONTROL;
                lblsActivitate[i].Text = activitate.Nume;
                lblsActivitate[i].Left = DIMENSIUNE_PAS_LABEL_X;
                lblsActivitate[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsActivitate[i]);

                //adaugare control de tip Label pentru date
                lblsData[i] = new Label();
                lblsData[i].Width = LATIME_CONTROL;
                lblsData[i].Text = $"{activitate.Data}";
                lblsData[i].Left = 2 * DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsData[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsData[i]);

                //adaugare control de tip Label pentru notele studentilor
                lblsTip[i] = new Label();
                lblsTip[i].Width = LATIME_CONTROL;
                lblsTip[i].Text = $"{activitate.Tip}";
                lblsTip[i].Left = DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsTip[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsTip[i]);

                //adaugare control de tip Label pentru descriere
                lblsTip[i] = new Label();
                lblsTip[i].Width = LATIME_CONTROL;
                lblsTip[i].Text = activitate.Descriere;
                lblsTip[i].Left = 3 * DIMENSIUNE_PAS_X + DIMENSIUNE_PAS_LABEL_X;
                lblsTip[i].Top = (i + 2) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsTip[i]);

                i++;
            }
        }
    }
}
