using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tomatina.ExplorerViews.Data;

namespace Tomatina.ExplorerViews.Controls
{
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_PopUp", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_List", Type = typeof(ListBox))]
    public class LucidTextBox : Control
    {
        static LucidTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LucidTextBox), new FrameworkPropertyMetadata(typeof(LucidTextBox)));
        }

        public LucidTextBox()
        {
            Loaded += OnLoaded;
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable), typeof(LucidTextBox));

        public IEnumerable Items
        {
            get
            {
                return (IEnumerable)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(LucidTextBox), new FrameworkPropertyMetadata(OnItemTemplateChanged));

        private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (PartList != null)
            {
                PartList.ItemTemplate = (DataTemplate)e.NewValue;
            }
            
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
       

        private Popup PartPopUp { get;  set; }
        private TextBox PartTextBox { get;  set; }
        private static ListBox PartList { get; set; }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            PartTextBox = GetTemplateChild("PART_TextBox") as TextBox;
            PartPopUp = GetTemplateChild("PART_PopUp") as Popup;
            PartList = GetTemplateChild("PART_List") as ListBox;
            if (PartPopUp != null)
            {
                PartPopUp.PlacementTarget = PartTextBox;
            }

            if (PartList != null)
            {
                PartList.KeyDown += OnPartListKeyDown;
            }

        }


        private void OnPartListKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Enter:
                    // Hide the Popup
                    PartPopUp.IsOpen = false;

                    ListBox lb = sender as ListBox;
                    if (lb == null)
                        return;
                    
                    // Get the selected item value
                    var country = lb.SelectedValue as Country ;

                    // Save the Caret position
                    var textToInsert =$"{country.Name}-{country.Code}:Lat:{country.Latitude} , lon:{country.Longitude}";

                    int i = PartTextBox.CaretIndex;
                    // Add text to the text
                    PartTextBox.Text = PartTextBox.Text.Insert(i, textToInsert);

                    // Move the caret to the end of the added text
                    PartTextBox.CaretIndex = i + textToInsert.Length;

                    // Move focus back to the text box.
                    // This will auto-hide the PopUp due to StaysOpen="false"
                    PartTextBox.Focus();
                    break;

                case System.Windows.Input.Key.Escape:
                    // Hide the Popup
                    PartPopUp.IsOpen = false;
                    break;
            }
        }

       
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            //if (e.Key != System.Windows.Input.Key.OemPeriod)
            //    return;
            if ((PartTextBox == null) || (PartTextBox.CaretIndex == 0))
                return;
            // Get the last word in the text (preceding the ".")
            string txt = PartTextBox.Text;
            int wordStart = txt.LastIndexOf(' ', PartTextBox.CaretIndex - 1);
            if (wordStart == -1)
                wordStart = 0;

            string lastWord = txt.Substring(wordStart, PartTextBox.CaretIndex - wordStart);



            // Check if the last word equal to the one we're waiting
            //if (lastWord.Trim().ToLower() != "item.")
            //    return;

            ShowPopup(PartTextBox.GetRectFromCharacterIndex(PartTextBox.CaretIndex, true));

           
        }

        private void ShowPopup(Rect placementRect)
        {
            PartPopUp.PlacementTarget = PartTextBox;
            PartPopUp.PlacementRectangle = placementRect;
            PartPopUp.IsOpen = true;
            PartPopUp.Visibility = Visibility.Visible;
        }
        private void HidePopup()
        {
            PartPopUp.IsOpen = false;
            PartPopUp.Visibility = Visibility.Collapsed;
        }
    }
}
