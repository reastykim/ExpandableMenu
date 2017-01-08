using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reasty.Controls
{
    public class ExpandableMenu : ListBox
    {
        static ExpandableMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandableMenu), new FrameworkPropertyMetadata(typeof(ExpandableMenu)));            
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }


        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            try
            {
                var actualHeight = ActualHeight;
                if (ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                {
                    var itemElements = Items.OfType<object>();
                    var firstOfvalidListBoxItems = (from item in itemElements
                                                    let l = ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem
                                                    where l.IsSelected == false && l.ActualHeight != 0 // 선택되면 Fill 되므로 선택되지 않은 ListBoxItem
                                                    select l).FirstOrDefault();

                    if (firstOfvalidListBoxItems == null)
                        return;

                    var collapsedHeight = firstOfvalidListBoxItems.ActualHeight * Items.Count;
                    foreach (var listBoxItem in itemElements)
                    {
                        var container = ItemContainerGenerator.ContainerFromItem(listBoxItem) as ListBoxItem;
                        Expander listBoxItemContainerExpander = VisualTreeHelper.GetChild(container, 0) as Expander;
                        if (listBoxItemContainerExpander == null)
                            break;

                        var element = listBoxItemContainerExpander.Content as FrameworkElement;
                        if (element == null)
                            break;

                        var elementHeight = (actualHeight - collapsedHeight);
                        if (elementHeight >= 0)
                        {
                            element.Height = elementHeight;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
