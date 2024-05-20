using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Obiect;
using ManagerDate;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Agenda_UI_WindowsForms
{
    public partial class Citire : Form
    {
        enum CriteriiCautare
        {
            Nimic = 0,
            Nume = 1,
            Tip = 2,
            Prioritate = 3,
        }

        ManagerActivitatiFisier managerActivitatiFisier;

        private Label lblActivitate;
        private Label lblData;
        private Label lblTip;
        private Label lblDescriere;
        private Label lblPrioritate;
        private Label lblOptiuni;

        private Label lblLinie;

        private TextBox txtActivitate;
        private DateTimePicker dtpData;
        private ComboBox lstTip;
        private TextBox txtDescriere;

        private CheckBox checkOptiune1;
        private CheckBox checkOptiune2;
        private CheckBox checkOptiune3;

        private RadioButton rbPrioritate0;
        private RadioButton rbPrioritate1;
        private RadioButton rbPrioritate2;
        private RadioButton rbPrioritate3;

        private Button btnAdauga;
        private Button btnAfiseaza;
        private Button btnCauta;
        private Button btnSterge;
        private Button btnAscunde;
        private Button btnRefresh;
        private DataGridView dgvAfisare;

        private ComboBox cbCriteriu;
        private TextBox txtCautare;

        private const int LATIME_CONTROL = 100;
        private const int LATIME_BUTON = 170;
        private const int LATIME_CONTROL_TEXT = 400;
        private const int LATIME_CONTROL_DESCRIERE = 400;
        private const int LUNGIME_CONTROL_DESCRIERE = 100;
        private const int DIMENSIUNE_PAS_LABEL_X = 40;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 190;

        private static int widthAfisare = 1500;
        private static int widthForm = 600;
        private static int heightForm = 400;

        public Citire()
        {
            InitializeComponent();

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string caleCompletaFisier = Path.Combine(locatieFisierSolutie, numeFisier);

            managerActivitatiFisier = new ManagerActivitatiFisier(caleCompletaFisier);
            int nrActivitatiFisier = 0;
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out nrActivitatiFisier);

            this.Size = new Size(widthForm, heightForm);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            //this.ForeColor = Color.Aquamarine;
            this.BackColor = Color.LightGray;
            this.Text = "Agenda";

            // DataGridView setup
            dgvAfisare = new DataGridView
            {
                Top = DIMENSIUNE_PAS_Y,
                Left = widthForm + 100,
                Width = 730,
                Height = 300,
                ColumnCount = 6,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ForeColor = Color.DarkBlue,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
            };
            dgvAfisare.Hide();
            dgvAfisare.AutoResizeColumns();
            this.Controls.Add(dgvAfisare);

            dgvAfisare.Columns[0].HeaderText = "Nume";
            dgvAfisare.Columns[1].HeaderText = "Tip";
            dgvAfisare.Columns[2].HeaderText = "Data";
            dgvAfisare.Columns[3].HeaderText = "Descriere";
            dgvAfisare.Columns[4].HeaderText = "Prioritate";
            dgvAfisare.Columns[5].HeaderText = "Optiuni";

            // Label for 'Activitate'
            lblActivitate = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Nume activitate:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblActivitate);

            // TextBox for 'Activitate'
            txtActivitate = new TextBox
            {
                Width = LATIME_CONTROL_TEXT,
                Left = DIMENSIUNE_PAS_X,
                Top = DIMENSIUNE_PAS_Y
            };
            this.Controls.Add(txtActivitate);

            // Label for 'Data'
            lblData = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Data:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 3 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblData);

            // DateTimePicker for 'Data'
            dtpData = new DateTimePicker
            {
                Width = LATIME_CONTROL_TEXT,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy HH:mm",
                Left = DIMENSIUNE_PAS_X,
                Top = 3 * DIMENSIUNE_PAS_Y
            };
            this.Controls.Add(dtpData);

            // Label for 'Tip'
            lblTip = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Tip:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 2 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblTip);

            // ComboBox for 'Tip'
            lstTip = new ComboBox
            {
                DataSource = Enum.GetValues(typeof(Activitate.TipActivitate)),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = LATIME_CONTROL_TEXT,
                Left = DIMENSIUNE_PAS_X,
                Top = 2 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lstTip);

            // Label for 'Descriere'
            lblDescriere = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Descriere:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 4 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblDescriere);

            // TextBox for 'Descriere'
            txtDescriere = new TextBox
            {
                Width = LATIME_CONTROL_DESCRIERE,
                Height = LUNGIME_CONTROL_DESCRIERE,
                WordWrap = true,
                AcceptsReturn = false,
                Multiline = false,
                Left = DIMENSIUNE_PAS_X,
                Top = 4 * DIMENSIUNE_PAS_Y
            };
            this.Controls.Add(txtDescriere);

            // Label for 'Prioritate'
            lblPrioritate = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Prioritate:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 5 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblPrioritate);

            // RadioButton for 'Prioritate'
            rbPrioritate0 = new RadioButton
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipPrioritate), 0),
                Width = 100,
                Left = DIMENSIUNE_PAS_X + 10,
                Top = 5 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
                Checked = true
            };
            this.Controls.Add(rbPrioritate0);

            rbPrioritate1 = new RadioButton
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipPrioritate), 1),
                Width = 80,
                Left = DIMENSIUNE_PAS_X + 100 + DIMENSIUNE_PAS_LABEL_X,
                Top = 5 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(rbPrioritate1);

            rbPrioritate2 = new RadioButton
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipPrioritate), 2),
                Width = 80,
                Left = DIMENSIUNE_PAS_X + 160 + 2 * DIMENSIUNE_PAS_LABEL_X,
                Top = 5 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(rbPrioritate2);

            rbPrioritate3 = new RadioButton
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipPrioritate), 3),
                Width = 80,
                Left = DIMENSIUNE_PAS_X + 220 + 3 * DIMENSIUNE_PAS_LABEL_X,
                Top = 5 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(rbPrioritate3);

            // Label for 'Optiuni'
            lblOptiuni = new Label
            {
                Width = LATIME_CONTROL,
                Text = "Optiuni:",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 6 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(lblOptiuni);

            // CheckBox for 'Optiuni'
            checkOptiune1 = new CheckBox
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipOptiuni),1),
                Width = 100,
                Left = DIMENSIUNE_PAS_X + 10,
                Top = 6 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(checkOptiune1);

            checkOptiune2 = new CheckBox
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipOptiuni), 2),
                Width = 100,
                Left = DIMENSIUNE_PAS_X + 100 + DIMENSIUNE_PAS_LABEL_X,
                Top = 6 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(checkOptiune2);

            checkOptiune3 = new CheckBox
            {
                Appearance = Appearance.Normal,
                Text = Enum.GetName(typeof(Activitate.TipOptiuni), 3),
                Width = 100,
                Left = DIMENSIUNE_PAS_X + 200 + 2 * DIMENSIUNE_PAS_LABEL_X,
                Top = 6 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
            };
            this.Controls.Add(checkOptiune3);

            // Label for 'Linie'
            lblLinie = new Label
            {
                Width = 2,
                BorderStyle = BorderStyle.FixedSingle,
                Text = string.Empty,
                Left = widthForm + 50,
                Top = DIMENSIUNE_PAS_Y,
                AutoSize = false,
                Height = 330,
                BackColor = Color.Black,
            };
            lblLinie.Hide();
            this.Controls.Add(lblLinie);

            // Button for 'Adauga'
            btnAdauga = new Button
            {
                Width = LATIME_BUTON,
                Text = "Adauga",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 7 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnAdauga.Click += BtnAdauga_Click;
            this.Controls.Add(btnAdauga);

            // Button for 'Afiseaza'
            btnAfiseaza = new Button
            {
                Width = LATIME_BUTON,
                Text = "Afiseaza",
                Left = DIMENSIUNE_PAS_LABEL_X + DIMENSIUNE_PAS_X,
                Top = 7 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnAfiseaza.Click += BtnAfiseaza_Click;
            this.Controls.Add(btnAfiseaza);

            // Button for 'Cauta'
            btnCauta = new Button
            {
                Width = LATIME_BUTON,
                Text = "Cauta",
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 8 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnCauta.Click += BtnCauta_Click;
            this.Controls.Add(btnCauta);

            // Button for 'Sterge'
            btnSterge = new Button
            {
                Width = LATIME_BUTON,
                Text = "Sterge",
                Left = widthForm + 300,
                Top = 11 * DIMENSIUNE_PAS_Y + 5,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnSterge.Hide();
            btnSterge.Click += BtnSterge_Click;
            this.Controls.Add(btnSterge);

            // Button for 'Ascunde'
            btnAscunde = new Button
            {
                Width = LATIME_BUTON,
                Text = "Ascunde",
                Left = widthForm + 100,
                Top = 11 * DIMENSIUNE_PAS_Y + 5,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnAscunde.Hide();
            btnAscunde.Click += BtnAscunde_Click;
            this.Controls.Add(btnAscunde);

            // Button for 'Refresh'
            btnRefresh = new Button
            {
                Width = LATIME_BUTON,
                Text = "Refresh",
                Left = widthForm + 500,
                Top = 11 * DIMENSIUNE_PAS_Y + 5,
                ForeColor = Color.DarkBlue,
                BackColor = Color.White
            };
            btnRefresh.Hide();
            btnRefresh.Click += BtnRefresh_Click;
            this.Controls.Add(btnRefresh);

            // ComboBox for 'Cautare'
            cbCriteriu = new ComboBox
            {
                DataSource = Enum.GetValues(typeof(CriteriiCautare)),
                Width = LATIME_CONTROL,
                Left = DIMENSIUNE_PAS_LABEL_X + DIMENSIUNE_PAS_X,
                Top = 8 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(cbCriteriu);

            // TextBox for 'Cautare'
            txtCautare = new TextBox
            {
                Text = null,
                Width = LATIME_CONTROL_TEXT,
                Left = DIMENSIUNE_PAS_LABEL_X,
                Top = 9 * DIMENSIUNE_PAS_Y,
                ForeColor = Color.DarkBlue
            };
            this.Controls.Add(txtCautare);
        }

        private void rbPrioritate0_Click(object sender, EventArgs e)
        {
            rbPrioritate0.Checked = true;
            rbPrioritate1.Checked = false;
            rbPrioritate2.Checked = false;
            rbPrioritate3.Checked = false;
        }
        private void rbPrioritate1_Click(object sender, EventArgs e)
        {
            rbPrioritate0.Checked = false;
            rbPrioritate1.Checked = true;
            rbPrioritate2.Checked = false;
            rbPrioritate3.Checked = false;
        }
        private void rbPrioritate2_Click(object sender, EventArgs e)
        {
            rbPrioritate0.Checked = false;
            rbPrioritate1.Checked = false;
            rbPrioritate2.Checked = true;
            rbPrioritate3.Checked = false;
        }
        private void rbPrioritate3_Click(object sender, EventArgs e)
        {
            rbPrioritate0.Checked = false;
            rbPrioritate1.Checked = false;
            rbPrioritate2.Checked = false;
            rbPrioritate3.Checked = true;
        }

        private void BtnCauta_Click(object sender, EventArgs e)
        {
            btnAdauga.ForeColor = Color.DarkBlue;
            btnSterge.ForeColor = Color.DarkBlue;
            if (cbCriteriu.SelectedItem.ToString() != "Nimic" && txtCautare.Text != null)
            {
                this.Size = new Size(widthAfisare, heightForm + 56);
                btnCauta.ForeColor = Color.DarkBlue;
                dgvAfisare.Show();
                dgvAfisare.Rows.Clear();
                lblLinie.Show();
                btnAscunde.Show();
                btnSterge.Show();
                string criteriu = cbCriteriu.SelectedItem.ToString();
                string valoareCautare = txtCautare.Text;
                int nrActivitatiFisier = 0;
                Activitate[] activitati = managerActivitatiFisier.GetActivitati(out nrActivitatiFisier);

                var rezultateCautare = activitati.Where(a => {
                    switch (criteriu)
                    {
                        case "Nume":
                            return a.Nume.IndexOf(valoareCautare, StringComparison.OrdinalIgnoreCase) >= 0;
                        case "Tip":
                            return a.Tip.ToString().IndexOf(valoareCautare, StringComparison.OrdinalIgnoreCase) >= 0;
                        case "Prioritate":
                            return a.Prioritate.ToString().IndexOf(valoareCautare, StringComparison.OrdinalIgnoreCase) >= 0;
                        default:
                            return false;
                    }
                }).ToArray();

                dgvAfisare.Rows.Clear();
                dgvAfisare.Show();

                foreach (Activitate activitate in rezultateCautare)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvAfisare);
                    row.Cells[0].Value = activitate.Nume;
                    row.Cells[1].Value = activitate.Tip;
                    row.Cells[2].Value = activitate.Data;
                    row.Cells[3].Value = activitate.Descriere;
                    row.Cells[4].Value = activitate.Prioritate;
                    row.Cells[5].Value = string.Join(",", activitate.Optiuni);

                    dgvAfisare.Rows.Add(row);
                }
            }
            else
            {
                btnCauta.ForeColor = Color.Red;
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            btnAdauga.ForeColor = Color.DarkBlue;
            btnCauta.ForeColor = Color.DarkBlue;
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out int nrActivitatiFisier);
            int index = dgvAfisare.CurrentCell.RowIndex;
            if (index == -1 || nrActivitatiFisier == index)
            {
                btnSterge.ForeColor = Color.Red;
                return;
            }
            else
            {
                btnSterge.ForeColor = Color.DarkBlue;
                managerActivitatiFisier.StergeActivitate(activitatiFisier[index]);
                AfiseazaActivitati();
            }
        }

        private void BtnAscunde_Click(object sender, EventArgs e)
        {
            dgvAfisare.Hide();
            lblLinie.Hide();
            btnAscunde.Hide();
            this.Size = new Size(widthForm + 97, heightForm + 56);
        }

        private void BtnAdauga_Click(object sender, EventArgs e)
        {
            dgvAfisare.Hide();
            lblLinie.Hide();
            btnAscunde.Hide();
            btnSterge.Hide();
            btnRefresh.Hide();
            this.Size = new Size(widthForm + 97, heightForm + 56);
            // Citirea unei activități noi de la utilizator și adăugarea în manager
            Activitate activitateNoua = CitireActivitate();
            if (activitateNoua != null)
            {
                managerActivitatiFisier.SalveazaActivitati(activitateNoua);
            }
            txtActivitate.Text = string.Empty;
            txtDescriere.Text = string.Empty;
            lstTip.SelectedIndex = 0;
            checkOptiune1.Checked = false;
            checkOptiune2.Checked = false;
            checkOptiune3.Checked = false;
            rbPrioritate0.Checked = true;
            rbPrioritate1.Checked = false;
            rbPrioritate2.Checked = false;
            rbPrioritate3.Checked = false;
        }

        private void BtnAfiseaza_Click(object sender, EventArgs e)
        {
            this.Size = new Size(widthAfisare, heightForm + 56);
            AfiseazaActivitati();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            this.Size = new Size(widthAfisare, heightForm + 56);
            AfiseazaActivitati();
        }

        private Activitate CitireActivitate()
        {
            lblActivitate.ForeColor = Color.DarkBlue;
            btnSterge.ForeColor = Color.DarkBlue;
            // Citirea detaliilor activității din controalele formularului
            if (string.IsNullOrEmpty(txtActivitate.Text))
            {
                lblActivitate.ForeColor = Color.Red;
                return null;
            }
            else
            {
                btnCauta.ForeColor = Color.DarkBlue;
                string nume = txtActivitate.Text.Trim();
                //string tip = lstTip.SelectedItem.ToString();
                string tip = lstTip.Text;
                string data = dtpData.Text;
                string descriere = txtDescriere.Text.Trim();
                string prioritate = "0";
                if (rbPrioritate1.Checked == true)
                {
                    prioritate = "1";
                }
                if (rbPrioritate2.Checked == true)
                {
                    prioritate = "2";
                }
                if (rbPrioritate3.Checked == true)
                {
                    prioritate = "3";
                }

                List<string> optiuniList = new List<string>();
                optiuniList.Add(Convert.ToString(Activitate.TipOptiuni.Fara));
                if (checkOptiune1.Checked | checkOptiune2.Checked | checkOptiune3.Checked)
                {
                    optiuniList.Clear();
                }
                if (checkOptiune1.Checked)
                    optiuniList.Add(checkOptiune1.Text);

                if (checkOptiune2.Checked)
                    optiuniList.Add(checkOptiune2.Text);

                if (checkOptiune3.Checked)
                    optiuniList.Add(checkOptiune3.Text);

                string[] optiuni = optiuniList.ToArray();

                DateTime parsedDate;
                if (DateTime.TryParseExact(dtpData.Text, "yyyy-MM-ddTHH:mmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedDate) ||
                    DateTime.TryParse(dtpData.Text, out parsedDate) || DateTime.TryParseExact(dtpData.Text, "MM-dd-yyytTHH:mmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedDate))
                {
                    string formattedDate = parsedDate.ToString("dd.MM.yyyy HH:mm");

                    // Crearea unei noi activități cu detaliile citite
                    return new Activitate(nume, tip, formattedDate, descriere, prioritate, optiuni);
                }

                return new Activitate();

                //return new Activitate(nume, tip, data, descriere, prioritate, optiuni);
            }
        }

        private void AfiseazaActivitati()
        {
            dgvAfisare.Show();
            dgvAfisare.Rows.Clear();
            lblLinie.Show();
            btnAscunde.Show();
            btnSterge.Show();
            btnRefresh.Show();
            Activitate[] activitatiFisier = managerActivitatiFisier.GetActivitati(out int nrActivitatiFisier);

            foreach (var activitate in activitatiFisier)
            {
                string optiuniString;
                if (activitate.Optiuni != null)
                {
                    optiuniString = string.Join(",", activitate.Optiuni);
                }
                else
                {
                    optiuniString = " Fara ";
                }

                DateTime parsedDate;

                if (DateTime.TryParseExact(activitate.Data, "yyyy-MM-ddTHH:mmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedDate) ||
                    DateTime.TryParse(activitate.Data, out parsedDate) || DateTime.TryParseExact(activitate.Data, "MM-dd-yyytTHH:mmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedDate))
                {
                    string formattedDate = parsedDate.ToString("dd.MM.yyyy HH:mm");
                    dgvAfisare.Rows.Add(activitate.Nume, activitate.Tip, formattedDate, activitate.Descriere, activitate.Prioritate, optiuniString);
                }
                else
                {
                    dgvAfisare.Rows.Add(activitate.Nume, activitate.Tip, activitate.Data, activitate.Descriere, activitate.Prioritate, optiuniString);
                }
            }
        }
    }
}
