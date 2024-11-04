using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherStatus
{
    public partial class Main : Form
    {
        string Fullname;
        HttpHelper httpHelper;
        Weather weather;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            CountrieForm countrieForm = new CountrieForm(this);
            countrieForm.ShowDialog();
        }

        public async void Main_Load(object sender, EventArgs e)
        {
            Fullname = Properties.Settings.Default.FullName;

            if (Fullname != string.Empty)
            {
                MessageBox.Show(Fullname);
                Cursor = Cursors.WaitCursor;
                httpHelper = new HttpHelper();
              await Task.Run(() => { loaddata(); });
                CNameLbl.Text = weather.location.country;
                CiNameLbl.Text = weather.location.name;
                TempLbl.Text = weather.current.temperature.ToString();
                WindLbl.Text = weather.current.wind_speed.ToString();
                HumLbl.Text = weather.current.humidity.ToString();
                string icon = weather.current.weather_icons[0];
                pictureBox1.ImageLocation = icon;
                pictureBox2.ImageLocation = icon;
                Cursor = Cursors.Default;



            }
        }
        private void loaddata()
        {
            string url = "http://api.weatherstack.com/current?access_key=97cb51c843c5c2b82e228a3c1fda4a0a&query="+Fullname;
            string jsondata = httpHelper.getjson(url);
            weather = Newtonsoft.Json.JsonConvert.DeserializeObject<Weather>(jsondata);


        }
    }
}
