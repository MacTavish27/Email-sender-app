using Microsoft.Win32;
using System;
using System.Net.Mail;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string FROM_EMAIL = "abduhakimovfayzullo@gmail.com";
        private string FROM_PASS = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                fileName.Text = openFileDialog.FileName;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            //setup
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential(FROM_EMAIL, FROM_PASS);
            SmtpServer.EnableSsl = true;

            MailMessage mail = new MailMessage();
            //default from email
            mail.From = new MailAddress(FROM_EMAIL);

            //email recipient
            string addresses = toEmail.Text;
            foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Contains("@") == false || address.Contains(".com") == false)
                {
                    MessageBox.Show("Please enter actual email address");
                    return;
                }
                mail.To.Add(address);
            }

            //email subject
            mail.Subject = subjectContent.Text;

            //email attachments
            if (string.IsNullOrEmpty(fileName.Text) == false)
            {
                mail.Attachments.Add(new Attachment(fileName.Text));
            }

            //email body
            mail.Body = emailContent.Text;

            //send email
            MessageBox.Show("Jo'natilmoqda!...\nIltimos, OK tugmachani bosib kuting.");
            SmtpServer.Send(mail);
            MessageBox.Show("Muvaffaqiyatli jo'natildi!");

            //refresh text boxes
            toEmail.Text = "";
            subjectContent.Text = "";
            fileName.Text = "";
            emailContent.Text = "";
        }
    }
}
