using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewNsns.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    //Метод меняет сообщение на экране
    public virtual async void ChangeMessage()
    {
        if (UserInput == null)
        {
            Message = "Field is empty.\n" +
                      "Type something like: 200 g chicken";
        }
        else
        {
            await HttpTest(UserInput);
            Message = httpResponse;
        }
    }
    //В эту переменную помещается json ответ
    protected static string httpResponse;
    
    //В эту переменную помещается пользовательский ввод
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

    //Сообщение для вывода на экран
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

    //Класс для парсинга json ответа
    public class Food
    {
        public  string Calories { get; set; }
        public TotalNutrients TotalNutrients { get; set; }
        
    }

    public class TotalNutrients
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

    public class Fat
    {
        public string Quantity { get; set; }
    }

    public class Sugar
    {
        public string Quantity { get; set; }
    }

   public class Cholesterol
    {
        public string Quantity { get; set; }
    }

    public class Sodium
    {
        public string Quantity { get; set; }
    }

   public  class Protein
    {
        public string Quantity { get; set; }
    }

    public class Carbohydrate
    {
        public string Quantity { get; set; }
    }

    public class Iron
    {
        public string Quantity { get; set; }
    }

    //Метод для получения json ответа и его парсинга
    private async Task HttpTest(string userInput)
    {
        //Вызов http клиента
        using var client = new HttpClient();
        
        //Ссылка на запрос с пользовательским вводом в виде параметра &ingr
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = "https://api.edamam.com/api/nutrition-data?app_id=2be3daa4&app_key=dc6727a9535ff25838f65828408be9c8" +
                      "&nutrition-type=cooking" +
                      $"&ingr={userInput}";
            try
            {
                //Отправка запроса
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                
                //Парсинг json ответа
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
            catch
            {
                httpResponse = "Error! :(\n" +
                               "Try something like: 200 g chicken";
            }
    }
}