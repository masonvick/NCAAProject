using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCAAProject
{
    public partial class TourneyForm : Form
    {
        public TourneyForm()
        {
            InitializeComponent();
            string yearQuery = "SELECT \r\n    [Year]\r\nFROM \r\n    NCAA.Tournament\r\nORDER BY \r\n    [Year] ASC";
            AddTourneyYears(yearQuery);


        }


        public void button1_Click(object sender, EventArgs e)
        {

            ShowTourneyInfo();
        }


        public void AddTourneyYears(string query)
        {
            try
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.UserID = "luke3802";
                cb.Password = "55Warriors2020";
                cb.DataSource = "mssql.cs.ksu.edu";
                cb["Database"] = "luke3802";
                cb.TrustServerCertificate = true;
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlConnection connection = new SqlConnection(cb.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string year = reader.GetInt32(0).ToString();
                            comboBox1.Items.Add(year);
                            comboBox2.Items.Add(year);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void ShowTourneyInfo()
        {
            int firstYear = Int32.Parse(comboBox1.SelectedIndex.ToString());
            int secondYear = Int32.Parse(comboBox2.SelectedIndex.ToString());

            string tourneyQuery = String.Format("SELECT \r\n    t.[Year] AS TournamentYear,\r\n    COUNT(CASE WHEN t1.Seed > t2.Seed THEN 1 ELSE NULL END) AS TotalUpsets,\r\n    SUM(g.WinningScore + g.LosingScore) AS TotalPoints\r\nFROM \r\n    NCAA.Tournament t\r\n    JOIN NCAA.Team t1 ON t1.[Year] = t.[Year]\r\n    JOIN NCAA.Team t2 ON t2.[Year] = t.[Year]\r\n    JOIN NCAA.Game g ON g.TournamentID = t.TournamentID\r\nWHERE \r\n    t.[Year] BETWEEN {0} AND {1}\r\n    AND t1.TeamID = g.WinningTeamID\r\n    AND t2.TeamID = g.LosingTeamID\r\nGROUP BY \r\n    t.[Year]\r\nORDER BY \r\n    TotalUpsets ASC", firstYear, secondYear);

            try
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.UserID = "luke3802";
                cb.Password = "55Warriors2020";
                cb.DataSource = "mssql.cs.ksu.edu";
                cb["Database"] = "luke3802";
                cb.TrustServerCertificate = true;
                SqlDataAdapter adapter = new SqlDataAdapter();
                using (SqlConnection connection = new SqlConnection(cb.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(tourneyQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string output = string.Format("Tournament Year: {0}\nTotal Upsets: {1}\nTotal Points: {2}\n\n",
                                reader["TournamentYear"], reader["TotalUpsets"], reader["TotalPoints"]);

                            richTextBox1.AppendText(output);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
