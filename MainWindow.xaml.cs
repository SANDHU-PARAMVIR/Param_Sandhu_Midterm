using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Param_Sandhu_Midterm
{
    public partial class MainWindow : Window
    {
        CovidDataReader covidData;

        public MainWindow()
        {
            InitializeComponent();
            covidData = new CovidDataReader("../../resources/data.csv");
            initialize();
        }

        async private void initialize()
        {
            try
            {
                await covidData.load();
                comboCountry.ItemsSource = covidData.Countries;
                datagridCovid.ItemsSource = covidData.Cases;
            }
            catch(Exception e)
            {
                Console.WriteLine("Data is loading...");
            }
        }

        private void btnDisplayAll_Click(object sender, RoutedEventArgs e)
        {
            datagridCovid.ItemsSource = covidData.Cases;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            datagridCovid.ItemsSource = covidData.fetchByCountry(comboCountry.SelectedItem.ToString());
        }

        private void datagridCovid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
