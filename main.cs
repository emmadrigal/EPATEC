using System;
using System.Xml;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

class piece{
    public string name;
    public int size;
}

class Product{
    public string Name;
    public int Price;
    public List<piece> pieces;
}

class principal {
      static void Main(){

        Product product = new Product();

        product.Name = "Apple";
        product.Price = 3;

        piece firstPiece = new piece();
        piece secondpiece = new piece();
        piece thirdPiece = new piece();

        firstPiece.name = "Llanta";
        firstPiece.size = 3;

        secondpiece.name = "Rueda";
        secondpiece.size = 5;

        thirdPiece.name = "Capote";
        thirdPiece.size = -2;

        List<piece> pieces = new List<piece>();
        pieces.Add(firstPiece);
        pieces.Add(secondpiece);
        pieces.Add(thirdPiece);

        product.pieces = pieces;

        string output = JsonConvert.SerializeObject(product);
        Product product2 = JsonConvert.DeserializeObject<Product>(output);
        Console.WriteLine(output);
        string json = "{\"empleado\" : { \"id\" : 1234}}";
        Console.WriteLine(json);
        

        JObject employee = JObject.Parse(json);//Convierte json a employee
        Console.WriteLine(employee.Properties().Select(p => p.Name).FirstOrDefault());


        JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
        Console.WriteLine("{" + results.ToString() + "}");
        empleado Empleado = JsonConvert.DeserializeObject<empleado>("{" + results.ToString() + "}");//Lo parsea hacia empleado
        Console.Write(Empleado.id);

        Console.ReadKey();

    }
}

class empleado
{
    public int id;  
}
