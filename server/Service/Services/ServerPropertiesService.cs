namespace Service.Services;

using CrossCutting.Attributes;
using Domain;
using Domain.Enums;
using Service.InputModels;
using Service.IServices;
using Service.Services.Util;
using System.Reflection;
using System.Text.RegularExpressions;

public class ServerPropertiesService : IServerPropertiesService
{
    public void ChangeServerProperties(ServerPropertiesInputModel serverProperties, string serverName)
    {
        var filePath = Path.Combine(PersistenceUtil.GetServerPath(serverName), "server.properties");
        // Read in the file 
        if (File.Exists(filePath)){
            string fileContents = File.ReadAllText(filePath);


        Type classType = serverProperties.GetType();
        foreach (PropertyInfo property in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var propertyValue = property.GetValue(serverProperties);
            if (propertyValue != null)
            {
                    var propertyName = GetPropertyAttributeName(property);
                    // Use a regular expression to find the line with the property we want to change
                    string pattern = $"{propertyName}=(.*)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(fileContents);

                    if (match.Success)
                    {
                        // Replace the old value with the new value
                        string oldValue = match.Groups[1].Value;
                        string newLine = $"{propertyName}={propertyValue}";
                        fileContents = fileContents.Replace($"{propertyName}={oldValue}", newLine);
                    }
                    else
                    {
                        throw new ArgumentException($"Property {property.Name} not found in file {filePath}");
                    }
                }
            } // Write the updated file contents back to the file
            File.WriteAllText(filePath, fileContents);
        }
    }

    public ServerProperties ParseServerProperties(string filePath)
    {
        var fileServerProperties = GetServerPropertiesFromFile(filePath);
        return ParseKeyValuePairListToServerProperties(fileServerProperties);
    }

    private ServerProperties ParseKeyValuePairListToServerProperties(List<KeyValuePair<string, string>> fileServerProperties)
    {
        var serverProperties = new ServerProperties();
        Type classType = serverProperties.GetType();
        foreach (PropertyInfo property in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var matchingProperty = fileServerProperties.Where(p => p.Key == GetPropertyAttributeName(property)).FirstOrDefault();
            if (matchingProperty.Key != null)
            {
                var propertyType = property.PropertyType;
               
                try
                {
                    object convertedValue;
                    if (propertyType.IsEnum)
                    {
                        convertedValue = Enum.Parse(propertyType, matchingProperty.Value);
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(matchingProperty.Value, propertyType);
                    }
                   
                    property.SetValue(serverProperties, convertedValue);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error parsing property value for {property.Name}: {ex.Message}");
                }
            }
        }

        return serverProperties;
    }
    
    private List<KeyValuePair<string, string>> GetServerPropertiesFromFile(string filePath)
    {
        var fileContents = File.ReadAllLines(filePath);
        var fileServerProperties = new List<KeyValuePair<string, string>>();
        foreach (var line in fileContents)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            {
                continue;
            }

            var keyValue = line.Split('=');
            if (keyValue.Length == 2)
            {
                fileServerProperties.Add(new KeyValuePair<string, string>(keyValue[0], keyValue[1]));
            }
        }
        return fileServerProperties;
    }


    private string GetPropertyAttributeName(PropertyInfo prop)
    {
        // Get the JsonPropertyName attribute for the property
        ServerPropertyNameAttribute attribute = (ServerPropertyNameAttribute)prop.GetCustomAttribute(typeof(ServerPropertyNameAttribute));

        // Get the value of the attribute
        if (attribute != null)
        {
            return attribute.Name;
        }
        else
        {
            throw new Exception($"No JsonPropertyNameAttribute found on {prop.Name}");
        }
    }
}
