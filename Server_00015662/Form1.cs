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




namespace CWProject2_00015662
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string GetLocalIP()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var network in networkInterfaces.Where(t => t.OperationalStatus == OperationalStatus.Up))
            {
                var properties = network.GetIPProperties();

                if (properties.GatewayAddresses.Count == 0) // should have a gateway address
                    continue;

                var preferredIp = properties.UnicastAddresses.FirstOrDefault(t =>
                    t.Address.AddressFamily == AddressFamily.InterNetwork && // IPv4 address
                    !IPAddress.IsLoopback(t.Address) && // not a loopback address
                    t.IsDnsEligible // not from 169.254.0.0 to 169.254.255.255 range used for not-yet assigned addresses
                    );
                if (preferredIp != null)
                    MessageBox.Show("IP is" + preferredIp.Address.ToString());
                    return preferredIp.Address.ToString();
            }

            // if we are here - nothing was found
            return "?";
        }

        private bool ValidateInputs()
        {
           if (!DateTime.TryParse(DTInTime.Text, out DateTime inTime) ||
                !DateTime.TryParse(DTOutTime.Text, out DateTime outTime) ||
                inTime >= outTime)
            {
                MessageBox.Show("Invalid IN/OUT times.");
                return false;
            }

            if (string.IsNullOrEmpty(tbxDailyLog.Text))
            {
                MessageBox.Show("Daily Log cannot be empty.");
                return false;
            }
            MessageBox.Show("Inputs validated!");
            return true;
        }
        private byte[] FormatData()
        {
            // Employee Number (10 bytes, padded with spaces)
            string employeeNumber = tbxEmpNum.Text.PadRight(10);

            // Convert Date, IN Time, and OUT Time to Ticks
            long dateTicks = DTDate.Value.Date.Ticks; // Only date part
            long inTimeTicks = DTInTime.Value.Ticks;
            long outTimeTicks = DTOutTime.Value.Ticks;

            // Daily Log (variable length)
            byte[] dailyLogBytes = System.Text.Encoding.UTF8.GetBytes(tbxDailyLog.Text);
            byte[] dailyLogLength = BitConverter.GetBytes((short)dailyLogBytes.Length); // 2 bytes

            // Combine all parts into a single byte array
            using (var ms = new System.IO.MemoryStream())
            {
                // Write Employee Number (10 bytes)
                ms.Write(System.Text.Encoding.UTF8.GetBytes(employeeNumber), 0, 10);

                // Write Date, IN Time, and OUT Time as 8-byte integers (Ticks)
                ms.Write(BitConverter.GetBytes(dateTicks), 0, 8);
                ms.Write(BitConverter.GetBytes(inTimeTicks), 0, 8);
                ms.Write(BitConverter.GetBytes(outTimeTicks), 0, 8);

                // Write Daily Log Length (2 bytes)
                ms.Write(dailyLogLength, 0, dailyLogLength.Length);

                // Write Daily Log Content (variable length)
                ms.Write(dailyLogBytes, 0, dailyLogBytes.Length);

                MessageBox.Show("Inputs formatted!");
                // Return the final byte array
                return ms.ToArray();
            }
    }

        private async void SendDataToServer(byte[] data)
        {
            try
            {
                using (TcpClient client = new TcpClient(GetLocalIP(), 50000)) // Adjust IP and port
                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                    MessageBox.Show("Data sent!!!");
                    await ReceiveServerResponse(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}");
            }
        }

        private async Task ReceiveServerResponse(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                // Create a buffer to read the response (4 bytes for an int)
                byte[] buffer = new byte[4];

                // Read the response bytes from the server
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    // Convert the received byte array back into an integer (hours)
                    int hoursWorked = BitConverter.ToInt32(buffer, 0);
                    MessageBox.Show("Data received!!!");
                    // Check if the hours worked is greater than 40
                    if (hoursWorked > 40)
                    {
                        MessageBox.Show($"Overtime detected! Hours worked: {hoursWorked} hours.");
                    }
                    else
                    {
                        MessageBox.Show($"No overtime");
                    }
                }
                else
                {
                    MessageBox.Show("No response from server.");
                }

                // Close the stream (optional, depending on your architecture)
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving server response: {ex.Message}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                byte[] bytes = FormatData();
                SendDataToServer(bytes);
            }
        }
    }
}
