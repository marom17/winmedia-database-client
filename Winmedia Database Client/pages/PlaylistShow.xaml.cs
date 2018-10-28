using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
    /// Interaction logic for PlaylistShow.xaml
    /// </summary>
    public partial class PlaylistShow : Page
    {
        private Boolean isDraging = false;
        private Boolean dropOnElement = false;
        private Boolean isUpdating = false;

        public PlaylistShow()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)this.PlList.Items).CollectionChanged += PlList_CollectionChanged;
        }

        public void ShowPlaylist(List<PlaylistElement> playlist)
        {
            this.PlList.Items.Clear();

            foreach (var item in playlist)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Content = item;
                this.PlList.Items.Add(viewItem);
            }
        }

        private void PlList_Drop(object sender, DragEventArgs e)
        {
            Music music = (Music)e.Data.GetData(typeof(Music));
            PlaylistElement element = (PlaylistElement)e.Data.GetData(typeof(PlaylistElement));
            this.ListChange(music, element, sender);


        }

        private void ListViewItem_Drop(object sender, DragEventArgs e)
        {
            dropOnElement = true;
            Music music = (Music)e.Data.GetData(typeof(Music));
            PlaylistElement element = (PlaylistElement)e.Data.GetData(typeof(PlaylistElement));

            this.ListChange(music, element, sender);
        }

        private void ListChange(Music music, PlaylistElement element, object sender)
        {
            if (music != null)
            {
                if (!dropOnElement || sender.GetType() == typeof(ListViewItem))
                {
                    if(sender.GetType() == typeof(ListViewItem))
                    {
                        int index = this.PlList.Items.IndexOf(((ListViewItem)sender));
                        ListViewItem viewItem = new ListViewItem();
                        viewItem.Content = new PlaylistElement(music);
                        this.PlList.Items.Insert(index, viewItem);
                    }
                    else
                    {
                        ListViewItem viewItem = new ListViewItem();
                        viewItem.Content = new PlaylistElement(music);
                        this.PlList.Items.Add(viewItem);
                    }                    
                }
                else
                {
                    dropOnElement = false;
                }

            }
            if (element != null)
            {
                if (!dropOnElement || sender.GetType() == typeof(ListViewItem))
                {
                    if (sender.GetType() == typeof(ListViewItem))
                    {
                        int indexSource = this.PlList.Items.IndexOf(DragDropHelper.DragSource);
                        int indexDest = this.PlList.Items.IndexOf(((ListViewItem)sender));
                        this.PlList.Items.RemoveAt(indexSource);
                        ListViewItem viewItem = new ListViewItem();
                        viewItem.Content = element;
                        this.PlList.Items.Insert(indexDest, viewItem);
                    }
                    else
                    {
                        int indexSource = this.PlList.Items.IndexOf(DragDropHelper.DragSource);
                        this.PlList.Items.RemoveAt(indexSource);
                        ListViewItem viewItem = new ListViewItem();
                        viewItem.Content = element;
                        this.PlList.Items.Add(viewItem);
                    }
                }
                else
                {
                    dropOnElement = false;
                }
            }
        }

        private void PlList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!isUpdating)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    this.calculateTime();
                }
                if (e.Action == NotifyCollectionChangedAction.Move)
                {
                    this.calculateTime();
                }
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    this.calculateTime();
                }
            }
        }

        private void calculateTime()
        {
            TimeSpan nextStart = new TimeSpan(0, 0, 0);
            ListView tmp = new ListView();
            isUpdating = true;
            foreach(ListViewItem item in this.PlList.Items)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Content = item.Content;
                tmp.Items.Add(viewItem);
            }
            this.PlList.Items.Clear();

            foreach (ListViewItem item in tmp.Items)
            {
                ((PlaylistElement)item.Content).Start = nextStart;

                nextStart = new TimeSpan(0, 0, (int)nextStart.TotalSeconds + (int)((PlaylistElement)item.Content).PrettyTime.TotalSeconds);

                ListViewItem viewItem = new ListViewItem();
                viewItem.Content = item.Content;
                this.PlList.Items.Add(viewItem);
                
            }
            tmp.Items.Clear();
            isUpdating = false;
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDraging = false;
            var item = ((PlaylistElement)(sender as ListViewItem).Content);
            if (item != null)
            {
                PlayerWindow.LoadFile(item.Music);

            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((PlaylistElement)(sender as ListViewItem).Content);
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
    }
}
