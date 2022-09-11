using JsonSerializeDeserialize.Config;
using JsonSerializeDeserialize.Services;

ConsoleEx.WriteLine("Start program...", 0, true);
int step = 1;

// Create a List<Item> object and fill it with seed data.
var list1 = DataService.GetSeedData(Constants.NumRows);
ConsoleEx.WriteLine($"{step++}. Created List<Item> {nameof(list1)} and filled it with seed data...");
ConsoleEx.WriteLine($"   Number of items in {nameof(list1)}: {list1.Count:N0}", 1, true);

// Serialize a List<T> object to create a JSON file.
var jsonFilePath = JsonService.SerializeJson(list1, Constants.JsonFileName, Constants.RootName);
ConsoleEx.WriteLine($"{step++}. Serialized {nameof(list1)} and created a JSON file...");
ConsoleEx.WriteLine($"   File path: {jsonFilePath}", 1, true);

// Deserialize a JSON file to create a List<T> object.
var list2 = JsonService.DeserializeJson(jsonFilePath, Constants.RootName);
ConsoleEx.WriteLine($"{step++}. Deserialized the JSON file to create {nameof(list2)}...");
ConsoleEx.WriteLine($"   Number of items in {nameof(list2)}: {list2.Count:N0}", 1, true);

// Deserialize a JSON file to create a List<T> object using enumeration.
var list3 = JsonService.DeserializeJsonByEnumeration(jsonFilePath, Constants.RootName);
ConsoleEx.WriteLine($"{step++}. Deserialized the JSON file to create {nameof(list3)} using enumeration...");
ConsoleEx.WriteLine($"   Number of items in {nameof(list3)}: {list3.Count:N0}", 1, true);

// Compare two List<T> objects for equality.
var equal1 = DataService.CompareLists(list1, list2);
ConsoleEx.WriteLine($"{step++}. Compared {nameof(list1)} and {nameof(list2)}...");
ConsoleEx.WriteLine($"   Are {nameof(list1)} and {nameof(list2)} equal?: {equal1}", 1, true);

// Compare two List<T> objects for equality.
var equal2 = DataService.CompareLists(list1, list3);
ConsoleEx.WriteLine($"{step++}. Compared {nameof(list1)} and {nameof(list3)}...");
ConsoleEx.WriteLine($"   Are {nameof(list1)} and {nameof(list3)} equal?: {equal2}", 1, true);

// Serialize a List<T> object and create a CSV file.
var csvFilePath = JsonService.SerializeCsv(list1, Constants.CsvFileName);
ConsoleEx.WriteLine($"{step++}. Serialized {nameof(list1)} and created a CSV file...");
ConsoleEx.WriteLine($"   File path: {csvFilePath}", 1, true);

// Deserialize a CSV file and create a List<T> object.
var list4 = JsonService.DeserializeCsv(csvFilePath);
ConsoleEx.WriteLine($"{step++}. Deserialized the CSV file to create {nameof(list4)}...");
ConsoleEx.WriteLine($"   Number of items in {nameof(list4)}: {list4.Count:N0}", 1, true);

// Compare the two List<Item> objects.
var equal4 = DataService.CompareLists(list1, list4);
ConsoleEx.WriteLine($"{step++}. Compared {nameof(list1)} and {nameof(list4)}...");
ConsoleEx.WriteLine($"   Are {nameof(list1)} and {nameof(list4)} equal?: {equal4}", 1, true);

// Perform an analysis on a List<T> object using LINQ queries.
ConsoleEx.WriteLine($"{step++}. Perform a data analysis on the items in {nameof(list1)}...", 0, true);
DataService.PerformAnalysis(list1);

ConsoleEx.WriteLine("End program.", 0, false, true);
