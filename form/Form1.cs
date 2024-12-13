using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form
{
    public partial class Form1 : Form
    {
        private Label counterLabel;
        private Timer timer;
        private int remainingTime = 60 * 10; // Set the initial remaining time to x seconds
        private bool counterDown = false; // Variable to track if the counter is down

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Set the form to maximize
            this.FormBorderStyle = FormBorderStyle.None; // Remove the border
            this.BackColor = Color.Black; // Set the background color to black

            Label label = new Label(); // Create a new label
            label.Text = "GO REST!"; // Set the label text
            label.Font = new Font("Arial", 48, FontStyle.Bold); // Set the font and size
            label.ForeColor = Color.White; // Set the text color to white
            label.AutoSize = true; // Auto size the label based on the text

            // Center the label on the form
            label.Location = new Point((this.ClientSize.Width - label.Width) / 2, (this.ClientSize.Height - label.Height) / 2);

            this.Controls.Add(label); // Add the label to the form

            counterLabel = new Label(); // Create a new label for the counter
            counterLabel.Font = new Font("Arial", 24, FontStyle.Regular); // Set the font and size
            counterLabel.ForeColor = Color.White; // Set the text color to white
            counterLabel.AutoSize = true; // Auto size the label based on the text

            // Position the counter label below the main label
            counterLabel.Location = new Point((this.ClientSize.Width - counterLabel.Width) / 2, label.Bottom + 20);

            this.Controls.Add(counterLabel); // Add the counter label to the form


            timer = new Timer();
            timer.Interval = 1000; // Set the timer interval to 1 second
            timer.Tick += Timer_Tick; // Attach the event handler for the timer tick

            timer.Start(); // Start the timer

            this.TopMost = true; // Set the form to always be on top
            this.ControlBox = false; // Disable the control box (close, minimize, maximize buttons)
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--; // Decrease the remaining time by 1 second

            if (remainingTime >= 0)
            {
                counterLabel.Text = $"Remaining Time: {remainingTime} seconds"; // Update the counter label text
            }
            else
            {
                counterLabel.Text = "Time's up!"; // Display "Time's up!" when the countdown is finished
                timer.Stop(); // Stop the timer

                counterDown = true; // Set the counterDown variable to true

                Button closeButton = new Button(); // Create a new button
                closeButton.Text = "Close"; // Set the button text
                closeButton.Font = new Font("Arial", 24, FontStyle.Regular); // Set the font and size
                closeButton.ForeColor = Color.White; // Set the text color to white
                closeButton.AutoSize = true; // Auto size the button based on the text

                // Position the button below the counter label
                closeButton.Location = new Point((this.ClientSize.Width - closeButton.Width) / 2, counterLabel.Bottom + 20);

                closeButton.Click += CloseButton_Click; // Attach the event handler for the button click

                this.Controls.Add(closeButton); // Add the button to the form

                Label autoCloseLabel = new Label(); // Create a new label for the auto close message
                autoCloseLabel.Text = "Close automatically in 10 seconds"; // Set the label text
                autoCloseLabel.Font = new Font("Arial", 12, FontStyle.Regular); // Set the font and size
                autoCloseLabel.ForeColor = Color.White; // Set the text color to white
                autoCloseLabel.AutoSize = true; // Auto size the label based on the text

                // Position the label below the close button
                autoCloseLabel.Location = new Point((this.ClientSize.Width - autoCloseLabel.Width) / 2, closeButton.Bottom + 10);

                this.Controls.Add(autoCloseLabel); // Add the label to the form

                // Automatically close the form after 10 seconds
                Task.Delay(10000).ContinueWith(_ =>
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Close();
                        }));
                    }
                });
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form when the button is clicked
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !counterDown)
            {
                e.Cancel = true; // Cancel the closing operation if the counter is not down
            }

            base.OnFormClosing(e);
        }
    }
}
