namespace Service.Services;

using CrossCutting.Attributes;
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
                var propertyValue = property.GetValue(serverProperties);
                if(propertyValue != null)
                {
                    var propertyName = GetPropertyName(property);
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

    private string GetPropertyName(PropertyInfo prop)
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
