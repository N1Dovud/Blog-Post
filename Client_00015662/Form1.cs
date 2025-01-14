using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Net.Http;
using System.Globalization;





namespace CWProject2_00015662
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void StartServer()
        {
            try
            {
                // Create and start the server
                TcpListener listener = new TcpListener(IPAddress.Any, 50000);
                listener.Start();

                MessageBox.Show("Server is listening...");

                while (true)
                {
                    // Accept incoming connection from client
                    TcpClient client = listener.AcceptTcpClient();
                    Task.Run(() => HandleClientAsync(client));  // Handle each client asynchronously
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Handle each client connection asynchronously
        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                MessageBox.Show("We found the client data!");
                NetworkStream stream = client.GetStream();
                List<byte> allData = new List<byte>();  // To collect all the incoming bytes
                byte[] buffer = new byte[256];  // Temporary buffer to hold chunks of data

                // Read client data in a loop until there's no more data to read
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    // Append the data read from the buffer to the allData list
                    allData.AddRange(buffer.Take(bytesRead));
                }

                // Once all data is read, convert the List<byte> to a byte array
                byte[] completeData = allData.ToArray();

                // Process the complete data (pass the client and the complete data)
                ProcessClientData(client, completeData);


                // Close connection and clean up
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client: {ex.Message}");
            }
        }

        // Process the client data and save it to the database
        private void ProcessClientData(TcpClient client, byte[] data)
        {
            try
            {
                // Step 1: Extract data based on the protocol
                MessageBox.Show("We are reading the client data!");
                // Extract employee number (first 10 bytes)
                string employeeNumber = Encoding.UTF8.GetString(data.Take(10).ToArray()).Trim();  // First 10 bytes for EmpNum

                // Extract the date as ticks (8 bytes)
                long dateTicks = BitConverter.ToInt64(data.Skip(10).Take(8).ToArray(), 0);  // Ticks are stored as 8 bytes
                DateTime date = new DateTime(dateTicks);  // Convert ticks to DateTime

                // Extract IN time as ticks (8 bytes)
                long inTimeTicks = BitConverter.ToInt64(data.Skip(18).Take(8).ToArray(), 0);  // IN time is 8 bytes (ticks)
                DateTime inTime = new DateTime(inTimeTicks);  // Convert IN ticks to DateTime

                // Extract OUT time as ticks (8 bytes)
                long outTimeTicks = BitConverter.ToInt64(data.Skip(26).Take(8).ToArray(), 0); // OUT time is 8 bytes (ticks)
                DateTime outTime = new DateTime(outTimeTicks);  // Convert OUT ticks to DateTime

                // Extract daily log length (2 bytes)
                short dailyLogLength = BitConverter.ToInt16(data.Skip(34).Take(2).ToArray(), 0);  // Assuming Daily Log Length is 2 bytes

                // Extract daily log content (variable length based on previous field)
                string dailyLogContent = Encoding.UTF8.GetString(data.Skip(36).Take(dailyLogLength).ToArray()).Trim(); // Daily Log

                // Step 2: Save the attendance to the database
                SaveAttendanceToDatabase(employeeNumber,  inTime, outTime, dailyLogContent, date);

                // Step 3: Get the current week range (simplified)
                DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);  // Adjust to the start of the week (e.g., Sunday)
                DateTime endOfWeek = startOfWeek.AddDays(7);  // End of the week (next Sunday)

                // Step 4: Calculate the total hours worked this week
                int totalHours = CalculateWeeklyHours(employeeNumber, startOfWeek, endOfWeek);

                SendServerResponse(client, totalHours);
            }
            catch (Exception ex)
            {
                // If any error occurs, send error message to the client
                MessageBox.Show( $"Error processing data: {ex.Message}");
            }
        }

        // Save data to SQLite database
        private void SaveAttendanceToDatabase(string employeeNumber, DateTime inTime, DateTime outTime, string dailyLog, DateTime date)
        {
            using (SQLiteConnection connection = new SQLiteConnection())
            {
                try
                {
                    // Convert DateTime to Ticks
                    long inTimeTicks = inTime.Ticks;
                    long outTimeTicks = outTime.Ticks;
                    long dateTicks = date.Ticks;

                    string query = "INSERT INTO Attendance (EmpNum, InTime, OutTime, DailyLog, Date) " +
                                   "VALUES (@EmployeeNumber, @InTime, @OutTime, @DailyLog, @Date)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeNumber", employeeNumber);
                        command.Parameters.AddWithValue("@InTime", inTimeTicks);
                        command.Parameters.AddWithValue("@OutTime", outTimeTicks);
                        command.Parameters.AddWithValue("@DailyLog", dailyLog);
                        command.Parameters.AddWithValue("@Date", dateTicks);

                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully saved the new log!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving to database: {ex.Message}");
                }
            }
        }

        // Send server response back to the client
        private void SendServerResponse(TcpClient client, int hours)
        {
            try
            {
                // Convert the integer 'hours' to a byte array
                byte[] responseBytes = BitConverter.GetBytes(hours);

                // Get the network stream to send data
                NetworkStream stream = client.GetStream();

                // Send the byte array response to the client
                stream.Write(responseBytes, 0, responseBytes.Length);

                // Close the stream after writing the data
                stream.Close();
            }
            catch (Exception ex)
            {
                // Show error message if there was an issue
                MessageBox.Show($"Error sending response: {ex.Message}");
            }
        }
        private int CalculateWeeklyHours(string employeeNumber, DateTime startDate, DateTime endDate)
        {
            double totalminutes = 0;

            using (SQLiteConnection connection = new SQLiteConnection())
            {
                try
                {
                    string query = "SELECT InTime, OutTime FROM Attendance WHERE EmpNum = @EmployeeNumber AND Date BETWEEN @StartDate AND @EndDate";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeNumber", employeeNumber);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        connection.Open();

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime inTime = new DateTime(reader.GetInt64(0));  // Convert Ticks to DateTime
                                DateTime outTime = new DateTime(reader.GetInt64(1));

                                // Calculate the hours worked for this entry
                                totalminutes += (outTime - inTime).TotalMinutes;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error calculating weekly hours: {ex.Message}");
                }
            }
            int hours = Convert.ToInt32(Math.Floor(totalminutes / 60));
            MessageBox.Show("Successfully calculated the hours!");
            return hours;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => StartServer());
            MessageBox.Show("Server is running");
        }
    }
}
