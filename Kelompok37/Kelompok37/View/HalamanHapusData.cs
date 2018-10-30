using Kelompok37.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok37.View
{
    public class HalamanHapusData : ContentPage
    {
        private ListView _listView;
        private Button _hapus;

        DataMahasiswa dataMahasiswa = new DataMahasiswa();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        public HalamanHapusData()
        {
            this.Title = "Hapus Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _hapus = new Button();
            _hapus.Text = "Hapus";
            _hapus.Clicked += _hapus_Clicked;
            stackLayout.Children.Add(_hapus);

            Content = stackLayout;
        }

        private async void _hapus_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<DataMahasiswa>().Delete(x => x.Nama == dataMahasiswa.Nama);
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            dataMahasiswa = (DataMahasiswa)e.SelectedItem;
        }
    }
}