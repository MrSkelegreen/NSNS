using Newtonsoft.Json;

namespace NewNsns.Classes;

class Food
{
    public double Calories { get; set; }
        
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
    public double Quantity { get; set; }
}

public class Sugar
{
    public double Quantity { get; set; }
}

public class Cholesterol
{
    public double Quantity { get; set; }
}

public class Sodium
{
    public double Quantity { get; set; }
}

public class Protein
{
    public double Quantity { get; set; }
}

public class Carbohydrate
{
    public double Quantity { get; set; }
}

public class Iron
{
    public double Quantity { get; set; }
}