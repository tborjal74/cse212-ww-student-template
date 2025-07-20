using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();
        foreach (var word in words)
        {
            var reversed = new string(word.Reverse().ToArray());
            if (wordSet.Contains(reversed) && word != reversed)
            {
                pairs.Add($"{word} & {reversed}");
                wordSet.Remove(word); // Avoid duplicates
                wordSet.Remove(reversed); // Avoid duplicates
            }
        }
        return pairs.ToArray();  // Convert the list of pairs to an array and return it
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            var degree = fields[3].Trim(); // Assuming the degree is in the 4th column (index 3)
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1; // Initialize count for this degree
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: remove spaces and make lowercase
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ", "");

        // If lengths differ, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        var charCount = new Dictionary<char, int>();

        // Count characters in word1
        foreach (var c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Subtract counts using word2
        foreach (var c in word2)
        {
            if (!charCount.ContainsKey(c))
                return false;
            charCount[c]--;
            if (charCount[c] < 0)
                return false;
        }

        // If all counts are zero, it's an anagram
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>public static string[] EarthquakeDailySummary()
public static string[] EarthquakeDailySummary()
{
    const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
    using var client = new HttpClient();
    using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
    using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
    using var reader = new StreamReader(jsonStream);
    var json = reader.ReadToEnd();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

    // Handles null or missing features
    if (featureCollection?.Features == null || featureCollection.Features.Count == 0)
        return new string[] { "No earthquakes reported today." };

    // Only include earthquakes with a valid magnitude
    var summaries = featureCollection.Features
        .Where(f => f?.Properties?.Mag != null)
        .Select(f =>
        {
            var place = string.IsNullOrWhiteSpace(f.Properties.Place) ? "Unknown" : f.Properties.Place;
            var mag = f.Properties.Mag.Value.ToString();
            return $"Place: {place} - Mag {mag}";
        })
        .ToArray();

    if (summaries.Length == 0)
        return new string[] { "No earthquakes reported today." };

    if (summaries.Length > 100)
        summaries = summaries.Take(100).ToArray();

    Array.Sort(summaries);
    return summaries;
}
}