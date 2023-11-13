// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
CacheProvider.CacheProvider<int, string> instance = CacheProvider.CacheProvider<int, string>.GetCacheInstance(5);
string val = string.Empty;
instance.AddItem(23, "Af", out val); ;
instance.AddItem(1,"As",out val);
instance.AddItem(2, "Aso",out val);
instance.AddItem(3, "As3",out val);
instance.AddItem(4, "Ask",out val);
instance.GetItem(23);
instance.AddItem(11, "Ask", out val);
Console.WriteLine(val);
Console.ReadLine();