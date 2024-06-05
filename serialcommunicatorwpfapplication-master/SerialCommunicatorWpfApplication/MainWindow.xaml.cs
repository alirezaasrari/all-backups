using System;
using System.Windows;
using System.IO.Ports;
using System.Collections.ObjectModel;
using SerialCommunicatorWpfApplication.Tools;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using SerialCommunicatorWpfApplication.Model;
using System.Text.Json;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Context = SerialCommunicatorWpfApplication.DataContext.Context;
using Microsoft.Win32;
using System.IO;
using System.Data;
using ExcelDataReader;
using System.Windows.Documents;
namespace SerialCommunicatorWpfApplication
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> item;
        public ObservableCollection<string> itemoperator;
        public ObservableCollection<string> itemsim;
        public ObservableCollection<string> dcuType;
        public ObservableCollection<List<ProductionPanel>> simlist;
        public MainWindow()
        {          
            InitializeComponent();
            dcuType = new ObservableCollection<string>()
            {
                "Digital Sahmab",
                "MBUS"
            };
            item = new ObservableCollection<string>()
            { 
                "Open Session", "Close Session",
                "Get DCU Setting", "Set DCU Setting",
                "Get Meter Setting", "Set Meter Setting",
                "Clear A Meter From DCU", "Get Valve Setting",
                "Set Valve Setting",
                "Set DateTime Setting", "Get DateTime Setting",
                "Set FWU Parameters", "FWU Verify",
                "FWU Complete", "Get DCU Data"
            };
            itemoperator = new ObservableCollection<string>()
            {
                "ایرانسل", "همراه اول", "رایتل", "شاتل"
            };
            itemsim = new ObservableCollection<string>()
            {
                "اعتباری", "دایمی", "دیتا",
            };
            myComboBox.ItemsSource = item;
            SelectDcuType.ItemsSource = dcuType;
            OperatorComboBox.ItemsSource = itemoperator;
            SimComboBox.ItemsSource = itemsim;
            Sendd.IsEnabled = false;
            CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
        }
        private async Task LoadDataAsync()
        {
            try
            {
                using Context context = new Context();
                List<ProductionPanel> data = await context.ProductionPanel.ToListAsync();
                ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(data);
                productionPanelList.ItemsSource = simlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        private async Task LoadLastTenConnectionDataAsync(long num)
        {
            try
            {
                teneventLoading.Visibility = Visibility.Visible;
                using Context context = new Context();
                List<TerminalDataModel> data = await context.NewTerminal.Where(a=>a.DataLoggerSerialInDecimal == num).Take(10).OrderByDescending(b=>b.Date).ToListAsync();
                ObservableCollection<TerminalDataModel> terminaldata = new ObservableCollection<TerminalDataModel>(data);
                tenlastconnection.ItemsSource = terminaldata;
                teneventLoading.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                teneventLoading.Visibility= Visibility.Collapsed;
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        public async void LastTenConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long dcunumber = Int64.Parse(dcunum.Text);
                await LoadLastTenConnectionDataAsync(dcunumber);
            }
            catch
            {
                MessageBox.Show("some problem occured");
            }
        }
        private async Task LoadLastTenEventDataAsync(long num)
        {
            try
            {
                tenconnectionLoading.Visibility = Visibility.Visible;
                using Context context = new Context();
                List<TerminalDataModel> data = await context.NewTerminal.Where(a => a.DataLoggerSerialInDecimal == num).Where(b=>b.CommandCode == "a6").Take(10).OrderByDescending(b => b.Date).ToListAsync();
                ObservableCollection<TerminalDataModel> terminaldata = new ObservableCollection<TerminalDataModel>(data);
                tenlastevent.ItemsSource = terminaldata;
                tenconnectionLoading.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                tenconnectionLoading.Visibility = Visibility.Collapsed;
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        public async void LastTenEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long dcunumber = Int64.Parse(dcunum.Text);
                await LoadLastTenEventDataAsync(dcunumber);
            }
            catch
            {
                MessageBox.Show("some problem occured");
            }
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (e.Command == ApplicationCommands.Copy ||
                    e.Command == ApplicationCommands.Cut ||
                    e.Command == ApplicationCommands.Paste)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public readonly SerialPort port = new SerialPort("COM4", 2400, Parity.Even, 8, StopBits.One);       
        public void Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                loading.Visibility = Visibility.Collapsed;
                loadinglabel.Visibility = Visibility.Collapsed;
                settingpanel.Visibility = Visibility.Collapsed;
                myComboBox.SelectedItem = null;
                packett.Text = string.Empty;
                responsee.Text = string.Empty;
                meaning.Text = string.Empty;
                Sendd.IsEnabled = false;
                ValueInput1.Text = string.Empty;
                ValueInput2.Text = string.Empty;
                ValueInput3.Text = string.Empty;
                ValueInput4.Text = string.Empty;
                ValueInput5.Text = string.Empty;
                ValueInput6.Text = string.Empty;
                ValueInput7.Text = string.Empty;
                ValueInput8.Text = string.Empty;
                ValueInput9.Text = string.Empty;
                ValueInput10.Text = string.Empty;
                ValueInput11.Text = string.Empty;
                ValueInput12.Text = string.Empty;
                port.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void TextChangeHandler(object sender, TextChangedEventArgs args)
        {
            try
            {
                if (UserName.Text == null || UserName.Text.Length != 11)
                {
                    Login.IsEnabled = false;
                }
                else
                {
                    Login.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void TextChangeHandler2(object sender, TextChangedEventArgs args)
        {
            try
            {
                if (Otp.Text == null || Otp.Text.Length != 5)
                {
                    LogIn.IsEnabled = false;
                }
                else
                {
                    LogIn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }           
        }
        public void TextChangeHandler3(object sender, TextChangedEventArgs args)
        {
            try
            {
                if (Number.Text == null || Number.Text.Length != 11)
                {
                    AddSimCardDone.IsEnabled = false;
                }
                else
                {
                    AddSimCardDone.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public async void ButtonLogin_Click(object sender, EventArgs e)
        {         
            try
            {
                if (Captcha.Text == CaptchaGenerator.Text)
                {
                    loginloading.Visibility = Visibility.Visible;
                    LoadingLogin.Visibility = Visibility.Visible;
                    try
                    {
                        HttpClient client = new HttpClient();
                        var url = new Uri("http://api.sahmab.co/accounts/login");
                        string credential = UserName.Text;
                        var requestData = new LoginRequest()
                        {
                            credential = credential
                        };
                        string requestBody = JsonSerializer.Serialize(requestData);

                        HttpResponseMessage response = await client.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            LoginResponseMode res = JsonSerializer.Deserialize<LoginResponseMode>(responseBody);
                            if (res.error == 0)
                            {
                                loginloading.Visibility = Visibility.Collapsed;
                                LoadingLogin.Visibility = Visibility.Collapsed;
                                FirstPage.Visibility = Visibility.Hidden;
                                SecondPage.Visibility = Visibility.Visible;
                                settingpanel.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                loginloading.Visibility = Visibility.Collapsed;
                                LoadingLogin.Visibility = Visibility.Collapsed;
                                CaptchaCheck.Foreground = Brushes.Red;
                                CaptchaCheck.Content = "لطفا شماره تلفن صحیح را وارد نمایید";
                            }
                        }
                        else
                        {
                            Captcha.Text = string.Empty;
                            CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                            loginloading.Visibility = Visibility.Collapsed;
                            LoadingLogin.Visibility = Visibility.Collapsed;
                            CaptchaCheck.Foreground = Brushes.Red;
                            CaptchaCheck.Content = "لطفا شماره تلفن صحیح را وارد نمایید";
                        }
                    }
                    catch (Exception)
                    {
                        Captcha.Text = string.Empty;
                        CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                        Otp.Text = string.Empty;
                        UserName.Text = string.Empty;
                        loginloading.Visibility = Visibility.Collapsed;
                        LoadingLogin.Visibility = Visibility.Collapsed;
                        CaptchaCheck.Foreground = Brushes.Red;
                        CaptchaCheck.Content = "لطفا شماره تلفن صحیح را وارد نمایید";
                    }
                }
                else
                {
                    Captcha.Text = string.Empty;
                    Otp.Text = string.Empty;
                    UserName.Text = string.Empty;
                    CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                    loginloading.Visibility = Visibility.Collapsed;
                    LoadingLogin.Visibility = Visibility.Collapsed;
                    CaptchaCheck.Foreground = Brushes.Red;
                    CaptchaCheck.Content = "لطفا کد امنیتی را بصورت صحیح وارد نمایید";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            try
            {
                ScrollViewer scv = (ScrollViewer)sender;
                scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
                e.Handled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void AddSimCard_Click(object sender, EventArgs e)
        {
            try
            {
                SimList.Visibility = Visibility.Collapsed;
                Modal.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        private async void AddSimCardDone_Click(object sender, EventArgs e)
        {
            try
            {
                AddSimLoading.Visibility = Visibility.Visible;
                using Context context = new Context();
                ProductionPanel productionPanel = new ProductionPanel
                {
                    Number = Number.Text,
                    Serial = Serial.Text,
                    Owner = Owner.Text,
                    Operator = OperatorComboBox.SelectedItem.ToString(),
                    Type = SimComboBox.SelectedItem.ToString(),
                    CreatedAt = DateTime.Now,
                };
                await context.AddAsync(productionPanel);
                await context.SaveChangesAsync();
                List<ProductionPanel> allPanels = await context.ProductionPanel.ToListAsync();
                AddSimLoading.Visibility = Visibility.Collapsed;
                Serial.Text = null;
                Owner.Text = null;
                OperatorComboBox.SelectedItem = null;
                SimComboBox.SelectedItem = null;
                Number.Text = null;
                ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                productionPanelList.ItemsSource = simlist;
                SimList.Visibility = Visibility.Visible;
                Modal.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        private async void AddGroupSimButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xlsm;*.xlsb)|*.xlsx;*.xlsm;*.xlsb";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        var parsedData = ExcelDataReader.ExcelReaderFactory.CreateReader(fileStream);
                        var dataSet = parsedData.AsDataSet();
                        List<ProductionPanel> dataList = new List<ProductionPanel>();
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            ProductionPanel dataModel = new ProductionPanel()
                            {
                                Number = row["Column1"].ToString(),
                                Operator = row["Column2"].ToString(),
                                Serial = row["Column3"].ToString(),
                                Owner = row["Column4"].ToString(),
                                Type = row["Column5"].ToString(),
                                CreatedAt = null,
                                DeletedAt = null,
                                UpdatedAt = null,
                            };
                            dataList.Add(dataModel);
                        }
                        using var context = new Context();
                        foreach (var data in dataList)
                        {
                            context.Add(data);
                        }
                        await context.SaveChangesAsync();
                        List<ProductionPanel> allPanels = await context.ProductionPanel.ToListAsync();
                        ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                        productionPanelList.ItemsSource = simlist;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }                    
        }
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchLoading.Visibility = Visibility.Visible;
                var searchcase = search.Text;
                using Context context = new Context();
                List<ProductionPanel> allPanels = await context.ProductionPanel.Where(a => a.Number == searchcase).ToListAsync();
                ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                productionPanelList.ItemsSource = simlist;
                searchLoading.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private async void UpdateDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(id.Text == null || id.Text == "")
                {
                    MessageBox.Show("لطفا ای دی مورد مورد ویرایش یا حذف را وارد کنید");                  
                }
                else
                {
                    using Context context = new Context();
                    ProductionPanel caseid = await context.ProductionPanel.Where(a => a.Id == Int64.Parse(id.Text)).FirstOrDefaultAsync();
                    if(caseid != null) 
                    {
                        context.ProductionPanel.Remove(caseid);
                        await context.SaveChangesAsync();
                        List<ProductionPanel> allPanels = await context.ProductionPanel.ToListAsync();
                        ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                        productionPanelList.ItemsSource = simlist;
                        id.Text = "";
                    }
                    else
                    {
                        id.Text = "";
                        MessageBox.Show("شماره وارد شده صحیح نمیباشد");  
                    }                                
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public async void RangeDelete_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                if(from.Text != null || from.Text != "" && to.Text != null || to.Text != "")
                {
                    int From = int.Parse(from.Text);
                    int To = int.Parse(to.Text);
                    using Context context = new Context();
                    List<ProductionPanel> List = await context.ProductionPanel.Where(a => a.Id >= From && a.Id <= To).ToListAsync();
                    if(List.Count == 0)
                    {
                        MessageBox.Show("بازه صحیح نمیباشد");
                    }
                    else
                    {
                        context.RemoveRange(List);
                        await context.SaveChangesAsync();
                        List<ProductionPanel> allPanels = await context.ProductionPanel.ToListAsync();
                        ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                        productionPanelList.ItemsSource = simlist;
                        from.Text = "";
                        to.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("لطفا بازه حذف را انتخاب کنید");
                }

            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchLoading.Visibility = Visibility.Visible;
                using Context context = new Context();
                List<ProductionPanel> allPanels = await context.ProductionPanel.ToListAsync();
                ObservableCollection<ProductionPanel> simlist = new ObservableCollection<ProductionPanel>(allPanels);
                productionPanelList.ItemsSource = simlist;
                searchLoading.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public async void ButtonLogin2_Click(object sender, EventArgs e)
        {          
            login2loading.Visibility = Visibility.Visible;
            LoadingLogin2.Visibility = Visibility.Visible;
            try
            {
                HttpClient client2 = new HttpClient();
                var url2 = new Uri("http://api.sahmab.co/accounts/validate-login");
                string credential2 = UserName.Text;
                string otp = Otp.Text;
                var requestData2 = new LoginResponseOtp() 
                {
                    credential = credential2,
                    code = otp
                };
                string requestBody2 = JsonSerializer.Serialize(requestData2);

                HttpResponseMessage response2 = await client2.PostAsync(url2, new StringContent(requestBody2, Encoding.UTF8, "application/json"));
                if (response2.IsSuccessStatusCode)
                {
                    string responseBody2 = await response2.Content.ReadAsStringAsync();
                    ValidateLoginResponse res = JsonSerializer.Deserialize<ValidateLoginResponse>(responseBody2);
                    bool hasaccess = res.data.user.roles.Any(a => a.title == "admin");
                    var NName = "";
                    var Lname = "";
                    if(res.data.user.first_name != null)
                    {
                        NName = res.data.user.first_name;
                    }
                    else
                    {
                        NName = "";
                    }
                    if (res.data.user.last_name != null)
                    {
                        Lname = res.data.user.last_name;
                    }
                    else
                    {
                        Lname = "";
                    }                   
                        if (res.error == 0)
                        {                          
                            login2loading.Visibility = Visibility.Collapsed;
                            LoadingLogin2.Visibility = Visibility.Collapsed;
                            FirstPage.Visibility = Visibility.Hidden;
                            SecondPage.Visibility = Visibility.Hidden;
                            ThirdPage.Visibility = Visibility.Hidden;
                            ThirdPageScroll.Visibility = Visibility.Hidden;
                            MainPage.Visibility = Visibility.Visible;
                            Hello.Visibility = Visibility.Visible;
                            Hello.Content = $"سلام {NName} {Lname}";
                            settingpanel.Visibility = Visibility.Collapsed;
                            SimList.Visibility = Visibility.Visible;
                            MainPage.Visibility = Visibility.Visible;
                            dashboord.Visibility = Visibility.Visible;
                            simtable.Visibility = Visibility.Collapsed;
                            headersimtable.Visibility = Visibility.Collapsed;
                            await LoadDataAsync();
                        }
                        else
                        {
                            CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                            Captcha.Text = string.Empty;
                            login2loading.Visibility = Visibility.Collapsed;
                            LoadingLogin2.Visibility = Visibility.Collapsed;
                            OtpCheck.Foreground = Brushes.Red;
                            OtpCheck.Visibility = Visibility.Visible;
                            OtpCheck.Content = "!ورود به سامانه ناموفق بود";
                        }
                }
                else
                {
                    Captcha.Text = string.Empty;
                    CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                    Otp.Text = string.Empty;
                    login2loading.Visibility = Visibility.Collapsed;
                    LoadingLogin2.Visibility = Visibility.Collapsed;
                    OtpCheck.Foreground = Brushes.Red;
                    OtpCheck.Visibility = Visibility.Visible;
                    OtpCheck.Content = "!لطفا رمز یکبار مصرف را بصورت صحیح وارد نمایید";
                }
            }
            catch (Exception)
            {
                Captcha.Text = string.Empty;
                CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                Otp.Text = string.Empty;
                login2loading.Visibility = Visibility.Collapsed;
                LoadingLogin2.Visibility = Visibility.Collapsed;
                FirstPage.Visibility = Visibility.Visible;
                SecondPage.Visibility = Visibility.Hidden;
                OtpCheck.Visibility = Visibility.Collapsed;
                CaptchaCheck.Foreground = Brushes.Red;
                CaptchaCheck.Content = "!خطای ناشناس در حین ورود به سامانه لطفا دوباره تلاش نمایید";
            }         
        }
        public void ButtonSimCard_Click(object sender, EventArgs e)
        {
            try
            {
                FirstPage.Visibility = Visibility.Hidden;
                SecondPage.Visibility = Visibility.Hidden;
                ThirdPage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Hidden;
                ThirdPageScroll.Visibility = Visibility.Hidden;
                settingpanel.Visibility = Visibility.Collapsed;
                SimList.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        public void ButtonSetting_Click(object sender, EventArgs e)
        {
            try
            {
                DcuPrimarySet.Visibility = Visibility.Collapsed;
                TestToServerReview.Visibility = Visibility.Collapsed;
                ProductDetailSubmitContainer.Visibility = Visibility.Collapsed;
                other.Visibility = Visibility.Collapsed;
                CardContainer.Visibility = Visibility.Visible;
                FirstPage.Visibility = Visibility.Hidden;
                SecondPage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Hidden;
                ThirdPageScroll.Visibility = Visibility.Visible;
                ThirdPage.Visibility = Visibility.Visible;
                simbtn.Background = Brushes.WhiteSmoke;
                settingpanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }
        public void ButtonDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                dashboord.Visibility = Visibility.Visible;
                headersimtable.Visibility = Visibility.Collapsed;
                simtable.Visibility = Visibility.Collapsed;
                simbtn.Background = Brushes.WhiteSmoke;
                dashbtn.Background = Brushes.Aqua;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ButtonSim_Click(object sender, EventArgs e)
        {
            try
            {
                headersimtable.Visibility = Visibility.Visible;
                simtable.Visibility = Visibility.Visible;
                SimList.Visibility = Visibility.Visible;
                dashboord.Visibility = Visibility.Visible;
                dashbtn.Background = Brushes.WhiteSmoke;
                Setting.Background = Brushes.WhiteSmoke;
                simbtn.Background = Brushes.Aqua;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ButtonLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                FirstPage.Visibility = Visibility.Visible;
                SecondPage.Visibility = Visibility.Hidden;
                ThirdPage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Hidden;
                ThirdPageScroll.Visibility = Visibility.Hidden;
                settingpanel.Visibility = Visibility.Collapsed;
                UserName.Text = "";
                Otp.Text = "";
                Captcha.Text = "";
                LogIn.IsEnabled = false;
                Login.IsEnabled = false;
                CaptchaGenerator.Text = RandomCaptchaGenerators.RandomCaptchaGenerator(5);
                port.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        public void Undo2_Click(object sender, EventArgs e)
        {
            try
            {
                UserName.Text = string.Empty;
                FirstPage.Visibility = Visibility.Visible;
                SecondPage.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        public void Undo_Click(object sender, EventArgs e)
        {
            try
            {
                dashbtn.Background = Brushes.Aqua;
                headersimtable.Visibility = Visibility.Collapsed;
                simtable.Visibility = Visibility.Collapsed;
                simbtn.Background = Brushes.WhiteSmoke;
                FirstPage.Visibility = Visibility.Hidden;
                SecondPage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Visible;
                dashboord.Visibility = Visibility.Visible;
                SimList.Visibility = Visibility.Visible;
                ThirdPage.Visibility = Visibility.Hidden;
                ThirdPageScroll.Visibility = Visibility.Hidden;
                settingpanel.Visibility = Visibility.Collapsed;
                responsee.Text = string.Empty;
                meaning.Text = string.Empty;
                packett.Text = string.Empty;
                ValueInput1.Text = string.Empty;
                ValueInput2.Text = string.Empty;
                ValueInput3.Text = string.Empty;
                ValueInput4.Text = string.Empty;
                ValueInput5.Text = string.Empty;
                ValueInput6.Text = string.Empty;
                ValueInput7.Text = string.Empty;
                ValueInput8.Text = string.Empty;
                ValueInput9.Text = string.Empty;
                ValueInput10.Text = string.Empty;
                ValueInput11.Text = string.Empty;
                ValueInput12.Text = string.Empty;
                myComboBox.SelectedItem = null;
                port.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }         
        }
        public void UndoFromCardContainer(object sender, RoutedEventArgs e)
        {
            try
            {
                dashbtn.Background = Brushes.Aqua;
                headersimtable.Visibility = Visibility.Collapsed;
                simtable.Visibility = Visibility.Collapsed;
                simbtn.Background = Brushes.WhiteSmoke;
                FirstPage.Visibility = Visibility.Hidden;
                SecondPage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Visible;
                dashboord.Visibility = Visibility.Visible;
                SimList.Visibility = Visibility.Visible;
                ThirdPage.Visibility = Visibility.Hidden;
                ThirdPageScroll.Visibility = Visibility.Hidden;
                settingpanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {

                MessageBox.Show("something went wrong!");
            }
        }
        public void SetPrimaryDcu(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility = Visibility.Collapsed;
                DcuPrimarySet.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void UndoFromTestToServerReview(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility = Visibility.Visible;
                TestToServerReview.Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("something wrong happened!");
            }
        }
        public async void GenerateSerial_Click(object sender, EventArgs e)
        {
            if(SelectDcuType.SelectedItem != null)
            {
                try
                {
                    int TypeDCU;
                    if (SelectDcuType.SelectedItem.ToString() == "Digital Sahmab")
                    {
                        TypeDCU = 3;
                    }
                    else if (SelectDcuType.SelectedItem.ToString() == "MBUS")
                    {
                        TypeDCU = 2;
                    }
                    else
                    {
                        TypeDCU = 0;
                    }
                    serialloading.Visibility = Visibility.Visible;
                    HttpClient client = new HttpClient();
                    var url = new Uri($"http://172.16.1.38:8089/SerialGenerator?type={TypeDCU}");
                    HttpResponseMessage response = await client.GetAsync(url.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        SerialReceivedFromApi.Text = res;
                        serialloading.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        serialloading.Visibility = Visibility.Collapsed;
                        MessageBox.Show("api problem");
                    };
                }
                catch (Exception ex)
                {
                    serialloading.Visibility = Visibility.Collapsed;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("please select one value from the combobox for the dcu type");
            }      
        }
        public void TestConnectionToServer(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility = Visibility.Collapsed;
                TestToServerReview.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void SubmitProductDetail(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility = Visibility.Collapsed;
                ProductDetailSubmitContainer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void UndoFromPrimaryDcuSetting(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility= Visibility.Visible;
                DcuPrimarySet.Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("something wrong happened!");
            }
        }
        public void UndoFromProductDetailSubmitContainer(object sender, RoutedEventArgs e)
        {
            try
            {
                CardContainer.Visibility = Visibility.Visible;
                ProductDetailSubmitContainer.Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("something wrong happened!");
            }
        }
        public void UndoModal_Click(object sender, EventArgs e)
        {
            try
            {
                SimList.Visibility = Visibility.Visible;
                Modal.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }       
        }
        public void Change_Combo(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (myComboBox.SelectedItem != null)
                {
                    Sendd.IsEnabled = true;
                    if (myComboBox.SelectedItem.ToString() == "Set DCU Setting")
                    {
                        settingpanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        settingpanel.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    Sendd.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }          
        }
        private async void ButtonSend_Click(object sender, EventArgs e)
        {         
            try
            {
                string cmd = myComboBox.SelectedItem.ToString();               
                if (cmd == "Open Session")
                {
                    port.Close();
                    port.Open();
                    string packet = PacketGenerator.GeneratePacket(cmd);
                    byte[] packetToSend = StringToByteArray.StringToByteArrayy(packet);
                    packett.Text = BitConverter.ToString(packetToSend).Replace("-", "");
                    port.Write(packetToSend, 0, packetToSend.Length);
                    loadinglabel.Visibility = Visibility.Visible;
                    loading.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    loadinglabel.Visibility = Visibility.Collapsed;
                    loading.Visibility = Visibility.Collapsed;
                    byte[] response = new byte[port.BytesToRead];
                    int bytesToRead = port.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        int bytesRead = port.Read(response, 0, Math.Min(bytesToRead, response.Length));                     
                        responsee.Text = BitConverter.ToString(response).Replace("-", "");
                        var res = BitConverter.ToString(response).Replace("-", "");
                        meaning.Text = HexToString.HexToStringConvert(res.Substring(18, res.Length - 22));
                        ValueInput1.Text = meaning.Text.Split(':')[1].Split(',')[0];
                        ValueInput3.Text = meaning.Text.Split(':')[3].Split(',')[0];
                        ValueInput4.Text = meaning.Text.Split(':')[4].Split(',')[0];
                    }
                    else
                    {
                        responsee.Text = "no response received";
                    }
                }
                else
                {
                    settingpanel.Visibility = Visibility.Collapsed;
                    string packet = PacketGenerator.GeneratePacket2(cmd, ValueInput1.Text, ValueInput2.Text, ValueInput3.Text
                    , ValueInput4.Text, ValueInput5.Text, ValueInput6.Text, ValueInput7.Text, ValueInput8.Text, ValueInput9.Text
                    , ValueInput10.Text, ValueInput11.Text, ValueInput12.Text);
                    byte[] packetToSend = StringToByteArray.StringToByteArrayy(packet);
                    packett.Text = BitConverter.ToString(packetToSend).Replace("-", "");
                    port.Write(packetToSend, 0, packetToSend.Length);
                    loadinglabel.Visibility = Visibility.Visible;
                    loading.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    loadinglabel.Visibility = Visibility.Collapsed;
                    loading.Visibility = Visibility.Collapsed;
                    byte[] response = new byte[port.BytesToRead];
                    int bytesToRead = port.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        int bytesRead = port.Read(response, 0, Math.Min(bytesToRead, response.Length));
                        responsee.Text = BitConverter.ToString(response).Replace("-", "");
                        var res = BitConverter.ToString(response).Replace("-", "");
                        meaning.Text = HexToString.HexToStringConvert(res.Substring(18, res.Length - 22));
                    }
                    else
                    {
                        responsee.Text = "!پاسخی دریافت نشد";
                    }
                }                             
            }
            catch (Exception ex)
            {
                loading.Visibility = Visibility.Hidden;
                loadinglabel.Visibility = Visibility.Hidden;
                responsee.Text = ex.Message;
            }
        }
    }
}
