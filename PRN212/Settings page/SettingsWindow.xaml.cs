using PRN212.Help_page;
using PRN212.Authentication_page;
using PRN212.Calendar_Page;
using PRN212.Home;
using PRN212.Customize_Page;
using PRN212.SaveData;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using PRN212.Tasks_page;

namespace PRN212.SettingsPage
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // Display the current User's name and email
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text = CurrentUser.User.Email;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Home mainWindow = new Home();
            mainWindow.Show();
            this.Close();
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarPage mainWindow = new CalendarPage();
            mainWindow.Show();
            this.Close();
        }

        private void TaskWindow_Click(object sender, RoutedEventArgs e)
        {
            TasksPage mainWindow = new TasksPage();
            mainWindow.Show();
            this.Close();
        }

        private void Customize_Click(object sender, RoutedEventArgs e)
        {
            CustomizePage mainWindow = new CustomizePage();
            mainWindow.Show();
            this.Close();
        }

        private void SettingWindow_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage mainWindow = new SettingsPage();
            mainWindow.Show();
            this.Close();
        }

        private void HelpWindow_Click(object sender, RoutedEventArgs e)
        {
            HelpPage mainWindow = new HelpPage();
            mainWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Check if the text boxes are empty
            if (string.IsNullOrWhiteSpace(tb_name.Text) ||
                string.IsNullOrWhiteSpace(tb_email.Text) ||
                string.IsNullOrWhiteSpace(tb_newPassword.Password) ||
                string.IsNullOrWhiteSpace(tb_password.Password))
            {
                MessageBox.Show("Please fill in all fields before saving changes.");
                return;
            }

            // Find the current User in the list
            UserData user = users.Where(u => u.Username == CurrentUser.User.Username).FirstOrDefault();

            // If the user was found, verify the old password
            if (user != null)
            {
                if (user.Password != tb_password.Password)
                {
                    MessageBox.Show("The old password is incorrect.");
                    return;
                }

                user.Username = tb_name.Text;
                user.Email = tb_email.Text;
                user.Password = tb_newPassword.Password;

                // Update properties of the UserData class
                CurrentUser.User.Username = tb_name.Text;
                CurrentUser.User.Email = tb_email.Text;
            }

            // Save the JSON file
            json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);

            SettingsPage mainWindow = new SettingsPage();
            mainWindow.Show();
            this.Close();
        }

        private void EndSession_Click(object sender, RoutedEventArgs e)
        {
            LoginPage mainWindow = new LoginPage();
            mainWindow.Show();
            this.Close();
        }

        private void tb_name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                userImage.Source = new BitmapImage(fileUri);

                // Maintain image dimensions
                userImage.Width = 95;
                userImage.Height = 101;
            }
        }
    }
}
