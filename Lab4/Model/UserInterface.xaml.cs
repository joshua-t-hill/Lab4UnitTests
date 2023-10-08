//using CommunityToolkit.Maui.Alerts;
//using CommunityToolkit.Maui.Core;
//using CommunityToolkit.Maui.Views;
//using CS341Lab3.Model;

//namespace CS341Lab3;
///// <summary>
///// Handles the main page and its controls.
///// </summary>
//public partial class UserInterface : ContentPage
//{
//    public UserInterface()
//    {
//        InitializeComponent();

//        BindingContext = MauiProgram.BusinessLogic; //"brains"
//    }

//    /// <summary>
//    /// Handles AddAirport button click event.
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    void AddAirport_Clicked(System.Object sender, System.EventArgs e)
//    {
//        ErrorHandling result = MauiProgram.BusinessLogic.AddAirport(IdENT.Text, CityENT.Text, DateENT.Text, RatingENT.Text);

//        if (result.ErrorType != Constants.ErrorType.NoError) //error check
//        {
//            _ = DisplayAlert("An error occurred:", result.Message, "OK");
//        }

//        //clear text in user input fields
//        IdENT.Text = "";
//        CityENT.Text = "";
//        DateENT.Text = "";
//        RatingENT.Text = "";

//        IdENT.Focus();
//    }

//    /// <summary>
//    /// Handles DeleteAirport button click event.
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    void DeleteAirport_Clicked(System.Object sender, System.EventArgs e)
//    {
//        var click = sender as Button; //get row that clicked delete button belongs to

//        if (click.BindingContext is Airport selectedAirport) //ignore event if no airport is selected; prevents crashing
//        {
//            ErrorHandling result = MauiProgram.BusinessLogic.DeleteAirport(selectedAirport.Id);

//            if (result.ErrorType != Constants.ErrorType.NoError) //error check
//            {
//                _ = DisplayAlert("An error occurred:", result.Message, "OK");
//            }
//        }
//    }

//    /// <summary>
//    /// Handles EditAirport button click event.
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    async void EditAirport_ClickedAsync(System.Object sender, System.EventArgs e)
//    {
//        Airport selectedAirport = CA.SelectedItem as Airport; //grab selected airport row

//        var popup = new EditAirportPopup(selectedAirport);
//        EditAirportUserInput response = (EditAirportUserInput)await this.ShowPopupAsync(popup);

//        if (response != null) //null indicates that the "Cancel" button was clickd on the Edit popup
//        {
//            ErrorHandling result = //result =.NoError if successful
//                MauiProgram.BusinessLogic.EditAirport
//                (
//                    selectedAirport.Id,
//                    response.City,
//                    response.Date,
//                    response.Rating
//                );

//            if (result.ErrorType != Constants.ErrorType.NoError) //error check
//            {
//                _ = DisplayAlert("An error occurred:", result.Message, "OK");
//            }
//        }
//    }

//    /// <summary>
//    /// Handles the CalculateStatistics button click event.
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    void CalculateStatistics_Clicked(object sender, EventArgs e)
//    {
//        CancellationTokenSource cancellationTokenSource = new();

//        //Use the Toast functionality to display the generated string form BusinessLogic.CalculateStatistics() call
//        var toast = Toast.Make(MauiProgram.BusinessLogic.CalculateStatistics(), ToastDuration.Long);
//        toast.Show(cancellationTokenSource.Token);
//    }
//}