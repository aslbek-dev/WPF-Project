using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Windows;  
using System.Windows.Controls;  
using System.Windows.Data;  
using System.Windows.Documents;  
using System.Windows.Input;  
using System.Windows.Media;  
using System.Windows.Media.Imaging;  
using System.Windows.Navigation;  
using System.Windows.Shapes;  
using System.Data;  
using System.Data.SqlClient;  
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Wpf
{  
    /// <summary>  
    /// Interaction logic for MainWindow.xaml  
    /// </summary>   
    public partial class LoginWindow : Window  
    {  
        private MySqlConnection connection;
        public LoginWindow()  
        {  
            InitializeComponent();
            string connectionString = "server=localhost;user=root;database=your_database;password=your_password";
            connection = new MySqlConnection(connectionString); 
        }  
        RegisterWindow registration = new RegisterWindow();  
        Welcome welcome = new Welcome();  
        private void button1_Click(object sender, RoutedEventArgs e)
         {  
            if (textBoxEmail.Text.Length == 0)  
            {  
                errormessage.Text = "Enter an email.";  
                textBoxEmail.Focus();  
            }  
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))  
            {  
                errormessage.Text = "Enter a valid email.";  
                textBoxEmail.Select(0, textBoxEmail.Text.Length);  
                textBoxEmail.Focus();  
            }  
            else  
            {  
                string email = textBoxEmail.Text;  
                string password = passwordBox1.Password;  
                MySqlConnection con = new MySqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");  
                con.Open();  
                MySqlCommand cmd = new MySqlCommand("Select * from Registration where Email='" + email + "'  and password='" + password + "'", con);  
                cmd.CommandType = CommandType.Text;  
                MySqlDataAdapter adapter = new MySqlDataAdapter();  
                adapter.SelectCommand = cmd;
                 DataSet dataSet = new DataSet();  
                adapter.Fill(dataSet);  
                if (dataSet.Tables[0].Rows.Count > 0)  
                {  
                    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();  
                    welcome.TextBlockName.Text = username;//Sending value from one form to another form.  
                    welcome.Show();  
                    Close();  
                }  
                else  
                {  
                    errormessage.Text = "Sorry! Please enter existing emailid/password.";  
                }  
                con.Close();  
            }  
        }  
        private void buttonRegister_Click(object sender, RoutedEventArgs e)  
        {  
            registration.Show();  
            Close();  
        }  
    }  
}  
                  