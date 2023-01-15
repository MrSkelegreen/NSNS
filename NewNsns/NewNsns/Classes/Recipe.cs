using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Java.IO;
using Newtonsoft.Json;

namespace NewNsns.Classes;

public class JsonRecipe
{
    public int count { get; set; }
    public Hits[] hits { get; set; }
}

public class Hits
{
    public Recipe recipe { get; set; }
}

public class Recipe
{
    
    [JsonProperty ("label")]
    public string Title { get; set; }
    
    public string image { get; set; }
    
    
    [JsonProperty ("source")]
    public string Author { get; set; }
    
    public string url { get; set; }
    
    public string[] ingredientLines { get; set; }

    public double calories { get; set; }
    
    public double totalWeight { get; set; }
    
    public TotalNutrients totalNutrients { get; set; }
}



