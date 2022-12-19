using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using NewNsns.Views;
using Newtonsoft.Json;
using Avalonia.ReactiveUI;
using Xunit;


namespace NewNsns.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    //TODO Загрузочный бар
    public async void ChangeMessage()
    {
        try
        {
            if (NutrientMode)
            {
                if (UserInput == null) return;
                
                await HttpTest(UserInput);
                
                Message = httpResponse;
                IsTboxVisible = true;
                
            }
            else
            {
                IsTboxVisible = true;
                Message = "Coming soon!";
            }
        }
        catch
        {
            IsTboxVisible = true;
            Message = "Error! Try Again :(";
        }
        
    }
    
    public void NutrientModChange()
    {
        if (!NutrientMode)
        {
            RecipeMode = false;
            NutrientMode = true;
        }
    }
    public void RecipeModChange()
    {
        if (!RecipeMode)
        {
            NutrientMode = false;
            RecipeMode = true;
        }
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

    private string _message;
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isTboxVisible;
    public bool IsTboxVisible
    {
        get => _isTboxVisible;
        set
        {
            _isTboxVisible = value;
            OnPropertyChanged();
        }
    }
    
    private bool _nutrientMode = true;

    public bool NutrientMode
    {
        get => _nutrientMode;
        set
        {
            _nutrientMode = value;
            OnPropertyChanged();
        } 
    }
    private bool _recipeMode = false;

    public bool RecipeMode
    {
        get => _recipeMode;
        set
        {
            _recipeMode = value;
            OnPropertyChanged();
        } 
    }
    
    class Food
    {
        public  string Calories { get; set; }
        
        public  string TotalWeight { get; set; }
        
        public TotalNutrients TotalNutrients { get; set; }
        
        public override string ToString()
        {
            return $"{Calories}: {TotalWeight}";
        }
    }

    class TotalNutrients
    {
        public Fat Fat { get; set; }
        public Sugar Sugar { get; set; }
        
        [JsonProperty("Chole")]
        public Cholesterol Cholesterol { get; set; }
        
        [JsonProperty("NA")]
        public Sodium  Sodium { get; set; }
        
        [JsonProperty("PROCNT")]
        public Protein Protein { get; set; }
        
        [JsonProperty("CHOCDF")]
        public Carbohydrate Carbohydrate { get; set; }
        
        [JsonProperty("FE")]
        public Iron Iron { get; set; }
    }

    class Fat
    {
        public string Quantity { get; set; }
    }

    class Sugar
    {
        public string Quantity { get; set; }
    }

    class Cholesterol
    {
        public string Quantity { get; set; }
    }

    class Sodium
    {
        public string Quantity { get; set; }
    }

    class Protein
    {
        public string Quantity { get; set; }
    }

    class Carbohydrate
    {
        public string Quantity { get; set; }
    }

    class Iron
    {
        public string Quantity { get; set; }
    }
    
    static async Task HttpTest(string userInput)
    {
        //Зачеем меня призвали?
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "https://api.edamam.com/api/nutrition-data?app_id=2be3daa4&app_key=dc6727a9535ff25838f65828408be9c8" +
                      "&nutrition-type=cooking" +
                      $"&ingr={userInput}";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            Food food = JsonConvert.DeserializeObject<Food>(resp);
        
            httpResponse = $"Calories: {food.Calories} kcal" +
                           $"\nFat: {food.TotalNutrients.Fat.Quantity} g" +
                           $"\nSugar: {food.TotalNutrients.Sugar.Quantity} g" +
                           $"\nCholesterol: {food.TotalNutrients.Cholesterol.Quantity} mg" +
                           $"\nSodium: {food.TotalNutrients.Sodium.Quantity} mg" +
                           $"\nProtein: {food.TotalNutrients.Protein.Quantity} g" +
                           $"\nCarbohydrate: {food.TotalNutrients.Carbohydrate.Quantity} g" +
                           $"\nIron: {food.TotalNutrients.Iron.Quantity} mg";
    }

    public ICommand OpenWebCommand { get; }
    
    public async void Contact()
    {
        
    }
    
}