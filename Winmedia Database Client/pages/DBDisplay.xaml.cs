﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour DBDisplay.xaml
    /// </summary>
    public partial class DBDisplay : Page
    {
        private Boolean ArtistAsc = false;
        private Boolean TitleAsc = false;
        private MainWindow parent;
        private Boolean isDraging = false;

        public DBDisplay(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();

            new Task(() => {
                DBHelper.connect();
                List<Music> MusList = DBHelper.GetMusics(-1);
                foreach(var music in MusList)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        this.MusicList.Items.Add(music);
                    });
                }
                DBHelper.disconnect();

                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    this.MusicList.SelectedIndex = 0;
                    PlayerWindow.LoadFile((Music)this.MusicList.SelectedItem);
                });

            }).Start();
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDraging = false;
            var item = ((Music)(sender as ListViewItem).Content);
            if (item != null)
            {
                PlayerWindow.LoadFile(item);
                
            }
        }        

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((Music)(sender as ListViewItem).Content);
            if (item != null)
            {
                isDraging = true;
                DragDropHelper.DragSource = ((ListViewItem)sender);
            }
        }

        private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDraging)
            {
                DragDrop.DoDragDrop(DragDropHelper.DragSource, DragDropHelper.DragSource.Content, DragDropEffects.Move);
                isDraging = false;
            }
        }

        private void MusicList_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            ICollectionView view = CollectionViewSource.GetDefaultView(this.MusicList.Items);
            view.SortDescriptions.Clear();
            switch (headerClicked.Content)
            {
                case "Artist":
                    view.SortDescriptions.Add(new SortDescription("Artist", ArtistAsc? ListSortDirection.Descending: ListSortDirection.Ascending));
                    ArtistAsc = !ArtistAsc;
                    break;

                case "Title":
                    view.SortDescriptions.Add(new SortDescription("Title", TitleAsc ? ListSortDirection.Descending : ListSortDirection.Ascending));
                    TitleAsc = !TitleAsc;
                    break;
            }

            view.Refresh();
            
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchMusic(-1);
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchMusic(-1);
            }
        }

        public void SearchMusic(int cat)
        {
            this.MusicList.Items.Clear();
            new Task(() =>
            {
                DBHelper.connect();

                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    List<Music> MusList;

                    if(cat == -1)
                    {
                        MusList = DBHelper.GetMusics(this.SearchField.Text);
                    }
                    else
                    {
                        MusList = DBHelper.GetMusics(cat);
                    }
                    

                    foreach (var music in MusList)
                    {
                        this.MusicList.Items.Add(music);
                    }

                    DBHelper.disconnect();
                    this.MusicList.SelectedIndex = 0;
                    if (this.MusicList.SelectedItem != null)
                    {
                        PlayerWindow.LoadFile((Music)this.MusicList.SelectedItem);
                    }
                });
            }).Start();
        }

        private void MusicList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                parent.StartImport(files);
            }
        }

        
    }
}
