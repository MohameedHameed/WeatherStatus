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
    
    public partial class CountrieForm : Form
    {
        Country country;
        HttpHelper httpHelper;
        string countryname;
        string cityname;
        string fulname;
        List<string> FullNames;
        Main main;
        public CountrieForm(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        private async void CountrieForm_Load(object sender, EventArgs e)
        {
            httpHelper = new HttpHelper();
            FullNames = new List<string>();
            Cursor = Cursors.WaitCursor;
            await Task.Run(() => { loaddata(); });
            await Task.Run(() => { getcountrynameandcitytocombox(); });
            comboBox1.DataSource = FullNames;
            Cursor = Cursors.Default;
        }
        private void loaddata()
        {
            string jsondata = httpHelper.getjson("https://countriesnow.space/api/v0.1/countries");
            country=Newtonsoft.Json.JsonConvert.DeserializeObject<Country>(jsondata);
            
           
        }

        private void getcountrynameandcitytocombox() {
        
            for(int i = 0; i < country.data.Length; i++)
            {
                 countryname = country.data[i].country;
                for(int j = 0; j < country.data[i].cities.Length;j++)
                {
                  cityname = country.data[i].cities[j];
                    fulname = countryname + ',' + cityname;
                    FullNames.Add(fulname);

                }
            }

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FullName=comboBox1.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Saved Sucess");
            main.Main_Load(sender,e);
            this.Close();
        }
    }
}
