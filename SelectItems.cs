using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SelectObjects
{
    public partial class SelectItems<T> : Form where T : class
    {
        public ICollection<T> Result { get; private set; }

        public SelectItems(string text, string caption, IEnumerable<T> items, Func<T, string[]> itemDisplayColumns, bool multiSelect)
        {
            if (!items.Any())
                throw new ArgumentException("The items collection cannot be empty when passed to SelectItems", "items");

            InitializeComponent();

            //Assign properties to the form and form objects
            lblCaption.Text = text;
            Text = caption;
            lstItems.MultiSelect = multiSelect;

            //Add items to the List Box
            lstItems.Items.AddRange(
                items.Select(i => new ListViewItem(itemDisplayColumns(i)) { Tag = i })
                    .ToArray()
                );

            //Add displays to each column for each item
            lstItems.Columns.AddRange(
                itemDisplayColumns(items.First())
                    .Select(s => new ColumnHeader() { Text = "", Width = -2 }) //-2 Auto Resizes Columns on Headers and Content
                    .ToArray()
            );
        }

        private void SelectItems_Load(object sender, EventArgs e)
        {
            //Resize form based on list content.
            var columnHeaders = lstItems.Columns.Cast<ColumnHeader>();
            Width = Math.Max(400, columnHeaders.Sum(ch => ch.Width) + 50) + 2;
            
            lstItems.Height = lstItems.Height + (Math.Min(19, lstItems.Items.Count - 1) * 25) - 2;
            Height = lblCaption.Height + cmdOK.Height + lstItems.Height + 90;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
                Result = lstItems.SelectedItems
                            .Cast<ListViewItem>()
                            .Select(item => (T)item.Tag)
                            .ToList();
            else
                Result = new List<T>();
        }
    }
}
