using System.Data;
using Microsoft.Data.SqlClient;

namespace NCAAProject
{
    public partial class CoachForm : Form
    {
        public CoachForm()
        {
            InitializeComponent();

            //ADD COACHES TO DROP DOWN LISTS
            string coachListSql = "SELECT \r\n  CONCAT(FirstName, ' ', LastName) AS FullName\r\nFROM \r\n  NCAA.HeadCoach;";
            AddCoachesToDropDown(coachListSql);

            string compareSql =
                "SELECT\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name] AS CollegeName,\r\n  COUNT(CASE WHEN WinningCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalWins,\r\n  COUNT(CASE WHEN LosingCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalLosses,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 2 THEN 1 END) AS TotalChips,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 4 THEN 1 END) AS FinalFourAppearances" +
                "FROM\r\n  HeadCoach CoachA\r\n  INNER JOIN Team WinningTeam ON WinningTeam.CoachID = CoachA.CoachID\r\n  INNER JOIN College CollegeA ON CollegeA.CollegeID = WinningTeam.CollegeID\r\n  INNER JOIN Game ON Game.WinningTeamID = WinningTeam.TeamID\r\n  INNER JOIN HeadCoach WinningCoach ON WinningCoach.CoachID = WinningTeam.CoachID\r\n  INNER JOIN Team LosingTeam ON Game.LosingTeamID = LosingTeam.TeamID\r\n  INNER JOIN HeadCoach LosingCoach ON LosingCoach.CoachID = LosingTeam.CoachID" +
                "WHERE\r\n  CoachA.FirstName = 'Bill'\r\n  AND CoachA.LastName = 'Self'" +
                "GROUP BY\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name];";


            

            
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            DisplayCoachInfo(1);
            DisplayCoachInfo(2);
        }

        private void AddCoachesToDropDown(string query)
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
                            string fullName = reader.GetString(0);
                            dropDownList1.Items.Add(fullName);
                            dropDownList2.Items.Add(fullName);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DisplayCoachInfo(int index)
        {
            if(index == 1)
            {
                string coach1 = dropDownList1.SelectedItem.ToString();
                string[] name1 = coach1.Split(' ');

                string firstCoachQuery = String.Format(
                "SELECT\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name] AS CollegeName,\r\n  COUNT(CASE WHEN WinningCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalWins,\r\n  COUNT(CASE WHEN LosingCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalLosses,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 2 THEN 1 END) AS TotalChips,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 4 THEN 1 END) AS FinalFourAppearances \r\n" +
                "FROM\r\n  NCAA.HeadCoach CoachA\r\n  INNER JOIN NCAA.Team WinningTeam ON WinningTeam.CoachID = CoachA.CoachID\r\n  INNER JOIN NCAA.College CollegeA ON CollegeA.CollegeID = WinningTeam.CollegeID\r\n  INNER JOIN NCAA.Game ON Game.WinningTeamID = WinningTeam.TeamID\r\n  INNER JOIN NCAA.HeadCoach WinningCoach ON WinningCoach.CoachID = WinningTeam.CoachID\r\n  INNER JOIN NCAA.Team LosingTeam ON Game.LosingTeamID = LosingTeam.TeamID\r\n  INNER JOIN NCAA.HeadCoach LosingCoach ON LosingCoach.CoachID = LosingTeam.CoachID\r\n" +
                "WHERE\r\n  CoachA.FirstName = '{0}'\r\n  AND CoachA.LastName = '{1}'\r\n" +
                "GROUP BY\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name];", name1[0], name1[1]);

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
                        using (SqlCommand command = new SqlCommand(firstCoachQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                string fullName = reader.GetString(0) + " " + reader.GetString(1);
                                string collegeName = reader.GetString(2);
                                int totalWins = reader.GetInt32(3);
                                int totalLosses = reader.GetInt32(4);
                                int totalChips = reader.GetInt32(5);
                                int finalFourAppearances = reader.GetInt32(6);

                                string output = string.Format("Coach: {0}\nCollege: {1}\nTotal Wins: {2}\nTotal Losses: {3}\nTotal Chips: {4}\nFinal Four Appearances: {5}",
                                    fullName, collegeName, totalWins, totalLosses, totalChips, finalFourAppearances);

                                coachRichTextBox1.Text = output;
                            }
                        }
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if(index == 2)
            {
                string coach2 = dropDownList2.SelectedItem.ToString();

                string[] name2 = coach2.Split(' ');



                string secondCoachQuery = String.Format(
                    "SELECT\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name] AS CollegeName,\r\n  COUNT(CASE WHEN WinningCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalWins,\r\n  COUNT(CASE WHEN LosingCoach.CoachID = CoachA.CoachID THEN 1 END) AS TotalLosses,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 2 THEN 1 END) AS TotalChips,\r\n  COUNT(CASE WHEN WinningTeam.CoachID = CoachA.CoachID AND Game.[Round] = 4 THEN 1 END) AS FinalFourAppearances \r\n" +
                    "FROM\r\n  NCAA.HeadCoach CoachA\r\n  INNER JOIN NCAA.Team WinningTeam ON WinningTeam.CoachID = CoachA.CoachID\r\n  INNER JOIN NCAA.College CollegeA ON CollegeA.CollegeID = WinningTeam.CollegeID\r\n  INNER JOIN NCAA.Game ON Game.WinningTeamID = WinningTeam.TeamID\r\n  INNER JOIN NCAA.HeadCoach WinningCoach ON WinningCoach.CoachID = WinningTeam.CoachID\r\n  INNER JOIN NCAA.Team LosingTeam ON Game.LosingTeamID = LosingTeam.TeamID\r\n  INNER JOIN NCAA.HeadCoach LosingCoach ON LosingCoach.CoachID = LosingTeam.CoachID\r\n" +
                    "WHERE\r\n  CoachA.FirstName = '{0}'\r\n  AND CoachA.LastName = '{1}'\r\n" +
                    "GROUP BY\r\n  CoachA.FirstName,\r\n  CoachA.LastName,\r\n  CollegeA.[Name];", name2[0], name2[1]);


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
                        using (SqlCommand command = new SqlCommand(secondCoachQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                string fullName = reader.GetString(0) + " " + reader.GetString(1);
                                string collegeName = reader.GetString(2);
                                int totalWins = reader.GetInt32(3);
                                int totalLosses = reader.GetInt32(4);
                                int totalChips = reader.GetInt32(5);
                                int finalFourAppearances = reader.GetInt32(6);

                                string output = string.Format("Coach: {0}\nCollege: {1}\nTotal Wins: {2}\nTotal Losses: {3}\nTotal Chips: {4}\nFinal Four Appearances: {5}",
                                    fullName, collegeName, totalWins, totalLosses, totalChips, finalFourAppearances);

                                coachRichTextBox2.Text = output;
                            }
                        }
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("HOW?????");
            }

        }

    }
}