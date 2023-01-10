using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Graphics.Pdf;
using Android.Provider;
using AsyncImageLoader;
using NewNsns.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using ReactiveUI;
using Process = System.Diagnostics.Process;


namespace NewNsns.ViewModels;

public class MainViewModel :  INotifyPropertyChanged, IReactiveObject
{
    public ObservableCollection<Hits> Recipes { get; set; }
    public MainViewModel()
    {
        GetUserInput = ReactiveCommand.Create<string>(ChangeMessage);
        IsPrBarVisible = false;
        IsRecipesVisible = false;

        string url = "asyncImageLoader:ImageLoader.Source=\"http://almode.ru/uploads/posts/2020-10/1603445421_55-p-kianu-rivz-73.jpg\"";
        Recipe recipe = new Recipe(){Title = "Яйца на сале", image = url};
        Hits hit = new Hits(){recipe = recipe};

        Recipes = new ObservableCollection<Hits>{};
        //Recipes.Add(hit);
    }

    public ICommand GetUserInput { get; }
    
    private async void ChangeMessage(string usrInput)
    {
        if (string.IsNullOrEmpty(usrInput))
        {
            IsRecipesVisible = false;
            IsTboxVisible = true;
            Message = "Field is empty";
        }
        else
        {
            if (NutrientMode)
            {
                IsRecipesVisible = false;
                InitializationNotifier = NotifyTaskCompletion.Create(GetNutritionResponse(usrInput));
                IsPrBarVisible = true;
                await GetNutritionResponse(usrInput);
                IsPrBarVisible = false;
                
                Message = httpResponse;
                IsTboxVisible = true;
            
            }
            else
            {
                IsTboxVisible = false;
                IsRecipesVisible = false;
                IsPrBarVisible = true;
            
                Recipes.Clear();
                try
                {
                
                    await GetRecipeResponse(usrInput);
                    IsPrBarVisible = false;
                    foreach (var h in jsonRecipe.hits)
                    {
                        Recipes.Add(h);
                    }

                    IsRecipesVisible = true;
                }
                catch
                {
                    IsRecipesVisible = false;
                    IsTboxVisible = true;
                    Message = "Nothing found :(";
                }
                IsPrBarVisible = false;
            }
        }
    }
    
    static async Task GetNutritionResponse(string userInput)
    {
        using var client = new HttpClient();
        try
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "https://api.edamam.com/api/nutrition-data?app_id=2be3daa4&app_key=dc6727a9535ff25838f65828408be9c8" +
                      "&nutrition-type=cooking" +
                      $"&ingr={userInput}";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            Food food = JsonConvert.DeserializeObject<Food>(resp);
            
        
            httpResponse = $"Calories: {Math.Round(food.Calories,2)} kcal" +
                           $"\nFat: {Math.Round(food.TotalNutrients.Fat.Quantity,2)} g" +
                           $"\nSugar: {Math.Round(food.TotalNutrients.Sugar.Quantity, 2)} g" +
                           $"\nCholesterol: {Math.Round(food.TotalNutrients.Cholesterol.Quantity,2)} mg" +
                           $"\nSodium: {Math.Round(food.TotalNutrients.Sodium.Quantity,2)} mg" +
                           $"\nProtein: {Math.Round(food.TotalNutrients.Protein.Quantity, 2)} g" +
                           $"\nCarbohydrate: {Math.Round(food.TotalNutrients.Carbohydrate.Quantity,2)} g" +
                           $"\nIron: {Math.Round(food.TotalNutrients.Iron.Quantity,2)} mg";
        }
        catch
        {
            httpResponse = "Error! :(";
        }
        
    }

    static async Task GetRecipeResponse(string userInput)
    {
        using var client = new HttpClient();
        
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "https://api.edamam.com/api/recipes/v2?type=public&" +
                      $"q={userInput}" +
                      "&app_id=39c3b6ba&app_key=ea2a12a0ccc033922ff35b00b1a0af33";
            
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            jsonRecipe = JsonConvert.DeserializeObject<JsonRecipe>(resp);

            if (jsonRecipe.count < 1)
            {
                throw new Exception("Nothing found");
            }
    }

    private static JsonRecipe jsonRecipe { get; set; }
    
    public void Contact()
    {
        var url = "https://vk.com/forbess357";
        
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
    
    private string? _userInput;

    public string? UserInput
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

    public ICommand OpenWebCommand { get; }

    private bool _isPrBarVisible;

    public bool IsPrBarVisible
    {
        get => _isPrBarVisible;
        set
        {
            _isPrBarVisible = value;
            OnPropertyChanged();
        }
    }
    private bool _isRecipesVisible;

    public bool IsRecipesVisible
    {
        get => _isRecipesVisible;
        set
        {
            _isRecipesVisible = value;
            OnPropertyChanged();
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    
    private INotifyTaskCompletion InitializationNotifier { get; set; }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        
    }
}