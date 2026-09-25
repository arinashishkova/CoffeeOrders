using CoffeeOrders.Core;
using System.Windows;

namespace CoffeeOrders.WpfApp
{
    public partial class MainWindow : Window
    {
        private CoffeeOrder[] orders = new CoffeeOrder[100];
        private int count = 0;
        private int[] visibleOrderIndexes = new int[100];

        public MainWindow()
        {
            InitializeComponent();

            RefreshDataGrid();
            UpdateSummary();
        }

        private void RefreshDataGrid()
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            CoffeeOrder[] filteredOrders = new CoffeeOrder[count];
            int filteredCount = 0;

            for (int i = 0; i < count; i++)
            {
                string customerName = orders[i].CustomerName.ToLower();

                if (searchText == "" || customerName.Contains(searchText))
                {
                    filteredOrders[filteredCount] = orders[i];
                    visibleOrderIndexes[filteredCount] = i;
                    filteredCount++;
                }
            }

            CoffeeOrder[] visibleOrders = new CoffeeOrder[filteredCount];

            for (int i = 0; i < filteredCount; i++)
            {
                visibleOrders[i] = filteredOrders[i];
            }

            OrdersDataGrid.ItemsSource = visibleOrders;
        }

        private void UpdateSummary()
        {
            decimal total = 0;

            for (int i = 0; i < count; i++)
            {
                total += orders[i].Price;
            }

            SummaryTextBlock.Text =
                $"Orders: {count} | Total: €{total:F2}";
        }

        // ADD button
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (count >= orders.Length)
            {
                MessageBox.Show(Properties.Resources.OrdersFullMessage);
                return;
            }

            OrderWindow dialog = new OrderWindow();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                orders[count] = dialog.Order;
                count++; 

                RefreshDataGrid();
                UpdateSummary();
            }
        }

        // EDIT button
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedViewIndex = OrdersDataGrid.SelectedIndex;

            if (selectedViewIndex < 0)
            {
                MessageBox.Show(Properties.Resources.NoSelectionMessage);
                return;
            }

            int selectedIndex = visibleOrderIndexes[selectedViewIndex];

            CoffeeOrder selectedOrder = orders[selectedIndex];

            OrderWindow dialog = new OrderWindow(selectedOrder);
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                orders[selectedIndex] = dialog.Order;

                RefreshDataGrid();
                UpdateSummary();
            }
        }

        // DELETE button
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedViewIndex = OrdersDataGrid.SelectedIndex;

            if (selectedViewIndex < 0)
            {
                MessageBox.Show(Properties.Resources.NoSelectionMessage);
                return;
            }

            int selectedIndex = visibleOrderIndexes[selectedViewIndex];

            MessageBoxResult answer = MessageBox.Show(
                Properties.Resources.DeleteConfirmMessage,
                Properties.Resources.DeleteConfirmTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (answer != MessageBoxResult.Yes)
            {
                return;
            }

            // changing inex by -1
            for (int i = selectedIndex; i < count - 1; i++)
            {
                orders[i] = orders[i + 1];
            }

            count--;
            orders[count] = default;

            RefreshDataGrid();
            UpdateSummary();
        }

        private void SearchTextBox_TextChanged(
            object sender,
            System.Windows.Controls.TextChangedEventArgs e)
        {
            RefreshDataGrid();
        }
    }
}