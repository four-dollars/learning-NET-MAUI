namespace Phoneword
{
    public partial class MainPage : ContentPage
    {

        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e) //判別使用者輸入確認是否啟用撥打按鈕
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            if(!string.IsNullOrEmpty(translatedNumber) )
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call "+translatedNumber;
            }
            else
            {
                CallButton.IsEnabled=false;
                CallButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e) //非同步作業
        {
            if (await this.DisplayAlert (
                "Dial a Number",
                "Would you like to call "+translatedNumber+ "?",
                "Yes",
                "No")) //標題、訊息、接受、取消
            {
                try //PhoneDialer類別:針對各平台提供抽象的電話撥號功能
                {
                    if (PhoneDialer.Default.IsSupported)
                    {
                        PhoneDialer.Default.Open(translatedNumber); //嘗試使用電話撥號程式撥打由參數所提供的號碼
                    }
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception) 
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
                

            }
        }
    }

}
