using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewNsns.Views;
using Nito.AsyncEx;

namespace NewNsns.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{

    /*public MainViewModel()
    {
        InitializationNotifier = NotifyTaskCompletion.Create(HttpTest());
    }*/
    

    //public INotifyTaskCompletion InitializationNotifier { get; private set; }
    
    public void ChangeMessage()
    {
        TestMessage = "Чот появилось";

        HttpTest(UserInput);
        TestMessage = httpResponse;
    }

    private static string httpResponse;
    
    private  string _userInput;

    public string UserInput
    {
        get => _userInput;
        set
        {
            _userInput = value;
            OnPropertyChanged();
        }
    }
    
    
    private string _testMessage;
    public string TestMessage
    {
        get => _testMessage;
        set
        {
            _testMessage = value;
            OnPropertyChanged();
        }
    }
    
    class Food
    {
        public  string Calories { get; set; }
        
        public  string TotalWeight { get; set; }

        public override string ToString()
        {
            return $"{Calories}: {TotalWeight}";
        }
    }
    
    static async Task HttpTest(string userInput)
    {
        
        //Зачеем меня призвали?
        using var client = new HttpClient();
        
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var url = "https://api.edamam.com/api/nutrition-data?app_id=2be3daa4&app_key=dc6727a9535ff25838f65828408be9c8" +
                  "&nutrition-type=cooking" +
                  $"&ingr=200 g chocolate,\n200 g chicken";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var resp = await response.Content.ReadAsStringAsync();
        Food food = JsonConvert.DeserializeObject<Food>(resp);
        httpResponse = $"{food.Calories}, {food.TotalWeight}";
        

    }
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}