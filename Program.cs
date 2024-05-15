using System.Globalization;
using CsvHelper;
using HtmlAgilityPack;
using WebScraper;

// Initialize an empty list of our model, we will hydrate this during our scrape.
var pokemonList = new List<Pokemon>();
// Initlalize url
var scrapeUrl = "https://scrapeme.live/shop/";
// Initialization for HtmlAgility
var agilityWeb = new HtmlWeb();


// Todo Enque Pagination, get rid of the magic string
var currentPage = agilityWeb.Load(scrapeUrl);

// Grab all relevant items by QSA, this becomes our node
var node = currentPage.DocumentNode.QuerySelectorAll("li.product");

// Iterate over our node
foreach (var element in node) 
{ 
	// populate variables
	var name = HtmlEntity.DeEntitize(element.QuerySelector("h2").InnerText); 
	var url = HtmlEntity.DeEntitize(element.QuerySelector("a").Attributes["href"].Value); 
	var image = HtmlEntity.DeEntitize(element.QuerySelector("img").Attributes["src"].Value); 
	var price = HtmlEntity.DeEntitize(element.QuerySelector(".price").InnerText); 
	
    // to hydrate our List<Model>
	var pokemon = new Pokemon() { Name = name, Url = url, Image = image, Price = price};

	// now add them 
	pokemonList.Add(pokemon); 
}

// Output to console
foreach (var pokemon in pokemonList)
{
    Console.WriteLine($"Name: {pokemon.Name}\n Url: {pokemon.Url}\n Image: {pokemon.Image}\n Price: {pokemon.Price}");
}

// Initalize File 
using (var writer = new StreamWriter("pokemon.csv")) 

// Initialize CSV write
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) 
{ 
	// populating the CSV file 
	csv.WriteRecords(pokemonList); 
}




