using CoffeeOrders.Core;
using System.Windows;

namespace CoffeeOrders.WpfApp
{
    public partial class OrderWindow : Window
    {
        public CoffeeOrder Order { get; private set; }

        public OrderWindow()
        {
            InitializeComponent();

            DrinkTypeComboBox.ItemsSource = Enum.GetValues<DrinkType>();
            DrinkSizeComboBox.ItemsSource = Enum.GetValues<DrinkSize>();
            StatusComboBox.ItemsSource = Enum.GetValues<OrderStatus>();

            DrinkTypeComboBox.SelectedIndex = 0;
            DrinkSizeComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndex = 0;
        }

        public OrderWindow(CoffeeOrder order) : this()
        {
            Title = "Edit Coffee Order";

            CustomerNameTextBox.Text = order.CustomerName;
            DrinkTypeComboBox.SelectedItem = order.DrinkType;
            DrinkSizeComboBox.SelectedItem = order.DrinkSize;
            QuantityTextBox.Text = order.Quantity.ToString();
            StatusComboBox.SelectedItem = order.Status;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkTypeComboBox.SelectedItem == null ||
                DrinkSizeComboBox.SelectedItem == null ||
                StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.SelectOptionsMessage);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show(Properties.Resources.InvalidQuantityMessage);
                return;
            }

            DrinkType drinkType =
                (DrinkType)DrinkTypeComboBox.SelectedItem;

            DrinkSize drinkSize =
                (DrinkSize)DrinkSizeComboBox.SelectedItem;

            OrderStatus status =
                (OrderStatus)StatusComboBox.SelectedItem;

            try
            {
                Order = OrderLogic.CreateOrder(
                    CustomerNameTextBox.Text,
                    drinkType,
                    drinkSize,
                    quantity,
                    status);

                DialogResult = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show(
                    Properties.Resources.InvalidQuantityMessage);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(
                    Properties.Resources.LargeEspressoMessage);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(
                    Properties.Resources.InvalidNameMessage);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}